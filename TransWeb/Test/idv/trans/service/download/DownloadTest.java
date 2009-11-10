package idv.trans.service.download;

import java.util.List;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import idv.trans.model.Download;
import idv.trans.system.SpringTest;
import idv.trans.util.SpringUtil;

public class DownloadTest extends SpringTest {
	
	Logger logger = LoggerFactory.getLogger(DownloadTest.class);
	
	
	
	public void testList() {
		DownloadService service = (DownloadService)SpringUtil.getBean("DownloadService");
		List<Download> list = service.listAll();
		for (Download download : list) {
			logger.debug(download.getName());
		}
		
	}
	public void testListByPriority1(){
		DownloadService service = (DownloadService)SpringUtil.getBean("DownloadService");
		List<Download> list = service.listByPriority((short)1);
		for (Download download : list) {
			logger.debug(download.getName());
		}
	}

}
