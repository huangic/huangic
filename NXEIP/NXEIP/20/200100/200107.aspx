<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="200107.aspx.cs" Inherits="_20_200100_200107" EnableEventValidation="false" %>

<%@ Register Src="../../lib/Navigator.ascx" TagName="Navigator" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="MattBerseth.WebControls" Namespace="MattBerseth.WebControls"
    TagPrefix="cc1" %>
<%@ Register src="../../lib/people/PeopleDetail.ascx" tagname="PeopleDetail" tagprefix="uc2" %>
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
                tb_init('a.thickbox');
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    
    <asp:ObjectDataSource ID="ObjectDataSource3" runat="server" EnablePaging="True" OldValuesParameterFormatString="original_{0}"
        SelectCountMethod="GetSearchDataCount" SelectMethod="GetSearchData" TypeName="NXEIP.DAO.Doc09DAO">
        <SelectParameters>
            <asp:Parameter DefaultValue="" Name="dep_no" Type="Int32" />
            <asp:Parameter DefaultValue="" Name="cat_no" Type="Int32" />
            <asp:Parameter DefaultValue="" Name="file" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <uc1:Navigator ID="Navigator1" runat="server" SysFuncNo="200107" />
    
    <div class="select">
            <span class="a-letter-2">檔名：<span class="a-letter-1">
                    <asp:TextBox ID="tb_file" runat="server"></asp:TextBox>
                     &nbsp;<asp:Button ID="Button1" runat="server" Text="搜尋" CssClass="b-input" CausesValidation="False"
                        OnClick="Button1_Click" />
                </span>
                
                
                </span>
        </div>
    
    
    
    
    <div class="tableDiv">
        
        <div class="header">
            <div class="h1">
            </div>
            <div class="h2">
                <div class="function">
                    <input type="button" class="thickbox b-input" alt="200107-2.aspx?modal=true&TB_iframe=true&height=378&width=600"
                        value="新增檔案" />
                </div>
            </div>
            <div class="h3">
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <cc1:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    CellPadding="3" CellSpacing="3" DataSourceID="ObjectDataSource3" EmptyDataText="查無資料"
                    GridLines="None" OnRowDataBound="GridView1_RowDataBound" EnableViewState="False">
                    <Columns>
                        <asp:TemplateField HeaderText="檔案類別">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# GetCatName((Int32)Eval("s06_no")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField HeaderText="檔案類別">
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# GetCatChildName((Int32)Eval("s06_no")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:BoundField DataField="d09_note" HeaderText="使用說明" />
                       
                       
                        <asp:TemplateField HeaderText="上傳日期">
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%# new ChangeObject()._ADtoROC((DateTime)Eval("d09_date")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>


                         <asp:TemplateField HeaderText="上傳單位">
                            <ItemTemplate>
                                <asp:Label ID="Label4" runat="server" Text='<%# GetDepartmentName((Int32)Eval("d09_depno")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                     
                         <asp:TemplateField HeaderText="上傳人員">
                            <ItemTemplate>
                                <asp:Label ID="Label5" runat="server" Text='<%# new NXEIP.DAO.PeopleDAO().GetPeopleNameByUid((Int32)Eval("d09_peouid")) %>'></asp:Label>

                               

                                <uc2:PeopleDetail ID="PeopleDetail1" runat="server" peo_uid='<%# Eval("d09_peouid") %>'/>

                               

                            </ItemTemplate>
                        </asp:TemplateField>





                        <asp:TemplateField HeaderText="附件">
                            <ItemTemplate>
                                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataSource2"
                                    GridLines="None" ShowHeader="False">
                                    <Columns>
                                        <asp:TemplateField ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="HyperLink1" runat="server" CssClass="download imageButton" Target="_blank"
                                                    NavigateUrl='<%#String.Format("200104-1.ashx?d06={0}&d07={1}",Eval("d09_no"),Eval("d10_no"))  %>'><span>下載</span></asp:HyperLink>
                                                <asp:Label ID="Label5" runat="server" Text='<%# String.Format("{0} (下載次數:{1})", Eval("d10_file"),Eval("d10_count")) %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" OldValuesParameterFormatString="original_{0}"
                                    SelectMethod="GetAllWithDoc09No" TypeName="NXEIP.DAO.Doc10DAO">
                                    <SelectParameters>
                                        <asp:Parameter Name="doc09_no" Type="Int32" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink2" runat="server" CssClass="thickbox imageButton edit" NavigateUrl='<%# string.Format("200107-3.aspx?id={0}&modal=true&TB_iframe=true&height=378&width=600",Eval("d09_no"))%>' Enabled='<%# GetModifyVisible((int)Eval("d09_peouid"))%>'><span>修改</span></asp:HyperLink>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </cc1:GridView>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
        <div class="footer">
            <div class="f1">
            </div>
            <div class="f2">
            </div>
            <div class="f3">
            </div>
        </div>
        <div class="pager">
            <asp:DataPager ID="DataPager1" runat="server" PagedControlID="GridView1" PageSize="10">
                <Fields>
                    <asp:NextPreviousPagerField ShowNextPageButton="False" />
                    <asp:NumericPagerField />
                    <asp:NextPreviousPagerField ShowPreviousPageButton="False" />
                </Fields>
            </asp:DataPager>
        </div>
    </div>
</asp:Content>
