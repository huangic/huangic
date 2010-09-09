<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="350103.aspx.cs" Inherits="_35_350100_350103" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../../lib/tree/jQueryPeopleTree.ascx" TagName="jQueryPeopleTree"
    TagPrefix="uc1" %>
<%@ Register Src="../../lib/Navigator.ascx" TagName="Navigator" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

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
    <uc2:Navigator ID="Navigator1" runat="server" SysFuncNo="350103" />
    <div class="tableDiv">
        <div class="header">
            <div class="h1">
            </div>
            <div class="h2">
            </div>
            <div class="h3">
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <asp:Panel ID="Panel1" runat="server">
                    <table>
                        <tr>
                            <th style="width:25%">
                                請選擇欲查詢人員
                            </th>
                            <td>
                                <uc1:jQueryPeopleTree ID="jQueryPeopleTree1" runat="server" />
                            </td>
                        </tr>
                    </table>
                    <div class="bottom">
                        <asp:Button ID="but_ok" runat="server" CssClass="b-input" Text="確定" OnClick="but_ok_Click" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="but_cancel" runat="server" CssClass="a-input" Text="取消" OnClick="but_cancel_Click" />
                    </div>
                </asp:Panel>
                <!-- 修改頁面 !-->
                <asp:Panel ID="Panel2" runat="server">
                    <table>
                        <tr>
                            <th>
                                姓名
                            </th>
                            <td>
                                <asp:Label ID="lab_name" runat="server"></asp:Label>
                            </td>
                            <th>
                                人事編號
                            </th>
                            <td>
                                <asp:Label ID="lab_workid" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                登入帳號
                            </th>
                            <td>
                                <asp:TextBox ID="tbox_accounts" runat="server" Height="20px" MaxLength="20"></asp:TextBox>
                                <span class="a-letter-Red">帳號若不變，請勿修改</span>
                            </td>
                            <th>
                                密碼
                            </th>
                            <td>
                                <asp:TextBox ID="tbox_passwd" runat="server" Height="20px" MaxLength="20" TextMode="Password"></asp:TextBox>
                                <span class="a-letter-Red">密碼若不變，請保持空白</span>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                帳號使用狀況
                            </th>
                            <td colspan="3">
                                <asp:DropDownList ID="ddl_status" runat="server">
                                    <asp:ListItem Value="1">啟用</asp:ListItem>
                                    <asp:ListItem Value="2">未啟用</asp:ListItem>
                                </asp:DropDownList>
                                <asp:Label ID="lab_oldaccount" runat="server" Visible="False"></asp:Label>
                                <asp:Label ID="lab_oldpasswd" runat="server" Visible="False"></asp:Label>
                                <asp:Label ID="lab_accno" runat="server" Visible="False"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <div class="bottom">
                        <asp:Button ID="Button1" runat="server" CssClass="b-input" Text="確定" OnClick="Button1_Click" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="Button2" runat="server" CssClass="a-input" Text="取消" OnClick="Button2_Click" />
                    </div>
                    <div align="center">
                        <asp:Label ID="lab_msg" runat="server" ForeColor="Red"></asp:Label>
                    </div>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
