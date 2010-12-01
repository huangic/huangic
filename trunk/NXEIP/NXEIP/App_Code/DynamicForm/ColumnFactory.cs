using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;



namespace NXEIP.DynamicForm
{
    /// <summary>
    /// ColumnFactory 的摘要描述
    /// </summary>
    public class ColumnFactory
    {
        public ColumnFactory()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }

        public WebControl[] CreateWebControl(Column column){

            ColumnType CType = ColumnType.GetColumnType(column.ColumnType);
            
            //建立說明文字
            Label l = new Label();
            l.Text = column.Name;


            //轉換成WebControl

            if (CType.InputType == "inputbox") {
                TextBox t = new TextBox();
                t.ID = column.UID;
                if (column.MaxLength > 0) {
                    t.MaxLength = column.MaxLength;
                }
                return new WebControl[]{l,t};

            }


            if (CType.InputType == "textarea")
            {
                TextBox t = new TextBox();
                t.TextMode = TextBoxMode.MultiLine;
                t.ID = column.UID;
                if (column.MaxLength > 0)
                {
                    t.MaxLength = column.MaxLength;
                }
                return new WebControl[] { l, t };

            }

            if (CType.InputType == "dropdownlist")
            {
                DropDownList ddl = new DropDownList();


                foreach (string i in column.Items) {
                    String[] item = i.Split('@');
                    ddl.Items.Add(new ListItem(item[0], item[1]));
                }

                
                
                
                return new WebControl[] { l, ddl };

            }

            if (CType.InputType == "radiobutton")
            {
                RadioButtonList rbl = new RadioButtonList();

                rbl.RepeatLayout = RepeatLayout.Flow;
                rbl.RepeatDirection = RepeatDirection.Horizontal;
                foreach (string i in column.Items)
                {
                    String[] item = i.Split('@');
                    rbl.Items.Add(new ListItem(item[0], item[1]));
                }




                return new WebControl[] { l, rbl };

            }


            if (CType.InputType == "checkbox")
            {
                CheckBoxList rbl = new CheckBoxList();

                rbl.RepeatLayout = RepeatLayout.Flow;
                rbl.RepeatDirection = RepeatDirection.Horizontal;
                foreach (string i in column.Items)
                {
                    String[] item = i.Split('@');
                    rbl.Items.Add(new ListItem(item[0], item[1]));
                }




                return new WebControl[] { l, rbl };

            }


            if (CType.InputType == "listbox")
            {
                ListBox rbl = new ListBox();

               
                foreach (string i in column.Items)
                {
                    String[] item = i.Split('@');
                    rbl.Items.Add(new ListItem(item[0], item[1]));
                }




                return new WebControl[] { l, rbl };

            }




            return null;
        }

        public List<WebControl[]> ConvertColumsToWebControl(List<Column> columns) {
            List<WebControl[]> list = new List<WebControl[]>();
            
            
            foreach (Column c in columns) {
                list.Add(CreateWebControl(c));
            }

            return list;
        }
    }
}