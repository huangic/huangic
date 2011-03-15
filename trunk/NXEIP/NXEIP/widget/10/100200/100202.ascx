<%@ Control Language="C#" AutoEventWireup="true" CodeFile="100202.ascx.cs" Inherits="widget_10_100200_100202" %>


<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
    OldValuesParameterFormatString="original_{0}" SelectMethod="GetTreatData" 
    TypeName="NXEIP.DAO._100202DAO">
    <SelectParameters>
        <asp:Parameter Name="status" Type="String" DefaultValue="1" />
        <asp:Parameter Name="tra_peouid" Type="Int32" />
        <asp:Parameter Name="keyword" Type="String" />
    </SelectParameters>
</asp:ObjectDataSource>
<div class="block-1">
          <div class="headerin">
            <div class="in4">應用系統資訊</div>
              <!--  <div class="more"></div> -->
          </div>
          <table cellspacing="0" cellpadding="0" border="0" class="first">
            <tbody><tr>
              <td class="dot_a14-3">&nbsp;</td>
             
              <td><div class="row_1">待辦事項</div></td>
             
            </tr>
            <tr>
              <td>&nbsp;</td>
              <td>
              <div class="row_n">
                <ul>
                 <asp:Repeater ID="DataList1" runat="server" DataSourceID="ObjectDataSource1">
              
                <ItemTemplate>
                     <li><asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/10/100200/100202.aspx"><%# Eval("Treat.tre_name") %></asp:HyperLink></li>
                </ItemTemplate>
                </asp:Repeater>
                        </ul>
                </div>
             </td>
            </tr>
            
          </tbody></table>
        </div>