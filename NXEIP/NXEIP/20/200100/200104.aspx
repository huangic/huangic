<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="200104.aspx.cs" Inherits="_20_200100_200104" EnableEventValidation="false" %>

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
    <asp:ObjectDataSource ID="ObjectDataSource_Department" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetLevelOneDepartment" TypeName="NXEIP.DAO.DepartmentsDAO"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" EnablePaging="True" OldValuesParameterFormatString="original_{0}"
        SelectCountMethod="GetAllCount" SelectMethod="GetAll" TypeName="NXEIP.DAO.Doc06DAO">
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjectDataSource3" runat="server" EnablePaging="True" OldValuesParameterFormatString="original_{0}"
        SelectCountMethod="GetSearchDataCount" SelectMethod="GetSearchData" TypeName="NXEIP.DAO.Doc06DAO">
        <SelectParameters>
            <asp:Parameter DefaultValue="" Name="dep_no" Type="Int32" />
            <asp:Parameter DefaultValue="" Name="number" Type="String" />
            <asp:Parameter DefaultValue="" Name="file" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <uc1:Navigator ID="Navigator1" runat="server" SysFuncNo="200104" />
    
    <div class="select">
            <span class="a-letter-2">單位：
                <asp:DropDownList ID="ddl_unit" runat="server" DataSourceID="ObjectDataSource_Department"
                    DataTextField="dep_name" DataValueField="dep_no" AppendDataBoundItems="True">
                    <asp:ListItem Value="">請選擇</asp:ListItem>
                </asp:DropDownList>
                &nbsp;部門 ：<asp:DropDownList ID="ddl_department" runat="server">
                </asp:DropDownList>
                <asp:CascadingDropDown ID="ddl_department_CascadingDropDown" runat="server" Category="department"
                    ContextKey="unit" Enabled="True" LoadingText="載入中" ParentControlID="ddl_unit"
                    PromptText="請選擇" ServiceMethod="GetDropDownContents2" TargetControlID="ddl_department"
                    UseContextKey="True">
                </asp:CascadingDropDown>
                &nbsp; 文號：<span class="a-letter-1">
                    <asp:TextBox ID="tb_number" runat="server"></asp:TextBox>
                   
                </span>
                檔名：<span class="a-letter-1">
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
                    <input type="button" class="thickbox b-input" alt="200104-2.aspx?modal=true&TB_iframe=true&height=378&width=800"
                        value="新增公文附件" />
                </div>
            </div>
            <div class="h3">
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <cc1:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    CellPadding="3" CellSpacing="3" DataSourceID="ObjectDataSource3" EmptyDataText="查無資料"
                    GridLines="None" OnRowDataBound="GridView1_RowDataBound"
                     DataKeyNames="d06_no" 
                    onrowcommand="GridView1_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="發文單位">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# GetDepartmentName((Int32)Eval("d06_depno")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="d06_number" HeaderText="公文文號" />
                        <asp:TemplateField HeaderText="建檔人員">
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# new NXEIP.DAO.PeopleDAO().GetPeopleNameByUid((Int32)Eval("d06_peouid")) %>'></asp:Label>

                               

                                <uc2:PeopleDetail ID="PeopleDetail1" runat="server" peo_uid='<%# Eval("d06_peouid") %>'/>

                               

                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="建檔日期">
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%# new ChangeObject()._ADtoROC((DateTime)Eval("d06_date")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="電話(分機)">
                            <ItemTemplate>
                                <asp:Label ID="Label4" runat="server" Text='<%# string.Format("電話:{0}<br/>分機:{1}", Eval("d06_tel"), Eval("d06_ext")) %>'></asp:Label>
                            </ItemTemplate>



                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="是否公開">
                            <ItemTemplate>
                                <asp:Label ID="Label5" runat="server" Text='<%# ((string)Eval("d06_open")).Equals("1")?"否":"是" %>'></asp:Label>
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
                                                    NavigateUrl='<%#String.Format("200104-1.ashx?d06={0}&d07={1}",Eval("d06_no"),Eval("d07_no"))  %>'><span>下載</span></asp:HyperLink>
                                                <asp:Label ID="Label5" runat="server" Text='<%# String.Format("{0} (下載次數:{1})", Eval("d07_file"),Eval("d07_count")) %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" OldValuesParameterFormatString="original_{0}"
                                    SelectMethod="GetAllWithDoc06No" TypeName="NXEIP.DAO.Doc07DAO">
                                    <SelectParameters>
                                        <asp:Parameter Name="doc06_no" Type="Int32" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink2" runat="server" CssClass="thickbox imageButton edit" NavigateUrl='<%# string.Format("200104-3.aspx?id={0}&modal=true&TB_iframe=true&height=378&width=600",Eval("d06_no"))%>' Enabled='<%# GetModifyVisible((int)Eval("d06_peouid"))%>'><span>修改</span></asp:HyperLink>
                                <asp:LinkButton ID="LinkButton1" runat="server" CssClass=" imageButton delete"  CommandName="del" Enabled='<%# GetModifyVisible((int)Eval("d06_peouid"))%>' OnClientClick="return confirm('確定要刪除?')"><span>刪除</span></asp:LinkButton>
                            
                            
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
