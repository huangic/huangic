<%@ Page Language="C#" AutoEventWireup="true" CodeFile="200107-2.aspx.cs" Inherits="_20_200100_200107_2" EnableEventValidation="false" %>

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
   
    <uc2:Navigator ID="Navigator1" runat="server" SysFuncNo="200107" SubFunc="新增檔案" />
    
   <div class="tableDiv">
        <asp:HiddenField ID="hidden_arg_no" runat="server" />
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
            OldValuesParameterFormatString="original_{0}" SelectMethod="GetS06FromSufNO" 
            TypeName="NXEIP.DAO.Sys06DAO">
            <SelectParameters>
                <asp:Parameter DefaultValue="200107" Name="suf_no" Type="Int32" />
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
                        上傳單位
                    </th>
                    <td>
                                               
                        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                                               
                    </td>
                     <th>
                        上傳人員
                    </th>
                    <td>
                                               
                        <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
                                               
                    </td>


                </tr>
                <tr>
                    <th>
                        檔案類別
                    </th>
                    <td>
                        <asp:DropDownList ID="ddl_cat" runat="server" DataSourceID="ObjectDataSource1" 
                            DataTextField="s06_name" DataValueField="s06_no">
                        </asp:DropDownList>
                        子類別<asp:DropDownList ID="ddl_childcat" runat="server">
                        </asp:DropDownList>
                        <asp:CascadingDropDown ID="ddl_childcat_CascadingDropDown" runat="server" 
                            Enabled="True" ServiceMethod="GetDropDownContents" 
                            TargetControlID="ddl_childcat" UseContextKey="True" 
                            ParentControlID="ddl_cat" Category="child" PromptText="請選擇">
                        </asp:CascadingDropDown>
                    </td>
                   <th>
                        適用單位
                    </th>
                    <td colspan="3">
                        <asp:RadioButtonList ID="RadioButtonList2" runat="server" BorderStyle="None" 
                            RepeatDirection="Horizontal" RepeatLayout="Flow">
                            
                            <asp:ListItem Value="1" Selected="True">全部</asp:ListItem>
                            <asp:ListItem Value="2">單位</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>

                </tr>
               <tr>
                    <th>
                        說明
                    </th>
                    <td colspan="3">
                        <asp:TextBox ID="tb_note" runat="server" TextMode="MultiLine" MaxLength="100"></asp:TextBox>
                       
                        
                    </td>
                </tr>
                 <tr>
                    <th>
                        附件<br/><asp:Label ID="lb_size" runat="server" Text="Label"></asp:Label>
                    </th>
                    <td colspan="3">
                        
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
