<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="100301-4.aspx.cs" Inherits="_10_100300_100301_4" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

      <div class="nav">
        <span>個人應用 / 行事曆 /<strong> 個人行事曆 </strong></span>
    </div>
    <div class="PersonalCalendarLayout">
        <div class="left">
            <div class="tabHeader">
                <div class="Tab">
                    <div class="t1">
                    </div>
                    <div class="t2">
                        日</div>
                    <div class="t3">
                    </div>
                </div>
                <div class="Tab">
                    <div class="t1">
                    </div>
                    <div class="t2">
                        <a href="100301-1.aspx">周</a></div>
                    <div class="t3">
                    </div>
                </div>
                <div class="Tab">
                    <div class="t1">
                    </div>
                    <div class="t2">
                        <a href="100301-2.aspx">月</a></div>
                    <div class="t3">
                    </div>
                </div>
                <div class="Tab">
                    <div class="t1">
                    </div>
                    <div class="t2">
                        <a href="100301-3.aspx">年</a></div>
                    <div class="t3">
                    </div>
                </div>
                <div class="currentTab">
                    <div class="t1">
                    </div>
                    <div class="t2">
                        <a href="100301-4.aspx">列表</a></div>
                    <div class="t3">
                    </div>
                </div>
            </div>
            <div class="block-1">
                <div class="header">
                    <div class="h1">
                    </div>
                    <div class="h2">
                        2010年4月</div>
                    <div class="h3">
                    </div>
                </div>
                <table class="calendar">
                    <tbody>
                        <tr>
                            <th class="holiday">
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
                            <th class="holiday">
                                六
                            </th>
                        </tr>
                        <tr>
                            <td class="holiday">
                                26
                            </td>
                            <td>
                                27
                            </td>
                            <td>
                                28
                            </td>
                            <td>
                                29
                            </td>
                            <td>
                                30
                            </td>
                            <td>
                                31
                            </td>
                            <td class="holiday">
                                <a href="#">1</a>
                            </td>
                        </tr>
                        <tr>
                            <td class="holiday">
                                <a href="#">2</a>
                            </td>
                            <td class="today">
                                <a href="#">3</a>
                            </td>
                            <td>
                                <a href="#">4</a>
                            </td>
                            <td>
                                <a href="#">5</a>
                            </td>
                            <td>
                                <a href="#">6</a>
                            </td>
                            <td>
                                <a href="#">7</a>
                            </td>
                            <td class="holiday">
                                <a href="#">8</a>
                            </td>
                        </tr>
                        <tr>
                            <td class="holiday">
                                <a href="#">9</a>
                            </td>
                            <td>
                                <a href="#">10</a>
                            </td>
                            <td>
                                <a href="#">11</a>
                            </td>
                            <td>
                                <a href="#">12</a>
                            </td>
                            <td>
                                <a href="#">13</a>
                            </td>
                            <td>
                                <a href="#">14</a>
                            </td>
                            <td class="holiday">
                                <a href="#">15</a>
                            </td>
                        </tr>
                        <tr>
                            <td class="holiday">
                                <a href="#">16</a>
                            </td>
                            <td>
                                <a href="#">17</a>
                            </td>
                            <td>
                                <a href="#">18</a>
                            </td>
                            <td>
                                <a href="#">19</a>
                            </td>
                            <td>
                                <a href="#">20</a>
                            </td>
                            <td>
                                <a href="#">21</a>
                            </td>
                            <td class="holiday">
                                <a href="#">22</a>
                            </td>
                        </tr>
                        <tr>
                            <td class="holiday">
                                <a href="#">23</a>
                            </td>
                            <td>
                                <a href="#">24</a>
                            </td>
                            <td>
                                <a href="#">25</a>
                            </td>
                            <td>
                                <a href="#">26</a>
                            </td>
                            <td>
                                <a href="#">27</a>
                            </td>
                            <td>
                                <a href="#">28</a>
                            </td>
                            <td class="holiday">
                                <a href="#">29</a>
                            </td>
                        </tr>
                        <tr>
                            <td class="holiday">
                                <a href="#">30</a>
                            </td>
                            <td>
                                <a href="#">31</a>
                            </td>
                            <td>
                                1
                            </td>
                            <td>
                                2
                            </td>
                            <td>
                                3
                            </td>
                            <td>
                                4
                            </td>
                            <td class="holiday">
                                5
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div class="center">
                <span class="a-letter-2">今天是2010-05-01星期六</span>
            </div>
            
            <div class="border-bottom-block">
                <span class="icon a-letter-1">可查看之他人行事曆
                    <br>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 部門
                    <select name="select">
                        <option selected="selected">請選擇</option>
                    </select>
                    <br>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 姓名 </span>
                <select name="select2">
                    <option selected="selected">請選擇</option>
                </select>
                <input type="button" value="搜尋" class="b-input" name="Submit">
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
                        <div class="function">
                            <input type="button" value="列印" class="b-input" name="Submit32">
                        </div>
                    </div>
                    <div class="h3">
                    </div>
                </div>
                <table class="event-list">
                                        <tbody>
                                            <tr>
                                                <th rowspan="3">
                                                   
                                                        05-01<br/>
                                                        星期一
                                                </th>
                                                <td>
                                                        <span class="icon-event">開會</span>
                                                        <br />
                                                        <span class="icon-place">08:00</span>
                                                        <span class="icon-place">會議室</span>
                                                </td>

                                            </tr>
                                             <tr>
                                              
                                                <td>
                                                    
                                                       <span class="icon-event">開會</span>
                                                        <br />
                                                        <span class="icon-place">12:00</span>
                                                        <span class="icon-place">會議室</span>
                                                </td>

                                            </tr>
                                             <tr>
                                                
                                                <td>
                                                    
                                                        <span class="icon-event">開會</span>
                                                        <br />
                                                        <span class="icon-place">16:00</span>
                                                        <span class="icon-place">會議室</span>
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

