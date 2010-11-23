<%@ Page Language="C#" AutoEventWireup="true" CodeFile="300502-1.aspx.cs" Inherits="_30_300500_300502_1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../../lib/CssLayout.ascx" TagName="CssLayout" TagPrefix="uc1" %>
<%@ Register Src="../../lib/Navigator.ascx" TagName="Navigator" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <uc1:CssLayout ID="CssLayout1" runat="server" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:ObjectDataSource ID="DepartmentDataSource_spot" runat="server" SelectMethod="GetFloorsSpot"
        TypeName="NXEIP.DAO._200303DAO" 
        OldValuesParameterFormatString="original_{0}"></asp:ObjectDataSource>
    <asp:HiddenField ID="hidden_flo_no" runat="server" />
    <uc2:Navigator ID="Navigator1" runat="server" SysFuncNo="300502" />
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
                    <th>
                        所在地
                    </th>
                    <td>
                        <asp:DropDownList ID="ddl_spot" runat="server" DataSourceID="DepartmentDataSource_spot"
                            DataTextField="spo_name" DataValueField="spo_no">
                        </asp:DropDownList>
                        <br />
                    </td>
                   
                </tr>
                 <tr>
                    <th>
                        樓層
                    </th>
                    <td>
                        <asp:TextBox ID="tbx_flo_level" runat="server"></asp:TextBox>請輸入數字樓層 地下一樓請輸入B1
                    </td>
                   
                </tr>

                <tr>
                    <th>
                        部門中文名稱
                    </th>
                    <td>
                        <asp:TextBox ID="tbx_flo_name" runat="server"></asp:TextBox>
                    </td>
                   
                </tr>
               
                <tr>
                    <th>
                         部門英文名稱
                    </th>
                    <td>
                        <asp:TextBox ID="tbx_flo_ename" runat="server"></asp:TextBox>
                    </td>
                   
                   
                </tr>
                <tr>
                    <th>
                        排序編號
                    </th>
                     <td>
                        <asp:TextBox ID="tbx_flo_order" runat="server"></asp:TextBox>
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
                UseSubmitBehavior="False" />
        </div>
    </div>
    </form>
</body>
</html>
