<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="350204-2.aspx.cs" Inherits="_35_350200_350204_2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../../lib/calendar.ascx" TagName="calendar" TagPrefix="uc2" %>
<%@ Register Src="../../lib/FileUpload.ascx" TagName="FileUpload" TagPrefix="uc3" %>
<%@ Register Src="../../lib/Navigator.ascx" TagName="Navigator" TagPrefix="uc4" %>
<%@ Register src="../../lib/tree/DepartTreeTextBox.ascx" tagname="DepartTreeTextBox" tagprefix="uc5" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:ObjectDataSource ID="ODS_profess" runat="server" SelectMethod="GetAll" TypeName="NXEIP.DAO.TypesDAO">
        <SelectParameters>
            <asp:Parameter Name="type_code" Type="String" DefaultValue="profess" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ODS_ptype" runat="server" SelectMethod="GetAll" TypeName="NXEIP.DAO.TypesDAO">
        <SelectParameters>
            <asp:Parameter DefaultValue="ptype" Name="type_code" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:NXEIPConnectionString %>"
        SelectCommand="SELECT typ_cname, typ_no FROM types WHERE (typ_status = '1') AND (typ_code = 'work') ORDER BY typ_order">
    </asp:SqlDataSource>
    <uc4:Navigator ID="Navigator1" runat="server" SysFuncNo="350204" />
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
                <th>
                    <span class="a-letter-Red">* </span>身分證字號
                </th>
                <td>
                    <asp:TextBox ID="tbox_cardid" runat="server"></asp:TextBox>
                </td>
                <th>
                    <span class="a-letter-Red">* </span>姓名
                </th>
                <td colspan="2">
                    <asp:TextBox ID="tbox_name" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>
                    <span class="a-letter-Red">* </span>員工帳號
                </th>
                <td>
                    <asp:TextBox ID="tbox_account" runat="server"></asp:TextBox>
                </td>
                <th>
                    <span class="a-letter-Red">* </span>人事編號
                </th>
                <td colspan="2">
                    <asp:TextBox ID="tbox_workid" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>
                    <span class="a-letter-Red">* </span>服務單位
                </th>
                <td>
                    <uc5:DepartTreeTextBox ID="DepartTreeTextBox1" runat="server" />
                </td>
                <th>
                    個人照片
                </th>
                <td colspan="2">
                    <uc3:FileUpload ID="FileUpload1" runat="server" />
                </td>
            </tr>
            <tr>
                <th>
                    <span class="a-letter-Red">* </span>職稱
                </th>
                <td>
                    <asp:DropDownList ID="ddl_profess" runat="server" DataSourceID="ODS_profess" DataTextField="typ_cname"
                        DataValueField="typ_no">
                    </asp:DropDownList>
                </td>
                <th>
                    <span class="a-letter-Red">* </span>人員類別
                </th>
                <td colspan="2">
                    <asp:DropDownList ID="ddl_ptype" runat="server" DataSourceID="ODS_ptype" DataTextField="typ_cname"
                        DataValueField="typ_no">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th>
                    <span class="a-letter-Red">* </span>到職日期
                </th>
                <td>
                    <uc2:calendar ID="calendar1" runat="server" />
                </td>
                <th>
                    生日
                </th>
                <td colspan="2">
                    <uc2:calendar ID="calendar2" runat="server" />
                </td>
            </tr>
            <tr>
                <th>
                    連絡地址
                </th>
                <td>
                    <asp:TextBox ID="tbox_addr" runat="server" Width="250px"></asp:TextBox>
                </td>
                <th>
                    連絡電話
                </th>
                <td colspan="2">
                    <asp:TextBox ID="tbox_tel" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>
                    電子郵件
                </th>
                <td>
                    <asp:TextBox ID="tbox_mail" runat="server" Width="250px"></asp:TextBox>
                </td>
                <th>
                    辦公室電話
                </th>
                <td colspan="2">
                    <asp:TextBox ID="tbox_otel" runat="server"></asp:TextBox>
                    <span class="a-letter-2">分機：</span><asp:TextBox ID="tbox_extension" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>
                    在職狀況
                </th>
                <td>
                    <div>
                        <asp:DropDownList ID="ddl_jobtype" runat="server" AutoPostBack="True" DataSourceID="SqlDataSource1"
                            DataTextField="typ_cname" DataValueField="typ_no" OnSelectedIndexChanged="ddl_jobtype_SelectedIndexChanged">
                        </asp:DropDownList>
                        &nbsp;&nbsp;
                        <uc2:calendar ID="calendar3" runat="server" />
                    </div>
                </td>
                <th>
                    員工備註
                </th>
                <td colspan="2">
                    <asp:TextBox ID="tbox_memo" runat="server"></asp:TextBox>
                    <asp:Label ID="lab_filename" runat="server" Visible="False"></asp:Label>
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
            <asp:Button ID="Button1" runat="server" CssClass="b-input" Text="確定" OnClick="Button1_Click" />
            &nbsp;&nbsp;&nbsp;
            <asp:Button ID="Button2" runat="server" CssClass="a-input" Text="取消" OnClick="Button2_Click" />
        </div>
        <div id="div_msg" runat="server">
        </div>
        <div id="calendarDiv">
        </div>
    </div>
</asp:Content>
