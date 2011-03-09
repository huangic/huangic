using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entity;

public partial class lib_ShowPic : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            string tb = Request["tb"];
            string picorder = Request["picorder"];
            int pkno = Convert.ToInt32(Request["pkno"]);
            string filename = "";
            byte[] files1 = null;
            try
            {
                using (NXEIPEntities model = new NXEIPEntities())
                {

                    if (tb.Equals("rooms"))
                    {
                        #region 場地圖片
                        rooms rooms1 = (from r in model.rooms where r.roo_no == pkno select r).FirstOrDefault();
                        if (rooms1 != null)
                        {
                            if (picorder.Equals("1"))
                            {
                                if (rooms1.roo_pictype.Trim().Length > 0)
                                {
                                    filename = rooms1.roo_pictype;
                                    files1 = rooms1.roo_picture;
                                }
                            }
                            else
                            {
                                if (rooms1.roo_planetype.Trim().Length > 0)
                                {
                                    filename = rooms1.roo_planetype;
                                    files1 = rooms1.roo_plane;
                                }
                            }
                        }
                        #endregion
                    }
                    else if (tb.Equals("equipments"))
                    {
                        #region 設備圖片
                        equipments equ1 = (from equ in model.equipments where equ.equ_no == pkno select equ).FirstOrDefault();
                        if (equ1 != null)
                        {
                            filename = equ1.equ_pictype;
                            files1 = equ1.equ_pic;
                        }
                        #endregion
                    }
                    else if (tb.Equals("m02"))
                    {
                        #region 設備圖片
                        m02 tbdata = (from tbm02 in model.m02 where tbm02.m02_no == pkno select tbm02).FirstOrDefault();
                        if (tbdata != null)
                        {
                            filename = tbdata.m02_pictype;
                            files1 = tbdata.m02_pic;
                        }
                        #endregion
                    }
                }
                if (filename.Length > 0)
                {
                    Response.AddHeader("Accept-Language", "zh-tw");
                    Response.AddHeader("content-disposition", "attachment; filename=" + filename);
                    Response.ContentType = "Application/octet-stream";
                    Response.BinaryWrite(files1);
                }
            }
            catch
            {
                Response.Write("圖片顯示發生錯誤!!");
            }
        }
    }
}