<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="200301-1.aspx.cs" Inherits="_20_200300_200301_1" %>

<%@ Register Src="../../lib/Navigator.ascx" TagName="Navigator" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<uc1:Navigator ID="Navigator1" runat="server" SysFuncNo="200301" SubFunc="新增美食區" />
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
        OldValuesParameterFormatString="original_{0}" SelectMethod="GetS06FromSufNO" 
        TypeName="NXEIP.DAO.Sys06DAO">
        <SelectParameters>
            <asp:Parameter DefaultValue="200301" Name="suf_no" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
<div class="tableDiv">
        <div class="header">
            <div class="h1">
            </div>
            <div class="h2">
            </div>
            <div class="h3">
            </div>
        </div>
        <table id="table1">
            <tr>
                <th style="width:15%">
                    所屬分類
                </th>
                <td colspan="3">
                    
                    <asp:DropDownList ID="ddl_s06" runat="server" AppendDataBoundItems="True" 
                        DataSourceID="ObjectDataSource1" DataTextField="s06_name" 
                        DataValueField="s06_no">
                        <asp:ListItem Value="0">請選擇</asp:ListItem>
                    </asp:DropDownList>
                    
                </td>
            </tr>
            <tr>
                <th style="width:15%">
                    商店名稱
                </th>
                <td>
                    <asp:TextBox ID="tbox_name" runat="server"></asp:TextBox>
                </td>
                <th style="width:15%">
                    商店電話
                </th>
                <td>
                    <asp:TextBox ID="tbox_tel" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th style="width:15%">
                    商店所在地
                </th>
                <td>
                    <asp:TextBox ID="tbox_area" runat="server" Width="200px"></asp:TextBox>
                </td>
                <th style="width:15%">
                    商店網址
                </th>
                <td>
                    <asp:TextBox ID="tbox_www" runat="server" Width="250px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <th style="width:15%">
                    商店介紹
                </th>
                <td colspan="3">
                    <asp:TextBox ID="tbox_desc" runat="server" Height="175px" TextMode="MultiLine" 
                        Width="400px"></asp:TextBox>
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
            <asp:Button ID="Button1" runat="server" CssClass="b-input" Text="新增資料" 
                onclick="Button1_Click"/>
            &nbsp;&nbsp;
            <asp:Button ID="btn_cancel" runat="server" CssClass="a-input" Text="回美食區" OnClick="btn_cancel_Click" />
        </div>
    </div>
</asp:Content>

