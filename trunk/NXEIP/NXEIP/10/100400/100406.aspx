<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="100406.aspx.cs" Inherits="_10_100400_100406" EnableEventValidation="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register Src="../../lib/Navigator.ascx" TagName="Navigator" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <ajaxtoolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" CombineScripts="false">
    </ajaxtoolkit:ToolkitScriptManager>
    <asp:Label ID="lab_etime" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lab_stime" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lab_today" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lab_BorrowsSignType" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lab_chekuan" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lab_car" runat="server" Visible="False"></asp:Label>
    <uc1:Navigator ID="Navigator1" runat="server" SysFuncNo="100406" />
    <div class="PersonalCalendarLayout">
        <div class="left">
            <div class="block-1">
                <div class="header_place">請先選擇車輛</div>
                <table class="calendar-place">
                    <tr>
                        <td colspan="2">
                            <div class="select-L">
                                <div class="bp1">&nbsp;車輛種類</div>
                                <div class="bp2">
                                    <asp:DropDownList ID="ddl_chekuan" runat="server" CssClass="select4">
                                    </asp:DropDownList>
                                    <ajaxtoolkit:CascadingDropDown ID="ddl_chekuan_CascadingDropDown" runat="server" Category="chekuan"
                                        LoadingText="讀取中..." PromptText="請選擇" PromptValue="0" ServiceMethod="GetChekuan"
                                        TargetControlID="ddl_chekuan" ServicePath="../../WebService/m02ws.asmx" 
                                        UseContextKey="True">
                                    </ajaxtoolkit:CascadingDropDown>
                                </div>
                            </div>
                            <div class="select-L">
                                <div class="bp1">&nbsp;牌照號碼</div>
                                <div class="bp2">
                                    <asp:DropDownList ID="ddl_car" runat="server" CssClass="select4" AutoPostBack="True"
                                        OnSelectedIndexChanged="ddl_car_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <ajaxtoolkit:CascadingDropDown ID="ddl_car_CascadingDropDown" runat="server" TargetControlID="ddl_car"
                                        Category="car" ContextKey="" LoadingText="讀取中..." ParentControlID="ddl_chekuan"
                                        PromptText="請選擇" PromptValue="0" ServiceMethod="GetCar" ServicePath="../../WebService/m02ws.asmx">
                                    </ajaxtoolkit:CascadingDropDown>
                                </div>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td class="place1_bg">車輛編號</td>
                        <td class="place2_bg"><asp:Label ID="lab_code" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="place1_bg">保 &nbsp;管 &nbsp;人</td>
                        <td class="place2_bg"><asp:Label ID="lab_peouid" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="place1_bg">顏　　色</td>
                        <td class="place2_bg"><asp:Label ID="lab_color" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="place1_bg">廠　　牌</td>
                        <td class="place2_bg"><asp:Label ID="lab_mark" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="place1_bg">汽缸排氣量</td>
                        <td class="place2_bg"><asp:Label ID="lab_cc" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="place1_bg">車輛備註</td>
                        <td class="place2_bg"><asp:Label ID="lab_memo" runat="server"></asp:Label></td>
                    </tr>
                </table>
            </div>
        </div>
        <div class="right">
            <div class="layoutDiv">
                <div class="header">
                    <div class="h1"></div>
                    <div class="h2">
                        <div class="h2_place">
                            <ul>
                                <li><asp:LinkButton ID="lbtn_PMonth" runat="server" onclick="lbtn_PMonth_Click">&lt;&lt;上個月</asp:LinkButton></li>
                                <li><asp:LinkButton ID="lbtn_PWeeks" runat="server" onclick="lbtn_PWeeks_Click">&lt;上星期</asp:LinkButton></li>
                            </ul>
                        </div>
                        <div class="h3_place">
                            <ul>
                                <li><asp:LinkButton ID="lbtn_NMonth" runat="server" onclick="lbtn_NMonth_Click">下個月&gt;&gt;</asp:LinkButton></li>
                                <li><asp:LinkButton ID="lbtn_NWeeks" runat="server" onclick="lbtn_NWeeks_Click">下星期&gt;</asp:LinkButton></li>
                            </ul>
                        </div>
                    </div>
                    <div class="h3">
                    </div>
                </div>
                <asp:Table ID="Table1" runat="server" CssClass="calendar-place-week">
                    <asp:TableRow ID="TableRow1" runat="server">
                        <asp:TableCell ID="TableCell0" CssClass="headtitle" runat="server" RowSpan="2">時間</asp:TableCell>
                        <asp:TableCell ID="TableCell1" CssClass="headtitle" runat="server">一</asp:TableCell>
                        <asp:TableCell ID="TableCell2" CssClass="headtitle" runat="server">二</asp:TableCell>
                        <asp:TableCell ID="TableCell3" CssClass="headtitle" runat="server">三</asp:TableCell>
                        <asp:TableCell ID="TableCell4" CssClass="headtitle" runat="server">四</asp:TableCell>
                        <asp:TableCell ID="TableCell5" CssClass="headtitle" runat="server">五</asp:TableCell>
                        <asp:TableCell ID="TableCell6" CssClass="headtitle" runat="server">六</asp:TableCell>
                        <asp:TableCell ID="TableCell7" CssClass="headtitle" runat="server">日</asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="TableRow2" runat="server">
                        <asp:TableCell ID="TableCell8" CssClass="headtitle" runat="server">10-04</asp:TableCell>
                        <asp:TableCell ID="TableCell9" CssClass="headtitle" runat="server">10-05</asp:TableCell>
                        <asp:TableCell ID="TableCell10" CssClass="headtitle" runat="server">10-06</asp:TableCell>
                        <asp:TableCell ID="TableCell11" CssClass="headtitle" runat="server">10-07</asp:TableCell>
                        <asp:TableCell ID="TableCell12" CssClass="headtitle" runat="server">10-08</asp:TableCell>
                        <asp:TableCell ID="TableCell13" CssClass="headtitle" runat="server">10-09</asp:TableCell>
                        <asp:TableCell ID="TableCell14" CssClass="headtitle" runat="server">10-10</asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="TableRow3" runat="server">
                        <asp:TableCell ID="TableCell15" runat="server" CssClass="rowtime_bg">06:00</asp:TableCell>
                        <asp:TableCell ID="TableCell16" runat="server" CssClass="Nholiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell17" runat="server" CssClass="Nholiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell18" runat="server" CssClass="Nholiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell19" runat="server" CssClass="Nholiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell20" runat="server" CssClass="Nholiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell21" runat="server" CssClass="holiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell22" runat="server" CssClass="holiday_bg"></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="TableRow4" runat="server">
                        <asp:TableCell ID="TableCell23" runat="server" CssClass="rowtime_bg">07:00</asp:TableCell>
                        <asp:TableCell ID="TableCell24" runat="server" CssClass="Nholiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell25" runat="server" CssClass="Nholiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell26" runat="server" CssClass="Nholiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell27" runat="server" CssClass="Nholiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell28" runat="server" CssClass="Nholiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell29" runat="server" CssClass="holiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell30" runat="server" CssClass="holiday_bg"></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="TableRow5" runat="server">
                        <asp:TableCell ID="TableCell31" runat="server" CssClass="rowtime_bg">08:00</asp:TableCell>
                        <asp:TableCell ID="TableCell32" runat="server" CssClass="Nholiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell33" runat="server" CssClass="Nholiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell34" runat="server" CssClass="Nholiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell35" runat="server" CssClass="Nholiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell36" runat="server" CssClass="Nholiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell37" runat="server" CssClass="holiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell38" runat="server" CssClass="holiday_bg"></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="TableRow6" runat="server">
                        <asp:TableCell ID="TableCell39" runat="server" CssClass="rowtime_bg">09:00</asp:TableCell>
                        <asp:TableCell ID="TableCell40" runat="server" CssClass="Nholiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell41" runat="server" CssClass="Nholiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell42" runat="server" CssClass="Nholiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell43" runat="server" CssClass="Nholiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell44" runat="server" CssClass="Nholiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell45" runat="server" CssClass="holiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell46" runat="server" CssClass="holiday_bg"></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="TableRow7" runat="server">
                        <asp:TableCell ID="TableCell47" runat="server" CssClass="rowtime_bg">10:00</asp:TableCell>
                        <asp:TableCell ID="TableCell48" runat="server" CssClass="Nholiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell49" runat="server" CssClass="Nholiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell50" runat="server" CssClass="Nholiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell51" runat="server" CssClass="Nholiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell52" runat="server" CssClass="Nholiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell53" runat="server" CssClass="holiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell54" runat="server" CssClass="holiday_bg"></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="TableRow8" runat="server">
                        <asp:TableCell ID="TableCell55" runat="server" CssClass="rowtime_bg">11:00</asp:TableCell>
                        <asp:TableCell ID="TableCell56" runat="server" CssClass="Nholiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell57" runat="server" CssClass="Nholiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell58" runat="server" CssClass="Nholiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell59" runat="server" CssClass="Nholiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell60" runat="server" CssClass="Nholiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell61" runat="server" CssClass="holiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell62" runat="server" CssClass="holiday_bg"></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="TableRow9" runat="server">
                        <asp:TableCell ID="TableCell63" runat="server" CssClass="rowtime_bg">12:00</asp:TableCell>
                        <asp:TableCell ID="TableCell64" runat="server" CssClass="Nholiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell65" runat="server" CssClass="Nholiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell66" runat="server" CssClass="Nholiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell67" runat="server" CssClass="Nholiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell68" runat="server" CssClass="Nholiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell69" runat="server" CssClass="holiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell70" runat="server" CssClass="holiday_bg"></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="TableRow10" runat="server">
                        <asp:TableCell ID="TableCell71" runat="server" CssClass="rowtime_bg">13:00</asp:TableCell>
                        <asp:TableCell ID="TableCell72" runat="server" CssClass="Nholiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell73" runat="server" CssClass="Nholiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell74" runat="server" CssClass="Nholiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell75" runat="server" CssClass="Nholiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell76" runat="server" CssClass="Nholiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell77" runat="server" CssClass="holiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell78" runat="server" CssClass="holiday_bg"></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="TableRow11" runat="server">
                        <asp:TableCell ID="TableCell79" runat="server" CssClass="rowtime_bg">14:00</asp:TableCell>
                        <asp:TableCell ID="TableCell80" runat="server" CssClass="Nholiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell81" runat="server" CssClass="Nholiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell82" runat="server" CssClass="Nholiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell83" runat="server" CssClass="Nholiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell84" runat="server" CssClass="Nholiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell85" runat="server" CssClass="holiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell86" runat="server" CssClass="holiday_bg"></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="TableRow12" runat="server">
                        <asp:TableCell ID="TableCell87" runat="server" CssClass="rowtime_bg">15:00</asp:TableCell>
                        <asp:TableCell ID="TableCell88" runat="server" CssClass="Nholiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell89" runat="server" CssClass="Nholiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell90" runat="server" CssClass="Nholiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell91" runat="server" CssClass="Nholiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell92" runat="server" CssClass="Nholiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell93" runat="server" CssClass="holiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell94" runat="server" CssClass="holiday_bg"></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="TableRow13" runat="server">
                        <asp:TableCell ID="TableCell95" runat="server" CssClass="rowtime_bg">16:00</asp:TableCell>
                        <asp:TableCell ID="TableCell96" runat="server" CssClass="Nholiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell97" runat="server" CssClass="Nholiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell98" runat="server" CssClass="Nholiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell99" runat="server" CssClass="Nholiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell100" runat="server" CssClass="Nholiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell101" runat="server" CssClass="holiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell102" runat="server" CssClass="holiday_bg"></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="TableRow14" runat="server">
                        <asp:TableCell ID="TableCell103" runat="server" CssClass="rowtime_bg">17:00</asp:TableCell>
                        <asp:TableCell ID="TableCell104" runat="server" CssClass="Nholiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell105" runat="server" CssClass="Nholiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell106" runat="server" CssClass="Nholiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell107" runat="server" CssClass="Nholiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell108" runat="server" CssClass="Nholiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell109" runat="server" CssClass="holiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell110" runat="server" CssClass="holiday_bg"></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="TableRow15" runat="server">
                        <asp:TableCell ID="TableCell111" runat="server" CssClass="rowtime_bg">18:00</asp:TableCell>
                        <asp:TableCell ID="TableCell112" runat="server" CssClass="Nholiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell113" runat="server" CssClass="Nholiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell114" runat="server" CssClass="Nholiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell115" runat="server" CssClass="Nholiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell116" runat="server" CssClass="Nholiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell117" runat="server" CssClass="holiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell118" runat="server" CssClass="holiday_bg"></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="TableRow16" runat="server">
                        <asp:TableCell ID="TableCell119" runat="server" CssClass="rowtime_bg">19:00</asp:TableCell>
                        <asp:TableCell ID="TableCell120" runat="server" CssClass="Nholiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell121" runat="server" CssClass="Nholiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell122" runat="server" CssClass="Nholiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell123" runat="server" CssClass="Nholiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell124" runat="server" CssClass="Nholiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell125" runat="server" CssClass="holiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell126" runat="server" CssClass="holiday_bg"></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="TableRow17" runat="server">
                        <asp:TableCell ID="TableCell127" runat="server" CssClass="rowtime_bg">20:00</asp:TableCell>
                        <asp:TableCell ID="TableCell128" runat="server" CssClass="Nholiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell129" runat="server" CssClass="Nholiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell130" runat="server" CssClass="Nholiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell131" runat="server" CssClass="Nholiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell132" runat="server" CssClass="Nholiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell133" runat="server" CssClass="holiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell134" runat="server" CssClass="holiday_bg"></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="TableRow18" runat="server">
                        <asp:TableCell ID="TableCell135" runat="server" CssClass="rowtime_bg">21:00</asp:TableCell>
                        <asp:TableCell ID="TableCell136" runat="server" CssClass="Nholiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell137" runat="server" CssClass="Nholiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell138" runat="server" CssClass="Nholiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell139" runat="server" CssClass="Nholiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell140" runat="server" CssClass="Nholiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell141" runat="server" CssClass="holiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell142" runat="server" CssClass="holiday_bg"></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="TableRow19" runat="server">
                        <asp:TableCell ID="TableCell143" runat="server" CssClass="rowtime_bg">22:00</asp:TableCell>
                        <asp:TableCell ID="TableCell144" runat="server" CssClass="Nholiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell145" runat="server" CssClass="Nholiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell146" runat="server" CssClass="Nholiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell147" runat="server" CssClass="Nholiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell148" runat="server" CssClass="Nholiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell149" runat="server" CssClass="holiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell150" runat="server" CssClass="holiday_bg"></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="TableRow20" runat="server">
                        <asp:TableCell ID="TableCell151" runat="server" CssClass="rowtime_bg">23:00</asp:TableCell>
                        <asp:TableCell ID="TableCell152" runat="server" CssClass="Nholiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell153" runat="server" CssClass="Nholiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell154" runat="server" CssClass="Nholiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell155" runat="server" CssClass="Nholiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell156" runat="server" CssClass="Nholiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell157" runat="server" CssClass="holiday_bg"></asp:TableCell>
                        <asp:TableCell ID="TableCell158" runat="server" CssClass="holiday_bg"></asp:TableCell>
                    </asp:TableRow>
                </asp:Table>
                <div class="footer">
                    <div class="f1">
                    </div>
                    <div class="f2">
                    </div>
                    <div class="f3">
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>