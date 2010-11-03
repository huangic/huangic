using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NXEIP.JsTree;

namespace NXEIP.FileManager.Json{
/// <summary>
/// PermissionTreeAttrJson 的摘要描述
/// </summary>
public class PermissionTreeAttr:JsTreeAttr
{
	public PermissionTreeAttr()
	{
		
	}

    public String rel { get; set; }
    public String nType { get; set; }
    public String nName { get; set; }

}
}