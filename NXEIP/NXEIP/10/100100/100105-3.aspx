<%@ Page Language="C#" AutoEventWireup="true" CodeFile="100105-3.aspx.cs" Inherits="_10_100100_100105_3" %>

<%@ Register Assembly="MattBerseth.WebControls" Namespace="MattBerseth.WebControls"
    TagPrefix="cc1" %>
<%@ Register src="../../lib/CssLayout.ascx" tagname="CssLayout" tagprefix="uc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <uc1:CssLayout ID="CssLayout1" runat="server" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
      <asp:UpdatePanel ID="UpdatePanel1" runat="server">
      <ContentTemplate>
    



    <div class="tableDiv">
        <div class="header">
            <div class="h1"></div>
             <div class="h2"></div>
              <div class="h3"></div>
        </div>
      
            <cc1:GridView ID="GridView1" runat="server">
        </cc1:GridView>
            
        
        

         <div class="footer">
            <div class="f1"></div>
             <div class="f2"></div>
              <div class="f3"></div>
        </div>
    
    </div>
    </ContentTemplate>
        </asp:UpdatePanel>
    
    </div>
    </form>
</body>
</html>
