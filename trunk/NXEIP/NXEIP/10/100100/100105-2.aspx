<%@ Page Language="C#" AutoEventWireup="true" CodeFile="100105-2.aspx.cs" Inherits="_10_100100_100105_2" %>



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
             __doPostBack('<%=UpdatePanel1.ClientID%>', '');
             tb_remove();
         }
     </script>

</head>
<body>

    <form id="form1" runat="server">
  
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
  
    <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
        SelectMethod="GetFilePermission" TypeName="NXEIP.DAO.DocPermissionDAO" 
        OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:Parameter Name="doc_no" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
  
 <div class="tableDiv" >
 <div class="header">
   <div class="h1"></div>
   <div class="h2"><div class="name">檔案權限</div></div>
   <div class="h3"></div>
 </div>
     <asp:UpdatePanel ID="UpdatePanel1" runat="server">
      <ContentTemplate>

      <cc1:GridView ID="GridView1" runat="server" DataSourceID="ObjectDataSource1" 
         AutoGenerateColumns="False" EmptyDataText="無資料" DataKeyNames="id,type,d03_no" 
              EnableViewState="False" onrowcommand="GridView1_RowCommand" 
              GridLines="None" CellSpacing="2">
      <Columns>
          <asp:BoundField DataField="value" HeaderText="群組名稱" />
          <asp:ButtonField CommandName="disable" Text="刪除" />
      </Columns>
       </cc1:GridView>
      </ContentTemplate>
     </asp:UpdatePanel>
 
  




 

 <div class="footer">
   <div class="f1"></div>
   <div class="f2"></div>
   <div class="f3"></div>
 </div>

 

 </div>

 <div style="text-align:center">
     
    <input id="CancelButton" type="button"  title="取消"  value="關閉"  onclick="self.parent.tb_remove()" class="b-input" />
    <input id="AddPermission" type="button" title="新增" value="新增" alt="100105/PermissionTree.aspx?modal=true&TB_iframe=true&width=470"  class="thickbox b-input" />
    
 </div>
    
    </form>
</body>
</html>
