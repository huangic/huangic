package idv.trans.service.admin;

import java.util.Date;

import idv.trans.model.Userinfo;
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
		//如果沒問題~那就資料建一建寫入吧
		
		
		
		Userinfo newUser=new Userinfo();
		newUser.setAccount(user.getAccount());
		newUser.setDept(user.getDept());
		newUser.setEmail(user.getEmail());
		newUser.setNote(user.getNote());
		newUser.setPassword(user.getPassword());
		newUser.setPriority(user.getPriority());
		newUser.setPwdexpiredate(new Date());
		newUser.setRole(user.getRole());
		newUser.setStatus(user.getStatus());
		newUser.setTel(user.getTel());
		newUser.setUid(user.getUid());
		//newUser.setUserid(userid);
		newUser.setUsername(user.getUsername());
		
		dao.save(newUser);
		
		
	}
}
