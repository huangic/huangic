package idv.trans.system;

import junit.framework.TestCase;

import org.springframework.context.ApplicationContext;
import org.springframework.context.support.FileSystemXmlApplicationContext;

public class SpringTest extends TestCase {

	ApplicationContext context;

	protected void setUp() throws Exception {
		super.setUp();
		String[] ConfigureXML = { "classpath:applicationContext-Test.xml"
				};

		context = new FileSystemXmlApplicationContext(
				ConfigureXML);

	}

	protected void tearDown() throws Exception {
		super.tearDown();
	}

	
}
