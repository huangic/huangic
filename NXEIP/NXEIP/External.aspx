<%@ Page Title="" Language="C#" MasterPageFile="~/MainPage.master" AutoEventWireup="true" CodeFile="External.aspx.cs" Inherits="External" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
 
  <!--
    <script type="text/javascript" src="js/autoHeight.js"></script>
 

    <script type="text/javascript" src="js/jquery.iframe.autoHeight.js"></script>
    <script type="text/javascript">
        // match all iframes / use jQuery or alias $

        $(function () { 
        //jQuery('iframe').iframeAutoHeight();

        // only panoramic.html iframe with some extra height
        // $('iframe.photo').iframeAutoHeight({heightOffset: 50});
        // NOTES: you can wrap this in document ready if you like
        // but IE8 didn't always like it
       

        })
    </script>
  
-->

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div>
    
    
    <iframe id="external"  scrolling="auto" class="autoHeight" src='<%=url%>' width="100%" height="1000px" 
      frameborder="0"  >
    
    </iframe>
    
    
    
    
    </div>
</asp:Content>

