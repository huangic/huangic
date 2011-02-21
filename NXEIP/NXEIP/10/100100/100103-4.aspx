<%@ Page Language="C#" AutoEventWireup="true" CodeFile="100103-4.aspx.cs" Inherits="_10_100100_100103_4" %>

<%@ Register Assembly="MattBerseth.WebControls" Namespace="MattBerseth.WebControls"
    TagPrefix="cc1" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../../lib/CssLayout.ascx" TagName="CssLayout" TagPrefix="uc1" %>
<%@ Register Src="../../lib/Navigator.ascx" TagName="Navigator" TagPrefix="uc2" %>

<%@ Register src="../../lib/FileUpload.ascx" tagname="FileUpload" tagprefix="uc3" %>
<%@ Register src="../../lib/SWFUpload/UC_SWFUpload.ascx" tagname="UC_SWFUpload" tagprefix="uc4" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <uc1:CssLayout ID="CssLayout1" runat="server" />
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <uc2:Navigator ID="Navigator1" runat="server" SysFuncNo="100103" SubFunc="新增相片" />
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
        OldValuesParameterFormatString="original_{0}" SelectMethod="GetUploadPhoto" 
        TypeName="NXEIP.DAO._100103DAO">
        <SelectParameters>
            <asp:Parameter Name="album_no" Type="Int32" />
            <asp:Parameter Name="fileNo" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
   <div class="tableDiv">
        <asp:HiddenField ID="hidden_upload" runat="server" />
        <div class="header">
            <div class="h1">
            </div>
            <div class="h2">
            <div class="name">
                <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
            </div>
            </div>
            <div class="h3">
            </div>
        </div>
        
       <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
        
            <asp:Panel ID="panel_upload" runat="server">
            
            <uc4:UC_SWFUpload ID="UC_SWFUpload1" runat="server" />
            </asp:Panel>

            <asp:Panel ID="showUpload" runat="server" Visible="false">
                <cc1:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"  GridLines="None"
                    DataSourceID="ObjectDataSource1" DataKeyNames="alb_no,pho_no">
                
                    <Columns>
                        <asp:TemplateField HeaderText="縮圖">
                           <ItemTemplate>
                               <asp:Image ID="Image1" runat="server" ImageUrl='<%# String.Format("100103-1.ashx?album={0}&photo={1}",Eval("alb_no"),Eval("pho_no")) %>' />
                           </ItemTemplate>
                        </asp:TemplateField>


                           <asp:TemplateField HeaderText="檔案">
                           <ItemTemplate>
                               <asp:Label ID="Label1" runat="server" Text='<%#Eval("pho_name") %>'></asp:Label>
                               
                           </ItemTemplate>
                           </asp:TemplateField>

                          <asp:TemplateField HeaderText="描述">
                           <ItemTemplate>
                               <asp:TextBox ID="TextBox1" runat="server" Text='<%#Eval("pho_desc") %>'></asp:TextBox>
                           </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                
                </cc1:GridView>

            </asp:Panel>



        
       
       
                      
                     


            



        <div class="footer">
            <div class="f1">
            </div>
            <div class="f2">
            </div>
            <div class="f3">
            </div>
        </div>
        <div class="bottom">
            <asp:Button ID="btn_ok" runat="server" CssClass="b-input" Text="確定" 
                onclick="btn_ok_Click" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btn_save" runat="server" CssClass="b-input" Text="存檔"  Visible="false"
                onclick="btn_save_Click" />


           
            <input type="button" class="a-input" onclick="self.parent.tb_remove()"  value="取消" />
           
         
        </div>

          </ContentTemplate>
       </asp:UpdatePanel>
    </div>
    </form>
</body>
</html>
