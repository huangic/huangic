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
 
      




                <div class="nav"><span>帳號管理 / 人員管理 /<strong> 職稱管理 </strong></span></div>


               
                <div class="tableDiv">
                <asp:HiddenField ID="hidden_typ_no" runat="server" />
                
                 <div class="header">
                     <div class="h1"></div>
                         <div class="h2">
                         <div class="name">  <asp:Label ID="lab_mode" runat="server"></asp:Label>職稱管理</div>
                                            
                    </div>
                    <div class="h3"></div>   
                </div>
                
                
                
                
       
                                
                                <table  border="0" cellpadding="3" cellspacing="3" width="100%">
                                    <tbody>
                                        
                                        <tr>
                                            <th>
                                                <div align="right">
                                                    職稱代碼
                                                </div>
                                            </th>
                                            <td>
                                                <asp:TextBox ID="tbx_typ_number" runat="server"></asp:TextBox><span class="a-letter-Red">代碼長度為4位數</span>
                                            </td>
                                            
                                        </tr>
                                        <tr>
                                            <th>
                                                <div align="right">
                                                    職稱中文</div>
                                            </th>
                                            <td>
                                                <asp:TextBox ID="tbx_typ_cname" runat="server"></asp:TextBox>
                                            </td>
                                          
                                        </tr>
                                        <tr>
                                            <th>
                                                <div align="right">
                                                    職稱英文</div>
                                            </th>
                                            <td>
                                                <asp:TextBox ID="tbx_typ_ename" runat="server"></asp:TextBox>
                                            </td>
                                     
                                        </tr>
                                        <tr>
                                            <th>
                                                <div align="right">
                                                    排列順序</div>
                                            </th>
                                            <td>
                                                <asp:TextBox ID="tbx_typ_order" runat="server" ></asp:TextBox>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                               
                              <div class="footer">
                                <div class="f1"></div>
                                <div class="f2"></div>
                                <div class="f3"></div>
                              </div>
                               

                               <div class="bottom">
                                 <asp:Button ID="btn_ok" runat="server" CssClass="b-input" Text="確定" 
                                                       OnClick="btn_ok_Click"/>
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                 <asp:Button ID="btn_cancel" runat="server" CssClass="a-input" Text="取消" OnClientClick="self.parent.tb_remove()"  UseSubmitBehavior="false"/>
                                                    &nbsp;
                               </div>
                               
                             
                                
                                
                                
                           
                </div>
              

</form>
</body>