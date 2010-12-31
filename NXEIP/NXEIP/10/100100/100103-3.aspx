<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="100103-3.aspx.cs" Inherits="_10_100100_100103_3" %>

<%@ Register Src="../../lib/Navigator.ascx" TagName="Navigator" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="MattBerseth.WebControls" Namespace="MattBerseth.WebControls"
    TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript" src="../../js/lytebox.js"></script> 
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
        OldValuesParameterFormatString="original_{0}" SelectMethod="GetAlbumPhoto" 
        TypeName="NXEIP.DAO._100103DAO">
        <SelectParameters>
            <asp:Parameter Name="album_no" Type="Int32" />
          
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
                <asp:Button ID="btn_del" class="b-input"  OnClientClick="return confirm('確定要刪除相片?')" OnClick="btn_del_Click" runat="server" Text="刪除相片" />
                
            </div>

            <div class="b6">
                <asp:Button ID="btn_cover" class="b-input"  runat="server" Text="設為封面" 
                    onclick="btn_cover_Click" />
                
            </div>

            <div class="b6">
                <input type="button" class="thickbox b-input" alt="100103-4.aspx?id=<%=Request["album"] %>&modal=true&TB_iframe=true&height=600&width=600"
                    value="新增相片" >
            </div>

              </asp:Panel>
            <ul>
            <li><span class="a-title">
                <asp:Literal ID="lit_album" runat="server" Text=""></asp:Literal>相簿內容</span></li>
            </ul>
        </div>
        <div class="box">
            <div class="head">
                <ul>
                    <li><span class="a-title">共 <asp:Literal ID="lit_photo_count" runat="server" Text="0"></asp:Literal> 張相片</span></li>
                </ul>
            </div>

             <div class="block3">
                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="100103.aspx">回上一層</asp:HyperLink>
             </div>  
            

             
            

             

        </div>



        
            <div class="box">
            <asp:ListView ID="ListView1" runat="server" 
            DataSourceID="ObjectDataSource1"  DataKeyNames="alb_no,pho_no">
            <LayoutTemplate>
           
                <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
            
            </LayoutTemplate>
            
            
            
            
            <ItemTemplate>
            
            <div class="content">
                <div class="photo_bg1">
                    
                    <asp:HyperLink ID="HyperLink4" runat="server" rel="lytebox[album]" title='<%# Eval("pho_desc") %>' NavigateUrl='<%#String.Format("100103-1.ashx?pic=org&album={0}&photo={1}",Eval("alb_no"),Eval("pho_no")) %>'>
                    
                    <asp:Image ID="Image1" runat="server" CssClass="pic_01"  AlternateText='<%#Eval("pho_name") %>' ImageUrl='<%# String.Format("100103-1.ashx?album={0}&photo={1}",Eval("alb_no"),Eval("pho_no"))%>'/>
                   </asp:HyperLink>
                </div>
                <div class="ps2">
                    
                    <asp:HyperLink  CssClass="a-letter-t2"   ID="HyperLink3" runat="server" rel="lytebox[album]" title='<%# Eval("pho_desc") %>' NavigateUrl='<%#String.Format("100103-1.ashx?pic=org&album={0}&photo={1}",Eval("alb_no"),Eval("pho_no")) %>'>
                    
                      <%#Eval("pho_name") %>
                    <br />
                     <%#Eval("pho_desc") %>&nbsp;
                    
                    </asp:HyperLink>
                    
                   
                </div>
                
                    <asp:Panel class="ps3" ID="Panel1" runat="server" Visible='<%#CheckPermission((Int32)Eval("pho_createuid")) %>'  >
                    
                    <asp:CheckBox ID="CheckBox1" runat="server" />
                    <asp:HyperLink ID="HyperLink2" runat="server" CssClass="thickbox imageButton edit" NavigateUrl='<%# string.Format("100103-5.aspx?album={0}&photo={1}&modal=true&TB_iframe=true&height=378&width=600",Eval("alb_no"),Eval("pho_no"))%>'><span>修改</span></asp:HyperLink>
                   
                    </asp:Panel>
                
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
