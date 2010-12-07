<%@ Page Language="C#" AutoEventWireup="true" CodeFile="200402-2.aspx.cs" Inherits="_20_200400_200402_2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register src="../../lib/CssLayout.ascx" tagname="CssLayout" tagprefix="uc1" %>
<%@ Register src="../../lib/Navigator.ascx" tagname="Navigator" tagprefix="uc2" %>
<%@ Register Assembly="MattBerseth.WebControls" Namespace="MattBerseth.WebControls"
    TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <uc1:CssLayout ID="CssLayout1" runat="server" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:ObjectDataSource ID="ODS_1" runat="server" SelectMethod="GetDoc10FromE05" TypeName="NXEIP.DAO.Doc10DAO"
        EnablePaging="True" SelectCountMethod="GetDoc10FromE05Count" 
        OldValuesParameterFormatString="">
        <SelectParameters>
            <asp:Parameter Name="e02_no" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:HiddenField ID="hidd_no" runat="server" />
    <uc2:Navigator ID="Navigator1" runat="server" SubFunc="200402" />
    <div class="tableDiv">
        <div class="header">
            <div class="h1">
            </div>
            <div class="h2">
            </div>
            <div class="h3">
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <cc1:GridView ID="GridView1" runat="server" DataSourceID="ODS_1" AutoGenerateColumns="False"
                    Width="100%" AllowPaging="True" CellPadding="3" CellSpacing="3" GridLines="None"
                    OnRowCommand="GridView1_RowCommand" EnableViewState="False" EmptyDataText="目前無資料"
                    OnRowDataBound="GridView1_RowDataBound" DataKeyNames="d10_no">
                    <Columns>
                        <asp:BoundField DataField="d10_file" HeaderText="檔案名稱" SortExpression="d10_file" />
                        <asp:BoundField DataField="d10_count" HeaderText="下載次數" SortExpression="d10_count">
                            <ItemStyle HorizontalAlign="Center" Width="10%" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="下載">
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink1" runat="server" CssClass="download imageButton" Target="_blank"
                                    NavigateUrl='<%#String.Format("~/20/200100/200107-1.ashx?d09={1}&d10={0}",Eval("d10_no"),Eval("d09_no"))%>'><span>下載</span></asp:HyperLink>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="7%" />
                        </asp:TemplateField>
                    </Columns>
                </cc1:GridView>
                <div class="pager">
                    <asp:DataPager ID="DataPager1" runat="server" PagedControlID="GridView1" PageSize="10">
                        <Fields>
                            <NXEIP:GooglePagerField />
                        </Fields>
                    </asp:DataPager>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
        <div class="bottom">
            <asp:Button ID="btn_cancel" runat="server" CssClass="a-input" Text="關閉" OnClientClick="self.parent.tb_remove()"
                UseSubmitBehavior="false" />
        </div>
    </div>
    </form>
</body>
</html>
