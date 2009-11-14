package idv.trans.struts.action;

import java.io.File;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.LinkedHashMap;
import java.util.List;

import javax.management.relation.Role;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.HttpSession;

import idv.trans.model.Message;
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
import com.sun.org.apache.bcel.internal.generic.NEW;

public class Download extends ActionSupport {
	
	Logger logger = LoggerFactory.getLogger(Download.class);
	
	List<idv.trans.model.Download> downloads;
	
	private File upload;//The actual file
    private String uploadContentType; //The content type of the file
    private String uploadFileName; //The uploaded file name
    
    private String name;
    private String filename;
    private String filepath;
    private short priority; 
    
    private LinkedHashMap permissionRole;
    
    private Message message;
    
    
    
    //	initial
	private void init() {
		SystemVar var = (SystemVar) SpringUtil.getBean("SystemVar");
		permissionRole = var.getSystemPermission();
	}

    //list download files
	public String download() {
		init();
		try{
			HttpServletRequest request = ServletActionContext.getRequest();
			HttpServletResponse response = ServletActionContext.getResponse();
			HttpSession session = request.getSession();
			
			SessionUserInfo sessionUserInfo = (SessionUserInfo) session.getAttribute("UserInfo");
			
			DownloadService service = (DownloadService) SpringUtil.getBean("DownloadService");
			
			downloads = service.list(sessionUserInfo.getUserInfo());
			
		}catch (Exception e) {
			e.printStackTrace();
			return "ERROR";
		}
		
		return "SUCCESS";
	}
	
	//show upload file form
	public String showUploadFileForm() {
		init();
		return "SUCCESS";
	}
	
	
	//上傳下載區檔案
	public String uploadFile() {
		UploadService uService = (UploadService) SpringUtil.getBean("UploadService");
		
		DownloadService dService = (DownloadService) SpringUtil.getBean("DownloadService");
		
		init();
		try {
			//賦予新檔名
			filename = uService.getNewFileName(uploadFileName);
			uService.uploadFile(filename, upload);
			
		} catch (Exception e) {
			message = new Message("檔案上傳失敗，請重新上傳");
			logger.debug("Upload TransFile error");
			logger.debug(e.getMessage());
			
			e.printStackTrace();
			return "ERROR";
		}
		
		//結果儲存至db
		try{
			dService.save(this);
		}catch (Exception e) {
			message = new Message("資料庫寫入錯誤，請重新上傳");
			logger.debug("inert to DB error");
			logger.debug(e.getMessage());
			
			e.printStackTrace();
			return "ERROR";
		}
		
		
		
		message = new Message("新增成功");
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
	public List<idv.trans.model.Download> getDownloads() {
		return downloads;
	}	
	public void setDownloads(List<idv.trans.model.Download> downloads) {
		this.downloads = downloads;
	}
	public String getName() {
		return name;
	}
	public void setName(String name) {
		this.name = name;
	}
	public String getFilename() {
		return filename;
	}
	public void setFilename(String filename) {
		this.filename = filename;
	}
	public String getFilepath() {
		return filepath;
	}
	public void setFilepath(String filepath) {
		this.filepath = filepath;
	}
	public short getPriority() {
		return priority;
	}
	public void setPriority(short priority) {
		this.priority = priority;
	}
	public LinkedHashMap getPermissionRole() {
		return permissionRole;
	}
	public void setPermissionRole(LinkedHashMap permissionRole) {
		this.permissionRole = permissionRole;
	}
	public Message getMessage() {
		return message;
	}
	public void setMessage(Message message) {
		this.message = message;
	}
}
