<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="301102.aspx.cs" Inherits="_30_301100_301102" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register Assembly="MattBerseth.WebControls" Namespace="MattBerseth.WebControls"
    TagPrefix="cc1" %>
<%@ Register Src="../../lib/Navigator.ascx" TagName="Navigator" TagPrefix="uc1" %>
<%@ Register Src="../../lib/ImageUpload.ascx" TagName="ImageUpload" TagPrefix="uc5" %>
<%@ Register Src="../../lib/tree/DepartTreeTextBox.ascx" TagName="DepartTreeTextBox"
    TagPrefix="uc3" %>
<%@ Register Src="../../lib/calendar.ascx" TagName="calendar" TagPrefix="uc6" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <ajaxtoolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" CombineScripts="False">
    </ajaxtoolkit:ToolkitScriptManager>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" EnablePaging="True" SelectCountMethod="GetAllCount"
        SelectMethod="GetAll" TypeName="NXEIP.DAO.M02DAO" OldValuesParameterFormatString="original_{0}">
    </asp:ObjectDataSource>
    <script type="text/javascript" src="../../js/lytebox.js"></script>
    <asp:Label ID="lab_no" runat="server" Visible="False"></asp:Label><asp:Label ID="lab_mode"
        runat="server" Visible="False"></asp:Label><asp:Label ID="lab_pageIndex" runat="server"
            Visible="False"></asp:Label>
    <uc1:Navigator ID="Navigator1" runat="server" SysFuncNo="301102" />
    <asp:MultiView ID="MultiView1" runat="server">
        <asp:View ID="List" runat="server">
            <div class="tableDiv">
                <div class="header">
                    <div class="h1">
                    </div>
                    <div class="h2">
                        <div class="function">
                            <asp:Button ID="btn_add" runat="server" Text="新增車輛" CssClass="b-input" OnClick="btn_add_Click" />
                        </div>
                    </div>
                    <div class="h3">
                    </div>
                </div>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <cc1:GridView ID="GridView1" runat="server" DataSourceID="ObjectDataSource1" AllowPaging="True"
                            AutoGenerateColumns="False" CellPadding="3" CellSpacing="3" CssClass="tableData"
                            EmptyDataText="查無資料" DataKeyNames="m02_no" OnRowDataBound="GridView1_RowDataBound"
                            GridLines="None" OnRowCommand="GridView1_RowCommand">
                            <Columns>
                                <asp:BoundField DataField="m02_chekuan" HeaderText="車別" SortExpression="m02_chekuan" />
                                <asp:BoundField DataField="m02_number" HeaderText="排照號碼" SortExpression="m02_number" />
                                <asp:BoundField DataField="m02_code" HeaderText="車輛編號" SortExpression="m02_code" />
                                <asp:BoundField DataField="m02_peouid" HeaderText="保管人" SortExpression="m02_peouid" />
                                <asp:BoundField DataField="m02_mark" HeaderText="廠牌" SortExpression="m02_mark" />
                                <asp:BoundField DataField="m02_sdate" DataFormatString="{0:yyyy-MM-dd}" HeaderText="發照日期"
                                    SortExpression="m02_sdate" />
                                <asp:BoundField DataField="m02_change" DataFormatString="{0:yyyy-MM-dd}" HeaderText="換照日期"
                                    SortExpression="m02_change" />
                                <asp:BoundField DataField="m02_testdate" DataFormatString="{0:yyyy-MM-dd}" HeaderText="檢驗日期"
                                    SortExpression="m02_testdate" />
                                <asp:TemplateField HeaderText="修改">
                                    <ItemTemplate>
                                        <asp:Button ID="Button2" runat="server" CommandName="modify" CommandArgument="<%# Container.DataItemIndex %>"
                                            CssClass="edit" /></ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="35px" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="刪除">
                                    <ItemTemplate>
                                        <asp:Button ID="Button3" runat="server" CommandName="del" CommandArgument="<%# Container.DataItemIndex %>"
                                            CssClass="delete" OnClientClick=" return confirm('確定要刪除?')" /></ItemTemplate>
                                    <HeaderStyle HorizontalAlign="Center" />
                                    <ItemStyle HorizontalAlign="Center" Width="35px" />
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle HorizontalAlign="Left" />
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
                        <div id="div_msg" runat="server">
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </asp:View>
        <asp:View ID="Modify" runat="server">
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
                        <th style="width: 120px">
                            <span class="a-letter-Red">*</span> 車輛編號
                        </th>
                        <td>
                            <asp:TextBox ID="txt_code" runat="server"></asp:TextBox>
                        </td>
                        <th style="width: 120px">
                            <span class="a-letter-Red">*</span> 保 管 人
                        </th>
                        <td>
                            <uc3:DepartTreeTextBox ID="DepartTreeTextBox1" runat="server" LeafType="People" PeopleType="All" />
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <span class="a-letter-Red">*</span> 牌照種類
                        </th>
                        <td>
                            <asp:DropDownList ID="ddl_platoon" runat="server">
                            </asp:DropDownList>
                        </td>
                        <th>
                            <span class="a-letter-Red">*</span> 牌照號碼
                        </th>
                        <td>
                            <asp:TextBox ID="txt_number" runat="server" Columns="18" MaxLength="20"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <span class="a-letter-Red">*</span> 發照日期
                        </th>
                        <td>
                            <uc6:calendar ID="cal_sdate" runat="server" />
                        </td>
                        <th>
                            <span class="a-letter-Red">*</span> 換照日期
                        </th>
                        <td>
                            <uc6:calendar ID="cal_change" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <span class="a-letter-Red">*</span> 車別名稱
                        </th>
                        <td>
                            <asp:DropDownList ID="ddl_chekuan" runat="server">
                            </asp:DropDownList>
                        </td>
                        <th>
                            <span class="a-letter-Red">*</span> 能源種類
                        </th>
                        <td>
                            <asp:DropDownList ID="ddl_energy" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <span class="a-letter-Red">*</span> 顏　　色
                        </th>
                        <td>
                            <asp:DropDownList ID="ddl_color" runat="server">
                            </asp:DropDownList>
                            色
                        </td>
                        <th>
                            <span class="a-letter-Red">*</span> 引擎號碼
                        </th>
                        <td>
                            <asp:TextBox ID="txt_engine" runat="server" Columns="20"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <span class="a-letter-Red">*</span> 廠　　牌
                        </th>
                        <td>
                            <asp:DropDownList ID="ddl_mark" runat="server">
                            </asp:DropDownList>
                        </td>
                        <th>
                            型 式
                        </th>
                        <td>
                            <asp:TextBox ID="txt_type" runat="server" Columns="20"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <span class="a-letter-Red">*</span> 年　　份
                        </th>
                        <td>
                            <asp:TextBox ID="txt_year" runat="server" Columns="3"></asp:TextBox>
                            年
                        </td>
                        <th>
                            <span class="a-letter-Red">*</span> 汽缸總排氣量
                        </th>
                        <td>
                            <asp:TextBox ID="txt_cc" runat="server" Columns="10"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            使用年限
                        </th>
                        <td>
                            <asp:TextBox ID="txt_useyear" runat="server" Columns="3"></asp:TextBox>
                            年
                        </td>
                        <th>
                            <span class="a-letter-Red">*</span> 座　　位
                        </th>
                        <td>
                            <asp:TextBox ID="txt_count" runat="server" Columns="3"></asp:TextBox>
                            人
                        </td>
                    </tr>
                    <tr>
                        <th>
                            <span class="a-letter-Red">*</span> 車輛來源
                        </th>
                        <td>
                            <asp:DropDownList ID="ddl_source" runat="server">
                            </asp:DropDownList>
                        </td>
                        <th>
                            <span class="a-letter-Red">*</span> 廠　　商
                        </th>
                        <td>
                            <asp:DropDownList ID="ddl_factory" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            購入日期
                        </th>
                        <td>
                            <uc6:calendar ID="cal_buydate" runat="server" />
                        </td>
                        <th>
                            移入日期
                        </th>
                        <td>
                            <uc6:calendar ID="cal_cdate" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <th>
                            檢驗日期
                        </th>
                        <td>
                            <uc6:calendar ID="cal_testdate" runat="server" />
                        </td>
                        <th>
                            報廢日期
                        </th>
                        <td>
                            <uc6:calendar ID="cal_overdate" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <th>
                            車輛備註
                        </th>
                        <td>
                            <asp:TextBox ID="txt_memo" runat="server" Columns="40" Rows="6" TextMode="MultiLine"></asp:TextBox>
                        </td>
                        <th>
                            車輛圖片
                        </th>
                        <td>
                            <uc5:ImageUpload ID="ImageUpload1" runat="server" PicHeight="350" PicSize="2048"
                                PicType="jpg,gif,jpge,bmp,png" PicWidth="400" Thumbnail="True" ThumbnailMode="CUT"
                                PicTitle="車輛圖片" />
                            <asp:Panel ID="Panel1" runat="server">
                                <table border="0" cellpadding="0" cellspacing="0">
                                    <tr>
                                        <td width="60">
                                            <div id="div_pic1" runat="server">
                                            </div>
                                        </td>
                                        <td valign="bottom">
                                            <asp:LinkButton ID="lbtn_delpic1" runat="server" OnClick="lbtn_delpic1_Click">[刪除]</asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <th>
                            車輛狀態
                        </th>
                        <td colspan="3">
                            <asp:RadioButtonList ID="rbl_status" runat="server" RepeatDirection="Horizontal"
                                RepeatLayout="Flow">
                                <asp:ListItem Value="1">可派遣</asp:ListItem>
                                <asp:ListItem Value="2">不可派遣</asp:ListItem>
                                <asp:ListItem Value="3">已報廢</asp:ListItem>
                            </asp:RadioButtonList>
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
                    <asp:Button ID="btn_submit" runat="server" CssClass="b-input" Text="確定" OnClick="btn_submit_Click" />
                    &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btn_cancel" runat="server" CssClass="a-input" Text="取消" OnClick="btn_cancel_Click" />
                </div>
                <div id="div1" runat="server">
                </div>
            </div>
        </asp:View>
    </asp:MultiView>
</asp:Content>
