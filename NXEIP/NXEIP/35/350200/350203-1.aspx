<%@ Page Language="C#" AutoEventWireup="true" CodeFile="350203-1.aspx.cs" Inherits="_35_350200_350203_1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="~/css/eip.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
        <asp:ObjectDataSource ID="DepartmentDataSourceDDL" runat="server" 
            SelectMethod="GetAll" TypeName="NXEIP.DAO.DepartmentsDAO">
        </asp:ObjectDataSource>
        <asp:HiddenField ID="hidden_dep_no" runat="server" />
                <table width="600px" cellspacing="20" cellpadding="0" border="0" bgcolor="White">
                    <tbody>
                        <tr>
                            <td valign="top" height="22">
                                <table width="100%" cellspacing="0" cellpadding="0" border="0">
                                    <tbody>
                                        <tr>
                                            <td width="17">
                                                <img width="17" height="22" src="../../image/b01.gif">
                                            </td>
                                            <td background="../../image/b01-1.gif" class="b01">
                                                帳號管理 / 人員管理 /<strong> 部門管理</strong>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tbody>
                                        <tr>
                                            <td width="17">
                                                <img height="29" src="../../image/b02.gif" width="17"></img>
                                            </td>
                                            <td background="../../image/b02-1.gif" class="a02-15">
                                                <asp:Label ID="lab_headermode" runat="server"></asp:Label>
                                                部門資料
                                            </td>
                                            <td background="../../image/b02-1.gif">
                                                <div align="right">
                                                </div>
                                            </td>
                                            <td width="17">
                                                <img height="29" src="../../image/b02-2.gif" width="17"></img>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                <table bgcolor="#ffffff" border="0" cellpadding="3" cellspacing="3" width="100%">
                                    <tbody>
                                        <tr>
                                            <td bgcolor="#eeeeee" class="a-letter-2" width="100">
                                                <div align="right">
                                                    上層部門
                                                </div>
                                            </td>
                                            <td bgcolor="#eeeeee" class="a-letter-1">
                                                <asp:DropDownList ID="ddl_depart" runat="server" DataSourceID="DepartmentDataSourceDDL"
                                                    DataTextField="dep_name" DataValueField="dep_no">
                                                </asp:DropDownList>
                                                <br />
                                            </td>
                                            <td bgcolor="#eeeeee" class="a-letter-2" width="100">
                                                <div align="right">
                                                    部門代號
                                                </div>
                                            </td>
                                            <td bgcolor="#eeeeee" class="a-letter-1">
                                                <asp:TextBox ID="tbx_dep_code" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td bgcolor="#eeeeee" class="a-letter-2">
                                                <div align="right">
                                                    部門中文名稱
                                                </div>
                                            </td>
                                            <td bgcolor="#eeeeee" class="a-letter-1">
                                                <asp:TextBox ID="tbx_dep_name" runat="server"></asp:TextBox>
                                            </td>
                                            <td bgcolor="#eeeeee" class="a-letter-2">
                                                <div align="right">
                                                    部門英文名稱
                                                </div>
                                            </td>
                                            <td bgcolor="#eeeeee" class="a-letter-1">
                                                <asp:TextBox ID="tbx_dep_ename" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td bgcolor="#eeeeee" class="a-letter-2">
                                                <div align="right">
                                                    電話</div>
                                            </td>
                                            <td bgcolor="#eeeeee" class="a-letter-1">
                                                <asp:TextBox ID="tbx_dep_tel" runat="server"></asp:TextBox>
                                            </td>
                                            <td bgcolor="#eeeeee" class="a-letter-2">
                                                <div align="right">
                                                    傳真
                                                </div>
                                            </td>
                                            <td bgcolor="#eeeeee" class="a-letter-1">
                                                <asp:TextBox ID="tbx_dep_fax" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td bgcolor="#eeeeee" class="a-letter-2">
                                                <div align="right">
                                                    地址</div>
                                            </td>
                                            <td bgcolor="#eeeeee" class="a-letter-1">
                                                <asp:TextBox ID="tbx_dep_addr" runat="server"></asp:TextBox>
                                            </td>
                                            <td bgcolor="#eeeeee" class="a-letter-2">
                                                <div align="right">
                                                    排序編號</div>
                                            </td>
                                            <td bgcolor="#eeeeee" class="a-letter-1">
                                                <asp:TextBox ID="tbx_dep_order" runat="server"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td bgcolor="#eeeeee" class="a-letter-2">
                                                <div align="right">
                                                    簡介</div>
                                            </td>
                                            <td bgcolor="#eeeeee" class="a-letter-1" colspan="3">
                                                <asp:TextBox ID="tbx_dep_note" runat="server" Height="100px" TextMode="MultiLine"
                                                    Width="370px"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                <table border="0" cellpadding="0" cellspacing="10" width="100%" bgcolor="White">
                                    <tbody>
                                        <tr>
                                            <td>
                                                <div align="center">
                                                    <asp:Button ID="btn_ok" runat="server" CssClass="b-input" Text="確定" OnClick="btn_ok_Click" />
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:Button ID="btn_cancel" runat="server" CssClass="a-input" Text="取消" OnClientClick="self.parent.tb_remove()" UseSubmitBehavior="False" />
                                                    &nbsp;</div>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                    </tbody>
                </table>
    </div>
    </form>
</body>
</html>
