<%@ Page Language="C#" AutoEventWireup="true" CodeFile="applys.aspx.cs" Inherits="applyAccount" %>

<%@ Register src="../lib/CssLayout.ascx" tagname="CssLayout" tagprefix="uc1" %>

<%@ Register src="../lib/tree/DepartTreeListBox.ascx" tagname="DepartTreeListBox" tagprefix="uc2" %>

<%@ Register src="../lib/tree/DepartTreeTextBox.ascx" tagname="DepartTreeTextBox" tagprefix="uc3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <uc1:CssLayout ID="CssLayout1" runat="server" />
    <script type="text/javascript" src='../js/jquery-1.4.2.min.js'></script>
    <script type="text/javascript" src='../js/thickbox.js'></script>
    <script type="text/javascript" src="../js/jquery.validate.min.js"></script>
    <script type="text/javascript">
        $(function () {
            //須與form表單ID名稱相同
            $("#form1").validate();
        });
</script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
        OldValuesParameterFormatString="original_{0}" SelectMethod="GetAll" 
        TypeName="NXEIP.DAO.TypesDAO">
        <SelectParameters>
            <asp:Parameter DefaultValue="profess" Name="type_code" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <div class="tableDiv">
        <div class="header">
            <div class="h1">
            </div>
            <div class="h2">
                <div class="name">
                帳號申請表單 
                </div>
                    
            </div>
            <div class="h3">
            </div>
        </div>
        <table>
            <tr>
                <td colspan="4">
                    <span class="a-letter-Red">*</span> 表必填欄位
                </td>
            </tr>
            <tr>
                <th style="width:12%" class="a-letter-2">
                <span class="a-letter-Red">*</span> 身分證字號
                </th>
                <td>
                <asp:TextBox ID="tbox_idcard" runat="server" MaxLength="10" Width="90px" CssClass="required"></asp:TextBox>
                </td>
                <th style="width:12%">
                <span class="a-letter-Red">*</span> 姓名
                </th>
                <td>
                <asp:TextBox ID="tbox_name" runat="server" MaxLength="12" Width="90px" CssClass="required"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th style="width:12%">
                <span class="a-letter-Red">*</span> 登入帳號
                </th>
                <td>
                <asp:TextBox ID="tbox_account" runat="server" MaxLength="12" Width="90px" CssClass="required" minLength="3"></asp:TextBox>
                <span class="a-letter-1">
                    12碼以內數字，英文之字元
                </span>
                </td>
                <th style="width:12%">
                <span class="a-letter-Red">*</span> 登入密碼
                </th>
                <td>
                <asp:TextBox ID="tbox_pass" runat="server" MaxLength="12" Width="110px" 
                        CssClass="required" minLength="4" TextMode="Password"></asp:TextBox>
                <span class="a-letter-1">
                    12碼以內數字，英文及符號之字元
                </span>
                </td>
            </tr>
            <tr>
                <th style="width:12%">
                <span class="a-letter-Red">*</span> 服務單位
                </th>
                <td>
                    
                    <uc3:DepartTreeTextBox ID="DepartTreeTextBox1" runat="server" CssClass="required" />
                </td>
                <th style="width:12%">
                <span class="a-letter-Red">*</span> 職稱
                </th>
                <td>
                    <asp:DropDownList ID="ddl_pro" runat="server" AppendDataBoundItems="True" 
                        DataSourceID="ObjectDataSource1" DataTextField="typ_cname" 
                        DataValueField="typ_no">
                        <asp:ListItem Value="0">請選擇</asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th style="width:12%">
                   公務電話
                </th>
                <td>
                <asp:TextBox ID="tbox_officetel" runat="server" MaxLength="12" Width="90px"></asp:TextBox>
                <span class="a-letter-1">格式: 2991111#1234</span>
                </td>
                <th style="width:12%">
                <span class="a-letter-Red">*</span> 電子郵件
                </th>
                <td>
                <asp:TextBox ID="tbox_mail" runat="server" MaxLength="100" Width="185px" CssClass="required email"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th style="width:12%">
                 手機號碼
                </th>
                <td colspan="3">
                    <asp:TextBox ID="tbox_cellph" runat="server" MaxLength="10" Width="90px" CssClass="digits" minLength="10"></asp:TextBox>
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
            <asp:Button ID="btn_ok" runat="server" CssClass="b-input" Text="確定" OnClick="btn_ok_Click" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btn_cancel" runat="server" CssClass="a-input" Text="取消" OnClientClick="self.parent.tb_remove()"
                UseSubmitBehavior="false" />
        </div>
    </div>
    
    </form>
</body>
</html>
