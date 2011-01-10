<%@ Control Language="C#" AutoEventWireup="true" CodeFile="100103-1.ascx.cs" Inherits="widget_10_100100_100103_1" %>
<%@ Import Namespace="NXEIP.DAO"  %>

<asp:ObjectDataSource ID="ObjectDataSource_Depart" runat="server" 
    OldValuesParameterFormatString="original_{0}" SelectMethod="GetTopUploadDepartPhoto" 
    TypeName="NXEIP.DAO._100103DAO">
    <SelectParameters>
        <asp:Parameter Name="num" Type="Int32" />
        <asp:Parameter Name="dep_no" Type="Int32" />
    </SelectParameters>
</asp:ObjectDataSource>
<asp:ObjectDataSource ID="ObjectDataSource_Unit" runat="server" 
    OldValuesParameterFormatString="original_{0}" SelectMethod="GetTopUploadUnitPhoto" 
    TypeName="NXEIP.DAO._100103DAO">
    <SelectParameters>
        <asp:Parameter Name="num" Type="Int32" />
    </SelectParameters>
</asp:ObjectDataSource>


              
              
              <div class="first">
              <div class="header2">
                <ul>
                <li class="in_personal">
                  <asp:Label ID="Label" runat="server" Text="Label"></asp:Label>
                </li>
                     <li class="more">
                <asp:HyperLink ID="HyperLink1" CssClass=" imageButton" runat="server" NavigateUrl="~/10/100100/100103.aspx"><span>more</span></asp:HyperLink>
                      </li>
                </ul>
              </div>

              <div class="big2Div_personal">
              
              
               <asp:ListView ID="ListView1" runat="server" DataSourceID="ObjectDataSource_Depart">

               <ItemTemplate>
                  
                  <div class="box">
                  <div class="photo_personal">
                    <asp:HyperLink ID="HyperLink4" runat="server" rel="lytebox[album]" title='<%# Eval("pho_desc") %>' NavigateUrl='<%#String.Format("~/10/100100/100103-3.aspx?album={0}",Eval("alb_no")) %>'>
                    
                    <asp:Image ID="Image1"  Width="100%"  runat="server"  AlternateText='<%# Eval("pho_name") %>' ImageUrl='<%# String.Format("~/10/100100/100103-1.ashx?album={0}&photo={1}",Eval("alb_no"),Eval("pho_no"))%>'/>
                   </asp:HyperLink>
                  
                  </div>
                  
                  <div class="ps2">
                       <asp:Label ID="Label1" runat="server" Text='<%# GetDepartName((Int32)Eval("album.alb_dep")) %>'></asp:Label> </div>
                  
                  <div class="ps3"><asp:Label ID="Label2" runat="server" Text='<%#GetPeopleName((Int32)Eval("album.peo_uid")) %>'></asp:Label></div>
                  </div>
                  
                  
                  
                  
               
               </ItemTemplate>

                   </asp:ListView>
              </div>
              </div>
        

    
 

    
