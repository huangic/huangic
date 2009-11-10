package idv.trans.struts.action;

import idv.trans.model.Message;
import idv.trans.model.SessionUserInfo;
import idv.trans.model.Userinfo;
import idv.trans.service.admin.UserService;
import idv.trans.util.SpringUtil;

import java.util.LinkedHashMap;
import java.util.List;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.HttpSession;

import org.apache.struts2.ServletActionContext;

public class User {
	UserService service=(UserService)SpringUtil.getBean("UserService");
	
	private  List<Userinfo> users;


   private Message message=null;



	private LinkedHashMap permissionRole;
	//權限MAP



private Userinfo userinfo;


	//角色MAP
	private LinkedHashMap userRole;
	
	public String addForm(){
		//初始化
		
		
		
		init();
		return "SUCCESS";
		
	}

	public String edit(){
		init();
		
		try{
		
		//FIND USER by pk
			service.findUser(this, this.getUserinfo().getUserid());
		return "SUCCESS";
		}catch(Exception ex){
			
		return "ERROR";
		}
	}

	public Message getMessage() {
		return message;
	}

	
	
	public LinkedHashMap getPermissionRole() {
		return permissionRole;
	}

	
	
	public Userinfo getUserinfo() {
		return userinfo;
	}
	
	


	public LinkedHashMap getUserRole() {
		return userRole;
	}
	
	
	public List<Userinfo> getUsers() {
	return this.users;
}
	public String save(){
		try{
		service.insertUser(this);
		return "SUCCESS";
		}catch(Exception ex){
			
		return "ERROR";
		}
		
		
	}
	
	public String search(){
		//將條件送到SERVICE
		
		try{
		service.findAllUsers(this);
		return "SUCCESS";
		}catch(Exception ex){
			
		return "ERROR";
		}
	}
	
	
	
	
	
	
	

	

	

	

	

	public void setMessage(Message message) {
		this.message = message;
	}

	public void setPermissionRole(LinkedHashMap permissionRole) {
		this.permissionRole = permissionRole;
	}

	public void setUserinfo(Userinfo userinfo) {
		this.userinfo = userinfo;
	}
	
	
	

	
	
	public void setUserRole(LinkedHashMap userRole) {
		this.userRole = userRole;
	}
	
	
	public void setUsers(List<Userinfo> users) {
		this.users = users;
	}
	
	public String toSearch(){
		
		
		init();
		
		return "SUCCESS";
	}
	
	private void init(){
		HttpServletRequest request = ServletActionContext.getRequest();
    	HttpServletResponse response = ServletActionContext.getResponse();
    	HttpSession session = request.getSession();
    	SessionUserInfo userInfo=(SessionUserInfo) session.getAttribute("UserInfo");
    	
    	service.init(this, userInfo.getUserInfo().getRole(), userInfo.getUserInfo().getPriority());
    	
    	
	}
	
}
