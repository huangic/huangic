<%@ Control Language="C#" AutoEventWireup="true" CodeFile="200601.ascx.cs" Inherits="widget_20_200600_200601" %>
<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
    OldValuesParameterFormatString="original_{0}" SelectMethod="GetNewTopic" 
    TypeName="NXEIP.DAO._200601WidgetDAO">
    <SelectParameters>
        <asp:Parameter Name="num" Type="Int32" DefaultValue="10" />
    </SelectParameters>
                 </asp:ObjectDataSource>


<div class="block-1">

          <div class="headerin">
             <div class="in3">
                 
                討論區最新主題
             </div>
             
              <div >
                     <ul>
                     <li class="more" >
                      <asp:HyperLink ID="HyperLink1" CssClass="imageButton" runat="server" NavigateUrl="~/20/200600/200601.aspx"><span>more</span></asp:HyperLink>
                
                     </li>
                     </ul>   
                     
              </div>
              
          
          </div>
          <div class="first">
            
            <div class="row_n">           



            
              <asp:DataList ID="DataList1" runat="server" DataSourceID="ObjectDataSource1">
            
                <ItemTemplate>
                 <li class="dot_a51">
                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%# String.Format("~/20/200600/200601-2.aspx?tao_no={0}",Eval("tao_no")) %>'><%#Eval("t01_subject") %></asp:HyperLink></li>
                
                </ItemTemplate>
            
              </asp:DataList>
                   
           </div>
          </div>
          </div> 
          
