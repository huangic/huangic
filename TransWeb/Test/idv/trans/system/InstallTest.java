package idv.trans.system;

import idv.trans.struts.action.Install;
import junit.framework.TestCase;

public class InstallTest extends TestCase {
   
	
	public void testInstall(){
		Install install=new Install();
		
		install.index();
		
	}
}
