package idv.trans.struts.action;

import idv.trans.model.FileinfoUser;
import idv.trans.model.SessionUserInfo;
import idv.trans.service.record.RecordService;
import idv.trans.util.SpringUtil;

import java.util.Date;
import java.util.LinkedHashMap;
import java.util.List;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.HttpSession;

import org.apache.struts2.ServletActionContext;

public class Record {
    
	RecordService service=(RecordService)SpringUtil.getBean("RecordService");
	
	Integer fileid;
	
	String username;
	String account;
	Short role;
	Short priority;
	
	Date StartDate;
	Date EndDate;
	
	
	public RecordService getService() {
		return service;
	}


	public void setService(RecordService service) {
		this.service = service;
	}


	public Integer getFileid() {
		return fileid;
	}


	public void setFileid(Integer fileid) {
		this.fileid = fileid;
	}


	public String getUsername() {
		return username;
	}


	public void setUsername(String username) {
		this.username = username;
	}


	public String getAccount() {
		return account;
	}


	public void setAccount(String account) {
		this.account = account;
	}


	public Short getRole() {
		return role;
	}


	public void setRole(Short role) {
		this.role = role;
	}


	public Short getPriority() {
		return priority;
	}


	public void setPriority(Short priority) {
		this.priority = priority;
	}


	public Date getStartDate() {
		return StartDate;
	}


	public void setStartDate(Date startDate) {
		StartDate = startDate;
	}


	public Date getEndDate() {
		return EndDate;
	}


	public void setEndDate(Date endDate) {
		EndDate = endDate;
	}


	public List<Short> getStatus() {
		return status;
	}


	public void setStatus(List<Short> status) {
		this.status = status;
	}


	public LinkedHashMap getPermissionRole() {
		return permissionRole;
	}


	public void setPermissionRole(LinkedHashMap permissionRole) {
		this.permissionRole = permissionRole;
	}


	public LinkedHashMap getUserRole() {
		return userRole;
	}


	public void setUserRole(LinkedHashMap userRole) {
		this.userRole = userRole;
	}


	public List<FileinfoUser> getRecords() {
		return records;
	}


	public void setRecords(List<FileinfoUser> records) {
		this.records = records;
	}

	List<Short> status; 
	
	
	private LinkedHashMap permissionRole;
	// 權限MAP
	// 角色MAP
	private LinkedHashMap userRole;
	
	List<FileinfoUser> records;
	
	public String toSearch(){
    	init();
		return "SUCCESS";
    }
    
    
    public String search(){
    	//
    	records=service.findFile(this);
    	
    	
    	
    	return "SUCCESS";
    }
    
    public void init(){
    	
    	HttpServletRequest request = ServletActionContext.getRequest();
		HttpServletResponse response = ServletActionContext.getResponse();
		HttpSession session = request.getSession();
		SessionUserInfo userInfo = (SessionUserInfo) session
				.getAttribute("UserInfo");

		service.init(this, userInfo.getUserInfo().getRole(), userInfo
				.getUserInfo().getPriority());

    	
    	
    	
    }
    
    
    
}
