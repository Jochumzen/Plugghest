<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="View.ascx.cs" Inherits="Plugghest.Modules.ViewPluggs.View" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<div>


    <%--<asp:Panel ID="pnlGrid" runat="server"></asp:Panel>--%>


  
 <telerik:RadGrid runat="server" ID="RadGrid_ViewPlugg" AllowPaging="True" AllowSorting="true"  AutoGenerateColumns="False"
      DataKeyNames="PluggId"  OnSortCommand="RadGrid_ViewPlugg_SortCommand" OnPageIndexChanged="RadGrid_ViewPlugg_PageIndexChanged" OnPageSizeChanged="RadGrid_ViewPlugg_PageSizeChanged" >

       <MasterTableView>
      <Columns>
        <telerik:GridBoundColumn UniqueName="PluggId" HeaderText="PluggId" DataField="PluggId">
        </telerik:GridBoundColumn>

         <%--<telerik:GridBoundColumn UniqueName="PluggName" HeaderText="Plugg Name" DataField="PluggName">
        </telerik:GridBoundColumn>--%>

         <telerik:GridTemplateColumn UniqueName="PluggName"  HeaderText="Plugg Name" SortExpression="PluggName" >
         <HeaderTemplate>Plugg Name</HeaderTemplate>
         <ItemTemplate>
           <a href ='<%#"/"+(Page as DotNetNuke.Framework.PageBase).PageCulture.Name+"/"+Eval("PluggId")+".aspx" %>'> <%#Eval("PluggName")%>  </a>
          </ItemTemplate>
          </telerik:GridTemplateColumn>

    
        <telerik:GridBoundColumn UniqueName="UserName" HeaderText="Created By" DataField="UserName"></telerik:GridBoundColumn>
             
      </Columns>
    </MasterTableView>

    </telerik:RadGrid>


 


</div>
