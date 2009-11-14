package idv.trans.service.batch;

import idv.trans.util.SpringUtil;

import org.quartz.JobExecutionContext;
import org.quartz.JobExecutionException;
import org.springframework.scheduling.quartz.QuartzJobBean;

public class BatchJob extends QuartzJobBean {

	

	@Override
	protected void executeInternal(JobExecutionContext arg0)
			throws JobExecutionException {
		// TODO Auto-generated method stub
		BatchService service=(BatchService)SpringUtil.getBean("BatchService");
	   service.doBatch();
	}

}
