package idv.trans.struts.action;

import idv.trans.service.upload.UploadService;
import idv.trans.util.SpringUtil;

import java.io.File;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.util.ArrayList;
import java.util.Iterator;
import java.util.List;

import org.apache.struts2.ServletActionContext;

import com.opensymphony.xwork2.ActionSupport;

public class Upload extends ActionSupport {
	
	//封装多个上传文件域的属性
	private List<File> upload = new ArrayList<File>();
	// 封装多个上传文件类型的属性
	private List<String> uploadContentType = new ArrayList<String>();
	// 封装多个上传文件名的属性
	private List<String> uploadFileName = new ArrayList<String>();
	
	//show form
	public String showUploadTransFileForm() {
		return "SUCCESS";
	}
	
	//儲存
	public String uploadTransFileSave() {
		UploadService uService = (UploadService) SpringUtil.getBean("UploadService");
		
		for (int i = 0; i < upload.size(); i++) {
			try {
				//賦予新檔名
				uService.uploadTransFile(uploadFileName.get(i), upload.get(i));
				
			} catch (Exception e) {
				e.printStackTrace();
				addActionError(e.getMessage());

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
