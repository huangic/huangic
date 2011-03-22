<%@ Control Language="C#" AutoEventWireup="true" CodeFile="100103.ascx.cs" Inherits="widget_10_100100_100103" %>


<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
    OldValuesParameterFormatString="original_{0}" SelectMethod="GetTopUploadPhoto" 
    TypeName="NXEIP.DAO._100103DAO">
    <SelectParameters>
        <asp:Parameter Name="num" Type="Int32" />
        <asp:Parameter Name="peo_uid" Type="Int32" />
    </SelectParameters>
</asp:ObjectDataSource>

<div class="block-1">
           <script type="text/javascript">

               $(function () {
                   $('.block-1 .photo').cycle(
                    
                   );

               });

           </script>        
         
         
          <div class="headerin">
          <ul>
            <li class="in1">網路相簿</li>
          </ul>
          </div>
              
              
              <div class="first">
              <div class="photo">
              
              
               <asp:ListView ID="ListView1" runat="server" DataSourceID="ObjectDataSource1">

               <ItemTemplate>
                    <asp:HyperLink ID="HyperLink4" runat="server" rel="lytebox[album]" title='<%# Eval("pho_desc") %>' NavigateUrl='<%#String.Format("~/10/100100/100103-3.aspx?album={0}",Eval("alb_no")) %>'>
                    
                    <asp:Image ID="Image1"   runat="server"  AlternateText='<%#Eval("pho_name") %>' ImageUrl='<%# String.Format("~/10/100100/100103-1.ashx?album={0}&photo={1}",Eval("alb_no"),Eval("pho_no"))%>'/>
                   </asp:HyperLink>
               
               </ItemTemplate>

                   </asp:ListView>
              </div>
              </div>
        </div>

    
 

    
