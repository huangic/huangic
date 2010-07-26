<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="350203.aspx.cs" Inherits="_35_350200_350203" %>

<%@ Register Assembly="MattBerseth.WebControls" Namespace="MattBerseth.WebControls"
    TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register Src="../../lib/messagebox/OkMessagebox.ascx" TagName="OkMessagebox"
    TagPrefix="uc2" %>
<%@ Register src="../../lib/messagebox/ConfirmMessagebox.ascx" tagname="ConfirmMessagebox" tagprefix="uc3" %>
<%@ Register src="../../lib/SWFUpload/UC_SWFUpload.ascx" tagname="UC_SWFUpload" tagprefix="uc4" %>
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
    <asp:ObjectDataSource ID="DepartmentsDataSource" runat="server" SelectMethod="GetAll"
        TypeName="NXEIP.DAO.DepartmentsDAO" EnablePaging="True" SelectCountMethod="GetAllCount">
    </asp:ObjectDataSource>
    
    
    <table width="100%" cellspacing="20" cellpadding="0" border="0">
        <tbody>
            <tr>
                <td valign="top" height="22">
                    <table width="100%" cellspacing="0" cellpadding="0" border="0">
                        <tbody>
                            <tr>
                                <td width="17">
                                    <img width="17" height="22" src="../../image/b01.gif">
                                </td>
                                <td background="../../image/b01-1.gif" class="b01">
                                    帳號管理 / 人員管理 /<strong> 部門管理 </strong>
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
                                    部門管理
                                </td>
                                <td background="../../image/b02-1.gif">
                                    <div align="right">
                                        <input type="button" class="thickbox b-input" alt="350203-1.aspx?modal=true&TB_iframe=true" value="新增部門資料">
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
                                <cc1:GridView ID="GridView1" runat="server" DataSourceID="DepartmentsDataSource"
                                    AutoGenerateColumns="False" Width="100%" AllowPaging="True" CellPadding="3" CellSpacing="3"
                                    CssClass="tableData" GridLines="None" OnRowCommand="GridView1_RowCommand" DataKeyNames="dep_no"
                                    EnableViewState="False">
                                    <Columns>
                                        <asp:BoundField DataField="dep_code" HeaderText="部門代碼" SortExpression="dep_no" />
                                        <asp:BoundField DataField="dep_name" HeaderText="部門中文名稱" SortExpression="dep_name" />
                                        <asp:BoundField DataField="dep_ename" HeaderText="部門英文名稱" SortExpression="dep_ename" />
                                        <asp:BoundField DataField="dep_tel" HeaderText="電話" SortExpression="dep_tel" />
                                        <asp:BoundField DataField="dep_addr" HeaderText="地址" SortExpression="dep_addr" />
                                        <asp:BoundField DataField="dep_fax" HeaderText="傳真" SortExpression="dep_fax" />
                                        <asp:BoundField DataField="dep_order" HeaderText="順序" SortExpression="dep_order" />
                                        
                                        <asp:TemplateField HeaderText="修改" ItemStyle-HorizontalAlign="Center">
					                        <ItemTemplate >
                                             <a id="btnShowPopup" runat="server" class="thickbox" 
                                                 title='<%# Eval("dep_name", "修改{0}") %>' 
                                                 href='<%# Eval("dep_no", "350203-1.aspx?modal=true&mode=edit&ID={0}&TB_iframe=true&height=450&width=600") %>'>
                                                 <asp:Image ID="Image1" ImageUrl="~/image/edit.gif" runat="server" />
                                             </a>                
					                        </ItemTemplate>
				                         </asp:TemplateField>
				                         
				                         <asp:TemplateField HeaderText="刪除">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ImageButton1" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                                    CommandName="disable" ImageUrl="~/image/delete.gif" OnClientClick=" return confirm('確定要刪除?')" />
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
