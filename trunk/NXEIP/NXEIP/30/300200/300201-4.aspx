<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="300201-4.aspx.cs" Inherits="_30_300200_300201_4" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="MattBerseth.WebControls" Namespace="MattBerseth.WebControls" TagPrefix="cc1" %>
<%@ Register Src="../../lib/Navigator.ascx" TagName="Navigator" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <uc1:Navigator ID="Navigator1" runat="server" SysFuncNo="300201" />
    <div class="tableDiv">
        <div class="header">
            <div class="h1">
            </div>
            <div class="h2">
                <div class="name">問卷維護 - 題目預覽</div>
                <div class="function">
                    <input type="button" class="thickbox b-input" alt="300201-1.aspx?height=450&width=800&TB_iframe=true&modal=true"value="新增問卷" />
                </div>
            </div>
            <div class="h3">
            </div>
        </div>
        <table>
            <tbody>
                <tr>
                    <td>
                        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="table--1">
                        <tr>
                          <td height="30" align="center"><span class="a02-19">差勤系統使用情形</span></td>
                        </tr>
                      </table>
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
    </div>
</asp:Content>

