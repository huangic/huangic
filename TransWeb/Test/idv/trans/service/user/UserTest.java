package idv.trans.service.user;

import idv.trans.service.admin.UserService;
import idv.trans.struts.action.User;
import idv.trans.system.SpringTest;
import idv.trans.util.SpringUtil;

public class UserTest extends SpringTest {

	
	  public void testUserInit(){
		  UserService service=(UserService)SpringUtil.getBean("UserService");
		  
		  service.init(new User(), Short.parseShort("1"), Short.valueOf("2"));
		  
		  service.init(new User(), Short.parseShort("2"), Short.valueOf("2"));
		  
	  }
}
