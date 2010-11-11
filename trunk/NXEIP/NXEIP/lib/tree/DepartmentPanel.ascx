<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DepartmentPanel.ascx.cs" Inherits="lib_tree_DepartmentPanel" %>


<script type="text/javascript">
    $(document).ready(function () {
        $("#<%=this.DepartTree.ClientID %>").DepartTree({
            params:{
                TreeType:<%=(int)this.TreeType %>,
                LeafType:<%=(int)this.LeafType %>,
                SelectMode:<%=(int)this.SelectMode %>,
                PeopleStatus:<%=(int)this.PeopleStatus%>,
                 PeopleColumn:<%=(int)this.PeopleColumn%>,
                  PeopleType:<%=(int)this.PeopleType%>
            },
            
            rootUrl:"<%=ResolveClientUrl("~/lib/tree/DepartTreeMethod.ashx") %>",
            listBoxID:"<%=this.ListBox2.ClientID %>",
            peopleImg:"<%=ResolveClientUrl("~/image/v05.gif") %>",
            id:"<%=this.ClientID %>",
            parentSessionID:"<%=this.ParentSessionID%>"
        });

    });

</script>




<table border="0" cellpadding="3" cellspacing="1" bgcolor="#CCCCCC">
                <tr>
                    <td bgcolor="#eeeeee" class="a-letter-2">
                        <div align="center">
                            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                        </div>
                    </td>
                    <td bgcolor="#eeeeee" class="a-letter-2">
                        <div align="center">
                            <span class="a-letter-1">選取清單</span></div>
                            
                             
                    </td>
                </tr>
                <tr>
                    <td  valign="top" bgcolor="#FFFFFF" class="a-letter-1">
                        
                        <!-- TREE VIEW-->
                      
                      <div id="DepartTree" class="tree" runat="server" >
                     
                      </div>
                        
                        
                    </td>
                    <td  valign="top" bgcolor="#FFFFFF" class="a-letter-1">
                        <div align="center" style="top:50%;">
                            
                            <asp:ListBox ID="ListBox2" runat="server" Height="250px" Width="185px" 
                                SelectionMode="Multiple"></asp:ListBox>
                        
                           

                        </div>
                        <asp:Panel ID="ButtonPanel" runat="server" HorizontalAlign="Center">
                        <div style=" text-align:center;">
                         <input type="button" class="TreeDel b-input"  value="刪除"/>
                         </div>
                        
                        </asp:Panel>
                        

                    </td>
                </tr>
            </table>