<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="100402-1.aspx.cs" Inherits="_10_100400_100402_1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxtoolkit" %>
<%@ Register Src="../../lib/Navigator.ascx" TagName="Navigator" TagPrefix="uc1" %>
<%@ Register src="../../lib/calendar.ascx" tagname="calendar" tagprefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <ajaxtoolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajaxtoolkit:ToolkitScriptManager>
    <script type="text/javascript" src="../../js/lytebox.js"></script>
    <uc1:Navigator ID="Navigator1" runat="server" SysFuncNo="100402" />
    <div class="tableDiv">
        <div class="header">
            <div class="h1"></div>
            <div class="h2"><div class="name">場地申請</div></div>
            <div class="h3"></div>
        </div>
        <table>
            <tbody>
              <tr>
                <td colspan="4">
                  <ul>
                    <li><span class="a-letter-Red">ㄧ、注意事項：申請借用場地設施時，如無特殊情況，少數人員開會，不得借用大型會議室或一個會議事先登錄借用多天。</span></li>
                    <li><span class="a-letter-Red">二、目前各會議室以不敷使用，即日起暫不提供府外機關借用。會議主持人為市長、副市長、秘書長等會議用途則不在此限。</span></li>
                    <li><span class="a-letter-Red">三、東哲廳、電腦教室仍開放本府及所屬機關借用。</span></li>
                  </ul>
                </td>
              </tr>
              <tr>
                <th style="width:13%"><span class="a-letter-Red">* </span>申請日期</th>
                <td style="width:37%"><asp:Label ID="lab_today" runat="server"></asp:Label>
                    (<asp:Label ID="lab_week" runat="server"></asp:Label>)
                    登記時段<asp:Label ID="lab_stime" runat="server"></asp:Label>
                    ，使用時數：
                        <asp:DropDownList ID="ddl_usehour" runat="server">
                            <asp:ListItem Value="0">請選擇</asp:ListItem>
                        </asp:DropDownList>
                    <asp:Label ID="lab_etime" runat="server" Visible="False"></asp:Label>
                </td>
                <th style="width:13%">所在地</th>
                <td style="width:37%">
                    <asp:Label ID="lab_sponame" runat="server"></asp:Label>
                  </td>
              </tr>
              <tr>
                <th>登記者</th>
                <td><asp:Label ID="lab_applyuser" runat="server"></asp:Label>
                    </td>
                <th>所在樓層</th>
                <td>
                    <asp:Label ID="lab_floor" runat="server"></asp:Label>
                  </td>
              </tr>
              <tr>
                <th>使用狀況</th>
                <td><asp:Label ID="lab_apply" runat="server">可申請</asp:Label>
                    </td>
                <th>場地</th>
                <td>
                    <asp:Label ID="lab_rooname" runat="server"></asp:Label>
                  </td>
              </tr>
              <tr>
                <th><span class="a-letter-Red">* </span>主持人</th>
                <td><asp:TextBox ID="txt_host" runat="server" Columns="20"></asp:TextBox></td>
                <th>場地分機</th>
                <td>
                    <asp:Label ID="lab_telephone" runat="server"></asp:Label>
                </td>
              </tr>
              <tr>
                <th>借用單位</th>
                <td><asp:Label ID="lab_depart" runat="server"></asp:Label>
                    </td>
                <th>第一保管人</th>
                <td>
                    <asp:Label ID="lab_oneuid" runat="server"></asp:Label>
                  </td>
              </tr>
              <tr>
                <th><span class="a-letter-Red">* </span>與會人數</th>
                <td><asp:TextBox ID="txt_count" runat="server" Columns="5"></asp:TextBox>&nbsp;人</td>
                <th>第一保管人電話</th>
                <td>
                    <asp:Label ID="lab_tel1" runat="server"></asp:Label>
                    <asp:Label ID="lab_ext1" runat="server"></asp:Label>
                  </td>
              </tr>
              <tr>
                <th><span class="a-letter-Red">* </span>承辦人電話</th>
                <td><asp:TextBox ID="txt_tel" runat="server" Columns="20"></asp:TextBox></td>
                <th>第二保管人</th>
                <td>
                    <asp:Label ID="lab_twouid" runat="server"></asp:Label>
                  </td>
              </tr>
              <tr>
                <th>是否公開</th>
                <td>
                    <asp:RadioButtonList ID="rbl_open" runat="server" RepeatDirection="Horizontal" 
                        RepeatLayout="Flow">
                        <asp:ListItem Value="1">公開</asp:ListItem>
                        <asp:ListItem Value="2">不公開</asp:ListItem>
                    </asp:RadioButtonList>
                  </td>
                <th>第二保管人電話</th>
                <td>
                    <asp:Label ID="lab_tel2" runat="server"></asp:Label>
                    <asp:Label ID="lab_ext2" runat="server"></asp:Label>
                  </td>
              </tr>
              <tr>
                <th><span class="a-letter-Red">* </span>開會開始時間</th>
                <td><uc3:calendar ID="cl_mdate" runat="server" _Show="False" />&nbsp;&nbsp;
                        <asp:DropDownList ID="ddl_hour" runat="server">
                            <asp:ListItem Value="0">請選擇</asp:ListItem>
                            <asp:ListItem Value="06">06</asp:ListItem>
                            <asp:ListItem Value="07">07</asp:ListItem>
                            <asp:ListItem Value="08">08</asp:ListItem>
                            <asp:ListItem Value="09">09</asp:ListItem>
                            <asp:ListItem Value="10">10</asp:ListItem>
                            <asp:ListItem Value="11">11</asp:ListItem>
                            <asp:ListItem Value="12">12</asp:ListItem>
                            <asp:ListItem Value="13">13</asp:ListItem>
                            <asp:ListItem Value="14">14</asp:ListItem>
                            <asp:ListItem Value="15">15</asp:ListItem>
                            <asp:ListItem Value="16">16</asp:ListItem>
                            <asp:ListItem Value="17">17</asp:ListItem>
                            <asp:ListItem Value="18">18</asp:ListItem>
                            <asp:ListItem Value="19">19</asp:ListItem>
                            <asp:ListItem Value="20">20</asp:ListItem>
                            <asp:ListItem Value="21">21</asp:ListItem>
                            <asp:ListItem Value="22">22</asp:ListItem>
                            <asp:ListItem Value="23">23</asp:ListItem>
                        </asp:DropDownList>
                  時
                        <asp:DropDownList ID="ddl_min" runat="server">
                            <asp:ListItem Value="0">請選擇</asp:ListItem>
                            <asp:ListItem Value="00">00</asp:ListItem>
                            <asp:ListItem Value="10">20</asp:ListItem>
                            <asp:ListItem Value="20">10</asp:ListItem>
                            <asp:ListItem Value="30">40</asp:ListItem>
                            <asp:ListItem Value="40">30</asp:ListItem>
                            <asp:ListItem Value="50">50</asp:ListItem>
                        </asp:DropDownList>
                        &nbsp;分</td>
                <th>容納人數</th>
                <td>
                    <asp:Label ID="lab_human" runat="server"></asp:Label>
                  </td>
              </tr>
              <tr>
                <th rowspan="3"><span class="a-letter-Red">* </span>申請事由</th>
                <td rowspan="3">
                  <asp:TextBox ID="txt_reason" runat="server" Columns="40" Rows="5" TextMode="MultiLine"></asp:TextBox>
                </td>
                <th>可使用時間</th>
                <td>
                    <asp:Label ID="lab_usetime" runat="server"></asp:Label>
                  </td>
              </tr>
              <tr>
                <th>場地描述</th>
                <td><asp:Label ID="lab_describe" runat="server"></asp:Label></td>
              </tr>
              <tr>
                <th>場地相關圖檔</th>
                <td>
                    <ul>
                      <li class="p1"><asp:HyperLink ID="hl_pic1" runat="server" CssClass="row_schedule">場地圖</asp:HyperLink></li>
                      <li class="p1"><asp:HyperLink ID="hl_pic2" runat="server" CssClass="row_schedule">場地平面圖</asp:HyperLink></li>
                    </ul>
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
            <asp:Button ID="btn_apply" runat="server" CssClass="b-input" Text="確定申請" 
                onclick="btn_apply_Click" />&nbsp;&nbsp;
            <asp:Button ID="btn_delete" runat="server" CssClass="b-input" Text="取消申請" 
                onclick="btn_delete_Click" />&nbsp;&nbsp;
            <asp:Button ID="btn_goback" runat="server" CssClass="a-input" Text="回上一頁" 
                onclick="btn_goback_Click" />
        </div>
        <div id="div_msg" runat="server">
        </div>
        <asp:Label ID="lab_no" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lab_spot" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lab_rooms" runat="server" Visible="False"></asp:Label>
    <asp:Label ID="lab_PetitionSignType" runat="server" Visible="False"></asp:Label>
    </div>
</asp:Content>
