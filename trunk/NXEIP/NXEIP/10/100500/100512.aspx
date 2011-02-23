<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="100512.aspx.cs"
    Inherits="_10_100500_100512" %>

<%@ Register Src="../../lib/CssLayout.ascx" TagName="CssLayout" TagPrefix="uc1" %>
<%@ Register Src="../../lib/Navigator.ascx" TagName="Navigator" TagPrefix="uc2" %>
<%@ Register Src="../../lib/calendar.ascx" TagName="calendar" TagPrefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>單位行事曆</title>
    <uc1:CssLayout ID="CssLayout1" runat="server" />
    <script type="text/javascript" src="../../js/jscolor.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <uc2:Navigator ID="Navigator1" runat="server" SysFuncNo="100512" />
    <div class="tableDiv">
        <div class="header">
            <div class="h1">
            </div>
            <div class="h2">
                <div class="name">
                    <asp:Label ID="lab_today" runat="server" Text="100年02月23日 星期三"></asp:Label></div>
            </div>
            <div class="h3">
            </div>
        </div>
        <asp:Table ID="Table1" runat="server" Width="100%" CssClass="calendar-biglist">
        </asp:Table>
        <div id="div_msg" runat="server">
            <asp:Label ID="lab_msg" runat="server"></asp:Label>
        </div>
        <div class="footer">
            <div class="f1">
            </div>
            <div class="f2">
            </div>
            <div class="f3">
            </div>
        </div>
        <div class="bottom">
            <asp:Button ID="btn_cancel" runat="server" CssClass="b-input" Text="關閉視窗" OnClientClick="self.parent.tb_remove()"
                UseSubmitBehavior="false" />
        </div>
    </div>
    </form>
</body>
</html>
