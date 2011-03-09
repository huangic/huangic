﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="200702.aspx.cs" Inherits="_20_200700_200702" %>

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
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectCountMethod="Get_askDataCount" SelectMethod="Get_askData" TypeName="NXEIP.DAO._200702DAO"
        EnablePaging="True">
        <SelectParameters>
            <asp:Parameter Name="self" Type="String" />
            <asp:Parameter Name="qat_no" Type="Int32" />
            <asp:Parameter Name="key" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <uc3:Navigator ID="Navigator1" runat="server" SysFuncNo="200701" />
    
    <div class="tableDiv">
    
    </div>
    
    <div class="tableDiv">
        <div class="header">
            <div class="h1">
            </div>
            <div class="h2">
                <div class="function">
                    <input type="button" class="thickbox b-input" alt="200702-1.aspx?TB_iframe=true&modal=true"
                        value="我要發問" />
                </div>
            </div>
            <div class="h3">
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <cc1:GridView ID="GridView1" runat="server" DataSourceID="ObjectDataSource1" AutoGenerateColumns="False"
                    Width="100%" AllowPaging="True" CellPadding="3" CellSpacing="3" GridLines="None"
                    OnRowCommand="GridView1_RowCommand" EmptyDataText="目前無資料" OnRowDataBound="GridView1_RowDataBound"
                    DataKeyNames="ask_no">
                    <Columns>
                        
                        <asp:TemplateField HeaderText="問題">
                            <ItemTemplate>
                                <div><%# Eval("ask_question","問：{0}") %></div>
                                <div><%# Eval("ask_answer","答：{0}")%></div>
                            </ItemTemplate>
                            <ItemStyle Width="60%" />
                        </asp:TemplateField>

                        <asp:BoundField DataField="ask_date" HeaderText="發問時間" 
                            SortExpression="ask_date" DataFormatString="{0:yyyy-MM-dd HH:mm}">
                            <ItemStyle Width="10%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ask_rdate" HeaderText="回覆時間" 
                            SortExpression="ask_rdate" DataFormatString="{0:yyyy-MM-dd HH:mm}">
                            <ItemStyle Width="10%" />
                        </asp:BoundField>
                        <asp:BoundField DataField="ask_peouid" HeaderText="發問者" 
                            SortExpression="ask_peouid" >
                            <ItemStyle Width="10%" />
                        </asp:BoundField>
                        <asp:TemplateField HeaderText="回覆">
                            <ItemTemplate>
                                <a id="btnShowPopup2" runat="server" class="thickbox imageButton edit" href='<%# Eval("qat_no", "200701-1.aspx?qat_no={0}&mode=modify&modal=true&TB_iframe=true") %>'>
                                    <span>下載</span></a>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="5%" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="刪除">
                            <ItemTemplate>
                                <asp:Button ID="Button1" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                    CommandName="del" OnClientClick=" return confirm('確定要刪除?')" CssClass="delete" />
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Width="5%" />
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

