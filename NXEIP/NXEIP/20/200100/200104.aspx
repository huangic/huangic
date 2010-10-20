<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="200104.aspx.cs" Inherits="_20_200100_200104" %>

<%@ Register src="../../lib/Navigator.ascx" tagname="Navigator" tagprefix="uc1" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>

<%@ Register assembly="MattBerseth.WebControls" namespace="MattBerseth.WebControls" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>

    <asp:ObjectDataSource ID="ObjectDataSource_Department" runat="server" 
        OldValuesParameterFormatString="original_{0}" 
        SelectMethod="GetLevelOneDepartment" TypeName="NXEIP.DAO.DepartmentsDAO">
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjectDataSourceDoc06" runat="server" 
        EnablePaging="True" OldValuesParameterFormatString="original_{0}" 
        SelectCountMethod="GetAllCount" SelectMethod="GetAll" 
        TypeName="NXEIP.DAO.Doc06DAO"></asp:ObjectDataSource>


<uc1:Navigator ID="Navigator1" runat="server" SysFuncNo="200104" />

<div class="tableDiv">
 
<div>
    <span class="a-letter-2">單位：
                     
    <asp:DropDownList ID="ddl_unit" runat="server" 
        DataSourceID="ObjectDataSource_Department" DataTextField="dep_name" 
        DataValueField="dep_no" AppendDataBoundItems="True">
        <asp:ListItem>請選擇</asp:ListItem>
    </asp:DropDownList>
    
&nbsp;部門
                    ：<asp:DropDownList ID="ddl_department" runat="server">
    </asp:DropDownList>
    <asp:CascadingDropDown ID="ddl_department_CascadingDropDown" runat="server" 
        Category="department" ContextKey="unit" Enabled="True" LoadingText="載入中" 
        ParentControlID="ddl_unit" PromptText="請選擇" 
        ServiceMethod="GetDropDownContents2" TargetControlID="ddl_department" 
        UseContextKey="True">
    </asp:CascadingDropDown>
&nbsp;
                關鍵字：<span class="a-letter-1">
                <asp:TextBox ID="tb_word" runat="server"></asp:TextBox>
&nbsp;<asp:Button ID="Button1" runat="server" Text="搜尋"  CssClass="b-input"/>

                </span></span>
</div>


    <div class="header">
        <div class="h1"></div>
        <div class="h2">
            <div class="function">
             <input type="button" class="thickbox b-input" alt="200104-1.aspx?modal=true&TB_iframe=true&height=378&width=600"
                        value="新增公文附件" />
            </div>
        </div>
        <div class="h3"></div>
    </div>

    <cc1:GridView ID="GridView1" runat="server" AllowPaging="True" 
        AutoGenerateColumns="False" CellPadding="3" CellSpacing="3" 
        DataSourceID="ObjectDataSourceDoc06" EmptyDataText="查無資料" GridLines="None">
        <Columns>
            <asp:BoundField DataField="d06_depno" HeaderText="發文單位" 
                SortExpression="d06_depno" />
            <asp:DynamicField DataField="d06_number" HeaderText="公文文號" />
            <asp:BoundField DataField="d06_peouid" HeaderText="建檔人員" 
                SortExpression="d06_peouid" />
             <asp:TemplateField HeaderText="建檔日期">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" 
                        Text='<%# new ChangeObject()._ADtoROC((DateTime)Eval("d06_date")) %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
            
            <asp:TemplateField HeaderText="電話(分機)">
                <ItemTemplate>
                    <asp:Label ID="Label1" runat="server" 
                        Text='<%# string.Format("電話:{0}<br/>分機:{1}", Eval("d06_tel"), Eval("d06_ext")) %>'></asp:Label>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        </cc1:GridView>


    <div class="footer">
    <div class="f1">
        
        </div>
    <div class="f2">
        
        </div>
    <div class="f3"></div>
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

