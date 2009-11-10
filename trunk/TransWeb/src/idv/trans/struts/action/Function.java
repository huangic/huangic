package idv.trans.struts.action;

import idv.trans.model.SessionUserInfo;
import idv.trans.model.Userinfo;
import idv.trans.service.system.Functions;
import idv.trans.util.SpringUtil;

import java.util.HashMap;
import java.util.LinkedHashMap;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.HttpSession;

import org.apache.struts2.ServletActionContext;

import com.opensymphony.xwork2.ActionSupport;

public class Function extends ActionSupport {

	private HashMap permissionMap = null;

	public HashMap getPermissionMap() {
		return permissionMap;
	}

	public void setPermissionMap(HashMap permissionMap) {
		this.permissionMap = permissionMap;
	}

	public String execute() {
		// 讀USER SESSION 產生左側功能

		HttpServletRequest request = ServletActionContext.getRequest();
		HttpServletResponse response = ServletActionContext.getResponse();
		HttpSession session = request.getSession();

		SessionUserInfo sessionUserInfo = (SessionUserInfo) session
				.getAttribute("UserInfo");

		Userinfo user = sessionUserInfo.getUserInfo();

		LinkedHashMap permissionMap = (LinkedHashMap) ((Functions) SpringUtil
				.getBean("functionBean")).getPermission(String.valueOf(user
				.getRole()), String.valueOf(user.getPriority()));

		this.setPermissionMap(permissionMap);

		return "SUCCESS";

	}

}
