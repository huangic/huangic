<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="350201-1.aspx.cs" Inherits="_35_350200_350201_1" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>
        
    
    </title>
      <link href="~/css/eip.css" rel="stylesheet" type="text/css" />
</head>
<body>


 
    <form id="form1" runat="server">
   <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
     </asp:ToolkitScriptManager>
 
      
               
                <div class="AddForm">
                <asp:HiddenField ID="hidden_typ_no" runat="server" />
                <table width="600px" cellspacing="20" cellpadding="0" border="0" bgcolor="White">
                    <tbody>
                        <tr>
                            <td valign="top" height="22">
                                <table width="100%" cellspacing="0" cellpadding="0" border="0">
                                    <tbody>
                                        <tr>
                                            <td width="17">
                                                <img width="17" height="22" src="../../image/b01.gif">
                                            </td>
                                            <td background="../../image/b01-1.gif" class="b01">
                                                帳號管理 / 人員管理 /<strong> 職稱管理 -
                                                    <asp:Label ID="lab_mode" runat="server">新增</asp:Label>職稱資料</strong>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                    <tbody>
                                        <tr>
                                            <td class=leftheaderbg>
                                               
                                            </td>
                                            <td class="a02-15 headerbg">
                                                <asp:Label ID="lab_headermode" runat="server"></asp:Label>
                                                職稱資料
                                            </td>
                                            <td class=headerbg>
                                                <div align="right">
                                                </div>
                                            </td>
                                            <td class="rightheaderbg">
                                                
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                <table bgcolor="#ffffff" border="0" cellpadding="3" cellspacing="3" width="100%">
                                    <tbody>
                                        
                                        <tr>
                                            <td bgcolor="#eeeeee" class="a-letter-2">
                                                <div align="right">
                                                    職稱代碼
                                                </div>
                                            </td>
                                            <td bgcolor="#eeeeee" class="a-letter-1">
                                                <asp:TextBox ID="tbx_typ_number" runat="server"></asp:TextBox><span class="a-letter-Red">代碼長度為4位數</span>
                                            </td>
                                            
                                        </tr>
                                        <tr>
                                            <td bgcolor="#eeeeee" class="a-letter-2">
                                                <div align="right">
                                                    職稱中文</div>
                                            </td>
                                            <td bgcolor="#eeeeee" class="a-letter-1">
                                                <asp:TextBox ID="tbx_typ_cname" runat="server"></asp:TextBox>
                                            </td>
                                          
                                        </tr>
                                        <tr>
                                            <td bgcolor="#eeeeee" class="a-letter-2">
                                                <div align="right">
                                                    職稱英文</div>
                                            </td>
                                            <td bgcolor="#eeeeee" class="a-letter-1">
                                                <asp:TextBox ID="tbx_typ_ename" runat="server"></asp:TextBox>
                                            </td>
                                     
                                        </tr>
                                        <tr>
                                            <td bgcolor="#eeeeee" class="a-letter-2">
                                                <div align="right">
                                                    排列順序</div>
                                            </td>
                                            <td bgcolor="#eeeeee" class="a-letter-1" colspan="3">
                                                <asp:TextBox ID="tbx_typ_order" runat="server" ></asp:TextBox>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                               
                                 <table width="100%" border="0" cellspacing="0" cellpadding="0">
                
                   <tr>
                    <td class="leftfootbg">
                       
                    </td>
                    <td class="footbg">
                        &nbsp;
                    </td>
                    <td class="rightfootbg">
                        
                    </td>
                </tr>
               
            </table>
                               
                               
                                <table border="0" cellpadding="0" cellspacing="10" width="100%" bgcolor="White">
                                    <tbody>
                                        <tr>
                                            <td>
                                                <div align="center">
                                                    <asp:Button ID="btn_ok" runat="server" CssClass="b-input" Text="確定" 
                                                       OnClick="btn_ok_Click"  />
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:Button ID="btn_cancel" runat="server" CssClass="a-input" Text="取消" OnClientClick="self.parent.tb_remove()"  UseSubmitBehavior="false"/>
                                                    &nbsp;</div>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                                
                                
                                
                            </td>
                        </tr>
                    </tbody>
                </table>
                </div>
              

</form>
</body>