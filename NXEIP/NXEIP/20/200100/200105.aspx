<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="200105.aspx.cs" Inherits="_20_200100_200105" EnableEventValidation="false" %>

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
                tb_init('.thickbox');
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    
    <asp:ObjectDataSource ID="ObjectDataSource_d11" runat="server" EnablePaging="True" OldValuesParameterFormatString="original_{0}"
        SelectCountMethod="GetSearchDataCount" SelectMethod="GetSearchData" TypeName="NXEIP.DAO.Doc11DAO">
        <SelectParameters>
            <asp:Parameter DefaultValue="" Name="subject" Type="String" />
            <asp:Parameter DefaultValue="" Name="file" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <uc1:Navigator ID="Navigator1" runat="server" SysFuncNo="200105" />
    
    <div class="select">
            <span class="a-letter-2">主旨：
               
              
               <span class="a-letter-1">
                    <asp:TextBox ID="tb_subject" runat="server"></asp:TextBox>
                   
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
                    <input type="button" class="thickbox b-input" alt="200105-2.aspx?modal=true&TB_iframe=true&height=400&width=700"
                        value="新增回傳檔案" />
                </div>
            </div>
            <div class="h3">
            </div>
        </div>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <cc1:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    CellPadding="3" CellSpacing="3" DataSourceID="ObjectDataSource_d11" EmptyDataText="查無資料"
                    GridLines="None" OnRowDataBound="GridView1_RowDataBound"
                    EnableViewState="False" DataKeyNames="d11_no" 
                    onrowcommand="GridView1_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="d11_subject" HeaderText="主旨" 
                            SortExpression="d11_subject" />
                        <asp:TemplateField HeaderText="上傳日期&lt;br/&gt;截止日期">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# new ChangeObject()._ADtoROC((DateTime)Eval("d11_date")) %>'></asp:Label>
                               <br />
                                <asp:Label ID="Label2"  CssClass="a-letter-Red" runat="server" Text='<%# new ChangeObject()._ADtoROC((DateTime)Eval("d11_edate")) %>'></asp:Label>
                            </ItemTemplate>


                        </asp:TemplateField>
                          <asp:TemplateField HeaderText="上傳單位">
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%# GetDepartmentName((Int32)Eval("d11_depno")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>






                        <asp:TemplateField HeaderText="人員&lt;br/&gt;電話(分機)">
                            <ItemTemplate>
                                <asp:Label ID="Label4" runat="server" Text='<%# new NXEIP.DAO.PeopleDAO().GetPeopleNameByUid((Int32)Eval("d11_peouid")) %>'></asp:Label>
                                <br />
                                <asp:Label ID="Label6" runat="server" Text='<%# string.Format("電話:{0}<br/>分機:{1}",Eval("d11_tel"), Eval("d11_ext")) %>'></asp:Label>
                            </ItemTemplate>



                        </asp:TemplateField>
                        <asp:BoundField DataField="d11_use" HeaderText="適用單位" 
                            SortExpression="d11_use" />

                        <asp:TemplateField HeaderText="下載">
                            <ItemTemplate>
                                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" 
                                    DataSourceID="ObjectDataSource2" GridLines="None" ShowHeader="False">
                                    <Columns>
                                        <asp:TemplateField ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="HyperLink1" runat="server" CssClass="download imageButton" 
                                                    NavigateUrl='<%#String.Format("200105-1.ashx?d11={0}&d07={1}",Eval("d11_no"),Eval("d12_no"))  %>' 
                                                    Target="_blank"><span>下載</span></asp:HyperLink>
                                                <asp:Label ID="Label5" runat="server" 
                                                    Text='<%# String.Format("{0} (下載次數:{1})", Eval("d12_file"),Eval("d12_count")) %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" 
                                    OldValuesParameterFormatString="original_{0}" SelectMethod="GetAllWithDoc11No" 
                                    TypeName="NXEIP.DAO.Doc12DAO">
                                    <SelectParameters>
                                        <asp:Parameter Name="doc11_no" Type="Int32" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="我要回傳">
                          <ItemTemplate>
                             
                             
                              <asp:HyperLink ID="HyperLink3"  CssClass="thickbox b-input" NavigateUrl='<%# string.Format("200105-4.aspx?id={0}&modal=true&TB_iframe=true&height=400&width=700",Eval("d11_no"))%>' runat="server">我要回傳</asp:HyperLink>
                             
                             
                              
                                                         
                               
                              
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="已回傳">
                            <ItemTemplate>
                             
                             
                              <asp:HyperLink ID="HyperLink4"  CssClass="thickbox b-input" NavigateUrl='<%# string.Format("200105-5.aspx?id={0}&modal=true&TB_iframe=true&height=400&width=700",Eval("d11_no"))%>' runat="server">已回傳</asp:HyperLink>
                             
                             
                              
                                                         
                               
                              
                            </ItemTemplate>

                        </asp:TemplateField>
                       <asp:TemplateField>
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink2" runat="server" CssClass="thickbox imageButton edit" NavigateUrl='<%# string.Format("200105-3.aspx?id={0}&modal=true&TB_iframe=true&height=378&width=600",Eval("d11_no"))%>' Enabled='<%# GetModifyVisible((int)Eval("d11_peouid"))%>'><span>修改</span></asp:HyperLink>
                                <asp:LinkButton ID="LinkButton1" runat="server" CssClass=" imageButton delete"  CommandName="del" Enabled='<%# GetModifyVisible((int)Eval("d11_peouid"))%>' OnClientClick="return confirm('確定要刪除?')"><span>刪除</span></asp:LinkButton>
                            
                            
                            </ItemTemplate>
                            </asp:TemplateField>
                    </Columns>
                </cc1:GridView>
            </ContentTemplate>
            
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
