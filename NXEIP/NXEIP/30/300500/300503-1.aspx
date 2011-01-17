<%@ Page Language="C#" AutoEventWireup="true" CodeFile="300503-1.aspx.cs" Inherits="_30_300500_300503_1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../../lib/CssLayout.ascx" TagName="CssLayout" TagPrefix="uc1" %>
<%@ Register Src="../../lib/Navigator.ascx" TagName="Navigator" TagPrefix="uc2" %>

<%@ Register src="../../lib/FileUpload.ascx" tagname="FileUpload" tagprefix="uc3" %>
<%@ Register src="../../lib/SWFUpload/UC_SWFUpload.ascx" tagname="UC_SWFUpload" tagprefix="uc4" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <uc1:CssLayout ID="CssLayout1" runat="server" />
</head>
<body>
    <form id="form1" runat="server">
   
    <asp:ObjectDataSource ID="ObjectDataSource_sys" runat="server" 
        OldValuesParameterFormatString="original_{0}" SelectMethod="GetS06FromSufNO" 
        TypeName="NXEIP.DAO.Sys06DAO">
        <SelectParameters>
            <asp:Parameter DefaultValue="200103" Name="suf_no" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
   
    <uc2:Navigator ID="Navigator1" runat="server" SysFuncNo="300503" SubFunc="新增公務電話" />
    
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
                
                <th>所屬單位</th>

                <td>
                    <asp:DropDownList ID="DropDownList1" runat="server" 
                        DataSourceID="ObjectDataSource_sys" DataTextField="s06_name" 
                        DataValueField="s06_no">
                    </asp:DropDownList>
                
                </td>
                </tr>


               
                 <tr>
                    <th>
                        附件<br/><asp:Label ID="lb_size" runat="server" Text="Label"></asp:Label>
                    </th>
                    <td >
                        
                        <uc4:UC_SWFUpload ID="UC_SWFUpload1" runat="server" />
                        
                    </td>
                </tr>
                 <tr>
                    <th>
                        備註
                    </th>
                    <td >
                        
                         <asp:TextBox ID="tb_memo" runat="server"  TextMode="MultiLine"></asp:TextBox>
                        
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
