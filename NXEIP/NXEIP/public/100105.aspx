<%@ Page Language="C#" AutoEventWireup="true" CodeFile="100105.aspx.cs" Inherits="public_100105" %>

<%@ Register src="../lib/CssLayout.ascx" tagname="CssLayout" tagprefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="MattBerseth.WebControls" Namespace="MattBerseth.WebControls"
    TagPrefix="cc1" %>
<%@ Register src="~/lib/people/PeopleDetail.ascx" tagname="PeopleDetail" tagprefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>分享文件下載</title>
    <script type="text/javascript" src='../js/jquery-1.4.2.min.js'></script>
    <script type="text/javascript" src='../js/jquery.log.js'></script>
    <script type="text/javascript" src='../js/thickbox.js'></script>
    <script type="text/javascript" src='../js/jquery.open.js'></script>
    <script type="text/javascript" src='../js/jquery.validate.min.js'></script>
    <uc1:CssLayout ID="CssLayout1" runat="server" />
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
</head>
<body>

    <div class="wrap">
    <form id="form1" runat="server">
   
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:ObjectDataSource ID="ObjectDataSource3" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetPublicFile" TypeName="NXEIP.DAO._100105DAO">
        <SelectParameters>
            <asp:Parameter Name="network" Type="String" />
            <asp:Parameter Name="pwd" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
     <ContentTemplate>


    <div class="select">
            <span class="a-letter-2">&nbsp;密碼：<span class="a-letter-1">
                    <asp:TextBox ID="tb_number" runat="server" CssClass="required"  minlength="10" MaxLength="10" ToolTip="請輸入密碼" TextMode="Password"></asp:TextBox>
                   
            </span>
                
                <asp:Button ID="Button1" runat="server" Text="確認" CssClass="b-input" CausesValidation="False"
                        OnClick="Button1_Click" />
             
                
                
                </span>
        </div>
    
    
    
    
    <div class="tableDiv">
        
        <div class="header">
            <div class="h1">
            </div>
            <div class="h2">
                
            </div>
            <div class="h3">
            </div>
        </div>
        
                <cc1:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                    CellPadding="3" CellSpacing="3" DataSourceID="ObjectDataSource3" EmptyDataText="查無資料"
                    GridLines="None">
                    <Columns>
                        <asp:TemplateField HeaderText="單位">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# GetDepartmentName((Int32)Eval("dep_no")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                      
                        <asp:TemplateField HeaderText="建檔人員">
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# new NXEIP.DAO.PeopleDAO().GetPeopleNameByUid((Int32)Eval("peo_uid")) %>'></asp:Label>

                               

                                <uc2:PeopleDetail ID="PeopleDetail1" runat="server" peo_uid='<%# Eval("peo_uid") %>'/>

                               

                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="建檔日期">
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%# new ChangeObject()._ADtoROC((DateTime)Eval("d01_createtime")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                  


                        <asp:TemplateField HeaderText="附件">
                            <ItemTemplate>
                                
                                <asp:HyperLink ID="HyperLink1" runat="server" CssClass="download imageButton" Target="_blank"
                                                    NavigateUrl='<%#String.Format("~/public/100105-1.ashx?code={0}",Eval("d01_url"))  %>'><span>下載</span></asp:HyperLink>
                                
                                <asp:Label ID="Label4" runat="server" Text='<%# Eval("d01_file") %>'></asp:Label>
                                
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
        </div>
            </ContentTemplate>
          
        </asp:UpdatePanel>
       
      
   



    
    </form>
    </div>
</body>
</html>
