package idv.trans.service.record;

import idv.trans.model.Fileinfo;
import idv.trans.model.FileinfoDAO;
import idv.trans.struts.action.Record;
import idv.trans.util.SpringUtil;

import java.util.List;

public class RecordService {

	
	
	public List<Fileinfo> findFile(Record record){
		FileinfoDAO dao=(FileinfoDAO)SpringUtil.getBean("FileinfoDAO");
		
 		
		return null;	
	}
	
	
}
