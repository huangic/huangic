<%@ Page  Title=""  Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AjaxTest.aspx.cs" EnableEventValidation="false" Inherits="AjaxTest" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp"    %>












<%@ Register src="lib/tree/DepartmentPanel.ascx" tagname="DepartmentPanel" tagprefix="uc1" %>












<%@ Register src="lib/tree/DepartTreeListBox.ascx" tagname="DepartTreeListBox" tagprefix="uc2" %>












<%@ Register src="lib/tree/DepartTreeTextBox.ascx" tagname="DepartTreeTextBox" tagprefix="uc3" %>












<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">






</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    
    <asp:Button ID="Button3" runat="server" Text="Check DataBase" 
        onclick="Button3_Click" />
      <br />
    <asp:TextBox ID="TextBox1" runat="server" Height="151px" Width="569px"></asp:TextBox>

  
    <br />

    

    <uc1:DepartmentPanel ID="DepartmentPanel1" runat="server" LeafType="People" 
        SelectMode="Multi" ShowDeleteButton="False" PeopleColumn="Name,Ext" 
        TreeType="Parallel" />

    

    <br />
    <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Button" />
   
    <br />
    <uc2:DepartTreeListBox ID="DepartTreeListBox1" runat="server" 
        LeafType="Department" TreeType="All" PeopleColumn="Name,Title" 
        PeopleType="Contract" />
    <asp:Button ID="Button2" runat="server" onclick="Button2_Click" Text="Button" />
   
    <br />
   
    <br />
    <uc3:DepartTreeTextBox ID="DepartTreeTextBox1" runat="server" LeafType="People" 
        PeopleColumn="Name,Title,WorkId" PeopleType="All" />
    <br />

    

    <br />

    

</asp:Content>

