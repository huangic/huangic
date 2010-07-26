<%@ Control Language="C#" AutoEventWireup="true" CodeFile="jQueryPeopleTree.ascx.cs" Inherits="lib_tree_jQueryDepartTree"  EnableViewState="false"%>

    <div style="display:inline;">
    
      <div style="display:inline;">
        
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" EnableViewState="False" 
              RenderMode="Inline">
<ContentTemplate>
        
        <asp:ListBox ID="ListBox1" runat="server" Width="100px"></asp:ListBox>
   </ContentTemplate>
</asp:UpdatePanel>
   
   
   
        </div>   
     <div style="display:inline;text-align:center;width: 20px">
         
       <asp:HyperLink ID="HyperLink1" runat="server" CssClass="thickbox">
       
           <asp:Image ID="Image1" runat="server" ImageUrl="~/image/peruse.gif" />
       </asp:HyperLink>
     </div>

</div>



