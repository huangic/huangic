package idv.trans;


import junit.framework.TestCase;

import org.springframework.context.ApplicationContext;
import org.springframework.context.support.FileSystemXmlApplicationContext;

public class DbTest extends TestCase {

	ApplicationContext context;

	protected void setUp() throws Exception {
		super.setUp();
		String[] ConfigureXML = { "classpath:applicationContext-system.xml",
				//"classpath:applicationContext-DAO.xml",
				"classpath:applicationContext-DB-test.xml",
				"classpath:applicationContext-Service.xml"};

		context = new FileSystemXmlApplicationContext(
				ConfigureXML);

	}

	protected void tearDown() throws Exception {
		super.tearDown();
	}

	public void testSpringInit() {

	}

	

}
