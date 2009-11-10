package idv.trans.service.index;

import idv.trans.system.SpringTest;
import idv.trans.util.SpringUtil;


public class LoginTest extends SpringTest {
    
	
	public void testLogin(){
		LoginService service=(LoginService)SpringUtil.getBean("LoginService");
		service.login("", "");
	}
	public void testLoginMyne(){
		LoginService service=(LoginService)SpringUtil.getBean("LoginService");
		service.login("myne", "1111");
	}
	
}
