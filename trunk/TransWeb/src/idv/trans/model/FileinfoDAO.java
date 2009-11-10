package idv.trans.model;

import java.util.Date;
import java.util.List;
import org.apache.commons.logging.Log;
import org.apache.commons.logging.LogFactory;
import org.hibernate.LockMode;
import org.springframework.context.ApplicationContext;
import org.springframework.orm.hibernate3.support.HibernateDaoSupport;

/**
 * A data access object (DAO) providing persistence and search support for
 * Fileinfo entities. Transaction control of the save(), update() and delete()
 * operations can directly support Spring container-managed transactions or they
 * can be augmented to handle user-managed Spring transactions. Each of these
 * methods provides additional information for how to configure it for the
 * desired type of transaction control.
 * 
 * @see idv.trans.model.Fileinfo
 * @author MyEclipse Persistence Tools
 */

public class FileinfoDAO extends BaseDao {
	private static final Log log = LogFactory.getLog(FileinfoDAO.class);
	// property constants
	public static final String USERID = "userid";
	public static final String FILENAME = "filename";
	public static final String NEWFILENAME = "newfilename";
	public static final String FILEPATH = "filepath";
	public static final String STATUS = "status";
	public static final String ALLNUM = "allnum";
	public static final String SUCCESSNUM = "successnum";
	public static final String ERRORNUM = "errornum";
	public static final String LOGPATH = "logpath";
	public static final String LOGFILENAME = "logfilename";

	protected void initDao() {
		// do nothing
	}

	public void save(Fileinfo transientInstance) {
		log.debug("saving Fileinfo instance");
		try {
			getHibernateTemplate().save(transientInstance);
			log.debug("save successful");
		} catch (RuntimeException re) {
			log.error("save failed", re);
			throw re;
		}
	}

	public void delete(Fileinfo persistentInstance) {
		log.debug("deleting Fileinfo instance");
		try {
			getHibernateTemplate().delete(persistentInstance);
			log.debug("delete successful");
		} catch (RuntimeException re) {
			log.error("delete failed", re);
			throw re;
		}
	}

	public Fileinfo findById(java.lang.Long id) {
		log.debug("getting Fileinfo instance with id: " + id);
		try {
			Fileinfo instance = (Fileinfo) getHibernateTemplate().get(
					"idv.trans.model.Fileinfo", id);
			return instance;
		} catch (RuntimeException re) {
			log.error("get failed", re);
			throw re;
		}
	}

	public List findByExample(Fileinfo instance) {
		log.debug("finding Fileinfo instance by example");
		try {
			List results = getHibernateTemplate().findByExample(instance);
			log.debug("find by example successful, result size: "
					+ results.size());
			return results;
		} catch (RuntimeException re) {
			log.error("find by example failed", re);
			throw re;
		}
	}

	public List findByProperty(String propertyName, Object value) {
		log.debug("finding Fileinfo instance with property: " + propertyName
				+ ", value: " + value);
		try {
			String queryString = "from Fileinfo as model where model."
					+ propertyName + "= ?";
			return getHibernateTemplate().find(queryString, value);
		} catch (RuntimeException re) {
			log.error("find by property name failed", re);
			throw re;
		}
	}

	public List findByUserid(Object userid) {
		return findByProperty(USERID, userid);
	}

	public List findByFilename(Object filename) {
		return findByProperty(FILENAME, filename);
	}

	public List findByNewfilename(Object newfilename) {
		return findByProperty(NEWFILENAME, newfilename);
	}

	public List findByFilepath(Object filepath) {
		return findByProperty(FILEPATH, filepath);
	}

	public List findByStatus(Object status) {
		return findByProperty(STATUS, status);
	}

	public List findByAllnum(Object allnum) {
		return findByProperty(ALLNUM, allnum);
	}

	public List findBySuccessnum(Object successnum) {
		return findByProperty(SUCCESSNUM, successnum);
	}

	public List findByErrornum(Object errornum) {
		return findByProperty(ERRORNUM, errornum);
	}

	public List findByLogpath(Object logpath) {
		return findByProperty(LOGPATH, logpath);
	}

	public List findByLogfilename(Object logfilename) {
		return findByProperty(LOGFILENAME, logfilename);
	}

	public List findAll() {
		log.debug("finding all Fileinfo instances");
		try {
			String queryString = "from Fileinfo";
			return getHibernateTemplate().find(queryString);
		} catch (RuntimeException re) {
			log.error("find all failed", re);
			throw re;
		}
	}

	public Fileinfo merge(Fileinfo detachedInstance) {
		log.debug("merging Fileinfo instance");
		try {
			Fileinfo result = (Fileinfo) getHibernateTemplate().merge(
					detachedInstance);
			log.debug("merge successful");
			return result;
		} catch (RuntimeException re) {
			log.error("merge failed", re);
			throw re;
		}
	}

	public void attachDirty(Fileinfo instance) {
		log.debug("attaching dirty Fileinfo instance");
		try {
			getHibernateTemplate().saveOrUpdate(instance);
			log.debug("attach successful");
		} catch (RuntimeException re) {
			log.error("attach failed", re);
			throw re;
		}
	}

	public void attachClean(Fileinfo instance) {
		log.debug("attaching clean Fileinfo instance");
		try {
			getHibernateTemplate().lock(instance, LockMode.NONE);
			log.debug("attach successful");
		} catch (RuntimeException re) {
			log.error("attach failed", re);
			throw re;
		}
	}

	public static FileinfoDAO getFromApplicationContext(ApplicationContext ctx) {
		return (FileinfoDAO) ctx.getBean("FileinfoDAO");
	}
}