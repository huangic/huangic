<%@ Page Language="C#" AutoEventWireup="true" CodeFile="200601-2.aspx.cs" Inherits="_20_200600_200601_2"
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
  
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" EnablePaging="True" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTopicList" TypeName="NXEIP.DAO._200601_2DAO" 
        SelectCountMethod="GetTopicListCount">
        <SelectParameters>
            <asp:QueryStringParameter Name="tao_no" QueryStringField="tao_no" Type="Int32" />
            <asp:Parameter Name="peo_uid" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <uc1:Navigator ID="Navigator1" runat="server" SysFuncNo="200601" SubFunc="主題列表" />
    <div class="tableDiv">
        <div class="talk">
            <div class="select">
                <div class="b6">
                    <a href="#" class="b-input">回討論區總表</a>
                </div>
                <div class="b6">
                    <a href="#" class="b-input">列印</a></div>
                <div class="b6">
                    <a href="#" class="b-input">文章查詢 </a>
                </div>
                <div class="b6">
                    <a href="#" class="b-input">觀看精華區 </a>
                </div>
                <div class="b6">
                    <a href="#" class="b-input">發表主題 </a>
                </div>
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <cc1:GridView ID="GridView1" CssClass="box" runat="server" 
                        AutoGenerateColumns="False" CellSpacing="1"
                        DataSourceID="ObjectDataSource1" GridLines="None" EmptyDataText="沒有任何主題" 
                        AllowPaging="True">
                        <Columns>
                            <asp:TemplateField HeaderText="主題">
                           <ItemStyle CssClass="row1_bg" />
                                <ItemTemplate>
                                    <ul>
                                        <li class="t1">
                                            <asp:HyperLink ID="HyperLink1" runat="server" Text='<%# Eval("Name") %>'></asp:HyperLink>
                                        </li>
                                    </ul>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="發表人">
                            <ItemStyle CssClass="row6_bg" />
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%#Eval("Author")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="回覆">
                            <ItemStyle CssClass="row3_bg" />
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%#Eval("RelayCount")%>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="最後發表">
                            <ItemStyle CssClass="row2_bg" />
                                <ItemTemplate>
                                    <div class="t4">
                                        <asp:Label ID="Label3" runat="server" Text='<%#  GetROCDT((DateTime)Eval("LastRelayDate"))%>'></asp:Label></div>
                                    <div class="t5">
                                        <ul>
                                            <li class="name">
                                                <asp:Label ID="Label4" runat="server" Text='<%#Eval("LastRelayAuthor") %>'></asp:Label></li>
                                            <li class="arrow_ms02"></li>
                                        </ul>
                                    </div>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="刪除">
                            <ItemStyle CssClass="row5_bg" />
                                <ItemTemplate>
                                    <asp:Button ID="Button1" runat="server" CssClass="delete" Visible='<%# (bool)Eval("HasPermission")%>'
                                        CommandName="del" OnClientClick="return confirm('確定要刪除?')" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </cc1:GridView>
                    <div class="pager">
                        <asp:DataPager ID="DataPager1" runat="server" PagedControlID="GridView1" PageSize="10">
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
