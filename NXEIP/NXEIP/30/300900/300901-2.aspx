<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="300901-2.aspx.cs" Inherits="_30_300900_300901_2" enableEventValidation="false" %>

<%@ Register Assembly="MattBerseth.WebControls" Namespace="MattBerseth.WebControls"
    TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../../lib/Navigator.ascx" TagName="Navigator" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function update(id,msg) {

            if (id == '1') {
                __doPostBack('<%=UpdatePanel1.ClientID%>', '');
            }

            if (id == '2') {
                __doPostBack('<%=UpdatePanel2.ClientID%>', '');
            }
            
            tb_remove();

            if (msg != undefined) {
                alert(msg);
            }
        }

        function updateStatus(id,msg) {

            if (id == '1') {
                __doPostBack('<%=UpdatePanel1.ClientID%>', '');
            }

            if (id == '2') {
                __doPostBack('<%=UpdatePanel2.ClientID%>', '');
            }

            //tb_remove();

            if (msg != undefined) {
                alert(msg);
            }
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
    <asp:ObjectDataSource ID="DataSource" runat="server" SelectMethod="GetColumnsByFormNO"
        TypeName="NXEIP.DAO.Form01DAO" 
        OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:QueryStringParameter Name="f01_no" QueryStringField="ID" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="DataSourceFooter" runat="server" SelectMethod="GetFooterByFormNO"
        TypeName="NXEIP.DAO.Form01DAO" 
        OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:QueryStringParameter Name="f01_no" QueryStringField="ID" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <uc1:Navigator ID="Navigator1" runat="server" SysFuncNo="300901"  SubFunc="表單欄位"/>
    <div class="tableDiv">
        <div class="header">
            <div class="h1">
            </div>
            <div class="h2">
             <div class="name">表單欄位 </div>
                <div class="function">
                    <input type="button" class="thickbox b-input" alt='<%=String.Format("300901-3.aspx?ID={0}&modal=true&TB_iframe=true&height=400&width=600",Request.QueryString["ID"])%>'
                        value="新增欄位" />
                </div>
            </div>
            <div class="h3">
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
              
                <cc1:GridView ID="GridView1" runat="server" DataSourceID="DataSource"
                    AutoGenerateColumns="False" Width="100%"  EmptyDataText="目前無資料"
                    CellPadding="3" CellSpacing="3"
                    CssClass="tableData" GridLines="None" OnRowCommand="GridView1_RowCommand" DataKeyNames="UID"
                    >
                    <Columns>
                        <asp:BoundField DataField="Name" HeaderText="欄位名稱" 
                            SortExpression="Name" ItemStyle-Width="200px" />
                        
                       
                        <asp:BoundField DataField="Description" HeaderText="欄位說明" 
                            SortExpression="Description" />
                        
                        
                                              
                        <asp:TemplateField HeaderText="修改" ItemStyle-HorizontalAlign="Center">
                            <ItemStyle Width="30px" />
                            <ItemTemplate>
                                <a id="btnShowPopup" runat="server" class="thickbox imageButton edit" title='修改'
                                    href='<%# String.Format("300901-3.aspx?modal=true&mode=edit&ID={0}&UID={1}&TB_iframe=true&height=600&width=600",Request.QueryString["ID"],Eval("UID")) %>'>
                                    <span>修改</span>
                                </a>
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
              </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <br />
    <div class="tableDiv">
        <div class="header">
            <div class="h1">
            </div>
            <div class="h2">
                <div class="name">表尾欄位</div>
                <div class="function">
                    <input type="button" class="thickbox b-input" alt='<%=String.Format("300901-7.aspx?ID={0}&modal=true&TB_iframe=true&height=400&width=600",Request.QueryString["ID"])%>'
                        value="新增欄位" />
                </div>
            </div>
            <div class="h3">
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
               
                <cc1:GridView ID="GridView2" runat="server" DataSourceID="DataSourceFooter"
                    AutoGenerateColumns="False" Width="100%"  EmptyDataText="目前無資料"
                    CellPadding="3" CellSpacing="3"
                    CssClass="tableData" GridLines="None" OnRowCommand="GridView2_RowCommand" DataKeyNames="UID"
                    >
                    <Columns>
                        <asp:BoundField DataField="Name" HeaderText="欄位名稱" 
                            SortExpression="Name" ItemStyle-Width="200px" />
                        
                       
                        <asp:BoundField DataField="Description" HeaderText="欄位說明" 
                            SortExpression="Description" />
                        
                        
                                              
                        <asp:TemplateField HeaderText="修改" ItemStyle-HorizontalAlign="Center">
                            <ItemStyle Width="30px" />
                            <ItemTemplate>
                                <a id="btnShowPopup" runat="server" class="thickbox imageButton edit" title='修改'
                                    href='<%# String.Format("300901-7.aspx?modal=true&mode=edit&ID={0}&UID={1}&TB_iframe=true&height=600&width=600",Request.QueryString["ID"],Eval("UID")) %>'>
                                    <span>修改</span>
                                </a>
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
              </ContentTemplate>
        </asp:UpdatePanel>
    </div>

</asp:Content>
