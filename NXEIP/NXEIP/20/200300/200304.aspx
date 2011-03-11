<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="200304.aspx.cs" Inherits="_20_200300_200304" EnableEventValidation="false" %>

<%@ Register Src="../../lib/Navigator.ascx" TagName="Navigator" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="MattBerseth.WebControls" Namespace="MattBerseth.WebControls"
    TagPrefix="cc1" %>
<%@ Register src="../../lib/people/PeopleDetail.ascx" tagname="PeopleDetail" tagprefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript">
        function update(msg) {

            __doPostBack('<%=UpdatePanel1.ClientID%>', '');
            tb_remove();


            //alert(msg);
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
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    
    <asp:ObjectDataSource ID="ObjectDataSource3" runat="server" EnablePaging="True" OldValuesParameterFormatString="original_{0}"
        SelectCountMethod="GetSearchDataCount" SelectMethod="GetSearchData" 
        TypeName="NXEIP.DAO.CooperDAO">
        <SelectParameters>
            <asp:Parameter DefaultValue="" Name="cat_no" Type="Int32" />
            <asp:Parameter Name="name" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <asp:ObjectDataSource ID="ObjectDataSource_CAT" runat="server" 
        OldValuesParameterFormatString="original_{0}" SelectMethod="GetS06FromSufNO" 
        TypeName="NXEIP.DAO.Sys06DAO">
        <SelectParameters>
            <asp:Parameter DefaultValue="200304" Name="suf_no" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
    
    
    
    <uc1:Navigator ID="Navigator1" runat="server" SysFuncNo="200304" />
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

        <asp:HiddenField ID="hidden_cat" runat="server" />
    
         
           <div class="select-1">

               <asp:ListView ID="lv_cat" runat="server" DataSourceID="ObjectDataSource_CAT"  
                   DataKeyNames="s06_no" onitemcommand="lv_cat_ItemCommand"
                   >
                        <ItemTemplate>
                            <span><asp:LinkButton ID="lb_cat" runat="server"  CssClass='<%# hidden_cat.Value.Equals( Eval("s06_no").ToString()) ? "a-letter-s1":""  %>'  CommandName="click_cat"><%# Eval("s06_name") %></asp:LinkButton></span>
                                      
                        
                        </ItemTemplate>
  
                       
               </asp:ListView>
           </div>

           
        
         <div  class="select" >
            <span class="a-letter-2">商品：<span class="a-letter-1">
                    <asp:TextBox ID="tb_file" runat="server"></asp:TextBox>
                     &nbsp;
             <asp:Button ID="Button1" runat="server" Text="搜尋" CssClass="b-input" CausesValidation="False"
                        OnClick="Button1_Click" />
                </span>
                
                
                </span>
        </div>
        
       
 
    
    
   
   
    
    
    
    <div class="tableDiv" style="width:600px">
        
        <div class="header">
            <div class="h1">
            </div>
            <div class="h2">
                <div class="function">
                 
                    <input type="button" class="thickbox b-input" alt="200304-1.aspx?modal=true&TB_iframe=true&height=378&width=800"
                        value="新增商品" />
                </div>
            </div>
            <div class="h3">
            </div>
        </div>
     
                <cc1:GridView ID="GridView1" runat="server" AllowPaging="True" AutoGenerateColumns="False"
                    CellPadding="3" CellSpacing="3" DataSourceID="ObjectDataSource3" EmptyDataText="查無資料"
                    GridLines="None"  
                    onrowcommand="GridView1_RowCommand" 
                    DataKeyNames="coo_no"  >
                    <Columns>
                        <asp:TemplateField HeaderText="商品類別">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# GetCatName((Int32)Eval("coo_s06no")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        


                        <asp:BoundField DataField="coo_name" HeaderText="商品名稱" />
                       
                     
                        <asp:BoundField DataField="coo_price" HeaderText="價格" />
                       


                        
                        <asp:TemplateField HeaderText="修改">
                            <ItemTemplate>
                                <asp:HyperLink ID="HyperLink2" runat="server" CssClass="thickbox imageButton edit" NavigateUrl='<%# string.Format("200304-1.aspx?mode=edit&id={0}&modal=true&TB_iframe=true&height=378&width=600",Eval("coo_no"))%>' Enabled='<%# GetModifyVisible((int)Eval("coo_createuid"))%>'><span>修改</span></asp:HyperLink>
                                <asp:LinkButton ID="LinkButton1" runat="server" CssClass=" imageButton delete" 
                                    Enabled='<%# GetModifyVisible((int)Eval("coo_createuid"))%>' 
                                    OnClientClick="return confirm('確定要刪除?')" CommandName="del"><span>刪除</span></asp:LinkButton>
                            
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </cc1:GridView>
            
        <div class="footer">
            <div class="f1">
            </div>
            <div class="f2">
            </div>
            <div class="f3">
            </div>
        </div>

        
        
        <div class="pager">
            <asp:DataPager ID="DataPager1" runat="server" PagedControlID="GridView1" PageSize="25">
                <Fields>
                    <NXEIP:GooglePagerField />
                </Fields>
            </asp:DataPager>
        </div>
       
    </div>
     </ContentTemplate>
        </asp:UpdatePanel>

</asp:Content>
