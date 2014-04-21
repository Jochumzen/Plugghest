<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="View.ascx.cs" Inherits="Plugghest.Modules.CourseMenu.View" %>

   <asp:Menu ID="Menu_Pluggs" runat="server" BackColor="White" DynamicHorizontalOffset="2" Font-Names="Verdana" Font-Size="12px" ForeColor="Black" StaticSubMenuIndent="10px" ItemWrap="True" BorderWidth="1px" RenderingMode="Table" Width="360px" Font-Bold="True">
      <DynamicHoverStyle BackColor="White" ForeColor="White" />
      <DynamicMenuItemStyle HorizontalPadding="7px" VerticalPadding="4px" />
      <DynamicMenuStyle BackColor="GreenYellow" />
      <DynamicSelectedStyle BackColor="Chartreuse" />
      <StaticHoverStyle BackColor="LimeGreen" ForeColor="White" />
      <StaticMenuItemStyle HorizontalPadding="16px" VerticalPadding="6px" />
      <StaticSelectedStyle BackColor="GreenYellow"  ForeColor="White"/>    
	</asp:Menu>

<asp:Label ID="lbltest" runat="server"></asp:Label>
