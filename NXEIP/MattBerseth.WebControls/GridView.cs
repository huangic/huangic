using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


namespace MattBerseth.WebControls
{
    /// <summary>
    /// 
    /// </summary>
    [ToolboxData("<{0}:GridView runat=server></{0}:GridView>")]
    public class GridView : System.Web.UI.WebControls.GridView, IPageableItemContainer
    {
        /// <summary>
        /// TotalRowCountAvailable event key
        /// </summary>
        private static readonly object EventTotalRowCountAvailable = new object();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dataSource"></param>
        /// <param name="dataBinding"></param>
        /// <returns></returns>
        protected override int CreateChildControls(IEnumerable dataSource, bool dataBinding)
        {
            int rows = base.CreateChildControls(dataSource, dataBinding);

            //EmptyShowHeader
            if (rows == 0)
            {
                this.Controls.Clear();
                this.Controls.Add(CreateEmptyTable());
            }

            //  if the paging feature is enabled, determine
            //  the total number of rows in the datasource
            if (this.AllowPaging)
            {
                //  if we are databinding, use the number of rows that were created,
                //  otherwise cast the datasource to an Collection and use that as the count
                int totalRowCount = dataBinding ? rows : ((ICollection)dataSource).Count;

                //  raise the row count available event
                IPageableItemContainer pageableItemContainer = this as IPageableItemContainer;
                this.OnTotalRowCountAvailable(
                    new PageEventArgs(
                        pageableItemContainer.StartRowIndex, 
                        pageableItemContainer.MaximumRows, 
                        totalRowCount
                    )
                );

                //  make sure the top and bottom pager rows are not visible
                if (this.TopPagerRow != null)
                {
                    this.TopPagerRow.Visible = false;
                }

                if (this.BottomPagerRow != null)
                {
                    this.BottomPagerRow.Visible = false;
                }
            }

            return rows;
        }

        private Table CreateEmptyTable()
        {
            Table oTable;
            GridViewRow oGridViewRow;
            TableCell oCell;
            int iCount;
            GridViewRowEventArgs e;

            oTable = base.CreateChildTable();
            iCount = this.Columns.Count - 1;

            //建立標題列
            oGridViewRow = base.CreateRow(-1, -1, DataControlRowType.Header, DataControlRowState.Normal);

            DataControlField[] oFields = new DataControlField[iCount + 1];
            this.Columns.CopyTo(oFields, 0);

            //取得目前定義 Columns 複本
            this.InitializeRow(oGridViewRow, oFields);

            //資料列初始化 
            e = new GridViewRowEventArgs(oGridViewRow);
            this.OnRowCreated(e);

            //引發 RowCreated 事件
            oTable.Rows.Add(oGridViewRow);

            //建立空白的資料列

            oGridViewRow = new GridViewRow(-1, -1, DataControlRowType.DataRow, DataControlRowState.Normal);
            oCell = new TableCell();
            oCell.ColumnSpan = oFields.Length;
            oCell.Width = Unit.Percentage(100);
            oCell.Text = this.EmptyDataText;
            oCell.HorizontalAlign = HorizontalAlign.Center;
            oGridViewRow.Cells.Add(oCell);
            oTable.Rows.Add(oGridViewRow);

            return oTable;
        }

        #region IPageableItemContainer Interface

        /// <summary>
        /// 
        /// </summary>
        /// <param name="startRowIndex"></param>
        /// <param name="maximumRows"></param>
        /// <param name="databind"></param>
        void IPageableItemContainer.SetPageProperties(
            int startRowIndex, int maximumRows, bool databind)
        {
            int newPageIndex = (startRowIndex / maximumRows);
            this.PageSize = maximumRows;

            if (this.PageIndex != newPageIndex)
            {
                bool isCanceled = false;
                if (databind)
                {
                    //  create the event args and raise the event
                    GridViewPageEventArgs args = new GridViewPageEventArgs(newPageIndex);
                    this.OnPageIndexChanging(args);

                    isCanceled = args.Cancel;
                    newPageIndex = args.NewPageIndex;
                }

                //  if the event wasn't cancelled
                //  go ahead and change the paging values
                if (!isCanceled)
                {
                    this.PageIndex = newPageIndex;

                    if (databind)
                    {
                        this.OnPageIndexChanged(EventArgs.Empty);
                    }
                }

                if (databind)
                {
                    this.RequiresDataBinding = true;
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        int IPageableItemContainer.StartRowIndex
        {
            get { return this.PageSize * this.PageIndex; }
        }

        /// <summary>
        /// 
        /// </summary>
        int IPageableItemContainer.MaximumRows
        {
            get { return this.PageSize; }
        }

        /// <summary>
        /// 
        /// </summary>
        event EventHandler<PageEventArgs> IPageableItemContainer.TotalRowCountAvailable
        {
            add { base.Events.AddHandler(GridView.EventTotalRowCountAvailable, value); }
            remove { base.Events.RemoveHandler(GridView.EventTotalRowCountAvailable, value); }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnTotalRowCountAvailable(PageEventArgs e)
        {
            EventHandler<PageEventArgs> handler = (EventHandler<PageEventArgs>)base.Events[GridView.EventTotalRowCountAvailable];
            if (handler != null)
            {
                handler(this, e);
            }
        }

        #endregion
    }
}
