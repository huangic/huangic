<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ConfirmMessagebox.ascx.cs" Inherits="lib_messagebox_ConfirmMessagebox" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Panel ID="pnlPop" runat="server" CssClass="confirm-dialog" DefaultButton="btnYes">
   
     
                <div class="inner">
                    <p><asp:Label ID="lab_title" runat="server"></asp:Label></p>
                    <h2><asp:Label ID="lab_meg" runat="server" Text="Message"></asp:Label></h2>
                    <div class="base">
                        <asp:Button ID="btnYes" runat="server" Text="確定" CssClass="b-input"  />
                        
                         
                        <asp:Button ID="btnNo" runat="server" Text="取消"  CssClass="a-input"/>
                       <!--
                        <asp:LinkButton ID="LinkButton1" runat="server" CssClass="close" 
                            OnClientClick="$find('mdlPopup').hide(); return false;" />
                       -->
                    </div>
                </div>
          
      
        
   
    
    
</asp:Panel>

<script type="text/javascript">
   



    function ConfirmOK()
        {
            this._ConfirmPop.hide();
            __doPostBack(this._Source.name,'');
        }


        function ConfirmNo()
        {
            this._ConfirmPop.hide();
            //__doPostBack(sender, e);
            this._Source = null;
            this._ConfirmPop = null;
        }
        
        
       
        
        </script>
        

<asp:ModalPopupExtender ID="confirmModal" runat="server" 
BehaviorID="confirmModal"
    DynamicServicePath="" Enabled="True" TargetControlID="pnlPop" BackgroundCssClass="modalBackground" PopupControlID="pnlPop" CancelControlID="btnNo" OkControlID="btnYes" OnCancelScript="ConfirmNo()" OnOkScript="ConfirmOK()">
</asp:ModalPopupExtender>
