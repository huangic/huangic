<%@ Control Language="C#" AutoEventWireup="true" CodeFile="100507.ascx.cs" Inherits="widget_10_100500_100507" %>
<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
    OldValuesParameterFormatString="original_{0}" SelectMethod="GetAvailableSys" 
    TypeName="NXEIP.DAO.SysDAO">
    <SelectParameters>
        <asp:Parameter Name="user_login" Type="String" />
    </SelectParameters>
                 </asp:ObjectDataSource>




<script type="text/javascript">
    $(function () {
        //先藏CONTENT
        $("#<%=this.application.ClientID%> .app_more").hide();

        $(".head").css("cursor", "pointer");

        $("#<%=this.app_more.ClientID%>").click(function () {

            $("#<%=this.application.ClientID%> .app_more").toggle('slow');

        });

       

    });

    </script>

<div class="block-1">

          <div class="headerin">
             <div class="in3">
                 
                 個人應用程式</div>
             
              <div  runat="server" id="app_more" class="more"></div>
              
          
          </div>
          <div class="first">
            


              <div id="application" runat="server">
                
              
              </div>



            <!--
              <asp:DataList ID="DataList1" runat="server" DataSourceID="ObjectDataSource1">
            
                <ItemTemplate>
                 <li class="dot_a51">
                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/10/100500/100507.aspx"><%#Eval("sys_name") %></asp:HyperLink></li>
                
                </ItemTemplate>
            
              </asp:DataList>
            -->             
           
          </div>
          </div> 
          
