package idv.trans.service.system;

import java.util.HashMap;

public class SystemVar{

	HashMap userLevel;
	HashMap systemPermission;
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
	public HashMap getUserLevel() {
		return userLevel;
	}
	public void setUserLevel(HashMap userLevel) {
		this.userLevel = userLevel;
	}
	public HashMap getSystemPermission() {
		return systemPermission;
	}
	public void setSystemPermission(HashMap systemPermission) {
		systemPermission = systemPermission;
	}
}
