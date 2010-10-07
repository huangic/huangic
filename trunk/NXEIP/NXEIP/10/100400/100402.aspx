﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="100402.aspx.cs" Inherits="_10_100400_100402" %>

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
                        <asp:DropDownList ID="ddl_rooms" runat="server" CssClass="select4">
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
                                <th>
                                    所在地
                                </th>
                                <td>
                                    北區職訓中心
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    所在樓層
                                </th>
                                <td>
                                    6樓
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    場地分機
                                </th>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    保管人
                                </th>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    保管人分機
                                </th>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    容納人數
                                </th>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    場地描述
                                </th>
                                <td>
                                </td>
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
                    <div class="h2">
                        <div class="name">
                            2010年4月 / 管理者</div>
                    </div>
                    <div class="h3">
                    </div>
                </div>
                <table class="big-calendar-time">
                    <tbody>
                        <tr>
                            <th>
                                時間
                            </th>
                            <th>
                                日
                            </th>
                            <th>
                                一
                            </th>
                            <th>
                                二
                            </th>
                            <th>
                                三
                            </th>
                            <th>
                                四
                            </th>
                            <th>
                                五
                            </th>
                            <th>
                                六
                            </th>
                        </tr>
                        <tr>
                            <td class="time">
                                <a href="a03-01-new.htm">06：00</a>
                            </td>
                            <td class="holiday">
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                            <td>
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                            <td>
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                            <td>
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                            <td>
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                            <td>
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                            <td class="holiday">
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                        </tr>
                        <tr>
                            <td class="time">
                                <a class="a-letter-1" href="#">07：00</a>
                            </td>
                            <td class="holiday">
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                            <td>
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                            <td>
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                            <td>
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                            <td>
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                            <td>
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                            <td class="holiday">
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                        </tr>
                        <tr>
                            <td class="time">
                                <a href="a04-02-new.htm">08：00</a>
                            </td>
                            <td class="holiday">
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                            <td rowspan="4">
                                人事室：<br>
                                鄭英法
                            </td>
                            <td>
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                            <td>
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                            <td>
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                            <td>
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                            <td class="holiday">
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                        </tr>
                        <tr>
                            <td class="time">
                                <a class="a-letter-1" href="#">09：00</a>
                            </td>
                            <td class="holiday">
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                            <td>
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                            <td>
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                            <td>
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                            <td>
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                            <td class="holiday">
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                        </tr>
                        <tr>
                            <td class="time">
                                <a href="a04-02-new.htm">10：00</a>
                            </td>
                            <td class="holiday">
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                            <td>
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                            <td>
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                            <td>
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                            <td>
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                            <td class="holiday">
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                        </tr>
                        <tr>
                            <td class="time">
                                <a href="a04-02-new.htm">11：00</a>
                            </td>
                            <td class="holiday">
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                            <td>
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                            <td>
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                            <td>
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                            <td>
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                            <td class="holiday">
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                        </tr>
                        <tr>
                            <td class="time">
                                <a class="a-letter-1" href="#">12：00</a>
                            </td>
                            <td class="holiday">
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                            <td>
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                            <td>
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                            <td>
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                            <td>
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                            <td rowspan="4">
                                人事室：<br>
                                鄭英法
                            </td>
                            <td class="holiday">
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                        </tr>
                        <tr>
                            <td class="time">
                                <a class="a-letter-1" href="#">13：00</a>
                            </td>
                            <td class="holiday">
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                            <td>
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                            <td>
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                            <td>
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                            <td>
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                            <td class="holiday">
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                        </tr>
                        <tr>
                            <td class="time">
                                <a class="a-letter-1" href="#">14：00</a>
                            </td>
                            <td class="holiday">
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                            <td>
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                            <td>
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                            <td>
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                            <td>
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                            <td class="holiday">
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                        </tr>
                        <tr>
                            <td class="time">
                                <a class="a-letter-1" href="#">15：00</a>
                            </td>
                            <td class="holiday">
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                            <td>
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                            <td>
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                            <td>
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                            <td>
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                            <td class="holiday">
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                        </tr>
                        <tr>
                            <td class="time">
                                <a class="a-letter-1" href="#">16：00</a>
                            </td>
                            <td class="holiday">
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                            <td>
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                            <td>
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                            <td>
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                            <td>
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                            <td>
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                            <td class="holiday">
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                        </tr>
                        <tr>
                            <td class="time">
                                <a href="#">17：00</a>
                            </td>
                            <td class="holiday">
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                            <td>
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                            <td>
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                            <td>
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                            <td>
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                            <td>
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                            <td class="holiday">
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                        </tr>
                        <tr>
                            <td class="time">
                                <a href="#">18：00</a>
                            </td>
                            <td class="holiday">
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                            <td>
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                            <td>
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                            <td>
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                            <td>
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                            <td>
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                            <td class="holiday">
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                        </tr>
                        <tr>
                            <td class="time">
                                <a href="#">19：00</a>
                            </td>
                            <td class="holiday">
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                            <td>
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                            <td>
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                            <td>
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                            <td>
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                            <td>
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                            <td class="holiday">
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                        </tr>
                        <tr>
                            <td class="time">
                                <a href="#">20：00</a>
                            </td>
                            <td class="holiday">
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                            <td>
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                            <td>
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                            <td>
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                            <td>
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                            <td>
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                            <td class="holiday">
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                        </tr>
                        <tr>
                            <td class="time">
                                <a href="#">21：00</a>
                            </td>
                            <td class="holiday">
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                            <td>
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                            <td>
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                            <td>
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                            <td>
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                            <td>
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                            <td class="holiday">
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                        </tr>
                        <tr>
                            <td class="time">
                                <a href="#">22：00</a>
                            </td>
                            <td class="holiday">
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                            <td>
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                            <td>
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                            <td>
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                            <td>
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                            <td>
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                            <td class="holiday">
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                        </tr>
                        <tr>
                            <td class="time">
                                <a href="#">23：00</a>
                            </td>
                            <td class="holiday">
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                            <td>
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                            <td>
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                            <td>
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                            <td>
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                            <td>
                                <a href="a04-02-new.htm">申請</a>
                            </td>
                            <td class="holiday">
                                <a href="a04-02-new.htm">申請</a>
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
            </div>
        </div>
    </div>
</asp:Content>
