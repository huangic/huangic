<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="300303-2.aspx.cs" Inherits="_30_300300_300303_2" %>

<%@ Register Src="../../lib/Navigator.ascx" TagName="Navigator" TagPrefix="uc1" %>
<%@ Register Src="../../lib/calendar.ascx" TagName="calendar" TagPrefix="uc2" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script type="text/javascript">
        
        function pageLoad(sender, args) {
            if (args.get_isPartialLoad()) {
                //重新初始化日期元件
                popUpCal.init();
            }
        }

        //輸入檢查
        var check_flag = true;

        var TableName = "table2";

        $(document).ready(function () {
            //批次開班
            $('#div_flag').hide();
            
            //刪除row
            $('#' + TableName + ' td img.delete').live('click', function () {
                DeleteRow(this);
            });

            //TextBox檢核互動,以Table的ID=ItemTable,找這個Table裡的textbox (input type=textbox)
            $('#Text1').keyup(function () {
                check = true;
                //檢查是否含非數字字元     
                if (isNaN($(this).val())) {
                    alert('只可輸入數字字元!');
                    this.value = this.value.replace(/[\D]/g, '');
                    check_flag = false;
                }
                //檢查是否只輸入0     
                if ($(this).val() == '0') {
                    alert('梯次至少是1!');
                    this.value = '1';
                    check_flag = true;
                }
            }).focusout(function () {
                //檢查是否未輸入數量     
                if ($(this).val() == '') {
                    alert('梯次至少是1!');
                    this.value = '1';
                    check_flag = true;
                }
            });
            $('#<%=tbox_flag.ClientID%>').keyup(function () {
                check = true;
                //檢查是否含非數字字元     
                if (isNaN($(this).val())) {
                    alert('只可輸入數字字元!');
                    this.value = this.value.replace(/[\D]/g, '');
                    check_flag = false;
                }
                //檢查是否只輸入0     
                if ($(this).val() == '0') {
                    alert('期別至少是1!');
                    this.value = '1';
                    check_flag = true;
                }
            }).focusout(function () {
                //檢查是否未輸入數量     
                if ($(this).val() == '') {
                    alert('期別至少是1!');
                    this.value = '1';
                    check_flag = true;
                }
            });
        });

        function addTable_click() {
            $('#div_flag').show();
            if (check_flag) {
                var len = parseInt($('#Text1').val());
                var sr = 1;
                var stop = 1;
                var trlen = $("#" + TableName + " tr").length;
                if (trlen > 1) {
                    sr = trlen;
                    stop = len + trlen - 1;
                } else {
                    stop = len;
                }
                for (var i = sr; i <= stop; i++) {
                    InsertRows(i);
                }
                //重新計算期別數
                ReCountRows();
            }
        }

        function InsertRows(idnum) {
            //var ohtml = $('tbody:eq(0)').html();
            //var row1 = $('tbody tr:eq(0)').html(); $('tbody').html(ohtml + '<tr>' + row1 + '</tr>');
            var hr_option = "";
            for (var h = 1; h <= 23; h++) {
                var tmp = "";
                if (h < 10) {
                    tmp = "0" + h;
                } else {
                    tmp = h;
                }
                if (h == 8) {
                    hr_option += "<option selected='selected'>" + tmp + "</option>";
                } else {
                    hr_option += "<option >" + tmp + "</option>";
                }
            }
            var hr_option2 = "";
            for (var h = 1; h <= 23; h++) {
                var tmp = "";
                if (h < 10) {
                    tmp = "0" + h;
                } else {
                    tmp = h;
                }
                if (h == 17) {
                    hr_option2 += "<option selected='selected'>" + tmp + "</option>";
                } else {
                    hr_option2 += "<option >" + tmp + "</option>";
                }
            }
            var mm_option = "";
            for (var m = 0; m <= 5; m++) {
                mm_option += "<option >" + m + "0</option>";
            }

            //當前期別數
            var flag = parseInt($('#<%=tbox_flag.ClientID%>').val());

            var str = "<tr>";
            str += "<td align='center'><input id='cbox_" + idnum + "' type='checkbox' /></td>";
            str += "<td align='center'>第" + (idnum + flag) + "期</td>";
            str += "<td><input id='tbox_1_" + idnum + "' type='text' class='calendarSelectDate' style='width:65px' /></td>";
            str += "<td><input id='tbox_2_" + idnum + "' type='text' class='calendarSelectDate' style='width:65px' /></td>";
            str += "<td><input id='tbox_3_" + idnum + "' type='text' class='calendarSelectDate' style='width:65px' />&nbsp;<select id='Select_h1_" + idnum + "'>" + hr_option + "</select>時&nbsp;<select id='Select_m1_" + idnum + "'>" + mm_option + "</select>分</td>";
            str += "<td><input id='tbox_4_" + idnum + "' type='text' class='calendarSelectDate' style='width:65px' />&nbsp;<select id='Select_h2_" + idnum + "'>" + hr_option2 + "</select>時&nbsp;<select id='Select_m2_" + idnum + "'>" + mm_option + "</select>分</td>";
            str += "<td align='center'><img class='delete' src='../../image/delete.gif' /></td>";
            str += "</tr>";
            $(str).appendTo($("#" + TableName));
        }

        function DeleteRow(elem) {
            var row = $(elem).parents('tr');
            var index = $('tr').index(row);
            if (index != 1) {
                //row.hide();
                row.remove();
            }
           
            ReCountRows();
        }

        function ReCountRows() {
            //當前期別數
            var flag = parseInt($('#<%=tbox_flag.ClientID%>').val());
            var trlen = $("#" + TableName + " tr").length;
            for (var i = 1; i <= trlen; i++) {
                $("#" + TableName).find("tr:eq(" + i + ")").find("td:eq(1)").text("第" + (i + flag) + "期");
            }
        }

        //取值
        function getInput_click() {
            var trlen = $("#" + TableName + " tr").length;
            var str = "";
            
            for (var i = 1; i < trlen; i++) {

                //取期別
                var flag = $('#' + TableName + ' tr:eq(' + i + ') td:eq(1)').text();

                //取日期
                var cellText = "";
                for (var j = 1; j <= 4; j++) {
                    if (cellText == "") {
                        cellText = $('#tbox_' + j + '_' + i).val();
                    } else {
                        cellText += "_" + $('#tbox_' + j + '_' + i).val();
                    }
                    if (j == 3) {
                        cellText += " " + $('#Select_h1_' + i).val() + ":" + $('#Select_m1_'+i).val();
                    }
                    if (j == 4) {
                        cellText += " " + $('#Select_h2_' + i).val() + ":" + $('#Select_m2_' + i).val();
                    }
                }

                if (str == "") {
                    str = flag + "_" + cellText;
                } else {
                    str += "," + flag + "_" + cellText;
                }
            }
            $('#<%=hidd_date.ClientID%>').val(str);
            //alert("str"+str);

        }

        //設定值
        function setInput_click(n) {
            if (checkDate($('#Text2').val())) {
                $('#Text2').focus();
                alert("日期錯誤!");
            } else {
                var trlen = $("#" + TableName + " tr").length;
                for (var i = 1; i < trlen; i++) {
                    if ($('#cbox_' + i).attr('checked')) {
                        $('#tbox_' + n + '_' + i).val($('#Text2').val());
                    }
                }
            }
        }

        //批次開班日期檢查
        function checkInput() {
            
            if (checkDataInput()) {
                var check = false;
                var trlen = $("#" + TableName + " tr").length;
                for (var i = 1; i < trlen; i++) {
                    for (var n = 1; n <= 4; n++) {
                        if (checkDate($('#tbox_' + n + '_' + i).val())) {
                            $('#tbox_' + n + '_' + i).focus();
                            check = true;
                            break;
                        }
                    }
                    if (check) {
                        alert("日期錯誤!");
                        break;
                    }
                }
                //時間比較
                if (!check) {
                    for (var i = 1; i < trlen; i++) {
                        if (twoDateComp($('#tbox_1_' + i).val() + "-00-00", $('#tbox_2_' + i).val() + "-00-00")) {
                            alert('報名開始日期 需早於 報名結束日期');
                            check = true;
                        }
                        if (twoDateComp($('#tbox_3_' + i).val() + "-" + $('#Select_h1_' + i).val() + "-" + $('#Select_m1_' + i).val(), $('#tbox_4_' + i).val() + "-" + $('#Select_h2_' + i).val() + "-" + $('#Select_m2_' + i).val())) {
                            alert('上課開始時間 需早於 上課結束時間');
                            check = true;
                        }
                    }
                }

                if (!check) {
                    //組合日期字串
                    getInput_click();
                }

                return !check;

            } else {
                return false;
            }
        }

        function checkDate(theDate) {
            var check = false;
            var tmp = theDate.split('-');
            var y = parseInt(tmp[0]) + 1911;
            //2010-02-02
            var newdate = new Date(y + "-" + tmp[1] + "-" + tmp[2]).toLocaleString();
            if (newdate == "Invalid Date") {
                check = true;
            }
            return check;
        }

        function checkDataInput() {
            var c = true;
            if ($('#<%=ddl_type_2.ClientID%>').val() == '0') {
                alert('請選擇課程類別!');
                c = false;
            }
            if (c && $('#<%=tbox_name.ClientID%>').val() == '') {
                alert('請輸入課程名稱!');
                c = false;
            }
            if (c && $('#<%=ddl_e01.ClientID%>').val() == '請選擇') {
                alert('請選擇上課地點!');
                c = false;
            }
//            if (c && $('#<%=tbox_people.ClientID%>').val() == '') {
//                alert('請輸入招收名額上限!');
//                c = false;
//            } 
            if (c && $('#<%=tbox_people.ClientID%>').val() != '') {
                if (isNaN($('#<%=tbox_people.ClientID%>').val())) {
                    alert('招收名額只可輸入數字字元!');
                    $('#<%=tbox_people.ClientID%>').val($('#<%=tbox_people.ClientID%>').val().replace(/[\D]/g, ''));
                    c = false;
                }
            }
            if (c && $('#<%=tbox_hour.ClientID%>').val() == '') {
                alert('請輸入認證時數!');
                c = false;
            } 
            if (c && $('#<%=tbox_hour.ClientID%>').val() != '') {
                if (isNaN($('#<%=tbox_hour.ClientID%>').val())) {
                    alert('認證時數只可輸入數字字元!');
                    $('#<%=tbox_hour.ClientID%>').val($('#<%=tbox_hour.ClientID%>').val().replace(/[\D]/g, ''));
                    c = false;
                }
            }
//            if (c && $('#<%=tbox_teacher.ClientID%>').val() == '') {
//                alert('請輸入講師姓名!');
//                c = false;
//            }
            var dateID = ['#<%=cal_opendate._GetID%>', '#<%=cal_signsdate._GetID%>', '#<%=cal_signedate._GetID%>', '#<%=cal_sdate._GetID%>', '#<%=cal_edate._GetID%>'];
            var datename = ['上線開放日期', '報名開始日期', '報名結束日期', '上課開始時間', '上課結束時間'];
            if (c) {
                for (var i = 0; i < dateID.length; i++) {

                    if ($(dateID[i]).val() == '') {
                        alert('請輸入' + datename[i]);
                        c = false;
                        break;
                    } else {
                        if (checkDate($(dateID[i]).val())) {
                            alert(datename[i] + '錯誤!');
                            c = false;
                            break;
                        }
                    }
                }
            }
            //檢查時間前後
            if (c) {
                if (twoDateComp($(dateID[1]).val()+"-00-00", $(dateID[2]).val()+"-00-00")) {
                    alert(datename[1] + ' 需早於 ' + datename[2]);
                    c = false;
                }
                if (twoDateComp($(dateID[3]).val() + "-" + $('#<%=ddl_sh.ClientID%>').val() + "-" + $('#<%=ddl_sm.ClientID%>').val(), $(dateID[4]).val() + "-" + $('#<%=ddl_eh.ClientID%>').val() + "-" + $('#<%=ddl_em.ClientID%>').val())) {
                    alert(datename[3] + ' 需早於 ' + datename[4]);
                    c = false;
                }
            }
            return c;
        }

        //日期比較
        function twoDateComp(sd, ed) {
            var check = false;
            var tmp1 = sd.split('-');
            var tmp2 = ed.split('-');
            var y1 = parseInt(tmp1[0]) + 1911;
            var y2 = parseInt(tmp2[0]) + 1911;

            var newsd = new Date();
            newsd.setFullYear(y1);
            newsd.setMonth(parseInt(tmp1[1]));
            newsd.setDate(parseInt(tmp1[2]));
            newsd.setHours(parseInt(tmp1[3]));
            newsd.setMinutes(parseInt(tmp1[4]));
            newsd.setSeconds(0);
            newsd.setMilliseconds(0);

            var newed = new Date();
            newed.setFullYear(y2);
            newed.setMonth(parseInt(tmp2[1]));
            newed.setDate(parseInt(tmp2[2]));
            newed.setHours(parseInt(tmp2[3]));
            newed.setMinutes(parseInt(tmp2[4]));
            newed.setSeconds(0);
            newed.setMilliseconds(0);

            if (newsd > newed) {
                check = true;
            }

            return check;
        }
        
        
    </script>
    <style type="text/css">
        #Text3
        {
            width: 68px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ObjectDataSource ID="ODS_e01" runat="server" SelectMethod="GetAll" TypeName="NXEIP.DAO.e01DAO">
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ODS_type_1" runat="server" SelectMethod="GetClassParentData"
        TypeName="NXEIP.DAO.TypesDAO"></asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ODS_type_2" runat="server" SelectMethod="GetClassData"
        TypeName="NXEIP.DAO.TypesDAO">
        <SelectParameters>
            <asp:Parameter Name="typ_parent" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:HiddenField ID="hidd_model" runat="server" />
    <asp:HiddenField ID="hidd_no" runat="server" />
    <asp:HiddenField ID="hidd_class" runat="server" />
    
    <uc1:Navigator ID="Navigator1" runat="server" SysFuncNo="300303" />
    <div class="tableDiv">
        <div class="header">
            <div class="h1">
            </div>
            <div class="h2">
                <div class="name">
                    <span style="color: Red">*</span>表必填欄位
                </div>
            </div>
            <div class="h3">
            </div>
        </div>
        
            
                <table>
                        <tr>
                            <th>
                                學習機構
                            </th>
                            <td>
                                <asp:Label ID="lab_mechani" runat="server"></asp:Label>
                            </td>
                            <th>
                                <span style="color: Red">*</span>期別
                            </th>
                            <td>
                                <asp:TextBox ID="tbox_flag" runat="server" Width="50px">1</asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                <span style="color: Red">*</span>課程名稱
                            </th>
                            <td colspan="3">
                                課程大類&nbsp;
                                <asp:DropDownList ID="ddl_type_1" runat="server" AutoPostBack="True" DataSourceID="ODS_type_1"
                                    DataTextField="typ_cname" DataValueField="typ_no" OnSelectedIndexChanged="ddl_type_1_SelectedIndexChanged">
                                </asp:DropDownList>
                                課程類別&nbsp;<asp:DropDownList ID="ddl_type_2" runat="server" DataSourceID="ODS_type_2"
                                    DataTextField="typ_cname" DataValueField="typ_no">
                                </asp:DropDownList>
                                &nbsp;<asp:TextBox ID="tbox_name" runat="server" Width="300px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                課程簡介
                            </th>
                            <td>
                                <asp:TextBox ID="tbox_memo" runat="server" Height="95px" TextMode="MultiLine" Width="250px"></asp:TextBox>
                            </td>
                            <th>
                                資格條件說明
                            </th>
                            <td>
                                <asp:TextBox ID="tbox_limit" runat="server" Height="95px" TextMode="MultiLine" Width="250px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                <span style="color: Red">*</span>上課地點
                            </th>
                            <td>
                                <asp:DropDownList ID="ddl_e01" runat="server" DataSourceID="ODS_e01" DataTextField="e01_name"
                                    DataValueField="e01_no">
                                </asp:DropDownList>
                            </td>
                            <th>
                                招收名額上限
                            </th>
                            <td>
                                <asp:TextBox ID="tbox_people" runat="server" Width="75px"></asp:TextBox>人
                            </td>
                        </tr>
                        <tr>
                            <th>
                                認證時數
                            </th>
                            <td>
                                <asp:TextBox ID="tbox_hour" runat="server" Width="75px">0</asp:TextBox>小時
                            </td>
                            <th>
                                <span style="color: Red">*</span>報名審核狀況
                            </th>
                            <td>
                                <asp:RadioButtonList ID="rbl_check" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                    <asp:ListItem Selected="True" Value="1">審核</asp:ListItem>
                                    <asp:ListItem Value="2">不審核</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <th>
                                講師姓名
                            </th>
                            <td>
                                <asp:TextBox ID="tbox_teacher" runat="server" Width="100px"></asp:TextBox>
                            </td>
                            <th>
                                <span style="color: Red">*</span>上線開放日期
                            </th>
                            <td>
                                <uc2:calendar ID="cal_opendate" runat="server" _Show="false" />
                            </td>
                        </tr>
                        <tr>
                            <th>
                                <span style="color: Red">*</span>報名開始日期
                            </th>
                            <td>
                                <uc2:calendar ID="cal_signsdate" runat="server" _Show="false" />
                            </td>
                            <th>
                                <span style="color: Red">*</span>報名結束日期
                            </th>
                            <td>
                                <uc2:calendar ID="cal_signedate" runat="server" _Show="false" />
                            </td>
                        </tr>
                        <tr>
                            <th>
                                <span style="color: Red">*</span>上課開始時間
                            </th>
                            <td>
                                <uc2:calendar ID="cal_sdate" runat="server" _Show="false" />
                                &nbsp;
                                <asp:DropDownList ID="ddl_sh" runat="server">
                                    <asp:ListItem>01</asp:ListItem>
                                    <asp:ListItem>02</asp:ListItem>
                                    <asp:ListItem>03</asp:ListItem>
                                    <asp:ListItem>04</asp:ListItem>
                                    <asp:ListItem>05</asp:ListItem>
                                    <asp:ListItem>06</asp:ListItem>
                                    <asp:ListItem>07</asp:ListItem>
                                    <asp:ListItem Selected="True">08</asp:ListItem>
                                    <asp:ListItem>09</asp:ListItem>
                                    <asp:ListItem>10</asp:ListItem>
                                    <asp:ListItem>11</asp:ListItem>
                                    <asp:ListItem>12</asp:ListItem>
                                    <asp:ListItem>13</asp:ListItem>
                                    <asp:ListItem>14</asp:ListItem>
                                    <asp:ListItem>15</asp:ListItem>
                                    <asp:ListItem>16</asp:ListItem>
                                    <asp:ListItem>17</asp:ListItem>
                                    <asp:ListItem>18</asp:ListItem>
                                    <asp:ListItem>19</asp:ListItem>
                                    <asp:ListItem>20</asp:ListItem>
                                    <asp:ListItem>21</asp:ListItem>
                                    <asp:ListItem>22</asp:ListItem>
                                    <asp:ListItem>23</asp:ListItem>
                                </asp:DropDownList>
                                時
                                &nbsp;
                                <asp:DropDownList ID="ddl_sm" runat="server">
                                    <asp:ListItem Selected="True">00</asp:ListItem>
                                    <asp:ListItem>10</asp:ListItem>
                                    <asp:ListItem>20</asp:ListItem>
                                    <asp:ListItem>30</asp:ListItem>
                                    <asp:ListItem>40</asp:ListItem>
                                    <asp:ListItem>50</asp:ListItem>
                                </asp:DropDownList>
                                分
                            </td>
                            <th>
                                <span style="color: Red">*</span>上課結束時間
                            </th>
                            <td>
                                <uc2:calendar ID="cal_edate" runat="server" _Show="false" />
                                &nbsp;
                                <asp:DropDownList ID="ddl_eh" runat="server">
                                    <asp:ListItem>01</asp:ListItem>
                                    <asp:ListItem>02</asp:ListItem>
                                    <asp:ListItem>03</asp:ListItem>
                                    <asp:ListItem>04</asp:ListItem>
                                    <asp:ListItem>05</asp:ListItem>
                                    <asp:ListItem>06</asp:ListItem>
                                    <asp:ListItem>07</asp:ListItem>
                                    <asp:ListItem>08</asp:ListItem>
                                    <asp:ListItem>09</asp:ListItem>
                                    <asp:ListItem>10</asp:ListItem>
                                    <asp:ListItem>11</asp:ListItem>
                                    <asp:ListItem>12</asp:ListItem>
                                    <asp:ListItem>13</asp:ListItem>
                                    <asp:ListItem>14</asp:ListItem>
                                    <asp:ListItem>15</asp:ListItem>
                                    <asp:ListItem>16</asp:ListItem>
                                    <asp:ListItem Selected="True">17</asp:ListItem>
                                    <asp:ListItem>18</asp:ListItem>
                                    <asp:ListItem>19</asp:ListItem>
                                    <asp:ListItem>20</asp:ListItem>
                                    <asp:ListItem>21</asp:ListItem>
                                    <asp:ListItem>22</asp:ListItem>
                                    <asp:ListItem>23</asp:ListItem>
                                </asp:DropDownList>
                                時
                                &nbsp;
                                <asp:DropDownList ID="ddl_em" runat="server">
                                    <asp:ListItem Selected="True">00</asp:ListItem>
                                    <asp:ListItem>10</asp:ListItem>
                                    <asp:ListItem>20</asp:ListItem>
                                    <asp:ListItem>30</asp:ListItem>
                                    <asp:ListItem>40</asp:ListItem>
                                    <asp:ListItem>50</asp:ListItem>
                                </asp:DropDownList>
                                分
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
            <asp:Button ID="btn_submit" runat="server" CssClass="b-input" Text="確定" OnClick="btn_submit_Click"
                OnClientClick="return checkInput()" />
            &nbsp;&nbsp;&nbsp;
            <asp:Button ID="btn_cancel" runat="server" CssClass="a-input" Text="取消" OnClick="btn_cancel_Click" />
        </div>
        <asp:Panel ID="Panel2" runat="server">
            <div>
                若需批次開班，請填寫梯次並按下批次開班，梯次&nbsp;<input id="Text1" type="text" style="width:50px" />&nbsp;
                <input id="Button3" type="button" value="批次開班" class="b-input" onclick="addTable_click()" />
            </div>
        </asp:Panel>
        <br />
        <div id="div_flag">
            <div>
                日期：<input id="Text2" type="text" class="calendarSelectDate" style="width: 75px" />
                &nbsp;<input type="button" onclick="setInput_click(1);" value="填滿報名開始日期" class="b-input" />
                &nbsp;<input type="button" onclick="setInput_click(2);" value="填滿報名結束日期" class="b-input" />
                &nbsp;<input type="button" onclick="setInput_click(3);" value="填滿上課開始日期" class="b-input" />
                &nbsp;<input type="button" onclick="setInput_click(4);" value="填滿上課結束日期" class="b-input" />
            </div>
            <br />
            <div>
                <table id="table2">
                    <tr>
                        <td align="center" style="width: 5%">
                            選擇
                        </td>
                        <td align="center" style="width: 9%">
                            期別
                        </td>
                        <td style="width: 15%">
                            報名開始日期
                        </td>
                        <td style="width: 15%">
                            報名結束日期
                        </td>
                        <td style="width: 26%">
                            上課開始日期
                        </td>
                        <td style="width: 26%">
                            上課結束日期
                        </td>
                        <td align="center" style="width: 4%">
                            刪除
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div id="calendarDiv">
        </div>
        <div id="div_msg" runat="server">
        </div>
        <div>
            <asp:HiddenField ID="hidd_date" runat="server" Value="" />
        </div>
    </div>
</asp:Content>
