<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="350303.aspx.cs" Inherits="_35_350300_350303" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register Assembly="MattBerseth.WebControls" Namespace="MattBerseth.WebControls" TagPrefix="cc1" %>
<%@ Register Src="../../lib/Navigator.ascx" TagName="Navigator" TagPrefix="uc1" %>
<%@ Register src="../../lib/calendar.ascx" tagname="calendar" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <ajaxtoolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajaxtoolkit:ToolkitScriptManager>
    <script type="text/javascript" src="../../js/lytebox.js"></script>
    <asp:Label ID="lab_pageIndex" runat="server" Visible="False"></asp:Label>
    <uc1:Navigator ID="Navigator1" runat="server" SysFuncNo="350303" />
    <div class="tableDiv">
        <table>
            <tr>
                <td>
                    總計筆數：&nbsp;<asp:Label ID="lab_total" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    已匯筆數：&nbsp;<asp:Label ID="lab_OkCount" runat="server"></asp:Label>
                    
                </td>
            </tr>
            <tr>
                <td>
                    未匯筆數：&nbsp;<asp:Label ID="lab_NoCount" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button 
                        ID="btn_submit" runat="server" CssClass="b-input" Text="依據姓名匯入" 
                        onclick="btn_submit_Click" />
                </td>
            </tr>
            <tr>
              <td>匯入情況：<asp:Label ID="lab_outxt" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
        <div class="bottom">
        </div>
    </div>
</asp:Content>
