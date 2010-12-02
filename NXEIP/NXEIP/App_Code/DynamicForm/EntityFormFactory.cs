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
    public class EntityFormFactory:AbstractFormFactory
    {
        public EntityFormFactory()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }



        public override Form GetInstance(string form_id)
        {
            int f01_no = int.Parse(form_id);
           
            Form f = new Form();

            UtilityDAO udao = new UtilityDAO();

            using (NXEIPEntities model = new NXEIPEntities())
            {
                var form = (from d in model.form01 where d.f01_no == f01_no select d).First();
            

            

            f.Id = form.f01_no.ToString();
            f.Name = form.f01_name;
            f.Status = form.f01_status;
            f.CreareUserNO = form.f01_createuid+"";
            f.CreateUser = udao.Get_PeopleName(form.f01_createuid.Value);
            f.HandleUserNO = form.peo_uid+"";
            f.HandleUser = udao.Get_PeopleName(form.peo_uid);
            f.Columns = Column.ConvertJonToColumns(form.f01_columns);
            f.Description = form.f01_description;
            }
            return f;
        }

        public override Form CreateFormInstance()
        {
            return new Form();
        }
    }
}