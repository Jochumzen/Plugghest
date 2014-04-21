<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="View.ascx.cs" Inherits="Christoc.Modules.EditCourseItems.View" %>

<div></div>
<link href="/DesktopModules/Plugghest_Subjects/js/jqtree.css" rel="stylesheet" />
<script src="/DesktopModules/Plugghest_Subjects/js/tree.jquery.js"></script>

<div>
    <asp:Label ID="lblHeading" runat="server">Heading</asp:Label>
    <asp:TextBox ID="txtheading" runat="server"></asp:TextBox>
    <asp:Button ID="btnAddHeading" runat="server" Text="Insert Heading"/>
</div>
<div>
    <asp:Label ID="lbladdPlug" runat="server">Add Plug</asp:Label>
    <asp:TextBox ID="txtaddPlug" runat="server"></asp:TextBox>
    <asp:Button ID="btnAddplug" runat="server" Text="Insert Plug" />
</div>
<div>
    <asp:Button  ID="btnRemoveSelectedItem" Text="Remove Selected item" runat="server"/>
</div>
<div>
    <asp:Button ID="btnSave" runat="server" Text="Save" />
    <asp:button ID="btnCancel" runat="server" Text="Cancel" />
</div>
  <div class="resp_container">
          <div class="tree">
       <div id="tree2"></div>
      </div>
      <asp:HiddenField runat="server" ID="hdnTreeData" />
      </div>
<asp:Label runat="server" ID="lblError" ForeColor="Red"></asp:Label>
<asp:HiddenField ID="hdnNodeSubjectId" runat="server" /> 

<script type="text/javascript">

    $(document).ready(function () {
        $('#tree2').tree({
            data: eval($("#" + '<%=hdnTreeData.ClientID%>').attr('value')),
            selectable: true,
            autoEscape: false,
            autoOpen: true
        });
    });

    function getCourseid()
    {
        var node = $('#tree2').tree('getSelectedNode');

        var Error = "";

        if (!node)
            Error = 'Please Select Node \n';

        if (Error != "") {
            alert(Error);
            return false;
        }
        $("#<%=hdnNodeSubjectId.ClientID%>").val(node.SubjectID);
    }

</script>