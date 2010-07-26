using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;
using System.Web.Script.Serialization;
using log4net;

/// <summary>
/// FileManger 的摘要描述
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// 若要允許使用 ASP.NET AJAX 從指令碼呼叫此 Web 服務，請取消註解下一行。
[System.Web.Script.Services.ScriptService]
public class FileManager : System.Web.Services.WebService
{
    private static ILog logger = LogManager.GetLogger(typeof(FileManager));
    public FileManager()
    {

        //如果使用設計的元件，請取消註解下行程式碼 
        //InitializeComponent(); 
    }

    [WebMethod]
    [ScriptMethod( ResponseFormat = ResponseFormat.Json)]

    public string HelloWorld()
    {
        return "Hello World";
    }
    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
    public ICollection<FileJson> getFolder(String operation, string id)
    {
        logger.Debug("operation:"+operation+",ID"+id);

        ICollection<FileJson> list = new LinkedList<FileJson>();
        FileJson f = new FileJson();

        
        f.data = "Test";
        f.state = "closed";
        f.attr.id = "1";




        list.Add(f);
   
        //return Newtonsoft.Json.JsonConvert.SerializeObject( list);


        return list;
    }
}

