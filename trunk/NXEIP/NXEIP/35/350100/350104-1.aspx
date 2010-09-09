<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="350104-1.aspx.cs" Inherits="_35_350100_350104_1" %>

<%@ Register Src="../../lib/Navigator.ascx" TagName="Navigator" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <uc1:Navigator ID="Navigator1" runat="server" SysFuncNo="350104" />
    <div class="tableDiv">
        <div class="header">
            <div class="h1">
            </div>
            <div class="h2">
            </div>
            <div class="h3">
            </div>
        </div>
        <table width="100%" border="0" cellpadding="3" cellspacing="3" bgcolor="#FFFFFF">
            <tr>
                <td width="15%" align="right" bgcolor="#eeeeee" class="a-letter-2">
                    系統編號
                </td>
                <td bgcolor="#EEEEEE" class="a-letter-1" width="35%">
                    &nbsp;<asp:TextBox ID="tbox_sysNo" runat="server"></asp:TextBox>
                    <asp:Label ID="lab_sysNo" runat="server"></asp:Label>
                    <span id="span_1" class="a-letter-Red" runat="server">新增後不可更改</span>
                </td>
                <td align="right" bgcolor="#eeeeee" class="a-letter-2" width="15%">
                    分類名稱
                </td>
                <td bgcolor="#EEEEEE" class="a-letter-1" width="35%">
                    &nbsp;<asp:TextBox ID="tbox_name" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" bgcolor="#EEEEEE" class="a-letter-2">
                    排列順序
                </td>
                <td bgcolor="#EEEEEE" class="a-letter-1">
                    &nbsp;<asp:TextBox ID="tbox_order" runat="server"></asp:TextBox>
                </td>
                <td align="right" bgcolor="#EEEEEE" class="a-letter-2">
                    分類狀態
                </td>
                <td bgcolor="#EEEEEE" class="a-letter-1">
                    &nbsp;
                    <asp:RadioButtonList ID="rbl_status" runat="server" RepeatDirection="Horizontal"
                        RepeatLayout="Flow">
                        <asp:ListItem Selected="True" Value="1">上架</asp:ListItem>
                        <asp:ListItem Value="2">下架</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td align="right" bgcolor="#EEEEEE" class="a-letter-2">
                    未選圖示
                </td>
                <td bgcolor="#EEEEEE" class="a-letter-1">
                    <div id="div_defpic" runat="server">
                    </div>
                    <span class="table-a1">&nbsp;<asp:FileUpload ID="FileUpload1" runat="server" />
                    </span>
                </td>
                <td align="right" bgcolor="#EEEEEE" class="a-letter-2">
                    已選圖示
                </td>
                <td bgcolor="#EEEEEE" class="a-letter-1">
                    <div id="div_ovepic" runat="server">
                    </div>
                    <span class="table-a1">&nbsp;<asp:FileUpload ID="FileUpload2" runat="server" />
                    </span>
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
            &nbsp;&nbsp;&nbsp;
            <asp:Button ID="Button2" runat="server" CssClass="a-input" Text="取消" OnClick="Button2_Click" />
        </div>
        <div id="div_msg" runat="server">
        </div>
    </div>
</asp:Content>
