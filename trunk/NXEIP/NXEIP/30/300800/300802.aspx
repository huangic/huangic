<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="300802.aspx.cs" Inherits="_30_300800_300802" EnableEventValidation="false" %>

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


            if (msg != undefined) {

                alert(msg);
            }
        }


       
        function pageLoad(sender, args) {
            if (args.get_isPartialLoad()) {
                //  reapply the thick box stuff
                tb_init('a.thickbox input.thickbox');
            }
        }
               

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    
    <asp:ObjectDataSource ID="ObjectDataSource3" runat="server"  OldValuesParameterFormatString="original_{0}"
         SelectMethod="GetPeopleAlbum" TypeName="NXEIP.DAO._300802DAO" 
        SelectCountMethod="GetPeopleAlbumCount">
        <SelectParameters>
            <asp:Parameter Name="peo_uid" Type="Int32" />
        </SelectParameters>
        
    </asp:ObjectDataSource>
    
    
    
    <uc1:Navigator ID="Navigator1" runat="server" SysFuncNo="300802" />
    
    
    
   
   
    
    
    
    <div class="tableDiv">
        
        

        <div class="header">
            <div class="h1">
            </div>
            <div class="h2">
              
            </div>
            <div class="h3">
            </div>
        </div>
         <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
            <ContentTemplate>
                <cc1:GridView ID="GridView1" runat="server"  AutoGenerateColumns="False"
                    CellPadding="3" CellSpacing="3" DataSourceID="ObjectDataSource3" EmptyDataText="查無資料"
                    GridLines="None"  
                     
                    DataKeyNames="alb_no" onrowcommand="GridView1_RowCommand">
                    <Columns>
                                                 
                        
                         <asp:TemplateField HeaderText="部門" ItemStyle-Width="100px">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# GetDepartmentName((Int32)Eval("alb_dep")) %>'></asp:Label>
                                  
                            </ItemTemplate>
                        </asp:TemplateField>
                         


                       <asp:TemplateField HeaderText="申請人" ItemStyle-Width="100px">
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# Eval("people.peo_name") %>'></asp:Label>
                                  <uc2:PeopleDetail ID="PeopleDetail1" runat="server" peo_uid='<%# Eval("peo_uid") %>'/>
                            </ItemTemplate>
                        </asp:TemplateField>
                       
                       
                        <asp:TemplateField HeaderText="相簿名稱">
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%# Eval("alb_name") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>


                         <asp:TemplateField HeaderText="相簿說明" ItemStyle-Width="500px">
                            <ItemTemplate>
                                <asp:Label ID="Label4" runat="server" Text='<%# Eval("alb_desc") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                     
                        

                                                
                        <asp:TemplateField HeaderText="審核" ItemStyle-Width="100px">
                            <ItemTemplate>
                                <asp:Button ID="Button1" runat="server"  CommandName="apply" Text="通過"  OnClientClick="return confirm('確定通過?')"/>
                                <asp:Button ID="Button2" runat="server"  CommandName="close" Text="不通過" OnClientClick="return confirm('確定不通過?')"/>
                         
                                 
                               
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
           
           
           
            </ContentTemplate>
           
       
           
        </asp:UpdatePanel>
        
        
        
    </div>
    

</asp:Content>
