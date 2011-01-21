<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="200301.aspx.cs" Inherits="_20_200300_200301" %>

<%@ Register Src="../../lib/Navigator.ascx" TagName="Navigator" TagPrefix="uc1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetS06FromSufNO" TypeName="NXEIP.DAO.Sys06DAO">
        <SelectParameters>
            <asp:Parameter DefaultValue="200301" Name="suf_no" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:HiddenField ID="hidden_1" runat="server" />
    <uc1:Navigator ID="Navigator1" runat="server" SysFuncNo="200301" />

    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <ContentTemplate>
            <div class="tableDiv">
                <table>
                    <tr>
                        <td>
                            商店名稱：<asp:TextBox ID="tbox_name" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <div class="bottom">
                    <asp:Button ID="btn_ok" runat="server" CssClass="b-input" Text="查詢商店" OnClick="btn_ok_Click" />
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            
            <div class="select-1">
                <asp:ListView ID="lv_food" runat="server" DataSourceID="ObjectDataSource1" DataKeyNames="s06_no"
                    OnItemCommand="lv_food_ItemCommand">
                    <ItemTemplate>
                        <span>
                            <asp:LinkButton ID="lb_cat" runat="server" CssClass='<%# hidden_1.Value.Equals( Eval("s06_no").ToString()) ? "a-letter-s1":""  %>'
                                CommandName="click"><%# Eval("s06_name") %></asp:LinkButton></span>
                    </ItemTemplate>
                </asp:ListView>
            </div>
            <div id="div_foods" class="food" runat="server">
                <div class="select" style="text-align: right">
                    <div class="function">
                        <asp:Button ID="Button1" runat="server" Text="我要提供資料" CssClass="b-input" 
                            onclick="Button1_Click" />
                    </div>
                </div>
                
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

