<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="View.ascx.cs" Inherits="Plugghest.Modules.CourseMenu.View" %>

<asp:Label ID="lbltest" runat="server"></asp:Label>


  <asp:Menu ID="Menu_Pluggs" runat="server" BackColor="#F7F6F3" DynamicHorizontalOffset="2" Font-Names="Verdana" Font-Size="12px" ForeColor="#7C6F57" StaticSubMenuIndent="10px">
      <DynamicHoverStyle BackColor="#7C6F57" ForeColor="White" />
      <DynamicMenuItemStyle HorizontalPadding="7px" VerticalPadding="4px" />
      <DynamicMenuStyle BackColor="#F7F6F3" />
      <DynamicSelectedStyle BackColor="#5D7B9D" />
      <StaticHoverStyle BackColor="#7C6F57" ForeColor="White" />
      <StaticMenuItemStyle HorizontalPadding="16px" VerticalPadding="6px" />
      <StaticSelectedStyle BackColor="#7C6F57"  ForeColor="White"/>
   </asp:Menu>