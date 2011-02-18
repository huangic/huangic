<%@ Page Language="C#" AutoEventWireup="true" CodeFile="300801-1.aspx.cs" Inherits="_30_300800_300801_1" %>

<%@ Register src="../../lib/Navigator.ascx" tagname="Navigator" tagprefix="uc1" %>

<%@ Register src="../../lib/CssLayout.ascx" tagname="CssLayout" tagprefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <uc2:CssLayout ID="CssLayout1" runat="server" />
</head>
<body>
    <form id="form1" runat="server">
    <uc1:Navigator ID="Navigator1" runat="server" SysFuncNo="300801" SubFunc="回覆出席" />
    <asp:HiddenField ID="hidd_no" runat="server" />
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
                    <th style="width:15%">
                        發布人
                    </th>
                    <td >
                       <asp:Label ID="lab_people" runat="server"></asp:Label>
                    </td>
                    <th style="width:15%">
                        發布日期
                    </th>
                    <td >
                       <asp:Label ID="lab_date" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th style="width:15%">
                        最新消息標題
                    </th>
                    <td >
                       <asp:Label ID="lab_subject" runat="server"></asp:Label>
                    </td>
                    <th style="width:15%">
                        最新消息內容
                    </th>
                    <td >
                       <asp:Label ID="lab_content" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th style="width:15%">
                        審核結果
                    </th>
                    <td>
                        
                        <asp:RadioButtonList ID="rbl_status" runat="server" 
                            RepeatDirection="Horizontal" RepeatLayout="Flow">
                            <asp:ListItem Selected="True" Value="1">通過</asp:ListItem>
                            <asp:ListItem Value="2">不通過</asp:ListItem>
                        </asp:RadioButtonList>
                        
                    </td>
                    <th style="width:15%">
                        審核事由
                    </th>
                    <td>
                        <asp:TextBox ID="tbox_reason" runat="server" Height="75px" Width="225px" 
                            TextMode="MultiLine"></asp:TextBox>
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
            <asp:Button ID="Button1" runat="server" CssClass="a-input" OnClientClick="self.parent.tb_remove()"
                UseSubmitBehavior="false" Text="取消" />
        </div>
    </div>
    </form>
</body>
</html>
