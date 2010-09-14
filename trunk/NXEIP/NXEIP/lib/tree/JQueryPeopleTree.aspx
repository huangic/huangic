<%@ Page Language="C#" AutoEventWireup="true" CodeFile="JQueryPeopleTree.aspx.cs" Inherits="lib_tree_JQueryPeopleTree" %>
<%@ Register Assembly="System.Web.Entity, Version=3.5.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089"
    Namespace="System.Web.UI.WebControls" TagPrefix="asp" %>
    <%@ Register src="../../lib/CssLayout.ascx" tagname="CssLayout" tagprefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    
   <uc2:CssLayout ID="CssLayout1" runat="server" />
    
    <script type="text/javascript" src="../../js/jquery-1.4.2.min.js"></script>
    <script type="text/javascript" src="../../js/jquery.cookie.js"></script>
    <script type="text/javascript" src="../../js/jquery.treeview.min.js"></script>
    <script type="text/javascript" src="../../js/jquery.treeview.async.js"></script>
    
    <script type="text/javascript">




        $(document).ready(
        function() {
            $('#DepartTree').treeview({
                url: "TreePeopleMethod.ashx?mode=select",
                treedblclick: treedblclick
            });
            // $('#DepartTree').treeview({ url: "TreeService.asmx/getTreeNode" });
        }
        );

        function getOptions() {
            options = new Array();

            

            $("#ListBox2").children().each(function(e) {
                options.push({value: $(this).val(), text: $(this).html() });

            });
            return options;
        
        }

        function save() {


            //alert(self.parent.tb_ClientID);




            $.ajax({
                type: "POST",
                url: "TreePeopleMethod.ashx?mode=save",
                data: "{\"para\":" + JSON.stringify(getOptions()) + "}",
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                complete: function() {

                    //self.parent.updateClientID("test");

                self.parent.__doPostBack(self.parent.tb_ClientID, '')
                    self.parent.tb_remove();
                }
            });


        }

        
        
        function delOption(){
            //刪除選擇的OPTION
            
             $("#ListBox2 option:selected").each(function () {
                $(this).remove();
              });
          }



          ///這邊完全修改客製將結點的職加入LISTBOX //只有CLASS 為PEOPLE才可以被點選
          function treedblclick(e) {
            
            if(!$(this).hasClass("people")){
                return;
            }
              //alert(e);
              //alert($(owner).html);

              // alert($(this).html());
              itemReduplicate = false;
              id = $(this).parent().attr("id");

              $('#ListBox2').children().each(function() {
                  //判斷重負
                  // alert($(this).val());

                  if (id == $(this).val()) {

                      itemReduplicate = true;
                  }


              });

              if (!itemReduplicate) {
                  $('#ListBox2').append($("<option/>").attr("value", id).text($(this).html()));
              }

              //$('#ListBox2').
              //   append($("<option/>"),{value: $(this:parent).attr("id"),text: $(this).html()}); 


          }
    
    </script>
    
    
    
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <asp:Panel ID="Panel1" runat="server"  CssClass="DepartView" Width="530px">
  
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr>
                     <td class="leftheaderbg"/>
                    <td class="a02-15 headerbg"">
                       人員選取
                    </td>
                   
                    < <td class="rightheaderbg">
                       </td>
                </tr>
            </table>
            <table width="100%" border="0" cellpadding="3" cellspacing="1" bgcolor="#CCCCCC">
                <tr>
                    <td bgcolor="#eeeeee" class="a-letter-2">
                        <div align="center">
                            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                            </div>
                    </td>
                    <td bgcolor="#eeeeee" class="a-letter-2">
                        <div align="center">
                            <span class="a-letter-1">選取名單</span></div>
                            
                             
                    </td>
                </tr>
                <tr>
                    <td width="50%" valign="top" bgcolor="#FFFFFF" class="a-letter-1">
                        
                        <!-- TREE VIEW-->
                      
                      <div class="tree">
                        <ul id="DepartTree">
                        
                        </ul>
                      </div>
                        
                        
                    </td>
                    <td width="50%" valign="top" bgcolor="#FFFFFF" class="a-letter-1">
                        <div align="center" style="top:50%;">
                            
                            <asp:ListBox ID="ListBox2" runat="server" Height="280px" Width="185px" 
                                SelectionMode="Multiple"></asp:ListBox>
                        </div>
                    </td>
                </tr>
            </table>
            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                <tr>
                   
                    <td class="leftfootbg">
                       
                    </td>
                    <td class="footbg">
                        &nbsp;
                    </td>
                    <td class="rightfootbg">
                        
                    </td>
                </tr>
               
            </table>
            <table width="100%" border="0" cellpadding="0" cellspacing="10" bgcolor="#FFFFFF">
                <tr>
                    <td>
                        <div align="center">
                           <button type="button" class="b-input" onclick="save()">確定</button>
                            
                            &nbsp;&nbsp;&nbsp;&nbsp;
                           
                           <button type="button" class="a-input" onclick="delOption()">刪除</button>
                            
                            &nbsp;&nbsp;&nbsp;&nbsp;
                          <button type="button" class="a-input" onclick="self.parent.tb_remove()">關閉</button>
                           
                            
                        </div>
                    </td>
                </tr>
            </table>
           
        
</asp:Panel>

    </div>
    </form>
</body>
</html>
