<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="200802.aspx.cs" Inherits="_20_200800_200802" EnableEventValidation="false" %>

<%@ Register Src="../../lib/Navigator.ascx" TagName="Navigator" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="MattBerseth.WebControls" Namespace="MattBerseth.WebControls"
    TagPrefix="cc1" %>
<%@ Register Src="../../lib/people/PeopleDetail.ascx" TagName="PeopleDetail" TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function update(msg) {

            __doPostBack('<%=UpdatePanel1.ClientID%>', '');
            tb_remove();


            //alert(msg);
        }

        function pageLoad(sender, args) {
            if (args.get_isPartialLoad()) {
                //  reapply the thick box stuff
                tb_init('a.thickbox,input.thickbox');
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:ObjectDataSource ID="ObjectDataSource_unm" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetSearchData"  SelectCountMethod="GetSearchDataCount" 
        TypeName="NXEIP.DAO.UnmarriedDAO" EnablePaging="True">
        <SelectParameters>
            <asp:Parameter Name="name" Type="String" />
            <asp:Parameter Name="sex" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <uc1:Navigator ID="Navigator1" runat="server" SysFuncNo="200802" />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <div class="select">
                    
                    
                    
                    <span class="a-letter-2">
                    姓名：<span class="a-letter-1">
                        <asp:TextBox ID="tb_name" runat="server"></asp:TextBox>
                    
                    
                    性別 <span class="a-letter-1">
                        <asp:DropDownList ID="ddl_sex" runat="server" AppendDataBoundItems="True">
                            <asp:ListItem Value="">全部</asp:ListItem>
                            <asp:ListItem Value="1">男</asp:ListItem>
                            <asp:ListItem Value="2">女</asp:ListItem>
                        </asp:DropDownList>
                    </span>
                        &nbsp;<asp:Button ID="btn_search" runat="server" Text="搜尋" CssClass="b-input"
                            CausesValidation="False" onclick="btn_search_Click" />
                    </span></span>
               
            </div>
            
                <div class="tableDiv">
                    <div class="header">
                        <div class="h1">
                        </div>
                        <div class="h2">
                            <div class="function">
                                <input type="button" class="thickbox b-input" alt="200802-1.aspx?modal=true&TB_iframe=true&height=450&width=620"
                                    value="新增未婚同仁" />
                            </div>
                        </div>
                        <div class="h3">
                        </div>
                    </div>
                    <cc1:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                        CellPadding="3" CellSpacing="3" DataSourceID="ObjectDataSource_unm" EmptyDataText="查無資料"
                        GridLines="None" DataKeyNames="unm_no" OnRowCommand="GridView1_RowCommand">
                        <Columns>
                            <asp:TemplateField HeaderText="服務機關">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# GetDepartmentName((Int32)Eval("unm_depno")) %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="職稱">
                                <ItemTemplate>
                                    <asp:Label ID="Label2" runat="server" Text='<%#GetTitleName((Int32)Eval("unm_typno")) %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="姓名">
                                <ItemTemplate>
                                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("unm_name") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="年齡">
                                <ItemTemplate>
                                    <asp:Label ID="Label4" runat="server" Text='<%# Eval("unm_age") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="身高">
                                <ItemTemplate>
                                    <asp:Label ID="Label5" runat="server" Text='<%# Eval("unm_height") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="體重">
                                <ItemTemplate>
                                    <asp:Label ID="Label6" runat="server" Text='<%# Eval("unm_weight") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="預覽">
                                <ItemTemplate>
                                    <a class="imageButton download" title='預覽' href='<%# Eval("unm_no", "200802-2.ashx?ID={0}") %>'>
                                        <span>預覽</span> </a>
                                    
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="修改" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <a id="btnShowPopup" runat="server" class="thickbox imageButton edit" title="修改"
                                        href='<%# String.Format("200802-1.aspx?modal=true&mode=edit&ID={0}&TB_iframe=true&height=450&width=600",Eval("unm_no")) %>'>
                                        <span>修改</span> </a>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="刪除">
                                <ItemTemplate>
                                    <asp:Button ID="Button1" runat="server" CommandArgument="<%# Container.DataItemIndex %>"
                                        CommandName="disable" OnClientClick=" return confirm('確定要刪除?')" CssClass="delete" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
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
                </div>
            
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
