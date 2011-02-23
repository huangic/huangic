<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="350204-1.aspx.cs" Inherits="_35_350200_350204_1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="MattBerseth.WebControls" Namespace="MattBerseth.WebControls"
    TagPrefix="cc1" %>
<%@ Register src="../../lib/Navigator.ascx" tagname="Navigator" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:NXEIPConnectionString %>"
        SelectCommand="SELECT people.peo_uid, people.peo_workid, people.peo_name, departments.dep_name, people.peo_jobtype, people.peo_pfofess, people.peo_ptype, people.peo_arrivedate, people.peo_leave FROM people INNER JOIN departments ON people.dep_no = departments.dep_no">
    </asp:SqlDataSource>
    <uc1:Navigator ID="Navigator1" runat="server" SysFuncNo="350204" />
    <div class="tableDiv">
        <div class="header">
            <div class="h1">
            </div>
            <div class="h2">
                <div class="function">
                    <asp:Button ID="Button1" runat="server" Text="匯出Excel" CssClass="b-input" OnClick="Button1_Click" />
                </div>
            </div>
            <div class="h3">
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <cc1:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    CellPadding="3" CellSpacing="3" CssClass="tableData" 
                    DataKeyNames="peo_uid" DataSourceID="SqlDataSource1"
                    GridLines="None" Width="100%" OnRowDataBound="GridView1_RowDataBound" 
                    OnRowCommand="GridView1_RowCommand" 
                    onpageindexchanged="GridView1_PageIndexChanged">
                    <Columns>
                        <asp:BoundField DataField="dep_name" HeaderText="單位" SortExpression="dep_name" />
                        <asp:BoundField DataField="peo_pfofess" HeaderText="職稱" SortExpression="peo_pfofess" />
                        <asp:BoundField DataField="peo_name" HeaderText="姓名" SortExpression="peo_name" />
                        <asp:BoundField DataField="peo_ptype" HeaderText="人員類別" SortExpression="peo_ptype" />
                        <asp:BoundField DataField="peo_workid" HeaderText="人事編號" SortExpression="peo_workid" />
                        <asp:BoundField DataField="peo_arrivedate" DataFormatString="{0:yyyy-MM-dd}" HeaderText="到職日"
                            SortExpression="peo_arrivedate" />
                        <asp:BoundField DataField="peo_leave" DataFormatString="{0:yyyy-MM-dd}" HeaderText="離職日"
                            SortExpression="peo_leave" />

                        <asp:ButtonField CommandName="modify" HeaderText="修改" ControlStyle-CssClass="edit" ButtonType="Button" >
                        <ControlStyle CssClass="edit" />
                        <ItemStyle HorizontalAlign="Center" Width="7%" />
                        </asp:ButtonField>

                        <asp:ButtonField CommandName="peruse" HeaderText="檢視" ControlStyle-CssClass="peruse" ButtonType="Button" >
                        <ControlStyle CssClass="peruse" />
                        <ItemStyle HorizontalAlign="Center" Width="7%" />
                        </asp:ButtonField>

                    </Columns>
                </cc1:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="footer">
            <div class="f1">
            </div>
            <div class="f2">
            </div>
            <div class="f3">
            </div>
        </div>
        <div class="pager">
            <asp:DataPager ID="DataPager1" runat="server" PagedControlID="GridView1" PageSize="25">
                <Fields>
                    <NXEIP:GooglePagerField />
                </Fields>
            </asp:DataPager>
        </div>
        <div id="div_msg" runat="server">
            <asp:Label ID="lab_sql" runat="server" Visible="False"></asp:Label>
        </div>
    </div>
</asp:Content>
