package idv.trans.system;
import idv.trans.service.system.Functions;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;




public class SystemFunctinoTest extends SpringTest {
	Logger logger = LoggerFactory.getLogger(SystemFunctinoTest.class);
	
	protected void setUp() throws Exception {
		super.setUp();
	}

	public void testSystemFunctionCreate(){
		
		Functions fun=(Functions)context.getBean("functionBean");
		
		//fun.getPermission("3","1");
		
		
		logger.debug("OK");
	}
	
}
