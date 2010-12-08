<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="350204.aspx.cs" Inherits="_35_350200_350204" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../../lib/Navigator.ascx" TagName="Navigator" TagPrefix="uc3" %>
<%@ Register src="../../lib/tree/DepartTreeListBox.ascx" tagname="DepartTreeListBox" tagprefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetAll"
        TypeName="NXEIP.DAO.TypesDAO">
        <SelectParameters>
            <asp:Parameter DefaultValue="work" Name="type_code" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" SelectMethod="GetAll"
        TypeName="NXEIP.DAO.TypesDAO">
        <SelectParameters>
            <asp:Parameter DefaultValue="ptype" Name="type_code" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjectDataSource3" runat="server" SelectMethod="GetAll"
        TypeName="NXEIP.DAO.TypesDAO">
        <SelectParameters>
            <asp:Parameter DefaultValue="profess" Name="type_code" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <uc3:Navigator ID="Navigator1" runat="server" SysFuncNo="350204" />
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
                <td >
                    <asp:CheckBox ID="cbox_work" runat="server" />
                </td>
                <td >
                    在職狀況
                    <asp:DropDownList ID="ddl_work" runat="server" DataSourceID="ObjectDataSource1" DataTextField="typ_cname"
                        DataValueField="typ_no">
                    </asp:DropDownList>
                </td>
                <td >
                    <asp:CheckBox ID="cbox_name" runat="server" />
                </td>
                <td >
                    姓名
                    <asp:TextBox ID="tbox_name" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td >
                    <asp:CheckBox ID="cbox_workid" runat="server" />
                </td>
                <td >
                    人事編號
                    <asp:TextBox ID="tbox_workid" runat="server"></asp:TextBox>
                </td>
                <td >
                    <asp:CheckBox ID="cbox_account" runat="server" />
                </td>
                <td >
                    員工帳號
                    <asp:TextBox ID="tbox_account" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td >
                    <asp:CheckBox ID="cbox_ptype" runat="server" />
                </td>
                <td >
                    人員類別
                    <asp:DropDownList ID="ddl_ptype" runat="server" DataSourceID="ObjectDataSource2"
                        DataTextField="typ_cname" DataValueField="typ_no">
                    </asp:DropDownList>
                </td>
                <td >
                   <asp:CheckBox ID="cbox_profess" runat="server" />
                </td>
                <td >
                    職稱
                    <asp:DropDownList ID="ddl_profess" runat="server" DataSourceID="ObjectDataSource3"
                        DataTextField="typ_cname" DataValueField="typ_no">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td >
                    <asp:CheckBox ID="cbox_dearp" runat="server" />
                </td>
                <td >
                    <table border="0" cellpadding="0" cellspacing="3">
                        <tr>
                            <td>
                                請選擇部門
                            </td>
                            <td>
                                <uc4:DepartTreeListBox ID="DepartTreeListBox_depart" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
                <td >
                   <asp:CheckBox ID="cbox_people" runat="server" />
                </td>
                <td >
                    <table border="0" cellpadding="0" cellspacing="3">
                        <tr>
                            <td>
                                請選擇人員
                            </td>
                            <td>
                                <uc4:DepartTreeListBox ID="DepartTreeListBox_people" runat="server" 
                                    LeafType="People" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <div class="bottom">
            <asp:Button ID="Button1" runat="server" CssClass="b-input" Text="確定" OnClick="Button1_Click" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="Button2" runat="server" CssClass="a-input" Text="取消" OnClick="Button2_Click" />
        </div>
        <div class="footer">
            <div class="f1">
            </div>
            <div class="f2">
            </div>
            <div class="f3">
            </div>
        </div>
    </div>
</asp:Content>
