package idv.trans.struts.action;

import idv.trans.model.Fileinfo;
import idv.trans.service.record.RecordService;
import idv.trans.util.SpringUtil;

import java.util.LinkedHashMap;
import java.util.List;

public class Record {
    
	RecordService service=(RecordService)SpringUtil.getBean("RecordService");
	
	Integer fileid;
	
	String username;
	String account;
	Short role;
	Short priority;
	
	private LinkedHashMap permissionRole;
	// 權限MAP
	// 角色MAP
	private LinkedHashMap userRole;
	
	List<Fileinfo> files;
	
	public String toSearch(){
    	return "SUCCESS";
    }
    
    
    public String search(){
    	//
    	files=service.findFile(this);
    	
    	
    	
    	return "SUCCESS";
    }
    
    public void init(){
    	
    }
    
}
