<%@ Page Language="C#" AutoEventWireup="true" CodeFile="100403-1.aspx.cs" Inherits="_10_100400_100403_1" %>

<%@ Register src="../../lib/CssLayout.ascx" tagname="CssLayout" tagprefix="uc1" %>

<%@ Register src="../../lib/Navigator.ascx" tagname="Navigator" tagprefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <uc1:CssLayout ID="CssLayout1" runat="server" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetSpot"
        TypeName="NXEIP.DAO._100403DAO" 
        OldValuesParameterFormatString="original_{0}">
    </asp:ObjectDataSource>
    <uc2:Navigator ID="Navigator1" runat="server" SubFunc="我要叫修" />
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
                <th style="width: 15%">
                    叫修單位
                </th>
                <td>
                    <asp:Label ID="lab_dep" runat="server"></asp:Label>
                </td>
                <th style="width: 15%">
                    叫修人員
                </th>
                <td>
                    <asp:Label ID="lab_people" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th style="width: 15%">
                    地點
                </th>
                <td colspan="3">
                    <asp:DropDownList ID="ddl_spot" runat="server" 
                        DataSourceID="ObjectDataSource1" DataTextField="spo_name" 
                        DataValueField="spo_no" AppendDataBoundItems="True">
                        <asp:ListItem Value="0">請選擇</asp:ListItem>
                    </asp:DropDownList>&nbsp;&nbsp;
                    樓層
                    <asp:DropDownList ID="ddl_floor" runat="server">
                        <asp:ListItem Value="0">請選擇</asp:ListItem>
                        <asp:ListItem>1樓</asp:ListItem>
                        <asp:ListItem>2樓</asp:ListItem>
                        <asp:ListItem>3樓</asp:ListItem>
                        <asp:ListItem>4樓</asp:ListItem>
                        <asp:ListItem>5樓</asp:ListItem>
                        <asp:ListItem>6樓</asp:ListItem>
                        <asp:ListItem>7樓</asp:ListItem>
                        <asp:ListItem>8樓</asp:ListItem>
                        <asp:ListItem>9樓</asp:ListItem>
                        <asp:ListItem>10樓</asp:ListItem>
                        <asp:ListItem>11樓</asp:ListItem>
                        <asp:ListItem>12樓</asp:ListItem>
                        <asp:ListItem>地下1樓</asp:ListItem>
                        <asp:ListItem>地下2樓</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th style="width: 15%">
                    故障原因
                </th>
                <td colspan="3">
                    <asp:TextBox ID="tbox_reason" runat="server" Height="125px" 
                        TextMode="MultiLine" Width="370px"></asp:TextBox>
                </td>
            </tr>
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
            <asp:Button ID="Button1" runat="server" Text="送出" CssClass="b-input" 
                onclick="Button1_Click" />
            &nbsp;&nbsp;
            <asp:Button ID="btn_cancel" runat="server" CssClass="a-input" Text="關閉" OnClientClick="self.parent.tb_remove()"
                UseSubmitBehavior="false" />
        </div>
    </div>
    </form>
</body>
</html>
