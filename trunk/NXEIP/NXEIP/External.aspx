<%@ Page Title="" Language="C#" MasterPageFile="~/MainPage.master" AutoEventWireup="true" CodeFile="External.aspx.cs" Inherits="External" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 
 <!--
    <script type="text/javascript" src="js/autoHeight.js"></script>
  -->
    <script type="text/javascript" src="js/jquery.iframe.autoHeight.js"></script>
    <script type="text/javascript">
        // match all iframes / use jQuery or alias $

        $(function () { 
        jQuery('iframe').iframeAutoHeight();

        // only panoramic.html iframe with some extra height
        // $('iframe.photo').iframeAutoHeight({heightOffset: 50});
        // NOTES: you can wrap this in document ready if you like
        // but IE8 didn't always like it
        //document.domain = "<%=Request["url"]%>";

        })
    </script>
  
 <!--
 <script>
 
 function SetCwinHeight(obj)
 {
   var cwin=obj;
   if (document.getElementById)
   {
     if (cwin && !window.opera)
     {
       if (cwin.contentDocument && cwin.contentDocument.body.offsetHeight)
         cwin.height = cwin.contentDocument.body.offsetHeight + 20; //FF NS
       else if(cwin.Document && cwin.Document.body.scrollHeight)
         cwin.height = cwin.Document.body.scrollHeight + 10;//IE
     }
     else
     {
         if(cwin.contentWindow.document && cwin.contentWindow.document.body.scrollHeight)
             cwin.height = cwin.contentWindow.document.body.scrollHeight;//Opera
     }
   }
 }
 
 
 </script>
 -->
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
    
    
    <iframe id="external"  scrolling="auto" class="autoHeight" src='http://<%=url%>' width="100%" height="2000px" 
      frameborder="0"  >
    
    </iframe>
    
    
    
    
    </div>
</asp:Content>

