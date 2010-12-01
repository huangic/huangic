using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;
using System.ComponentModel;


namespace NXEIP.DynamicForm
{

    /// <summary>
    /// 要填入的欄位的摘要描述
    /// </summary>
    /// 
     
    /*
    <asp:ListItem Value="0">單行文字輸入</asp:ListItem>
    <asp:ListItem Value="1">多行文字輸入</asp:ListItem>
    <asp:ListItem Value="2">下拉式選單</asp:ListItem>
    <asp:ListItem Value="3">選項項目</asp:ListItem>
    <asp:ListItem Value="4">勾選項目</asp:ListItem>
    <asp:ListItem Value="5">多選項目</asp:ListItem>
    */

    [DataObject(true)]
    public class ColumnType {
        public int No { get; set; }
        public String Name { get; set; }
        public String InputType { get; set; }
        [DefaultValue(false)]
        public bool ShowItem { get; set; }


        private  static List<ColumnType> list = new List<ColumnType>() { 
            new ColumnType(){No=0, Name="文字輸入-單行", InputType="inputbox"},
            new ColumnType(){No=1,Name="文字輸入-多行", InputType="textarea"},
            new ColumnType(){No=2,Name="單選-下拉式項目", InputType="dropdownlist",ShowItem=true},
            new ColumnType(){No=3,Name="單選-點選項目", InputType="radiobutton",ShowItem=true},
            new ColumnType(){No=4,Name="複選-勾選項目", InputType="checkbox",ShowItem=true},
            new ColumnType(){No=5,Name="複選-多選項目", InputType="listbox",ShowItem=true}
        };

        public static ColumnType GetColumnType(int no)
        {
            return list.Find(x=>x.No==no);
        }

        public List<ColumnType> GetList()
        {
            return list;
        }
    }

}