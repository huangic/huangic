<%@ Page Language="C#" AutoEventWireup="true" CodeFile="100204-2.aspx.cs" Inherits="_10_100200_100204_2" %>

<%@ Register Src="../../lib/CssLayout.ascx" TagName="CssLayout" TagPrefix="uc1" %>
<%@ Register Src="../../lib/Navigator.ascx" TagName="Navigator" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <uc1:CssLayout ID="CssLayout1" runat="server" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetData" TypeName="NXEIP.DAO.New02DAO">
        <SelectParameters>
            <asp:Parameter Name="n01_no" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjectDataSource3" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetData" TypeName="NXEIP.DAO.New03DAO">
        <SelectParameters>
            <asp:Parameter Name="n01_no" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <uc2:Navigator ID="Navigator1" runat="server" SysFuncNo="100204" SubFunc="內容" />
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
                <th style="width: 15%">
                    發佈單位
                </th>
                <td>
                    <asp:Label ID="lab_dep" runat="server"></asp:Label>
                </td>
                <th style="width: 15%">
                    發佈人員
                </th>
                <td>
                    <asp:Label ID="lab_people" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th style="width: 15%">
                    類別
                </th>
                <td>
                    <div id="div_sfu_name" runat="server">
                    </div>
                </td>
                <th style="width: 15%">
                    發佈日期
                </th>
                <td>
                    <div id="div_date" runat="server">
                    </div>
                </td>
            </tr>
            <tr>
                <th style="width: 15%">
                    標題
                </th>
                <td>
                    <div id="div_subject" runat="server">
                    </div>
                </td>
                <th style="width: 15%">
                    發佈單位
                </th>
                <td>
                    <div id="div_use" runat="server">
                    </div>
                </td>
            </tr>
            <tr>
                <th style="width: 15%">
                    消息內容
                </th>
                <td colspan="3">
                    <div id="div_content" runat="server">
                    </div>
                </td>
            </tr>
            <tr>
                <th style="width: 15%">
                    相關檔案
                </th>
                <td colspan="3" class="select-3">
                    <asp:ListView ID="ListView1" runat="server" DataSourceID="ObjectDataSource2" DataKeyNames="n01_no,n02_no">
                        <ItemTemplate>
                            <ol>
                                <li class="ins"><a class="thickbox a-letter-s3" title="下載檔案" href='<%# string.Format("100204-3.ashx?n01_no={0}&n02_no={1}",Eval("n01_no"),Eval("n02_no")) %>'>
                                    <%# Eval("n02_file") %>
                                </a></li>
                            </ol>
                        </ItemTemplate>
                    </asp:ListView>
                </td>
            </tr>
            <tr>
                <th style="width: 15%">
                    相關網址
                </th>
                <td colspan="3" class="select-3">
                    <asp:ListView ID="ListView2" runat="server" DataSourceID="ObjectDataSource3">
                        <ItemTemplate>
                            <ol>
                                <li><a class="thickbox a-letter-s3" href='<%# Eval("n03_address") %>' target="_blank">
                                    <%# Eval("n03_address") %>
                                </a></li>
                            </ol>
                        </ItemTemplate>
                    </asp:ListView>
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
            <asp:Button ID="btn_cancel" runat="server" CssClass="a-input" Text="關閉" OnClientClick="self.parent.tb_remove()"
                UseSubmitBehavior="false" />
        </div>
    </div>
    </form>
</body>
</html>
