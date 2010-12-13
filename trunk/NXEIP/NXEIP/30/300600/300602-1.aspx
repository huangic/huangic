<%@ Page Language="C#" AutoEventWireup="true" CodeFile="300602-1.aspx.cs" Inherits="_30_300600_300602_1" %>

<%@ Register src="../../lib/CssLayout.ascx" tagname="CssLayout" tagprefix="uc1" %>

<%@ Register src="../../lib/Navigator.ascx" tagname="Navigator" tagprefix="uc2" %>

<%@ Register src="../../lib/tree/DepartTreeTextBox.ascx" tagname="DepartTreeTextBox" tagprefix="uc3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <uc1:CssLayout ID="CssLayout1" runat="server" />
    <script type="text/javascript" src="../../js/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="../../js/thickbox.js"></script>
    
</head>
<body>
    <form id="form1" runat="server">
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
        OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" 
        TypeName="NXEIP.DAO.Rep05DAO"></asp:ObjectDataSource>
    <asp:HiddenField ID="hidd_r05no" runat="server" />
    <asp:HiddenField ID="hidd_r01no" runat="server" />
    <uc2:Navigator ID="Navigator1" runat="server" SysFuncNo="300602" />
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
                    維修類別
                </th>
                <td>
                    
                    <asp:DropDownList ID="DropDownList1" runat="server" AppendDataBoundItems="True" 
                        DataSourceID="ObjectDataSource1" DataTextField="r05_name" 
                        DataValueField="r05_no">
                        <asp:ListItem Value="0">請選擇</asp:ListItem>
                    </asp:DropDownList>
                    
                </td>
            </tr>
            <tr>
                <th style="width: 15%">
                    通知方式
                </th>
                <td>
                    <asp:CheckBoxList ID="CheckBoxList1" runat="server">
                        <asp:ListItem Value="1">電子郵件</asp:ListItem>
                        <asp:ListItem Value="1">訊息通知</asp:ListItem>
                        <asp:ListItem Value="1">e公務訊息</asp:ListItem>
                    </asp:CheckBoxList>

                </td>
            </tr>
            <tr>
                <th style="width: 15%">
                    管理者
                </th>
                <td>
                    <uc3:DepartTreeTextBox ID="DepartTreeTextBox1" runat="server" 
                        LeafType="People" />
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
