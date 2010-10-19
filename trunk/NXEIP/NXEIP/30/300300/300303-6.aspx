<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="300303-6.aspx.cs" Inherits="_30_300300_300303_6" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<%@ Register src="../../lib/Navigator.ascx" tagname="Navigator" tagprefix="uc1" %>
<%@ Register Assembly="MattBerseth.WebControls" Namespace="MattBerseth.WebControls"
    TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server"
        SelectMethod="Get_e04Data_pass" TypeName="NXEIP.DAO._300303DAO" 
        OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:Parameter Name="e02_no" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:HiddenField ID="hidd_no" runat="server" />
    <uc1:Navigator ID="Navigator1" runat="server" SysFuncNo="300303" />
    <div class="tableDiv">
        <div class="header">
            <div class="h1">
            </div>
            <div class="h2">
                <div class="name">
                    <asp:Label ID="lab_titile" runat="server"></asp:Label>
                </div>
            </div>
            <div class="h3">
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <cc1:GridView ID="GridView1" runat="server" DataSourceID="ObjectDataSource1" AutoGenerateColumns="False"
                    CellPadding="3" CellSpacing="3" CssClass="tableData" EmptyDataText="查無資料" DataKeyNames="e04_no"
                    OnRowDataBound="GridView1_RowDataBound" GridLines="None" EnableViewState="False">
                    <Columns>
                        <asp:TemplateField HeaderText="選取">
                            <ItemTemplate>
                                <asp:CheckBox ID="cbox" runat="server" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="10%" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="e04_depno" HeaderText="單位" SortExpression="e04_depno" />
                        <asp:BoundField DataField="e04_prono" HeaderText="職稱" SortExpression="e04_prono" />
                        <asp:BoundField DataField="e04_peouid" HeaderText="姓名" SortExpression="e04_peouid" />
                        <asp:BoundField DataField="e04_peouid" HeaderText="身份證字號" SortExpression="e04_peouid" />
                        <asp:BoundField DataField="e04_sign" HeaderText="是否出席" SortExpression="e04_sign">
                            <ItemStyle HorizontalAlign="Center" Width="10%" />
                        </asp:BoundField>
                    </Columns>
                </cc1:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="bottom">
            <asp:Button ID="Button1" runat="server" CssClass="b-input" Text="全選" 
                CommandArgument="1" onclick="Button1_Click" />
            &nbsp;
            <asp:Button ID="Button2" runat="server" CssClass="b-input" Text="取消全選" 
                CommandArgument="2" onclick="Button1_Click" />
            &nbsp;
            <asp:Button ID="btn_sign" runat="server" CssClass="b-input" Text="出席" 
                CommandArgument="1" onclick="btn_sign_Click" />
            &nbsp;
            <asp:Button ID="btn_unsign" runat="server" CssClass="b-input" Text="未出席" 
                CommandArgument="2" onclick="btn_sign_Click" />
            &nbsp;
            <asp:Button ID="btn_cancel" runat="server" CssClass="a-input" Text="回管理課程" OnClick="btn_cancel_Click" />
            &nbsp;
            <asp:Button ID="btn_cancel2" runat="server" CssClass="a-input" Text="回課程檢視" OnClick="btn_cancel2_Click" />
        </div>
        
    </div>
</asp:Content>

