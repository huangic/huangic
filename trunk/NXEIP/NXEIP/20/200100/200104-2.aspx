<%@ Page Language="C#" AutoEventWireup="true" CodeFile="200104-2.aspx.cs" Inherits="_20_200100_200104_2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../../lib/CssLayout.ascx" TagName="CssLayout" TagPrefix="uc1" %>
<%@ Register Src="../../lib/Navigator.ascx" TagName="Navigator" TagPrefix="uc2" %>

<%@ Register src="../../lib/FileUpload.ascx" tagname="FileUpload" tagprefix="uc3" %>
<%@ Register src="../../lib/SWFUpload/UC_SWFUpload.ascx" tagname="UC_SWFUpload" tagprefix="uc4" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <uc1:CssLayout ID="CssLayout1" runat="server" />
</head>
<body>
    <form id="form1" runat="server">
   
    <uc2:Navigator ID="Navigator1" runat="server" SysFuncNo="200104" SubFunc="新增公文附件" />
    
   <div class="tableDiv">
        <asp:HiddenField ID="hidden_arg_no" runat="server" />
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
                        發文單位
                    </th>
                    <td>
                                               
                        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                                               
                    </td>
                     <th>
                        建檔人員
                    </th>
                    <td>
                                               
                        <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
                                               
                    </td>


                </tr>
                <tr>
                    <th>
                        發文字號
                    </th>
                    <td>
                        <asp:TextBox ID="tb_number" runat="server"></asp:TextBox>
                    </td>
                    <th>
                        建檔人員電話
                    </th>
                    <td>
                        電話：<asp:TextBox ID="tb_tel" runat="server"></asp:TextBox><br />
                        分機：<asp:TextBox ID="tb_ext" runat="server"></asp:TextBox>
                    </td>

                </tr>
                <tr>
                    <th>
                        是否開放非公務機關查詢
                    </th>
                    <td colspan="3">
                        <asp:RadioButtonList ID="RadioButtonList1" runat="server" BorderStyle="None" 
                            RepeatDirection="Horizontal" RepeatLayout="Flow">
                            <asp:ListItem Value="2">是</asp:ListItem>
                            <asp:ListItem Value="1" Selected="True">否</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                 <tr>
                    <th>
                        附件<br/><asp:Label ID="lb_size" runat="server" Text="Label"></asp:Label>
                    </th>
                    <td colspan="3">
                        
                        <uc4:UC_SWFUpload ID="UC_SWFUpload1" runat="server" />
                        
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
            <asp:Button ID="btn_ok" runat="server" CssClass="b-input" Text="確定" 
                onclick="btn_ok_Click" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="Button1" runat="server" CssClass="a-input" 
                onclick="Button1_Click" Text="取消" />
        </div>
    </div>
    </form>
</body>
</html>
