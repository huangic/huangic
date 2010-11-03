<%@ Page Language="C#" AutoEventWireup="true" CodeFile="200105-5.aspx.cs" Inherits="_20_200100_200105_5" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../../lib/CssLayout.ascx" TagName="CssLayout" TagPrefix="uc1" %>
<%@ Register Src="../../lib/Navigator.ascx" TagName="Navigator" TagPrefix="uc2" %>
<%@ Register Src="../../lib/calendar.ascx" TagName="calendar" TagPrefix="uc5" %>
<%@ Register Src="../../lib/SWFUpload/UC_SWFUpload.ascx" TagName="UC_SWFUpload" TagPrefix="uc4" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <uc1:CssLayout ID="CssLayout1" runat="server" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetAllWithDoc11No" TypeName="NXEIP.DAO.Doc13DAO">
        <SelectParameters>
            <asp:QueryStringParameter Name="doc11_no" QueryStringField="id" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <uc2:Navigator ID="Navigator1" runat="server" SysFuncNo="200105" SubFunc="已回傳檔案" />
    <div class="tableDiv">
        <asp:HiddenField ID="hidden_doc11no" runat="server" />
        <div class="header">
            <div class="h1">
            </div>
            <div class="h2">
            </div>
            <div class="h3">
            </div>
        </div>
        <table>
            <tbody>
                <tr>
                    <th>
                        主旨
                    </th>
                    <td>
                        <asp:Label ID="lb_subject" runat="server" Text="Label"></asp:Label>
                    </td>
                    <th>
                        上傳期限
                    </th>
                    <td>
                        <asp:Label ID="lb_edate" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th>
                        上傳單位
                    </th>
                    <td>
                        <asp:Label ID="lb_dep" runat="server" Text="Label"></asp:Label>
                    </td>
                    <th>
                        上傳人員
                    </th>
                    <td>
                        <asp:Label ID="lb_peo" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th>
                        適用單位
                    </th>
                    <td colspan="3">
                        <asp:Label ID="lb_use" runat="server" Text="Label"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th>
                        已回傳人員
                    </th>
                    <td colspan="3">
                        <asp:GridView ID="GridView1" runat="server"  AutoGenerateColumns="False" DataSourceID="ObjectDataSource1"
                            GridLines="None" CellPadding="0" CellSpacing="1" EmptyDataText="無回傳資料">
                            <Columns>
                                <asp:TemplateField HeaderText="單位">
                                    <ItemTemplate>
                                        <asp:Label ID="Label1" runat="server" Text='<%# GetDepartment((Int32)Eval("d13_depno")) %>'>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="姓名">
                                    <ItemTemplate>
                                        <asp:Label ID="Label2" runat="server" Text='<%# GetPeople((Int32)Eval("d13_peouid")) %>'>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="檔案">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HyperLink1" runat="server" CssClass="download imageButton" Target="_blank"
                                            NavigateUrl='<%#String.Format("200105-6.ashx?d11={0}&d13={1}",Eval("d11_no"),Eval("d13_no"))  %>'><span>下載</span></asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </tbody>
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
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="Button1" runat="server" CssClass="a-input" Text="回上一頁" OnClientClick="self.parent.tb_remove();" />
        </div>
    </div>
    </form>
</body>
</html>
