using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using IMMENSITY.SWFUploadAPI;
using System.Runtime.Serialization.Json;
using System.Data.SqlClient;
using log4net;

public partial class AjaxTest : System.Web.UI.Page
{
    private static ILog logger = LogManager.GetLogger(typeof(AjaxTest));




    protected void Page_Load(object sender, EventArgs e)
    {

        // File Upload initial
        this.UC_SWFUpload1.SwfUploadInfo = new SWFUploadInfo()
        {
            UploadMode = UpMode.LIST,
            File_size_limit = 1,
            SubmitButtonId = this.Button1.ClientID
        };



        SWFUploadFileInfo uf = new SWFUploadFileInfo();
        DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(SWFUploadFileInfo));
        System.IO.MemoryStream ms = new System.IO.MemoryStream();
        serializer.WriteObject(ms, uf);   

       

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        foreach(var obj in this.jQueryDepartTree1.Items){
            logger.Debug(obj.Key +"_"+obj.Value);
        }
    }
}
