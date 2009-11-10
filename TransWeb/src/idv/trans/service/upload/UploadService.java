package idv.trans.service.upload;

import java.io.*;
import java.util.Iterator;
import java.util.List;

import idv.trans.service.system.SystemVar;
import idv.trans.util.SpringUtil;

import javax.servlet.http.HttpServletRequest;

import org.apache.commons.fileupload.*;
import org.apache.commons.fileupload.disk.DiskFileItemFactory;
import org.apache.commons.fileupload.servlet.ServletFileUpload;
import org.apache.commons.fileupload.util.Streams;
import org.apache.commons.io.FilenameUtils;

import org.slf4j.Logger;
import org.slf4j.LoggerFactory;

public class UploadService {
	Logger logger = LoggerFactory.getLogger(UploadService.class);
	
	/*
	 * 上傳下載區檔案
	 * */
	public void uploadFile(HttpServletRequest request) throws Exception {
		//系統參數
		SystemVar systemVar = (SystemVar) SpringUtil.getBean("SystemVar");
		
		String saveDirectory = systemVar.getDownloadPath();
		
		// setup output message
	    PrintWriter output;
	    //res.setContentType("text/html;charset=Big5");
	    //output = res.getWriter();
	    
	    logger.debug("File Upload Test");

	    // first check if the upload request coming in is a multipart request
	    boolean isMultipart = ServletFileUpload.isMultipartContent(request);

	    // Create a factory for disk-based file items
	    FileItemFactory factory = new DiskFileItemFactory();

	    // Create a new file upload handler
	    ServletFileUpload upload = new ServletFileUpload(factory);

	    List items = null;
	    try {
	      // parse this request by the handler
	      // this gives us a list of items from the request
	      items = upload.parseRequest(request);
	    } catch(FileUploadException e) {
	    }

	    Iterator itr = items.iterator();
	    while(itr.hasNext()) {
	      FileItem item = (FileItem) itr.next();

	      // check if the current item is a form field or an uploaded file
	      if(!item.isFormField()) {
	        // get the name of the field
	        String fieldName = item.getFieldName();

	        if(fieldName.equals("myfile")) {
	          // the item must be an uploaded file save it to disk.
	          String name = item.getName();
	          logger.debug("Upload Filename: " + name);
	          File savedFile = new File(saveDirectory + name);
	          try {
	            item.write(savedFile);
	          } catch (Exception e) {
	            logger.debug("cannot save the uploaded file.");
	          }
	        }
	      }
	    }

	    logger.debug("\n");
		
		
		logger.debug("下載區路徑："+saveDirectory);
	}
	
	
	/*
	 * 上傳轉檔檔案
	 * */
	public void uploadTransFile() {
		
	}
}
