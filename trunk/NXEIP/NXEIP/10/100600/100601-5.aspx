<%@ Page Language="C#" AutoEventWireup="true" CodeFile="100601-5.aspx.cs" Inherits="_10_100600_100601_5" %>

<%@ Register src="../../lib/Navigator.ascx" tagname="Navigator" tagprefix="uc1" %>
<%@ Register src="../../lib/CssLayout.ascx" tagname="CssLayout" tagprefix="uc2" %>
<%@ Register Assembly="MattBerseth.WebControls" Namespace="MattBerseth.WebControls"
    TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <uc2:CssLayout ID="CssLayout1" runat="server" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
        OldValuesParameterFormatString="original_{0}" 
        SelectMethod="Get_Conferen" TypeName="NXEIP.DAO._100601DAO">
        <SelectParameters>
            <asp:Parameter Name="mee_no" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" 
        OldValuesParameterFormatString="original_{0}" 
        SelectMethod="Get_Huiyi" TypeName="NXEIP.DAO._100601DAO">
        <SelectParameters>
            <asp:Parameter Name="mee_no" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <uc1:Navigator ID="Navigator1" runat="server" SysFuncNo="100601" SubFunc="檔案下載" />
    <asp:HiddenField ID="hidd_meeno" runat="server" />
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
            <tbody>
                <tr>
                    <th style="width: 15%">
                        開會事由
                    </th>
                    <td>
                        <asp:Label ID="lab_reason" runat="server"></asp:Label>
                    </td>
                    <th style="width: 15%">
                        開會地點
                    </th>
                    <td>
                        <asp:Label ID="lab_place" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th style="width: 15%">
                        開會日期
                    </th>
                    <td>
                        <asp:Label ID="lab_date" runat="server"></asp:Label>
                    </td>
                    <th style="width: 15%">
                        會議主持人
                    </th>
                    <td>
                        <asp:Label ID="lab_host" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th style="width:15%">
                        會議聯絡人
                    </th>
                    <td>
                        <asp:Label ID="lab_peoname" runat="server"></asp:Label>
                    </td>
                    <th style="width:15%">
                        聯絡人電話
                    </th>
                    <td>
                        <asp:Label ID="lab_tel" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th style="width: 15%">
                        會前資料列表
                    </th>
                    <td colspan="3">
                        <cc1:GridView ID="GridView2" runat="server" AutoGenerateColumns="False"
                            CellPadding="3" CellSpacing="3" DataSourceID="ObjectDataSource2" EmptyDataText="查無資料"
                            GridLines="None" 
                            DataKeyNames="mee_no,hui_no">
                            <Columns>
                                <asp:BoundField DataField="hui_file" HeaderText="會前資料名稱" 
                                    SortExpression="hui_file">
                                <ItemStyle Width="45%" />
                                </asp:BoundField>
                                <asp:BoundField DataField="hui_memo" HeaderText="檔案說明" 
                                    SortExpression="hui_memo">
                                <ItemStyle Width="45%" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="檢視">
                                    <ItemStyle HorizontalAlign="Center" Width="10%" />
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HyperLink1" runat="server" CssClass="thickbox imageButton edit"
                                            NavigateUrl='<%# string.Format("100601-4.ashx?type=1&mee_no={0}&hui_no={1}&modal=true&TB_iframe=true",Eval("mee_no"),Eval("hui_no"))%>'><span>回覆</span></asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                            </Columns>
                        </cc1:GridView>
                    </td>
                    
                </tr>
                <tr>
                    <th style="width: 15%">
                        會議紀錄列表
                    </th>
                    <td colspan="3">
                        <cc1:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                            CellPadding="3" CellSpacing="3" DataSourceID="ObjectDataSource1" EmptyDataText="查無資料"
                            GridLines="None" 
                            DataKeyNames="mee_no,con_no">
                            <Columns>
                                <asp:BoundField DataField="con_file" HeaderText="會議紀錄名稱" SortExpression="con_file">
                                <ItemStyle Width="90%" />
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="檢視">
                                    <ItemStyle HorizontalAlign="Center" Width="10%" />
                                    <ItemTemplate>
                                        <asp:HyperLink ID="HyperLink1" runat="server" CssClass="thickbox imageButton edit"
                                            NavigateUrl='<%# string.Format("100601-4.ashx?type=2&mee_no={0}&con_no={1}&modal=true&TB_iframe=true",Eval("mee_no"),Eval("con_no"))%>'><span>回覆</span></asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                
                            </Columns>
                        </cc1:GridView>
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
            <asp:Button ID="Button1" runat="server" CssClass="a-input" Text="關閉" OnClientClick="self.parent.tb_remove()"
                UseSubmitBehavior="false" />
        </div>
    </div>
    </form>
</body>
</html>
