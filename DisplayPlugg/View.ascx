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
    <div class="dispalyplug">
        <asp:Label runat="server" ID="lblYoutube"></asp:Label>
    </div>

    <br />
    <h2>Summary</h2>


    <asp:UpdatePanel runat="server" ID="EditUpdatePanel" UpdateMode="Conditional">
        <Triggers>

            <asp:AsyncPostBackTrigger ControlID="btnSaveTitle" />
        </Triggers>
        <ContentTemplate>
            <div id="divTitle" runat="server" class="dispalyplug">
                <asp:Label runat="server" ID="lblTitle"></asp:Label>
                <input runat="server" id="btnEditTitle" type="button" title="Edit" onclick="EditTitleFun(); return false;" value="Edit Title" />
                <asp:TextBox runat="server" ID="txtSaveTitle"></asp:TextBox>
                <%--<input  runat="server" id="btnSaveTitle" type="button" title="Save" value="Save Title" onclick="SaveTitleFun(); return false;"/>--%>
                <asp:Button ID="btnSaveTitle" runat="server" Text="Save Title" OnClick="btnSaveTitle_Click" />
                <asp:Button ID="btnCancelTitle"  runat="server" Text="Cancel" OnClientClick="CancelTitle();  return false;" />
            </div>
        </ContentTemplate>

    </asp:UpdatePanel>


    <asp:UpdatePanel runat="server" ID="UpdatePanel2" UpdateMode="Conditional">
        <Triggers>

            <asp:AsyncPostBackTrigger ControlID="btnSaveHtmltext" />
        </Triggers>
        <ContentTemplate>
            <div class="dispalyplug">
                <asp:Label runat="server" ID="lblHtmlText"></asp:Label>
            </div>
            <input runat="server" id="btnEditHtmlText" type="button" title="Edit" onclick="EditHtmlTextFun(); return false;" value="Edit Html Text" />
            <%--<asp:TextBox runat="server" ID="txtHtmlText" TextMode="MultiLine" style="width:100%;"></asp:TextBox>--%>
            <dnn:TextEditor ID="txtHtmlText" runat="server" Width="100%" Height="400px" />


            <%--<input  runat="server" id="btnSaveTitle" type="button" title="Save" value="Save Title" onclick="SaveTitleFun(); return false;"/>--%>
            <asp:Button ID="btnSaveHtmltext" runat="server" Text="Save Html Text" OnClick="btnSaveHtmltext_Click" OnClientClick="return HideRichTextEditor();" />
            <asp:Button ID="btnCancelHtmlText" runat="server" Text="Cancel" OnClientClick="CancelHtmlText();  return false;" />
        </ContentTemplate>

    </asp:UpdatePanel>




    <br />
    <div class="dispalyplug">
        <asp:Label runat="server" ID="lblLatexSepretor" Text="Latext Text"></asp:Label>
    </div>
    <asp:UpdatePanel runat="server" ID="UpdatePanel1" UpdateMode="Conditional">
        <Triggers>

            <asp:AsyncPostBackTrigger ControlID="btnSaveLatext" />
        </Triggers>
        <ContentTemplate>
            <asp:HiddenField ID="hdLatextText" runat="server" />
            <div class="dispalyplug">
                <asp:Label runat="server" ID="lblLatexText" Visible="false"></asp:Label>
            </div>
            <div class="dispalyplug">
                <asp:Label runat="server" ID="lblLatexTextInHtml"></asp:Label>
            </div>
            <input runat="server" id="btnEditLatextText" type="button" title="Edit" onclick="EditLatextTextFun(); return false;" value="Edit Latext Text" />
            <asp:TextBox runat="server" ID="txtLatextText" TextMode="MultiLine" Style="width: 100%; height: 250px;"></asp:TextBox>
            <%--<input  runat="server" id="btnSaveTitle" type="button" title="Save" value="Save Title" onclick="SaveTitleFun(); return false;"/>--%>
            <asp:Button ID="btnSaveLatext" runat="server" Text="Save Tatext Text" OnClick="btnSaveLatext_Click" />
            <asp:Button ID="btnCancelLatext" runat="server" Text="Cancel" OnClientClick="CancelLatexText();  return false;" />
        </ContentTemplate>
    </asp:UpdatePanel>
       
</div>
<script type="text/javascript">
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
    
</script>

