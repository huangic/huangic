package idv.trans.service.user;

import java.lang.reflect.InvocationTargetException;

import idv.trans.service.admin.UserService;
import idv.trans.struts.action.User;
import idv.trans.system.SpringTest;
import idv.trans.util.SpringUtil;

public class UserTest extends SpringTest {

	
	  public void testUID(){
		  String UID="E221061420";
		  String UID2="A125673091";
		  
		  UserService service=(UserService)SpringUtil.getBean("UserService");
		  
		  System.out.print(service.checkUID(UID.toCharArray()));
		  System.out.print(service.checkUID(UID2.toCharArray()));
		  
		  
	  }
	
	
	  public void testUserInit(){
		  UserService service=(UserService)SpringUtil.getBean("UserService");
		  
		  try {
			service.init(new User(), Short.parseShort("1"), Short.valueOf("2"));
		} catch (NumberFormatException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		} catch (IllegalAccessException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		} catch (InvocationTargetException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		  
		  try {
			service.init(new User(), Short.parseShort("2"), Short.valueOf("2"));
		} catch (NumberFormatException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		} catch (IllegalAccessException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		} catch (InvocationTargetException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		  
	  }
}
