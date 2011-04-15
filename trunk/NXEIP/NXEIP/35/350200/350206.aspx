<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="350206.aspx.cs" Inherits="_35_350200_350206"  EnableEventValidation="false"   %> 

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
    <asp:ObjectDataSource ID="DataSource1" runat="server" SelectMethod="GetManager"
        TypeName="NXEIP.DAO.ManagerDAO" EnablePaging="True" SelectCountMethod="GetManagerCount">
    </asp:ObjectDataSource>
    <uc1:Navigator ID="Navigator1" runat="server" SysFuncNo="350206" />
    <div class="tableDiv" style="width:500px">
        <div class="header">
            <div class="h1">
            </div>
            <div class="h2">
                <div class="function">
                    <input type="button" class="thickbox b-input" alt="350206-1.aspx?modal=true&TB_iframe=true"
                        value="新增管理人員" />
                </div>
            </div>
            <div class="h3">
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <cc1:GridView ID="GridView1" runat="server" DataSourceID="DataSource1"
                    AutoGenerateColumns="False" Width="100%" AllowPaging="True" CellPadding="3" CellSpacing="3"
                    CssClass="tableData" GridLines="None" OnRowCommand="GridView1_RowCommand" DataKeyNames="dep_no,man_no"
                   >
                    <Columns>
                        
                         <asp:TemplateField HeaderText="管理單位">
                            <ItemTemplate>

                           <asp:Label ID="Label1" runat="server" Text='<%#  Eval("departments.dep_name") %>'></asp:Label></li>
                    
                           </ItemTemplate>
                          </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="姓名">
                            <ItemTemplate>

                           <asp:Label ID="Label2" runat="server" Text='<%#  Eval("people.peo_name") %>'></asp:Label></li>
                    
                           </ItemTemplate>
                          </asp:TemplateField>


                        <asp:TemplateField HeaderText="管理類別">
                            <ItemTemplate>

                           <asp:Label ID="Label3" runat="server" Text='<%#  ((String)Eval("man_type")=="1")?"單位管理":"總管理" %>'></asp:Label></li>
                    
                           </ItemTemplate>
                          </asp:TemplateField>

                       
                        <asp:TemplateField HeaderText="修改" ItemStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <a id="btnShowPopup" runat="server" class="thickbox imageButton edit" title="修改"
                                    href='<%# String.Format("350206-1.aspx?modal=true&mode=edit&dep_no={0}&man_no={1}&TB_iframe=true&height=450&width=600",Eval("dep_no"),Eval("man_no")) %>'>
                                    <span>修改</span>
                                </a>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="刪除">
                            <ItemTemplate>
                                <asp:Button ID="Button1" runat="server" CommandName="disable" OnClientClick=" return confirm('確定要刪除?')" CssClass="delete"  />
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
                    <asp:DataPager ID="DataPager1" runat="server" PagedControlID="GridView1" PageSize="25">
                        <Fields>
                            <NXEIP:GooglePagerField />
                        </Fields>
                    </asp:DataPager>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
