<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="300502.aspx.cs" Inherits="_30_300500_300502" %>

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
    <asp:ObjectDataSource ID="DepartmentsDataSource" runat="server" SelectMethod="GetAll"
        TypeName="NXEIP.DAO.FloorsDAO" EnablePaging="True" SelectCountMethod="GetAllCount">
    </asp:ObjectDataSource>
    <uc1:Navigator ID="Navigator1" runat="server" SysFuncNo="300502" />
    <div class="tableDiv">
        <div class="header">
            <div class="h1">
            </div>
            <div class="h2">
                <div class="function">
                    <input type="button" class="thickbox b-input" alt="300502-1.aspx?modal=true&TB_iframe=true"
                        value="新增樓層資料" />
                </div>
            </div>
            <div class="h3">
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click"></asp:LinkButton>
                <cc1:GridView ID="GridView1" runat="server" DataSourceID="DepartmentsDataSource"
                    AutoGenerateColumns="False" Width="100%" AllowPaging="True" 
                    CellPadding="3" CellSpacing="3"
                    CssClass="tableData" GridLines="None" OnRowCommand="GridView1_RowCommand" DataKeyNames="flo_no"
                    >
                    <Columns>
                        <asp:TemplateField HeaderText="所在地">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%#Eval("spot.spo_name") %>'>'></asp:Label>
                            </ItemTemplate>
                        
                        </asp:TemplateField>
                        <asp:BoundField DataField="flo_level" HeaderText="樓層" 
                            SortExpression="flo_level" />
                        <asp:BoundField DataField="flo_name" HeaderText="單位中文名稱" 
                            SortExpression="flo_name" />
                        <asp:BoundField DataField="flo_ename" HeaderText="單位英文名稱" 
                            SortExpression="flo_ename" />
                        <asp:BoundField DataField="flo_order" HeaderText="順序" 
                            SortExpression="flo_order" />
                        <asp:TemplateField HeaderText="修改" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <a id="btnShowPopup" runat="server" class="thickbox imageButton edit" title='<%# Eval("flo_name", "修改{0}") %>'
                                    href='<%# Eval("flo_no", "300502-1.aspx?modal=true&mode=edit&ID={0}&TB_iframe=true&height=450&width=600") %>'>
                                    <span>修改</span>
                                </a>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="刪除">
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
                            <NXEIP:GooglePagerField />
                        </Fields>
                    </asp:DataPager>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
