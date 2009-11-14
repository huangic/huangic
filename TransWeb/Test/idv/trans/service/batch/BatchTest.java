package idv.trans.service.batch;

import idv.trans.system.SpringTest;
import idv.trans.util.SpringUtil;

public class BatchTest extends SpringTest {
    public void testBatch(){
    	//載入SERVICE
    	BatchService service=(BatchService)SpringUtil.getBean("BatchService");
    	service.doBatch();
    }
}
