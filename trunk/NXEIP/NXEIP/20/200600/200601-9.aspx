<%@ Page Language="C#" AutoEventWireup="true" CodeFile="200601-9.aspx.cs" Inherits="_20_200600_200601_9"
    MasterPageFile="~/MasterPage.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="MattBerseth.WebControls" Namespace="MattBerseth.WebControls"
    TagPrefix="cc1" %>
<%@ Register Src="~/lib/Navigator.ascx" TagName="Navigator" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function update(msg) {

            __doPostBack('<%=UpdatePanel1.ClientID%>', '');
            tb_remove();


            if (msg != undefined && msg!="") {

                alert(msg);
            }
        }

        function pageLoad(sender, args) {
            if (args.get_isPartialLoad()) {
                //  reapply the thick box stuff
                //tb_init('a.thickbox');
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
  
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" EnablePaging="True" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTao03" TypeName="NXEIP.DAO._200601_9DAO" 
        SelectCountMethod="GetTao03Count">
        <SelectParameters>
            <asp:Parameter Name="tao_no" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <uc1:Navigator ID="Navigator1" runat="server" SysFuncNo="200601" SubFunc="討論區會員管理" />
    <div class="tableDiv">
        <div class="talk">
            <div class="select">
                <div class="b6">
                    <a href="200601.aspx" class="b-input">回討論區總表</a>
                </div>

                <div class="b6">
                    <a  class="b-input" href="200601-2.aspx?tao_no=<%=Request["tao_no"] %>" class="b-input">回討論區</a>
                </div>


                <div class="b6">
                    <a class="thickbox b-input" href="200601-10.aspx?tao_no=<%=Request["tao_no"]%>&TB_iframe=true&height=400&width=450&modal=true" class="b-input">新增會員</a>
                </div>


                <!--
                <div class="b6">
                    <a href="#" class="b-input">列印</a></div>
                -->

               
              
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <cc1:GridView ID="GridView1" CssClass="box" runat="server" 
                        AutoGenerateColumns="False" CellSpacing="1"
                        DataSourceID="ObjectDataSource1" GridLines="None" EmptyDataText="沒有任何會員" 
                        AllowPaging="True" DataKeyNames="tao_no,t03_no" 
                        onrowcommand="GridView1_RowCommand">
                        <Columns>
                            <asp:TemplateField HeaderText="會員部門">
                           <ItemStyle CssClass="row6_bg" />
                                <ItemTemplate>
                                  <asp:Label ID="Label1" runat="server" Text='<%# Eval("people.departments.dep_name") %>'></asp:Label>      
                                    
                                              
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="會員名稱">
                            <ItemStyle CssClass="row6_bg" />
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%#Eval("people.peo_name")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="申請時間">
                            <ItemStyle CssClass="row3_bg" />
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Text='<%# GetROCDT((DateTime?)Eval("t03_date"))%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="狀態">
                            <ItemStyle CssClass="row2_bg" />
                                <ItemTemplate>
                                    
                                        <asp:Label ID="Label4" runat="server" Text='<%#  GetStatus((String)Eval("t03_status"))%>'></asp:Label>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>

                             <asp:TemplateField HeaderText="備註">
                            <ItemStyle CssClass="row1_bg" />
                                <ItemTemplate>
                                    <asp:Label ID="Label5" runat="server" Text='<%#Eval("t03_memo")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="管理">
                            <ItemStyle CssClass="row5_bg" />
                                <ItemTemplate>
                                     <asp:Button ID="Button2" runat="server" CssClass="add" 
                                        CommandName="apply" OnClientClick="return confirm('確定要通過申請?')" />
                                    
                                    
                                    <asp:Button ID="Button1" runat="server" CssClass="delete"  Visible='<%# ((String)Eval("t03_status")=="0") %>'
                                        CommandName="del" OnClientClick="return confirm('確定要刪除?')" />
                                    

                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </cc1:GridView>
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
    </div>
 
</asp:Content>
