package idv.trans.struts.action;

import idv.trans.model.Message;
import idv.trans.service.admin.UserService;
import idv.trans.service.system.SystemVar;
import idv.trans.util.SpringUtil;

import java.util.Date;
import java.util.LinkedHashMap;

public class User {
    
	private String account;
	
	private Message message=null;
	
	private String dept;

	private String email;

	private String note;

	private String password;



	private LinkedHashMap permissionRole;
	//權限MAP
	
	private Short priority;
	
	private Date pwdexpiredate;
	private Short role;
	private Short status;
	private String tel;
	private String uid;
	private Integer userid;
	private String username;
	//角色MAP
	private LinkedHashMap userRole;
	public void init(){
		UserService service=(UserService)SpringUtil.getBean("UserService");
		
		service.init(this);
		
		
	
	}
	
	public String addForm(){
		//初始化
		init();		
		return "SUCCESS";
		
	}
	public String getAccount() {
		return account;
	}
	public String getDept() {
		return dept;
	}
	public String getEmail() {
		return email;
	}
	
	
	
	
	
	public String getNote() {
		return note;
	}

	public String getPassword() {
		return password;
	}

	public LinkedHashMap getPermissionRole() {
		return permissionRole;
	}

	public Short getPriority() {
		return priority;
	}

	public Date getPwdexpiredate() {
		return pwdexpiredate;
	}

	public Short getRole() {
		return role;
	}

	public Short getStatus() {
		return status;
	}

	public String getTel() {
		return tel;
	}

	public String getUid() {
		return uid;
	}

	public Integer getUserid() {
		return userid;
	}

	public String getUsername() {
		return username;
	}

	public LinkedHashMap getUserRole() {
		return userRole;
	}

	public String Save(){
		UserService service=(UserService)SpringUtil.getBean("UserService");
		try{
		service.insertUser(this);
		return "SUCCESS";
		}catch(Exception ex){
			
		return "ERROR";
		}
		
		
	}

	public void setAccount(String account) {
		this.account = account;
	}

	public void setDept(String dept) {
		this.dept = dept;
	}

	public void setEmail(String email) {
		this.email = email;
	}

	public void setNote(String note) {
		this.note = note;
	}

	public void setPassword(String password) {
		this.password = password;
	}

	public void setPermissionRole(LinkedHashMap permissionRole) {
		this.permissionRole = permissionRole;
	}

	public void setPriority(Short priority) {
		this.priority = priority;
	}

	public void setPwdexpiredate(Date pwdexpiredate) {
		this.pwdexpiredate = pwdexpiredate;
	}

	public void setRole(Short role) {
		this.role = role;
	}

	public void setStatus(Short status) {
		this.status = status;
	}

	public void setTel(String tel) {
		this.tel = tel;
	}

	public void setUid(String uid) {
		this.uid = uid;
	}

	public void setUserid(Integer userid) {
		this.userid = userid;
	}

	public void setUsername(String username) {
		this.username = username;
	}
	
	public void setUserRole(LinkedHashMap userRole) {
		this.userRole = userRole;
	}
	public String Update(){
		return "";
		
	}
	
}
