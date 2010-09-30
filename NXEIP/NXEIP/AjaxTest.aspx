<%@ Page  Title=""  Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="AjaxTest.aspx.cs" Inherits="AjaxTest" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>



<%@ Register src="lib/SWFUpload/UC_SWFUpload.ascx" tagname="UC_SWFUpload" tagprefix="uc2" %>




<%@ Register src="lib/tree/jQueryDepartTree.ascx" tagname="jQueryDepartTree" tagprefix="uc4" %>






<%@ Register src="lib/tree/jQueryPeopleTree.ascx" tagname="jQueryPeopleTree" tagprefix="uc5" %>






<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script src="js/jquery-departTree.js" type="text/javascript"></script> 





</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>


    
       

    


    
       
    部門
    <uc4:jQueryDepartTree ID="jQueryDepartTree1" runat="server" />

    人員
    <uc5:jQueryPeopleTree ID="jQueryPeopleTree1" runat="server" />

    
     <asp:Button ID="Button1" runat="server" Text="Button" 
        onclick="Button1_Click" />

    

    <br />
    <br />
    <uc4:jQueryDepartTree ID="jQueryDepartTree2" runat="server" />

    

    <br />
    <uc5:jQueryPeopleTree ID="jQueryPeopleTree2" runat="server" />
    <br />
    <uc2:UC_SWFUpload ID="UC_SWFUpload1" runat="server" />
    <br />

    

</asp:Content>

