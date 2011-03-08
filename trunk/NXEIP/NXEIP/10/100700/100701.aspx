<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="100701.aspx.cs" Inherits="_10_100700_100701" %>

<%@ Register Src="../../lib/Navigator.ascx" TagName="Navigator" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="../../js/jquery-ui-1.8.2.custom.min.js"></script>
    <script type="text/javascript">
        $(function () {
            //先藏CONTENT
            //$(".ps2").hide();

            $(".head").css("cursor", "pointer");

            $(".head").click(function () {

                $(this).parent().find(".box .ps2").toggle('slow');

            });

            // $(".ps1").click(function () {

            //     $(this).parent().parent().children("li").toggle('slow');

            // });

        });

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:Navigator ID="Navigator1" runat="server" SysFuncNo="100701" />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
           
            <div id="application" class="app" runat="server">
                 <div style=" text-align:right">
            
            </div>
            </div>


        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
