<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="View.ascx.cs" Inherits="Plugghest.Modules.CreateCourse.View" %>

<%@ Register TagPrefix="dnn" TagName="label" Src="~/controls/LabelControl.ascx" %>
<%@ Register TagPrefix="dnn" TagName="TextEditor" Src="~/controls/TextEditor.ascx" %>

<link href="module.css" rel="stylesheet" />

<div class="CreateCource">
    <fieldset>
        <div class="dnnFormItem">
            <dnn:label id="lblLanguage" runat="server" controlname="DDLanguage" />
            <asp:DropDownList ID="DDLanguage" runat="server"></asp:DropDownList>
        </div>

        <div class="dnnFormItem">
            <dnn:label id="lblTitle" runat="server" Text="Title" />
            <asp:TextBox ID="txtTitle" runat="server" />
            <asp:RequiredFieldValidator ID="val_txtTitle" Display="Dynamic" runat="server" CssClass="valcls" ErrorMessage="Please Enter Title" ControlToValidate="txtTitle"></asp:RequiredFieldValidator>
        </div>

         <div class="dnnFormItem">
            <dnn:label id="lblPluggs" runat="server" />
            <asp:TextBox ID="txtPluggs" runat="server" />
        </div>

        <div style="margin-left:413px"><asp:Label ID="lblplugss" runat="server"></asp:Label></div>

        <div style="margin-left:413px"><asp:LinkButton ID="btnCheck" runat="server" OnClick="btnCheck_Click" 
                        resourcekey="btnCheck" CssClass="dnnPrimaryAction" Text="Check" /></div>



        <div class="dnnFormItem">
            <dnn:label id="lblEditPlug" runat="server" controlname="rdEditPlug" />
            <asp:RadioButtonList ID="rdEditPlug" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                <asp:ListItem resourcekey="Asynchronous" Value="Any registered user" Selected="True" />
                <asp:ListItem resourcekey="Synchronous" Value="Only me" />
            </asp:RadioButtonList>
        </div>

       <br />

        <div class="dnnFormItem">
            <dnn:TextEditor ID="txtHtmlText" runat="server" Width="100%" Height="400px" />
        </div>


        <div class="dnnFormItem">
            <ul class="dnnActions dnnClear">
                <li>
                    <asp:LinkButton ID="btnSubmit" runat="server" 
                        resourcekey="btnSubmit" CssClass="dnnPrimaryAction" Text="Submit" OnClick="btnSubmit_Click" /></li>
                <li>
                    <asp:LinkButton ID="btnCancel" runat="server" 
                        resourcekey="btnCancel" CssClass="dnnSecondaryAction" Text="Cancel" OnClick="btnCancel_Click" /></li>
            </ul>
        </div>
    </fieldset>


   
</div>