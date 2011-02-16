<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="200601.aspx.cs" Inherits="_20_200600_200601"   enableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="MattBerseth.WebControls" Namespace="MattBerseth.WebControls"
    TagPrefix="cc1" %>

<%@ Register Src="~/lib/Navigator.ascx" TagName="Navigator" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

<script type="text/javascript">
    function update(msg) {

        __doPostBack('<%=UpdatePanel1.ClientID%>', '');
        tb_remove();


        if (msg != undefined) {

            alert(msg);
        }
    }

    function pageLoad(sender, args) {
        if (args.get_isPartialLoad()) {
            //  reapply the thick box stuff
            tb_init('a.thickbox,input.thickbox');
        }
    }
    </script>


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
                    <a href="200601-3.aspx" class="b-input" >我的收藏文章 </a>
                    </div>
                  <div class="b6">
                    <a href="200601-8.aspx" class="b-input" >我的追蹤文章 </a>
                      </div>
                  <div class="b6">
                    <a href="200601-1.aspx?TB_iframe=true&height=450&width=650&modal=true" class="thickbox b-input" >討論區申請 </a>
                   </div>
                    
                    
                    
                    
                   
             
              
                    
                    
                    
                    
                   
             
            </div>



            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                
                <cc1:GridView ID="GridView1" runat="server" CssClass="box" CellPadding="1" 
                        CellSpacing="1" GridLines="None"
                AutoGenerateColumns="False" DataSourceID="ObjectDataSource_forum" 
                        DataKeyNames="Id" EmptyDataText="沒有任何討論區" onrowcommand="GridView1_RowCommand">
                    <Columns>
                        
                        
                        
                        
                        
                        <asp:TemplateField HeaderText="版面">
                            <ItemStyle CssClass="row1_bg" />
                           <ItemTemplate>
                            <ul>
                                           
           		   
             		<li class="t1">
                    
                           <asp:HyperLink ID="HyperLink1" runat="server" Text='<%# Eval("Name") %>' NavigateUrl='<%#String.Format("200601-2.aspx?tao_no={0}",Eval("Id")) %>'>
                            
                           </asp:HyperLink>
                    
                    </li>
             		
             		
                    <li class="t2">
                           <asp:Label ID="Label1" runat="server" Text='<%#Eval("Desc") %>'></asp:Label></li>
                    
                   
                    
                    <li class="t3">
                    
                     <asp:Label ID="Label2" runat="server" Text='<%#Eval("ManagerName") %>'></asp:Label></li>
                    
                    
                    
                    
                    
                           
                    
                    </li>          


                            </ul>
                            </ItemTemplate>

                        </asp:TemplateField>
                        
                        
                         <asp:TemplateField HeaderText="最新回應日期">
                            <ItemStyle CssClass="row2_bg" />
                           <ItemTemplate>

                           <asp:Label ID="Label3" runat="server" Text='<%#  GetROCDT((DateTime?)Eval("LastModifyDate")) %>'></asp:Label></li>
                    
                           </ItemTemplate>
                          </asp:TemplateField>
                                                
                       
                        <asp:BoundField DataField="ClickCount" HeaderText="人氣"  ItemStyle-CssClass="row3_bg"
                            SortExpression="ClickCount" />
                        <asp:BoundField DataField="RelayCount" HeaderText="回應" ItemStyle-CssClass="row4_bg"
                            SortExpression="RelayCount" />

                        <asp:TemplateField HeaderText="版型">
                            <ItemStyle CssClass="row5_bg" />
                           <ItemTemplate>

                           <asp:Label ID="Label4" runat="server" Text='<%#  GetLayoutName((String)Eval("Layout")) %>'></asp:Label></li>
                    
                           </ItemTemplate>
                          </asp:TemplateField>
                        

                         <asp:TemplateField HeaderText="訂閱">
                           <ItemStyle CssClass="row5_bg" />
                           <ItemTemplate>
                               <asp:Button ID="Button1" runat="server"  CssClass="thickbox order"  alt='<%# String.Format("200601-12.aspx?tao_no={0}&TB_iframe=true&height=150&width=450&modal=true",Eval("Id")) %>'  Visible='<%# (!(bool)Eval("Subscribe")) %>' />
                               <asp:Button ID="Button2" runat="server"  OnClientClick="return confirm('確定取消訂閱?')" CssClass="unorder"  Visible='<%# ((bool)Eval("Subscribe")) %>' CommandName="SubscribeCanel" />
                           </ItemTemplate>
                          </asp:TemplateField>


                           <asp:TemplateField HeaderText="修改" Visible="false">
                           <ItemStyle CssClass="row5_bg" />
                           <ItemTemplate>
                           
                           
                           <asp:HyperLink ID="HyperLink2" runat="server" CssClass="imageButton modify" Visible='<%# Eval("IsManager") %>'>
                            <span>修改</span>
                           </asp:HyperLink>


                              
                           
                           </ItemTemplate>
                          </asp:TemplateField>


                           <asp:TemplateField HeaderText="關閉">
                           <ItemStyle CssClass="row5_bg" />
                           <ItemTemplate>
                           
                           <asp:Button ID="Button3" runat="server" CssClass="exit" Visible='<%# (bool)Eval("IsRoot")%>' CommandName="close"  OnClientClick="return confirm('確定要關閉?')"/>
                         
                            </ItemTemplate>
                          </asp:TemplateField>
                       
                    </Columns>


                 </cc1:GridView>

                
                </ContentTemplate>


            </asp:UpdatePanel>



            
        </div>
    </div>
</asp:Content>
