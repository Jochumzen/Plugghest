<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="View.ascx.cs" Inherits="Plugghest.Modules.CreatePlugg.View" %>
<%@ Register TagPrefix="dnn" TagName="label" Src="~/controls/LabelControl.ascx" %>
<%@ Register TagPrefix="dnn" TagName="TextEditor" Src="~/controls/TextEditor.ascx" %>

<link href="../DesktopModules/Plugghest_Subjects/js/jqtree.css" rel="stylesheet" />
<script src="../DesktopModules/Plugghest_Subjects/js/tree.jquery.js"></script>
<link href="module.css" rel="stylesheet" />




<div class="CreatePluggBody">
    <fieldset>
        <div class="dnnFormItem">
            <dnn:label id="lblLanguage" runat="server" controlname="DDLanguage" />
            <asp:DropDownList ID="DDLanguage" runat="server"></asp:DropDownList>
        </div>

        <div class="dnnFormItem">
            <dnn:label id="lblTitle" runat="server" />
            <asp:TextBox ID="txtTitle" runat="server" />
            <asp:RequiredFieldValidator ID="val_txtTitle" Display="Dynamic" runat="server" CssClass="valcls" ErrorMessage="Please Enter Title" ControlToValidate="txtTitle"></asp:RequiredFieldValidator>
        </div>
        <div class="dnnFormItem">
            <dnn:label id="lblYouTube" runat="server" />
            <asp:TextBox ID="txtYouTube" runat="server" />
            <asp:CustomValidator runat="server" id="cusCustom" ForeColor="Red" controltovalidate="txtYouTube" onservervalidate="cusCustom_ServerValidate" errormessage="Incorrect format" />
        </div>

        <div class="nodediv">

        <div style="float:left">
            <dnn:label id="lblEditPlug" runat="server" controlname="rdEditPlug" />
            <asp:RadioButtonList ID="rdEditPlug" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                <asp:ListItem resourcekey="Asynchronous" Value="Any registered user" Selected="True" />
                <asp:ListItem resourcekey="Synchronous" Value="Only me" />
            </asp:RadioButtonList>
        </div>


       <div class="tree">
       <div id="tree2"></div>
      </div>
        <asp:HiddenField ID="hdnTreeData" runat="server" Value="" />
      </div>

        <div style="clear:both"> </div>


        <div class="dnnFormItem">
            <dnn:TextEditor ID="txtHtmlText" runat="server" Width="100%" Height="400px" />
        </div>
        <div class="dnnFormItem">
            <asp:Label ID="lbldescription" Text="Latex code:" runat="server"></asp:Label> <br />
            <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Rows="18" Columns="20" />
           </div>


        <div class="dnnFormItem">
            <ul class="dnnActions dnnClear">
                <li>
                    <asp:LinkButton ID="btnSubmit" runat="server" OnClick="btnSubmit_Click"
                        resourcekey="btnSubmit" CssClass="dnnPrimaryAction" Text="Submit" /></li>
                <li>
                    <asp:LinkButton ID="btnCancel" runat="server" OnClick="btnCancel_Click"
                        resourcekey="btnCancel" CssClass="dnnSecondaryAction" Text="Cancel" /></li>

                <li>
            </li>
            </ul>
        </div>
    </fieldset>

<asp:Label runat="server" ID="lblError" ForeColor="Red"></asp:Label>
   
</div>

<script type="text/javascript">

    $(document).ready(function () {
        $('#tree2').tree({
            data: eval($("#" + '<%=hdnTreeData.ClientID%>').attr('value')),
            //data:data,
            //dragAndDrop: true,
            selectable: true,
            autoEscape: false,
            autoOpen: true
        });
    });

    </script>