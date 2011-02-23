<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="301001.aspx.cs" Inherits="_30_301000_301001" EnableEventValidation="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register Assembly="MattBerseth.WebControls" Namespace="MattBerseth.WebControls" TagPrefix="cc1" %>
<%@ Register Src="../../lib/Navigator.ascx" TagName="Navigator" TagPrefix="uc1" %>
<%@ Register src="../../lib/FileUpload.ascx" tagname="FileUpload" tagprefix="uc2" %>
<%@ Register src="../../lib/ImageUpload.ascx" tagname="ImageUpload" tagprefix="uc5" %>
<%@ Register src="../../lib/tree/DepartTreeTextBox.ascx" tagname="DepartTreeTextBox" tagprefix="uc3" %>
<%@ Register src="../../lib/tree/DepartTreeListBox.ascx" tagname="DepartTreeListBox" tagprefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style1
        {
            height: 24px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <ajaxtoolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" CombineScripts="False">
    </ajaxtoolkit:ToolkitScriptManager>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" EnablePaging="True" 
        SelectCountMethod="GetAllCount" SelectMethod="GetAll" 
        TypeName="NXEIP.DAO.EquipmentsDAO" 
        OldValuesParameterFormatString="original_{0}"></asp:ObjectDataSource>
    <script type="text/javascript" src="../../js/lytebox.js"></script>
    <asp:Label ID="lab_no" runat="server" Visible="False"></asp:Label><asp:Label ID="lab_mode" runat="server" Visible="False"></asp:Label><asp:Label ID="lab_pageIndex" runat="server" Visible="False"></asp:Label>
    <uc1:Navigator ID="Navigator1" runat="server" SysFuncNo="301001" />
    <asp:MultiView ID="MultiView1" runat="server">
        <asp:View ID="List" runat="server">
            <div class="tableDiv">
                <div class="header">
                    <div class="h1">
                    </div>
                    <div class="h2">
                        <div class="function">
                            <asp:Button ID="btn_add" runat="server" Text="新增設備" CssClass="b-input" 
                                OnClick="btn_add_Click" />
                        </div>
                    </div>
                    <div class="h3">
                    </div>
                </div>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <cc1:GridView ID="GridView1" runat="server" DataSourceID="ObjectDataSource1" AllowPaging="True"
                            AutoGenerateColumns="False" CellPadding="3" CellSpacing="3" CssClass="tableData"
                            EmptyDataText="查無資料" DataKeyNames="equ_no" OnRowDataBound="GridView1_RowDataBound"
                            GridLines="None" OnRowCommand="GridView1_RowCommand"><Columns><asp:BoundField DataField="spo_no" HeaderText="所在地" SortExpression="spo_no" /><asp:BoundField 
                                DataField="equ_number" HeaderText="資產編號" SortExpression="equ_number" /><asp:BoundField 
                                DataField="equ_name" HeaderText="設備名稱" SortExpression="equ_name" /><asp:BoundField 
                                DataField="peo_uid" HeaderText="保管人" SortExpression="peo_uid" /><asp:BoundField 
                                DataField="equ_tel" HeaderText="保管人電話" SortExpression="equ_tel" /><asp:BoundField 
                                DataField="equ_descript" HeaderText="設備描述" SortExpression="equ_descript" /><asp:TemplateField HeaderText="修改"><ItemTemplate><asp:Button ID="Button2" runat="server" CommandName="modify" CommandArgument="<%# Container.DataItemIndex %>"
                                            CssClass="edit" /></ItemTemplate><HeaderStyle HorizontalAlign="Center" /><ItemStyle HorizontalAlign="Center" Width="35px" /></asp:TemplateField><asp:TemplateField HeaderText="刪除"><ItemTemplate><asp:Button ID="Button3" runat="server" CommandName="del" CommandArgument="<%# Container.DataItemIndex %>"
                                            CssClass="delete" OnClientClick=" return confirm('確定要刪除?')" /></ItemTemplate><HeaderStyle HorizontalAlign="Center" /><ItemStyle HorizontalAlign="Center" Width="35px" /></asp:TemplateField></Columns><HeaderStyle HorizontalAlign="Left" /></cc1:GridView>
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
                          <span class="a-letter-Red">*</span> 所在地
                      </th>
                      <td>
                          <asp:DropDownList ID="ddl_spot" runat="server">
                          </asp:DropDownList>
                      </td>
                      <th style="width: 120px"><span class="a-letter-Red">*</span> 資產編號</th>
                      <td>
                          <asp:TextBox ID="txt_number" runat="server" Columns="18" MaxLength="20"></asp:TextBox>
                      </td>
                 </tr>
                  <tr>
                      <th>
                          <span class="a-letter-Red">*</span> 設備名稱
                      </th>
                      <td>
                          <asp:TextBox ID="txt_name" runat="server"></asp:TextBox>
                      </td>
                      <th>
                          <span class="a-letter-Red">*</span> 可借用時間
                      </th>
                      <td>
                          起<asp:DropDownList ID="ddl_stime" runat="server">
                          </asp:DropDownList>
                          <ajaxtoolkit:CascadingDropDown ID="ddl_stime_CascadingDropDown" runat="server" Category="stime"
                              LoadingText="讀取中..." PromptText="請選擇" PromptValue="0" ServiceMethod="GetTimes"
                              ServicePath="../../WebService/calendar.asmx" UseContextKey="True" TargetControlID="ddl_stime"></ajaxtoolkit:CascadingDropDown>
                          ~ 迄
                          <asp:DropDownList ID="ddl_etime" runat="server">
                          </asp:DropDownList>
                          <ajaxtoolkit:CascadingDropDown ID="ddl_etime_CascadingDropDown" runat="server" Category="etime"
                              LoadingText="讀取中..." PromptText="請選擇" PromptValue="0" ServiceMethod="GetTimes"
                              ServicePath="../../WebService/calendar.asmx" TargetControlID="ddl_etime" UseContextKey="True"></ajaxtoolkit:CascadingDropDown>
                      </td>
                  </tr>
                  <tr>
                      <th>
                          <span class="a-letter-Red">*</span> 保管人
                      </th>
                      <td>
                          <uc3:DepartTreeTextBox ID="DepartTreeTextBox1" runat="server" LeafType="People" PeopleType="All" />
                      </td>
                      <th>
                          <span class="a-letter-Red">*</span> 保管人電話
                      </th>
                      <td>
                          <asp:TextBox ID="txt_tel" runat="server" Columns="10"></asp:TextBox>分機<asp:TextBox
                              ID="txt_ext" runat="server" Columns="5"></asp:TextBox>
                      </td>
                  </tr>
                  <tr>
                      <th>
                          設備圖片
                      </th>
                      <td>
                          <uc5:ImageUpload ID="ImageUpload1" runat="server" PicHeight="350" PicSize="2048"
                              PicType="jpg,gif,jpge,bmp,png" PicWidth="400" Thumbnail="True" ThumbnailMode="CUT"
                              PicTitle="場地圖片" />
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
                      <th>
                          設備描述
                      </th>
                      <td>
                          <asp:TextBox ID="txt_describe" runat="server" Columns="40" Rows="6" TextMode="MultiLine"></asp:TextBox>
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

