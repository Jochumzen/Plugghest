<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="View.ascx.cs" Inherits="Plugghest.Modules.DisplayPlugg.View" %>
<%@ Register TagPrefix="dnn" TagName="TextEditor" Src="~/controls/TextEditor.ascx" %>
<style>
    .dispalyplug
    {
        margin-bottom: 8px;
    }

    .Icon_1
    {
        background: url("http://plugghest.com/portals/_default/containers/20047-unlimitedcolorpack-033/images/quotationMarks.png") no-repeat scroll center center #FFFFFF;
        display: block;
        height: 36px;
        margin: -18px auto 0;
        width: 82px;
    }


    .title1
    {
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

<div style="width: 642px">
    <asp:Button ID="btnEditPlugg" runat="server" Text="Edit Plugg"  Visible="False" OnClick="btnEditPlugg_Click" />
    <asp:Button ID="btnExitEditMode" runat="server" Text="Exit Edit Mode"  Visible="False" OnClick="btnExitEditMode_Click" />
 
    </div>

<div id="divTitle" runat="server" class="dispalyplug"></div>
  <asp:Button ID="btnSaveSubjects" Text="Save Subjects" runat="server" OnClick="btnExitEditMode_Click"  />

<script type="text/javascript">
    $(document).ready(function () {

        function AddPlug_Click() {
            alert("calling");
        }
    });
    </script>
<%--<script type="text/javascript">
    $(document).ready(function () {

        $("#" + '<%=btnSaveTitle.ClientID%>').hide();
        $("#" + '<%=txtSaveTitle.ClientID%>').hide();

        $("#" + '<%=btnSaveLatext.ClientID%>').hide();
        $("#" + '<%=txtLatextText.ClientID%>').hide();

        $("#" + '<%=btnSaveHtmltext.ClientID%>').hide();
        $('.dnnForm.dnnTextEditor.dnnClear').hide();

    });
    function InitBlock() {

        $("#" + '<%=btnSaveTitle.ClientID%>').hide();
        $("#" + '<%=txtSaveTitle.ClientID%>').hide();

        $("#" + '<%=btnSaveLatext.ClientID%>').hide();
        $("#" + '<%=txtLatextText.ClientID%>').hide();

        $("#" + '<%=btnSaveHtmltext.ClientID%>').hide();
        $('.dnnForm.dnnTextEditor.dnnClear').hide();

    };
    function EditTitleFun() {
        $("#" + '<%=lblTitle.ClientID%>').hide();
        $("#" + '<%=btnEditTitle.ClientID%>').hide();
        $("#" + '<%=btnSaveTitle.ClientID%>').show();
        $("#" + '<%=txtSaveTitle.ClientID%>').show();
        $("#" + '<%=btnCancelTitle.ClientID%>').show();
        $("#" + '<%=txtSaveTitle.ClientID%>').val($("#" + '<%=lblTitle.ClientID%>').html());
    };
    function EditLatextTextFun() {
        $("#" + '<%=lblLatexText.ClientID%>').hide();
        $("#" + '<%=btnEditLatextText.ClientID%>').hide();
        $("#" + '<%=btnSaveLatext.ClientID%>').show();
        $("#" + '<%=btnCancelLatext.ClientID%>').show();
        $("#" + '<%=txtLatextText.ClientID%>').show();
        $("#" + '<%=txtLatextText.ClientID%>').val($("#" + '<%=hdLatextText.ClientID%>').val());
    };

    function EditHtmlTextFun() {
        $("#" + '<%=lblHtmlText.ClientID%>').hide();
        $("#" + '<%=btnEditHtmlText.ClientID%>').hide();
        $("#" + '<%=btnSaveHtmltext.ClientID%>').show();
        $("#" + '<%=btnCancelHtmlText.ClientID%>').show();
        $('.dnnForm.dnnTextEditor.dnnClear').show();
        $($(window.dnn_ctr618_View_txtHtmlText_txtHtmlText_contentIframe).contents().find('body')).html($("#" + '<%=lblHtmlText.ClientID%>').html())
    };

    function HideRichTextEditor() {
        $('.dnnForm.dnnTextEditor.dnnClear').hide();
    }

    function CancelHtmlText() {
        $('.dnnForm.dnnTextEditor.dnnClear').hide();
        $("#" + '<%=btnEditHtmlText.ClientID%>').show();
        $("#" + '<%=lblHtmlText.ClientID%>').show();
        $("#" + '<%=btnCancelHtmlText.ClientID%>').hide();
        $("#" + '<%=btnSaveHtmltext.ClientID%>').hide();
    }
    function CancelLatexText() {
        $("#" + '<%=txtLatextText.ClientID%>').hide();
        $("#" + '<%=btnEditLatextText.ClientID%>').show();
        $("#" + '<%=lblLatexText.ClientID%>').show();
        $("#" + '<%=btnCancelLatext.ClientID%>').hide();
        $("#" + '<%=btnSaveLatext.ClientID%>').hide();
    }
    function CancelTitle() {
        $("#" + '<%=txtSaveTitle.ClientID%>').hide();
        $("#" + '<%=btnEditTitle.ClientID%>').show();
        $("#" + '<%=lblTitle.ClientID%>').show();
        $("#" + '<%=btnCancelTitle.ClientID%>').hide();
        $("#" + '<%=btnSaveTitle.ClientID%>').hide();
     }
    
</script>--%>

