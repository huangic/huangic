<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="100106.aspx.cs" Inherits="_10_100100_100106" EnableEventValidation="false" %>

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
        SelectCountMethod="GetUserApplyEmailCount" SelectMethod="GetUserApplyEmail" TypeName="NXEIP.DAO.EmailDAO">
        <SelectParameters>
            <asp:Parameter DefaultValue="" Name="peo_uid" Type="Int32" />
            
        </SelectParameters>
    </asp:ObjectDataSource>
    <uc1:Navigator ID="Navigator1" runat="server" SysFuncNo="100106" />
    
    
    
    
    
    
    <div class="tableDiv">
        
        <div class="header">
            <div class="h1">
            </div>
            <div class="h2">
                <div class="function">
                    <input type="button" class="thickbox b-input" alt="100106-1.aspx?modal=true&TB_iframe=true&height=400&width=700"
                        value="申請電子郵件" />
                </div>
            </div>
            <div class="h3">
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <cc1:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    CellPadding="3" CellSpacing="3" DataSourceID="ObjectDataSource_d11" EmptyDataText="查無資料"
                    GridLines="None" DataKeyNames="ema_no" 
                    >
                    <Columns>
                        
                                                    
                        
                        
                       
                        
                         
                        
                        
                        <asp:TemplateField HeaderText="申請日期">
                             <ItemStyle Width="100px" />
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# GetROCDT((DateTime?)Eval("ema_apply")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                       

                        <asp:TemplateField HeaderText="電子郵件">
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("ema_mail") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        

                        <asp:TemplateField HeaderText="審核日期">
                             <ItemStyle Width="100px" />
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# GetROCDT((DateTime?)Eval("ema_checkdate")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="審核狀態">
                             <ItemStyle Width="100px" />
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# GetStatus((String)Eval("ema_status")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                         <asp:TemplateField HeaderText="審核人員">
                             <ItemStyle Width="100px" />
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# GetPeoName((Int32?)Eval("ema_check")) %>'></asp:Label>
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
