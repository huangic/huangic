<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="350206-1.aspx.cs"
    Inherits="_35_350200_350201_1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../../lib/CssLayout.ascx" TagName="CssLayout" TagPrefix="uc1" %>
<%@ Register Src="../../lib/Navigator.ascx" TagName="Navigator" TagPrefix="uc2" %>
<%@ Register src="../../lib/tree/DepartTreeTextBox.ascx" tagname="DepartTreeTextBox" tagprefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <uc1:CssLayout ID="CssLayout1" runat="server" />

    <script type="text/javascript" src="../../js/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="../../js/thickbox.js"></script>

     <script type="text/javascript">



         function pageLoad(sender, args) {
             if (args.get_isPartialLoad()) {
                 //  reapply the thick box stuff
                 tb_init('a.thickbox,input.thickbox');
             }
         }
    
    
    </script>


</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <uc2:Navigator ID="Navigator1" runat="server" SysFuncNo="350201" />
    <div class="tableDiv">
       <div class="header">
            <div class="h1">
            </div>
            <div class="h2">
            </div>
            <div class="h3">
            </div>
        </div>
        
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
              <table>
            <tbody>
                <tr>
                    <th>
                        管理類別
                    </th>
                    <td>
                        <asp:RadioButtonList ID="RadioButtonList1" runat="server"  
                            RepeatDirection="Horizontal" RepeatLayout="Flow" 
                            onselectedindexchanged="RadioButtonList1_SelectedIndexChanged" AutoPostBack="True">
                            <asp:ListItem Selected="True" Value="1">單位管理</asp:ListItem>
                            <asp:ListItem Value="2">總管理</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <th>
                        選擇單位
                    </th>
                    <td>
                        
                        <uc3:DepartTreeTextBox ID="DepartTreeTextBox1" runat="server" />
                        
                    </td>
                </tr>
                <tr>
                    <th>
                        選擇人員
                    </th>
                    <td>
                        
                        <uc3:DepartTreeTextBox ID="peopleTreeTextBox2" runat="server" 
                            LeafType="People" />
                        
                    </td>
                </tr>
               
            </tbody>
        </table>
            </ContentTemplate>

        </asp:UpdatePanel>
        
        
      
        <div class="footer">
            <div class="f1">
            </div>
            <div class="f2">
            </div>
            <div class="f3">
            </div>
        </div>
        <div class="bottom">
            <asp:Button ID="btn_ok" runat="server" CssClass="b-input" Text="確定" OnClick="btn_ok_Click" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btn_cancel" runat="server" CssClass="a-input" Text="取消" OnClientClick="self.parent.tb_remove()"
                UseSubmitBehavior="false" />
        </div>
    </div>
    </form>
</body>
</html>
