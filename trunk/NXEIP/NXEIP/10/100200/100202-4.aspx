<%@ Page Language="C#" AutoEventWireup="true" CodeFile="100202-4.aspx.cs" Inherits="_10_100200_100202_4"   ValidateRequest="false"%>

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

    <style type="text/css">
        .style1
        {
            width: 150px;
        }
    </style>

</head>
<body>
    <form id="form1" runat="server">
   
    <asp:ObjectDataSource ID="ObjectDataSource_turning" runat="server" 
        OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataByTreNO" 
        TypeName="NXEIP.DAO.TurningDAO">
        <SelectParameters>
            <asp:Parameter Name="tre_no" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>

    <asp:ObjectDataSource ID="ObjectDataSource_goback" runat="server" 
        OldValuesParameterFormatString="original_{0}" SelectMethod="GetDataByTdeNo" 
        TypeName="NXEIP.DAO.GobackDAO">
        <SelectParameters>
            <asp:Parameter Name="tde_no" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
   
    <uc2:Navigator ID="Navigator1" runat="server" SysFuncNo="100202" SubFunc="詳細內容" />
    
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
                    <th class="style1">
                        工作名稱
                    </th>
                    <td>
                        <asp:Label ID="lb_name" runat="server"></asp:Label>
                    </td>
                  </tr>

                   <tr>
                    <th class="style1">
                        工作期間
                    </th>
                    <td>
                        <asp:Label ID="lb_sdate" runat="server"></asp:Label>~<asp:Label ID="lb_edate" runat="server"></asp:Label>
                    </td>
                 </tr>
                  <tr>
                    <th class="style1">
                        工作期間
                    </th>
                    <td>
                        <asp:Label ID="lb_tre_peo" runat="server" ></asp:Label>
                    </td>
                 </tr>


                <tr>
                    <th class="style1">
                        工作說明
                    </th>
                    <td>
                        
                        <asp:Label ID="lb_work" runat="server"></asp:Label>
                        
                    </td>
                </tr>
                 <tr>
                    
                    <th class="style1">
                        參考文件<br/>
                    </th>
                    
                    
                    <td >
                        <asp:ListView ID="ListView1" runat="server" DataSourceID="ObjectDataSource_turning" 
                            DataKeyNames="tre_no,tur_no">
                            <ItemTemplate>
                                <li>
                                <asp:Label ID="Label3" runat="server" Text='<%# Eval("tur_subject") %>'></asp:Label>
                                <asp:HyperLink ID="HyperLink1" runat="server" CssClass="download imageButton" Target="_blank"
                                                    NavigateUrl='<%#String.Format("100202-1.ashx?id1={0}&id2={1}",Eval("tre_no"),Eval("tur_no"))  %>'><span>下載</span></asp:HyperLink>
                                
                                  
                                </li>
                            </ItemTemplate>
                          
                        </asp:ListView>
                    </td>


                </tr>



                <tr>
                <td colspan="2"></td>
                </tr>
               <tr>
                    <th class="style1">
                        執行人
                    </th>
                    <td>
                        
                        <asp:Label ID="lb_tde_peo" runat="server"></asp:Label>
                        
                    </td>
                   

                </tr>

                  <tr>
                    <th class="style1">
                        工作狀態
                    </th>
                    <td>
                        
                        <asp:Label ID="lb_status" runat="server"></asp:Label>
                        
                    </td>
                   

                </tr>

                <tr>
                    <th class="style1">
                        工作進度
                    </th>
                    <td>
                        
                        <asp:Label ID="lb_achieved" runat="server"></asp:Label>
                        
                    </td>
                   

                </tr>
              
                <tr>
                    <th class="style1">
                        執行情況說明
                    </th>
                    <td >
                        
                        <asp:Label ID="lb_description" runat="server"></asp:Label>
                        
                    </td>
                </tr>
                 
                         
                 
                 
                 
                 <tr>
                    
                    <th class="style1">
                        參考文件<br/>
                    </th>
                    
                    
                    <td >
                        <asp:ListView ID="ListView2" runat="server" DataSourceID="ObjectDataSource_goback" 
                            DataKeyNames="tde_no,gob_no">
                            <ItemTemplate>
                                <li>
                                <asp:Label ID="Label3" runat="server" Text='<%# Eval("gob_subject") %>'></asp:Label>
                                <asp:HyperLink ID="HyperLink1" runat="server" CssClass="download imageButton" Target="_blank"
                                                    NavigateUrl='<%#String.Format("100202-2.ashx?id1={0}&id2={1}",Eval("tde_no"),Eval("gob_no"))  %>'><span>下載</span></asp:HyperLink>
                                
                                  
                                </li>
                            </ItemTemplate>
                          
                        </asp:ListView>
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
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="Button1" runat="server" CssClass="a-input" 
                onclick="Button1_Click" Text="關閉" />
        </div>
    </div>
    </form>
</body>
</html>
