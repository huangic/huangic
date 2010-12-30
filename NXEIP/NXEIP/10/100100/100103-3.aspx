<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="100103-3.aspx.cs" Inherits="_10_100100_100103_3" %>

<%@ Register Src="../../lib/Navigator.ascx" TagName="Navigator" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="MattBerseth.WebControls" Namespace="MattBerseth.WebControls"
    TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">


 <script type="text/javascript">
     function update(msg) {
         __doPostBack('<%=UpdatePanel1.ClientID%>', '');
         tb_remove();

         if (msg != undefined) {
             alert(msg);
         }
     }

      function pageLoad(sender, args) {
        if (args.get_isPartialLoad()) {
            //  reapply the thick box stuff
             tb_init('.thickbox');
           }
        }
</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
        OldValuesParameterFormatString="original_{0}" SelectMethod="GetPeopleAlbum" 
        TypeName="NXEIP.DAO._100103DAO">
        <SelectParameters>
            <asp:Parameter Name="people" Type="Int32" />
            <asp:Parameter Name="dep_no" Type="Int32" />
            <asp:Parameter Name="alb_public" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    
    
    
    <uc1:Navigator ID="Navigator1" runat="server" SysFuncNo="100103" />
   
    
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
   
   
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
     
     
     <ContentTemplate>
   
   
   
    <div class="photo">

      
        
     


        <div class="select">
            <asp:Panel ID="Control" runat="server">
          
            <div class="b6">
                <asp:Button ID="btn_del" class="b-input"  runat="server" Text="刪除相片" />
                
            </div>
            <div class="b6">
                <input type="button" class="thickbox b-input" alt="100103-4.aspx?modal=true&TB_iframe=true"
                    value="新增相片" >
            </div>

              </asp:Panel>
            <ul>
            <li><span class="a-title">
                <asp:Literal ID="lit_album" runat="server" Text="個人"></asp:Literal>相簿</span></li>
            </ul>
        </div>
        <div class="box">
            <div class="head">
                <ul>
                    <li><span class="a-title">共 <asp:Literal ID="lit_album_count" runat="server" Text="0"></asp:Literal> 本相簿</span></li>
                </ul>
            </div>

             <div class="block3">
                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="100103.aspx">回上一層</asp:HyperLink>
             </div>  
            

             
            

             

        </div>



        
           
            <asp:ListView ID="ListView1" runat="server" 
            DataSourceID="ObjectDataSource1" >
            <LayoutTemplate>
            <div class="box">
                <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
             </div>
            </LayoutTemplate>
            
            
            
            
            <ItemTemplate>
            
            <div class="content">
                <div class="photo_bg1">
                    <asp:Image ID="Image1" runat="server" CssClass="pic_01"  AlternateText='<%#Eval("Album.alb_name") %>' ImageUrl='<%# String.Format("100103-1.ashx?album={0}&photo={1}",Eval("Album.alb_no"),Eval("Album.alb_cover"))%>'/>
                  
                </div>
                <div class="ps2">
                    <a class="a-letter-t2" href="#">
                    
                    <%#Eval("Album.alb_name") %>
                    
                    
                    <br />
                    
                      <%#Eval("Album.alb_desc") %></a>
                </div>
                <div class="ps3">
                    <input type="checkbox" id="checkbox2" name="checkbox2">
                    <%#Eval("Count") %> 張相片</div>
            </div>

            

            </ItemTemplate>


            <EmptyDataTemplate>
              無任何相簿
            </EmptyDataTemplate>
             
           
            
            </asp:ListView>
           
           
            

            
       
        



     
    </div>

       </ContentTemplate>
        
        
        </asp:UpdatePanel>

</asp:Content>
