package idv.trans.struts.action;

import java.io.File;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.util.List;

import javax.servlet.http.HttpServletRequest;

import idv.trans.service.upload.UploadService;
import idv.trans.util.SpringUtil;

import org.apache.commons.io.FileUtils;
import org.apache.struts2.ServletActionContext;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

import com.opensymphony.xwork2.ActionSupport;

public class Download extends ActionSupport {
	
	Logger logger = LoggerFactory.getLogger(Download.class);
	
	private File upload;//The actual file
    private String uploadContentType; //The content type of the file
    private String uploadFileName; //The uploaded file name
    private String fileCaption;//The caption of the file entered by user
    
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

	public String download() {
		
		return "SUCCESS";
	}
	
	//上傳下載區檔案
	public String uploadFile() {
		/*
		FileOutputStream fos=new FileOutputStream(getSavePath()+"\\"+getUploadFileName());
        FileInputStream fis=new FileInputStream(getUpload());
        byte[] buffer=new byte[1024];
        int len=0;
        while((len=fis.read(buffer))>0){
            fos.write(buffer, 0, len);
        } 
        */
		
        
		try {
			logger.debug("copy to c:/upload/myfile.txt");
			String fullFileName = "c:/upload/myfile.txt";

			File theFile = new File(fullFileName);

			FileUtils.copyFile(upload, theFile);

			} catch (Exception e) {
				
				e.printStackTrace();
			addActionError(e.getMessage());

			return INPUT;

			}
        	
		/*
		HttpServletRequest request = ServletActionContext.getRequest();
		
		UploadService service = (UploadService)SpringUtil.getBean("UploadService");
		//service.uploadFile(request);
		*/
		return "SUCCESS";
	}
}
