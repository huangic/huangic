<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="350205.aspx.cs" Inherits="_35_350200_350205" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../../lib/calendar.ascx" TagName="calendar" TagPrefix="uc2" %>
<%@ Register Src="../../lib/FileUpload.ascx" TagName="FileUpload" TagPrefix="uc3" %>
<%@ Register Src="../../lib/Navigator.ascx" TagName="Navigator" TagPrefix="uc4" %>
<%@ Register src="../../lib/tree/DepartTreeListBox.ascx" tagname="DepartTreeListBox" tagprefix="uc5" %>
<%@ Register src="../../lib/tree/DepartTreeTextBox.ascx" tagname="DepartTreeTextBox" tagprefix="uc6" %>
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
        SelectCommand="SELECT typ_cname, typ_number,typ_no FROM types WHERE (typ_status = '1') AND (typ_code = 'work') ORDER BY typ_order">
    </asp:SqlDataSource>
    <uc4:Navigator ID="Navigator1" runat="server" SysFuncNo="350205" />
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
                    <span class="a-letter-Red">* </span>�����Ҧr��
                </th>
                <td>
                    <asp:TextBox ID="tbox_cardid" runat="server"></asp:TextBox>
                </td>
                <th>
                    <span class="a-letter-Red">* </span>�m�W
                </th>
                <td colspan="2">
                    <asp:TextBox ID="tbox_name" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>
                    <span class="a-letter-Red">* </span>���u�b��
                </th>
                <td>
                    <asp:TextBox ID="tbox_workid" runat="server"></asp:TextBox>
                </td>
                <th>
                    �H�ƽs��
                </th>
                <td colspan="2">
                    <asp:TextBox ID="tbox_account" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>
                    <span class="a-letter-Red">* </span>�A�ȳ��
                </th>
                <td>
                    <uc6:DepartTreeTextBox ID="DepartTreeTextBox1" runat="server" />
                </td>
                <th>
                    �ӤH�Ӥ�
                </th>
                <td colspan="2">
                    <uc3:FileUpload ID="FileUpload1" runat="server" />
                </td>
            </tr>
            <tr>
                <th>
                    <span class="a-letter-Red">* </span>¾��
                </th>
                <td>
                    <asp:DropDownList ID="ddl_profess" runat="server" DataSourceID="ODS_profess" DataTextField="typ_cname"
                        DataValueField="typ_no">
                    </asp:DropDownList>
                </td>
                <th>
                    <span class="a-letter-Red">* </span>�H�����O
                </th>
                <td colspan="2">
                    <asp:DropDownList ID="ddl_ptype" runat="server" DataSourceID="ODS_ptype" DataTextField="typ_cname"
                        DataValueField="typ_no">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <th>
                    <span class="a-letter-Red">* </span>��¾���
                </th>
                <td>
                    <uc2:calendar ID="calendar1" runat="server" />
                </td>
                <th>
                    �ͤ�
                </th>
                <td colspan="2">
                    <uc2:calendar ID="calendar2" runat="server" />
                </td>
            </tr>
            <tr>
                <th>
                    �s���a�}
                </th>
                <td>
                    <asp:TextBox ID="tbox_addr" runat="server" Width="250px"></asp:TextBox>
                </td>
                <th>
                    �s���q��
                </th>
                <td colspan="2">
                    <asp:TextBox ID="tbox_tel" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>
                    �q�l�l��
                </th>
                <td>
                    <asp:TextBox ID="tbox_mail" runat="server" Width="250px"></asp:TextBox>
                </td>
                <th>
                    �줽�ǹq��
                </th>
                <td colspan="2">
                    <asp:TextBox ID="tbox_otel" runat="server"></asp:TextBox>
                    <span class="a-letter-2">�����G</span><asp:TextBox ID="tbox_extension" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th>
                    �b¾���p
                </th>
                <td>
                    <asp:DropDownList ID="ddl_jobtype" runat="server" AutoPostBack="True" DataSourceID="SqlDataSource1"
                        DataTextField="typ_cname" DataValueField="typ_no" OnSelectedIndexChanged="ddl_jobtype_SelectedIndexChanged">
                    </asp:DropDownList>
                    &nbsp;&nbsp;
                    <uc2:calendar ID="calendar3" runat="server" />
                </td>
                <th>
                    ���u�Ƶ�
                </th>
                <td colspan="2">
                    <asp:TextBox ID="tbox_memo" runat="server"></asp:TextBox>
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
            <asp:Button ID="Button1" runat="server" CssClass="b-input" Text="�T�w" OnClick="Button1_Click" />
            &nbsp;&nbsp;&nbsp;
            <asp:Button ID="Button2" runat="server" CssClass="a-input" Text="����" OnClick="Button2_Click" />
        </div>
        <div id="calendarDiv">
        </div>
    </div>
</asp:Content>
