<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OkMessagebox.ascx.cs" Inherits="lib_messagebox_OkMessagebox" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Panel ID="pnlPop" runat="server" CssClass="confirm-dialog">
   
     
                <div class="inner">
                    <p><asp:Label ID="lab_title" runat="server" Text="Title"></asp:Label></p>
                    <h2><asp:Label ID="lab_meg" runat="server" Text="Message"></asp:Label></h2>
                    <div class="base">
                        <asp:Button ID="btnYes" runat="server" Text="確定" CssClass="b-input"  />
                        
                         <!--
                        <asp:Button ID="btnNo" runat="server" Text="取消" CssClass="a-input"/>
                       
                        <asp:LinkButton ID="LinkButton1" runat="server" CssClass="close" 
                            OnClientClick="$find('mdlPopup').hide(); return false;" />
                            -->
                    </div>
                </div>
          
      
        
   
    
    
</asp:Panel>

<script type="text/javascript">
        function fnClickOK(sender, e)
        {
            __doPostBack(sender,e);
        }
        
        
          function fnClickNo(sender, e)
        {
            return false;
        }
        
        
       
        
        </script>
        

<asp:ModalPopupExtender ID="mdlPopupMsgBox" runat="server" 
 BehaviorID="mdlPopupMsgBox"
    DynamicServicePath="" Enabled="True" TargetControlID="pnlPop" BackgroundCssClass="modalBackground" PopupControlID="pnlPop">
</asp:ModalPopupExtender>
