<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="100201-1.aspx.cs" Inherits="_10_100200_100201_1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register src="../../lib/Navigator.ascx" tagname="Navigator" tagprefix="uc1" %>
<%@ Register src="../../lib/tree/DepartTreeListBox.ascx" tagname="DepartTreeListBox" tagprefix="uc4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <uc1:Navigator ID="Navigator1" runat="server" SysFuncNo="100201" SubFunc="發送訊息" />
    <div class="tableDiv">
        <div class="header">
            <div class="h1">
            </div>
            <div class="h2">
            </div>
            <div class="h3">
            </div>
        </div>
        <table>
            <tr>
                <th style="width:15%">
                    訊息發送者
                </th>
                <td style="width:35%">
                    <asp:Label ID="lab_name" runat="server"></asp:Label>
                </td>
                <th style="width:15%">
                    發送型式
                </th>
                <td style="width:35%">
                    
                    <asp:CheckBoxList ID="CheckBoxList1" runat="server" 
                        RepeatDirection="Horizontal" RepeatLayout="Flow">
                        <asp:ListItem Value="1">個人訊息</asp:ListItem>
                        <asp:ListItem Value="2">E-MAIL</asp:ListItem>
                    </asp:CheckBoxList>
                    
                </td>
            </tr>
            <tr>
                <th>
                    訊息主旨
                </th>
                <td colspan="3">
                    <asp:TextBox ID="tbox_subject" runat="server" Width="345px" MaxLength="100"></asp:TextBox>
                </td>
                
            </tr>
            <tr>
                <th>
                    訊息內容
                </th>
                <td colspan="3">
                    
                    <asp:TextBox ID="tbox_body" runat="server" Height="135px" TextMode="MultiLine" 
                        Width="345px"></asp:TextBox>
                    
                </td>
            </tr>
            <tr>
                <th>
                    連結網址
                </th>
                <td colspan="3">
                    
                    <asp:TextBox ID="tbox_link" runat="server" Width="345px" MaxLength="100"></asp:TextBox>
                    
                </td>
            </tr>
            <tr>
                <th>
                    請選擇人員
                </th>
                <td colspan="3">
                    <uc4:DepartTreeListBox ID="DepartTreeListBox_people" runat="server" 
                        LeafType="People" PeopleShowSelf="False" />
                </td>
                
            </tr>
        </table>
        <div class="footer">
            <div class="f1">
            </div>
            <div class="f2">
            </div>
            <div class="f3">
            </div>
        </div>
        <div class="bottom">
            <asp:Button ID="Button1" runat="server" CssClass="b-input" Text="確定" OnClick="Button1_Click" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="Button2" runat="server" CssClass="a-input" Text="取消" OnClick="Button2_Click" />
        </div>
    </div>
</asp:Content>

