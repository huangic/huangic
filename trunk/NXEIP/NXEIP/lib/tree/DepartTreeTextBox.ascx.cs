using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Collections;
using Entity;
using System.ComponentModel;
using NXEIP.Tree;

public partial class lib_tree_DepartTreeTextBox : System.Web.UI.UserControl
{

    /// <summary>
    /// 樹狀類型
    /// </summary>
    [Category("自訂屬性")]
    [Browsable(true)]
    [Description("樹狀類型")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue(DepartTreeEnum.NodeType.All)]
    public DepartTreeEnum.NodeType TreeType { get; set; }

    /// <summary>
    /// 樹葉類型
    /// </summary>
    [Category("自訂屬性")]
    [Browsable(true)]
    [Description("葉類型")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue(DepartTreeEnum.LeafType.Department)]
    public DepartTreeEnum.LeafType LeafType { get; set; }

    
    /// <summary>
    /// 人員類別
    /// </summary>
    [Category("人員屬性")]
    [Browsable(true)]
    [Description("人員顯示")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue(DepartTreeEnum.PeopleStatus.OnJob)]
    public DepartTreeEnum.PeopleStatus PeopleStatus { get; set; }


    [Category("人員類別")]
    [Browsable(true)]
    [Description("人員顯示")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue(DepartTreeEnum.PeopleType.General)]
    public DepartTreeEnum.PeopleType PeopleType { get; set; }

    [Category("人員類別")]
    [Browsable(true)]
    [Description("人員顯示欄位")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue(DepartTreeEnum.PeopleColumn.Name)]
    public DepartTreeEnum.PeopleColumn PeopleColumn { get; set; }





    /// <summary>
    /// 取所有的VALUE值
    /// </summary>
    [Browsable(false)]
    public List<String> ItemsValue
    {
        get
        {

            List<String> values = Items.Select(x => x.Key).ToList();

            return values;


        }
    }
     
    [Browsable(false)]
    public List<KeyValuePair<String,String>> Items{
        get{
            return (List<KeyValuePair<String, String>>)Session[SessionName]??new List<KeyValuePair<String,String>>();
        }
       
    }


    public void Add(String id)
    {
        Add(int.Parse(id));
    }

    public void Add(int id)
    {
        ChildNode ChildNodeStrategy = DepartTreeNodeFactory.CreateChildNode(this.LeafType);

        List<KeyValuePair<String, String>> items = this.Items;

        KeyValuePair<String, String>? item = ChildNodeStrategy.GetKeyValuePair(id);


        if (item.HasValue)
        {

            items.Add(item.Value);

            this.Session[SessionName] = items;
        }

        //this.Items = items;


    }



    private string SessionName {
        get{ 
        
        return this.TextBox1.ClientID;}
    }


    public void Clear() {
        Session[SessionName] = new List<KeyValuePair<String,String>>();
    }
    
    protected override void OnPreRender(EventArgs e)
    {
       
        
        
        //base.OnPreRender(e);
        try
        {
            List<KeyValuePair<String, String>> item = this.Items;

            //ListBox lb = (ListBox)this.ListBox1;

            //lb.Items.Clear();

            foreach (KeyValuePair<String, String> value in item)
            {
                TextBox1.Text = value.Value;
            }
        }
        catch
        {
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        this.HyperLink1.NavigateUrl = @"~/lib/tree/DepartTreePage.aspx
            ?session=" + this.SessionName + 
            "&TreeType="+(int)this.TreeType+
            "&LeafType="+(int)this.LeafType+
            "&PeopleStatus="+(int)this.PeopleStatus+
            "&SelectMode="+(int)DepartTreeEnum.SelectMode.Single+
            "&PeopleColumn=" + (int)this.PeopleColumn +
            "&PeopleType=" + (int)this.PeopleType +
            "&TB_iframe=true&height=420&width=540&modal=true";
        
        
    }


    
}
