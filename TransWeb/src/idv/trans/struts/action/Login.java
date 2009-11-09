package idv.trans.struts.action;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.HttpSession;

import org.apache.struts2.ServletActionContext;

import com.opensymphony.xwork2.ActionSupport;

public class Login extends ActionSupport {
    
	String account;
	String passwd;
	
	
	public String getAccount() {
		return account;
	}


	public void setAccount(String account) {
		this.account = account;
	}


	public String getPasswd() {
		return passwd;
	}


	public void setPasswd(String passwd) {
		this.passwd = passwd;
	}



	public String execute(){
    	//ActionContext.getContext().getSession().put("msg", "Hello World from Session!");
    	 
    	HttpServletRequest request = ServletActionContext.getRequest();
    	HttpServletResponse response = ServletActionContext.getResponse();
    	HttpSession session = request.getSession();

    	
    	
    	if(account.equals("CCC")){
    		return "false";
    		
    	}else{
    	   	return "SUCCESS";
    	}
    }
}
