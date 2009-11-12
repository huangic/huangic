package idv.trans.service.upload;

import java.io.*;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.Iterator;
import java.util.List;

import idv.trans.service.system.SystemVar;
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
			
			String saveDirectory = systemVar.getRealDir()+systemVar.getDownloadPath();
			
			logger.debug("copy to "+saveDirectory+uploadFileName);
			String fullFileName = saveDirectory+uploadFileName;

			File theFile = new File(fullFileName);

			FileUtils.copyFile(uploadFile, theFile);

			
	}
	
	
	/*
	 * 上傳檔案至轉檔區
	 * */
	public void uploadTransFile(String uploadFileName, File uploadFile) throws Exception {
		
		SystemVar systemVar = (SystemVar) SpringUtil.getBean("SystemVar");
		
		String saveDirectory = systemVar.getRealDir()+systemVar.getUploadPath();
		
		logger.debug("copy to "+saveDirectory+uploadFileName);
		String fullFileName = saveDirectory+uploadFileName;

		File theFile = new File(fullFileName);

		FileUtils.copyFile(uploadFile, theFile);
		
	}
	
	public String getNewFileName(String fileName) {
		
		String extFileName = fileName.substring(fileName.lastIndexOf("."));
		
		return "f"+getDateIndex()+extFileName;
	}
	
	public String getDateIndex() {
		return new SimpleDateFormat("yywwHHmmss").format(new Date());
	}
}
