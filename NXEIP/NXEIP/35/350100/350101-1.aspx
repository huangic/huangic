<%@ Page Language="C#" AutoEventWireup="true" CodeFile="350101-1.aspx.cs" Inherits="_35_350100_350101_1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="~/css/eip.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <div>
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <ContentTemplate>
                <asp:HiddenField ID="hidden_role_no" runat="server" />
                <table width="600px" cellspacing="20" cellpadding="0" border="0" bgcolor="White">
                    <tbody>
                        <tr>
                            <td valign="top" height="22">
                                <table width="100%" cellspacing="0" cellpadding="0" border="0">
                                    <tbody>
                                        <tr>
                                            <td width="17">
                                                <img width="17" height="22" src="../../image/b01.gif">
                                            </td>
                                            <td background="../../image/b01-1.gif" class="b01">
                                                帳號管理 / 權限管理 /<strong> 角色設定</strong>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tbody>
                                        <tr>
                                            <td width="17">
                                                <img height="29" src="../../image/b02.gif" width="17"></img>
                                            </td>
                                            <td background="../../image/b02-1.gif" class="a02-15">
                                                <asp:Label ID="lab_headermode" runat="server"></asp:Label>
                                                角色資料
                                            </td>
                                            <td background="../../image/b02-1.gif">
                                                <div align="right">
                                                </div>
                                            </td>
                                            <td width="17">
                                                <img height="29" src="../../image/b02-2.gif" width="17"></img>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                <table bgcolor="#ffffff" border="0" cellpadding="3" cellspacing="3" width="100%">
                                    <tbody>
                                        <tr>
                                            <td bgcolor="#eeeeee" class="a-letter-2" width="100">
                                                <div align="right">
                                                    角色名稱
                                                </div>
                                            </td>
                                            <td bgcolor="#eeeeee" class="a-letter-1">
                                                <asp:TextBox ID="tbx_role_name" runat="server" Height="20px" Width="200px"></asp:TextBox>
                                                <br />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td bgcolor="#eeeeee" class="a-letter-2">
                                                <div align="right">
                                                    角色備註
                                                </div>
                                            </td>
                                            <td bgcolor="#eeeeee" class="a-letter-1">
                                                <asp:TextBox ID="tbx_role_memo" runat="server" Height="60px" MaxLength="250" TextMode="MultiLine"
                                                    Width="200px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td width="17">
                                            <img src="../../image/b02-3.gif" width="17" height="17" />
                                        </td>
                                        <td background="../../image/b02-4.gif">
                                            &nbsp;
                                        </td>
                                        <td width="17">
                                            <img src="../../image/b02-5.gif" width="17" height="17" />
                                        </td>
                                    </tr>
                                </table>
                                <table border="0" cellpadding="0" cellspacing="10" width="100%" bgcolor="White">
                                    <tbody>
                                        <tr>
                                            <td>
                                                <div align="center">
                                                    <asp:Button ID="btn_ok" runat="server" CssClass="b-input" Text="確定" OnClick="btn_ok_Click" />
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:Button ID="btn_cancel" runat="server" CssClass="a-input" Text="取消" OnClientClick="self.parent.tb_remove()"
                                                        UseSubmitBehavior="False" />
                                                    &nbsp;</div>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                    </tbody>
                </table>
                <br />
                <br />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
