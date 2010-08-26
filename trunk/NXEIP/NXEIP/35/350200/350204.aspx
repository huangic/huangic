﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="350204.aspx.cs" Inherits="_35_350200_350204" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register Src="../../lib/tree/jQueryDepartTree.ascx" TagName="jQueryDepartTree"
    TagPrefix="uc1" %>
<%@ Register Src="../../lib/tree/jQueryPeopleTree.ascx" TagName="jQueryPeopleTree"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
        SelectMethod="GetAll" TypeName="NXEIP.DAO.TypesDAO">
        <SelectParameters>
            <asp:Parameter DefaultValue="work" Name="type_code" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" 
        SelectMethod="GetAll" TypeName="NXEIP.DAO.TypesDAO">
        <SelectParameters>
            <asp:Parameter DefaultValue="ptype" Name="type_code" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjectDataSource3" runat="server" 
        SelectMethod="GetAll" TypeName="NXEIP.DAO.TypesDAO">
        <SelectParameters>
            <asp:Parameter DefaultValue="profess" Name="type_code" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <table width="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF">
        <tr>
            <td>
                <!-- InstanceBeginEditable name="EditRegion3" -->
                <table width="100%" height="500" border="0" cellpadding="0" cellspacing="20">
                    <tr>
                        <td height="22" valign="top">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td width="17">
                                        <img src="../../image/b01.gif" width="17" height="22" />
                                    </td>
                                    <td background="../../image/b01-1.gif" class="b01">
                                        帳號管理 / 人員管理 /<strong> 人事資料查修</strong>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td class="leftheaderbg">
                                        &nbsp;
                                    </td>
                                    <td class="a02-15 headerbg">
                                        人事資料查修
                                    </td>
                                    <td class="rightheaderbg">
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                            <table width="100%" border="0" cellpadding="3" cellspacing="3" bgcolor="#FFFFFF">
                                <tr>
                                    <td width="100" align="right" bgcolor="#eeeeee" class="a-letter-2">
                                        &nbsp;<asp:CheckBox ID="cbox_work" 
                                            runat="server" />
                                    </td>
                                    <td align="left" bgcolor="#EEEEEE" class="a-letter-1">
                                        在職狀況
                                        <asp:DropDownList ID="ddl_work" runat="server" DataSourceID="ObjectDataSource1" 
                                            DataTextField="typ_cname" DataValueField="typ_no">
                                        </asp:DropDownList>
                                    </td>
                                    <td width="100" align="right" bgcolor="#eeeeee" class="a-letter-2">
                                        <asp:CheckBox ID="cbox_name" 
                                            runat="server" />
                                    </td>
                                    <td align="left" bgcolor="#EEEEEE" class="a-letter-1">
                                        姓名&nbsp; 
                                        <asp:TextBox ID="tbox_name" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" bgcolor="#EEEEEE" class="a-letter-2">
                                        &nbsp;<asp:CheckBox ID="cbox_workid" 
                                            runat="server" />
                                    </td>
                                    <td align="left" bgcolor="#EEEEEE" class="a-letter-1">
                                        人事編號&nbsp; 
                                        <asp:TextBox ID="tbox_workid" runat="server"></asp:TextBox>
                                    </td>
                                    <td align="right" bgcolor="#EEEEEE" class="a-letter-2">
                                        &nbsp;<asp:CheckBox ID="cbox_account" 
                                            runat="server" />
                                    </td>
                                    <td align="left" bgcolor="#EEEEEE" class="a-letter-1">
                                        員工帳號&nbsp; 
                                        <asp:TextBox ID="tbox_account" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" bgcolor="#EEEEEE" class="a-letter-2">
                                        &nbsp;<asp:CheckBox 
                                            ID="cbox_ptype" runat="server" />
                                    </td>
                                    <td align="left" bgcolor="#EEEEEE" class="a-letter-1">
                                        人員類別
                                        <asp:DropDownList ID="ddl_ptype" runat="server" 
                                            DataSourceID="ObjectDataSource2" DataTextField="typ_cname" 
                                            DataValueField="typ_no">
                                        </asp:DropDownList>
                                    </td>
                                    <td align="right" bgcolor="#EEEEEE" class="a-letter-2">
                                        &nbsp;<asp:CheckBox ID="cbox_profess" 
                                            runat="server" />
                                    </td>
                                    <td align="left" bgcolor="#EEEEEE" class="a-letter-1">
                                        職稱
                                        <asp:DropDownList ID="ddl_profess" runat="server" 
                                            DataSourceID="ObjectDataSource3" DataTextField="typ_cname" 
                                            DataValueField="typ_no">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" bgcolor="#EEEEEE" class="a-letter-2">
                                        &nbsp;<asp:CheckBox ID="cbox_dearp" 
                                            runat="server" />
                                    </td>
                                    <td align="left" bgcolor="#EEEEEE" class="a-letter-1">
                                        <table border="0" cellpadding="0" cellspacing="3">
                                            <tr>
                                                <td>
                                                    請選擇部門
                                                </td>
                                                <td>
                                                    <uc1:jQueryDepartTree ID="jQueryDepartTree1" runat="server" />
                                                    &nbsp;
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td align="right" bgcolor="#EEEEEE" class="a-letter-2">
                                        &nbsp;<asp:CheckBox ID="cbox_people" 
                                            runat="server" />
                                    </td>
                                    <td align="left" bgcolor="#EEEEEE" class="a-letter-1">
                                        <table border="0" cellpadding="0" cellspacing="3">
                                            <tr>
                                                <td>
                                                    請選擇人員
                                                </td>
                                                <td>
                                                    <uc2:jQueryPeopleTree ID="jQueryPeopleTree1" runat="server" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                            <table width="100%" border="0" cellspacing="10" cellpadding="0">
                                <tr>
                                    <td align="center">
                                        <asp:Button ID="Button1" runat="server" CssClass="b-input" Text="確定" 
                                            onclick="Button1_Click" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="Button2" runat="server" CssClass="a-input" Text="取消" 
                                            onclick="Button2_Click" />
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <!-- InstanceEndEditable -->
            </td>
        </tr>
    </table>
</asp:Content>
