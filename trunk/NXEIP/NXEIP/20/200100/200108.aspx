<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="200108.aspx.cs" Inherits="_20_200100_200108" %>

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
    <uc1:Navigator ID="Navigator1" runat="server" SysFuncNo="200108" />
    <div class="tableDiv">
        <div class="header">
            <div class="h1">
            </div>
            <div class="h2">
                
            </div>
            <div class="h3">
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click"></asp:LinkButton>
                <cc1:GridView ID="GridView1" runat="server" DataSourceID="DataSource"
                    AutoGenerateColumns="False" Width="100%" AllowPaging="True"  EmptyDataText="�ثe�L���"
                    CellPadding="3" CellSpacing="3"
                    CssClass="tableData" GridLines="None" DataKeyNames="f01_no"
                    >
                    <Columns>
                        <asp:BoundField DataField="f01_name" HeaderText="���W��" 
                            SortExpression="f01_name" ItemStyle-Width="200px" />
                        
                       
                        <asp:BoundField DataField="f01_description" HeaderText="��满��" 
                            SortExpression="f01_description" />
                                              
                         <asp:TemplateField HeaderText="��s���">
                            <ItemStyle Width="100px" />
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# new ChangeObject()._ADtoROC((DateTime)Eval("f01_createtime")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="�ӿ���">
                            <ItemStyle Width="100px" />
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("people.departments.dep_name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                         <asp:TemplateField HeaderText="�ӿ�H">
                            <ItemStyle Width="100px" />
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("people.peo_name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                                              
                        <asp:TemplateField HeaderText="����" ItemStyle-HorizontalAlign="Center">
                            <ItemStyle Width="70px" />
                            <ItemTemplate>
                                <a id="btnShowPopup" runat="server" class="imageButton edit" title='�ק�'
                                    href='<%# Eval("f01_no", "200108-1.aspx?ID={0}") %>'>
                                    <span>����</span>
                                </a>
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
