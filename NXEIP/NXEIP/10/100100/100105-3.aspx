<%@ Page Language="C#" AutoEventWireup="true" CodeFile="100105-3.aspx.cs" Inherits="_10_100100_100105_3" %>



<%@ Register src="../../lib/CssLayout.ascx" tagname="CssLayout" tagprefix="uc1" %>



<%@ Register assembly="MattBerseth.WebControls" namespace="MattBerseth.WebControls" tagprefix="cc1" %>






<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <uc1:CssLayout ID="CssLayout1" runat="server" />
     <script type="text/javascript" src="../../js/jquery-1.4.2.min.js"></script>
     <script type="text/javascript" src="../../js/thickbox.js"></script>
     <script type="text/javascript" src="../../js/jquery-ui-1.8.2.custom.min.js"></script>
     <script type="text/javascript" src="../../js/jquery.cookie.js"></script>
     <script type="text/javascript" src="../../js/jquery.hotkeys.js"></script>
     <script type="text/javascript" src="../../js/jquery.jstree.js"></script>
     <script type="text/javascript" src="../../js/jquery.FileExplorer.js"></script>

     <script type="text/javascript">
        
         function update() {
         
             tb_remove();
         }
     </script>

</head>
<body>

    <form id="form1" runat="server">
  
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
  
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
        SelectMethod="GetDocVersion" TypeName="NXEIP.DAO.DocPermissionDAO" 
        OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:QueryStringParameter Name="doc01_no" QueryStringField="id" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
  
    
   <div class="select">
       &nbsp;&nbsp;
       <span class="a-letter-2">說明</span><asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
       &nbsp;
       <span class="a-letter-2">檔案</span><asp:FileUpload ID="FileUpload1" runat="server"  />
       <asp:Button ID="Button1" runat="server" Text="存檔" CssClass="b-input" 
           onclick="Button1_Click1" />
   </div>

 <div class="tableDiv" >
 <div class="header">
   <div class="h1"></div>
   <div class="h2"><div class="name">檔案版本</div></div>
   <div class="h3"></div>
 </div>
     
     
     
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
     <ContentTemplate>
     

      <cc1:GridView ID="GridView1" runat="server" DataSourceID="ObjectDataSource1" 
         AutoGenerateColumns="False" EmptyDataText="無資料"  DataKeyNames="d01_no,d02_no"
             onrowcommand="GridView1_RowCommand" 
              GridLines="None" CellSpacing="2">
      <Columns>
          <asp:BoundField DataField="d02_version" HeaderText="版本" HeaderStyle-Width="40px" 
              SortExpression="d02_version" />
          <asp:BoundField DataField="d02_description" HeaderText="描述說明" />

          <asp:TemplateField HeaderText="公開" HeaderStyle-Width="40px">
            <ItemTemplate>
                <asp:Label ID="Label1" runat="server" Text='<%# ((string)Eval("d02_open")=="2"?"是":"") %>' ></asp:Label>
            </ItemTemplate>
          
          </asp:TemplateField >

          <asp:ButtonField CommandName="public" Text="公開" HeaderStyle-Width="40px" />
           
           <asp:TemplateField HeaderStyle-Width="40px">
           
           <ItemTemplate  >
               <asp:HyperLink ID="HyperLink1" runat="server" CssClass="download imageButton" Target="_blank"
                NavigateUrl='<%#String.Format("100105-4.ashx?d01={0}&d02={1}",Eval("d01_no"),Eval("d02_no"))  %>'><span>下載</span></asp:HyperLink>
             
               </ItemTemplate>
               </asp:TemplateField>
      </Columns>
      </cc1:GridView>
     
   </ContentTemplate></asp:UpdatePanel>


 <div class="footer">
   <div class="f1"></div>
   <div class="f2"></div>
   <div class="f3"></div>
 </div>

 

 </div>

 <div style="text-align:center">
     
    <input id="CancelButton" type="button"  title="取消"  value="關閉"  onclick="self.parent.tb_remove()" class="b-input" />
       
 </div>
    
    </form>
</body>
</html>
