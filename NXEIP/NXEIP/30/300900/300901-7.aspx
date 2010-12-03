<%@ Page Language="C#" AutoEventWireup="true" CodeFile="300901-7.aspx.cs" Inherits="_30_300900_300901_7" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../../lib/CssLayout.ascx" TagName="CssLayout" TagPrefix="uc1" %>
<%@ Register Src="../../lib/Navigator.ascx" TagName="Navigator" TagPrefix="uc2" %>
<%@ Register src="../../lib/tree/DepartTreeTextBox.ascx" tagname="DepartTreeTextBox" tagprefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <uc1:CssLayout ID="CssLayout1" runat="server" />
     <script type="text/javascript" src="../../js/jquery-1.4.2.min.js"></script>
     <script type="text/javascript" src="../../js/thickbox.js"></script>
    <script type="text/javascript">
       

        function pageLoad(sender, args) {
            if (args.get_isPartialLoad()) {
                //  reapply the thick box stuff
                tb_init('a.thickbox');
            }
        }
    
    
    </script>

</head>
<body>
    <form id="form1" runat="server">
    
    
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    
    <uc2:Navigator ID="Navigator1" runat="server" SysFuncNo="300901" />
    
    
    
    
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

       <asp:HiddenField ID="hidden_id" runat="server" />
       <asp:HiddenField ID="hidden_uid" runat="server" />
         <table>
            <tbody>
                <tr>
                    <th>
                        欄位名稱
                    </th>
                    <td>
                        <asp:TextBox ID="tb_name" runat="server" MaxLength="100"></asp:TextBox>
                    </td>
                   
                </tr>
                 <tr>
                    <th>
                        欄位說明
                    </th>
                    <td>
                        <asp:TextBox ID="tb_description" runat="server" MaxLength="255" 
                            TextMode="MultiLine"></asp:TextBox>
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
            <asp:Button ID="btn_countinue" runat="server" CssClass="b-input" Text="繼續新增" 
                onclick="btn_countinue_Click" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btn_cancel" runat="server" CssClass="a-input" Text="取消" OnClientClick="self.parent.update()"
                UseSubmitBehavior="False" />
        </div>
    </div>
    </form>
</body>
</html>
