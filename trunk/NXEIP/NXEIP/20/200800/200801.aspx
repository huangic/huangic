<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="200801.aspx.cs" Inherits="_20_200800_200801" %>

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
             tb_init('a.thickbox');
           }
        }
</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
    <asp:HiddenField ID="hidden_mode" runat="server" />
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
        OldValuesParameterFormatString="original_{0}" SelectMethod="GetSearchData" EnablePaging="true"  SelectCountMethod="GetSearchDataCount"
        TypeName="NXEIP.DAO.UnmarriedDAO">
        <SelectParameters>
            <asp:Parameter Name="name" Type="String" />
            <asp:Parameter Name="sex" Type="String" />
          
        </SelectParameters>
    </asp:ObjectDataSource>
    
    
    
    <uc1:Navigator ID="Navigator1" runat="server" SysFuncNo="200801" />
   
    
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
   
   
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
     
     
     <ContentTemplate>
   
   
   
    <div class="photo">

      
        
     


        <div class="select">
            

             
            

              <div class="b6">
                <asp:Button ID="Button2" CssClass="a-input"  CommandName="female" runat="server" Text="女同仁" onclick="Button_Click" />
              
              </div>

            <div class="b6">
                <asp:Button ID="Button1" CssClass="b-input2"  CommandName="male" runat="server" 
                    Text="男同仁" onclick="Button_Click" />
            </div>


            <ul>
            <li><span class="a-title">
                未婚同仁</span></li>
            </ul>
        </div>
        <div class="box">
            <div class="head">
               
            </div>
        

        </div>



        
           
            <asp:ListView ID="ListView1" runat="server" 
            DataSourceID="ObjectDataSource1" DataKeyNames="unm_no" >
            <LayoutTemplate>
            <div class="box">
                <asp:PlaceHolder ID="itemPlaceholder" runat="server"></asp:PlaceHolder>
             </div>
            </LayoutTemplate>
            
            
            
            
            <ItemTemplate>
            
            <div class="content">
                <div class="photo_bg1">
                    
                     <asp:HyperLink ID="HyperLink3" CssClass="thickbox a-letter-t2" runat="server" NavigateUrl='<%# String.Format("200801-2.aspx?ID={0}&TB_iframe=true&height=450&width=620",Eval("unm_no"))%>'>
                    <asp:Image ID="Image1" runat="server" CssClass="pic_01"  AlternateText='<%#Eval("unm_name") %>' ImageUrl='<%# String.Format("200801-1.ashx?id={0}",Eval("unm_no"))%>'/>
                  
                  </asp:HyperLink>
                </div>

                     
                <div class="ps2">
                    
                    


                    <asp:HyperLink ID="HyperLink1" CssClass="thickbox a-letter-t2" runat="server" NavigateUrl='<%# String.Format("200801-2.aspx?ID={0}&TB_iframe=true&height=450&width=620",Eval("unm_no"))%>'>
                    
                    <%# GetDepartmentName((Int32)Eval("unm_depno")) %>&nbsp;<%# GetTitleName((Int32)Eval("unm_typno")) %>&nbsp;
                    
                    <br />
                    
                       <%#Eval("unm_name") %>&nbsp;
                    </asp:HyperLink>
                
                
                
            
                </div>
                <div class="ps3">
                     
                </div>



            </div>

            

            </ItemTemplate>


            <EmptyDataTemplate>
              無任何資料
            </EmptyDataTemplate>
             
           
            
            </asp:ListView>
           
           
            

            
       
        



     
    </div>



          <div class="pager">
                        <asp:DataPager ID="DataPager1" runat="server" PageSize="25" 
                            PagedControlID="ListView1">
                            <Fields>
                                <NXEIP:GooglePagerField />
                            </Fields>
                        </asp:DataPager>
                    </div>

       </ContentTemplate>
        
        
        </asp:UpdatePanel>

</asp:Content>
