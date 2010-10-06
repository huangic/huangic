<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="300402-1.aspx.cs" Inherits="_30_300400_300402_1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../../lib/Navigator.ascx" TagName="Navigator" TagPrefix="uc1" %>
<%@ Register src="../../lib/FileUpload.ascx" tagname="FileUpload" tagprefix="uc2" %>
<%@ Register src="../../lib/tree/jQueryDepartTree.ascx" tagname="jQueryDepartTree" tagprefix="uc3" %>
<%@ Register src="../../lib/tree/jQueryPeopleTree.ascx" tagname="jQueryPeopleTree" tagprefix="uc4" %>
<%@ Register src="../../lib/ImageUpload.ascx" tagname="ImageUpload" tagprefix="uc5" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <script type="text/javascript" src="../..//js/lytebox.js"></script>
    <uc1:Navigator ID="Navigator1" runat="server" SysFuncNo="300402" />
    <div class="tableDiv">
        <div class="header">
            <div class="h1"></div>
            <div class="h2"></div>
            <div class="h3"></div>
        </div>
        <table>
            <tr>
                <th style="width:120px"><span class="a-letter-Red">*</span> 所在地</th>
                <td>
                    <asp:DropDownList ID="ddl_spot" runat="server">
                    </asp:DropDownList>
                </td>
                <th style="width:120px"><span class="a-letter-Red">*</span> 場地名稱</th>
                <td><asp:TextBox ID="txt_name" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <th><span class="a-letter-Red">*</span> 第一保管人</th>
                <td>
                    <uc4:jQueryPeopleTree ID="jQueryPeopleTree1" runat="server" />
                </td>
                <th><span class="a-letter-Red">*</span> 第一保管人電話</th>
                <td><asp:TextBox ID="txt_tel1" runat="server" Columns="10"></asp:TextBox>分機<asp:TextBox 
                        ID="txt_ext1" runat="server" Columns="5"></asp:TextBox></td>
            </tr>
            <tr>
                <th>第二保管人</th>
                <td>
                    <uc4:jQueryPeopleTree ID="jQueryPeopleTree2" runat="server" />
                </td>
                <th>第二保管人電話</th>
                <td><asp:TextBox ID="txt_tel2" runat="server" Columns="10"></asp:TextBox>分機<asp:TextBox 
                        ID="txt_ext2" runat="server" Columns="5"></asp:TextBox></td>
            </tr>
            <tr>
                <th><span class="a-letter-Red">*</span> 容納人數</th>
                <td><asp:TextBox ID="txt_human" runat="server" Columns="5"></asp:TextBox>人</td>
                <th><span class="a-letter-Red">*</span> 所在樓層</th>
                <td><asp:TextBox ID="txt_floor" runat="server" Columns="5"></asp:TextBox>樓</td>
            </tr>
            <tr>
                <th><span class="a-letter-Red">*</span> 可借用時間</th>
                <td>起<asp:DropDownList ID="ddl_stime" runat="server">
                    <asp:ListItem Value="0">請選擇</asp:ListItem>
                    <asp:ListItem Value="06:00">06:00</asp:ListItem>
                    <asp:ListItem Value="07:00">07:00</asp:ListItem>
                    <asp:ListItem Value="08:00">08:00</asp:ListItem>
                    <asp:ListItem Value="09:00">09:00</asp:ListItem>
                    <asp:ListItem Value="10:00">10:00</asp:ListItem>
                    <asp:ListItem Value="11:00">11:00</asp:ListItem>
                    <asp:ListItem Value="12:00">12:00</asp:ListItem>
                    <asp:ListItem Value="13:00">13:00</asp:ListItem>
                    <asp:ListItem Value="14:00">14:00</asp:ListItem>
                    <asp:ListItem Value="15:00">15:00</asp:ListItem>
                    <asp:ListItem Value="16:00">16:00</asp:ListItem>
                    <asp:ListItem Value="17:00">17:00</asp:ListItem>
                    <asp:ListItem Value="18:00">18:00</asp:ListItem>
                    <asp:ListItem Value="19:00">19:00</asp:ListItem>
                    <asp:ListItem Value="20:00">20:00</asp:ListItem>
                    </asp:DropDownList> ~ 迄 <asp:DropDownList ID="ddl_etime" runat="server">
                    <asp:ListItem Value="0">請選擇</asp:ListItem>
                    <asp:ListItem Value="06:00">06:00</asp:ListItem>
                    <asp:ListItem Value="07:00">07:00</asp:ListItem>
                    <asp:ListItem Value="08:00">08:00</asp:ListItem>
                    <asp:ListItem Value="09:00">09:00</asp:ListItem>
                    <asp:ListItem Value="10:00">10:00</asp:ListItem>
                    <asp:ListItem Value="11:00">11:00</asp:ListItem>
                    <asp:ListItem Value="12:00">12:00</asp:ListItem>
                    <asp:ListItem Value="13:00">13:00</asp:ListItem>
                    <asp:ListItem Value="14:00">14:00</asp:ListItem>
                    <asp:ListItem Value="15:00">15:00</asp:ListItem>
                    <asp:ListItem Value="16:00">16:00</asp:ListItem>
                    <asp:ListItem Value="17:00">17:00</asp:ListItem>
                    <asp:ListItem Value="18:00">18:00</asp:ListItem>
                    <asp:ListItem Value="19:00">19:00</asp:ListItem>
                    <asp:ListItem Value="20:00">20:00</asp:ListItem>
                    </asp:DropDownList> </td>
                <th>最低與會人數</th>
                <td><asp:TextBox ID="txt_count" runat="server" Columns="5"></asp:TextBox>人</td>
            </tr>
            <tr>
                <th>場地圖片</th>
                <td>
                    <uc5:ImageUpload ID="ImageUpload1" runat="server" PicHeight="350" 
                        PicSize="2048" PicType="jpg,gif,jpge,bmp,png" PicWidth="400" Thumbnail="True" 
                        ThumbnailMode="CUT" PicTitle="場地圖片" />
                    <asp:Panel ID="Panel1" runat="server">
                    <table border="0" cellpadding="0" cellspacing="0">
                      <tr>
                        <td width="60"><div id="div_pic1" runat="server"></div></td>
                        <td valign="bottom"><asp:LinkButton ID="lbtn_delpic1" runat="server" onclick="lbtn_delpic1_Click">[刪除]</asp:LinkButton></td>
                      </tr>
                    </table>
                    </asp:Panel>
                </td>
                <th>場地平面圖</th>
                <td>
                    <uc5:ImageUpload ID="ImageUpload2" runat="server" PicHeight="350" 
                        PicSize="2048" PicType="jpg,gif,jpge,bmp,png" PicWidth="400" Thumbnail="True" 
                        PicTitle="場地平面圖" ThumbnailMode="CUT" />
                    <asp:Panel ID="Panel2" runat="server">
                    <table border="0" cellpadding="0" cellspacing="0">
                      <tr>
                        <td width="60"><div id="div_pic2" runat="server"></div></td>
                        <td valign="bottom"><asp:LinkButton ID="lbtn_delpic2" runat="server" Visible="False" onclick="lbtn_delpic2_Click">[刪除]</asp:LinkButton></td>
                      </tr>
                    </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <th>場地描述</th>
                <td colspan="3">
                    <asp:TextBox ID="txt_describe" runat="server" Columns="60" Rows="3" 
                        TextMode="MultiLine"></asp:TextBox></td>
            </tr>
            <tr>
                <th>場地開放單位</th>
                <td colspan="3">
                  <table border="0" cellpadding="0" cellspacing="0" class="">
                    <tr>
                      <td colspan="2"><asp:RadioButton ID="rb_01" runat="server" GroupName="rb" Text="全部單位" /></td>
                    </tr>
                    <tr>
                      <td style="width:80px"><asp:RadioButton ID="rb_02" runat="server" GroupName="rb" Text="限制單位" /></td>
                      <td>
                          <uc3:jQueryDepartTree ID="jQueryDepartTree1" runat="server" />
                        </td>
                    </tr>
                  </table>
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
            <asp:Button ID="btn_submit" runat="server" CssClass="b-input" Text="確定" 
                onclick="btn_submit_Click" />
            &nbsp;&nbsp;&nbsp;
            <asp:Button ID="btn_cancel" runat="server" CssClass="a-input" Text="取消" 
                onclick="btn_cancel_Click" />
        </div>
        <div id="div_msg" runat="server">
        </div>
        <asp:Label ID="lab_no" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lab_mode" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lab_pageIndex" runat="server" Visible="False"></asp:Label>
    </div>
</asp:Content>
