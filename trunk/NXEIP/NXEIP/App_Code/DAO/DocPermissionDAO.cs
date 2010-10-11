using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NXEIP;
using Entity;

/// <summary>
/// DocPermission 的摘要描述
/// </summary>
public class DocPermissionDAO
{
	public DocPermissionDAO()
	{
		//
		// TODO: 在此加入建構函式的程式碼
		//
	}

    public IQueryable GetAll(int doc_no){
        using (NXEIPEntities model = new NXEIPEntities()) {
           
             //檔案權限的物件
            var groupA = 
                (from b in model.doc04
                from a in model.doc03 where a.d01_no == doc_no && a.d03_type=="2");
       
        
        
        }
    }
}