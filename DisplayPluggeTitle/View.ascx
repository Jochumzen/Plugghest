<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="View.ascx.cs" Inherits="Christoc.Modules.DisplayPluggeTitle.View" %>
  <div id="divTranslatedPluggeTitle" runat="server" >
                <asp:Label runat="server" ID="lblTranslatedPluggeTitle"></asp:Label>
                <input runat="server" id="btnEditTranslatedPluggeTitle" type="button" Title="Edit" onclick="EditTranslatedPluggeTitleFun(); return false;" value="Improve Google Translated Text" />
                <asp:TextBox runat="server" ID="txtTranslatedPluggeTitle"></asp:TextBox>
               
       <asp:Button ID="btnGoogleTextOk" runat="server" Text="Google Translated Text OK" OnClick="btnSaveTranslatedPluggeTitle_Click" />
         
                <asp:Button ID="btnSaveTranslatedPluggeTitle" runat="server" Text="Update" OnClick="btnSaveTranslatedPluggeTitle_Click" />
                <asp:Button ID="btnCancelTranslatedPluggeTitle"  runat="server" Text="Cancel" OnClientClick="CancelTranslatedPluggeTitle();  return false;" />
            </div>


<script type="text/javascript">
    $(document).ready(function () {

        $("#" + '<%=btnSaveTranslatedPluggeTitle.ClientID%>').hide();
        $("#" + '<%=txtTranslatedPluggeTitle.ClientID%>').hide();
    });
   
    function EditTranslatedPluggeTitleFun() {
        $("#" + '<%=lblTranslatedPluggeTitle.ClientID%>').hide();
        $("#" + '<%=btnEditTranslatedPluggeTitle.ClientID%>').hide();
        $("#" + '<%=btnSaveTranslatedPluggeTitle.ClientID%>').show();
        $("#" + '<%=txtTranslatedPluggeTitle.ClientID%>').show();
        $("#" + '<%=btnCancelTranslatedPluggeTitle.ClientID%>').show();
        $("#" + '<%=btnGoogleTextOk.ClientID%>').hide();

        
        $("#" + '<%=txtTranslatedPluggeTitle.ClientID%>').val($("#" + '<%=lblTranslatedPluggeTitle.ClientID%>').html());
    };
    function CancelTranslatedPluggeTitle() {
        $("#" + '<%=txtTranslatedPluggeTitle.ClientID%>').hide();
        $("#" + '<%=btnEditTranslatedPluggeTitle.ClientID%>').show();
        $("#" + '<%=lblTranslatedPluggeTitle.ClientID%>').show();
        $("#" + '<%=btnGoogleTextOk.ClientID%>').show();
        $("#" + '<%=btnCancelTranslatedPluggeTitle.ClientID%>').hide();
        $("#" + '<%=btnSaveTranslatedPluggeTitle.ClientID%>').hide();
        $("#" + '<%=lblTranslatedPluggeTitle.ClientID%>').html($("#" + '<%=txtTranslatedPluggeTitle.ClientID%>').val());
    }

</script>

