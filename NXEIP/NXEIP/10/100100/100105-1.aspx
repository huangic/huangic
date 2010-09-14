<%@ Page Language="C#" AutoEventWireup="true" CodeFile="100105-1.aspx.cs" Inherits="_10_100100_100105_1" %>

<%@ Register src="../../lib/SWFUpload/UC_SWFUpload.ascx" tagname="UC_SWFUpload" tagprefix="uc1" %>

<%@ Register src="../../lib/CssLayout.ascx" tagname="CssLayout" tagprefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <uc2:CssLayout ID="CssLayout1" runat="server" />
</head>
<body>
    <form id="form1" runat="server">
    <div class="tableDiv">
         <div class="header">
                    <div class="h1">
                    </div>
                    <div class="h2">
                        <div class="name">檔案上傳</div>
                    </div>
                    <div class="h3">
                    </div>
                </div>


        <div align="center">
        目前目錄:<asp:Literal ID="path" runat="server"></asp:Literal>
        <uc1:UC_SWFUpload ID="UC_SWFUpload1" runat="server" />
        
        
        
        
        </div>
            <div class="footer">
                    <div class="f1">
                    </div>
                    <div class="f2">
                    </div>
                    <div class="f3">
                    </div>
                </div>

            <div align="center">
                
            
                <asp:Button ID="Button1" runat="server" CssClass="b-input" 
                    onclick="Button1_Click" Text="確定" />
                <asp:Button ID="Button2" runat="server" CssClass="a-input" 
                    onclick="Button2_Click" Text="取消" />
                
            
             </div>
    </div>
    </form>
</body>
</html>
