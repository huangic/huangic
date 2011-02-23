<%@ Page Language="C#" AutoEventWireup="true" CodeFile="200601-4.aspx.cs" Inherits="_20_200600_200601_3"
    MasterPageFile="~/MasterPage.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="MattBerseth.WebControls" Namespace="MattBerseth.WebControls"
    TagPrefix="cc1" %>
<%@ Register Src="~/lib/Navigator.ascx" TagName="Navigator" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function update(msg) {

            __doPostBack('<%=UpdatePanel1.ClientID%>', '');
            tb_remove();


            if (msg != undefined && msg!="") {

                alert(msg);
            }
        }

        function pageLoad(sender, args) {
            if (args.get_isPartialLoad()) {
                //  reapply the thick box stuff
                tb_init('a.thickbox');
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
  
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" EnablePaging="True" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetTopicList" TypeName="NXEIP.DAO._200601_4DAO" 
        SelectCountMethod="GetTopicListCount">
        <SelectParameters>
            <asp:Parameter Name="tao_no" Type="Int32" />
            <asp:Parameter Name="t01_no"  Type="Int32" />
            <asp:Parameter Name="peo_uid" Type="Int32" />
             
        </SelectParameters>
    </asp:ObjectDataSource>
    <uc1:Navigator ID="Navigator1" runat="server" SysFuncNo="200601" SubFunc="內容列表" />
    <div class="tableDiv">
        <div class="talk">
            <div class="select">
                <div class="b6">
                    <a href="200601.aspx" class="b-input">回討論區總表</a>
                </div>
                <!--
                <div class="b6">
                    <a href="#" class="b-input">列印</a></div>
                -->

                <div class="b6">
                    <asp:HyperLink  CssClass="b-input" ID="hl_list" runat="server">觀看主題列表</asp:HyperLink>
                </div>
              </div>
        
        </div>
        
        
        <div class="talkn">
            <div class="right">
            </div>
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    
                    
                    <asp:ListView ID="ListView1" runat="server" DataSourceID="ObjectDataSource1" 
                        DataKeyNames="ForumId,Id,ParentId" onitemcommand="ListView1_ItemCommand" >
                    <ItemTemplate>
               <div class="box">
                <div class="title2"><span class="a-title">
                
                   <asp:Label ID="Label1" runat="server" Text='<%#Eval("Name") %>'></asp:Label>
                </span>
               </div>
               </div>

              <div class='<%# ((int)Eval("ParentId"))==0?"box2":"box3" %>' runat="server">
              
              <div class='<%# ((int)Eval("ParentId"))==0?"title4":"title5" %>'>
                    <ul>
                    <li class="talk_03">發表人</li>
                    <li class="talk_03"><span class="a-name">
                     <asp:Label ID="Label2" runat="server" Text='<%#Eval("Author") %>'/>
                    </span> </li>
                    <li class="talk_03"> 日期</li>
                    <li><span class="a-name">
                     <asp:Label ID="Label3" runat="server" Text='<%#GetROCDT((DateTime)Eval("PublishDate")) %>'/>
                    </span></li>
                    </ul>
             </div>
             <div class='<%#((String)Eval("Sex"))=="1"?"boy":"girl"%>'></div>
             <div class="content">
              <div class="b2">
                <asp:Label ID="Label4" runat="server" Text='<%#Eval("Content") %>'/>
              </div>
              
              <asp:Panel   runat="server" Visible='<%# Eval("HasFile")%>'>
                    附檔:<asp:HyperLink ID="HyperLink2" runat="server" Text='<%# Eval("FileName") %>' NavigateUrl='<%# String.Format("200601-13.ashx?tao_no={0}&t01_no={1}",Eval("ForumId"),Eval("Id")) %>'></asp:HyperLink>
              </asp:Panel>


              <div class="b4" runat="server" Visible='<%# Eval("HasPermission") %>' >
                <asp:HyperLink ID="HyperLink1" CssClass="thickbox" runat="server" NavigateUrl='<%# String.Format("200601-5.aspx?mode=edit&tao_no={0}&t01_no={1}&TB_iframe=true&height=450&width=650&modal=true",Eval("ForumId"),Eval("Id")) %>'>修改</asp:HyperLink>
                
                
              </div> 
              
              <div class="b4" runat="server" Visible='<%# Eval("HasPermission") %>' >
                  <asp:LinkButton ID="LinkButton1" OnClientClick="return confirm('確定要刪除?')" runat="server" CommandName="del">刪除</asp:LinkButton>
                 
                 
               
              </div>
              <div class="b4" runat="server" Visible='<%# Eval("IsManager") %>' >
                <asp:LinkButton ID="LinkButton3" OnClientClick="return confirm('確定要加入?')" runat="server" CommandName="AddFeatured">加入精華</asp:LinkButton>
                
              
              </div>
               <div id="Div1" class="b4" runat="server" visible='<%# IsCanWrite() %>'>
                <asp:LinkButton ID="LinkButton4" OnClientClick="return confirm('確定要加入?')" runat="server" CommandName="AddTrack">加入追蹤</asp:LinkButton>
                 
               
              </div>

              <div class="b4" runat="server" visible='<%# IsCanWrite() %>'>
                <asp:LinkButton ID="LinkButton2" OnClientClick="return confirm('確定要加入?')" runat="server" CommandName="AddFolder">加入收藏</asp:LinkButton>
                 
               
              </div>
              <div class="b4" runat="server"  visible='<%# IsCanWrite() %>'>
                <asp:HyperLink ID="HyperLink5" CssClass="thickbox" runat="server" NavigateUrl='<%# String.Format("200601-5.aspx?tao_no={0}&t01_no={1}&TB_iframe=true&height=450&width=650&modal=true",Eval("ForumId"),Eval("Id")) %>'>回應</asp:HyperLink>
                
              </div>
          </div>
             </div>

              
        
                    
                    </ItemTemplate>

                    </asp:ListView>
                    
                    
                    
                    <div class="pager">
                        <asp:DataPager ID="DataPager1" runat="server" PagedControlID="ListView1" 
                            PageSize="25">
                            <Fields>
                                <NXEIP:GooglePagerField />
                            </Fields>
                        </asp:DataPager>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
            
        </div>
    </div>
 
</asp:Content>
