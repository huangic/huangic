using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NLog;

public partial class _10_100100_100105 : System.Web.UI.Page
{

    private Logger logger = LogManager.GetCurrentClassLogger();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Navigator1.SysFuncNo = "100105";
        logger.Debug("TEST");
    }
}
