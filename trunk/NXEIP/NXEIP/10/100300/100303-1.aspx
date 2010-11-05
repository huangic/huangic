<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="100303-1.aspx.cs" Inherits="_10_100300_100303_1" %>
<%@ Register src="../../lib/Navigator.ascx" tagname="Navigator" tagprefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register src="../../lib/tree/jQueryDepartTree.ascx" tagname="jQueryDepartTree" tagprefix="uc2" %>
<%@ Register src="../../lib/tree/jQueryPeopleTree.ascx" tagname="jQueryPeopleTree" tagprefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <uc1:navigator ID="Navigator1" runat="server" SysFuncNo="100302" />
    <div class="tableDiv">
        <div class="header">
            <div class="h1"></div>
            <div class="h2"><div class="name">新增查看權限</div></div>
            <div class="h3"></div>
        </div>
        <table>
            <tbody>
                <tr>
                    <th>查看權限設定</th>
                    <td>
                        <table border="0" cellpadding="0" cellspacing="0" style="width:300px">
                          <tr>
                            <td>
                                <asp:RadioButton ID="rb_1" runat="server" Checked="True" GroupName="rb" 
                                    Text="人事編號：" />
                              </td>
                            <td>
                                <asp:TextBox ID="txt_workid" runat="server"></asp:TextBox>
                              </td>
                          </tr>
                          <tr>
                            <td>
                                <asp:RadioButton ID="rb_2" runat="server" GroupName="rb" Text="請選擇人員：" />
                              </td>
                            <td>
                                <uc3:jQueryPeopleTree ID="jQueryPeopleTree1" runat="server" />
                              </td>
                          </tr>
                          <tr>
                            <td>
                                <asp:RadioButton ID="rb_3" runat="server" GroupName="rb" Text="請選擇單位：" />
                              </td>
                            <td>
                                <uc2:jQueryDepartTree ID="jQueryDepartTree1" runat="server" />
                              </td>
                          </tr>
                        </table>
                    </td>
                </tr>
                 <tr>
                    <th>權限</th>
                    <td>
                        
                        <asp:RadioButtonList ID="rbl_right" runat="server" 
                            RepeatDirection="Horizontal" RepeatLayout="Flow">
                            <asp:ListItem Value="1" Selected="True">全體</asp:ListItem>
                            <asp:ListItem Value="2">單位(含子部門)</asp:ListItem>
                        </asp:RadioButtonList>
                        
                        <asp:Label ID="lab_pageIndex" runat="server" Visible="False"></asp:Label>
                        
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
            <asp:Button ID="btn_submit" runat="server" CssClass="b-input" Text="確定" OnClick="btn_submit_Click" />
            &nbsp;&nbsp;
            <asp:Button ID="btn_cancel" runat="server" CssClass="a-input" Text="取消" 
                onclick="btn_cancel_Click" />
        </div>
        <div id="div_msg" runat="server">
        </div>
    </div>
</asp:Content>