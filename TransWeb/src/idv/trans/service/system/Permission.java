package idv.trans.service.system;

import java.util.List;

public class Permission {
   
	String functionName;
	
	String functionURL;
	String permissionID="";
	List<String> UserLevel;
	public String getFunctionName() {
		return functionName;
	}
	public String getFunctionURL() {
		return functionURL;
	}
	public String getPermissionID() {
		return permissionID;
	}
	public List<String> getUserLevel() {
		return UserLevel;
	}
	public void setFunctionName(String functionName) {
		this.functionName = functionName;
	}
	public void setFunctionURL(String functionURL) {
		this.functionURL = functionURL;
	}
	public void setPermissionID(String permissionID) {
		this.permissionID = permissionID;
	}
	public void setUserLevel(List<String> userLevel) {
		UserLevel = userLevel;
	}
	
	
}
