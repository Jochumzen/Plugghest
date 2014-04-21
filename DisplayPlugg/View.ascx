<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="View.ascx.cs" Inherits="Plugghest.Modules.DisplayPlugg.View" %>

<style>
    .dispalyplug {
    margin-bottom: 8px;
}

 .Icon_1 {
    background: url("http://plugghest.com/portals/_default/containers/20047-unlimitedcolorpack-033/images/quotationMarks.png") no-repeat scroll center center #FFFFFF;
    display: block;
    height: 36px;
    margin: -18px auto 0;
    width: 82px;
}

 
.title1 {
    color: #666666;
    font-size: 16px;
    font-weight: bold;
    margin: 0;
    padding: 20px 0 0;
    vertical-align: middle;
    white-space: normal;
    /*text-align:center !important*/
}

</style>

<div style="width:642px">

   <div class="dispalyplug"><asp:Label runat="server" ID="lblYoutube"></asp:Label></div>

    <br />
    <h2>Summary</h2>

   <div class="dispalyplug"><asp:Label runat="server" ID="lblHtmlText" ></asp:Label></div> 

   <div class="dispalyplug"><asp:Label runat="server" ID="lblLatexText" Visible="false"></asp:Label></div>

    <br />
   <div class="dispalyplug"><asp:Label runat="server" ID="lblLatexTextInHtml"></asp:Label></div>

</div>