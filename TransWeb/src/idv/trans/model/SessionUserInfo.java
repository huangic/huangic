package idv.trans.model;

public class SessionUserInfo {
    Userinfo userInfo;
    String userRoleName;
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
