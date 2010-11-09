<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="100301-0.aspx.cs" Inherits="_10_100300_100301_0" %>
<%@ Register Src="../../lib/CssLayout.ascx" TagName="CssLayout" TagPrefix="uc1" %>
<%@ Register Src="../../lib/Navigator.ascx" TagName="Navigator" TagPrefix="uc2" %>
<%@ Register src="../../lib/calendar.ascx" tagname="calendar" tagprefix="uc3" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>新增行事曆</title>
    <uc1:csslayout ID="CssLayout1" runat="server" />
    <script type="text/javascript" src="../../js/jscolor.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <uc2:Navigator ID="Navigator1" runat="server" SysFuncNo="100301" />
    <div class="tableDiv">
        <div class="header">
            <div class="h1"></div>
            <div class="h2">
              <div class="name"><asp:Label ID="lab_UserName" runat="server" Text="管理者"></asp:Label>行事曆</div>
            </div>
            <div class="h3"></div>
        </div>
        <table>
            <tbody>
                <tr>
                    <th>日期時間</th>
                    <td>
                        開始時間<uc3:calendar ID="cl_sdate" runat="server" _Show="False" />
                        <asp:DropDownList ID="ddl_stime" runat="server">
                            <asp:ListItem Value="06:00">06:00</asp:ListItem>
                            <asp:ListItem Value="06:30">06:30</asp:ListItem>
                            <asp:ListItem Value="07:00">07:00</asp:ListItem>
                            <asp:ListItem Value="07:30">07:30</asp:ListItem>
                            <asp:ListItem Value="08:00">08:00</asp:ListItem>
                            <asp:ListItem Value="08:30">08:30</asp:ListItem>
                            <asp:ListItem Value="09:00">09:00</asp:ListItem>
                            <asp:ListItem Value="09:30">09:30</asp:ListItem>
                            <asp:ListItem Value="10:00">10:00</asp:ListItem>
                            <asp:ListItem Value="10:30">10:30</asp:ListItem>
                            <asp:ListItem Value="11:00">11:00</asp:ListItem>
                            <asp:ListItem Value="11:30">11:30</asp:ListItem>
                            <asp:ListItem Value="12:00">12:00</asp:ListItem>
                            <asp:ListItem Value="12:30">12:30</asp:ListItem>
                            <asp:ListItem Value="13:00">13:00</asp:ListItem>
                            <asp:ListItem Value="13:30">13:30</asp:ListItem>
                            <asp:ListItem Value="14:00">14:00</asp:ListItem>
                            <asp:ListItem Value="14:30">14:30</asp:ListItem>
                            <asp:ListItem Value="15:00">15:00</asp:ListItem>
                            <asp:ListItem Value="15:30">15:30</asp:ListItem>
                            <asp:ListItem Value="16:00">16:00</asp:ListItem>
                            <asp:ListItem Value="16:30">16:30</asp:ListItem>
                            <asp:ListItem Value="17:00">17:00</asp:ListItem>
                            <asp:ListItem Value="17:30">17:30</asp:ListItem>
                            <asp:ListItem Value="18:00">18:00</asp:ListItem>
                            <asp:ListItem Value="18:30">18:30</asp:ListItem>
                            <asp:ListItem Value="19:00">19:00</asp:ListItem>
                            <asp:ListItem Value="19:30">19:30</asp:ListItem>
                            <asp:ListItem Value="20:00">20:00</asp:ListItem>
                            <asp:ListItem Value="20:30">20:30</asp:ListItem>
                            <asp:ListItem Value="21:00">21:00</asp:ListItem>
                            <asp:ListItem Value="21:30">21:30</asp:ListItem>
                            <asp:ListItem Value="22:00">22:00</asp:ListItem>
                            <asp:ListItem Value="22:30">22:30</asp:ListItem>
                            <asp:ListItem Value="23:00">23:00</asp:ListItem>
                            <asp:ListItem Value="23:30">23:30</asp:ListItem>
                        </asp:DropDownList>
                        &nbsp;(日期格式YY-mm-dd)
                        <br />
                        結束時間<uc3:calendar ID="cl_edate" runat="server" _Show="False" />
                        <asp:DropDownList ID="ddl_etime" runat="server">
                            <asp:ListItem Value="06:00">06:00</asp:ListItem>
                            <asp:ListItem Value="06:30">06:30</asp:ListItem>
                            <asp:ListItem Value="07:00">07:00</asp:ListItem>
                            <asp:ListItem Value="07:30">07:30</asp:ListItem>
                            <asp:ListItem Value="08:00">08:00</asp:ListItem>
                            <asp:ListItem Value="08:30">08:30</asp:ListItem>
                            <asp:ListItem Value="09:00">09:00</asp:ListItem>
                            <asp:ListItem Value="09:30">09:30</asp:ListItem>
                            <asp:ListItem Value="10:00">10:00</asp:ListItem>
                            <asp:ListItem Value="10:30">10:30</asp:ListItem>
                            <asp:ListItem Value="11:00">11:00</asp:ListItem>
                            <asp:ListItem Value="11:30">11:30</asp:ListItem>
                            <asp:ListItem Value="12:00">12:00</asp:ListItem>
                            <asp:ListItem Value="12:30">12:30</asp:ListItem>
                            <asp:ListItem Value="13:00">13:00</asp:ListItem>
                            <asp:ListItem Value="13:30">13:30</asp:ListItem>
                            <asp:ListItem Value="14:00">14:00</asp:ListItem>
                            <asp:ListItem Value="14:30">14:30</asp:ListItem>
                            <asp:ListItem Value="15:00">15:00</asp:ListItem>
                            <asp:ListItem Value="15:30">15:30</asp:ListItem>
                            <asp:ListItem Value="16:00">16:00</asp:ListItem>
                            <asp:ListItem Value="16:30">16:30</asp:ListItem>
                            <asp:ListItem Value="17:00">17:00</asp:ListItem>
                            <asp:ListItem Value="17:30">17:30</asp:ListItem>
                            <asp:ListItem Value="18:00">18:00</asp:ListItem>
                            <asp:ListItem Value="18:30">18:30</asp:ListItem>
                            <asp:ListItem Value="19:00">19:00</asp:ListItem>
                            <asp:ListItem Value="19:30">19:30</asp:ListItem>
                            <asp:ListItem Value="20:00">20:00</asp:ListItem>
                            <asp:ListItem Value="20:30">20:30</asp:ListItem>
                            <asp:ListItem Value="21:00">21:00</asp:ListItem>
                            <asp:ListItem Value="21:30">21:30</asp:ListItem>
                            <asp:ListItem Value="22:00">22:00</asp:ListItem>
                            <asp:ListItem Value="22:30">22:30</asp:ListItem>
                            <asp:ListItem Value="23:00">23:00</asp:ListItem>
                            <asp:ListItem Value="23:30">23:30</asp:ListItem>
                        </asp:DropDownList>
                        &nbsp;(日期格式YY-mm-dd)
                    </td>
                    <th>
                        標題
                    </th>
                    <td>
                        <asp:TextBox ID="txt_title" runat="server" Columns="30"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>
                        地點
                    </th>
                    <td>
                        <asp:TextBox ID="txt_place" runat="server" Columns="30"></asp:TextBox>
                    </td>
                    <th>
                        背景顏色
                    </th>
                    <td>
                        <asp:TextBox ID="txt_bgcolor" runat="server" Columns="5" CssClass="ColorPicker"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>
                        預計計畫
                    </th>
                    <td>
                        <asp:TextBox ID="txt_project" runat="server" Columns="30" Rows="5" TextMode="MultiLine"></asp:TextBox>
                    </td>
                    <th>
                        紀錄結果
                    </th>
                    <td>
                        <asp:TextBox ID="txt_result" runat="server" Columns="30" Rows="5" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:CheckBox ID="cb_update" runat="server" Text="更新到週期性行程(修改或刪除)" ForeColor="Red" Font-Bold="True"></asp:CheckBox><br />
                        <asp:CheckBox ID="CB_c03" runat="server" Text="週期性行程" AutoPostBack="True" 
                            oncheckedchanged="CB_c03_CheckedChanged"></asp:CheckBox>
                        <asp:Panel ID="Panel0" runat="server">
                            <table>
                                <tbody>
                                    <tr>
                                        <th align="left">
                                            週期：<br />
                                            <asp:RadioButtonList ID="rbl_cycle" runat="server" AutoPostBack="True" RepeatColumns="2"
                                                RepeatDirection="Horizontal" RepeatLayout="Flow" 
                                                onselectedindexchanged="rbl_cycle_SelectedIndexChanged">
                                                <asp:ListItem Value="1">日循環</asp:ListItem>
                                                <asp:ListItem Value="2">週循環</asp:ListItem>
                                                <asp:ListItem Value="3">月循環</asp:ListItem>
                                                <asp:ListItem Value="4">年循環</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </th>
                                        <td style="width:80%">
                                            <asp:Panel ID="Panel1" runat="server">
                                                每<asp:TextBox ID="txt_1" runat="server" Columns="2" Width="30px">1</asp:TextBox>天
                                            </asp:Panel>
                                            <asp:Panel ID="Panel2" runat="server">
                                                每
                                                <asp:TextBox ID="txt_21" runat="server" Columns="2" Width="30px">1</asp:TextBox>個星期的<br />
                                                <asp:CheckBoxList ID="cbl_22" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                                    <asp:ListItem Value="0">星期日</asp:ListItem>
                                                    <asp:ListItem Value="1">星期一</asp:ListItem>
                                                    <asp:ListItem Value="2">星期二</asp:ListItem>
                                                    <asp:ListItem Value="3">星期三</asp:ListItem>
                                                    <asp:ListItem Value="4">星期四</asp:ListItem>
                                                    <asp:ListItem Value="5">星期五</asp:ListItem>
                                                    <asp:ListItem Value="6">星期六</asp:ListItem>
                                                </asp:CheckBoxList>
                                            </asp:Panel>
                                            <asp:Panel ID="Panel3" runat="server">
                                                <asp:RadioButton ID="rb_31" runat="server" Text="每" AutoPostBack="True" 
                                                    GroupName="rb30"></asp:RadioButton>
                                                <asp:TextBox ID="txt_311" runat="server" Columns="2" Width="30px">1</asp:TextBox>月，日期
                                                <asp:TextBox ID="txt_312" runat="server" Columns="2" Width="30px"></asp:TextBox>循環<font
                                                    color="red">(大於28的日期，不存在時，將會取值月底)</font><br />
                                                <asp:RadioButton ID="rb_32" runat="server" Text="每" AutoPostBack="True" 
                                                    GroupName="rb30"></asp:RadioButton>
                                                <asp:TextBox ID="txt_321" runat="server" Columns="2" Width="30px">1</asp:TextBox>月，
                                                <asp:DropDownList ID="ddl_322" runat="server">
                                                    <asp:ListItem Value="1">第一個</asp:ListItem>
                                                    <asp:ListItem Value="2">第二個</asp:ListItem>
                                                    <asp:ListItem Value="3">第三個</asp:ListItem>
                                                    <asp:ListItem Value="4">第四個</asp:ListItem>
                                                    <asp:ListItem Value="5">最末個</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddl_323" runat="server">
                                                    <asp:ListItem Value="1">星期一</asp:ListItem>
                                                    <asp:ListItem Value="2">星期二</asp:ListItem>
                                                    <asp:ListItem Value="3">星期三</asp:ListItem>
                                                    <asp:ListItem Value="4">星期四</asp:ListItem>
                                                    <asp:ListItem Value="5">星期五</asp:ListItem>
                                                    <asp:ListItem Value="6">星期六</asp:ListItem>
                                                    <asp:ListItem Value="0">星期日</asp:ListItem>
                                                </asp:DropDownList>
                                                循環</asp:Panel>
                                            <asp:Panel ID="Panel4" runat="server">
                                                <asp:RadioButton ID="rb_41" runat="server" Text="每年" AutoPostBack="True" 
                                                    GroupName="rb40"></asp:RadioButton>
                                                <asp:DropDownList ID="ddl_411" runat="server">
                                                    <asp:ListItem Value="01">一月</asp:ListItem>
                                                    <asp:ListItem Value="02">二月</asp:ListItem>
                                                    <asp:ListItem Value="03">三月</asp:ListItem>
                                                    <asp:ListItem Value="04">四月</asp:ListItem>
                                                    <asp:ListItem Value="05">五月</asp:ListItem>
                                                    <asp:ListItem Value="06">六月</asp:ListItem>
                                                    <asp:ListItem Value="07">七月</asp:ListItem>
                                                    <asp:ListItem Value="08">八月</asp:ListItem>
                                                    <asp:ListItem Value="09">九月</asp:ListItem>
                                                    <asp:ListItem Value="10">十月</asp:ListItem>
                                                    <asp:ListItem Value="11">十一月</asp:ListItem>
                                                    <asp:ListItem Value="12">十二月</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:TextBox ID="txt_412" runat="server" Columns="2" Width="30px"></asp:TextBox>日<br />
                                                <asp:RadioButton ID="rb_42" runat="server" Text="每年" AutoPostBack="True" 
                                                    GroupName="rb40"></asp:RadioButton>
                                                <asp:DropDownList ID="ddl_421" runat="server">
                                                    <asp:ListItem Value="01">一月</asp:ListItem>
                                                    <asp:ListItem Value="02">二月</asp:ListItem>
                                                    <asp:ListItem Value="03">三月</asp:ListItem>
                                                    <asp:ListItem Value="04">四月</asp:ListItem>
                                                    <asp:ListItem Value="05">五月</asp:ListItem>
                                                    <asp:ListItem Value="06">六月</asp:ListItem>
                                                    <asp:ListItem Value="07">七月</asp:ListItem>
                                                    <asp:ListItem Value="08">八月</asp:ListItem>
                                                    <asp:ListItem Value="09">九月</asp:ListItem>
                                                    <asp:ListItem Value="10">十月</asp:ListItem>
                                                    <asp:ListItem Value="11">十一月</asp:ListItem>
                                                    <asp:ListItem Value="12">十二月</asp:ListItem>
                                                </asp:DropDownList>
                                                ，
                                                <asp:DropDownList ID="ddl_422" runat="server">
                                                    <asp:ListItem Value="1">第一個</asp:ListItem>
                                                    <asp:ListItem Value="2">第二個</asp:ListItem>
                                                    <asp:ListItem Value="3">第三個</asp:ListItem>
                                                    <asp:ListItem Value="4">第四個</asp:ListItem>
                                                    <asp:ListItem Value="5">最末個</asp:ListItem>
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="ddl_423" runat="server">
                                                    <asp:ListItem Value="1">星期一</asp:ListItem>
                                                    <asp:ListItem Value="2">星期二</asp:ListItem>
                                                    <asp:ListItem Value="3">星期三</asp:ListItem>
                                                    <asp:ListItem Value="4">星期四</asp:ListItem>
                                                    <asp:ListItem Value="5">星期五</asp:ListItem>
                                                    <asp:ListItem Value="6">星期六</asp:ListItem>
                                                    <asp:ListItem Value="0">星期日</asp:ListItem>
                                                </asp:DropDownList>
                                            </asp:Panel>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th align="left">
                                            循環範圍：<br />
                                            開始日期<uc3:calendar ID="cl_sdate1" runat="server" _Show="False" />
                                        </th>
                                        <td bgcolor="#ffffff">
                                            <asp:RadioButton ID="rb_51" runat="server" Text="循環" AutoPostBack="True" 
                                                GroupName="rb50"></asp:RadioButton>
                                            <asp:TextBox ID="txt_qty" runat="server" Columns="2" Width="30px">1</asp:TextBox>次<br />
                                            <asp:RadioButton ID="rb_52" runat="server" Text="截止日期" AutoPostBack="True" 
                                                GroupName="rb50"></asp:RadioButton>
                                            <uc3:calendar ID="cl_edate1" runat="server" _Show="False" />
                                            </td>
                                    </tr>
                                </tbody>
                            </table>
                        </asp:Panel>
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
            <asp:Button ID="btn_cancel" runat="server" CssClass="a-input" Text="取消" OnClick="btn_cancel_Click" />
            &nbsp;&nbsp;
            <asp:Button ID="btn_del" runat="server" CssClass="b-input" Text="刪除" onclick="btn_del_Click" />
        </div>
        <div id="div_msg" runat="server">
        </div>
        <asp:Label ID="lab_no" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lab_peo_uid" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lab_today" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lab_source" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lab_c03_no" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lab_depart" runat="server" Visible="False"></asp:Label>
    </div>
    </form>
</body>
</html>
