<%@ Page Title="" Language="C#" MasterPageFile="~/MainPage.master" AutoEventWireup="true" CodeFile="error2.aspx.cs" Inherits="error_error2"  EnableSessionState="True" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </asp:ToolkitScriptManager>
 <div class="error">
        
        <asp:Literal ID="errorMsg" runat="server"></asp:Literal>
       
    </div>

     <div class="error">
        <asp:Literal ID="detail" runat="server"></asp:Literal>
    </div>
</asp:Content>

