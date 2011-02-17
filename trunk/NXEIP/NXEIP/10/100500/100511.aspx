<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="100511.aspx.cs" Inherits="_10_100500_100511" EnableEventValidation="false" %>

<%@ Register Src="../../lib/Navigator.ascx" TagName="Navigator" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="MattBerseth.WebControls" Namespace="MattBerseth.WebControls"
    TagPrefix="cc1" %>
<%@ Register Src="../../lib/people/PeopleDetail.ascx" TagName="PeopleDetail" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function update(msg) {

            __doPostBack('<%=UpdatePanel1.ClientID%>', '');
            tb_remove();


            //alert(msg);
        }

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
    


    


    


    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
        OldValuesParameterFormatString="original_{0}" SelectMethod="GetLayoutDir" 
        TypeName="NXEIP.DAO._100511DAO"></asp:ObjectDataSource>
    


    


    


    <uc1:Navigator ID="Navigator1" runat="server" SysFuncNo="100511" />
   
   
   
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    

  
  <div class="tableDiv">
    <div class="photo">
    <div class="select">
         <div class="b6"></div>
        <div class="b6"></div>
        <li> <span class="a-title">請選擇個人版型</span></li>
    </div>
          <div class="box">
           
           
      <asp:ListView ID="ListView1" runat="server" DataSourceID="ObjectDataSource1" 
                  onitemcommand="ListView1_ItemCommand" DataKeyNames="Name">
      
        <ItemTemplate>
                <div class="layout_content">
              <div class="layout1">
                
                    <asp:Image ID="Image1" runat="server" ImageUrl='<%# String.Format("~/style/{0}/layout.gif",Eval("Name")) %>'/>
              
              </div>
                   <div class="ps5">
                     
                        <asp:Label ID="Label1" runat="server" Text='<%# Eval("Name") %>'></asp:Label> 
                        <br> 
                        </br>           
                    <asp:Button ID="Button1" runat="server" Text="設為預設版型"  CommandName="Layout"/>
                </div>
              </div>



        </ItemTemplate>
      
      
      </asp:ListView>
                                 
      </div>
          
    </div>
  </div>


    
    
    

        </ContentTemplate>


    </asp:UpdatePanel>

</asp:Content>
