package idv.trans.service.admin;

import idv.trans.model.Userinfo;
import idv.trans.model.UserinfoDAO;
import idv.trans.service.system.SystemVar;
import idv.trans.struts.action.User;
import idv.trans.util.SpringUtil;

import java.util.List;

import org.hibernate.criterion.DetachedCriteria;

public class UserService {
   
	public void init(User user){
		
		SystemVar var=(SystemVar)SpringUtil.getBean("SystemVar");
		user.setUserRole(var.getUserLevel());
		user.setPermissionRole(var.getSystemPermission());
	}
	
	public void insertUser(User user){
		//檢查一下帳號跟身分證吧
		
		 
		
		
		UserinfoDAO dao=(UserinfoDAO)SpringUtil.getBean("UserinfoDAO");
		//
		//如果沒問題~那就資料建一建寫入吧
		
		
		Userinfo newUser=user.getUserinfo();
		
		dao.save(newUser);
		
		
	}


    public void findUser(User user,Integer id) throws Exception{
    	UserinfoDAO dao=(UserinfoDAO)SpringUtil.getBean("UserinfoDAO");
    	Userinfo userinfo=dao.findById(id);
    	
    	if(userinfo==null){
    		throw new  Exception("無此使用者");
    	}else{
    		user.setUserinfo(userinfo);    		
    	}
    	
    }
    
    public void findAllUsers(User user){
    	
    	//設定條件
    	
    	// 查詢條件
		DetachedCriteria criteria = DetachedCriteria.forClass(Userinfo.class);

    	//從USER去設定
    	
    	
    	
    	UserinfoDAO dao=(UserinfoDAO)SpringUtil.getBean("UserinfoDAO");
    
    	
    	List<Userinfo> users=(List<Userinfo>) (dao.getHibernateTemplate().findByCriteria(criteria));
    	
        user.setUsers(users);
    }
    

}
