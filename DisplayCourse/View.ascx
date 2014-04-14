<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="View.ascx.cs" Inherits="Plugghest.Modules.DisplayCourse.View" %>


<link href="module.css" rel="stylesheet" />



<div style="width:642px">

    <div class="dispalyplug title1"><asp:Label runat="server" ID="lblTitle"></asp:Label></div>
    
    <br />
    <asp:HyperLink ID="LnkBeginCourse" runat="server" Text="Begin course" CssClass="Button_default" ></asp:HyperLink>
    <br /><br />

    <span  class="dispalyplug">Description</span>

    <br /><br />

   <div class="dispalyplug"><asp:Label runat="server" ID="lblDescription" ></asp:Label></div> 

</div>
