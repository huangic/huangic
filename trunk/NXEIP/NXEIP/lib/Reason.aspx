<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Reason.aspx.cs" Inherits="lib_Reason" %>

<%@ Register src="CssLayout.ascx" tagname="CssLayout" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <uc1:CssLayout ID="CssLayout1" runat="server" />
</head>
<body>
    <form id="form1" runat="server">

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
            <tr>
                <th style="width:15%">
                    簽核意見
                </th>
                <td>
                    <asp:TextBox ID="tbox_reason" runat="server" Height="60px" TextMode="MultiLine" 
                        Width="380px" MaxLength="100"></asp:TextBox>
                </td>
            </tr>
        </table>


        <div class="bottom">
            <asp:Button ID="Button1" runat="server" CssClass="b-input" Text="個人詞庫" />
            &nbsp;&nbsp;
            <asp:Button ID="btn_ok" runat="server" CssClass="b-input" Text="確定" OnClick="btn_ok_Click" />
            &nbsp;&nbsp;&nbsp;
            <asp:Button ID="btn_cancel" runat="server" CssClass="a-input" Text="取消" OnClientClick="self.parent.tb_remove()"
                UseSubmitBehavior="false" />
        </div>
        <div id="div_msg" runat="server">
        </div>
    </div>
    </form>
</body>
</html>
