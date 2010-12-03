using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Entity;


namespace NXEIP.DynamicForm
{
    /// <summary>
    /// EntityFormFactory 的摘要描述
    /// </summary>
    public class EntitySubmitFactory:AbstractFormFactory
    {
        public EntitySubmitFactory()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }



        public override Form GetInstance(string form_id)
        {
            int f02_no = int.Parse(form_id);
           
            Form f = new Form();

            UtilityDAO udao = new UtilityDAO();

            using (NXEIPEntities model = new NXEIPEntities())
            {
                var form = (from d in model.form02 where d.f02_no == f02_no select d).First();
            

            

            f.Id = form.f02_no.ToString();

            f.CreareUserNO = form.f02_createuid+"";
            f.CreateUser = udao.Get_PeopleName(form.f02_createuid.Value);
            f.HandleUserNO = form.peo_uid+"";
            f.HandleUser = udao.Get_PeopleName(form.peo_uid.Value);
            f.Columns = Column.ConvertJonToColumns(form.f02_context);
            f.CreateTime = form.f02_createtime.Value;
              
            }
            return f;
        }

        public override Form CreateFormInstance()
        {
            return new Form();
        }
    }
}