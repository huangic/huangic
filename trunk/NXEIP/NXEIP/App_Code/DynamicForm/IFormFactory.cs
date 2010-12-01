using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;



namespace NXEIP.DynamicForm
{
    /// <summary>
    /// AbstructFormFactory 的摘要描述
    /// </summary>
    public abstract class AbstractFormFactory
    {


        public abstract Form GetInstance(String form_id);


        public abstract Form CreateFormInstance();
    }
}