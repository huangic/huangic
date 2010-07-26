<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="FileManager.aspx.cs" Inherits="FileExplorer_FileManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

     <script type="text/javascript" src="../js/jquery.cookie.js"></script>
     <script type="text/javascript" src="../js/jquery.hotkeys.js"></script>
     <script type="text/javascript" src="../js/jquery.jstree.js"></script>
      

       <script type="text/javascript">
           $(function() {
            $(".folderTree").jstree({
                "json_data": {
                    "ajax": {
                        "type": "POST",
                        "url": "FileManager.asmx/getFolder",
                        "contentType": "application/json; charset=utf-8",
                        "dataType":"json",
                        "async": "true",
                        "data":
                        function(n) {
                            return JSON.stringify({
                                "operation": "get_children",
                                "id": n.attr ? n.attr("id") : 0
                            });
                        }
                    }
                },
                "themes": {
                    "theme": "classic"
                },
                "plugins": ["themes", "json_data"]
            })
            })
       
       </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<div>
    <div class="folderTree">
        
    </div>
    
    <div class="fileTable">
    
    </div>
</div>




</asp:Content>

