<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="200601.aspx.cs" Inherits="_20_200600_200601" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="MattBerseth.WebControls" Namespace="MattBerseth.WebControls"
    TagPrefix="cc1" %>
<%@ Register Src="~/lib/people/PeopleDetail.ascx" TagName="PeopleDetail" TagPrefix="uc2" %>
<%@ Register Src="~/lib/Navigator.ascx" TagName="Navigator" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:ObjectDataSource ID="ObjectDataSource_forum" runat="server" 
        OldValuesParameterFormatString="original_{0}" SelectMethod="GetForums" 
        TypeName="NXEIP.DAO._200601DAO">
        <SelectParameters>
            <asp:Parameter Name="peo_uid" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <uc1:Navigator ID="Navigator1" runat="server" SysFuncNo="200601" />
    <div class="tableDiv">
        <div class="talk">
            <div class="select">
                 <!--
                 <div class="b6">
                    <a href="#" class=" b-input" >文章查詢 </a>
                 </div>
                 -->
                 
                  <div class="b6">
                    <a href="#" class=" b-input" >我的收藏文章 </a>
                    </div>
                  <div class="b6">
                    <a href="#" class=" b-input" >我的追蹤文章 </a>
                      </div>
                  <div class="b6">
                    <a href="#" class=" b-input" >討論區申請 </a>
                   </div>
                    
                    
                    
                    
                   
             
              
                    
                    
                    
                    
                   
             
            </div>


                <cc1:GridView ID="GridView1" runat="server" CssClass="box" CellPadding="1" CellSpacing="1" GridLines="None"
                AutoGenerateColumns="False" DataSourceID="ObjectDataSource_forum">
                    <Columns>
                        <asp:BoundField DataField="Id" HeaderText="Id" SortExpression="Id" />
                        <asp:BoundField DataField="Name" HeaderText="Name" SortExpression="Name" />
                        <asp:BoundField DataField="EngName" HeaderText="EngName" 
                            SortExpression="EngName" />
                        <asp:BoundField DataField="Desc" HeaderText="Desc" SortExpression="Desc" />
                        <asp:BoundField DataField="ClickCount" HeaderText="ClickCount" 
                            SortExpression="ClickCount" />
                        <asp:BoundField DataField="RelayCount" HeaderText="RelayCount" 
                            SortExpression="RelayCount" />
                        <asp:BoundField DataField="Layout" HeaderText="Layout" 
                            SortExpression="Layout" />
                        <asp:BoundField DataField="Permission" HeaderText="Permission" 
                            SortExpression="Permission" />
                        <asp:BoundField DataField="Subscribe" HeaderText="Subscribe" 
                            SortExpression="Subscribe" />
                    </Columns>


                 </cc1:GridView>


            <table class="box">
                <tbody>
                    <tr>
                        <th>
                            版面
                        </th>
                        <th>
                            最新回應日期
                        </th>
                        <th>
                            人氣
                        </th>
                        <th>
                            回應
                        </th>
                        <th>
                            版型
                        </th>
                        <th>
                            修改
                        </th>
                        <th>
                            關閉
                        </th>
                        <th>
                            訂閱通知
                        </th>
                        <th>
                            取消訂閱
                        </th>
                    </tr>
                    <tr>
                        <td class="row1_bg">
                         <ul>
                            <li class="t11"></li>
                            <li class="t2"><a href="#">我的討論區</a></li>
                            <li class="t12"></li>
                            <li class="t2">我的討論區 </li>
                            <li class="t13">版主:</li>
                            <li class="t3"><a href="#">府內單位管理\</a></li>
                            </ul>
                        </td>
                        <td class="row2_bg">
                            2010-05-21 15:42:13
                        </td>
                        <td class="row3_bg">
                            629
                        </td>
                        <td class="row3_bg">
                            9
                        </td>
                        <td class="row4_bg">
                            開放型
                        </td>
                        <td class="row5_bg">
                            <input type="submit" class="modify" id="ctl00_ContentPlaceHolder1_GridView1_ctl10_Button"
                                onclick=" return confirm('確定要刪除?');" value="" name="ctl00_ContentPlaceHolder1_GridView1_ctl10_Button2">
                        </td>
                        <td class="row5_bg">
                            <input type="submit" class="exit" id="ctl00_ContentPlaceHolder1_GridView1_ctl08_Button2"
                                onclick=" return confirm('確定要刪除?');" value="" name="ctl00_ContentPlaceHolder1_GridView1_ctl08_Button">
                        </td>
                        <td class="row7_bg">
                            <input type="checkbox" id="checkbox" name="checkbox">
                        </td>
                        <td class="row7_bg">
                            <input type="checkbox" id="checkbox2" name="checkbox2">
                        </td>
                    </tr>
                    <tr>
                        <td class="row1_bg">
                            <li class="t11"></li>
                            <li class="t2"><a href="#">員工入口網操作討論及應用建議</a></li>
                            <li class="t12"></li>
                            <li class="t2">開放給所有同仁對員工入口網之操作問題及各類應用之討論區 類應用之討論區 類應用之討論區</li>
                            <li class="t13">版主</li>
                            <li class="t3"><a href="#">李雄紹</a></li>
                        </td>
                        <td class="row2_bg">
                            2010-12-08 15:46:07
                        </td>
                        <td class="row3_bg">
                            1250
                        </td>
                        <td class="row3_bg">
                            57
                        </td>
                        <td class="row4_bg">
                            開放型
                        </td>
                        <td class="row5_bg">
                            <input type="submit" class="modify" id="ctl00_ContentPlaceHolder1_GridView1_ctl10_Button2"
                                onclick=" return confirm('確定要刪除?');" value="" name="ctl00_ContentPlaceHolder1_GridView1_ctl10_Button">
                        </td>
                        <td class="row5_bg">
                            <input type="submit" class="exit" id="ctl00_ContentPlaceHolder1_GridView1_ctl08_Button"
                                onclick=" return confirm('確定要刪除?');" value="" name="ctl00_ContentPlaceHolder1_GridView1_ctl08_Button2">
                        </td>
                        <td class="row7_bg">
                            <input type="checkbox" id="checkbox3" name="checkbox3">
                        </td>
                        <td class="row7_bg">
                            <input type="checkbox" id="checkbox4" name="checkbox4">
                        </td>
                    </tr>
                    <tr>
                        <td class="row1_bg">
                            <li class="t11"></li>
                            <li class="t2"><a href="#">攝影綜合討論區</a></li>
                            <li class="t12"></li>
                            <li class="t2">討論攝影技巧，心得及傳統/數位相機、周邊設備等訊交流</li>
                            <li class="t13">版主:</li>
                            <li class="t3"><a href="#">李雄紹 </a></li>
                        </td>
                        <td class="row2_bg">
                            2010-12-08 15:46:07
                        </td>
                        <td class="row3_bg">
                            458
                        </td>
                        <td class="row3_bg">
                            1
                        </td>
                        <td class="row4_bg">
                            封閉型
                        </td>
                        <td class="row5_bg">
                            <input type="submit" class="modify" id="ctl00_ContentPlaceHolder1_GridView1_ctl10_Button3"
                                onclick=" return confirm('確定要刪除?');" value="" name="ctl00_ContentPlaceHolder1_GridView1_ctl10_Button3">
                        </td>
                        <td class="row5_bg">
                            <input type="submit" class="exit" id="ctl00_ContentPlaceHolder1_GridView1_ctl08_Button3"
                                onclick=" return confirm('確定要刪除?');" value="" name="ctl00_ContentPlaceHolder1_GridView1_ctl08_Button3">
                        </td>
                        <td class="row7_bg">
                            <input type="checkbox" id="checkbox5" name="checkbox5">
                        </td>
                        <td class="row7_bg">
                            <input type="checkbox" id="checkbox6" name="checkbox6">
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>
