using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Threading;
using System.Text;


namespace NXEIP.Widget
{

    /// <summary>
    /// WidgetBase 的摘要描述
    /// </summary>
    public abstract class WidgetBaseControl : UserControl
    {
        public WidgetBaseControl()
        {
            
        }

        /// <summary>
        /// 可有可無 給CSS使用
        /// </summary>
        public abstract String Name
        {
            get;

        }


        private String _Title;

        public String Title
        {
            get { return _Title; }
            set { _Title = value; }
        }

        private int _WidgetId;

        public int WidgetID
        {
            get { return _WidgetId; }
            set { _WidgetId = value; }
        }

        private bool _IsEditable;
        public bool IsEditable
        {
            get { return _IsEditable; }
            set { _IsEditable = value; }

        }
        private bool _ShowTitle;
        /// <summary>
        /// Gets or sets a value indicating whether [show title].
        /// </summary>
        /// <value><c>true</c> if [show title]; otherwise, <c>false</c>.</value>
        public bool ShowTitle
        {
            get { return _ShowTitle; }
            set { _ShowTitle = value; }
        }



        public abstract void loadWidget();

        //
        protected override void Render(HtmlTextWriter writer)
        {
            this.loadWidget();

           

            StringBuilder sb = new StringBuilder();

            sb.Append("<div class=\"widget " + (IsEditable ? "widgetEdit movable removable " : "") + this.Name.Replace(" ", string.Empty).ToLowerInvariant() + "\" id=\"widget-" + WidgetID + "\">");


            sb.Append("<div class=\"widget-header\">");

            //如果是編輯模式就要加上編及項目
            if (IsEditable)
            {
                sb.Append("<strong>" + Title + "</strong>");

                //sb.Append("<a class=\"delete\" href=\"javascript:void(0)\" onclick=\"BlogEngine.widgetAdmin.removeWidget('" + WidgetID + "');return false\" title=\"移除\">X</a>");

                //sb.Append("<a class=\"edit\" href=\"javascript:void(0)\" onclick=\"BlogEngine.widgetAdmin.editWidget('" + Name + "', '" + WidgetID + "');return false\" title=\"修改 widget\">修改</a>");
                //sb.Append("<a class=\"move\" href=\"javascript:void(0)\" onclick=\"BlogEngine.widgetAdmin.initiateMoveWidget('" + WidgetID + "');return false\" title=\"搬移 widget\">搬移</a>");
            }

            //if (ShowTitle)

            //else
            //sb.Append("<br />");

            sb.Append("</div>");

            sb.Append("<div class=\"widget-content\">");

            writer.Write(sb.ToString());
            base.Render(writer);
            writer.Write("</div>");
            writer.Write("</div>");
        }

    }
}