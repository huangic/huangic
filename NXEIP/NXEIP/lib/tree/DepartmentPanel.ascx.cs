using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using NXEIP.Tree;
using System.ComponentModel;
using System.Collections.Specialized;
using System.ComponentModel.Design;
using System.Drawing.Design;
using Entity;



public partial class lib_tree_DepartmentPanel : System.Web.UI.UserControl
{




    [Category("自訂屬性")]
    [Browsable(true)]
    [Description("樹狀類型")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue(true)]
    public bool ShowDeleteButton { get; set; }

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
    /// 樹葉類型
    /// </summary>
    [Category("自訂屬性")]
    [Browsable(true)]
    [Description("選擇型態")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue(DepartTreeEnum.SelectMode.Multi)]
    public DepartTreeEnum.SelectMode SelectMode { get; set; }

    /// <summary>
    /// 人員類別
    /// </summary>
    [Category("人員屬性")]
    [Browsable(true)]
    [Description("人員顯示")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue(DepartTreeEnum.PeopleStatus.OnJob)]
    public DepartTreeEnum.PeopleStatus PeopleStatus{ get; set; }


    [Category("人員屬性")]
    [Browsable(true)]
    [Description("人員是否在職")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue(DepartTreeEnum.PeopleType.General)]
    public DepartTreeEnum.PeopleType PeopleType { get; set; }

     [Category("人員屬性")]
    [Browsable(true)]
    [Description("人員顯示欄位")]
    [RefreshProperties(RefreshProperties.All)]
    [DefaultValue(DepartTreeEnum.PeopleColumn.Name)]
    public DepartTreeEnum.PeopleColumn PeopleColumn { get; set; }



     [Category("人員是否秀自己?")]
     [Browsable(true)]
     [Description("人員是否秀自己")]
     [RefreshProperties(RefreshProperties.All)]
     [DefaultValue(DepartTreeEnum.PeopleShowSelf.True)]
     public DepartTreeEnum.PeopleShowSelf PeopleShowSelf { get; set; }


    public string ParentSessionID { get; set; }



    /// <summary>
    /// 取所有的物件VALUE值與TEXT值
    /// </summary>
     [Browsable(false)]
    public List<KeyValuePair<String, String>> Items { 
        //取LISTBOX
        get { 
                 
            //取SESSION
            
           
            return (List<KeyValuePair<String, String>>)Session[this.ClientID]??new List<KeyValuePair<String,String>>();
             
        }

        set {
            this.Session[this.ClientID] = value;
        }

    }
    /// <summary>
    /// 取所有的VALUE值
    /// </summary>
    [Browsable(false)]
    public List<String> ItemsValue {
        get {
           
                List<String> values = Items.Select(x => x.Key).ToList();

                return values;
           
                
        }
    }


    public void Clear() {
        this.Items = new List<KeyValuePair<string, string>>();
    
    }

    public void Add(String id) {
        Add(int.Parse(id));
    }

    public void Add(int id) {
        ChildNode ChildNodeStrategy = DepartTreeNodeFactory.CreateChildNode(this.LeafType);

        List<KeyValuePair<String, String>> items = this.Items;

        KeyValuePair<String, String>? item = ChildNodeStrategy.GetKeyValuePair(id);

        if (item.HasValue)
        {
            items.Add(item.Value);

            this.Items = items;
        }

    }

    
    protected void Page_Load(object sender, EventArgs e)
    {
          
        //init LABEL
        using (NXEIPEntities model = new NXEIPEntities()) {
            var dep = (from d in model.departments where d.dep_level == 0 select d).First();
            this.Label1.Text = dep.dep_name;
        }

        


    }

    protected override void OnPreRender(EventArgs e)
    {
        base.OnPreRender(e);
        //init script
        ScriptManager.RegisterClientScriptInclude(this, typeof(UserControl), "JQuery", ResolveClientUrl("~/js/jquery-1.4.2.min.js"));
        ScriptManager.RegisterClientScriptInclude(this, typeof(UserControl), "JsTree", ResolveClientUrl("~/js/jquery.jstree.js"));
        ScriptManager.RegisterClientScriptInclude(this, typeof(UserControl), "jQuery.Cookie", ResolveClientUrl("~/js/jquery.cookie.js"));

        ScriptManager.RegisterClientScriptInclude(this, typeof(UserControl), "DepartTree", ResolveClientUrl("~/js/jquery.department.tree.js"));

        this.ButtonPanel.Visible = this.ShowDeleteButton;


        //init listBox

        try
        {
            List<KeyValuePair<String, String>> item = this.Items;

            ListBox lb = (ListBox)this.ListBox2;

            lb.Items.Clear();

            foreach (KeyValuePair<String, String> value in item)
            {
                lb.Items.Add(new ListItem(value.Value, value.Key));
            }
        }
        catch
        {
        }

       
    }

    public void InitSetting(DepartTreeEnum setting) {
        this.TreeType = setting.TreeNodeType;
        this.LeafType = setting.TreeLeafType;
        this.SelectMode = setting.TreeSelectMode;
        this.PeopleColumn = setting.TreePeopleColumn;
        this.PeopleStatus = setting.TreePeopleStatus;
        this.PeopleType = setting.TreePeopleType;
        this.PeopleShowSelf = setting.TreePeopleShowSelf;
    }

}