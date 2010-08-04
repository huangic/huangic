<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="350103.aspx.cs" Inherits="_35_350100_350103" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../../lib/tree/jQueryPeopleTree.ascx" TagName="jQueryPeopleTree"
    TagPrefix="uc1" %>
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table width="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF">
                <tr>
                    <td>
                        <table width="100%" border="0" cellpadding="0" cellspacing="20">
                            <tr>
                                <td height="22" valign="top">
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td width="17">
                                                <img src="../../image/b01.gif" width="17" height="22" />
                                            </td>
                                            <td background="../../image/b01-1.gif" class="b01">
                                                帳號管理 / 權限管理 / <strong>帳號管理 </strong>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                        <tr>
                                            <td class="leftheaderbg">
                                                &nbsp;
                                            </td>
                                            <td class="a02-15 headerbg">
                                                帳號管理
                                            </td>
                                            <td class="rightheaderbg">
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                    <asp:Panel ID="Panel1" runat="server">
                                        <table width="100%" border="0" cellpadding="3" cellspacing="3">
                                            <tr>
                                                <td width="14%" bgcolor="#EEEEEE" class="a-letter-1">
                                                    <div align="center">
                                                        請選擇欲查詢人員
                                                    </div>
                                                </td>
                                                <td width="86%" valign="top" bgcolor="#EEEEEE" class="a-letter-1">
                                                    <uc1:jQueryPeopleTree ID="jQueryPeopleTree1" runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                        <table width="100%" border="0" cellpadding="0" cellspacing="10" bgcolor="#FFFFFF">
                                            <tr>
                                                <td>
                                                    <div align="center">
                                                        <asp:Button ID="but_ok" runat="server" CssClass="b-input" Text="確定" OnClick="but_ok_Click" />
                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                        <asp:Button ID="but_cancel" runat="server" CssClass="a-input" Text="取消" OnClick="but_cancel_Click" />
                                                        &nbsp;&nbsp;</div>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                    <!-- 修改頁面 ->
                                    <asp:Panel ID="Panel2" runat="server">
                                        <table width="100%" border="0" cellpadding="3" cellspacing="3" bgcolor="#FFFFFF">
                                            <tr>
                                                <td width="100" bgcolor="#eeeeee" class="a-letter-2">
                                                    <div align="right">
                                                        姓名</div>
                                                </td>
                                                <td bgcolor="#EEEEEE" class="a-letter-1">
                                                    <asp:Label ID="lab_name" runat="server"></asp:Label>
                                                </td>
                                                <td width="100" bgcolor="#eeeeee" class="a-letter-2">
                                                    <div align="right">
                                                        人事編號</div>
                                                </td>
                                                <td bgcolor="#EEEEEE" class="a-letter-1">
                                                    <asp:Label ID="lab_workid" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td bgcolor="#EEEEEE" class="a-letter-2">
                                                    <div align="right">
                                                        登入帳號</div>
                                                </td>
                                                <td bgcolor="#EEEEEE" class="a-letter-1">
                                                    <asp:TextBox ID="tbox_accounts" runat="server" Height="20px" MaxLength="20"></asp:TextBox>
                                                    <span class="a-letter-Red">帳號若不變，請勿修改</span>
                                                </td>
                                                <td bgcolor="#EEEEEE" class="a-letter-2">
                                                    <div align="right">
                                                        密碼</div>
                                                </td>
                                                <td bgcolor="#EEEEEE" class="a-letter-1">
                                                    <asp:TextBox ID="tbox_passwd" runat="server" Height="20px" MaxLength="20" 
                                                        TextMode="Password"></asp:TextBox>
                                                    <span class="a-letter-Red">密碼若不變，請保持空白</span>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td bgcolor="#EEEEEE" class="a-letter-2">
                                                    <div align="right">
                                                        帳號使用狀況
                                                    </div>
                                                </td>
                                                <td colspan="3" bgcolor="#EEEEEE" class="a-letter-1">
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
                                        <table width="100%" border="0" cellpadding="0" cellspacing="10" bgcolor="#FFFFFF">
                                            <tr>
                                                <td>
                                                    <div align="center">
                                                        <asp:Button ID="Button1" runat="server" CssClass="b-input" Text="確定" 
                                                            OnClick="Button1_Click" />
                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                        <asp:Button ID="Button2" runat="server" CssClass="a-input" Text="取消" 
                                                            OnClick="Button2_Click" />
                                                        &nbsp;&nbsp;</div>
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
