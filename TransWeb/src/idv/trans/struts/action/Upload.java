package idv.trans.struts.action;

import idv.trans.model.Fileinfo;
import idv.trans.model.Message;
import idv.trans.model.SessionUserInfo;
import idv.trans.model.Userinfo;
import idv.trans.service.upload.UploadService;
import idv.trans.util.SpringUtil;

import java.io.File;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.util.ArrayList;
import java.util.Iterator;
import java.util.List;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.HttpSession;

import org.apache.struts2.ServletActionContext;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.opensymphony.xwork2.ActionSupport;

public class Upload extends ActionSupport {
	
	Logger logger = LoggerFactory.getLogger(Upload.class);
	
	//封装多个上传文件域的属性
	private List<File> upload = new ArrayList<File>();
	// 封装多个上传文件类型的属性
	private List<String> uploadContentType = new ArrayList<String>();
	// 封装多个上传文件名的属性
	private List<String> uploadFileName = new ArrayList<String>();
	
	private Message message;
	
	
	
	//show form
	public String showUploadTransFileForm() {
		
		
		if(getUserPriority().equals(new Short("1"))){
			logger.debug("show upload trans form 1");
			return "SUCCESS_1";
		}else {
			logger.debug("show upload trans form 2");
			return "SUCCESS_2";
		}
	}
	
	//儲存
	public String uploadTransFileSave() {
		HttpServletRequest request = ServletActionContext.getRequest();
		HttpSession session = request.getSession();
		
		UploadService uService = (UploadService) SpringUtil.getBean("UploadService");
		
		SessionUserInfo sessionUserInfo = (SessionUserInfo) session.getAttribute("UserInfo");
		String userAccount =  sessionUserInfo.getUserInfo().getAccount();
		
		for (int i = 0; i < upload.size(); i++) {
			//儲存至web/upload/userAccount內
			try {
				logger.debug("上傳第"+i+"個檔案："+uploadFileName.get(i));
				uService.uploadTransFile(uploadFileName.get(i), upload.get(i),userAccount);
				
			} catch (Exception e) {
				message = new Message("檔案上傳失敗，請重新上傳");
				logger.debug("Upload TransFile error");
				logger.debug(e.getMessage());
				
				e.printStackTrace();
				if(getUserPriority().equals(new Short("1"))){
					return "ERROR_1";
				}else {
					return "ERROR_2";
				}
			}
			
			//資料寫至資料庫內
			try{
				logger.debug("第"+i+"個檔案，寫至資料庫");
				uService.save(i,this,sessionUserInfo.getUserInfo());
				
				message = new Message("新增成功");
				logger.debug("inert to DB successfully");
				
				
			}catch (Exception e) {
				message = new Message("資料庫寫入錯誤，請重新上傳");
				logger.debug("inert to DB error");
				logger.debug(e.getMessage());
				e.printStackTrace();
				
				if(getUserPriority().equals(new Short("1"))){
					return "ERROR_1";
				}else {
					return "ERROR_2";
				}
			}
		}
		
		
		//返回
		if(getUserPriority().equals(new Short("1"))){
			logger.debug("show upload trans form 1");
			return "SUCCESS_1";
		}else {
			logger.debug("show upload trans form 2");
			return "SUCCESS_2";
		}
	}
	
	public Short getUserPriority(){		
		HttpServletRequest request = ServletActionContext.getRequest();
		HttpSession session = request.getSession();
		
		SessionUserInfo sessionUserInfo = (SessionUserInfo) session.getAttribute("UserInfo");
		Userinfo user = sessionUserInfo.getUserInfo();
		
		return user.getPriority();
	}
	
	

	public List<File> getUpload() {
		return upload;
	}

	public void setUpload(List<File> upload) {
		this.upload = upload;
	}

	public List<String> getUploadContentType() {
		return uploadContentType;
	}

	public void setUploadContentType(List<String> uploadContentType) {
		this.uploadContentType = uploadContentType;
	}

	public List<String> getUploadFileName() {
		return uploadFileName;
	}

	public void setUploadFileName(List<String> uploadFileName) {
		this.uploadFileName = uploadFileName;
	}
	
	public Message getMessage() {
		return message;
	}
	
	public void setMessage(Message message) {
		this.message = message;
	}
	
}
