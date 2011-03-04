<%@ Control Language="C#" AutoEventWireup="true" CodeFile="100404.ascx.cs" Inherits="widget_10_100400_100404" %>
<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
    OldValuesParameterFormatString="original_{0}" SelectMethod="GetNewForm" 
    TypeName="NXEIP.DAO.Form01DAO">
    <SelectParameters>
        <asp:Parameter Name="num" Type="Int32" DefaultValue="10" />
    </SelectParameters>
                 </asp:ObjectDataSource>


<div class="block-1">

          <div class="headerin">
             <div class="in3">
                 
                最新線上表單
             </div>
             
              <div >
                     <ul>
                     <li class="more" >
                      <asp:HyperLink ID="HyperLink1" CssClass="imageButton" runat="server" NavigateUrl="~/10/100400/100404.aspx"><span>more</span></asp:HyperLink>
                
                     </li>
                     </ul>   
                     
              </div>
              
          
          </div>
          <div class="first">
            
            <div class="row_n">           



            
              <asp:DataList ID="DataList1" runat="server" DataSourceID="ObjectDataSource1">
            
                <ItemTemplate>
                 <li class="dot_a51">
                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# String.Format("~/10/100400/100404.aspx") %>'><%#Eval("f01_name") %></asp:HyperLink></li>
                
                </ItemTemplate>
            
              </asp:DataList>
                   
           </div>
          </div>
          </div> 
          
