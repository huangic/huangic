<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="300901-4.aspx.cs" Inherits="_30_300900_300901_4" %>

<%@ Register Assembly="MattBerseth.WebControls" Namespace="MattBerseth.WebControls"
    TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../../lib/Navigator.ascx" TagName="Navigator" TagPrefix="uc1" %>
<%@ Register src="../../lib/calendar.ascx" tagname="calendar" tagprefix="uc2" %>
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
    <asp:ObjectDataSource ID="DataSource" runat="server" SelectMethod="GetSubmitByFormNo"
        TypeName="NXEIP.DAO._300901DAO" EnablePaging="True" 
        SelectCountMethod="GetSubmitByFormNoCount" OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="" Name="f01_no" QueryStringField="ID" 
                Type="Int32" />
            <asp:Parameter Name="sDate" Type="DateTime" />
            <asp:Parameter Name="eDate" Type="DateTime" />
            <asp:Parameter Name="peo_name" Type="String" />
            <asp:Parameter Name="dep_no" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <uc1:Navigator ID="Navigator1" runat="server" SysFuncNo="300901" SubFunc="提交清單" />
   
   
  <div class="select">
            <span class="a-letter-2">日期：</span>
         
            <span class="a-letter-1">
                      <uc2:calendar ID="cal_sdate" runat="server" />至<uc2:calendar ID="cal_edate" runat="server" />
            </span>

            <span class="a-letter-2">人員：</span>
             <span class="a-letter-1">
                 <asp:TextBox ID="tb_people" runat="server"></asp:TextBox>
              </span>

            &nbsp;<asp:Button ID="btn_search" runat="server"  CssClass="b-input" Text="搜尋" 
                onclick="btn_search_Click" />
                          
                
                
               
        </div>
   
   
   
   
    <div class="tableDiv">
        <div class="header">
            <div class="h1">
            </div>
            <div class="h2">
                <div class="function">
                    
                </div>
            </div>
            <div class="h3">
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
           
                <cc1:GridView ID="GridView1" runat="server" DataSourceID="DataSource"
                    AutoGenerateColumns="False" Width="100%" AllowPaging="True"  EmptyDataText="目前無資料"
                    CellPadding="3" CellSpacing="3"
                    CssClass="tableData" GridLines="None" OnRowCommand="GridView1_RowCommand" DataKeyNames="Submit"
                    >
                    <Columns>
                        <asp:TemplateField HeaderText="表單名稱">
                            
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# (Eval("Form.f01_name")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                         <asp:TemplateField HeaderText="提交部門">
                            <ItemStyle Width="100px" />
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("Submit.people.departments.dep_name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="提交人">
                            <ItemStyle Width="100px" />
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("Submit.people.peo_name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                         <asp:TemplateField HeaderText="提交日期">
                            <ItemStyle Width="100px" />
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%# new ChangeObject()._ADtoROC((DateTime)Eval("Submit.f02_createtime")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                                              
                        <asp:TemplateField HeaderText="檢視" ItemStyle-HorizontalAlign="Center">
                            <ItemStyle Width="60px" />
                            <ItemTemplate>
                                <a id="btnShowPopup" runat="server" class="thickbox imageButton peruse" title='檢視'
                                    href='<%# String.Format( "300901-5.aspx?modal=true&mode=edit&ID={0}&SID={1}&TB_iframe=true&height=600&width=600",Eval("Form.f01_no"),Eval("Submit.f02_no")) %>'>
                                    <span>修改</span>
                                </a>
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
            
             <div class="pager">
        <asp:Button ID="Button2" runat="server" Text="回上一頁" CssClass="a-input" 
                 onclick="Button2_Click" />
        </div>
            
            
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btn_search" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
        

    </div>
</asp:Content>
