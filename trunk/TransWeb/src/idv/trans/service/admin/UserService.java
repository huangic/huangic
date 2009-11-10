package idv.trans.service.admin;

import idv.trans.model.UserinfoDAO;
import idv.trans.service.system.SystemVar;
import idv.trans.struts.action.User;
import idv.trans.util.SpringUtil;

public class UserService {
   
	public void init(User user){
		
		SystemVar var=(SystemVar)SpringUtil.getBean("SystemVar");
		user.setUserRole(var.getUserLevel());
		user.setPermissionRole(var.getSystemPermission());
	}
	
	public void insertUser(User user){
		//檢查一下帳號跟身分證吧
		UserinfoDAO dao=(UserinfoDAO)SpringUtil.getBean("UserinfoDAO");
		//
		
	}
}
