<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="View.ascx.cs" Inherits="Plugghest.Modules.ViewCourses.View" %>

<%@ Register TagPrefix="telerik" Namespace="Telerik.Web.UI" Assembly="Telerik.Web.UI" %>

<div>


    <%--<asp:Panel ID="pnlGrid" runat="server"></asp:Panel>--%>


  
 <telerik:RadGrid runat="server" ID="RadGrid_ViewPlugg" AllowPaging="True" AllowSorting="true"  AutoGenerateColumns="False"
      DataKeyNames="CourseId"  OnSortCommand="RadGrid_ViewPlugg_SortCommand" OnPageIndexChanged="RadGrid_ViewPlugg_PageIndexChanged" OnPageSizeChanged="RadGrid_ViewPlugg_PageSizeChanged" >

       <MasterTableView>
      <Columns>
        <telerik:GridBoundColumn UniqueName="CourseId" HeaderText="CourseId" DataField="CourseId">
        </telerik:GridBoundColumn>

         <%--<telerik:GridBoundColumn UniqueName="PluggName" HeaderText="Plugg Name" DataField="PluggName">
        </telerik:GridBoundColumn>--%>

          <telerik:GridTemplateColumn UniqueName="CourseName"  HeaderText="CourseName" SortExpression="CourseName" >
         <HeaderTemplate>Course Name</HeaderTemplate>
         <ItemTemplate>
           <a href ='<%#"/"+(Page as DotNetNuke.Framework.PageBase).PageCulture.Name+"/C"+Eval("CourseId")+".aspx" %>'> <%#Eval("CourseName")%>  </a>
          </ItemTemplate>
          </telerik:GridTemplateColumn>

    
        <telerik:GridBoundColumn UniqueName="UserName" HeaderText="Created By" DataField="UserName"></telerik:GridBoundColumn>
             
      </Columns>
    </MasterTableView>

    </telerik:RadGrid>


 


</div>
