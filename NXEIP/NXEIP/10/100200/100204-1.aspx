<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="100204-1.aspx.cs" Inherits="_10_100200_100204_1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Register src="../../lib/Navigator.ascx" tagname="Navigator" tagprefix="uc1" %>

<%@ Register src="../../lib/SWFUpload/UC_SWFUpload.ascx" tagname="UC_SWFUpload" tagprefix="uc2" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
        OldValuesParameterFormatString="original_{0}" SelectMethod="GetS06FromSufNOAll" 
        TypeName="NXEIP.DAO.Sys06DAO">
        <SelectParameters>
            <asp:Parameter Name="sfu_no" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" 
        OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" 
        TypeName="NXEIP.DAO.New02DAO">
        <SelectParameters>
            <asp:Parameter Name="n01_no" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:HiddenField ID="hidd_no" runat="server" />
    <asp:HiddenField ID="hidd_newno" runat="server" />
<uc1:Navigator ID="Navigator1" runat="server" SysFuncNo="100204" />
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
                <th style="width:15%">
                發佈單位
                </th>
                <td>
                    <asp:Label ID="lab_dep" runat="server" ></asp:Label>
                </td>
                <th style="width:15%">
                發佈人員
                </th>
                <td>
                    <asp:Label ID="lab_people" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th style="width:15%">
                類別
                </th>
                <td>
                    <asp:DropDownList ID="ddl_sys06" runat="server" AppendDataBoundItems="True" 
                        DataSourceID="ObjectDataSource1" DataTextField="s06_name" 
                        DataValueField="s06_no">
                        <asp:ListItem Selected="True" Value="0">最新消息</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <th style="width:15%">
                發佈日期
                </th>
                <td>
                    <asp:Label ID="lab_date" runat="server" ></asp:Label>
                </td>
            </tr>
            <tr>
                <th style="width:15%">
                標題
                </th>
                <td>
                    <asp:TextBox ID="tbox_subject" runat="server" MaxLength="200" Width="300px"></asp:TextBox>
                </td>
                <th style="width:15%">
                適用單位
                </th>
                <td>
                    <asp:RadioButtonList ID="rbl_use" runat="server" RepeatDirection="Horizontal" 
                        RepeatLayout="Flow">
                        <asp:ListItem Selected="True" Value="1">單位</asp:ListItem>
                        <asp:ListItem Value="2">全府</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <th style="width:15%">
                消息內容
                </th>
                <td valign="middle">
                    <asp:TextBox ID="tbox_content" runat="server" Height="125px" TextMode="MultiLine" 
                        Width="100%"></asp:TextBox>
                </td>
                <th style="width:15%">
                相關網址
                </th>
                <td>
                    <ol>
                        <li>
                            <asp:TextBox ID="tbox_http_1" runat="server" Width="270px" MaxLength="125">http://</asp:TextBox></li>
                        <li>
                            <asp:TextBox ID="tbox_http_2" runat="server" Width="270px" MaxLength="125">http://</asp:TextBox></li>
                        <li>
                            <asp:TextBox ID="tbox_http_3" runat="server" Width="270px" MaxLength="125">http://</asp:TextBox></li>
                        <li>
                            <asp:TextBox ID="tbox_http_4" runat="server" Width="270px" MaxLength="125">http://</asp:TextBox></li>
                        <li>
                            <asp:TextBox ID="tbox_http_5" runat="server" Width="270px" MaxLength="125">http://</asp:TextBox></li>
                    </ol>
                </td>
            </tr>
            <tr>
                <th style="width:15%">
                相關檔案
                <br/><asp:Label ID="lb_size" runat="server" Text="Label"></asp:Label>
                </th>
                <td colspan="3">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <div id="div_file" runat="server">
                                <asp:ListView ID="ListView1" runat="server" DataSourceID="ObjectDataSource2" 
                                    DataKeyNames="n01_no,n02_no" onitemcommand="ListView1_ItemCommand">
                                    <ItemTemplate>
                                        <li>
                                            <asp:Label ID="Label3" runat="server" Text='<%# Eval("n02_file") %>'></asp:Label>
                                            <asp:LinkButton ID="LinkButton1" runat="server" OnClientClick=" return confirm('確定要刪除?')"
                                                CssClass="imageButton delete" CommandName="del"><span>刪除</span></asp:LinkButton>
                                        </li>
                                    </ItemTemplate>
                                </asp:ListView>
                            </div>
                            <div>
                                <uc2:UC_SWFUpload ID="UC_SWFUpload1" runat="server" />
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
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
            <asp:Button ID="Button1" runat="server" Text="確定" CssClass="b-input" 
                onclick="Button1_Click" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="Button2" runat="server" Text="取消" CssClass="a-input" 
                onclick="Button2_Click" />
        </div>
    </div>
</asp:Content>

