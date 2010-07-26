<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="350101.aspx.cs" Inherits="_35_350100_350101" %>


<%@ Register Assembly="MattBerseth.WebControls" Namespace="MattBerseth.WebControls"
    TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register assembly="System.Web.Entity, Version=3.5.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089" namespace="System.Web.UI.WebControls" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1
        {
            height: 22px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>


  <asp:ObjectDataSource ID="RoleDataSource" runat="server" SelectMethod="GetAllRole"
        TypeName="NXEIP.DAO.RoleDAO" EnablePaging="True" 
        SelectCountMethod="GetAllRoleCount">
    </asp:ObjectDataSource>


&nbsp;<table width="100%" cellspacing="20" cellpadding="0" border="0">
        <tbody>
            <tr>
                <td valign="top" class="style1">
                    <table width="100%" cellspacing="0" cellpadding="0" border="0">
                        <tbody>
                            <tr>
                                <td width="17">
                                    <img width="17" height="22" src="../../image/b01.gif">
                                </td>
                                <td background="../../image/b01-1.gif" class="b01">
                                    帳號管理 / 權限管理 /<strong> 角色設定 </strong>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <table width="100%" cellspacing="0" cellpadding="0" border="0">
                        <tbody>
                            <tr>
                                <td width="17">
                                    <img width="17" height="29" src="../../image/b02.gif">
                                </td>
                                <td background="../../image/b02-1.gif" class="a02-15">
                                    角色設定
                                </td>
                                <td background="../../image/b02-1.gif">
                                    <div align="right">
                                        <asp:HyperLink ID="HyperLink1" runat="server" CssClass="thickbox" 
                                            NavigateUrl="350101-1.aspx?modal=true">HyperLink</asp:HyperLink>
                                        <asp:Button ID="Button1" runat="server" CssClass="b-input" Text="新增角色" />
                                        
                                     
                                        
                                        &nbsp;</div>
                                </td>
                                <td width="17">
                                    <img width="17" height="29" src="../../image/b02-2.gif">
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <strong>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <cc1:GridView ID="GridView1" runat="server" DataSourceID="RoleDataSource"
                                    AutoGenerateColumns="False" Width="100%" AllowPaging="True" 
                                    CellPadding="3" CellSpacing="3"
                                    CssClass="tableData" GridLines="None" OnRowCommand="GridView1_RowCommand" DataKeyNames="rol_no"
                                    EnableViewState="False" onrowdatabound="GridView1_RowDataBound">
                                    <Columns>
                                        <asp:BoundField DataField="rol_name" HeaderText="角色名稱" SortExpression="rol_no" />
                                        <asp:BoundField DataField="rol_memo" HeaderText="角色備註" 
                                            SortExpression="rol_memo" />
                                        <asp:BoundField DataField="rol_createuid" HeaderText="修建者" 
                                            SortExpression="rol_createuid" />
                                        <asp:BoundField DataField="rol_createtime" HeaderText="修建時間" SortExpression="rol_createtime" />
                                        <asp:ButtonField ButtonType="Image" ImageUrl="~/image/v05.gif" Text="人員明細" 
                                            HeaderText="人員明細" DataTextField="rol_no" >
                                        <ItemStyle HorizontalAlign="Center" />
                                        </asp:ButtonField>
                                        <asp:ButtonField Text="設定權限" ButtonType="Image" DataTextField="rol_no" 
                                            HeaderText="設定權限" ImageUrl="~/image/alter.gif" >
                                        <ItemStyle HorizontalAlign="Center" />
                                        </asp:ButtonField>
                                        <asp:ButtonField ButtonType="Image" CommandName="modify" HeaderText="修改" ImageUrl="~/image/edit.gif"
                                            Text="按鈕">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:ButtonField>
                                      
                                        <asp:TemplateField HeaderText="刪除">
                                             
                                            <ItemTemplate>
                                                  
                                                <asp:ImageButton ID="ImageButton1" runat="server" 
                                                    CommandArgument="<%# Container.DataItemIndex %>" CommandName="disable" 
                                                    ImageUrl="~/image/delete.gif" 
                                                    onclientclick="confirmMsgbox(this,&quot;OK?&quot;);return false;" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                    </Columns>
                                </cc1:GridView>
                                <div class="pager">
                                    <asp:DataPager ID="DataPager1" runat="server" PagedControlID="GridView1" PageSize="10">
                                        <Fields>
                                            <asp:NextPreviousPagerField ShowNextPageButton="False" />
                                            <asp:NumericPagerField />
                                            <asp:NextPreviousPagerField ShowPreviousPageButton="False" />
                                        </Fields>
                                    </asp:DataPager>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </strong>
                </td>
            </tr>
        </tbody>
        
       
        
        
    </table>








</asp:Content>

