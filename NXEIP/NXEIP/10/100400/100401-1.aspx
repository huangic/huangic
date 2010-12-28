<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="100401-1.aspx.cs" Inherits="_10_100400_100401_1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="MattBerseth.WebControls" Namespace="MattBerseth.WebControls" TagPrefix="cc1" %>
<%@ Register Src="../../lib/Navigator.ascx" TagName="Navigator" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <uc1:Navigator ID="Navigator1" runat="server" SysFuncNo="100401" />
    <div class="tableDiv">
        <div class="inquire">
        <div class="header">
            <div class="h1">
            </div>
            <div class="h2">
                <div class="name">問卷填寫</div>
                <div class="function">&nbsp;</div>
            </div>
            <div class="h3">
            </div>
        </div>
            <div class="boxA">
                <div class="head"><asp:Label ID="lab_quename" runat="server" CssClass="a02-19"></asp:Label></div>
                <div class="box">
                    <div class="content">
                        <div class="b2">
                            <span class="a-letter-t2">問卷說明：<asp:Label ID="lab_descript" runat="server"></asp:Label></span>
                        </div>
                        <div class="b2">
                            <span class="a-letter-t2">填寫期限：<asp:Label ID="lab_sdate" runat="server"></asp:Label>&nbsp;～
                                <asp:Label ID="lab_edate" runat="server"></asp:Label>
                            </span>
                        </div>
                    </div>
                    <asp:DataList ID="DataList1" runat="server" DataSourceID="SqlDataSource1" RepeatLayout="Flow" OnItemDataBound="DataList1_ItemDataBound" DataKeyField="the_no">
                        <ItemTemplate>
                            <div class="content">
                                <div class="b2">
                                    <li class="ps1"><asp:Label ID="the_nameLabel" runat="server" Text='<%# Eval("the_name") %>' CssClass="a-letter-t1" />
                                        <asp:Label ID="lab_type" runat="server" CssClass="a-letter-t1" Text='<%# Eval("the_type") %>' Visible="False" />
                                    </li>
                                </div>
                                <asp:Label ID="lab_answer" runat="server"></asp:Label>
                            </div>
                        </ItemTemplate>
                    </asp:DataList>
                    <div class="content">
                        <div class="b2">
                            <span class="a-letter-t2">
                                <asp:Label ID="lab_end" runat="server"></asp:Label><asp:Label ID="lab_no" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lab_count" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lab_total" runat="server" Visible="False">0</asp:Label>
                            </span>
                        </div>
                    </div>
                </div>
            </div>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:NXEIPConnectionString %>"
                SelectCommand="SELECT the_no, the_name, the_type FROM theme WHERE (que_no = @que_no) AND (the_status = '1') ORDER BY the_order">
                <SelectParameters>
                    <asp:QueryStringParameter DefaultValue="" Name="que_no" QueryStringField="no" />
                </SelectParameters>
            </asp:SqlDataSource>
            <div class="footer">
                <div class="f1">
                </div>
                <div class="f2">
                </div>
                <div class="f3">
                </div>
            </div>
            <div class="bottom">
                <asp:Button ID="btn_submit" runat="server" CssClass="b-input" Text="確定送出" 
                    OnClick="btn_submit_Click" />&nbsp;&nbsp;
                <asp:Button ID="btn_cancel" runat="server" CssClass="b-input" Text="取消重填" 
                    OnClick="btn_cancel_Click" />&nbsp;&nbsp;
                <asp:Button ID="btn_goback" runat="server" CssClass="a-input" Text="回上一頁" OnClick="btn_goback_Click" />
            </div>
        </div>
    </div>
</asp:Content>
