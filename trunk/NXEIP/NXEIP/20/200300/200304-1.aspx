<%@ Page Language="C#" AutoEventWireup="true" CodeFile="200304-1.aspx.cs" Inherits="_20_200300_200304_1" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../../lib/CssLayout.ascx" TagName="CssLayout" TagPrefix="uc1" %>
<%@ Register Src="../../lib/Navigator.ascx" TagName="Navigator" TagPrefix="uc2" %>

<%@ Register src="../../lib/FileUpload.ascx" tagname="FileUpload" tagprefix="uc3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <uc1:CssLayout ID="CssLayout1" runat="server" />
</head>
<body>
    <form id="form1" runat="server">
   
    <uc2:Navigator ID="Navigator1" runat="server" SysFuncNo="200304" SubFunc="新增商品" />
    
   <div class="tableDiv">
        <asp:HiddenField ID="hidden_arg_no" runat="server" />
       <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" CombineScripts="false">
       </asp:ToolkitScriptManager >
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
            OldValuesParameterFormatString="original_{0}" SelectMethod="GetS06FromSufNO" 
            TypeName="NXEIP.DAO.Sys06DAO">
            <SelectParameters>
                <asp:Parameter DefaultValue="200304" Name="suf_no" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
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
                        上傳人員
                    </th>
                    <td>
                                               
                        <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
                                               
                    </td>


                </tr>
                <tr>
                    <th>
                        類別
                    </th>
                    <td>
                        <asp:DropDownList ID="ddl_cat" runat="server" DataSourceID="ObjectDataSource1" 
                            DataTextField="s06_name" DataValueField="s06_no">
                        </asp:DropDownList>
                       
                    </td>
                                     

                </tr>
               <tr>
                    <th>
                        名稱
                    </th>
                    <td>
                        <asp:TextBox ID="tb_name" runat="server" MaxLength="40"></asp:TextBox>
                       
                        
                    </td>
                </tr>    
                
                 <tr>
                    <th>
                        價格
                    </th>
                    <td>
                        <asp:TextBox ID="tb_price" runat="server"></asp:TextBox>
                       
                        
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
