<%@ Page Language="C#" AutoEventWireup="true" CodeFile="100601-1.aspx.cs" Inherits="_10_100600_100601_1" %>

<%@ Register src="../../lib/CssLayout.ascx" tagname="CssLayout" tagprefix="uc1" %>

<%@ Register src="../../lib/Navigator.ascx" tagname="Navigator" tagprefix="uc2" %>

<%@ Register src="../../lib/calendar.ascx" tagname="calendar" tagprefix="uc3" %>

<%@ Register src="../../lib/tree/DepartTreeTextBox.ascx" tagname="DepartTreeTextBox" tagprefix="uc4" %>
<%@ Register src="../../lib/tree/DepartTreeListBox.ascx" tagname="DepartTreeListBox" tagprefix="uc5" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <uc1:CssLayout ID="CssLayout1" runat="server" />
    <script type="text/javascript" src="../../js/jquery-1.4.2.min.js"></script>
     <script type="text/javascript" src="../../js/thickbox.js"></script>

     <script type="text/javascript">


         function pageLoad(sender, args) {
             if (args.get_isPartialLoad()) {
                 //  reapply the thick box stuff
                 tb_init('a.thickbox');
             }
         }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <uc2:Navigator ID="Navigator1" runat="server" SysFuncNo="100601" SubFunc="新增會議" />
    <div class="tableDiv">
        <div class="header">
            <div class="h1">
            </div>
            <div class="h2">
                <div class="name"><span class="a-letter-Red">*</span>為必填值</div>
            </div>
            <div class="h3">
            </div>
        </div>
        <table>
            <tbody>
                <tr>
                    <th style="width:15%">
                        <span class="a-letter-Red">*</span>開會事由<br />(限100字)
                    </th>
                    <td colspan="3">
                        <asp:TextBox ID="tbox_reason" runat="server" Width="375px" MaxLength="100"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th style="width:15%">
                        <span class="a-letter-Red">*</span>開會時間
                    </th>
                    <td colspan="3">
                        <span>起<uc3:calendar ID="calendar1" runat="server" _Show="False" />
                        &nbsp;
                        <asp:DropDownList ID="ddl_stime_h" runat="server">
                        </asp:DropDownList>時
                        &nbsp;
                        <asp:DropDownList ID="ddl_stime_m" runat="server">
                            <asp:ListItem Value="0">00</asp:ListItem>
                            <asp:ListItem>30</asp:ListItem>
                        </asp:DropDownList>分
                        </span>
                        &nbsp;<span>訖<uc3:calendar ID="calendar2" runat="server" _Show="False" />
                        &nbsp;
                        <asp:DropDownList ID="ddl_etime_h" runat="server">
                        </asp:DropDownList>時
                        &nbsp;
                        <asp:DropDownList ID="ddl_etime_m" runat="server">
                            <asp:ListItem Value="0">00</asp:ListItem>
                            <asp:ListItem>30</asp:ListItem>
                        </asp:DropDownList>分
                        </span>
                    &nbsp;</td>
                </tr>
                <tr>
                    <th style="width:15%">
                        <span class="a-letter-Red">*</span>開會地點
                    </th>
                    <td>
                        
                        <asp:TextBox ID="tbox_place" runat="server" MaxLength="200"></asp:TextBox>
                        
                    </td>
                    <th style="width:15%">
                        會議設備
                    </th>
                    <td>
                        
                        <asp:TextBox ID="tbox_facility" runat="server" MaxLength="200"></asp:TextBox>
                        
                    </td>
                </tr>
                <tr>
                    <th style="width:15%">
                        <span class="a-letter-Red">*</span>主持人
                    </th>
                    <td>
                        
                        <uc4:DepartTreeTextBox ID="DepartTreeTextBox1" runat="server" 
                            LeafType="People" />
                        
                    </td>
                    <th style="width:15%">
                        <span class="a-letter-Red">*</span>記錄人員
                    </th>
                    <td>
                        
                        <uc4:DepartTreeTextBox ID="DepartTreeTextBox2" runat="server" 
                            LeafType="People" />
                        
                    </td>
                </tr>
                <tr>
                    <th style="width:15%">
                        聯絡人
                    </th>
                    <td>
                        
                        <asp:Label ID="lab_peoname" runat="server"></asp:Label>
                        
                    </td>
                    <th style="width:15%">
                        <span class="a-letter-Red">*</span>聯絡人電話
                    </th>
                    <td>
                        
                        <asp:TextBox ID="tbox_tel" runat="server" MaxLength="20"></asp:TextBox>
                        
                    </td>
                </tr>
                <tr>
                    <th style="width:15%">
                        <span class="a-letter-Red">*</span>聯絡人E-mail
                    </th>
                    <td>
                        
                        <asp:TextBox ID="tbox_email" runat="server" Width="200px" MaxLength="100"></asp:TextBox>
                        
                    </td>
                    <th style="width:15%">
                        聯絡人傳真
                    </th>
                    <td>
                        
                        <asp:TextBox ID="tbox_fax" runat="server" MaxLength="20"></asp:TextBox>
                        
                    </td>
                </tr>
                <tr>
                    <th style="width:15%">
                        線上邀請
                    </th>
                    <td colspan="3">
                        <asp:RadioButtonList ID="rbl_invite" runat="server" CellPadding="0" 
                            CellSpacing="0" RepeatDirection="Horizontal" Width="165px">
                            <asp:ListItem Value="1">發訊息</asp:ListItem>
                            <asp:ListItem Value="2" Selected="True">不發訊息</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <th style="width:15%">
                        出席人員
                    </th>
                    <td>
                        
                        <uc5:DepartTreeListBox ID="DepartTreeListBox1" runat="server" LeafType="People" 
                            PeopleShowSelf="False" />
                        
                    </td>
                    <th style="width:15%">
                        備註<br />(限600字)
                    </th>
                    <td>
                        
                        <asp:TextBox ID="tbox_memo" runat="server" Height="125px" TextMode="MultiLine" 
                            Width="275px" MaxLength="600"></asp:TextBox>
                        
                    </td>
                </tr>
                <tr>
                    <th>
                        會前資料<br />
                        <asp:Label ID="lb_size" runat="server" Text=""></asp:Label>
                    </th>
                    <td colspan="3">
                        資料說明：<asp:TextBox 
                            ID="tbox_file1" runat="server" Width="175px" MaxLength="200"></asp:TextBox><asp:FileUpload ID="FileUpload1"
                            runat="server" /><br />
                        資料說明：<asp:TextBox ID="tbox_file2" runat="server" 
                            Width="175px" MaxLength="200"></asp:TextBox><asp:FileUpload ID="FileUpload2"
                            runat="server" /><br />
                        資料說明：<asp:TextBox ID="tbox_file3" runat="server" 
                            Width="175px" MaxLength="200"></asp:TextBox><asp:FileUpload ID="FileUpload3"
                            runat="server" /><br />
                        資料說明：<asp:TextBox ID="tbox_file4" runat="server" 
                            Width="175px" MaxLength="200"></asp:TextBox><asp:FileUpload ID="FileUpload4"
                            runat="server" /><br />
                        資料說明：<asp:TextBox ID="tbox_file5" runat="server" 
                            Width="175px" MaxLength="200"></asp:TextBox><asp:FileUpload ID="FileUpload5"
                            runat="server" />
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
            <asp:Button ID="btn_ok" runat="server" CssClass="b-input" Text="確定" OnClick="btn_ok_Click" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="Button1" runat="server" CssClass="a-input" OnClientClick="self.parent.tb_remove()"
                UseSubmitBehavior="false" Text="取消" />
        </div>
    </div>
    </form>
</body>
</html>
