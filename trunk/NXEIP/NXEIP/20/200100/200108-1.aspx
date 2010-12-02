<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="200108-1.aspx.cs" Inherits="_20_200100_200108_1" %>

<%@ Register Assembly="MattBerseth.WebControls" Namespace="MattBerseth.WebControls"
    TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../../lib/Navigator.ascx" TagName="Navigator" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
      
                

        function pageLoad(sender, args) {
            if (args.get_isPartialLoad()) {
                //  reapply the thick box stuff
                tb_init('a.thickbox');
            }
        }
    
    
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:ObjectDataSource ID="DataSource" runat="server" SelectMethod="GetAll"
        TypeName="NXEIP.DAO.Form01DAO" EnablePaging="True" SelectCountMethod="GetAllCount">
    </asp:ObjectDataSource>
    <uc1:Navigator ID="Navigator1" runat="server" SysFuncNo="200108"  SubFunc="填寫表單"/>
    
    <div class="tableDiv" style="width:80%">
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
                        表單名稱
                    </th>
                    <td>
                                               
                        <asp:Label ID="lb_name" runat="server" Text=""></asp:Label>
                                               
                    </td>
                     <th>
                        承辦人員
                    </th>
                    <td>
                                               
                        <asp:Label ID="lb_people" runat="server" Text=""></asp:Label>
                                               
                    </td>


                </tr>
                <tr>
                    <th>
                        表單說明
                    </th>
                    <td colspan="3">
                        <asp:Label ID="lb_description" runat="server" Text=""></asp:Label>
                    </td>
                   
                    

                </tr>
               
                 
                 


            </tbody>
        </table>
        <br />
        <asp:Table ID="DynamicTable" runat="server" ClientIDMode="Static">
        </asp:Table>

        
                <div class="footer">
                    <div class="f1">
                        
                    </div>
                    <div class="f2">
                    </div>
                    <div class="f3">
                    </div>
                </div>
                
         
          <div class="bottom">
            <asp:Button ID="btn_ok" runat="server" CssClass="b-input" Text="確定" onclick="btn_ok_Click" 
                />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="Button1" runat="server" CssClass="a-input" 
                Text="取消" />
        </div>


    </div>
</asp:Content>
