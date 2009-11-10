package idv.trans.service.index;

import idv.trans.model.SessionUserInfo;
import idv.trans.model.Userinfo;
import idv.trans.model.UserinfoDAO;
import idv.trans.service.system.SystemVar;
import idv.trans.util.SpringUtil;

import java.util.Map.Entry;

import org.hibernate.criterion.DetachedCriteria;
import org.hibernate.criterion.Restrictions;

public class LoginService {

	public SessionUserInfo login(String account, String pwd) {
		UserinfoDAO dao = (UserinfoDAO) SpringUtil.getBean("UserinfoDAO");
		// 查詢條件
		DetachedCriteria criteria = DetachedCriteria.forClass(Userinfo.class)
				.add(Restrictions.eq("account", account)).add(
						Restrictions.eq("password", pwd));

		
		Userinfo user=null;
		SessionUserInfo sessionUser=null;
		try{
		user=(Userinfo)(dao.getHibernateTemplate().findByCriteria(criteria)).get(0);
		sessionUser=new SessionUserInfo();
		
		sessionUser.setUserInfo(user);
		
		SystemVar var=(SystemVar)SpringUtil.getBean("systemVarBean");
		  String RoleName=(String)var.getUserLevel().get(String.valueOf(user.getRole()));
		
		
		
		sessionUser.setUserRoleName(RoleName);
		
		
		}catch(Exception ex){
			ex.printStackTrace();
		}
		
		return sessionUser;
				

	}
}
