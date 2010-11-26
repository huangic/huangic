<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="300901-2.aspx.cs" Inherits="_30_300900_300901_2" %>

<%@ Register Assembly="MattBerseth.WebControls" Namespace="MattBerseth.WebControls"
    TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
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
    <asp:ObjectDataSource ID="DataSource" runat="server" SelectMethod="GetAll"
        TypeName="NXEIP.DAO.Form01DAO" EnablePaging="True" SelectCountMethod="GetAllCount">
    </asp:ObjectDataSource>
    <uc1:Navigator ID="Navigator1" runat="server" SysFuncNo="300901" />
    <div class="tableDiv">
        <div class="header">
            <div class="h1">
            </div>
            <div class="h2">
                <div class="function">
                    <input type="button" class="thickbox b-input" alt="300901-1.aspx?modal=true&TB_iframe=true&height=600&width=600""
                        value="新增表單" />
                </div>
            </div>
            <div class="h3">
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click"></asp:LinkButton>
                <cc1:GridView ID="GridView1" runat="server" DataSourceID="DataSource"
                    AutoGenerateColumns="False" Width="100%" AllowPaging="True"  EmptyDataText="目前無資料"
                    CellPadding="3" CellSpacing="3"
                    CssClass="tableData" GridLines="None" OnRowCommand="GridView1_RowCommand" DataKeyNames="f01_no"
                    >
                    <Columns>
                        <asp:BoundField DataField="f01_name" HeaderText="表單名稱" 
                            SortExpression="f01_name" ItemStyle-Width="200px" />
                        
                       
                        <asp:BoundField DataField="f01_description" HeaderText="表單說明" 
                            SortExpression="f01_description" />
                        <asp:TemplateField HeaderText="狀態">
                            <ItemStyle Width="50px" />
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# new ChangeObject()._ADtoROC((DateTime)Eval("f01_createtime")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                         <asp:TemplateField HeaderText="更新日期">
                            <ItemStyle Width="100px" />
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# new ChangeObject()._ADtoROC((DateTime)Eval("f01_createtime")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                                              
                        <asp:TemplateField HeaderText="修改" ItemStyle-HorizontalAlign="Center">
                            <ItemStyle Width="30px" />
                            <ItemTemplate>
                                <a id="btnShowPopup" runat="server" class="thickbox imageButton edit" title='修改'
                                    href='<%# Eval("f01_no", "300901-1.aspx?modal=true&mode=edit&ID={0}&TB_iframe=true&height=600&width=600") %>'>
                                    <span>修改</span>
                                </a>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="啟用">
                            <ItemStyle Width="30px" />
                            <ItemTemplate>
                                <asp:Button ID="Button2" runat="server" CommandArgument="<%# Container.DataItemIndex %>" CommandName="enable"  Text="啟用"  />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="刪除">
                            <ItemStyle Width="30px" />
                            <ItemTemplate>
                                <asp:Button ID="Button1" runat="server" CommandArgument="<%# Container.DataItemIndex %>" CommandName="disable" OnClientClick=" return confirm('確定要刪除?')" CssClass="delete"  />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                    </Columns>
                </cc1:GridView>
                <div class="footer">
                    <div class="f1">
                    </div>
                    <div class="f2">
                    </div>
                    <div class="f3">
                    </div>
                </div>
                <div class="pager">
                    <asp:DataPager ID="DataPager1" runat="server" PagedControlID="GridView1" PageSize="5">
                        <Fields>
                            <asp:NextPreviousPagerField ShowNextPageButton="False" />
                            <asp:NumericPagerField />
                            <asp:NextPreviousPagerField ShowPreviousPageButton="False" />
                        </Fields>
                    </asp:DataPager>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
