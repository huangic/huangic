<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="200501.aspx.cs" Inherits="_20_200500_200501" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="MattBerseth.WebControls" Namespace="MattBerseth.WebControls"
    TagPrefix="cc1" %>
<%@ Register src="~/lib/people/PeopleDetail.ascx" tagname="PeopleDetail" tagprefix="uc2" %>

<%@ Register src="~/lib/Navigator.ascx" tagname="Navigator" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:ObjectDataSource ID="ods_new01" runat="server" SelectMethod="GetDataBySysNo"
        TypeName="NXEIP.DAO.New01DAO" EnablePaging="True" 
        SelectCountMethod="GetDataBySysNoCount">
        <SelectParameters>
            <asp:Parameter Name="s06_no" Type="Int32" />
            <asp:Parameter Name="key" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ods_doc10" runat="server" SelectMethod="GetDataByS06No"
        TypeName="NXEIP.DAO.Doc10DAO" EnablePaging="True" 
        SelectCountMethod="GetDataByS06NoCount" 
        OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:Parameter Name="s06_no" Type="Int32" />
            <asp:Parameter Name="key" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:HiddenField ID="hidd_newS06no" runat="server" />
    <asp:HiddenField ID="hidd_d09S06no" runat="server" />
    
    <uc1:Navigator ID="Navigator1" runat="server" SysFuncNo="200501" />
    
    <div>
        <div class="select">
            <div class="center0">
                最新消息[<asp:Label ID="lab_newtitle" runat="server"></asp:Label>]查詢：
            </div>
            <div class="center1">
                <asp:TextBox ID="tbox_newkey" runat="server"></asp:TextBox>
            </div>
            <div class="b5">
                <asp:Button ID="Button1" runat="server" Text="查詢" CssClass="b-input" 
                    onclick="Button1_Click" />
            </div>
        </div>
        <div class="select-3">
            <asp:ListView ID="ListView1" runat="server" DataKeyNames="n01_no" 
                DataSourceID="ods_new01">
                <ItemTemplate>
                    <li class="ins"><a runat="server" title="查看內容" class="thickbox a-letter-s3" href='<%# Eval("n01_no", "~/10/100200/100204-2.aspx?modal=true&n01_no={0}&TB_iframe=true") %>'>
                        <%# string.Format("{0} ({1})",Eval("n01_subject"),new ChangeObject()._ADtoROC((DateTime)Eval("n01_date"))) %>
                    </a></li>
                </ItemTemplate>
            </asp:ListView>
        </div>
        <div class="pager">
            <asp:DataPager ID="DataPager2" runat="server" PagedControlID="ListView1" 
                PageSize="10">
                <Fields>
                    <asp:NextPreviousPagerField ShowNextPageButton="False" />
                    <asp:NumericPagerField />
                    <asp:NextPreviousPagerField ShowPreviousPageButton="False" />
                </Fields>
            </asp:DataPager>
        </div>
        <br />
        <div class="tableDiv">
            <div class="select">
                <div class="center0">
                    檔案區[<asp:Label ID="lab_d09title" runat="server"></asp:Label>]查詢：
                </div>
                <div class="center1">
                    <asp:TextBox ID="tbox_d09key" runat="server"></asp:TextBox>
                </div>
                <div class="b5">
                    <asp:Button ID="Button2" runat="server" Text="查詢" CssClass="b-input" 
                        onclick="Button2_Click" />
                </div>
            </div>
            <div class="header">
                <div class="h1">
                </div>
                <div class="h2">
                </div>
                <div class="h3">
                </div>
            </div>
            <cc1:GridView ID="GridView1" runat="server" DataSourceID="ods_doc10" AllowPaging="True"
                AutoGenerateColumns="False" CellPadding="3" CellSpacing="3" CssClass="tableData"
                EmptyDataText="查無資料" OnRowDataBound="GridView1_RowDataBound" GridLines="None"
                EnableViewState="False" DataKeyNames="d09_no,d10_no">
                <Columns>
                    <asp:BoundField DataField="d09_no" HeaderText="檔案類別" SortExpression="d09_no">
                        <ItemStyle Width="15%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="d09_no" HeaderText="檔案子類別" SortExpression="d09_no">
                        <ItemStyle Width="15%" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="檔案">
                        <ItemTemplate>
                            <asp:HyperLink ID="HyperLink1" runat="server" ToolTip="下載檔案" CssClass="download imageButton" Target="_blank"
                                NavigateUrl='<%#String.Format("~/20/200100/200107-1.ashx?d09={0}&d10={1}",Eval("d09_no"),Eval("d10_no"))  %>'><span>下載</span></asp:HyperLink>
                            <asp:Label ID="Label1" runat="server" Text='<%# Eval("d10_file") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Width="30%" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="d09_no" HeaderText="上傳時間" SortExpression="d09_no">
                        <ItemStyle Width="15%" />
                    </asp:BoundField>
                    <asp:BoundField DataField="d09_no" HeaderText="上傳單位" SortExpression="d09_no">
                        <ItemStyle Width="15%" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="上傳人員">
                        <ItemTemplate>
                            <asp:Label ID="lab_name" runat="server"></asp:Label>
                            <uc2:PeopleDetail ID="PeopleDetail1" runat="server" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Width="10%" />
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
                <asp:DataPager ID="DataPager1" runat="server" PagedControlID="GridView1" PageSize="10">
                    <Fields>
                        <asp:NextPreviousPagerField ShowNextPageButton="False" />
                        <asp:NumericPagerField />
                        <asp:NextPreviousPagerField ShowPreviousPageButton="False" />
                    </Fields>
                </asp:DataPager>
            </div>
        </div>
    </div>
</asp:Content>

