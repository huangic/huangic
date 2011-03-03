<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="100101.aspx.cs" Inherits="_10_100100_100101" EnableEventValidation="false" %>

<%@ Register Src="../../lib/Navigator.ascx" TagName="Navigator" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="MattBerseth.WebControls" Namespace="MattBerseth.WebControls"
    TagPrefix="cc1" %>
<%@ Register src="../../lib/people/PeopleDetail.ascx" tagname="PeopleDetail" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function update(msg) {

            __doPostBack('<%=UpdatePanel1.ClientID%>', '');
            tb_remove();

            if (msg) {
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
    
    <asp:ObjectDataSource ID="ObjectDataSource_d11" runat="server" EnablePaging="True" OldValuesParameterFormatString="original_{0}"
        SelectCountMethod="GetSearchDataCount" SelectMethod="GetSearchData" TypeName="NXEIP.DAO.IpaddressDAO">
        <SelectParameters>
            <asp:Parameter DefaultValue="" Name="ip" Type="String" />
            <asp:Parameter DefaultValue="" Name="name" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <uc1:Navigator ID="Navigator1" runat="server" SysFuncNo="100101" />
    
    <div class="select">
            <span class="a-letter-2">IP：
               
              
               <span class="a-letter-1">
                    <asp:TextBox ID="tb_ip" runat="server"></asp:TextBox>
                   
                </span>
                姓名：<span class="a-letter-1">
                    <asp:TextBox ID="tb_name" runat="server"></asp:TextBox>
                     &nbsp;<asp:Button ID="Button1" runat="server" Text="搜尋" CssClass="b-input" CausesValidation="False"
                        OnClick="Button1_Click" />
                </span>
                
                
                </span>
        </div>
    
    
    
    
    <div class="tableDiv">
        
        <div class="header">
            <div class="h1">
            </div>
            <div class="h2">
                <div class="function">
                    <input type="button" class="thickbox b-input" alt="100101-1.aspx?modal=true&TB_iframe=true&height=400&width=700"
                        value="登錄IP" />
                </div>
            </div>
            <div class="h3">
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <cc1:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    CellPadding="3" CellSpacing="3" DataSourceID="ObjectDataSource_d11" EmptyDataText="查無資料"
                    GridLines="None" DataKeyNames="ipa_no" 
                    onrowcommand="GridView1_RowCommand">
                    <Columns>
                        
                        <asp:TemplateField HeaderText="使用單位">
                            <ItemStyle Width="100px" />
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%#Eval("people.departments.dep_name")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="使用人員">
                             <ItemStyle Width="100px" />
                            <ItemTemplate>
                                <asp:Label ID="Label4" runat="server" Text='<%# Eval("people.peo_name") %>'></asp:Label>
                                
                            </ItemTemplate>



                        </asp:TemplateField>
                        
                        
                        
                        
                        
                        <asp:BoundField DataField="ipa_name" HeaderText="電腦名稱"  ItemStyle-Width="100px"
                            SortExpression="ipa_name" />
                        
                         <asp:BoundField DataField="ipa_start" HeaderText="IP位置"  ItemStyle-Width="100px"
                            SortExpression="ipa_start" />
                        
                        
                        <asp:TemplateField HeaderText="工作群組">
                             <ItemStyle Width="100px" />
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("ipa_group") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                       

                        <asp:TemplateField HeaderText="用途">
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("ipa_memo") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        
                       <asp:TemplateField HeaderText="修改">
                            
                            <ItemStyle Width="50px" HorizontalAlign="Center" />
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink2" runat="server" CssClass="thickbox imageButton edit" NavigateUrl='<%# string.Format("100101-1.aspx?mode=edit&id={0}&modal=true&TB_iframe=true&height=378&width=600",Eval("ipa_no"))%>' Enabled='<%# GetModifyVisible((int)Eval("peo_uid"))%>'><span>修改</span></asp:HyperLink>
                                                           
                            </ItemTemplate>
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
