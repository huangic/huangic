package idv.trans.struts.action;

import java.io.File;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.util.List;

import javax.management.relation.Role;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.HttpSession;

import idv.trans.model.SessionUserInfo;
import idv.trans.service.download.DownloadService;
import idv.trans.service.system.SystemVar;
import idv.trans.service.upload.UploadService;
import idv.trans.util.SpringUtil;

import org.apache.commons.io.FileUtils;
import org.apache.struts2.ServletActionContext;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.opensymphony.xwork2.ActionSupport;

public class Download extends ActionSupport {
	
	Logger logger = LoggerFactory.getLogger(Download.class);
	
	List<idv.trans.model.Download> downloads;
	
	private File upload;//The actual file
    private String uploadContentType; //The content type of the file
    private String uploadFileName; //The uploaded file name
    private String fileCaption;//The caption of the file entered by user


    //list download files
	public String download() {
		
		try{
			HttpServletRequest request = ServletActionContext.getRequest();
			HttpServletResponse response = ServletActionContext.getResponse();
			HttpSession session = request.getSession();
			
			SessionUserInfo sessionUserInfo = (SessionUserInfo) session.getAttribute("UserInfo");
			
			DownloadService service = (DownloadService) SpringUtil.getBean("DownloadService");
			
			if(sessionUserInfo.getUserInfo().getRole()==(short)1){
				logger.debug("Role 1 list all files.");
				downloads = service.listAll();
				
			}else{
				short priority = sessionUserInfo.getUserInfo().getPriority();
				logger.debug("Role "+priority+" list files.");
				downloads = service.listByPriority(priority);
			}
		}catch (Exception e) {
			e.printStackTrace();
			return "ERROR";
		}
		
		return "SUCCESS";
	}
	
	//上傳下載區檔案
	public String uploadFile() {
		try {
			
			UploadService service = (UploadService) SpringUtil.getBean("UploadService");
			service.uploadFile(uploadFileName, upload);
			
		} catch (Exception e) {
			e.printStackTrace();
			addActionError(e.getMessage());

			return "ERROR";
		}
		return "SUCCESS";
	}
	
	
	
	public File getUpload() {
		return upload;
	}
	public void setUpload(File upload) {
		this.upload = upload;
	}
	public String getUploadContentType() {
		return uploadContentType;
	}
	public void setUploadContentType(String uploadContentType) {
		this.uploadContentType = uploadContentType;
	}
	public String getUploadFileName() {
		return uploadFileName;
	}
	public void setUploadFileName(String uploadFileName) {
		this.uploadFileName = uploadFileName;
	}
	public String getFileCaption() {
		return fileCaption;
	}
	public void setFileCaption(String fileCaption) {
		this.fileCaption = fileCaption;
	}	
	public List<idv.trans.model.Download> getDownloads() {
		return downloads;
	}	
	public void setDownloads(List<idv.trans.model.Download> downloads) {
		this.downloads = downloads;
	}
}
