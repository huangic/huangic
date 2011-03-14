<%@ Page Language="C#" AutoEventWireup="true" CodeFile="200802-1.aspx.cs" Inherits="_20_200800_200802_1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../../lib/CssLayout.ascx" TagName="CssLayout" TagPrefix="uc1" %>
<%@ Register Src="../../lib/Navigator.ascx" TagName="Navigator" TagPrefix="uc2" %>

<%@ Register src="../../lib/FileUpload.ascx" tagname="FileUpload" tagprefix="uc3" %>
<%@ Register src="../../lib/SWFUpload/UC_SWFUpload.ascx" tagname="UC_SWFUpload" tagprefix="uc4" %>

<%@ Register src="../../lib/tree/DepartTreeTextBox.ascx" tagname="DepartTreeTextBox" tagprefix="uc5" %>

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
   
    <uc2:Navigator ID="Navigator1" runat="server" SysFuncNo="200802" SubFunc="新增未婚同仁" />
    
    
    
   
    

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
                                               
                       
                                               
                        <uc5:DepartTreeTextBox ID="DepartTreeTextBox1" runat="server" 
                            LeafType="People" />
                                               
                       
                                               
                    </td>
                     <th>
                        性別
                    </th>
                    <td>
                                               
                        
                                               
                        <asp:RadioButtonList ID="RadioButtonList1" runat="server" 
                            RepeatDirection="Horizontal" RepeatLayout="Flow">
                            <asp:ListItem Value="1" Selected>男</asp:ListItem>
                            <asp:ListItem Value="2">女</asp:ListItem>
                        </asp:RadioButtonList>
                                               
                        
                                               
                    </td>


                </tr>
                <!--
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
                -->
                <tr>
                    <th>
                       身高
                    </th>
                    <td class="style1" >
                       
                        <asp:TextBox ID="tb_height" runat="server"></asp:TextBox>
                       
                    </td>
                     <th>
                       體重
                    </th>
                    <td >
                       
                        <asp:TextBox ID="tb_wight" runat="server"></asp:TextBox>
                       
                    </td>
                </tr>
                  <tr>
                    <th>
                       年齡
                    </th>
                    <td class="style1" >
                       
                        <asp:TextBox ID="tb_age" runat="server"></asp:TextBox>
                       
                    </td>
                     <th>
                       顯示順序
                    </th>
                    <td >
                       
                        <asp:TextBox ID="tb_order" runat="server">0</asp:TextBox>
                       
                    </td>
                </tr>
                 
                  <tr>
                    <th>
                       學歷
                    </th>
                    <td class="style1" >
                       
                        <asp:TextBox ID="tb_school" runat="server" Height="93px" MaxLength="200" 
                            TextMode="MultiLine" Width="222px"></asp:TextBox>
                       
                    </td>
                     <th>
                       興趣
                    </th>
                    <td >
                       
                        <asp:TextBox ID="tb_interest" runat="server" Height="93px" MaxLength="200" 
                            TextMode="MultiLine" Width="222px"></asp:TextBox>
                       
                    </td>
                </tr>
                  <tr>
                    <th>
                       聯絡方式
                    </th>
                    <td colspan="3">
                       
                        <asp:TextBox ID="tb_contact" runat="server" Width="490px"></asp:TextBox>
                       
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
                       
                        <asp:TextBox ID="tb_condition" runat="server" Height="93px" MaxLength="200" 
                            TextMode="MultiLine" Width="222px"></asp:TextBox>
                       
                    </td>
                     <th>
                       自我介紹<br>(200字以內)
                    </th>
                    <td >
                       
                        <asp:TextBox ID="tb_introduce" runat="server" Height="93px" MaxLength="200" 
                            TextMode="MultiLine" Width="222px"></asp:TextBox>
                       
                    </td>
                </tr>


                 
                 <tr>
                    <th>
                        個人相片<br/><asp:Label ID="lb_size" runat="server" Text="Label"></asp:Label>
                    </th>
                    <td colspan="3">
                        
                        <asp:Image ID="Image1" Height="100" Width="150" runat="server" Visible="false" />
                        
                        <uc4:UC_SWFUpload ID="UC_SWFUpload1" runat="server" />
                        
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
            <asp:Button ID="Button1" runat="server" CssClass="a-input" 
                onclick="Button1_Click" Text="取消" />
        </div>
    </div>
    
    
    
    
    </form>
</body>
</html>
