<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true"
    CodeFile="350205.aspx.cs" Inherits="_35_350200_350205" %>

<%@ Register Src="../../lib/tree/jQueryDepartTree.ascx" TagName="jQueryDepartTree"
    TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../../lib/calendar.ascx" TagName="calendar" TagPrefix="uc2" %>
<%@ Register Src="../../lib/FileUpload.ascx" TagName="FileUpload" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table width="100%" border="0" cellpadding="0" cellspacing="0" bgcolor="#FFFFFF">
        <tr>
            <td>
                <!-- InstanceBeginEditable name="EditRegion3" -->
                <table width="100%" height="500" border="1" cellpadding="0" cellspacing="20">
                    <tr>
                        <td height="22" valign="top">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td colspan="2">
                                        <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
                                        </asp:ToolkitScriptManager>
                                        <asp:ObjectDataSource ID="ODS_profess" runat="server" SelectMethod="GetAll" TypeName="NXEIP.DAO.TypesDAO">
                                            <SelectParameters>
                                                <asp:Parameter Name="type_code" Type="String" DefaultValue="profess" />
                                            </SelectParameters>
                                        </asp:ObjectDataSource>
                                        <asp:ObjectDataSource ID="ODS_ptype" runat="server" SelectMethod="GetAll" TypeName="NXEIP.DAO.TypesDAO">
                                            <SelectParameters>
                                                <asp:Parameter DefaultValue="ptype" Name="type_code" Type="String" />
                                            </SelectParameters>
                                        </asp:ObjectDataSource>
                                        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
                                            ConnectionString="<%$ ConnectionStrings:NXEIPConnectionString %>" 
                                            SelectCommand="SELECT typ_cname, typ_number FROM types WHERE (typ_status = '1') AND (typ_code = 'work') ORDER BY typ_order">
                                        </asp:SqlDataSource>
                                    </td>
                                </tr>
                                <tr>
                                    <td width="17">
                                        <img src="../../image/b01.gif" width="17" height="22" />
                                    </td>
                                    <td background="../../image/b01-1.gif" class="b01">
                                        �b���޲z / �H���޲z /<strong> �H�Ƹ�ƫ���&nbsp;</strong>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">
                            <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td width="17">
                                        <img src="../../image/b02.gif" width="17" height="29" />
                                    </td>
                                    <td background="../../image/b02-1.gif" class="a02-15">
                                        �H�Ƹ�ƫ���
                                    </td>
                                    <td background="../../image/b02-1.gif">
                                        <div align="right">
                                        </div>
                                    </td>
                                    <td width="17">
                                        <img src="../../image/b02-2.gif" width="17" height="29" />
                                    </td>
                                </tr>
                            </table>
                            <table width="100%" border="0" cellpadding="3" cellspacing="3" bgcolor="#FFFFFF">
                                <tr>
                                    <td width="100" bgcolor="#eeeeee" class="a-letter-2">
                                        <div align="right">
                                            <span class="a-letter-Red">* </span>�����Ҧr��
                                        </div>
                                    </td>
                                    <td width="338" bgcolor="#EEEEEE" class="a-letter-1">
                                        <asp:TextBox ID="tbox_cardid" runat="server"></asp:TextBox>
                                    </td>
                                    <td width="82" bgcolor="#eeeeee" class="a-letter-2">
                                        <div align="right">
                                            <span class="a-letter-Red">* </span>�m�W</div>
                                    </td>
                                    <td colspan="2" bgcolor="#EEEEEE" class="a-letter-1">
                                        <asp:TextBox ID="tbox_name" runat="server" ></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td bgcolor="#EEEEEE" class="a-letter-2">
                                        <div align="right">
                                            <span class="a-letter-Red">* </span>���u�b��</div>
                                    </td>
                                    <td bgcolor="#EEEEEE" class="a-letter-1">
                                        <asp:TextBox ID="tbox_account" runat="server"></asp:TextBox>
                                    </td>
                                    <td bgcolor="#EEEEEE" class="a-letter-2">
                                        <div align="right">
                                            <span class="a-letter-Red">* </span>�H�ƽs��</div>
                                    </td>
                                    <td colspan="2" bgcolor="#EEEEEE" class="a-letter-1">
                                        <asp:TextBox ID="tbox_workid" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td bgcolor="#EEEEEE" class="a-letter-2">
                                        <div align="right">
                                            <span class="a-letter-Red">* </span>�A�ȳ��</div>
                                    </td>
                                    <td bgcolor="#EEEEEE" class="a-letter-1">
                                        <uc1:jQueryDepartTree ID="jQueryDepartTree1" runat="server" />
                                    </td>
                                    <td align="right" bgcolor="#EEEEEE" class="a-letter-2">
                                        �ӤH�Ӥ�
                                    </td>
                                    <td width="101" bgcolor="#EEEEEE" class="a-letter-1" colspan="2">
                                        <uc3:FileUpload ID="FileUpload1" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td bgcolor="#EEEEEE" class="a-letter-2">
                                        <div align="right">
                                            <span class="a-letter-Red">* </span>¾��</div>
                                    </td>
                                    <td bgcolor="#EEEEEE" class="a-letter-1">
                                        <asp:DropDownList ID="ddl_profess" runat="server" DataSourceID="ODS_profess" DataTextField="typ_cname"
                                            DataValueField="typ_no">
                                        </asp:DropDownList>
                                    </td>
                                    <td bgcolor="#EEEEEE" class="a-letter-2">
                                        <div align="right">
                                            <span class="a-letter-Red">* </span>�H�����O</div>
                                    </td>
                                    <td colspan="2" bgcolor="#EEEEEE" class="a-letter-1">
                                        <asp:DropDownList ID="ddl_ptype" runat="server" DataSourceID="ODS_ptype" DataTextField="typ_cname"
                                            DataValueField="typ_no">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td bgcolor="#EEEEEE" class="a-letter-2">
                                        <div align="right">
                                            <div align="right">
                                                <span class="a-letter-Red">* </span>��¾���</div>
                                        </div>
                                    </td>
                                    <td bgcolor="#EEEEEE" class="a-letter-1">
                                        <uc2:calendar ID="calendar1" runat="server" code="111" />
                                    </td>
                                    <td bgcolor="#EEEEEE" class="a-letter-2">
                                        <div align="right">
                                            �ͤ�</div>
                                    </td>
                                    <td colspan="2" bgcolor="#EEEEEE" class="a-letter-1">
                                        <uc2:calendar ID="calendar2" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td bgcolor="#EEEEEE" class="a-letter-2">
                                        <div align="right">
                                            �s���a�}</div>
                                    </td>
                                    <td bgcolor="#EEEEEE" class="a-letter-1">
                                        <asp:TextBox ID="tbox_addr" runat="server" Width="250px"></asp:TextBox>
                                        &nbsp;</td>
                                    <td bgcolor="#EEEEEE" class="a-letter-2">
                                        <div align="right">
                                            �s���q��</div>
                                    </td>
                                    <td colspan="2" bgcolor="#EEEEEE" class="a-letter-1">
                                        <asp:TextBox ID="tbox_tel" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td bgcolor="#EEEEEE" class="a-letter-2">
                                        <div align="right">
                                            �q�l�l��</div>
                                    </td>
                                    <td bgcolor="#EEEEEE" class="a-letter-1">
                                        <asp:TextBox ID="tbox_mail" runat="server" Width="250px"></asp:TextBox>
                                    </td>
                                    <td bgcolor="#EEEEEE" class="a-letter-2">
                                        <div align="right">
                                            �줽�ǹq��</div>
                                    </td>
                                    <td colspan="2" bgcolor="#EEEEEE" class="a-letter-1">
                                        <asp:TextBox ID="tbox_otel" runat="server" ></asp:TextBox>
                                        <span class="a-letter-2">�����G</span><asp:TextBox ID="tbox_extension" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td bgcolor="#EEEEEE" class="a-letter-2">
                                        <div align="right">
                                            �b¾���p</div>
                                    </td>
                                    <td bgcolor="#EEEEEE" class="a-letter-1">
                                        <div>
                                        <asp:DropDownList ID="ddl_jobtype" runat="server" AutoPostBack="True" 
                                            DataSourceID="SqlDataSource1" DataTextField="typ_cname" 
                                            DataValueField="typ_number" 
                                            onselectedindexchanged="ddl_jobtype_SelectedIndexChanged">
                                        </asp:DropDownList>
                                        &nbsp;&nbsp;
                                            <uc2:calendar ID="calendar3" runat="server" />
                                        
                                        </div>                                           
                                    </td>
                                    <td bgcolor="#EEEEEE" class="a-letter-2">
                                        <div align="right">
                                            ���u�Ƶ�</div>
                                    </td>
                                    <td colspan="2" bgcolor="#EEEEEE" class="a-letter-1">
                                        <asp:TextBox ID="tbox_memo" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                            <table width="100%" border="0" cellpadding="0" cellspacing="10" bgcolor="#FFFFFF">
                                <tr>
                                    <td>
                                        <div align="center">
                                            <asp:Button ID="Button1" runat="server" CssClass="b-input" Text="�T�w" OnClick="Button1_Click" />
                                            &nbsp;&nbsp;&nbsp;
                                            <asp:Button ID="Button2" runat="server" CssClass="a-input" Text="����" onclick="Button2_Click" />
                                        </div>
                                        <div id="calendarDiv">
                                        </div>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
                <!-- InstanceEndEditable -->
            </td>
        </tr>
    </table>
</asp:Content>
