package idv.trans.model;

public class SessionUserInfo {
    Userinfo userInfo;
    String userRoleName;
    String priorityName;
	public String getPriorityName() {
		return priorityName;
	}
	public void setPriorityName(String priorityName) {
		this.priorityName = priorityName;
	}
	public Userinfo getUserInfo() {
		return userInfo;
	}
	public void setUserInfo(Userinfo userInfo) {
		this.userInfo = userInfo;
	}
	public String getUserRoleName() {
		return userRoleName;
	}
	public void setUserRoleName(String userRoleName) {
		this.userRoleName = userRoleName;
	}
   
}
