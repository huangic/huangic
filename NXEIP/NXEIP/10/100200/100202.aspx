<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="100202.aspx.cs" Inherits="_10_100200_100202" EnableEventValidation="false" ValidateRequest="false" %>

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


            //alert(msg);
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
    
    <asp:ObjectDataSource ID="ObjectDataSource_treat" runat="server" 
        EnablePaging="True" OldValuesParameterFormatString="original_{0}"
        SelectCountMethod="GetTreatDataCount" SelectMethod="GetTreatData" 
        TypeName="NXEIP.DAO._100202DAO">
        <SelectParameters>
            <asp:Parameter Name="status" Type="String" />
            <asp:Parameter Name="tra_peouid" Type="Int32" />
            <asp:Parameter Name="keyword" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
   
   
   
    <uc1:Navigator ID="Navigator1" runat="server" SysFuncNo="100202" />
    
    <div class="select">
            <span class="a-letter-2">執行情況：<asp:DropDownList ID="DropDownList1" 
                runat="server">
                <asp:ListItem Value="1">執行中</asp:ListItem>
                <asp:ListItem Value="2">已完成</asp:ListItem>
                <asp:ListItem Value="3">逾期未完成</asp:ListItem>
            </asp:DropDownList>
&nbsp;關鍵字：<span class="a-letter-1">
                    <asp:TextBox ID="tb_keyword" runat="server"></asp:TextBox>
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
                    <input type="button" class="thickbox b-input" alt="100202-2.aspx?modal=true&TB_iframe=true&height=400&width=700"
                        value="新增待辦" />

                    <asp:Button ID="ShowPost" runat="server"  CssClass="b-input" Text="追蹤交辦事項" />   
                </div>
            </div>
            <div class="h3">
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <cc1:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    CellPadding="3" CellSpacing="3" DataSourceID="ObjectDataSource_treat" EmptyDataText="查無資料"
                    GridLines="None" DataKeyNames="Detail" 
                    onrowcommand="GridView1_RowCommand">
                    <Columns>
                        
                       
                       <asp:TemplateField HeaderText="待辦工作">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("Treat.tre_name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="來源">
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# GetTreatStatus((Int32)Eval("Treat.peo_uid"),(Int32)Eval("Detail.peo_uid")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        
                         <asp:TemplateField HeaderText="執行情況">
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%# Eval("Detail.tde_status") %>'></asp:Label>
                            </ItemTemplate>
                         </asp:TemplateField>

                         <asp:TemplateField HeaderText="進度">
                            <ItemTemplate>
                                <asp:Label ID="Label4" runat="server" Text='<%# Eval("Detail.tde_achieved") %>'></asp:Label>
                            </ItemTemplate>
                         </asp:TemplateField>



                        
                        <asp:TemplateField HeaderText="工作期間">
                            <ItemTemplate>
                                <asp:Label ID="Label5" runat="server" Text='<%# new ChangeObject()._ADtoROC((DateTime)Eval("Treat.tre_sdate")) %>'></asp:Label>
                                ~
                                <asp:Label ID="Label6" runat="server" Text='<%# new ChangeObject()._ADtoROC((DateTime)Eval("Treat.tre_edate")) %>'></asp:Label>
                            </ItemTemplate>


                        </asp:TemplateField>

                          <asp:TemplateField HeaderText="交辦人">
                            <ItemTemplate>
                                <asp:Label ID="Label7" runat="server" Text='<%# new NXEIP.DAO.PeopleDAO().GetPeopleNameByUid((Int32)Eval("Treat.peo_uid")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>






                        <asp:TemplateField HeaderText="執行人">
                            <ItemTemplate>
                                <asp:Label ID="Label8" runat="server" Text='<%# new NXEIP.DAO.PeopleDAO().GetPeopleNameByUid((Int32)Eval("Detail.peo_uid")) %>'></asp:Label>
                               
                            </ItemTemplate>

                        </asp:TemplateField>
                        

                        <asp:TemplateField HeaderText="進度回報">
                            <ItemStyle HorizontalAlign="Center"  Width="5em"/>
                            <ItemTemplate>
                                
                                <asp:HyperLink ID="HyperLink1" runat="server" CssClass="thickbox imageButton edit" NavigateUrl='<%# string.Format("200105-3.aspx?id={0}&modal=true&TB_iframe=true&height=378&width=600",Eval("Treat.tre_no"))%>' Enabled='<%# GetModifyVisible((int)Eval("Treat.peo_uid"))%>'><span>修改</span></asp:HyperLink>
                                
                             </ItemTemplate>

                        </asp:TemplateField>
                        


                        
                    
                       <asp:TemplateField HeaderText="檢視">
                            <ItemStyle HorizontalAlign="Center"  Width="3em"/>
                            <ItemTemplate>
                                
                                <asp:HyperLink ID="HyperLink2" runat="server" CssClass="thickbox imageButton detail" NavigateUrl='<%# string.Format("200105-3.aspx?id={0}&modal=true&TB_iframe=true&height=378&width=600",Eval("Treat.tre_no"))%>' ><span>檢視</span></asp:HyperLink>
                            
                            </ItemTemplate>
                            </asp:TemplateField>


                   <asp:TemplateField HeaderText="刪除">
                            <ItemStyle HorizontalAlign="Center"  Width="3em"/>
                            <ItemTemplate>
                                                         
                                <asp:LinkButton ID="LinkButton1" runat="server" CssClass=" imageButton delete"  CommandName="del" Enabled='<%# GetModifyVisible((int)Eval("Treat.peo_uid"))%>' OnClientClick="return confirm('確定要刪除?')"><span>刪除</span></asp:LinkButton>
                            
                            
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
            <asp:DataPager ID="DataPager1" runat="server" PagedControlID="GridView1" 
                PageSize="10">
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
