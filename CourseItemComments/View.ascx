<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="View.ascx.cs" Inherits="Pluggest.Modules.CourseItemComments.View" %>




    <asp:Label runat="server" ID="labHtmlText"></asp:Label>
                <input runat="server" id="btnEditHTML" type="button" title="Edit" onclick="EditHTMLFun(); return false;" value="Edit HTML" />
                <asp:TextBox runat="server" ID="txtHTML"></asp:TextBox>



                <asp:Button ID="btnSaveHTML" runat="server" Text="Save HTML"  OnClick="btnSaveHTML_Click" />
                <asp:Button ID="btnCancelHTML"  runat="server" Text="Cancel" OnClientClick="CancelHTML();  return false;" />




<script type="text/javascript">
    $(document).ready(function () {
        $("#" + '<%=btnSaveHTML.ClientID%>').hide();
        $("#" + '<%=btnCancelHTML.ClientID%>').hide();
        $("#" + '<%=txtHTML.ClientID%>').hide();
    });
   
    function EditHTMLFun() {
        $("#" + '<%=labHtmlText.ClientID%>').hide();
        $("#" + '<%=btnEditHTML.ClientID%>').hide();

        $("#" + '<%=txtHTML.ClientID%>').show();
        $("#" + '<%=btnSaveHTML.ClientID%>').show();
        $("#" + '<%=btnCancelHTML.ClientID%>').show();
        $("#" + '<%=txtHTML.ClientID%>').val($("#" + '<%=labHtmlText.ClientID%>').html());
    };
 

 

    function CancelHTML() {
        $("#" + '<%=labHtmlText.ClientID%>').show();
        $("#" + '<%=btnEditHTML.ClientID%>').show();

        $("#" + '<%=txtHTML.ClientID%>').hide();
        $("#" + '<%=btnSaveHTML.ClientID%>').hide();
        $("#" + '<%=btnCancelHTML.ClientID%>').hide();
    }
 

</script>
