package idv.trans.service.system;

import java.util.LinkedHashMap;

public class SystemVar{

	LinkedHashMap userLevel;
	LinkedHashMap systemPermission;
	String uploadPath="";
	String downloadPath="";
	String backupPath="";
	
	
	public String getUploadPath() {
		return uploadPath;
	}
	public void setUploadPath(String uploadPath) {
		this.uploadPath = uploadPath;
	}
	public String getDownloadPath() {
		return downloadPath;
	}
	public void setDownloadPath(String downloadPath) {
		this.downloadPath = downloadPath;
	}
	public String getBackupPath() {
		return backupPath;
	}
	public void setBackupPath(String backupPath) {
		this.backupPath = backupPath;
	}
	public LinkedHashMap getUserLevel() {
		return userLevel;
	}
	public void setUserLevel(LinkedHashMap userLevel) {
		this.userLevel = userLevel;
	}
	public LinkedHashMap getSystemPermission() {
		return systemPermission;
	}
	public void setSystemPermission(LinkedHashMap systemPermission) {
		this.systemPermission = systemPermission;
	}
}
