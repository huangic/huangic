<%@ Page Language="C#" AutoEventWireup="true" CodeFile="100601-2.aspx.cs" Inherits="_10_100600_100601_2" %>

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
<uc1:Navigator ID="Navigator1" runat="server" SysFuncNo="100601" SubFunc="回覆出席" />
    <asp:HiddenField ID="hidd_meeno" runat="server" />
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
                        開會事由
                    </th>
                    <td >
                       <asp:Label ID="lab_reason" runat="server"></asp:Label>
                    </td>
                    <th style="width:15%">
                        開會地點
                    </th>
                    <td >
                       <asp:Label ID="lab_place" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th style="width:15%">
                        開會日期
                    </th>
                    <td >
                       <asp:Label ID="lab_date" runat="server"></asp:Label>
                    </td>
                    <th style="width:15%">
                        會議主持人
                    </th>
                    <td >
                       <asp:Label ID="lab_host" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th style="width:15%">
                        會議聯絡人
                    </th>
                    <td>
                        <asp:Label ID="lab_peoname" runat="server"></asp:Label>
                    </td>
                    <th style="width:15%">
                        聯絡人電話
                    </th>
                    <td>
                        <asp:Label ID="lab_tel" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th style="width:15%">
                        是否出席會議
                    </th>
                    <td>
                        
                        <asp:RadioButtonList ID="rbl_status" runat="server" 
                            RepeatDirection="Horizontal" RepeatLayout="Flow">
                            <asp:ListItem Selected="True" Value="2">出席</asp:ListItem>
                            <asp:ListItem Value="3">不出席</asp:ListItem>
                        </asp:RadioButtonList>
                        
                    </td>
                    <th style="width:15%">
                        回覆事由
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
