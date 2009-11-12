package idv.trans.struts.action;

import idv.trans.model.Fileinfo;
import idv.trans.model.SessionUserInfo;
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
	
	//show form
	public String showUploadTransFileForm() {
		logger.debug("show upload trans form");
		return "SUCCESS";
	}
	
	//儲存
	public String uploadTransFileSave() {
		
		
		UploadService uService = (UploadService) SpringUtil.getBean("UploadService");
		HttpServletRequest request = ServletActionContext.getRequest();
		HttpServletResponse response = ServletActionContext.getResponse();
		HttpSession session = request.getSession();
		
		SessionUserInfo sessionUserInfo = (SessionUserInfo) session.getAttribute("UserInfo");
		
		String userAccount =  sessionUserInfo.getUserInfo().getAccount();
		
		for (int i = 0; i < upload.size(); i++) {
			//儲存至web/upload/userAccount內
			try {
				logger.debug("上傳第"+i+"個檔案："+uploadFileName.get(i));
				uService.uploadTransFile(uploadFileName.get(i), upload.get(i),userAccount);
				
			} catch (Exception e) {
				e.printStackTrace();
				addActionError(e.getMessage());

				return "ERROR";
			}
			
			//資料寫至資料庫內
			try{
				logger.debug("第"+i+"個檔案，寫至資料庫");
				uService.save(i,this,sessionUserInfo.getUserInfo());
			}catch (Exception e) {
				e.printStackTrace();
				return "ERROR";
			}
		}
		
		
		return "SUCCESS_1";
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
	
}
