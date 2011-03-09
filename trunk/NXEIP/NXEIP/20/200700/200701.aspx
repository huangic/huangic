<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="200701.aspx.cs" Inherits="_20_200700_200701" %>

<%@ Register Assembly="MattBerseth.WebControls" Namespace="MattBerseth.WebControls"
    TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../../lib/Navigator.ascx" TagName="Navigator" TagPrefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script type="text/javascript">
    function update(msg) {
        __doPostBack('<%=UpdatePanel1.ClientID%>', '');
        tb_remove();
        alert(msg);
    }

    function pageLoad(sender, args) {
        if (args.get_isPartialLoad()) {
            //  reapply the thick box stuff
            tb_init('a.thickbox');
        }
    }
    
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
        OldValuesParameterFormatString="original_{0}" 
        SelectCountMethod="Get_QAtypeDataCount" SelectMethod="Get_QAtypeData" 
        TypeName="NXEIP.DAO._200701DAO" EnablePaging="True"></asp:ObjectDataSource>
    <uc3:Navigator ID="Navigator1" runat="server" SysFuncNo="200701" />

    <div class="tableDiv">
        <div class="header">
            <div class="h1">
            </div>
            <div class="h2">
                <div class="function">
                    <input type="button" class="thickbox b-input" alt="200701-1.aspx?mode=new&TB_iframe=true&modal=true"
                        value="新增問答類別" />
                </div>
            </div>
            <div class="h3">
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <cc1:GridView ID="GridView1" runat="server" DataSourceID="ObjectDataSource1" AutoGenerateColumns="False"
                    Width="100%" AllowPaging="True" CellPadding="3" CellSpacing="3" GridLines="None"
                    OnRowCommand="GridView1_RowCommand" 
                    EmptyDataText="目前無資料" 
                    OnRowDataBound="GridView1_RowDataBound" 
                    DataKeyNames="qat_name,qat_self,qat_s06no,qat_r05no,qat_no">
                    <Columns>
                        <asp:BoundField DataField="qat_no" HeaderText="所屬類別" SortExpression="qat_no" >
                        <ItemStyle Width="15%" />
                        </asp:BoundField>

                        <asp:BoundField DataField="qat_name" HeaderText="問答類別名稱" 
                            SortExpression="qat_name">
                            <ItemStyle Width="20%" />
                        </asp:BoundField>

                        <asp:BoundField DataField="qat_note" HeaderText="問答類別說明" 
                            SortExpression="qat_note" >
                        <ItemStyle Width="35%" />
                        </asp:BoundField>

                        <asp:TemplateField HeaderText="設定管理者">
                            <ItemTemplate>
                                <a id="btnShowPopup" runat="server" class="thickbox imageButton edit" 
                                    href='<%# Eval("qat_no", "200701-2.aspx?qat_no={0}&modal=true&TB_iframe=true") %>'>
                                    <span>下載</span></a>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="10%" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="修改">
                            <ItemTemplate>
                                <a id="btnShowPopup2" runat="server" class="thickbox imageButton edit" 
                                    href='<%# Eval("qat_no", "200701-1.aspx?qat_no={0}&mode=modify&modal=true&TB_iframe=true") %>'>
                                    <span>下載</span></a>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="10%" />
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="刪除">
                            <ItemTemplate>
                                <asp:Button ID="Button1" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                    CommandName="del" OnClientClick=" return confirm('確定要刪除?')" CssClass="delete" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="10%" />
                        </asp:TemplateField>
                    </Columns>
                </cc1:GridView>
                <div class="footer">
                    <div class="f1">
                    </div>
                    <div class="f2">
                    </div>
                    <div class="f3">
                    </div>
                </div>
                <div class="pager">
                    <asp:DataPager ID="DataPager1" runat="server" PagedControlID="GridView1" PageSize="25">
                        <Fields>
                            <NXEIP:GooglePagerField />
                        </Fields>
                    </asp:DataPager>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

</asp:Content>

