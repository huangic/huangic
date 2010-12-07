<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="350202.aspx.cs" Inherits="_35_350200_350202" %>

<%@ Register Assembly="MattBerseth.WebControls" Namespace="MattBerseth.WebControls"
    TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="System.Web.Entity, Version=3.5.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
    Namespace="System.Web.UI.WebControls" TagPrefix="asp" %>
<%@ Register Src="../../lib/Navigator.ascx" TagName="Navigator" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function update(msg) {

            __doPostBack('<%=UpdatePanel1.ClientID%>', '');
            tb_remove();


            alert(msg);
        }

        function pageLoad(sender, args) {
            if (args.get_isPartialLoad()) {
                //  reapply the thick box stuff
                tb_init('a.thickbox');
            }
        }
    
    
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:ObjectDataSource ID="typesDataSource" runat="server" SelectMethod="GetAll" TypeName="NXEIP.DAO.TypesDAO"
        EnablePaging="True" SelectCountMethod="GetAllCount" >
        <SelectParameters>
            <asp:Parameter DefaultValue="ptype" Name="type_code" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <uc1:Navigator ID="Navigator1" runat="server" SysFuncNo="350202" />
    <div class="tableDiv">
        <div class="header">
            <div class="h1">
            </div>
            <div class="h2">
                <div class="function">
                    <input type="button" class="thickbox b-input" alt="350202-1.aspx?modal=true&TB_iframe=true"
                        value="新增人員類別" />
                </div>
            </div>
            <div class="h3">
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <cc1:GridView ID="GridView1" runat="server" DataSourceID="TypesDataSource" AutoGenerateColumns="False"
                    Width="100%" AllowPaging="True" CellPadding="3" CellSpacing="3" CssClass="tableData"
                    GridLines="None" OnRowCommand="GridView1_RowCommand" DataKeyNames="typ_no"
                    EmptyDataText="目前無資料" OnRowDataBound="GridView1_RowDataBound">
                    <Columns>
                        <asp:BoundField DataField="typ_number" HeaderText="類別代號" SortExpression="typ_number" />
                        <asp:BoundField DataField="typ_cname" HeaderText="人員類別 " SortExpression="typ_cname" />
                        <asp:BoundField DataField="typ_kind" HeaderText="行政類別 " SortExpression="typ_kind" />
                        <asp:BoundField DataField="typ_createuid" HeaderText="修建者" SortExpression="typ_createuid" />
                        <asp:BoundField DataField="typ_createtime" HeaderText="修建時間" SortExpression="typ_createtime"
                            DataFormatString="{0:yyyy-MM-dd hh:mm}" />
                        <asp:TemplateField HeaderText="修改" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <a id="btnShowPopup" runat="server" class="thickbox imageButton edit" title='<%# Eval("typ_cname", "修改{0}") %>'
                                    href='<%# Eval("typ_no", "350202-1.aspx?modal=true&mode=edit&ID={0}&TB_iframe=true&height=250&width=600") %>'>
                                    <span>修改</span>
                                </a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="刪除">
                            <ItemTemplate>
                                <asp:Button ID="Button1" runat="server" CommandArgument="<%# Container.DataItemIndex %>" CommandName="disable" OnClientClick=" return confirm('確定要刪除?')" CssClass="delete" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
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
            <asp:DataPager ID="DataPager1" runat="server" PagedControlID="GridView1" PageSize="10">
                <Fields>
                    <NXEIP:GooglePagerField />
                </Fields>
            </asp:DataPager>
        </div>
    </div>
</asp:Content>
