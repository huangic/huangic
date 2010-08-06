using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// ArgumentsObject 的摘要描述
/// </summary>
public class ArgumentsObject
{
    public ArgumentsObject()
    {
        //
        // TODO: 在此加入建構函式的程式碼
        //
    }

    public string Get_argValue(string var)
    {
        string argValue = string.Empty;

        try
        {

            System.Data.DataTable mytable = new DBObject().ExecuteQuery("select arg_value from arguments where arg_variable = '" + var + "'");

            if (mytable.Rows.Count > 0)
            {
                argValue = mytable.Rows[0]["arg_value"].ToString();
            }
        }
        catch
        {
        }


        return argValue;
    }
}
