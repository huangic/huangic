package idv.trans.struts.action;

import idv.trans.model.Message;
import idv.trans.model.SessionUserInfo;
import idv.trans.service.index.LoginService;
import idv.trans.util.SpringUtil;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.HttpSession;

import org.apache.struts2.ServletActionContext;

import com.opensymphony.xwork2.ActionContext;
import com.opensymphony.xwork2.ActionSupport;


public class Login extends ActionSupport {
    
	String account;
	String passwd;
	
	Message message=null;
	
	String UserName;
	
	
	
	public Message getMessage() {
		return message;
	}


	public void setMessage(Message message) {
		this.message = message;
	}


	public String getUserName() {
		return UserName;
	}


	public void setUserName(String userName) {
		UserName = userName;
	}


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
   
	public String topmenu(){
		
		return "SUCCESS";
	}
	
	
	public String index(){
		return "SUCCESS";
	}
	
	//登出
    public String logout(){
    	//幹掉SESSION 然後回到INDEX;
    	HttpServletRequest request = ServletActionContext.getRequest();
    	HttpServletResponse response = ServletActionContext.getResponse();
    	HttpSession session = request.getSession();
    	
    	   	
    	
    	session.removeAttribute("userInfo");
    	
    	
    	return "SUCCESS";
    }
    //登入
	public String execute(){
    	//ActionContext.getContext().getSession().put("msg", "Hello World from Session!");
    	try{ 
    	HttpServletRequest request = ServletActionContext.getRequest();
    	HttpServletResponse response = ServletActionContext.getResponse();
    	HttpSession session = request.getSession();

    	//登入
    		LoginService service=(LoginService)SpringUtil.getBean("LoginService");
    		//拿一下User的資料
    		SessionUserInfo user=service.login(this.account, this.passwd);
    		
    		if(user!=null){
    			
    			
    			
    			session.setAttribute("UserInfo", user);
    			return "SUCCESS";
    		}else{
    			this.message=new Message();
    			
    			message.setErrorMessage("帳號/密碼錯誤");
    			
    			
    			return "ERROR";
    		}
    		
    	}catch(Exception ex){
    		ex.printStackTrace();
    		
    		this.message=new Message();
			
			message.setErrorMessage("資料庫異常請稍後再試");
    		
    	   return "ERROR";
    	}
    	
    	
    	
    }
}