package idv.trans.service.download;

import java.util.List;

import idv.trans.model.Download;
import idv.trans.model.DownloadDAO;
import idv.trans.util.SpringUtil;

public class DownloadService {

	public List<Download> listByPriority(short priority) {
		
		DownloadDAO dao = (DownloadDAO) SpringUtil.getBean("DownloadDAO");
		
		List list = dao.findByPriority(priority);
		
		return list;
	}
	
	public List<Download> listAll() {
		
		DownloadDAO dao = (DownloadDAO) SpringUtil.getBean("DownloadDAO");
		
		return dao.findAll();
	}
	
	public void upload() {
		
	}
	
}
