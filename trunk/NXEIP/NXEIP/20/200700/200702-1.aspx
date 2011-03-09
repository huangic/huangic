<%@ Page Language="C#" AutoEventWireup="true" CodeFile="200702-1.aspx.cs" Inherits="_20_200700_200702_1" %>

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
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
        OldValuesParameterFormatString="original_{0}" SelectMethod="Get_qatype" 
        TypeName="NXEIP.DAO._200702DAO">
        <SelectParameters>
            <asp:Parameter DefaultValue="1" Name="self" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <uc2:Navigator ID="Navigator1" runat="server" SysFuncNo="200701" />
    <div class="tableDiv">
        <asp:HiddenField ID="hidden_no" runat="server" />
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
                        發問者單位
                    </th>
                    <td>
                        <asp:Label ID="lab_depname" runat="server"></asp:Label>
                    </td>
                    <th style="width:15%">
                        發問者
                    </th>
                    <td>
                        <asp:Label ID="lab_peoname" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th style="width:15%">
                        發問日期
                    </th>
                    <td colspan="3">
                        <asp:Label ID="lab_date" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th style="width:15%">
                        問題類別
                    </th>
                    <td colspan="3">
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:RadioButton ID="rb_self" runat="server" Text="其它類別" Checked="True" GroupName="G1" />
                                    &nbsp;
                                    <asp:DropDownList ID="ddl_self" runat="server" DataSourceID="ObjectDataSource1" 
                                        DataTextField="qat_name" DataValueField="qat_no">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:RadioButton ID="rb_s06no" runat="server" Text="業務資訊類" GroupName="G1" />
                                    &nbsp;
                                    <asp:DropDownList ID="ddl_sysfun" runat="server">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:RadioButton ID="rb_r05no" runat="server" Text="維修類" GroupName="G1" />
                                    &nbsp;
                                    <asp:DropDownList ID="ddl_r05" runat="server">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <th>
                        發問問題
                    </th>
                    <td colspan="3">
                        <asp:TextBox ID="tbox_question" runat="server" Width="295px" Height="85px" 
                            MaxLength="200" TextMode="MultiLine"></asp:TextBox>
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
                UseSubmitBehavior="false" />
        </div>
    </div>
    </form>
</body>
</html>
