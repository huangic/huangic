package idv.trans.util;

import org.springframework.beans.BeansException;
import org.springframework.context.ApplicationContext;
import org.springframework.context.ApplicationContextAware;

public class SpringUtil implements ApplicationContextAware{
  
	private static ApplicationContext ctx;
	
	


	public static Object getBean(String beanName){
	   return ctx.getBean(beanName);
	  
	}


	public void setApplicationContext(ApplicationContext arg0)
			throws BeansException {
		// TODO Auto-generated method stub
		this.ctx=arg0;
	}
}
