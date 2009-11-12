package idv.trans.service.record;

import idv.trans.struts.action.Record;
import idv.trans.system.SpringTest;
import idv.trans.util.SpringUtil;

public class RecordTest extends SpringTest {

	
	  public void testRecordInit(){
		  RecordService service=(RecordService)SpringUtil.getBean("RecordService");
		  
	
		  Record record=new Record();
		  
		  
		  record.setRole(Short.valueOf("3"));
		  //找出一般使用者的資料
		  service.findFile(record);
		  
	  }
}
