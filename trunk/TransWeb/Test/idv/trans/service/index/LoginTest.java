package idv.trans.service.index;

import idv.trans.model.SessionUserInfo;
import idv.trans.system.SpringTest;
import idv.trans.util.SpringUtil;
import junit.framework.Assert;


public class LoginTest extends SpringTest {
    
	
	public void testLogin(){
		LoginService service=(LoginService)SpringUtil.getBean("LoginService");
		
		SessionUserInfo user= service.login("", "");
		
		Assert.assertNull(user);
		
	}
	public void testLoginMyne(){
		LoginService service=(LoginService)SpringUtil.getBean("LoginService");
		SessionUserInfo user= service.login("MYNE", "1111");
		
		
		
		
		Assert.assertNotNull(user);
	}
	
}
