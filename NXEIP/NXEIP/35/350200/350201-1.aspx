<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="350201-1.aspx.cs"
    Inherits="_35_350200_350201_1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../../lib/CssLayout.ascx" TagName="CssLayout" TagPrefix="uc1" %>
<%@ Register Src="../../lib/Navigator.ascx" TagName="Navigator" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <uc1:CssLayout ID="CssLayout1" runat="server" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <uc2:Navigator ID="Navigator1" runat="server" SysFuncNo="350201" />
    <div class="tableDiv">
        <asp:HiddenField ID="hidden_typ_no" runat="server" />
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
                        職稱代碼
                    </th>
                    <td>
                        <asp:TextBox ID="tbx_typ_number" runat="server"></asp:TextBox><span class="a-letter-Red">代碼長度為4位數</span>
                    </td>
                </tr>
                <tr>
                    <th>
                        職稱中文
                    </th>
                    <td>
                        <asp:TextBox ID="tbx_typ_cname" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>
                        職稱英文
                    </th>
                    <td>
                        <asp:TextBox ID="tbx_typ_ename" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>
                        排列順序
                    </th>
                    <td>
                        <asp:TextBox ID="tbx_typ_order" runat="server"></asp:TextBox>
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
                UseSubmitBehavior="false" />
        </div>
    </div>
    </form>
</body>
</html>
