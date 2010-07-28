﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entity;
using NXEIP.DAO;
using IMMENSITY.SWFUploadAPI;
using System.Runtime.Serialization.Json;

public partial class _35_350200_350203 : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            
        }

     

    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int rowIndex = System.Convert.ToInt32(e.CommandArgument);
        int dep_no = System.Convert.ToInt32(this.GridView1.DataKeys[rowIndex].Value.ToString());

        if (e.CommandName.Equals("disable"))
        {
            delete(dep_no);
            return;
        }
    }

    


    private void delete(int dep_no)
    {
        DepartmentsDAO dao = new DepartmentsDAO();
        departments depart = dao.GetByDepNo(dep_no);

        depart.dep_status = "2";

        dao.Update();

        this.GridView1.DataBind();
        
        //this.OkMessagebox1.showMessagebox("delete"+dep_no);
    }

    

}