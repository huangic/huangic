<%@ Control Language="C#" AutoEventWireup="true" CodeFile="PeopleDetail.ascx.cs" Inherits="lib_PeopleDetail" %>
<asp:HyperLink ID="hl_detail" runat="server" CssClass="popup" 
    ImageUrl="~/image/tel_icon.gif" 
    NavigateUrl="~/lib/people/Detail.aspx?id={0}">詳細</asp:HyperLink>

