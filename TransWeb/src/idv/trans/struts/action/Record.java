package idv.trans.struts.action;

import idv.trans.model.FileinfoUser;
import idv.trans.model.Message;
import idv.trans.model.SessionUserInfo;
import idv.trans.service.admin.UserService;
import idv.trans.service.record.RecordService;
import idv.trans.util.SpringUtil;

import java.lang.reflect.InvocationTargetException;
import java.util.Date;
import java.util.LinkedHashMap;
import java.util.List;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.HttpSession;

import org.apache.struts2.ServletActionContext;

public class Record {
    
	RecordService service=(RecordService)SpringUtil.getBean("RecordService");
	
	Long fileid;
	
	String username;
	String account;
	Short role;
	Short priority;
	
	Date startDate;
	Date endDate;
	public Message getMessage() {
		return message;
	}


	public void setMessage(Message message) {
		this.message = message;
	}

	Message message;
	
	public RecordService getService() {
		return service;
	}


	public void setService(RecordService service) {
		this.service = service;
	}


	public Long getFileid() {
		return fileid;
	}


	public void setFileid(Long fileid) {
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
		return this.startDate;
	}


	public void setStartDate(Date startDate) {
		this.startDate = startDate;
	}


	public Date getEndDate() {
		return this.endDate;
	}


	public void setEndDate(Date endDate) {
		this.endDate = endDate;
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
    	HttpServletRequest request = ServletActionContext.getRequest();
		HttpServletResponse response = ServletActionContext.getResponse();
		HttpSession session = request.getSession();
    	
		//把這次的查詢條件放在SESSION 讓以後的作業直接回來
		session.setAttribute("recordSearch", this);
    	
    	
    	
    	return "SUCCESS";
    }
    
    public String cancel(){
    	//把這筆資料取消掉 如果他是代處理中的話
    	try{
    	  service.cancelTrans(this);	
    	  this.message=new Message("成功取消");
    	}catch(Exception ex){
    		this.message=new Message(ex.getMessage());
    	    
    	}
    	
    	HttpServletRequest request = ServletActionContext.getRequest();
		HttpServletResponse response = ServletActionContext.getResponse();
		HttpSession session = request.getSession();
    	
		//把這次的查詢條件放在SESSION 讓以後的作業直接回來
		Record oldrecord=(Record)session.getAttribute("recordSearch");
    	
    	
    	//把舊的條件丟給SEARCH
    	
    	records=service.findFile(oldrecord);
    	
    	
    	return "SUCCESS";
    }
    
    
    public void init(){
    	
    	UserService userservice = (UserService) SpringUtil.getBean("UserService");
    	
    	HttpServletRequest request = ServletActionContext.getRequest();
		HttpServletResponse response = ServletActionContext.getResponse();
		HttpSession session = request.getSession();
		SessionUserInfo userInfo = (SessionUserInfo) session
				.getAttribute("UserInfo");

		try {
			userservice.init(this, userInfo.getUserInfo().getRole(), userInfo
					.getUserInfo().getPriority());
		} catch (IllegalAccessException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		} catch (InvocationTargetException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}

    	
    	
    	
    }
    
    
    
}
