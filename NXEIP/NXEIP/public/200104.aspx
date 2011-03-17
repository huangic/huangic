<%@ Page Language="C#" AutoEventWireup="true" CodeFile="200104.aspx.cs" Inherits="public_200104" %>

<%@ Register src="../lib/CssLayout.ascx" tagname="CssLayout" tagprefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Assembly="MattBerseth.WebControls" Namespace="MattBerseth.WebControls"
    TagPrefix="cc1" %>
<%@ Register src="~/lib/people/PeopleDetail.ascx" tagname="PeopleDetail" tagprefix="uc2" %>

<%@ Register src="../lib/calendar.ascx" tagname="calendar" tagprefix="uc3" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>公文附件下載</title>
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


        $(function () {
            $("#form1").validate(
            {
                errorClass: "a02-15-Red"
            }
            );
        });

    </script>
</head>
<body>

    <div class="wrap">
    <form id="form1" runat="server">
   
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </asp:ToolkitScriptManager>
    <asp:ObjectDataSource ID="ObjectDataSource3" runat="server" OldValuesParameterFormatString="original_{0}"
        SelectMethod="GetPublicData" TypeName="NXEIP.DAO.Doc06DAO">
        <SelectParameters>
           
            <asp:Parameter DefaultValue="" Name="number" Type="String" />
            <asp:Parameter DefaultValue="" Name="peo_name" Type="String" />
            <asp:Parameter DefaultValue="" Name="publicDate" Type="DateTime" />
        </SelectParameters>
    </asp:ObjectDataSource>
    
    <div class="select">
            <span class="a-letter-2">&nbsp;文號：<span class="a-letter-1">
                    <asp:TextBox ID="tb_number" runat="server" CssClass="required"  minlength="10" MaxLength="10" ToolTip="請輸入10位數文號"></asp:TextBox>
                   
                </span>承辦人：<span class="a-letter-1">
                   <asp:TextBox ID="tb_peoname" runat="server" CssClass="required" ToolTip="請輸入承辦人"></asp:TextBox>
                     &nbsp;
                     
                     建檔日期:
                     <uc3:calendar ID="calendar1" runat="server" />
            <asp:Button ID="Button1" runat="server" Text="搜尋" CssClass="b-input" CausesValidation="False"
                        OnClick="Button1_Click" />
                </span>
                
                
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
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <cc1:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
                    CellPadding="3" CellSpacing="3" DataSourceID="ObjectDataSource3" EmptyDataText="查無資料"
                    GridLines="None" OnRowDataBound="GridView1_RowDataBound"
                     DataKeyNames="d06_no" 
                    onrowcommand="GridView1_RowCommand">
                    <Columns>
                        <asp:TemplateField HeaderText="發文單位">
                            <ItemTemplate>
                                <asp:Label ID="Label1" runat="server" Text='<%# GetDepartmentName((Int32)Eval("d06_depno")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="d06_number" HeaderText="公文文號" />
                        <asp:TemplateField HeaderText="建檔人員">
                            <ItemTemplate>
                                <asp:Label ID="Label2" runat="server" Text='<%# new NXEIP.DAO.PeopleDAO().GetPeopleNameByUid((Int32)Eval("d06_peouid")) %>'></asp:Label>

                               

                                <uc2:PeopleDetail ID="PeopleDetail1" runat="server" peo_uid='<%# Eval("d06_peouid") %>'/>

                               

                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="建檔日期">
                            <ItemTemplate>
                                <asp:Label ID="Label3" runat="server" Text='<%# new ChangeObject()._ADtoROC((DateTime)Eval("d06_date")) %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="電話(分機)">
                            <ItemTemplate>
                                <asp:Label ID="Label4" runat="server" Text='<%# string.Format("電話:{0}<br/>分機:{1}", Eval("d06_tel"), Eval("d06_ext")) %>'></asp:Label>
                            </ItemTemplate>



                        </asp:TemplateField>
                       


                        <asp:TemplateField HeaderText="附件">
                            <ItemTemplate>
                                <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataSource2"
                                    GridLines="None" ShowHeader="False">
                                    <Columns>
                                        <asp:TemplateField ShowHeader="False">
                                            <ItemTemplate>
                                                <asp:HyperLink ID="HyperLink1" runat="server" CssClass="download imageButton" Target="_blank"
                                                    NavigateUrl='<%#String.Format("200104-1.ashx?d06={0}&d07={1}",Eval("d06_no"),Eval("d07_no"))  %>'><span>下載</span></asp:HyperLink>
                                                <asp:Label ID="Label5" runat="server" Text='<%# String.Format("{0} (下載次數:{1})", Eval("d07_file"),Eval("d07_count")) %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <asp:ObjectDataSource ID="ObjectDataSource2" runat="server" OldValuesParameterFormatString="original_{0}"
                                    SelectMethod="GetAllWithDoc06No" TypeName="NXEIP.DAO.Doc07DAO">
                                    <SelectParameters>
                                        <asp:Parameter Name="doc06_no" Type="Int32" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                    </Columns>
                </cc1:GridView>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="Button1" EventName="Click" />
            </Triggers>
        </asp:UpdatePanel>
        <div class="footer">
            <div class="f1">
            </div>
            <div class="f2">
            </div>
            <div class="f3">
            </div>
        </div>
      
    </div>



    
    </form>
    </div>
</body>
</html>
