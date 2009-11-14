package idv.trans.service.admin;

import idv.trans.model.Userinfo;
import idv.trans.model.UserinfoDAO;
import idv.trans.service.system.SystemVar;
import idv.trans.struts.action.User;
import idv.trans.util.SpringUtil;

import java.lang.reflect.InvocationTargetException;
import java.util.Date;
import java.util.Iterator;
import java.util.LinkedHashMap;
import java.util.List;

import org.apache.commons.beanutils.BeanUtils;
import org.apache.commons.lang.time.DateUtils;
import org.hibernate.criterion.DetachedCriteria;
import org.hibernate.criterion.Restrictions;

public class UserService {

	public void init(Object bean, Short role, Short priority)
			throws IllegalAccessException, InvocationTargetException {

		SystemVar var = (SystemVar) SpringUtil.getBean("SystemVar");

		if (priority == null || priority.equals(Short.parseShort("0"))) {
			BeanUtils.setProperty(bean, "permissionRole", var
					.getSystemPermission());
		} else {
			LinkedHashMap old_prioritys = (LinkedHashMap) var
					.getSystemPermission().clone();

			LinkedHashMap prioritys = (LinkedHashMap) var.getSystemPermission()
					.clone();

			for (Iterator i = (Iterator) old_prioritys.keySet().iterator(); i
					.hasNext();) {
				String key = (String) i.next();
				if (!key.equals(priority.toString())) {
					prioritys.remove(key);
				}
			}

			BeanUtils.setProperty(bean, "permissionRole", prioritys);
		}

		if (role.toString().equals("1")) {

			// user.setUserRole(var.getUserLevel());
			BeanUtils.setProperty(bean, "userRole", var.getUserLevel());

		} else {
			LinkedHashMap roles = (LinkedHashMap) var.getUserLevel().clone();
			roles.remove("1");
			roles.remove("2");

			// user.setUserRole(roles);

			BeanUtils.setProperty(bean, "userRole", roles);

		}

		// user.setPermissionRole(prioritys);
	}

	public String insertUser(User user) throws Exception {
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

				if (1 == checkUID(user.getUserinfo().getUid().toCharArray())) {
					throw new Exception("身分證字號不合法");
				}

			}
			//
			// 如果沒問題~那就資料建一建寫入吧
			dao.save(newUser);

			return "ADD_SUCCESS";

		} else {
			// UPDATE
			Userinfo oldUser = dao.findById(newUser.getUserid());

			if (!user.getUserinfo().getUid().equals("")) {

				if (!user.getUserinfo().getUid().equals(oldUser.getUid())) {

					if (dao.findByUid(user.getUserinfo().getUid()).size() > 0) {
						throw new Exception("身分證字號重覆");
					}

					if (1 == checkUID(user.getUserinfo().getUid().toCharArray())) {
						throw new Exception("身分證字號不合法");
					}
				}

			}

			// 把值COPY一下
			if (newUser.getPassword().equals("")) {
				newUser.setPassword(oldUser.getPassword());
				newUser.setPwdexpiredate(oldUser.getPwdexpiredate());
			} else {
				newUser.setPwdexpiredate(new Date());
			}

			dao.attachDirty(newUser);
			return "UPDATE_SUCCESS";
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

	public List<Userinfo> findAllUsers(User user) {
		UserinfoDAO dao = (UserinfoDAO) SpringUtil.getBean("UserinfoDAO");

		// 設定條件
		// 查詢條件
		DetachedCriteria criteria = DetachedCriteria.forClass(Userinfo.class);

		try {
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

				criteria.add(Restrictions.eq("role", user.getUserinfo()
						.getRole()));

			}

			// 權限

			if (user.getUserinfo().getPriority() != null) {

				criteria.add(Restrictions.eq("priority", user.getUserinfo()
						.getPriority()));

			}
		} catch (Exception ex) {
			// 查詢條件的問題

		}
		// 從USER去設定

		List<Userinfo> users = (List<Userinfo>) (dao.getHibernateTemplate()
				.findByCriteria(criteria));

		return users;
	}

	public void changePassword(User user) throws Exception {
		// 更新一下密碼
		UserinfoDAO dao = (UserinfoDAO) SpringUtil.getBean("UserinfoDAO");

		Userinfo olduser = dao.findById(user.getUserinfo().getUserid());

		try {
			if (olduser.getPassword().equals(user.getOldPassword())) {
				// 存檔
				olduser.setPassword(user.getUserinfo().getPassword());

				// 過期日+60天
				Date expire = DateUtils.addDays(new Date(), 60);

				olduser.setPwdexpiredate(expire);

				dao.attachDirty(olduser);
			} else {
				throw new Exception("密碼錯誤");
			}
		} catch (Exception ex) {
			throw new Exception("密碼錯誤");
		}

	}

	public int checkUID(char pass[]) {
		int rc = 0, dex = 0;
		String[] n = { "10", "11", "12", "13", "14", "15", "16", "17", "34",
				"18", "19", "20", "21", "22", "35", "23", "24", "25", "26",
				"27", "28", "29", "32", "30", "31", "33" };
		String idaccept = "";
		char cc;
		idaccept = "";
		if (!Character.isLetter(pass[0])) {
			rc = 1;
			return (rc);
		}
		for (int i = 1; i <= 9; i++) {
			if (!Character.isDigit(pass[i])) {
				rc = 1;
				return (rc);
			}
		}
		for (int i = 0; i <= 9; i++) {
			// System.out.println("ID"+i+":"+pass[i]); //測試用@.@
			idaccept = idaccept + pass[i];
		}
		cc = Character.toUpperCase(pass[0]); // 將值過來的值轉換成大寫
		dex = ((int) cc) - 65; // 將大寫的英文字母轉換成10進位碼並減65對應到n陣列的index值
		String D0 = n[dex];
		int D00 = Integer.parseInt(D0.substring(0, 1));
		int D01 = Integer.parseInt(D0.substring(1, 2));
		int D1 = Integer.parseInt(idaccept.substring(1, 2));
		int D2 = Integer.parseInt(idaccept.substring(2, 3));
		int D3 = Integer.parseInt(idaccept.substring(3, 4));
		int D4 = Integer.parseInt(idaccept.substring(4, 5));
		int D5 = Integer.parseInt(idaccept.substring(5, 6));
		int D6 = Integer.parseInt(idaccept.substring(6, 7));
		int D7 = Integer.parseInt(idaccept.substring(7, 8));
		int D8 = Integer.parseInt(idaccept.substring(8, 9));
		int D9 = Integer.parseInt(idaccept.substring(9));
		int CheckCode = 10 - (((D00 * 1) + (D01 * 9) + (D1 * 8) + (D2 * 7)
				+ (D3 * 6) + (D4 * 5) + (D5 * 4) + (D6 * 3) + (D7 * 2) + (D8 * 1)) % 10);
		if (CheckCode != D9) {
			rc = 1;
		}
		return (rc);
	}

}
