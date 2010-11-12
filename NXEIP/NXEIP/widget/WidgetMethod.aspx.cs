using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NXEIP.DAO;
using Entity;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data.Common;
using System.Data;
using NXEIP.Widget.Json;
using NXEIP.Widget;

public partial class WidgetMethod : System.Web.UI.Page
{

    private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
    


    [System.Web.Services.WebMethod(true)]
    public static int SaveLayout(WidgetObj widgetObj, WidgetJson widgetPage)
    {
        //Database db = DatabaseFactory.CreateDatabase("NXEIPConnectionString");






        SessionObject sessionObj = new SessionObject();


        int page_no = GetCurrentPage(widgetPage);


        #region 砍掉所有的BLOCK
        //String delSql = "delete from block where pag_no=@pag_no";
        //DbCommand delCmd = db.GetSqlStringCommand(delSql);
        //db.AddInParameter(delCmd, "@pag_no", DbType.Int32, page_no);
        //db.ExecuteNonQuery(delCmd);

        using (NXEIPEntities model = new NXEIPEntities()) {
            var blocks = (from d in model.block where d.pag_no == page_no select d);
        
            foreach(var block in blocks){
                model.block.DeleteObject(block);
            }
            model.SaveChanges();
        }






        logger.Debug("Delete this page_no[" + page_no + "] block");

        #endregion



        //SAVE ALL BLOCK

        foreach (var p in widgetObj.Place)
        {
            //get all palce
            String place = p.Name;
            int order = 0;

            #region 存檔每一份BLOCK
            using (NXEIPEntities model = new NXEIPEntities())
            {

                foreach (var b in p.Block)
                {
                    order++;

                    //存檔BLOCK

                   

                    //String InsertSql = @"INSERT INTO block(wid_no, pag_no, blo_layout, blo_order, blo_createuid, blo_createtime) VALUES (@wid_no, @pag_no, @blo_layout, @blo_order, @blo_createuid, @blo_createtime)";
                    //DbCommand InsertCmd = db.GetSqlStringCommand(InsertSql);

                    //設定參數
                    //db.AddInParameter(InsertCmd, "@wid_no", DbType.Int32, b.WidgetID);
                    //db.AddInParameter(InsertCmd, "@pag_no", DbType.Int32, page_no);
                    //db.AddInParameter(InsertCmd, "@blo_layout", DbType.String, p.Name);
                    //db.AddInParameter(InsertCmd, "@blo_order", DbType.Int32, order);
                    //db.AddInParameter(InsertCmd, "@blo_createuid", DbType.Int32, System.Convert.ToInt32(sessionObj.sessionUserID));
                    //db.AddInParameter(InsertCmd, "@blo_createtime", DbType.DateTime, DateTime.Now);

                    //db.ExecuteNonQuery(InsertCmd);
                    block bk = new block();

                    bk.wid_no = b.WidgetID;
                    bk.pag_no = page_no;
                    bk.blo_layout = p.Name;
                    bk.blo_order = order;
                    bk.blo_createuid = System.Convert.ToInt32(sessionObj.sessionUserID);
                    bk.blo_createtime = DateTime.Now;
                    bk.blo_setting = b.Param;
                    model.block.AddObject(bk);


                }

                model.SaveChanges();
            }
            #endregion



        }


        //幹掉 SESSION 讓PAGE 重新生
        HttpContext.Current.Session[widgetPage.SessionName] = null;

        //保留TMP的免得又在編輯(改為使用AJAX)
        //HttpContext.Current.Session[widgetPage.SessionTmpName] = widgetObj;



        return 1;
    }



    /// <summary>
    /// 取回現在的page_no; //如果沒有就新建一個回傳
    /// </summary>
    /// <param name="widgetPage"></param>
    /// <returns></returns>
    public static int GetCurrentPage(WidgetJson widgetPage)
    {
        //取回現在的page_no; //如果沒有就新建一個回傳




        int uid = System.Convert.ToInt32(widgetPage.Uid);

        int page_no;
        try
        {
            page_no = GetPageNo(uid, widgetPage.PageType);

        }
        catch
        {
            //NEW 一個PAGE
            Entity.page newPage = new Entity.page();

            newPage.pag_createuid = uid;
            newPage.pag_createtime = DateTime.Now;
            newPage.pag_type = widgetPage.PageType;
            newPage.pag_uid = uid;

            // dao.AddPage(newPage);

            page_no = newPage.pag_no;
        }

        return page_no;

    }


    public static int GetPageNo(int uid, String page_type)
    {
        WidgetDAO Dao = new WidgetDAO();
        return Dao.GetPageNo(uid, page_type).Value;
    }

  

   
}
