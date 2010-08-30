<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="100501.aspx.cs" Inherits="_10_100500_100501" %>

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
        <h2><span>可用Widget功能</span></h2>
        
       <!--
            
                <li>
                    <a id="ctl00_ContentPlaceHolder1_Widget-1" href="javascript:__doPostBack(&#39;ctl00$ContentPlaceHolder1$Widget-1&#39;,&#39;&#39;)">個人資訊
                    </a>
                </li>
                <li>
                    <a id="ctl00_ContentPlaceHolder1_Widget-2" href="javascript:__doPostBack(&#39;ctl00$ContentPlaceHolder1$Widget-2&#39;,&#39;&#39;)">時間
                    </a>
                </li>
                <li>
                    <a id="ctl00_ContentPlaceHolder1_Widget-3" href="javascript:__doPostBack(&#39;ctl00$ContentPlaceHolder1$Widget-3&#39;,&#39;&#39;)">個人最新消息
                    </a>
                </li>
            
        -->    
        
       
                
     </div>
        
    
    </asp:Panel>
   
   

   






  

    <asp:Panel ID="Panel1"  CssClass="dock" runat="server" visible="false" >
         


              


            <div class="controlPanel">
                
                
               
                
                
                <div class="head"></div>
                <ul>
                    <li><a class="save" onclick="saveLayout()"><span>存檔</span></a></li>
                    <li><a class="setting" onclick="widget_init()"><span>設定</span></a></li>
                    <li><a class="widget" onclick="$('.widgetfunc').slideToggle('slow')"><span>Widget</span></a></li>
                </ul>
            </div>

    </asp:Panel>




        
        </ContentTemplate>
    
    
    </asp:UpdatePanel>
   

</asp:Content>

