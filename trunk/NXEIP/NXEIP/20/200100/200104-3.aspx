<%@ Page Language="C#" AutoEventWireup="true" CodeFile="200104-3.aspx.cs" Inherits="_20_200100_200104_3" %>

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
      <script type="text/javascript" src="../../js/jquery-1.4.2.min.js"></script>
     <script type="text/javascript">
         function file_delete(d06, d07) {
             if (confirm("確定要刪除?")) {
                 //ajax
                 $.ajaxSetup({ "cache": false });
                 $.ajax({});

             }
        };
     
     </script>

</head>
<body>
    
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:HiddenField ID="hidden_d06_no" runat="server" />
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
        OldValuesParameterFormatString="original_{0}" SelectMethod="GetAllWithDoc06No" 
        TypeName="NXEIP.DAO.Doc07DAO">
        <SelectParameters>
            <asp:QueryStringParameter DefaultValue="" Name="doc06_no" QueryStringField="id" 
                Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <uc2:Navigator ID="Navigator1" runat="server" SysFuncNo="200104" SubFunc="修改公文附件" />
   
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
                        發文單位
                    </th>
                    <td>
                                               
                        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                                               
                    </td>
                     <th>
                        建檔人員
                    </th>
                    <td>
                                               
                        <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
                                               
                    </td>


                </tr>
                <tr>
                    <th>
                        發文字號
                    </th>
                    <td>
                        <asp:TextBox ID="tb_number" runat="server"></asp:TextBox>
                    </td>
                    <th>
                        建檔人員電話
                    </th>
                    <td>
                        電話：<asp:TextBox ID="tb_tel" runat="server"></asp:TextBox><br />
                        分機：<asp:TextBox ID="tb_ext" runat="server"></asp:TextBox>
                    </td>

                </tr>
                <tr>
                    <th>
                        是否開放非公務機關查詢
                    </th>
                    <td colspan="3">
                        <asp:RadioButtonList ID="RadioButtonList1" runat="server" BorderStyle="None" 
                            RepeatDirection="Horizontal" RepeatLayout="Flow">
                            <asp:ListItem Value="2">是</asp:ListItem>
                            <asp:ListItem Value="1" Selected="True">否</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                 <tr>
                    <th>
                        附件<br/><asp:Label ID="lb_size" runat="server" Text="Label"></asp:Label>
                    </th>
                    <td colspan="3">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <asp:ListView ID="ListView1" runat="server" DataSourceID="ObjectDataSource1" 
                            DataKeyNames="d06_no,d07_no" onitemcommand="ListView1_ItemCommand">
                            <ItemTemplate>
                                <li><asp:Label ID="Label3" runat="server" Text='<%# Eval("d07_file") %>'></asp:Label>
                                    <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick=" return confirm('確定要刪除?')" CssClass="imageButton delete"   CommandName="del" ><span>刪除</span></asp:LinkButton>
                                </li>
                            </ItemTemplate>
                          
                        </asp:ListView>

                            
                            </ContentTemplate>
                        </asp:UpdatePanel>


                        
                        
                        <br />
                        
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
