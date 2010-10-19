<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="300303-1.aspx.cs" Inherits="_30_300300_300303_1" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="System.Web.Entity, Version=3.5.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
    Namespace="System.Web.UI.WebControls" TagPrefix="asp" %>
<%@ Register Src="../../lib/Navigator.ascx" TagName="Navigator" TagPrefix="uc1" %>
<%@ Register src="../../lib/tree/jQueryDepartTree.ascx" tagname="jQueryDepartTree" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
<script type="text/javascript" language="javascript">

    var TableName = '<%=Table3.ClientID%>';

    $(document).ready(function () {

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
            }
            //檢查是否只輸入0     
            if ($(this).val() == '0') {
                alert('至少是1人!');
                this.value = '1';
            }
        }).focusout(function () {
            //檢查是否未輸入數量     
            if ($(this).val() == '') {
                alert('至少是1人!');
                this.value = '1';
            }
        });

    });

    
    function InsertRows() {

        //人數
        var people = $('#Text1').val();
        
        //檢查是否有部門
        if ($('#<%=jQueryDepartTree1.ListBoxClientID%> option:first').text() == '') {
            alert('請選擇部門!');
            return false;
        }
        
        //加入列表
        $('#<%=jQueryDepartTree1.ListBoxClientID%> option').each(function () {
            var dep_val = $(this).val();
            var dep_text = $(this).text();
            //是否已有在列表上
            var check = true;
            var trlen = $("#" + TableName + " tr").length;
            for (var i = 1; i <= trlen; i++) {
                var dep_no = $('#tbox_' + i).val();
                if (dep_val == dep_no) {
                    check = false;
                    //更換人數
                    var oldpeo = parseInt($("#" + TableName).find("tr:eq(" + i + ")").find("td:eq(1)").text());
                    $("#" + TableName).find("tr:eq(" + i + ")").find("td:eq(1)").text((parseInt(people) + oldpeo));
                    break;
                }
            }
            if (check) {
                var idnum = $("#" + TableName + " tr").length;
                var str = "<tr>";
                str += "<td><input id='tbox_" + idnum + "' type='hidden' value='" + dep_val + "' />" + dep_text + "</td>";
                str += "<td align='center'>" + people + "</td>";
                str += "<td align='center'><img class='delete' src='../../image/delete.gif' /></td>";
                str += "</tr>";
                $(str).appendTo($("#" + TableName));
            }
        });
    }

    function DeleteRow(elem) {
        var row = $(elem).parent().parent();
        row.remove();
    }

    function GetTableValue() {
        var str = "";
        var trlen = $("#" + TableName + " tr").length;
        for (var i = 1; i < trlen; i++) {
            var dep_no = $('#tbox_'+i).val();
            var people = $("#" + TableName).find("tr:eq(" + i + ")").find("td:eq(1)").text();

            if (str == "") {
                str = dep_no + "_" + people;
            } else {
                str += "," + dep_no + "_" + people;
            }
        }
        $('#<%=hidd_data.ClientID%>').val(str);
    }

</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:HiddenField ID="hidd_no" runat="server" />
    <uc1:Navigator ID="Navigator1" runat="server" SysFuncNo="300303" />
    <div class="tableDiv">
        <div class="header">
            <div class="h1">
            </div>
            <div class="h2">
                
            </div>
            <div class="h3">
            </div>
        </div>
        <table id="table1">
            <tr>
                <th>
                    學習機構
                </th>
                <td>
                    <asp:Label ID="lab_mechani" runat="server"></asp:Label>
                </td>
                <th>
                    課程代碼
                </th>
                <td>
                    <asp:Label ID="lab_code" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    課程類別
                </th>
                <td>
                    <asp:Label ID="lab_typ_name" runat="server"></asp:Label>
                </td>
                <th>
                    課程名稱(期別)
                </th>
                <td>
                    <asp:Label ID="lab_name_flag" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <th>
                    部門人數限制
                </th>
                <td colspan="3">
                    部門&nbsp;
                    <uc2:jQueryDepartTree ID="jQueryDepartTree1" runat="server" />
                    &nbsp;限制人數&nbsp;<input id="Text1" type="text" style="width: 50px" value="1" />
                    &nbsp;<input id="Button1" type="button" value="加入" class="b-input" onclick="InsertRows()" />
                </td>
            </tr>
            <tr>
                <th>
                    &nbsp;
                </th>
                <td colspan="3">
                    <div>
                        <asp:Table ID="Table3" runat="server">
                            <asp:TableRow runat="server">
                                <asp:TableCell runat="server" HorizontalAlign="Center" Width="150px">部門</asp:TableCell>
                                <asp:TableCell runat="server" HorizontalAlign="Center" Width="75px">人數限制</asp:TableCell>
                                <asp:TableCell runat="server" HorizontalAlign="Center" Width="45px">刪除</asp:TableCell>
                            </asp:TableRow>
                        </asp:Table>
                    </div>
                </td>
            </tr>
        </table>
        <div class="bottom">
            <asp:Button ID="btn_submit" runat="server" CssClass="b-input" Text="確定" OnClick="btn_submit_Click"
                OnClientClick="GetTableValue()" />
            &nbsp;&nbsp;&nbsp;
            <asp:Button ID="btn_cancel" runat="server" CssClass="a-input" Text="取消" OnClick="btn_cancel_Click" />
        </div>
    <div>
    </div>
        <asp:HiddenField ID="hidd_data" runat="server" />
    </div>
</asp:Content>

