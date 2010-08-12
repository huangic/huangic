using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using IMMENSITY.SWFUploadAPI;
using System.Runtime.Serialization.Json;

public partial class _10_100100_100105_1 : System.Web.UI.Page
{

    SessionObject sessionObj = new SessionObject();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        



            this.UC_SWFUpload1.SwfUploadInfo = new SWFUploadInfo()
            {
                UploadMode = UpMode.LIST,
                File_size_limit = 1,
                Path="/upload/"+sessionObj.sessionUserID+"/",
                  
                Upload_url="~/lib/SWFUpload/uploadFileManager.aspx",
                
                SubmitButtonId = this.Button1.ClientID
            };



            SWFUploadFileInfo uf = new SWFUploadFileInfo();
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(SWFUploadFileInfo));
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            serializer.WriteObject(ms, uf);   

        
        
    }
    protected void Button2_Click(object sender, EventArgs e)
    {

        String msg = "Test";
        this.Page.ClientScript.RegisterStartupScript(typeof(_10_100100_100105_1), "closeThickBox", "self.parent.update('" + msg + "');", true);

    }
    protected void Button1_Click(object sender, EventArgs e)
    {

        String msg = "Test"; 
        
        this.Page.ClientScript.RegisterStartupScript(typeof(_10_100100_100105_1), "closeThickBox", "self.parent.update('" + msg + "');", true);

    }
}