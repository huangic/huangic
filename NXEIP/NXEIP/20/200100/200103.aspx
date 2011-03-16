<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="200103.aspx.cs" Inherits="_20_200100_200103" EnableEventValidation="false" %>

<%@ Register Src="../../lib/Navigator.ascx" TagName="Navigator" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="MattBerseth.WebControls" Namespace="MattBerseth.WebControls"
    TagPrefix="cc1" %>
<%@ Register Src="../../lib/people/PeopleDetail.ascx" TagName="PeopleDetail" TagPrefix="uc2" %>
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
    <asp:ObjectDataSource ID="ObjectDataSource_people" runat="server" EnablePaging="True"
        OldValuesParameterFormatString="original_{0}" SelectCountMethod="GetPeopleOfficialsCount"
        SelectMethod="GetPeopleOfficials" TypeName="NXEIP.DAO.PeopleDAO">
        <SelectParameters>
            <asp:Parameter Name="keyword" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>


    <asp:ObjectDataSource ID="ObjectDataSource_dep" runat="server" EnablePaging="True"
        OldValuesParameterFormatString="original_{0}" SelectCountMethod="GetOfficialsCount"
        SelectMethod="GetOfficials" TypeName="NXEIP.DAO.OfficialsDAO">
        <SelectParameters>
            <asp:Parameter Name="off_type" Type="String" />
            <asp:Parameter Name="keyword" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>


    <asp:ObjectDataSource ID="ObjectDataSource_sys" runat="server" 
        OldValuesParameterFormatString="original_{0}" SelectMethod="GetS06FromSufNO" 
        TypeName="NXEIP.DAO.Sys06DAO">
        <SelectParameters>
            <asp:Parameter DefaultValue="200103" Name="suf_no" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>


    <uc1:Navigator ID="Navigator1" runat="server" SysFuncNo="200103" />
   
   
   
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    

  
     <div  style=" text-align:right">  
       
            
            <asp:Button ID="btn_dep" CssClass="b-input2" runat="server" Text="部門公務電話" 
                onclick="btn_dep_Click" />
            
            <asp:Button ID="btn_personal" CssClass="a-input" runat="server" Text="個人公務電話" 
                onclick="btn_personal_Click" />
       
      
            
        
    </div>


    <div class="select">
        <asp:Panel ID="Panel_dep" runat="server" Wrap="False">
            <span class="a-letter-2">所屬單位 <span class="a-letter-1">
                <asp:DropDownList ID="ddl_unit" runat="server" 
                DataSourceID="ObjectDataSource_sys" DataTextField="s06_name" 
                DataValueField="s06_no" AppendDataBoundItems="True">
                    <asp:ListItem Value="">全部</asp:ListItem>
                </asp:DropDownList>
            </span>檔名：<span class="a-letter-1">
                <asp:TextBox ID="tb_file" runat="server"></asp:TextBox>
                &nbsp;<asp:Button ID="btn_dep_search" runat="server" Text="搜尋" 
                CssClass="b-input" CausesValidation="False" onclick="btn_dep_search_Click"
                     />
            </span></span>
        </asp:Panel>
       
        <asp:Panel ID="Panel_personal" runat="server" Wrap="False">
            <span class="a-letter-2">人員 <span class="a-letter-1">
                <asp:TextBox ID="tb_people" runat="server"></asp:TextBox>
            </span>
             &nbsp;<asp:Button ID="btn_peo_search" runat="server" Text="搜尋" 
                CssClass="b-input" CausesValidation="False" onclick="btn_peo_search_Click"
                    />
            </span>
        </asp:Panel>
      </div>
    <asp:Panel ID="Panel_dep_grid" runat="server">
        <div class="tableDiv">
            <div class="header">
                <div class="h1">
                </div>
                <div class="h2">
                </div>
                <div class="h3">
                </div>
            </div>
          
                    <cc1:GridView ID="GridView_dep" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        CellPadding="3" CellSpacing="3" 
                DataSourceID="ObjectDataSource_dep" EmptyDataText="查無資料"
                        GridLines="None"  DataKeyNames="off_no" 
                onrowcommand="GridView_dep_RowCommand">
                        <Columns>
                            
                            <asp:TemplateField HeaderText="所屬單位">
                               <HeaderStyle Width="80px" />
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# GetSys06Name(Eval("off_type")) %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>



                            <asp:TemplateField HeaderText="上傳單位">
                               <HeaderStyle Width="80px" />
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%#Eval("off_depname") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>


                           
                            <asp:TemplateField HeaderText="上傳人員">
                               <HeaderStyle Width="100px" />
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Text='<%# new NXEIP.DAO.PeopleDAO().GetPeopleNameByUid((Int32)Eval("off_peouid")) %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            
                             <asp:TemplateField HeaderText="上傳電話">
                                <ItemTemplate>
                                    <asp:Label ID="Label4" runat="server" Text='<%# Eval("off_tel") %>'></asp:Label>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>
                             <asp:TemplateField HeaderText="備註">
                                <ItemTemplate>
                                    <asp:Label ID="Label5" runat="server" Text='<%# Eval("off_memo") %>'></asp:Label>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="檔案">
                                <ItemTemplate>
                                   <a id="btnShowPopup" runat="server" class="imageButton download" title='<%# Eval("off_name", "下載{0}") %>'
                                    href='<%# Eval("off_no", "200103-1.ashx?ID={0}") %>'>
                                    <span>下載</span>
                                </a>
                                    <asp:Label ID="Label6" runat="server" Text='<%# Eval("off_name") %>'></asp:Label>
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
                        <asp:DataPager ID="DataPager2" runat="server" PagedControlID="GridView_dep" PageSize="25">
                            <Fields>
                                <NXEIP:GooglePagerField />
                            </Fields>
                        </asp:DataPager>
                    </div>
                
        </div>
    </asp:Panel>
    <asp:Panel ID="Panel_personal_grid" runat="server">
        <div class="tableDiv">
            <div class="header">
                <div class="h1">
                </div>
                <div class="h2">
                </div>
                <div class="h3">
                </div>
            </div>
           
                    <cc1:GridView ID="GridView_peo" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        CellPadding="3" CellSpacing="3" DataSourceID="ObjectDataSource_people" EmptyDataText="查無資料"
                        GridLines="None"  DataKeyNames="peo_uid"
                        >
                        <Columns>
                          
                            <asp:TemplateField HeaderText="所屬單位">
                                <HeaderStyle Width="80px" />
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# GetDepartmentName((Int32)Eval("dep_no")) %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                                                     
                            
                            <asp:TemplateField HeaderText="人員名稱">
                                <HeaderStyle Width="100px" />
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("peo_name") %>'></asp:Label>
                                   
                                </ItemTemplate>
                            </asp:TemplateField>

                              <asp:TemplateField HeaderText="電話">
                               <HeaderStyle Width="80px" />
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("peo_tel") %>'></asp:Label>
                                   
                                </ItemTemplate>
                            </asp:TemplateField>

                              <asp:TemplateField HeaderText="行動電話">
                                <HeaderStyle Width="80px" />
                                <ItemTemplate>
                                    <asp:Label ID="Label4" runat="server" Text='<%# Eval("peo_cellphone") %>'></asp:Label>
                                   
                                </ItemTemplate>
                            </asp:TemplateField>

                              <asp:TemplateField HeaderText="E-Mail">
                                <HeaderStyle Width="150px" />
                                <ItemTemplate>
                                    <asp:Label ID="Label4" runat="server" Text='<%# Eval("peo_email") %>'></asp:Label>
                                   
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
                        <asp:DataPager ID="DataPager1" runat="server" PagedControlID="GridView_peo" PageSize="25">
                            <Fields>
                                <NXEIP:GooglePagerField />
                            </Fields>
                        </asp:DataPager>
                    </div>
                
        </div>
    </asp:Panel>

        </ContentTemplate>


    </asp:UpdatePanel>

</asp:Content>
