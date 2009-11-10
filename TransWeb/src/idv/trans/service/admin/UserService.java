package idv.trans.service.admin;

import idv.trans.model.Userinfo;
import idv.trans.model.UserinfoDAO;
import idv.trans.service.system.SystemVar;
import idv.trans.struts.action.User;
import idv.trans.util.SpringUtil;

import java.util.Iterator;
import java.util.LinkedHashMap;
import java.util.List;
import java.util.Map.Entry;

import org.hibernate.criterion.DetachedCriteria;

public class UserService {

	public void init(User user, Short role, Short priority) {

		SystemVar var = (SystemVar) SpringUtil.getBean("SystemVar");

		if (role.toString().equals("1")) {

			user.setUserRole(var.getUserLevel());
			user.setPermissionRole(var.getSystemPermission());
		}else{
			LinkedHashMap roles=(LinkedHashMap)var.getUserLevel().clone();
			LinkedHashMap old_prioritys=(LinkedHashMap)var.getSystemPermission().clone();
			
			LinkedHashMap prioritys=(LinkedHashMap)var.getSystemPermission().clone();
			roles.remove("1");
			roles.remove("2");
			
			for(Iterator i=(Iterator) old_prioritys.keySet().iterator();i.hasNext();){
				String key=(String)i.next();
				if(!key.equals(priority.toString())){
					prioritys.remove(key);
				}
			}
			
			user.setUserRole(roles);
			user.setPermissionRole(prioritys);
		}
	}

	public void insertUser(User user) {
		// 檢查一下帳號跟身分證吧

		UserinfoDAO dao = (UserinfoDAO) SpringUtil.getBean("UserinfoDAO");
		//
		// 如果沒問題~那就資料建一建寫入吧

		Userinfo newUser = user.getUserinfo();

		dao.save(newUser);

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

		// 設定條件

		// 查詢條件
		DetachedCriteria criteria = DetachedCriteria.forClass(Userinfo.class);

		// 從USER去設定

		UserinfoDAO dao = (UserinfoDAO) SpringUtil.getBean("UserinfoDAO");

		List<Userinfo> users = (List<Userinfo>) (dao.getHibernateTemplate()
				.findByCriteria(criteria));

		user.setUsers(users);
	}

}
