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
	UserService service = (UserService) SpringUtil.getBean("UserService");

	private boolean redirect;
	
	private List<Userinfo> users;

	private Message message;

	public String getOldPassword() {
		return oldPassword;
	}

	public void setOldPassword(String oldPassword) {
		this.oldPassword = oldPassword;
	}

	private String oldPassword;

	private LinkedHashMap permissionRole;
	// 權限MAP

	private Userinfo userinfo;

	// 角色MAP
	private LinkedHashMap userRole;

	public String addForm() {
		// 初始化

		init();
		return "SUCCESS";

	}

	public String edit() {
		init();

		try {

			// FIND USER by pk
			service.findUser(this, this.getUserinfo().getUserid());
			return "SUCCESS";
		} catch (Exception ex) {

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

	public String save() {
		init();
		try {

			service.insertUser(this);

			// 存檔後轉條列業
			return "LIST";

		} catch (Exception ex) {

			this.message = new Message();
			this.message.setErrorMessage(ex.getMessage());

			return "ADD_ERROR";
		}

	}

	public String search() {
		// 將條件送到SERVICE

		try {
			service.findAllUsers(this);
			return "SUCCESS";
		} catch (Exception ex) {
			ex.printStackTrace();
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

	public String toSearch() {

		init();

		return "SUCCESS";
	}

	private void init() {
		HttpServletRequest request = ServletActionContext.getRequest();
		HttpServletResponse response = ServletActionContext.getResponse();
		HttpSession session = request.getSession();
		SessionUserInfo userInfo = (SessionUserInfo) session
				.getAttribute("UserInfo");

		service.init(this, userInfo.getUserInfo().getRole(), userInfo
				.getUserInfo().getPriority());

	}

	
	public String changePwd(){
		
		return "SUCCESS";
	}
	
	public String changePwdSave(){
		try{
		service.changePassword(this);
		
		HttpServletRequest request = ServletActionContext.getRequest();
    	HttpServletResponse response = ServletActionContext.getResponse();
    	HttpSession session = request.getSession();
		
		session.removeAttribute("UserInfo");
		
		this.message=new Message("密碼修改後，請重新登入!");
		this.redirect=true;
		
		return "SUCCESS";
		}catch(Exception ex){
		
			this.message=new Message(ex.getMessage());
			
		return "ERROR";
		}
	}

	public boolean isRedirect() {
		return redirect;
	}

	public void setRedirect(boolean redirect) {
		this.redirect = redirect;
	}
	
	
}
