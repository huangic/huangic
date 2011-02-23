<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="300901.aspx.cs" Inherits="_30_300900_300901" %>

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
    <asp:ObjectDataSource ID="DataSource" runat="server" SelectMethod="GetFormByPeoAndKeyword"
        TypeName="NXEIP.DAO.Form01DAO" EnablePaging="True" 
        SelectCountMethod="GetFormByPeoAndKeywordCount" 
        OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:Parameter Name="peouid" Type="Int32" />
            <asp:Parameter Name="keyword" Type="String" />
        </SelectParameters>
          
    </asp:ObjectDataSource>
    <uc1:Navigator ID="Navigator1" runat="server" SysFuncNo="300901" />
    
    
    <div  class="select" >
            <span class="a-letter-2">���W�١G<span class="a-letter-1">
                    <asp:TextBox ID="tb_keyword" runat="server"></asp:TextBox>
                     &nbsp;
           
             
             <asp:Button ID="Button1" runat="server" Text="�j�M" CssClass="b-input" 
                CausesValidation="False" onclick="Button1_Click"
                         />
                </span>
                
                
                </span>
        </div>
    
    
    
    
    <div class="tableDiv">
        <div class="header">
            <div class="h1">
            </div>
            <div class="h2">
                <div class="function">
                    <input type="button" class="thickbox b-input" alt="300901-1.aspx?modal=true&TB_iframe=true&height=400&width=600""
                        value="�s�W���" />
                </div>
            </div>
            <div class="h3">
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                
                <cc1:GridView ID="GridView1" runat="server" DataSourceID="DataSource"
                    AutoGenerateColumns="False" Width="100%" AllowPaging="True"  EmptyDataText="�ثe�L���"
                    CellPadding="3" CellSpacing="3"
                    CssClass="tableData" GridLines="None" OnRowCommand="GridView1_RowCommand" DataKeyNames="f01_no"
                    >
                    <Columns>
                        <asp:BoundField DataField="f01_name" HeaderText="���W��" 
                            SortExpression="f01_name" ItemStyle-Width="200px" />
                        
                       
                        <asp:BoundField DataField="f01_description" HeaderText="��满��" 
                            SortExpression="f01_description" />
                        <asp:TemplateField HeaderText="���A">
                            <ItemStyle Width="50px" />
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# ShowStatus((String)Eval("f01_status")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                         <asp:TemplateField HeaderText="��s���">
                            <ItemStyle Width="100px" />
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# new ChangeObject()._ADtoROC((DateTime)Eval("f01_createtime")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                                              
                        <asp:TemplateField HeaderText="�ק�" ItemStyle-HorizontalAlign="Center">
                            <ItemStyle Width="60px" />
                            <ItemTemplate>
                                <a id="btnShowPopup" runat="server" class="thickbox imageButton edit" title='�ק�'
                                    href='<%# Eval("f01_no", "300901-1.aspx?modal=true&mode=edit&ID={0}&TB_iframe=true&height=300&width=600") %>'>
                                    <span>�ק�</span>
                                </a>
                            </ItemTemplate>
                           
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="������" ItemStyle-HorizontalAlign="Center">
                            <ItemStyle Width="60px" />
                            <ItemTemplate>
                                <a class="imageButton edit" title='������'
                                    href='<%# Eval("f01_no", "300901-2.aspx?modal=true&mode=edit&ID={0}&TB_iframe=true&height=300&width=600") %>'>
                                    <span>������</span>
                                </a>
                            </ItemTemplate>
                            
                        </asp:TemplateField>

                         

                        <asp:TemplateField HeaderText="����M��" ItemStyle-HorizontalAlign="Center">
                            <ItemStyle Width="60px" />
                            <ItemTemplate>
                                <a class="imageButton peruse" title='������'
                                    href='<%# Eval("f01_no", "300901-4.aspx?modal=true&mode=edit&ID={0}&TB_iframe=true&height=300&width=600") %>'>
                                    <span>����M��</span>
                                </a>
                            </ItemTemplate>
                            
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="�ҥ�">
                            <ItemStyle Width="30px" />
                            <ItemTemplate>
                                <asp:Button ID="Button2" runat="server" CommandArgument="<%# Container.DataItemIndex %>" CommandName="enable"  Text="�ҥ�"  Visible='<%#!IsEnable((String)Eval("f01_status")) %>' />
                                <asp:Button ID="Button3" runat="server" CommandArgument="<%# Container.DataItemIndex %>" CommandName="disable"  Text="����" Visible='<%#IsEnable((String)Eval("f01_status")) %>'  />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="�R��">
                            <ItemStyle Width="30px" />
                            <ItemTemplate>
                                <asp:Button ID="Button1" runat="server" CommandArgument="<%# Container.DataItemIndex %>" CommandName="delete" OnClientClick=" return confirm('�T�w�n�R��?')" CssClass="delete"  />
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
                   <div class="pager">
                    <asp:DataPager ID="DataPager1" runat="server"  PagedControlID="GridView1" PageSize="25">
                        <Fields>
                           <NXEIP:GooglePagerField />
                        </Fields>
                    </asp:DataPager>

                </div>
                </div>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
    </div>
</asp:Content>
