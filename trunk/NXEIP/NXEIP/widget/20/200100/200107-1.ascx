<%@ Control Language="C#" AutoEventWireup="true" CodeFile="200107-1.ascx.cs" Inherits="widget_20_200100_200107_1" %>
<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" 
    OldValuesParameterFormatString="original_{0}" SelectMethod="GetUnitData" 
    TypeName="NXEIP.DAO.Doc09DAO">
    <SelectParameters>
       
        <asp:Parameter Name="takeNum" Type="Int32" DefaultValue="5" />
    </SelectParameters>
                 </asp:ObjectDataSource>


<div class="block-1">

          <div class="headerin">
             <div class="in3">
                 
                全府檔案區
             </div>
             
              <div >
                     <ul>
                     <li class="more" >
                      <asp:HyperLink ID="HyperLink1" CssClass="imageButton" runat="server" NavigateUrl="~/20/200100/200107.aspx"><span>more</span></asp:HyperLink>
                
                     </li>
                     </ul>   
                     
              </div>
              
          
          </div>
          <div class="first">
            <div class="row_1">
                    



            <ul>
              <asp:Repeater ID="DataList1" runat="server" DataSourceID="ObjectDataSource1">
                
                <ItemTemplate>
                 <li>
                     <%# GetS06Name((Int32)Eval("s06_no")) %>-                    
                    <%#Eval("d09_note") %>
                    <span style="float:right; color:Red"><%#new ChangeObject()._ADtoROC((DateTime)Eval("d09_date")) %></span>
                    
                    </li>
                
                </ItemTemplate>
            
              </asp:Repeater>
                 </ul>  
          </div>
          </div>
          </div> 
          
