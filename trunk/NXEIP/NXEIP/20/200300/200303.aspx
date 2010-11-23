<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="200303.aspx.cs" Inherits="_20_200300_200303" EnableEventValidation="false" %>

<%@ Register Src="../../lib/Navigator.ascx" TagName="Navigator" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="MattBerseth.WebControls" Namespace="MattBerseth.WebControls"
    TagPrefix="cc1" %>
<%@ Register src="../../lib/people/PeopleDetail.ascx" tagname="PeopleDetail" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
      

       

   



    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    
   
    
    <asp:ObjectDataSource ID="ObjectDataSource_spot" runat="server" 
        OldValuesParameterFormatString="original_{0}" SelectMethod="GetFloorsSpot" 
        TypeName="NXEIP.DAO._200303DAO"></asp:ObjectDataSource>
    
   
    
    <uc1:Navigator ID="Navigator1" runat="server" SysFuncNo="200303" />
    
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="hidden_spot" runat="server" />
        
        
           <div class="select-1">

               <asp:ListView ID="lv_spot" runat="server" DataSourceID="ObjectDataSource_spot"  
                   DataKeyNames="spo_no" 
                   >
                        <ItemTemplate>
                            <span><asp:LinkButton ID="lb_cat" runat="server"  CssClass='<%# hidden_spot.Value.Equals( Eval("spo_no").ToString()) ? "a-letter-s1":""  %>'  CommandName="click_cat"><%# Eval("spo_name") %></asp:LinkButton></span>
                                      
                        
                        </ItemTemplate>
  
                       
               </asp:ListView>
           </div>

          
        <div id="div_floor" class="floor" runat="server">
                   
          <div class="box">
           	   <div class="head">14樓</div>
         	    
         	      <div class="content">
                    <div class="b1"></div>
       		           <div class="b2">
                    巿政資料中心 (Iinstitute For informatin Industry)、員工圖書室會議室 (Iinstitute For informatin Industry)、員工圖書室 (Iinstitute For informatin Industry)、會議室 (Iinstitute For informatin Industry)、員工圖書室會議室 (Iinstitute For informatin Industry)</div>
         
                    </div>
               </div>
          </div>
          
          
          
          
          
                    
          
    </div>
         
        
        </ContentTemplate>
    </asp:UpdatePanel>
 
    
    
   
   
    
    
    
    
    

</asp:Content>
