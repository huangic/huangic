<%@ Page Language="C#" AutoEventWireup="true" CodeFile="300501-1.aspx.cs" Inherits="_30_300500_300501_1" EnableEventValidation="false" %>

<%@ Register src="../../lib/CssLayout.ascx" tagname="CssLayout" tagprefix="uc1" %>

<%@ Register src="../../lib/Navigator.ascx" tagname="Navigator" tagprefix="uc2" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

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
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetOpenData" TypeName="NXEIP.DAO.SysfuctionDAO"></asp:ObjectDataSource>
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <uc2:Navigator ID="Navigator1" runat="server" SysFuncNo="300501" />
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
                    系統名稱
                </th>
                <td>
                    <asp:DropDownList ID="ddl_sysfun" runat="server" DataSourceID="ObjectDataSource1"
                        DataTextField="sfu_name" DataValueField="sfu_no">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th style="width: 15%">
                    上層類別分類
                </th>
                <td>
                    <asp:DropDownList ID="ddl_parent" runat="server" AppendDataBoundItems="True">
                        <asp:ListItem Value="0">無</asp:ListItem>
                    </asp:DropDownList>
                    <asp:CascadingDropDown ID="ddl_parent_CascadingDropDown" runat="server" Enabled="True"
                        ParentControlID="ddl_sysfun" ServiceMethod="GetDropDownContents2" TargetControlID="ddl_parent"
                        UseContextKey="True" LoadingText="載入中" Category="sysfun" PromptText="無" 
                        PromptValue="0">
                    </asp:CascadingDropDown>
                </td>
            </tr>
            <tr>
                <th style="width: 15%">
                    類別分類名稱
                </th>
                <td>
                    <asp:TextBox ID="tbox_name" runat="server" MaxLength="50" Width="150px"></asp:TextBox>
                </td>
            </tr>
        </table>
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
