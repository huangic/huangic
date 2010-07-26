<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="350202.aspx.cs" Inherits="_35_350200_350202" %>

<%@ Register Assembly="MattBerseth.WebControls" Namespace="MattBerseth.WebControls"
    TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="System.Web.Entity, Version=3.5.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
    Namespace="System.Web.UI.WebControls" TagPrefix="asp" %>
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
        EnablePaging="True" SelectCountMethod="GetAllCount">
        <SelectParameters>
            <asp:Parameter DefaultValue="ptype" Name="type_code" Type="String" />
        </SelectParameters>
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
                                    帳號管理 / 人員管理 /<strong> 人員類別管理 </strong>
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
                                            人員類別管理
                                        </td>
                                        <td background="../../image/b02-1.gif">
                                            <div align="right">
                                                
                                                <input type="button" class="thickbox b-input" alt="350202-1.aspx?modal=true&TB_iframe=true" value="新增人員類別">
                                                
                                            </div>
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
                            
                                <cc1:GridView ID="GridView1" runat="server" DataSourceID="TypesDataSource" AutoGenerateColumns="False"
                                    Width="100%" AllowPaging="True" CellPadding="3" CellSpacing="3" CssClass="tableData"
                                    GridLines="None" OnRowCommand="GridView1_RowCommand" DataKeyNames="typ_no" EnableViewState="False"
                                    EmptyDataText="目前無資料" onrowdatabound="GridView1_RowDataBound">
                                    <Columns>
                                        <asp:BoundField DataField="typ_code" HeaderText="類別代號" SortExpression="typ_code" />
                                        <asp:BoundField DataField="typ_cname" HeaderText="人員類別 " SortExpression="typ_cname" />
                                        <asp:BoundField DataField="typ_createuid" HeaderText="修建者" SortExpression="typ_createuid" />
                                        <asp:BoundField DataField="typ_createtime" HeaderText="修建時間" SortExpression="typ_createtime" />
                                        
                                         <asp:TemplateField HeaderText="修改" ItemStyle-HorizontalAlign="Center">
					                        <ItemTemplate >
                                             <a id="btnShowPopup" runat="server" class="thickbox" 
                                                 title='<%# Eval("typ_cname", "修改{0}") %>' 
                                                 href='<%# Eval("typ_no", "350202-1.aspx?modal=true&mode=edit&ID={0}&TB_iframe=true&height=250&width=600") %>'>
                                                 <asp:Image ImageUrl="~/image/edit.gif" runat="server" />
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
                                
                                 </ContentTemplate>
                    </asp:UpdatePanel>
                                <div class="pager">
                                    <asp:DataPager ID="DataPager1" runat="server" PagedControlID="GridView1" PageSize="10">
                                        <Fields>
                                            <asp:NextPreviousPagerField ShowNextPageButton="False" />
                                            <asp:NumericPagerField />
                                            <asp:NextPreviousPagerField ShowPreviousPageButton="False" />
                                        </Fields>
                                    </asp:DataPager>
                                </div>
                            </strong></td> </tr> </tbody> </table>
                       
    </table>
    
    

    
    
    
</asp:Content>

    