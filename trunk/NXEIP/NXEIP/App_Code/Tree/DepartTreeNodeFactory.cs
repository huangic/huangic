using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NXEIP.Tree;

/// <summary>
/// DepartTreeNodeFactory 的摘要描述
/// </summary>
public class DepartTreeNodeFactory
{
	public DepartTreeNodeFactory()
	{
		//
		// TODO: 在此加入建構函式的程式碼
		//
	}


    public static DepartTreeNode CreateDepartTreeNode(DepartTreeEnum obj ){

        DepartTreeNode tree=null;
        
        if (obj.TreeNodeType == DepartTreeEnum.NodeType.All) {
            tree = new AllDepartTreeNode();
        }


        if (obj.TreeNodeType == DepartTreeEnum.NodeType.Self) { 
            int dep_id=Convert.ToInt32(HttpContext.Current.Session["UserDepartID"]);


            tree = new SelfDepartTreeNode()
            {
                CurrentDepId = dep_id
            };
        }


        if (obj.TreeNodeType == DepartTreeEnum.NodeType.Parallel)
        {
            int dep_id = Convert.ToInt32(HttpContext.Current.Session["UserDepartID"]);


            tree = new ParallelDepartTreeNode()
            {
                CurrentDepId = dep_id
            };
        }


        Dictionary<String, String> setting = new Dictionary<string, string>();

        setting.Add("people_uid", (String)HttpContext.Current.Session["UserID"]);



        SetChildNode(obj, tree,setting);
       



        return tree;
    }


    private static void SetChildNode(DepartTreeEnum obj, DepartTreeNode tree,Dictionary<String,String> Setting) {
        if (obj.TreeLeafType == DepartTreeEnum.LeafType.Department)
        {
            tree.AddChildNodeStrategy(new DepartChildNode() { checkChildPeople = false });
        }

        if (obj.TreeLeafType == DepartTreeEnum.LeafType.People)
        {

            int peo_uid = int.Parse(Setting["people_uid"]??"0");
            
            tree.AddChildNodeStrategy(new DepartChildNode() { checkChildPeople = true });
            tree.AddChildNodeStrategy(new PeopleChildNode() { setting = obj, people_uid = peo_uid });
        }
    }

    /// <summary>
    /// CHILD NODE 的生成
    /// LISTBOX 與TEXTBOX使用
    /// </summary>
    /// <param name="leaf"></param>
    /// <returns></returns>
    public static ChildNode CreateChildNode(DepartTreeEnum.LeafType leaf){
        if (leaf == DepartTreeEnum.LeafType.Department)
        {
            return new DepartChildNode();
        }
        else {
            return new PeopleChildNode();
        }
    }
}