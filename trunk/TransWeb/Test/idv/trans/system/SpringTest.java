package idv.trans.system;

import junit.framework.TestCase;

import org.springframework.context.ApplicationContext;
import org.springframework.context.support.FileSystemXmlApplicationContext;



public class SpringTest extends TestCase {

	ApplicationContext context;
	
	protected void setUp() throws Exception {
		super.setUp();
		context = new FileSystemXmlApplicationContext("classpath:applicationContext-system.xml"); 
		
		
		
	}

	protected void tearDown() throws Exception {
		super.tearDown();
	}

	public void testSpringInit(){
 		
		
	}
	
	public void testSpringJar(){
		
		//SpringA s=(SpringA)context.getBean("springA");
		
		//System.out.println(s.getName());
		
	}
	
	public void testSpec(){
		
		
		
	}
	
}
