package idv.trans.model;

import java.util.Date;

/**
 * Userinfo entity.
 * 
 * @author MyEclipse Persistence Tools
 */

public class Userinfo implements java.io.Serializable {

	// Fields

	private Integer userid;
	private String account;
	private String password;
	private String username;
	private String uid;
	private String dept;
	private String email;
	private String tel;
	private String note;
	private Short role;
	private Short priority;
	private Short status;
	private Date pwdexpiredate;

	// Constructors

	/** default constructor */
	public Userinfo() {
	}

	/** full constructor */
	public Userinfo(String account, String password, String username,
			String uid, String dept, String email, String tel, String note,
			Short role, Short priority, Short status, Date pwdexpiredate) {
		this.account = account;
		this.password = password;
		this.username = username;
		this.uid = uid;
		this.dept = dept;
		this.email = email;
		this.tel = tel;
		this.note = note;
		this.role = role;
		this.priority = priority;
		this.status = status;
		this.pwdexpiredate = pwdexpiredate;
	}

	// Property accessors

	public Integer getUserid() {
		return this.userid;
	}

	public void setUserid(Integer userid) {
		this.userid = userid;
	}

	public String getAccount() {
		return this.account;
	}

	public void setAccount(String account) {
		this.account = account;
	}

	public String getPassword() {
		return this.password;
	}

	public void setPassword(String password) {
		this.password = password;
	}

	public String getUsername() {
		return this.username;
	}

	public void setUsername(String username) {
		this.username = username;
	}

	public String getUid() {
		return this.uid;
	}

	public void setUid(String uid) {
		this.uid = uid;
	}

	public String getDept() {
		return this.dept;
	}

	public void setDept(String dept) {
		this.dept = dept;
	}

	public String getEmail() {
		return this.email;
	}

	public void setEmail(String email) {
		this.email = email;
	}

	public String getTel() {
		return this.tel;
	}

	public void setTel(String tel) {
		this.tel = tel;
	}

	public String getNote() {
		return this.note;
	}

	public void setNote(String note) {
		this.note = note;
	}

	public Short getRole() {
		return this.role;
	}

	public void setRole(Short role) {
		this.role = role;
	}

	public Short getPriority() {
		return this.priority;
	}

	public void setPriority(Short priority) {
		this.priority = priority;
	}

	public Short getStatus() {
		return this.status;
	}

	public void setStatus(Short status) {
		this.status = status;
	}

	public Date getPwdexpiredate() {
		return this.pwdexpiredate;
	}

	public void setPwdexpiredate(Date pwdexpiredate) {
		this.pwdexpiredate = pwdexpiredate;
	}

}