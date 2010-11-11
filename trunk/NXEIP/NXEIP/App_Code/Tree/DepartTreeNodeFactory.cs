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

        SetChildNode(obj, tree);
       



        return tree;
    }


    private static void SetChildNode(DepartTreeEnum obj, DepartTreeNode tree) {
        if (obj.TreeLeafType == DepartTreeEnum.LeafType.Department)
        {
            tree.AddChildNodeStrategy(new DepartChildNode() { checkChildPeople = false });
        }

        if (obj.TreeLeafType == DepartTreeEnum.LeafType.People)
        {
            tree.AddChildNodeStrategy(new DepartChildNode() { checkChildPeople = true });
            tree.AddChildNodeStrategy(new PeopleChildNode() {  status=(int)obj.TreePeopleStatus });
        }
    }


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