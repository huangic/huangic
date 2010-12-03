using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI;



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

        private WebControl[] CreateWebControl(Column column){

            ColumnType CType = ColumnType.GetColumnType(column.ColumnType);
            
            //建立說明文字
            Label l = new Label();
            l.Text = column.Name;
            if (column.Required)
                l.Text += "(必填)";

            WebControl[] ws={l,null};

            ws[1] = this.GetColumnTypeWebControl(column);
            //轉換成WebControl
            if (ws[1] != null)
            {
                ws[1].ClientIDMode = ClientIDMode.Static;
            }

            InitListControl(ws[1],column);

         


            return ws;
        }

        public List<WebControl[]> ConvertColumsToWebControl(List<Column> columns) {
            List<WebControl[]> list = new List<WebControl[]>();
            
            
            foreach (Column c in columns) {
                list.Add(CreateWebControl(c));
            }

            return list;
        }


        public List<Column> GetWebControlValue(Control master, List<Column> columns) {


            foreach (Column col in columns)
            {
                WebControl item = (WebControl)master.FindControl(col.UID);

                List<String> values = new List<string>();
                
                
                if (item is ListControl) {
                    ListControl lc = item as ListControl;
                    //col.Value = lc.SelectedValue;

                    //var listvalue=lc.Items
                    values = new List<string>();

                    foreach (ListItem i in lc.Items) {
                        if (i.Selected) {
                            values.Add(i.Value);
                        }
                    }

                    col.Value = values;

                }

                if (item is TextBox) {
                    TextBox tb = item as TextBox;
                    values.Add(tb.Text);
                }
                col.Value = values;
            }
            return columns;
        }

        /// <summary>
        /// 取欄位的WebControl
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        private WebControl GetColumnTypeWebControl(Column column){
            ColumnType CType = ColumnType.GetColumnType(column.ColumnType);
           
            if (CType.InputType == "inputbox")
            {
                TextBox t = new TextBox();
                t.ID = column.UID;

                if (column.MaxLength > 0)
                {
                    t.MaxLength = column.MaxLength;
                }
                return t;

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
                return t;

            }

            if (CType.InputType == "dropdownlist")
            {
                DropDownList c = new DropDownList();
                c.ID = column.UID;

                c.ClientIDMode = ClientIDMode.Static;

                return c;

            }

            if (CType.InputType == "radiobutton")
            {
                RadioButtonList c = new RadioButtonList();
                c.ID = column.UID;
                c.RepeatLayout = RepeatLayout.Flow;
                c.RepeatDirection = RepeatDirection.Horizontal;
                return c;

            }


            if (CType.InputType == "checkbox")
            {
                CheckBoxList c = new CheckBoxList();
                c.ID = column.UID;
                c.RepeatLayout = RepeatLayout.Flow;
                c.RepeatDirection = RepeatDirection.Vertical;
                
                return c;

            }


            if (CType.InputType == "listbox")
            {
                ListBox c = new ListBox();
                c.ID = column.UID;
                c.SelectionMode = ListSelectionMode.Multiple;
                return c;
            }




            return null;
        }

        /// <summary>
        /// 將有選單的List加上LISTITEM;
        /// </summary>
        /// <param name="wc"></param>
        /// <param name="column"></param>
        private void InitListControl(WebControl wc,Column column){

            //如果是LIST 類型的 要塞入LISTITEM
            if (wc is ListControl)
            {
                ListControl ls =wc as ListControl;

                if (ls is DropDownList) {
                    ls.Items.Add(new ListItem("請選擇", ""));
                }


                foreach (string i in column.Items)
                {
                    String[] item = i.Split('@');
                    ls.Items.Add(new ListItem(item[0], item[1]));
                }

            }

         

        }

        private WebControl[] CreateDisplayWebControl(Column column) {
            ColumnType CType = ColumnType.GetColumnType(column.ColumnType);

            //建立說明文字
            Label l = new Label();
            l.Text = column.Name;

            WebControl[] ws = { l, null };


            Label lb_value = new Label();

            lb_value.Text = String.Join("<br/>", column.Value);

            ws[1] = lb_value;
            //轉換成WebControl
            if (ws[1] != null)
            {
                ws[1].ClientIDMode = ClientIDMode.Static;
            }

            InitListControl(ws[1], column);




            return ws;
        }

        public List<WebControl[]> ConvertColumsToDisplayWebControl(List<Column> columns)
        {
            List<WebControl[]> list = new List<WebControl[]>();


            foreach (Column c in columns)
            {
                list.Add( this.CreateDisplayWebControl(c));
            }

            return list;
        }
        


    }
}