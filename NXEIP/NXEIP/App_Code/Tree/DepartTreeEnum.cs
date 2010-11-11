using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;



namespace NXEIP.Tree
{
    /// <summary>
    /// 部門樹功能的列舉直
    /// </summary>
    public class DepartTreeEnum
    {
        public DepartTreeEnum()
        {
            //
            // TODO: 在此加入建構函式的程式碼
            //
        }

        public DepartTreeEnum(HttpRequest request) {
            this.TreeLeafType = (DepartTreeEnum.LeafType)int.Parse(request["LeafType"]);
            this.TreeNodeType = (DepartTreeEnum.NodeType)int.Parse(request["TreeType"]);
            this.TreeSelectMode = (DepartTreeEnum.SelectMode)int.Parse(request["SelectMode"]);
            this.TreePeopleStatus = (DepartTreeEnum.PeopleStatus)int.Parse(request["PeopleStatus"]);
            this.TreePeopleColumn = (DepartTreeEnum.PeopleColumn)int.Parse(request["PeopleColumn"]);
            this.TreePeopleType = (DepartTreeEnum.PeopleType)int.Parse(request["PeopleType"]);
        }




        public NodeType TreeNodeType { get; set; }

        public LeafType TreeLeafType { get; set; }

        public PeopleStatus TreePeopleStatus { get; set; }

        public SelectMode TreeSelectMode { get; set; }

        public PeopleType TreePeopleType{get;set;}

        public PeopleColumn TreePeopleColumn { get; set; }


        /// <summary>
        /// 節點類型
        /// </summary>
        public enum NodeType:int{
            All,Child,SameLevel,Auth
        }
        /// <summary>
        /// 葉類型
        /// </summary>
        public enum LeafType : int { 
            Department,People
        }


        /// <summary>
        /// 選擇模式
        /// </summary>
        public enum SelectMode : int { 
            Multi,Single
        }
        /// <summary>
        /// 人員狀態
        /// </summary>
        public enum PeopleStatus : int { 
            All,OnJob, StopJob
        }

        
        public enum PeopleType : int {
            General,Contract 
        }
        
        [Flags]
        public enum PeopleColumn : int { 
            Name=1,
            Title=2,
            Ext=4
        }

    }
}