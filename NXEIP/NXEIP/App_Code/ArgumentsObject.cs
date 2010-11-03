using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entity;
using NXEIP.DAO;

/// <summary>
/// ArgumentsObject 的摘要描述
/// </summary>
public class ArgumentsObject
{
    public ArgumentsObject()
    {
      
    }

    public string Get_argValue(string var)
    {
        try
        {
            return new ArgumentsDAO().GetValueByVariable(var);
        }
        catch
        {
            return null;
        }
    }
}
