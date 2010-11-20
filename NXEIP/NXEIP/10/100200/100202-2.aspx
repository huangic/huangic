<%@ Page Language="C#" AutoEventWireup="true" CodeFile="100202-2.aspx.cs" Inherits="_10_100200_100202_2"   ValidateRequest="false"%>

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

    <uc2:Navigator ID="Navigator1" runat="server" SysFuncNo="100202" SubFunc="新增待辦事項" />
    
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
                      <asp:TextBox ID="tb_name" runat="server"></asp:TextBox>
                    </td>
                  


                </tr>
                <tr>
                    <th>
                        工作期間
                    </th>
                    <td>
                        
                        <uc5:calendar ID="calendar1" runat="server" />
                        至<uc5:calendar ID="calendar2" runat="server" />
                        
                    </td>
                   

                </tr>
                <tr>
                    <th>
                       工作說明
                    </th>
                    <td >
                        <asp:TextBox ID="tb_work" runat="server" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                 
                 <tr>
                    <th>
                       執行者
                    </th>
                    <td>
                       
                     
                        <asp:RadioButtonList ID="RadioButtonList1" runat="server" 
                            RepeatDirection="Horizontal" RepeatLayout="Flow" 
                            
                            Width="100px">
                            <asp:ListItem Value="0" Selected>待辦</asp:ListItem>
                            <asp:ListItem Value="1">交辦</asp:ListItem>
                        </asp:RadioButtonList>
                        <uc6:DepartTreeListBox ID="DepartTreeListBox1" runat="server"  Visible="True" 
                            LeafType="People"/>
                    

                    </td>
                 </tr>               
                 
                 
                 
                 <tr>
                    
                    <th>
                        參考文件<br/><asp:Label ID="lb_size" runat="server" Text="Label"></asp:Label>
                    </th>
                    
                    
                    <td >
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                        <asp:Panel ID="Panel_upload" runat="server" >
                        
                        <uc4:UC_SWFUpload ID="UC_SWFUpload1" runat="server" />
                            <asp:Panel ID="Panel1" runat="server" HorizontalAlign="Center">
                           <asp:Button ID="Button2" runat="server" Text="儲存" 
                                onclick="Button2_Click" CssClass="b-input" />
                           
                            </asp:Panel>
                            

                        </asp:Panel>
                        
                        <asp:Panel ID="Panel_uploaded" runat="server" Visible="false">
                            <asp:GridView ID="GridView1"  CssClass="tableData" runat="server" AutoGenerateColumns="False" 
                                BorderStyle="None" GridLines="None" CellSpacing="-1">
                                
                                <Columns>
                                    <asp:TemplateField HeaderText="說明">
                                      <ItemTemplate>
                                          <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                      </ItemTemplate>
                                    
                                    
                                        <ItemStyle Width="30%" />
                                    
                                    
                                    </asp:TemplateField>
                                    <asp:BoundField  HeaderText="檔名" DataField="FileName"/>
                                </Columns>
                                
                            </asp:GridView>
                        </asp:Panel>
                        </ContentTemplate>
                        </asp:UpdatePanel>
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
