package idv.trans.service.admin;

import idv.trans.model.Userinfo;
import idv.trans.model.UserinfoDAO;
import idv.trans.service.system.SystemVar;
import idv.trans.struts.action.User;
import idv.trans.util.SpringUtil;

import java.util.Date;
import java.util.Iterator;
import java.util.LinkedHashMap;
import java.util.List;

import org.apache.commons.lang.time.DateUtils;
import org.hibernate.criterion.DetachedCriteria;
import org.hibernate.criterion.Restrictions;

public class UserService {
	
	
	public void init(User user, Short role, Short priority) {

		SystemVar var = (SystemVar) SpringUtil.getBean("SystemVar");

		if (role.toString().equals("1")) {

			user.setUserRole(var.getUserLevel());
			user.setPermissionRole(var.getSystemPermission());
		} else {
			LinkedHashMap roles = (LinkedHashMap) var.getUserLevel().clone();
			LinkedHashMap old_prioritys = (LinkedHashMap) var
					.getSystemPermission().clone();

			LinkedHashMap prioritys = (LinkedHashMap) var.getSystemPermission()
					.clone();
			roles.remove("1");
			roles.remove("2");

			for (Iterator i = (Iterator) old_prioritys.keySet().iterator(); i
					.hasNext();) {
				String key = (String) i.next();
				if (!key.equals(priority.toString())) {
					prioritys.remove(key);
				}
			}

			user.setUserRole(roles);
			user.setPermissionRole(prioritys);
		}
	}

	public void insertUser(User user) throws Exception {
		UserinfoDAO dao = (UserinfoDAO) SpringUtil.getBean("UserinfoDAO");

		
		// 如果沒有ID那就檢查
		Userinfo newUser = user.getUserinfo();

		if (newUser.getUserid() == null) {

			// 檢查一下帳號跟身分證吧
			if (dao.findByAccount(user.getUserinfo().getAccount()).size() > 0) {
				throw new Exception("帳號重覆");
			}
			;

			if (!user.getUserinfo().getUid().equals("")) {

				if (dao.findByUid(user.getUserinfo().getUid()).size() > 0) {
					throw new Exception("身分證字號重覆");
				}
				;
			}
			//
			// 如果沒問題~那就資料建一建寫入吧
			dao.save(newUser);
		} else {
			// UPDATE
			Userinfo oldUser = dao.findById(newUser.getUserid());
			// 把值COPY一下
			if (newUser.getPassword().equals("")) {
				newUser.setPassword(oldUser.getPassword());
				newUser.setPwdexpiredate(oldUser.getPwdexpiredate());
			} else {
				newUser.setPwdexpiredate(new Date());
			}

			dao.attachDirty(newUser);

		}

	}

	public void findUser(User user, Integer id) throws Exception {
		UserinfoDAO dao = (UserinfoDAO) SpringUtil.getBean("UserinfoDAO");

		
		Userinfo userinfo = dao.findById(id);

		if (userinfo == null) {
			throw new Exception("無此使用者");
		} else {
			user.setUserinfo(userinfo);
		}

	}

	public void findAllUsers(User user) {
		UserinfoDAO dao = (UserinfoDAO) SpringUtil.getBean("UserinfoDAO");

		// 設定條件
		// 查詢條件
		DetachedCriteria criteria = DetachedCriteria.forClass(Userinfo.class);

		// 使用者帳號
		if (!user.getUserinfo().getAccount().equals("")) {
			criteria.add(Restrictions.eq("account", user.getUserinfo()
					.getAccount()));

		}

		// 使用者名稱
		if (!user.getUserinfo().getUsername().equals("")) {
			criteria.add(Restrictions.like("username", user.getUserinfo()
					.getUsername()));

		}

		// 角色
		if (user.getUserinfo().getRole() != null) {

			criteria.add(Restrictions.eq("role", user.getUserinfo().getRole()));

		}

		// 權限

		if (user.getUserinfo().getPriority() != null) {

			criteria.add(Restrictions.eq("priority", user.getUserinfo()
					.getPriority()));

		}

		// 從USER去設定

		List<Userinfo> users = (List<Userinfo>) (dao.getHibernateTemplate()
				.findByCriteria(criteria));

		user.setUsers(users);
	}

	public void changePassword(User user) throws Exception {
		// 更新一下密碼
		UserinfoDAO dao = (UserinfoDAO) SpringUtil.getBean("UserinfoDAO");

		Userinfo olduser = dao.findById(user.getUserinfo().getUserid());

		try {
			if (olduser.getPassword().equals(user.getOldPassword())) {
                //存檔
				olduser.setPassword(user.getUserinfo().getPassword());
				
				//過期日+100天
				Date expire=DateUtils.addDays(new Date(), 100);
				
				 olduser.setPwdexpiredate(expire);
				
				dao.attachDirty(olduser);
			}else{
				throw new Exception("密碼錯誤");
			}
		} catch (Exception ex) {
            throw new Exception("密碼錯誤");
		}

	}

}
