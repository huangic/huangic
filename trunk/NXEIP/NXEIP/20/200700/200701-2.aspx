<%@ Page Language="C#" AutoEventWireup="true" CodeFile="200701-2.aspx.cs" Inherits="_20_200700_200701_2" %>

<%@ Register Assembly="MattBerseth.WebControls" Namespace="MattBerseth.WebControls"
    TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../../lib/CssLayout.ascx" TagName="CssLayout" TagPrefix="uc1" %>
<%@ Register Src="../../lib/Navigator.ascx" TagName="Navigator" TagPrefix="uc2" %>

<%@ Register src="../../lib/tree/DepartTreeListBox.ascx" tagname="DepartTreeListBox" tagprefix="uc3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <uc1:CssLayout ID="CssLayout1" runat="server" />
    <script type="text/javascript" src="../../js/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="../../js/thickbox.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
        OldValuesParameterFormatString="original_{0}" SelectMethod="Get_QAManager" 
        TypeName="NXEIP.DAO._200701DAO" EnablePaging="True" 
        SelectCountMethod="Get_QAManagerCount">
        <SelectParameters>
            <asp:Parameter Name="qat_no" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    
    <uc2:Navigator ID="Navigator1" runat="server" SysFuncNo="200701" />
    <div class="tableDiv">
        <asp:HiddenField ID="hidden_no" runat="server" />
        <div class="header">
            <div class="h1">
            </div>
            <div class="h2">
                <div class="name">
                    <asp:Label ID="lab_name" runat="server" ></asp:Label>
                </div>
            </div>
            <div class="h3">
            </div>
        </div>
        <table>
            <tbody>
                <tr>
                    <th style="width:15%">
                        管理員列表
                    </th>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                <cc1:GridView ID="GridView1" runat="server" DataSourceID="ObjectDataSource1" AutoGenerateColumns="False"
                                    Width="100%" AllowPaging="True" CellPadding="3" CellSpacing="3" GridLines="None"
                                    OnRowCommand="GridView1_RowCommand" EmptyDataText="目前無資料" 
                                    OnRowDataBound="GridView1_RowDataBound" DataKeyNames="qam_peouid">
                                    <Columns>
                                        <asp:BoundField DataField="qam_peouid" HeaderText="人員名稱" 
                                            SortExpression="qam_peouid">
                                            <ItemStyle HorizontalAlign="Center" Width="20%" />
                                        </asp:BoundField>
                                        
                                        
                                        <asp:TemplateField HeaderText="刪除">
                                            <ItemTemplate>
                                                <asp:Button ID="Button1" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                                    CommandName="del" OnClientClick=" return confirm('確定要刪除?')" CssClass="delete" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" Width="10%" />
                                        </asp:TemplateField>
                                    </Columns>
                                </cc1:GridView>
                                <div class="pager">
                                    <asp:DataPager ID="DataPager1" runat="server" PagedControlID="GridView1" PageSize="25">
                                        <Fields>
                                            <NXEIP:GooglePagerField />
                                        </Fields>
                                    </asp:DataPager>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                </tr>
                <tr>
                    <th>
                        選擇人員
                    </th>
                    <td>
                        
                        <uc3:DepartTreeListBox ID="DepartTreeListBox1" runat="server" 
                            LeafType="People" />
                        
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
            <asp:Button ID="btn_ok" runat="server" CssClass="b-input" Text="確定" OnClick="btn_ok_Click" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btn_cancel" runat="server" CssClass="a-input" Text="取消" OnClientClick="self.parent.tb_remove()"
                UseSubmitBehavior="false" />
        </div>
    </div>
    </form>
</body>
</html>
