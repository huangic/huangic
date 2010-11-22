<%@ Page Language="C#" AutoEventWireup="true" CodeFile="100202-3.aspx.cs" Inherits="_10_100200_100202_3"   ValidateRequest="false"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../../lib/CssLayout.ascx" TagName="CssLayout" TagPrefix="uc1" %>
<%@ Register Src="../../lib/Navigator.ascx" TagName="Navigator" TagPrefix="uc2" %>

<%@ Register src="../../lib/FileUpload.ascx" tagname="FileUpload" tagprefix="uc3" %>
<%@ Register src="../../lib/SWFUpload/UC_SWFUpload.ascx" tagname="UC_SWFUpload" tagprefix="uc4" %>

<%@ Register src="../../lib/calendar.ascx" tagname="calendar" tagprefix="uc5" %>

<%@ Register src="../../lib/tree/DepartTreeListBox.ascx" tagname="DepartTreeListBox" tagprefix="uc6" %>

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
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:HiddenField ID="hidden_tde_no" runat="server" />
    <uc2:Navigator ID="Navigator1" runat="server" SysFuncNo="100202" SubFunc="回報待辦事項" />
    
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
            <tbody>
                <tr>
                    <th>
                        工作名稱
                    </th>
                    <td>
                        <asp:Label ID="lb_name" runat="server"></asp:Label>
                    </td>
                  


                </tr>
                <tr>
                    <th>
                        工作進度
                    </th>
                    <td>
                        
                        <asp:DropDownList ID="DropDownList1" runat="server">
                            <asp:ListItem  Selected="True">0</asp:ListItem>
                            <asp:ListItem>10</asp:ListItem>
                            <asp:ListItem>20</asp:ListItem>
                            <asp:ListItem>30</asp:ListItem>
                            <asp:ListItem>40</asp:ListItem>
                            <asp:ListItem>50</asp:ListItem>
                            <asp:ListItem>60</asp:ListItem>
                            <asp:ListItem>70</asp:ListItem>
                            <asp:ListItem>80</asp:ListItem>
                            <asp:ListItem>90</asp:ListItem>
                            <asp:ListItem>100</asp:ListItem>
                        </asp:DropDownList>
                        %(前次進度<asp:Label ID="lb_achievedd" runat="server"></asp:Label>
                        %)</td>
                   

                </tr>
                <tr>
                    <th>
                        執行情況說明
                    </th>
                    <td >
                        <asp:TextBox ID="tb_work" runat="server" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                 
                         
                 
                 
                 
                 <tr>
                    
                    <th>
                        參考文件<br/><asp:Label ID="lb_size" runat="server" Text="Label"></asp:Label>
                    </th>
                    
                    
                    <td >
                        檔案說明:<asp:TextBox ID="tb_file1" runat="server"></asp:TextBox><asp:FileUpload ID="FileUpload1" runat="server" /><br/>
                        檔案說明:<asp:TextBox ID="tb_file2" runat="server"></asp:TextBox><asp:FileUpload ID="FileUpload2" runat="server" /><br/>
                        檔案說明:<asp:TextBox ID="tb_file3" runat="server"></asp:TextBox><asp:FileUpload ID="FileUpload3" runat="server" /><br/>
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
        <div class="bottom">
            <asp:Button ID="btn_ok" runat="server" CssClass="b-input" Text="確定" 
                onclick="btn_ok_Click" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btn_complete" runat="server" CssClass="b-input" Text="完成進度" onclick="btn_complete_Click" 
                />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="Button1" runat="server" CssClass="a-input" 
                onclick="Button1_Click" Text="取消" />
        </div>
    </div>
    </form>
</body>
</html>
