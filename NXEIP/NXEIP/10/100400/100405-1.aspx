<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="100405-1.aspx.cs" Inherits="_10_100400_100405_1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register Src="../../lib/Navigator.ascx" TagName="Navigator" TagPrefix="uc1" %>
<%@ Register src="../../lib/calendar.ascx" tagname="calendar" tagprefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <ajaxtoolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajaxtoolkit:ToolkitScriptManager>
    <script type="text/javascript" src="../../js/lytebox.js"></script>
    <uc1:Navigator ID="Navigator1" runat="server" SysFuncNo="100405" />
    <div class="tableDiv">
        <div class="header">
            <div class="h1">
            </div>
            <div class="h2">
                <div class="name">
                    設備借用-申請</div>
            </div>
            <div class="h3">
            </div>
        </div>
        <table>
            <tbody>
                <tr>
                    <th style="width: 13%">
                        <span class="a-letter-Red">* </span>申請日期
                    </th>
                    <td style="width: 37%">
                        <asp:Label ID="lab_today" runat="server"></asp:Label>
                        (<asp:Label ID="lab_week" runat="server"></asp:Label>) 登記時段<asp:Label ID="lab_stime"
                            runat="server"></asp:Label>
                        ，使用時數：
                        <asp:DropDownList ID="ddl_usehour" runat="server">
                            <asp:ListItem Value="0">請選擇</asp:ListItem>
                        </asp:DropDownList>
                        <asp:Label ID="lab_etime" runat="server" Visible="False"></asp:Label>
                    </td>
                    <th style="width: 13%">
                        所在地
                    </th>
                    <td style="width: 37%">
                        <asp:Label ID="lab_sponame" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th>
                        登記者
                    </th>
                    <td>
                        <asp:Label ID="lab_applyuser" runat="server"></asp:Label>
                    </td>
                    <th>
                        資產編號
                    </th>
                    <td>
                        <asp:Label ID="lab_number" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th>
                        使用狀況
                    </th>
                    <td>
                        <asp:Label ID="lab_apply" runat="server">可申請</asp:Label>
                    </td>
                    <th>
                        設備
                    </th>
                    <td>
                        <asp:Label ID="lab_equname" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th>
                        借用單位
                    </th>
                    <td>
                        <asp:Label ID="lab_depart" runat="server"></asp:Label>
                    </td>
                    <th>
                        保管人
                    </th>
                    <td>
                        <asp:Label ID="lab_peouid" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th>
                        <span class="a-letter-Red">* </span>登記者電話
                    </th>
                    <td>
                        <asp:TextBox ID="txt_tel" runat="server" Columns="20"></asp:TextBox>
                    </td>
                    <th>
                        <div align="right">
                            <div align="right">
                                保管人電話與分機</div>
                        </div>
                    </th>
                    <td>
                        <asp:Label ID="lab_tel" runat="server"></asp:Label>
                        <asp:Label ID="lab_ext" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th rowspan="3">
                        <span class="a-letter-Red">* </span>申請事由
                    </th>
                    <td rowspan="3">
                        <asp:TextBox ID="txt_reason" runat="server" Columns="40" Rows="5" TextMode="MultiLine"></asp:TextBox>
                    </td>
                    <th>
                        可使用時間
                    </th>
                    <td>
                        <asp:Label ID="lab_usetime" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th>
                        設備描述
                    </th>
                    <td>
                        <asp:Label ID="lab_descript" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th>
                        設備圖片
                    </th>
                    <td>
                        <ul>
                            <li class="p1">
                                <asp:HyperLink ID="hl_pic1" runat="server" CssClass="row_schedule">設備圖片</asp:HyperLink></li>
                        </ul>
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
            <asp:Button ID="btn_apply" runat="server" CssClass="b-input" Text="確定申請" OnClick="btn_apply_Click" />&nbsp;&nbsp;
            <asp:Button ID="btn_delete" runat="server" CssClass="b-input" Text="取消申請" OnClick="btn_delete_Click" />&nbsp;&nbsp;
            <asp:Button ID="btn_goback" runat="server" CssClass="a-input" Text="回上一頁" OnClick="btn_goback_Click" />
        </div>
        <div id="div_msg" runat="server">
        </div>
        <asp:Label ID="lab_no" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lab_spot" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lab_equ" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lab_BorrowsSignType" runat="server" Visible="False"></asp:Label>
    </div>
</asp:Content>
