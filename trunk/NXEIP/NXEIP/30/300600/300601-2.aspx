<%@ Page Language="C#" AutoEventWireup="true" CodeFile="300601-2.aspx.cs" Inherits="_30_300600_300601_2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

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
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" SelectMethod="SearchRep06Parent"
        TypeName="NXEIP.DAO._100403DAO" 
        OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:Parameter Name="r05_no" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjectDataSource3" runat="server" SelectMethod="SearchRep06Son"
        TypeName="NXEIP.DAO._100403DAO" 
        OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:Parameter Name="r06_no" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:HiddenField ID="hidd_r02no" runat="server" />
    <uc2:Navigator ID="Navigator1" runat="server" SubFunc="維修回覆" SysFuncNo="300601" />
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
                    叫修單位
                </th>
                <td>
                    <asp:Label ID="lab_dep" runat="server"></asp:Label>
                </td>
                <th style="width: 15%">
                    叫修人員
                </th>
                <td>
                    <asp:Label ID="lab_people" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th style="width: 15%">
                    地點
                </th>
                <td colspan="3">
                    <asp:Label ID="lab_spot" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th style="width: 15%">
                    故障原因
                </th>
                <td colspan="3">
                    <asp:Label ID="lab_reason" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <th style="width: 15%">
                    問題類別
                </th>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <asp:DropDownList ID="ddl_rep06_par" runat="server" AutoPostBack="True" DataSourceID="ObjectDataSource2"
                                DataTextField="r06_name" DataValueField="r06_no" OnSelectedIndexChanged="ddl_rep06_par_SelectedIndexChanged">
                            </asp:DropDownList>
                            &nbsp;&nbsp;子分類：
                            <asp:DropDownList ID="ddl_rep06_son" runat="server" DataSourceID="ObjectDataSource3"
                                DataTextField="r06_name" DataValueField="r06_no">
                            </asp:DropDownList>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
                <th style="width: 15%">
                    維修人員
                </th>
                <td>
                    <asp:Label ID="lab_replyname" runat="server" ></asp:Label>
                </td>
            </tr>
            <tr>
                <th style="width: 15%">
                    維修回覆
                </th>
                <td colspan="3">
                    <asp:TextBox ID="tbox_reply" runat="server" Height="125px" TextMode="MultiLine" 
                        Width="375px"></asp:TextBox>
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
