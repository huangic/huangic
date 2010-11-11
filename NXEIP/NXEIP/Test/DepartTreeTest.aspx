<%@ Page  Title=""  Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="DepartTreeTest.aspx.cs" EnableEventValidation="false" Inherits="_Test_DepartTreeTest" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp"    %>












<%@ Register src="../lib/tree/DepartmentPanel.ascx" tagname="DepartmentPanel" tagprefix="uc1" %>












<%@ Register src="../lib/tree/DepartTreeListBox.ascx" tagname="DepartTreeListBox" tagprefix="uc2" %>












<%@ Register src="../lib/tree/DepartTreeTextBox.ascx" tagname="DepartTreeTextBox" tagprefix="uc3" %>












<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">






</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    

    <br />
    <br />

    

    <uc1:DepartmentPanel ID="DepartmentPanel1" runat="server" LeafType="Department" 
        SelectMode="Multi" ShowDeleteButton="False" PeopleColumn="Name,Ext" />

    

    <br />
    <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="確定" />
    <input  type="button" class="TreeDel" value="刪除"/>

    <br />
    <uc2:DepartTreeListBox ID="DepartTreeListBox1" runat="server" 
        LeafType="Department" TreeType="All" PeopleColumn="Name,Title" 
        PeopleType="Contract" />
    <asp:Button ID="Button2" runat="server" onclick="Button2_Click" Text="確定" />
   
    <br />
   
    <br />
    <uc3:DepartTreeTextBox ID="DepartTreeTextBox1" runat="server" />
    <br />

    

</asp:Content>

