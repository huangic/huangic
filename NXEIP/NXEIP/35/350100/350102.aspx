<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="350102.aspx.cs" Inherits="_35_350100_350102" %>

<%@ Register Src="../../lib/tree/jQueryDepartTree.ascx" TagName="jQueryDepartTree"
    TagPrefix="uc1" %>
<%@ Register Src="../../lib/tree/jQueryPeopleTree.ascx" TagName="jQueryPeopleTree"
    TagPrefix="uc2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../../lib/Navigator.ascx" TagName="Navigator" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">

        function pageLoad(sender, args) {
            if (args.get_isPartialLoad()) {
                //  reapply the thick box stuff
                tb_init('a.thickbox');
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <uc3:Navigator ID="Navigator1" runat="server" SysFuncNo="350102" />
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
                <th>
                    請選擇帳號及角色
                </th>
                <td>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <table width="72%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td width="112">
                                        &nbsp;
                                    </td>
                                    <td width="180">
                                        &nbsp;
                                    </td>
                                    <td width="113" align="center">
                                        目前設定
                                    </td>
                                    <td>
                                    </td>
                                    <td width="132" align="center">
                                        目前未設定
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:RadioButton ID="rbl_people" runat="server" Text="設定多人帳號" GroupName="G1" AutoPostBack="True"
                                            Checked="True" OnCheckedChanged="rbl_people_CheckedChanged" />
                                    </td>
                                    <td rowspan="2">
                                        <table border="0" align="center" cellpadding="0" cellspacing="3">
                                            <tr>
                                                <td height="120px">
                                                    <asp:Panel ID="Panel1" runat="server">
                                                        <uc2:jQueryPeopleTree ID="jQueryPeopleTree1" runat="server" />
                                                    </asp:Panel>
                                                </td>
                                                <td height="120px">
                                                    <asp:Panel ID="Panel2" runat="server">
                                                        <uc1:jQueryDepartTree ID="jQueryDepartTree1" runat="server" />
                                                    </asp:Panel>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td rowspan="2">
                                        <asp:ListBox ID="lbox_set" runat="server" Height="120px" Width="120px"></asp:ListBox>
                                    </td>
                                    <td>
                                        &nbsp;
                                        <asp:Button ID="Button1" runat="server" Text="&gt; &gt;" CssClass="b-input" OnClick="Button1_Click" />
                                        <br />
                                        <br />
                                    </td>
                                    <td rowspan="2" align="left">
                                        <asp:ListBox ID="lbox_noset" runat="server" Height="120px" Width="120px" DataSourceID="SqlDataSource1"
                                            DataTextField="rol_name" DataValueField="rol_no"></asp:ListBox>
                                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:NXEIPConnectionString %>"
                                            SelectCommand="SELECT rol_no, rol_name FROM role"></asp:SqlDataSource>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:RadioButton ID="rbl_dep" runat="server" Text="設定部門帳號" GroupName="G1" AutoPostBack="True"
                                            OnCheckedChanged="rbl_people_CheckedChanged" />
                                    </td>
                                    <td width="53">
                                        &nbsp;
                                        <asp:Button ID="Button2" runat="server" Text="&lt; &lt;" CssClass="b-input" OnClick="Button2_Click" />
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </td>
            </tr>
        </table>
        <div class="bottom">
            <asp:Button ID="but_ok" runat="server" CssClass="b-input" Text="確定" OnClick="but_ok_Click" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="but_cancel" runat="server" CssClass="a-input" Text="取消" OnClick="but_cancel_Click" />
        </div>
    </div>
</asp:Content>
