<%@ Page Title="" Language="C#" MasterPageFile="~/MainPage.master" AutoEventWireup="true" CodeFile="100503.aspx.cs" Inherits="_10_100500_100503" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">



<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        
             <div id="leftDiv" class="leftbg widget-place" runat="server">
        <asp:Label id="holder1" runat="server" visible="false"> 左方區塊 </asp:Label>
    </div>
    <div id="centerDiv" class="center widget-place" runat="server" >
      <asp:Label id="holder2"  runat="server" visible="false"> 中央區塊 </asp:Label>
        
    </div>
    <div id="rightDiv" class="rightbg widget-place" runat="server" >
      <asp:Label id="holder3" runat="server" visible="false"> 右方區塊 </asp:Label>
    </div>
   
    <asp:Panel ID="Panel2" runat="server">
      <div id="func" class="widgetfunc" runat="server">
        可用Widget功能
        
     
      
      
       </div>
    
    
    </asp:Panel>
   
   
    <asp:Panel ID="Panel1"  CssClass="dock" runat="server" visible="false" >
          <div class="controlPanel" >
              版型設定|
                  <input type="button" class="b-input" onclick="saveLayout()"  value="存檔" />
                  <input type="button" class="a-input" onclick="widget_init()"  value="設定" />
                  <input type="button" class="a-input" onclick="$('.widgetfunc').slideToggle()"  value="Widget" />
               
            </div>
    </asp:Panel>
        
        </ContentTemplate>
    
    
    </asp:UpdatePanel>
   

</asp:Content>

