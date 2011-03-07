<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="300703.aspx.cs" Inherits="_30_300700_300703" EnableEventValidation="false" %>

<%@ Register Src="../../lib/Navigator.ascx" TagName="Navigator" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="MattBerseth.WebControls" Namespace="MattBerseth.WebControls"
    TagPrefix="cc1" %>
<%@ Register src="../../lib/people/PeopleDetail.ascx" tagname="PeopleDetail" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function update(msg) {

            $("#" + '<%=hidden_reason.ClientID%>').val(msg);
            //用隱藏BUTTIN的
            __doPostBack(chang('<%=LinkButton1.ClientID%>'), '');
            tb_remove();
            

            //alert(msg);
        }


        function chang(str) {
            //將底線換成$符號
            var regex = /\_/g;
            return str.replace(regex, '$');
        }

        function pageLoad(sender, args) {
            if (args.get_isPartialLoad()) {
                //  reapply the thick box stuff
                tb_init('a.thickbox input.thickbox');
            }
        }

        jQuery(document).ready(function () {
            $('.checkall').click(function () {
                $("input[type='checkbox']").attr("Checked", true);
            });

            $('.uncheckall').click(function () {
                $("input[type='checkbox']").removeAttr("Checked");
            });
        });



    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    
    <asp:ObjectDataSource ID="ObjectDataSource3" runat="server"  OldValuesParameterFormatString="original_{0}"
         SelectMethod="GetApplyEmail" TypeName="NXEIP.DAO.EmailDAO">
       
    </asp:ObjectDataSource>
    
    
    
    
    
    
    <uc1:Navigator ID="Navigator1" runat="server" SysFuncNo="300703" />
    
      
   
    
    
    
    <div class="tableDiv">
        
        

        <div class="header">
            <div class="h1">
            </div>
            <div class="h2">
                <div class="function">
                     <input type="button" class="checkall b-input" value="全選" />
                     <input type="button" class="uncheckall b-input" value="取消全選" />
                    <asp:Button ID="btn_ok" runat="server" Text="核可" CssClass="show b-input" 
                         onclick="btn_ok_Click"/>
                       
                    <input type="button" class="thickbox b-input" alt="../../lib/Reason.aspx?modal=true&TB_iframe=true&height=350&width=450"
                        value="未核可" />

                    <asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click"></asp:LinkButton>
                </div>
            </div>
            <div class="h3">
            </div>
        </div>
         <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
            <ContentTemplate>
                 
               <asp:HiddenField ID="hidden_reason" runat="server"/>
           

                <cc1:GridView ID="GridView1" runat="server"  AutoGenerateColumns="False"
                    CellPadding="3" CellSpacing="3" DataSourceID="ObjectDataSource3" EmptyDataText="查無資料"
                    GridLines="None" 
                     
                    DataKeyNames="ema_no">
                    <Columns>
                         <asp:TemplateField HeaderText="選取">
                         
                            <ItemTemplate>
                            <asp:CheckBox ID="cbox" runat="server" />
                            
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="30px" />
                        </asp:TemplateField>
                        
                        
                        <asp:TemplateField HeaderText="申請單位">
                            <ItemTemplate>
                                <asp:Label ID="Label4" runat="server" Text='<%# GetDepartmentName((Int32)Eval("ema_depno")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="申請人員">
                            <ItemTemplate>
                                <asp:Label ID="Label5" runat="server" Text='<%# new NXEIP.DAO.PeopleDAO().GetPeopleNameByUid((Int32)Eval("ema_peouid")) %>'></asp:Label>

                                <uc2:PeopleDetail ID="PeopleDetail1" runat="server" peo_uid='<%# Eval("ema_peouid") %>'/>

                            </ItemTemplate>
                        </asp:TemplateField>


                      
                       
                       
                        <asp:TemplateField HeaderText="申請日期">
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%#GetROCDate((DateTime?)Eval("ema_apply")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>


                        
                     
                         

                        <asp:TemplateField HeaderText="審核人員&lt;br/&gt;退件原因" >
                          
                            
                            
                            <ItemTemplate>
                                <asp:Label ID="Label8" runat="server" Text='<%# GetPeopleName((Int32?)Eval("ema_check")) %>'></asp:Label>
                                <uc2:PeopleDetail ID="PeopleDetail2" runat="server" peo_uid='<%# Eval("ema_check") %>'/>

                                <asp:Label ID="Label9" runat="server" Text='<%# GetROCDate((DateTime?)Eval("ema_checkdate")) %>'></asp:Label>
                                
                                <br />
                                <asp:Label ID="Label6" runat="server" Text='<%# Eval("ema_note") %>'></asp:Label>
                            
                            
                            
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="狀態">
                            <ItemTemplate>
                                <asp:Label ID="Label7" runat="server" Text='<%# GetStatus((String)Eval("ema_status")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        
                    </Columns>
                </cc1:GridView>
            </ContentTemplate>
           
             <Triggers>
                 <asp:AsyncPostBackTrigger ControlID="btn_ok" EventName="Click" />
                 <asp:AsyncPostBackTrigger ControlID="LinkButton1" EventName="Click" />
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

        
        
    </div>
    

</asp:Content>
