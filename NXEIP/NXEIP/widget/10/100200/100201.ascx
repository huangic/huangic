<%@ Control Language="C#" AutoEventWireup="true" CodeFile="100201.ascx.cs" Inherits="widget_10_100200_100201" %>
<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetData"
        TypeName="NXEIP.DAO.MessageDAO" 
    OldValuesParameterFormatString="original_{0}">
        <SelectParameters>
            <asp:Parameter Name="peo_uid" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
<div class="block-1">
    <div class="headerin">
        <div class="in4">
            應用系統資訊</div>
        <!--  
              <div class="more"></div>
          -->
    </div>
    <table cellspacing="0" cellpadding="0" border="0" class="first">
        <tbody>
            <tr>
                <td class="dot_a14-3">
                    &nbsp;
                </td>
                <td>
                    <div class="row_1">
                        個人訊息</div>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td>
                    <asp:DataList ID="DataList1" runat="server" DataSourceID="ObjectDataSource1">
                        <ItemTemplate>
                            <li>
                                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/10/100200/100201.aspx"><%# Eval("mes_subject") %></asp:HyperLink></li>
                        </ItemTemplate>
                    </asp:DataList>
                </td>
            </tr>
        </tbody>
    </table>
</div>
