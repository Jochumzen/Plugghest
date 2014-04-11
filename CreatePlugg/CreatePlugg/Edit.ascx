<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Edit.ascx.cs" Inherits="Plugghest.Modules.CreatePlugg.Edit" %>
<%@ Register TagPrefix="dnn" TagName="label" Src="~/controls/LabelControl.ascx" %>
<div class="dnnForm dnnEditBasicSettings" id="dnnEditBasicSettings">
    <div class="dnnFormExpandContent dnnRight "><a href=""><%=LocalizeString("ExpandAll")%></a></div>
    
</div>

<asp:Label ID="lblError" runat="server" ForeColor="Red" ></asp:Label>

<br /><br />
<asp:DropDownList ID="DDLanguage" runat="server" Visible="false"></asp:DropDownList>

<asp:LinkButton ID="btnRemovePluggs" runat="server"
    OnClick="btnRemovePluggs_Click" Text="Remove Pluggs" CssClass="dnnSecondaryAction" />

<asp:LinkButton ID="CreatePages" runat="server"
    OnClick="CreatePages_Click" Text="Create Pages" CssClass="dnnSecondaryAction" />

<br /><br /><br />

 <div class="dnnFormItem">
            <asp:FileUpload ID="Upload_Textfile" runat="server"  />
</div>
<br />

<div>

<div style="float:left; margin-right:30px">
<asp:LinkButton ID="btnReadTextFile" runat="server" OnClick="btnReadTextFile_Click" 
                        CssClass="dnnSecondaryAction" Text="Read Plugg from text file" />
</div>

<div>
<asp:LinkButton ID="btnReadZipFile" runat="server" OnClick="btnReadZipFile_Click" 
                        CssClass="dnnSecondaryAction" Text="Read several Pluggs from zip-file" />
</div>

</div>







<script language="javascript" type="text/javascript">
    /*globals jQuery, window, Sys */
    (function ($, Sys) {
        function dnnEditBasicSettings() {
            $('#dnnEditBasicSettings').dnnPanels();
            $('#dnnEditBasicSettings .dnnFormExpandContent a').dnnExpandAll({ expandText: '<%=Localization.GetString("ExpandAll", LocalResourceFile)%>', collapseText: '<%=Localization.GetString("CollapseAll", LocalResourceFile)%>', targetArea: '#dnnEditBasicSettings' });
        }

        $(document).ready(function () {
            dnnEditBasicSettings();
            Sys.WebForms.PageRequestManager.getInstance().add_endRequest(function () {
                dnnEditBasicSettings();
            });
        });

    }(jQuery, window.Sys));
</script>
