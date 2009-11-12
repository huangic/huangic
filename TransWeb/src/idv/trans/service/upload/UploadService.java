package idv.trans.service.upload;

import java.io.*;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.Iterator;
import java.util.List;

import idv.trans.model.Download;
import idv.trans.model.DownloadDAO;
import idv.trans.model.Fileinfo;
import idv.trans.model.FileinfoDAO;
import idv.trans.model.Userinfo;
import idv.trans.service.system.SystemVar;
import idv.trans.struts.action.Upload;
import idv.trans.util.SpringUtil;

import javax.servlet.http.HttpServletRequest;

import org.apache.commons.fileupload.*;
import org.apache.commons.fileupload.disk.DiskFileItemFactory;
import org.apache.commons.fileupload.servlet.ServletFileUpload;
import org.apache.commons.fileupload.util.Streams;
import org.apache.commons.io.FileUtils;
import org.apache.commons.io.FilenameUtils;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

public class UploadService {
	Logger logger = LoggerFactory.getLogger(UploadService.class);
	
	/*
	 * 上傳檔案至下載區
	 * */
	public void uploadFile(String uploadFileName, File uploadFile) throws Exception {
		
			SystemVar systemVar = (SystemVar) SpringUtil.getBean("SystemVar");
			
			String saveDirectory = systemVar.getRealDir()+"/"+systemVar.getDownloadPath();
			
			logger.debug("copy to "+saveDirectory+uploadFileName);
			String fullFileName = saveDirectory+uploadFileName;

			File theFile = new File(fullFileName);

			FileUtils.copyFile(uploadFile, theFile);

			
	}
	
	
	/*
	 * 上傳檔案至轉檔區
	 * */
	public void uploadTransFile(String uploadFileName, File uploadFile,String uploadUserName) throws Exception {
		
		SystemVar systemVar = (SystemVar) SpringUtil.getBean("SystemVar");
		
		String saveDirectory = systemVar.getRealDir()+"/"+systemVar.getUploadPath()+"/"+uploadUserName+"/";
		
		logger.debug("copy to "+saveDirectory+uploadFileName);
		String fullFileName = saveDirectory+uploadFileName;

		File theFile = new File(fullFileName);

		FileUtils.copyFile(uploadFile, theFile);
		
	}
	
	//寫到資料庫
	public void save(int iFile,Upload uploadAct,Userinfo user) throws Exception{
		
		FileinfoDAO dao = (FileinfoDAO) SpringUtil.getBean("FileinfoDAO");
		SystemVar systemVar = (SystemVar)SpringUtil.getBean("SystemVar");
		
		String saveDirectory = systemVar.getRealDir()+"/"+systemVar.getUploadPath()+"/"+user.getAccount()+"/";
		String uploadFileName = uploadAct.getUploadFileName().get(iFile);
		
		//檢查一下是不是有重覆的資料在資料庫，用上傳檔名檢查
		List<Fileinfo> list = dao.findByFilename(uploadFileName);
		if(list.size()>0){
			Fileinfo oldFileinfo = list.get(0);	
			oldFileinfo.setUserid(user.getUserid()+"");
			oldFileinfo.setFilepath(saveDirectory);
			oldFileinfo.setStatus(new Short("1"));
			oldFileinfo.setUploaddate(new Date());
			
			dao.attachDirty(oldFileinfo);
		}else {
			Fileinfo newFileinfo = new Fileinfo();
			
			newFileinfo.setUserid(user.getUserid()+"");
			newFileinfo.setFilename(uploadFileName);
			newFileinfo.setNewfilename(uploadFileName);
			newFileinfo.setFilepath(saveDirectory);
			newFileinfo.setStatus(new Short("1"));
			newFileinfo.setUploaddate(new Date());
			dao.save(newFileinfo);
		}
		
		
	}
	
	
	public String getNewFileName(String fileName) {
		
		String extFileName = fileName.substring(fileName.lastIndexOf("."));
		
		return "f"+getDateIndex()+extFileName;
	}
	
	public String getDateIndex() {
		return new SimpleDateFormat("yywwHHmmss").format(new Date());
	}
}
