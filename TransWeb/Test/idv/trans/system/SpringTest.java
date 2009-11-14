package idv.trans.system;

import junit.framework.TestCase;

import org.springframework.context.ApplicationContext;
import org.springframework.context.support.FileSystemXmlApplicationContext;

public class SpringTest extends TestCase {

	ApplicationContext context;

	protected void setUp() throws Exception {
		super.setUp();
		String[] ConfigureXML = { "classpath:applicationContext-system.xml",
				"classpath:applicationContext-DB.xml",
				"classpath:applicationContext-DAO.xml",
				"classpath:applicationContext-Service.xml",
				"classpath:applicationContext-BatchRule.xml"
		};

		context = new FileSystemXmlApplicationContext(
				ConfigureXML);

	}

	protected void tearDown() throws Exception {
		super.tearDown();
	}

	
}
