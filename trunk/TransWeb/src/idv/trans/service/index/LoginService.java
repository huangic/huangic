package idv.trans.service.index;

import idv.trans.model.Userinfo;
import idv.trans.model.UserinfoDAO;
import idv.trans.util.SpringUtil;

import org.hibernate.criterion.DetachedCriteria;
import org.hibernate.criterion.Restrictions;

public class LoginService {

	public Userinfo login(String account, String pwd) {
		UserinfoDAO dao = (UserinfoDAO) SpringUtil.getBean("UserinfoDAO");
		// 查詢條件
		DetachedCriteria criteria = DetachedCriteria.forClass(Userinfo.class)
				.add(Restrictions.eq("account", account)).add(
						Restrictions.eq("password", pwd));

		return (Userinfo) (dao.getHibernateTemplate().findByCriteria(criteria)
				.get(0));

	}
}
