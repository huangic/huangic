<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="200107.aspx.cs" Inherits="_20_200100_200107" EnableEventValidation="false" %>

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

        jQuery(document).ready(function () {
            jQuery('.show').click(function () {
                jQuery('.show').removeClass("b-input2").addClass("b-input");
                jQuery(this).removeClass("b-input").addClass("b-input2");
            });
        });



    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    
    <asp:ObjectDataSource ID="ObjectDataSource3" runat="server" EnablePaging="True" OldValuesParameterFormatString="original_{0}"
        SelectCountMethod="GetSearchDataCount" SelectMethod="GetSearchData" TypeName="NXEIP.DAO.Doc09DAO">
        <SelectParameters>
            <asp:Parameter DefaultValue="" Name="dep_no" Type="Int32" />
            <asp:Parameter DefaultValue="" Name="cat_no" Type="Int32" />
            <asp:Parameter DefaultValue="" Name="file" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjectDataSource_CAT" runat="server" 
        OldValuesParameterFormatString="original_{0}" SelectMethod="GetS06FromSufNO" 
        TypeName="NXEIP.DAO.Sys06DAO">
        <SelectParameters>
            <asp:Parameter DefaultValue="200107" Name="suf_no" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    
    <asp:ObjectDataSource ID="ObjectDataSource_Child" runat="server" 
        OldValuesParameterFormatString="original_{0}" 
        SelectMethod="GetS06FromParentS06" TypeName="NXEIP.DAO.Sys06DAO">
        <SelectParameters>
            <asp:ControlParameter ControlID="hidden_cat" Name="s06_no" 
                PropertyName="Value" Type="Int32" DefaultValue="" />
        </SelectParameters>
    </asp:ObjectDataSource>
    
    <asp:ObjectDataSource ID="ObjectDataSource_mydata" runat="server"  EnablePaging="true"
        OldValuesParameterFormatString="original_{0}" SelectMethod="GetSearchMyData" 
        TypeName="NXEIP.DAO.Doc09DAO" SelectCountMethod="GetSearchMyDataCount">
        <SelectParameters>
            <asp:Parameter Name="peo_uid" Type="Int32" />
            <asp:Parameter Name="cat_no" Type="Int32" />
            <asp:Parameter Name="file" Type="String" />
            <asp:Parameter Name="status" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    
    <uc1:Navigator ID="Navigator1" runat="server" SysFuncNo="200107" />
    
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>

        <asp:HiddenField ID="hidden_cat" runat="server" />
        <asp:HiddenField ID="hidden_childcat" runat="server" />
         <asp:HiddenField ID="hidden_depno" runat="server" />
          <asp:HiddenField ID="hidden_show_myfile" runat="server"/>
           <div class="select-1">

               <asp:ListView ID="lv_cat" runat="server" DataSourceID="ObjectDataSource_CAT"  
                   DataKeyNames="s06_no" onitemcommand="lv_cat_ItemCommand"
                   >
                        <ItemTemplate>
                            <span><asp:LinkButton ID="lb_cat" runat="server"  CssClass='<%# hidden_cat.Value.Equals( Eval("s06_no").ToString()) ? "a-letter-s1":""  %>'  CommandName="click_cat"><%# Eval("s06_name") %></asp:LinkButton></span>
                                      
                        
                        </ItemTemplate>
  
                       
               </asp:ListView>
           </div>

           <div id="childDiv" class="select-1" runat="server">

                   <asp:ListView ID="lv_child" runat="server" 
                       DataSourceID="ObjectDataSource_Child" DataKeyNames="s06_no" 
                       onitemcommand="lv_child_ItemCommand" >
                        <ItemTemplate>
                        <span><asp:LinkButton ID="lb_childcat" runat="server"  Visible='<%# !(hidden_childcat.Value.Equals(Eval("s06_no").ToString())) %>'  CommandName="click_childcat"><%# Eval("s06_name") %></asp:LinkButton></span>
                            <asp:Label ID="Label7" runat="server" CssClass="a-letter-s1"  Visible='<%# hidden_childcat.Value.Equals(Eval("s06_no").ToString()) %>' Text='<%# Eval("s06_name") %>'></asp:Label>
                        </ItemTemplate>
             
                      
                   </asp:ListView>
           </div>
        
         <div  class="select" >
            <span class="a-letter-2">檔名：<span class="a-letter-1">
                    <asp:TextBox ID="tb_file" runat="server"></asp:TextBox>
                     &nbsp;<asp:Label ID="lb_status" runat="server" Text="狀態:"></asp:Label>
             <asp:DropDownList ID="DropDownList1" runat="server">
                 <asp:ListItem Value="">全部</asp:ListItem>
                 <asp:ListItem Value="1">通過</asp:ListItem>
                 <asp:ListItem Value="2">未通過</asp:ListItem>
                 <asp:ListItem Value="3">送審中</asp:ListItem>
             </asp:DropDownList>
             <asp:Button ID="Button1" runat="server" Text="搜尋" CssClass="b-input" CausesValidation="False"
                        OnClick="Button1_Click" />
                </span>
                
                
                </span>
        </div>
        
        </ContentTemplate>
    </asp:UpdatePanel>
 
    
    
   
   
    
    
    
    <div class="tableDiv">
        
        <div class="header">
            <div class="h1">
            </div>
            <div class="h2">
                <div class="function">
                    <asp:Button ID="btn_all" runat="server" Text="全府檔案" CssClass="show b-input2" 
                        onclick="btn_all_Click" />
                    <asp:Button ID="btn_dep" runat="server" Text="單位檔案" CssClass="show b-input" 
                        onclick="btn_dep_Click" />
                        <asp:Button ID="btn_my" runat="server" Text="我的檔案" CssClass="show b-input" onclick="btn_my_Click" 
                         />
                    <input type="button" class="thickbox b-input" alt="200107-2.aspx?modal=true&TB_iframe=true&height=378&width=600"
                        value="新增檔案" />
                </div>
            </div>
            <div class="h3">
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
            <ContentTemplate> 
                <cc1:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    CellPadding="3" CellSpacing="3" DataSourceID="ObjectDataSource3" EmptyDataText="查無資料"
                    GridLines="None" OnRowDataBound="GridView1_RowDataBound" 
                    onrowcommand="GridView1_RowCommand" 
                    DataKeyNames="d09_no">
                    <Columns>
                        <asp:TemplateField HeaderText="檔案類別">
                            <HeaderStyle Width="80px" />
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# GetCatName((Int32)Eval("s06_no")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="檔案類別">
                            <HeaderStyle Width="80px" />
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# GetCatChildName((Int32)Eval("s06_no")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:BoundField DataField="d09_note" HeaderText="使用說明" />
                       
                       
                        <asp:TemplateField HeaderText="上傳日期">
                            <HeaderStyle Width="80px" />
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%# new ChangeObject()._ADtoROC((DateTime)Eval("d09_date")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>


                         <asp:TemplateField HeaderText="上傳單位">
                          <HeaderStyle Width="80px" />
                            <ItemTemplate>
                                <asp:Label ID="Label4" runat="server" Text='<%# GetDepartmentName((Int32)Eval("d09_depno")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                     
                         <asp:TemplateField HeaderText="上傳人員">
                            <HeaderStyle Width="100px" />
                            <ItemTemplate>
                                <asp:Label ID="Label5" runat="server" Text='<%# new NXEIP.DAO.PeopleDAO().GetPeopleNameByUid((Int32)Eval("d09_peouid")) %>'></asp:Label>

                               

                                <uc2:PeopleDetail ID="PeopleDetail1" runat="server" peo_uid='<%# Eval("d09_peouid") %>'/>

                               

                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="審核人員&lt;br/&gt;退件原因" >
                          
                            
                            
                            <ItemTemplate>
                                <asp:Label ID="Label8" runat="server" Text='<%# GetPeopleName((Int32?)Eval("d09_checkuid")) %>'></asp:Label>
                                <uc2:PeopleDetail ID="PeopleDetail2" runat="server" peo_uid='<%# Eval("d09_checkuid") %>'/>

                                <asp:Label ID="Label9" runat="server" Text='<%# GetROCDate((DateTime?)Eval("d09_checkdate")) %>'></asp:Label>
                                
                                <br />
                                <asp:Label ID="Label6" runat="server" Text='<%# Eval("d09_reason") %>'></asp:Label>
                            
                            
                            
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="狀態">
                             <HeaderStyle Width="50px" />
                            <ItemTemplate>
                                <asp:Label ID="Label7" runat="server" Text='<%# GetStatus((String)Eval("d09_status")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        



                        <asp:TemplateField HeaderText="附件">
                            <ItemTemplate>
                                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataSource2"
                                    GridLines="None" ShowHeader="False" DataKeyNames="d09_no">
                                    <Columns>
                                        <asp:TemplateField ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="HyperLink1" runat="server" CssClass="download imageButton" Target="_blank"
                                                    NavigateUrl='<%#String.Format("200107-1.ashx?d09={0}&d10={1}",Eval("d09_no"),Eval("d10_no"))  %>'><span>下載</span></asp:HyperLink>
                                                <asp:Label ID="Label5" runat="server" Text='<%# String.Format("{0} (下載次數:{1})", Eval("d10_file"),Eval("d10_count")) %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" OldValuesParameterFormatString="original_{0}"
                                    SelectMethod="GetAllWithDoc09No" TypeName="NXEIP.DAO.Doc10DAO">
                                    <SelectParameters>
                                        <asp:Parameter Name="doc09_no" Type="Int32" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                            </ItemTemplate>
                            <HeaderStyle Width="300px" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderStyle Width="60px" />
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink2" runat="server" CssClass="thickbox imageButton edit" NavigateUrl='<%# string.Format("200107-3.aspx?id={0}&modal=true&TB_iframe=true&height=378&width=600",Eval("d09_no"))%>'  Visible='<%# GetModifyVisible((int)Eval("d09_peouid"))%>'><span>修改</span></asp:HyperLink>
                                <asp:LinkButton ID="LinkButton1" runat="server" CssClass=" imageButton delete" 
                                    Visible='<%# GetModifyVisible((int)Eval("d09_peouid"))%>' 
                                    OnClientClick="return confirm('確定要刪除?')" CommandName="del"><span>刪除</span></asp:LinkButton>
                            
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </cc1:GridView>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click" />
                 <asp:AsyncPostBackTrigger ControlID="btn_my" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btn_all" EventName="Click" />
                <asp:AsyncPostBackTrigger ControlID="btn_dep" EventName="Click" />
                
            </Triggers>
        </asp:UpdatePanel>
        <div class="footer">
            <div class="f1">
            </div>
            <div class="f2">
            </div>
            <div class="f3">
            </div>
        </div>

        
         <asp:UpdatePanel ID="UpdatePanel3" runat="server" >
            <ContentTemplate> 
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
