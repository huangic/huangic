<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="100402.aspx.cs" Inherits="_10_100400_100402" EnableEventValidation="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register Src="../../lib/Navigator.ascx" TagName="Navigator" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <ajaxtoolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajaxtoolkit:ToolkitScriptManager>
    <uc1:Navigator ID="Navigator1" runat="server" SysFuncNo="100402" />
    <div class="placeLayout">
        <div class="left">
            <div class="layoutDiv">
                <div class="header">
                    <div class="h1">
                    </div>
                    <div class="h2">
                        <div class="name1">
                            請先選擇場所</div>
                    </div>
                    <div class="h3">
                    </div>
                </div>
                <div class="top">
                    <div align="left">
                        <span class="icon">所在地</span>
                        <asp:DropDownList ID="ddl_spot" runat="server" CssClass="select4">
                        </asp:DropDownList>
                        <ajaxtoolkit:CascadingDropDown ID="ddl_spot_CascadingDropDown" runat="server" Category="spot"
                            LoadingText="讀取中..." PromptText="請選擇" PromptValue="0" ServiceMethod="GetSpot"
                            TargetControlID="ddl_spot" ServicePath="../../WebService/place.asmx">
                        </ajaxtoolkit:CascadingDropDown>
                        <br />
                        <span class="icon">場地</span>
                        <asp:DropDownList ID="ddl_rooms" runat="server" CssClass="select4" 
                            AutoPostBack="True" 
                            onselectedindexchanged="ddl_rooms_SelectedIndexChanged">
                        </asp:DropDownList>
                        <ajaxtoolkit:CascadingDropDown ID="ddl_rooms_CascadingDropDown" runat="server" TargetControlID="ddl_rooms"
                            Category="rooms" ContextKey="" LoadingText="讀取中..." ParentControlID="ddl_spot"
                            PromptText="請選擇" PromptValue="0" ServiceMethod="GetRooms" ServicePath="../../WebService/place.asmx">
                        </ajaxtoolkit:CascadingDropDown>
                    </div>
                </div>
                <div class="bottom">
                    <table class="place">
                        <tbody>
                            <tr>
                                <th>所在地</th>
                                <td><asp:Label ID="lab_spot" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <th>所在樓層</th>
                                <td><asp:Label ID="lab_floor" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <th>保管人</th>
                                <td><asp:Label ID="lab_oneuid" runat="server"></asp:Label></td>
                            </tr>
                            <tr>
                                <th>保管人分機</th>
                                <td><asp:Label ID="lab_ext" runat="server"></asp:Label><asp:Label ID="lab_stime" 
                                        runat="server" Visible="False"></asp:Label></td>
                            </tr>
                            <tr>
                                <th>容納人數</th>
                                <td><asp:Label ID="lab_human" runat="server"></asp:Label><asp:Label ID="lab_etime" 
                                        runat="server" Visible="False"></asp:Label></td>
                            </tr>
                            <tr>
                                <th>場地描述</th>
                                <td><asp:Label ID="lab_describe" runat="server"></asp:Label></td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
        <div class="right">
            <div class="layoutDiv">
                <div class="header">
                    <div class="h1">
                    </div>
                    <div class="h21">
                        <div class="name2">
                            <asp:LinkButton ID="LinkButton1" runat="server">&lt;&lt;上個月</asp:LinkButton>&nbsp;<asp:LinkButton ID="LinkButton2" runat="server">&lt;&lt;上星期</asp:LinkButton>
                        </div>
                        <div class="name3">&nbsp;
                            <asp:Label ID="lab_today" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="lab_PetitionSignType" runat="server" Visible="False"></asp:Label>
                         </div>
                        <div class="name4">
                            <asp:LinkButton ID="LinkButton4" runat="server">下星期&gt;&gt;</asp:LinkButton>&nbsp;<asp:LinkButton ID="LinkButton3" runat="server">下個月&gt;&gt;</asp:LinkButton>
                        </div>
                    </div>
                    <div class="h3">
                    </div>
                </div>
                <asp:Table ID="Table1" runat="server" CssClass="place-calendar-time">
                    <asp:TableRow runat="server">
                        <asp:TableCell runat="server" RowSpan="2" CssClass="title">時間</asp:TableCell>
                        <asp:TableCell ID="TableCell1" CssClass="title" runat="server">一</asp:TableCell>
                        <asp:TableCell ID="TableCell2" CssClass="title" runat="server">二</asp:TableCell>
                        <asp:TableCell ID="TableCell3" CssClass="title" runat="server">三</asp:TableCell>
                        <asp:TableCell ID="TableCell4" CssClass="title" runat="server">四</asp:TableCell>
                        <asp:TableCell ID="TableCell5" CssClass="title" runat="server">五</asp:TableCell>
                        <asp:TableCell ID="TableCell6" CssClass="title" runat="server">六</asp:TableCell>
                        <asp:TableCell ID="TableCell7" CssClass="title" runat="server">日</asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow runat="server" CssClass="title">
                        <asp:TableCell ID="TableCell8" CssClass="title" runat="server">10/04</asp:TableCell>
                        <asp:TableCell ID="TableCell9" CssClass="title" runat="server">10/05</asp:TableCell>
                        <asp:TableCell ID="TableCell10" CssClass="title" runat="server">10/06</asp:TableCell>
                        <asp:TableCell ID="TableCell11" CssClass="title" runat="server">10/07</asp:TableCell>
                        <asp:TableCell ID="TableCell12" CssClass="title" runat="server">10/08</asp:TableCell>
                        <asp:TableCell ID="TableCell13" CssClass="title" runat="server">10/09</asp:TableCell>
                        <asp:TableCell ID="TableCell14" CssClass="title" runat="server">10/10</asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow runat="server">
                        <asp:TableCell ID="TableCell15" runat="server" CssClass="time">06:00</asp:TableCell>
                        <asp:TableCell ID="TableCell16" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell17" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell18" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell19" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell20" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell21" class="holiday" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell22" class="holiday" runat="server"></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="TableRow1" runat="server">
                        <asp:TableCell ID="TableCell23" runat="server" CssClass="time">07:00</asp:TableCell>
                        <asp:TableCell ID="TableCell24" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell25" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell26" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell27" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell28" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell29" class="holiday" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell30" class="holiday" runat="server"></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="TableRow2" runat="server">
                        <asp:TableCell ID="TableCell31" runat="server" CssClass="time">08:00</asp:TableCell>
                        <asp:TableCell ID="TableCell32" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell33" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell34" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell35" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell36" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell37" class="holiday" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell38" class="holiday" runat="server"></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="TableRow3" runat="server">
                        <asp:TableCell ID="TableCell39" runat="server" CssClass="time">09:00</asp:TableCell>
                        <asp:TableCell ID="TableCell40" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell41" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell42" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell43" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell44" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell45" class="holiday" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell46" class="holiday" runat="server"></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="TableRow4" runat="server">
                        <asp:TableCell ID="TableCell47" runat="server" CssClass="time">10:00</asp:TableCell>
                        <asp:TableCell ID="TableCell48" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell49" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell50" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell51" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell52" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell53" class="holiday" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell54" class="holiday" runat="server"></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="TableRow5" runat="server">
                        <asp:TableCell ID="TableCell55" runat="server" CssClass="time">11:00</asp:TableCell>
                        <asp:TableCell ID="TableCell56" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell57" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell58" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell59" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell60" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell61" class="holiday" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell62" class="holiday" runat="server"></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="TableRow6" runat="server">
                        <asp:TableCell ID="TableCell63" runat="server" CssClass="time">12:00</asp:TableCell>
                        <asp:TableCell ID="TableCell64" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell65" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell66" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell67" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell68" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell69" class="holiday" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell70" class="holiday" runat="server"></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="TableRow7" runat="server">
                        <asp:TableCell ID="TableCell71" runat="server" CssClass="time">13:00</asp:TableCell>
                        <asp:TableCell ID="TableCell72" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell73" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell74" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell75" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell76" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell77" class="holiday" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell78" class="holiday" runat="server"></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="TableRow8" runat="server">
                        <asp:TableCell ID="TableCell79" runat="server" CssClass="time">14:00</asp:TableCell>
                        <asp:TableCell ID="TableCell80" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell81" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell82" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell83" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell84" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell85" class="holiday" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell86" class="holiday" runat="server"></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="TableRow9" runat="server">
                        <asp:TableCell ID="TableCell87" runat="server" CssClass="time">15:00</asp:TableCell>
                        <asp:TableCell ID="TableCell88" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell89" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell90" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell91" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell92" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell93" class="holiday" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell94" class="holiday" runat="server"></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="TableRow10" runat="server">
                        <asp:TableCell ID="TableCell95" runat="server" CssClass="time">16:00</asp:TableCell>
                        <asp:TableCell ID="TableCell96" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell97" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell98" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell99" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell100" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell101" class="holiday" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell102" class="holiday" runat="server"></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="TableRow11" runat="server">
                        <asp:TableCell ID="TableCell103" runat="server" CssClass="time">17:00</asp:TableCell>
                        <asp:TableCell ID="TableCell104" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell105" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell106" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell107" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell108" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell109" class="holiday" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell110" class="holiday" runat="server"></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="TableRow12" runat="server">
                        <asp:TableCell ID="TableCell111" runat="server" CssClass="time">18:00</asp:TableCell>
                        <asp:TableCell ID="TableCell112" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell113" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell114" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell115" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell116" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell117" class="holiday" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell118" class="holiday" runat="server"></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="TableRow13" runat="server">
                        <asp:TableCell ID="TableCell119" runat="server" CssClass="time">19:00</asp:TableCell>
                        <asp:TableCell ID="TableCell120" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell121" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell122" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell123" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell124" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell125" class="holiday" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell126" class="holiday" runat="server"></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="TableRow14" runat="server">
                        <asp:TableCell ID="TableCell127" runat="server" CssClass="time">20:00</asp:TableCell>
                        <asp:TableCell ID="TableCell128" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell129" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell130" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell131" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell132" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell133" class="holiday" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell134" class="holiday" runat="server"></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="TableRow15" runat="server">
                        <asp:TableCell ID="TableCell135" runat="server" CssClass="time">21:00</asp:TableCell>
                        <asp:TableCell ID="TableCell136" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell137" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell138" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell139" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell140" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell141" class="holiday" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell142" class="holiday" runat="server"></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="TableRow16" runat="server">
                        <asp:TableCell ID="TableCell143" runat="server" CssClass="time">22:00</asp:TableCell>
                        <asp:TableCell ID="TableCell144" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell145" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell146" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell147" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell148" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell149" class="holiday" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell150" class="holiday" runat="server"></asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow ID="TableRow17" runat="server">
                        <asp:TableCell ID="TableCell151" runat="server" CssClass="time">23:00</asp:TableCell>
                        <asp:TableCell ID="TableCell152" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell153" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell154" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell155" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell156" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell157" class="holiday" runat="server"></asp:TableCell>
                        <asp:TableCell ID="TableCell158" class="holiday" runat="server"></asp:TableCell>
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
