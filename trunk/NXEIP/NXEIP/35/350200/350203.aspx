<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="350203.aspx.cs" Inherits="_35_350200_350203" %>

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
        TypeName="NXEIP.DAO.DepartmentsDAO" EnablePaging="True" SelectCountMethod="GetAllCount">
    </asp:ObjectDataSource>
    <uc1:Navigator ID="Navigator1" runat="server" SysFuncNo="350203" />
    <div class="tableDiv">
        <div class="header">
            <div class="h1">
            </div>
            <div class="h2">
                <div class="function">
                    <input type="button" class="thickbox b-input" alt="350203-1.aspx?modal=true&TB_iframe=true"
                        value="�s�W�������" />
                </div>
            </div>
            <div class="h3">
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <cc1:GridView ID="GridView1" runat="server" DataSourceID="DepartmentsDataSource"
                    AutoGenerateColumns="False" Width="100%" AllowPaging="True" CellPadding="3" CellSpacing="3"
                    CssClass="tableData" GridLines="None" OnRowCommand="GridView1_RowCommand" DataKeyNames="dep_no"
                   >
                    <Columns>
                        <asp:BoundField DataField="dep_code" HeaderText="�����N�X" SortExpression="dep_no" />
                        <asp:BoundField DataField="dep_name" HeaderText="��������W��" SortExpression="dep_name" />
                        <asp:BoundField DataField="dep_ename" HeaderText="�����^��W��" SortExpression="dep_ename" />
                        <asp:BoundField DataField="dep_tel" HeaderText="�q��" SortExpression="dep_tel" />
                        <asp:BoundField DataField="dep_addr" HeaderText="�a�}" SortExpression="dep_addr" />
                        <asp:BoundField DataField="dep_fax" HeaderText="�ǯu" SortExpression="dep_fax" />
                        <asp:BoundField DataField="dep_order" HeaderText="����" SortExpression="dep_order" />
                        <asp:TemplateField HeaderText="�ק�" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <a id="btnShowPopup" runat="server" class="thickbox imageButton edit" title='<%# Eval("dep_name", "�ק�{0}") %>'
                                    href='<%# Eval("dep_no", "350203-1.aspx?modal=true&mode=edit&ID={0}&TB_iframe=true&height=450&width=600") %>'>
                                    <span>�ק�</span>
                                </a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="�R��">
                            <ItemTemplate>
                                <asp:Button ID="Button1" runat="server" CommandArgument="<%# Container.DataItemIndex %>" CommandName="disable" OnClientClick=" return confirm('�T�w�n�R��?')" CssClass="delete"  />
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
                    <asp:DataPager ID="DataPager1" runat="server" PagedControlID="GridView1" PageSize="10">
                        <Fields>
                            <NXEIP:GooglePagerField />
                        </Fields>
                    </asp:DataPager>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
