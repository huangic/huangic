<%@ Page Language="C#" AutoEventWireup="true" CodeFile="350101-1.aspx.cs" Inherits="_35_350100_350101_1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../../lib/CssLayout.ascx" TagName="CssLayout" TagPrefix="uc1" %>
<%@ Register Src="../../lib/Navigator.ascx" TagName="Navigator" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <uc1:CssLayout ID="CssLayout1" runat="server" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:HiddenField ID="hidden_role_no" runat="server" />
    <uc2:Navigator ID="Navigator1" runat="server" SysFuncNo="350101" />
    <div class="tableDiv">
        <div class="header">
            <div class="h1">
            </div>
            <div class="h2">
            </div>
            <div class="h3">
            </div>
        </div>
        <table>
            <tbody>
                <tr>
                    <th>
                        角色名稱
                    </th>
                    <td>
                        <asp:TextBox ID="tbx_role_name" runat="server" Height="20px" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>
                        角色備註
                    </th>
                    <td>
                        <asp:TextBox ID="tbx_role_memo" runat="server" Height="60px" MaxLength="250" TextMode="MultiLine"
                            Width="200px"></asp:TextBox>
                    </td>
                </tr>
            </tbody>
        </table>
        <div class="footer">
            <div class="f1">
            </div>
            <div class="f2">
            </div>
            <div class="f3">
            </div>
        </div>
        <div class="bottom">
            <asp:Button ID="btn_ok" runat="server" CssClass="b-input" Text="確定" OnClick="btn_ok_Click" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btn_cancel" runat="server" CssClass="a-input" Text="取消" OnClientClick="self.parent.tb_remove()"
                UseSubmitBehavior="False" />
        </div>
    </div>
    </form>
</body>
</html>
