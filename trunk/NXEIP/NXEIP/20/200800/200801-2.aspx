<%@ Page Language="C#" AutoEventWireup="true" CodeFile="200801-2.aspx.cs" Inherits="_20_200800_200802_1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../../lib/CssLayout.ascx" TagName="CssLayout" TagPrefix="uc1" %>
<%@ Register Src="../../lib/Navigator.ascx" TagName="Navigator" TagPrefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>
   
    </title>
     <uc1:CssLayout ID="CssLayout1" runat="server" />
    <style type="text/css">
        .style1
        {
            width: 200px;
        }
    </style>

    <script type="text/javascript" src="../../js/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="../../js/thickbox.js"></script>

</head>
<body>
    <form id="form1" runat="server">
   
    <uc2:Navigator ID="Navigator1" runat="server" SysFuncNo="200801"  />
    
    
    
   
    

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
                        姓名
                    </th>
                    <td class="style1">
                                               
                                       
                        <asp:Label ID="lb_name" runat="server"/>
                                               
                       
                                               
                    </td>
                     <th>
                        性別
                    </th>
                    <td>
                                               
                                                                
                       <asp:Label ID="lb_sex" runat="server"/>
                                               
                        
                                               
                    </td>


                </tr>
                
                <tr>
                
                     
                     <th>
                        服務機關
                    </th>
                    <td class="style1">
                        
                        <asp:Label ID="lb_dep" runat="server"></asp:Label>
                        
                    </td>
                    <th>
                        職稱
                    </th>
                    <td>
                        
                        <asp:Label ID="lb_title" runat="server"></asp:Label>
                        
                    </td>
                 
                    

                </tr>
              
                <tr>
                    <th>
                       身高
                    </th>
                    <td class="style1" >
                       
                       <asp:Label ID="lb_height" runat="server"></asp:Label>
                       
                    </td>
                     <th>
                       體重
                    </th>
                    <td >
                       <asp:Label ID="lb_weight" runat="server"></asp:Label>
                       
                    </td>
                </tr>
                  <tr>
                    <th>
                       年齡
                    </th>
                    <td  colspan="3" class="style1" >
                       
                        <asp:Label ID="lb_age" runat="server"></asp:Label>
                       
                    </td>
                   
                </tr>
                 
                  <tr>
                    <th>
                       學歷
                    </th>
                    <td class="style1" >
                       
                       <asp:Label ID="lb_school" runat="server"></asp:Label>
                       
                    </td>
                     <th>
                       興趣
                    </th>
                    <td >
                       
                        <asp:Label ID="lb_interest" runat="server"></asp:Label>
                       
                    </td>
                </tr>
                  <tr>
                    <th>
                       聯絡方式
                    </th>
                    <td colspan="3">
                       
                        <asp:Label ID="lb_contact" runat="server"></asp:Label>
                       
                    </td>
                   
                </tr>

                <tr>
                    <!--
                    <th>
                       居住地
                    </th>
                    <td colspan="3">
                       
                    </td>
                   -->
                </tr>
                 

                                   <tr>
                    <th>
                       徵友條件
                       <br>(200字以內)
                    </th>
                    <td class="style1" >
                       
                       <asp:Label ID="lb_condition" runat="server"></asp:Label>
                       
                       
                    </td>
                     <th>
                       自我介紹<br>(200字以內)
                    </th>
                    <td >
                       <asp:Label ID="lb_introduce" runat="server"></asp:Label>
                       
                       
                    </td>
                </tr>


                 
                 <tr>
                    <th>
                        個人相片<br/></asp:Label>
                    </th>
                    <td colspan="3">
                        
                        <asp:Image ID="Image1" Height="200" Width="300" runat="server" Visible="false" />
                                          
                        
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
    
    
    
    
    </form>
</body>
</html>
