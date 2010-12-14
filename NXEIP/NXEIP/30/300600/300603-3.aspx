<%@ Page Language="C#" AutoEventWireup="true" CodeFile="300603-3.aspx.cs" Inherits="_30_300600_300603_3" %>

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
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetRep06Parent" TypeName="NXEIP.DAO.Rep06DAO">
        <SelectParameters>
            <asp:Parameter Name="r05_no" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:HiddenField ID="hidd_r05no" runat="server" />
    <asp:HiddenField ID="hidd_r06no" runat="server" />
    <uc2:Navigator ID="Navigator1" runat="server" SysFuncNo="300603" />
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
                    維修父類別
                </th>
                <td>
                    <asp:DropDownList ID="DropDownList1" runat="server" AppendDataBoundItems="True" 
                        DataSourceID="ObjectDataSource1" DataTextField="r06_name" 
                        DataValueField="r06_no">
                        <asp:ListItem Value="0">無</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th style="width: 15%">
                    子類別名稱
                </th>
                <td>
                    <asp:TextBox ID="tbox_name" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <th style="width: 15%">
                    排序
                </th>
                <td>
                    <asp:TextBox ID="tbox_order" runat="server"></asp:TextBox>
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
