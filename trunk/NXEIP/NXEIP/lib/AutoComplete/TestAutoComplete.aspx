<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="TestAutoComplete.aspx.cs" Inherits="lib_AutoComplete_TestAutoComplete" %>

<%@ Register Src="Autocomplete.ascx" TagName="Autocomplete" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:Autocomplete ID="Autocomplete1" runat="server" />
    <div id="div_msg" runat="server"></div>
    <asp:Button ID="Button1" runat="server" Text="Button" onclick="Button1_Click" />
</asp:Content>
