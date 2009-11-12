package idv.trans.service.download;

import java.util.List;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import bsh.This;

import idv.trans.model.Download;
import idv.trans.model.DownloadDAO;
import idv.trans.model.Userinfo;
import idv.trans.service.system.SystemVar;
import idv.trans.util.SpringUtil;

public class DownloadService {
	
	Logger logger = LoggerFactory.getLogger(This.class);

	public List<Download> listByPriority(short priority) {
		
		DownloadDAO dao = (DownloadDAO) SpringUtil.getBean("DownloadDAO");
		
		List list = dao.findByPriority(priority);
		
		
		return list;
	}
	
	public List<Download> listAll() {
		
		DownloadDAO dao = (DownloadDAO) SpringUtil.getBean("DownloadDAO");
		
		return dao.findAll();
	}
	
	
	
	public List list(Userinfo userinfo)throws Exception{
		
		if(userinfo.getRole().equals(new Short("1"))||
				userinfo.getRole().equals(new Short("2"))){
			logger.debug("Role 1 list all files.");
			return listAll();
			
		}else{
			short priority = userinfo.getPriority();
			logger.debug("Role "+priority+" list files.");
			return listByPriority(priority);
		}
	}
	
	//儲存至資料庫
	public void save(idv.trans.struts.action.Download downloadAct) {
		DownloadDAO dao = (DownloadDAO) SpringUtil.getBean("DownloadDAO");
		SystemVar systemVar = (SystemVar)SpringUtil.getBean("SystemVar");
		Download download = new Download();
		download.setName(downloadAct.getName());
		download.setFilename(downloadAct.getFilename());
		download.setFilepath(systemVar.getDownloadPath());
		download.setPriority(downloadAct.getPriority());
		dao.save(download);
		
	}
	
}
