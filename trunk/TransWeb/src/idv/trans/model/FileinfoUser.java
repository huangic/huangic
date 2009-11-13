package idv.trans.model;

import java.util.Date;


/**
 * FileinfoUser entity. @author MyEclipse Persistence Tools
 */

public class FileinfoUser  implements java.io.Serializable {


    // Fields    

     private Long fileid;
     private Integer userid;
     private String filename;
     private String newfilename;
     private String filepath;
     private Date uploaddate;
     private Date transdate;
     private Short status;
     private Integer allnum;
     private Integer successnum;
     private Integer errornum;
     private String logpath;
     private String logfilename;
     private Userinfo uploaduser;


    // Constructors

    /** default constructor */
    public FileinfoUser() {
    }

    
    /** full constructor */
    public FileinfoUser(Integer userid, String filename, String newfilename, String filepath, Date uploaddate, Date transdate, Short status, Integer allnum, Integer successnum, Integer errornum, String logpath, String logfilename, Userinfo uploaduser) {
        this.userid = userid;
        this.filename = filename;
        this.newfilename = newfilename;
        this.filepath = filepath;
        this.uploaddate = uploaddate;
        this.transdate = transdate;
        this.status = status;
        this.allnum = allnum;
        this.successnum = successnum;
        this.errornum = errornum;
        this.logpath = logpath;
        this.logfilename = logfilename;
        this.uploaduser = uploaduser;
    }

   
    // Property accessors

    public Long getFileid() {
        return this.fileid;
    }
    
    public void setFileid(Long fileid) {
        this.fileid = fileid;
    }

    public Integer getUserid() {
        return this.userid;
    }
    
    public void setUserid(Integer userid) {
        this.userid = userid;
    }

    public String getFilename() {
        return this.filename;
    }
    
    public void setFilename(String filename) {
        this.filename = filename;
    }

    public String getNewfilename() {
        return this.newfilename;
    }
    
    public void setNewfilename(String newfilename) {
        this.newfilename = newfilename;
    }

    public String getFilepath() {
        return this.filepath;
    }
    
    public void setFilepath(String filepath) {
        this.filepath = filepath;
    }

    public Date getUploaddate() {
        return this.uploaddate;
    }
    
    public void setUploaddate(Date uploaddate) {
        this.uploaddate = uploaddate;
    }

    public Date getTransdate() {
        return this.transdate;
    }
    
    public void setTransdate(Date transdate) {
        this.transdate = transdate;
    }

    public Short getStatus() {
        return this.status;
    }
    
    public void setStatus(Short status) {
        this.status = status;
    }

    public Integer getAllnum() {
        return this.allnum;
    }
    
    public void setAllnum(Integer allnum) {
        this.allnum = allnum;
    }

    public Integer getSuccessnum() {
        return this.successnum;
    }
    
    public void setSuccessnum(Integer successnum) {
        this.successnum = successnum;
    }

    public Integer getErrornum() {
        return this.errornum;
    }
    
    public void setErrornum(Integer errornum) {
        this.errornum = errornum;
    }

    public String getLogpath() {
        return this.logpath;
    }
    
    public void setLogpath(String logpath) {
        this.logpath = logpath;
    }

    public String getLogfilename() {
        return this.logfilename;
    }
    
    public void setLogfilename(String logfilename) {
        this.logfilename = logfilename;
    }

    public Userinfo getUploaduser() {
        return this.uploaduser;
    }
    
    public void setUploaduser(Userinfo uploaduser) {
        this.uploaduser = uploaduser;
    }
   








}