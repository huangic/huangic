package idv.trans.service.record;

import idv.trans.model.Fileinfo;
import idv.trans.model.FileinfoDAO;
import idv.trans.model.FileinfoUser;
import idv.trans.struts.action.Record;
import idv.trans.util.CheckUtil;
import idv.trans.util.SpringUtil;

import java.util.Date;
import java.util.List;

import org.apache.commons.lang.time.DateUtils;
import org.hibernate.criterion.DetachedCriteria;
import org.hibernate.criterion.Order;
import org.hibernate.criterion.Restrictions;

public class RecordService {

	
	
	
	
	public List<FileinfoUser> findFile(Record record) {
		FileinfoDAO dao = (FileinfoDAO) SpringUtil.getBean("FileinfoDAO");
		DetachedCriteria criteria = DetachedCriteria.forClass(
				FileinfoUser.class, "files").createAlias("files.uploaduser",
				"user");
      
		// 設定條件
		try {
			// 使用者角色
			if (!CheckUtil.isNull(record.getRole())) {
				if (!record.getRole().equals("")) {
					criteria
							.add(Restrictions.eq("user.role", record.getRole()));

				}
			}

			// 使用者帳號
			if (!record.getAccount().equals("")) {
				criteria.add(Restrictions.eq("user.account", record
						.getAccount()));

			}
			
			

			// 檔案名稱
			if (!record.getFilename().equals("")) {
				criteria.add(Restrictions.like("filename", record
						.getFilename()+"%"));

			}
			

			// 使用者系統全縣
			if (!CheckUtil.isNull(record.getPriority())) {
			
			if (!record.getPriority().equals("")) {
				criteria.add(Restrictions.eq("user.priority", record
						.getPriority()));

			}
			}

			// 日期區間
			if (record.getStartDate() != null && record.getEndDate() != null) {
				Date endDate=DateUtils.addDays(record.getEndDate(), 1);
				
				criteria.add(Restrictions.between("uploaddate", record
						.getStartDate(), endDate));

			}
			
			//狀態
			List<Short> status=record.getStatus();
			
			
			//for(int i=0,size=(status.size());i<size;i++) {
				
				
				criteria.add(Restrictions.in("status", status));

			//}
				
				criteria.addOrder(Order.desc("uploaddate"));
			

		} catch (Exception ex) {
			// 查詢條件的問題
			ex.printStackTrace();
		}

		// 查詢
		List<FileinfoUser> records = (List<FileinfoUser>) (dao
				.getHibernateTemplate().findByCriteria(criteria));

		return records;
	}
	
	public void cancelTrans(Record record) throws Exception{
		FileinfoDAO dao = (FileinfoDAO) SpringUtil.getBean("FileinfoDAO");
		
		Long fileid=record.getFileid();
		
		Fileinfo file=(Fileinfo)dao.findById(fileid);
		if(file==null){
			throw new Exception("操作錯誤，無此資料");
				
		}else{
			if(file.getStatus()!=1){
				throw new Exception("此資料非待處理中，無法取消");
			}
			file.setStatus(Short.valueOf("4"));
			//file.setTransdate(new Date());
			
			dao.attachDirty(file);
		}
		
		
	}
	
	
	

}
