<%@ Page Language="C#" AutoEventWireup="true" CodeFile="200601-11.aspx.cs" Inherits="_20_200600_200601_11"
    MasterPageFile="~/MasterPage.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="MattBerseth.WebControls" Namespace="MattBerseth.WebControls"
    TagPrefix="cc1" %>
<%@ Register Src="~/lib/Navigator.ascx" TagName="Navigator" TagPrefix="uc1" %>
<%@ Register src="../../lib/calendar.ascx" tagname="calendar" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function update(msg) {

            __doPostBack('<%=UpdatePanel1.ClientID%>', '');
            tb_remove();


            if (msg != undefined && msg!="") {

                alert(msg);
            }
        }

        function pageLoad(sender, args) {
            if (args.get_isPartialLoad()) {
                //  reapply the thick box stuff
                //tb_init('a.thickbox');
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
  
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" EnablePaging="True" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTao03" TypeName="NXEIP.DAO._200601_9DAO" 
        SelectCountMethod="GetTao03Count">
        <SelectParameters>
            <asp:Parameter Name="tao_no" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <uc1:Navigator ID="Navigator1" runat="server" SysFuncNo="200601" SubFunc="討論區文章查詢" />
    <div class="tableDiv">
        
            
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
            <div class="talkn">
       
          
            <table class="back">
          <tbody>
            
            <tr>
              <td class="bg1_title" colspan="2">內容查詢</td>
              </tr>
            <tr>
              <td class="bg1">關鍵字</td>
              <td class="bg2">
                <asp:TextBox ID="tb_keyword" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
              <td class="bg1"><label for="ctl00_ContentPlaceHolder1_txtNN" id="ctl00_ContentPlaceHolder1_lblNN">查詢欄位</label></td>
              <td class="bg2">
              
                    <asp:RadioButtonList ID="rb_option" runat="server" 
                        RepeatDirection="Horizontal" RepeatLayout="Flow">
                        <asp:ListItem Selected="True" Value="1">討論主題</asp:ListItem>
                        <asp:ListItem Value="2">討論內容</asp:ListItem>
                        <asp:ListItem Value="3">附件名稱</asp:ListItem>
                    </asp:RadioButtonList>
                         
              
             
            </tr>
            
            <tr>
              <td class="bg1">

               <asp:CheckBox runat="server" ID="timeFlag" Text="查詢回應日期" />
               
           
              </td>
              <td class="bg2">
                 <ul>
                 <li>自 </li>
                 <li>
                     <uc2:calendar ID="sdate" runat="server" />
                     </li>
                 <li> 至 </li>
                 <li><uc2:calendar ID="edate" runat="server" /></li>
                 </ul>
                </td>
              
            </tr>
            
            <tr>
              <td colspan="2">
                 <div class="bg_">
                 <div class="b0">
                 <li class="b7">
                 <a href="200601.aspx">討論區總表</a></li>
                 <li class="b7"><a href="200601-2.aspx?tao_no=<%=Request["tao_no"] %>">回討論區</a></li>
                 <li class="b7">
                    <asp:LinkButton ID="LinkButton1" runat="server">開始查詢</asp:LinkButton>
                 </li>
                 </div> </div>  </td>
            </tr>
          </tbody>
          </table>
       
      </div>

      </ContentTemplate>

            </asp:UpdatePanel>
            
        
    </div>
 
</asp:Content>
