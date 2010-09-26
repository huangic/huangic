<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Autocomplete.ascx.cs"
    Inherits="lib_Autocomplete" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


<script type="text/javascript">

    $(function () {

        $("#<%=tbox_str.ClientID %>").autocomplete("<%Response.Write(SrcUrl); %>",
            {

                delay: 10,

                width: 220,

                minChars: 1, //至少輸入幾個字元才開始給提示?

                matchSubset: false,

                matchContains: false,

                cacheLength: 0,

                noCache: true, //黑暗版自訂參數，每次都重新連後端查詢(適用總資料筆數很多時)

                onItemSelect: findValue,

                onFindValue: findValue,

                formatItem: function (row) {

                    return "<div style='height:12px'><div style='float:left;padding-right:5px;'></div>" +

                    "<div style='float:left;padding-right:2px;'>" + row[1] + " " + row[2] + " " + row[3] + "</div></div>";

                },

                autoFill: false,

                mustMatch: false //是否允許輸入提示清單上沒有的值?

            });

        function findValue(li) {

            if (li != null) {

                $("#<%=tbox_str.ClientID %>").val(li.extra[1]);
                $("#<%=hidd_value.ClientID %>").val(li.extra[0] + "," + li.extra[1] + "," + li.extra[2] + "," + li.extra[3] + "," + li.extra[4]);
            }
        }

    });

</script>


<div>
    <asp:TextBox ID="tbox_str" runat="server" Width="120px"></asp:TextBox>
    <asp:HiddenField ID="hidd_value" runat="server" />
</div>
<div id="msg" runat="server"></div>

