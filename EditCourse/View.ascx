<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="View.ascx.cs" Inherits="Plugghest.Modules.EditCourse.View" %>

<%-- <link href="/DesktopModules/Plugghest_Subjects/js/jqtree.css" rel="stylesheet" />
<script src="/DesktopModules/Plugghest_Subjects/js/tree.jquery.js"></script>--%>

<link href="/js/js_tree/jqtree.css" rel="stylesheet" />
<script src="/js/js_tree/tree.jquery.js"></script> 

<div class="resp_container">
    <div class="tree">
        <div id="tree2"></div>
    </div>
</div>
<asp:HiddenField ID="hdnTreeData" runat="server" Value="" />
<asp:HiddenField ID="hdnGetJosnResult" runat="server" />
<asp:HiddenField ID="hdnNodeCourseItemID" runat="server" />
<br />
<asp:Label runat="server" ID="lblError"  ForeColor="Red"></asp:Label>
<div>
    Heading&nbsp;&nbsp; 
            &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="txtHeading" runat="server"></asp:TextBox>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
          <input type="button" id="addheading" value="Add Heading" onclick="return addHeading()" />

    <%--<asp:Button ID="btnAddHeading" runat="server" OnClientClick="return AddTempHeading();" Text="Insert Heading" OnClick="btnAddHeading_Click" />--%>
                        <br />
    <br />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
        <ContentTemplate>
            <td>Add Plug</td>
            <td>&nbsp;&nbsp;&nbsp;
                <asp:TextBox ID="txtAddPlugg" runat="server"></asp:TextBox></td>
            <asp:Label ID="lblPlugg" runat="server"></asp:Label>
            <td></td>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnAddplugg" EventName="click" />
        </Triggers>
    </asp:UpdatePanel>
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:Button ID="btnAddplugg" runat="server" Text="Insert Plugg" OnClick="btnAddplugg_Click" />
    <br />
    <br />
    <asp:Button ID="btnRemoveSelectedItem" Text="Remove Selected item" runat="server" OnClientClick="return RemoveNode();" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>
        
                <asp:Button ID="btnSave" runat="server" Text="Save" OnClientClick="return getjson();" onclick="btnSave_Click" />
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                <asp:Button ID="btnCancel" runat="server" Text="Cancel" />
</div>
<div>
</div>

<script type="text/javascript">
    $(document).ready(function () {
        $('#tree2').tree({
            data: eval($("#" + '<%=hdnTreeData.ClientID%>').attr('value')),
            selectable: true,
            autoEscape: false,
            autoOpen: true,
            dragAndDrop: true
        });
        $("#lblError").hide();
    });

    function getjson() {
        var record = $('#tree2').tree('toJson');
        $("#<%=hdnGetJosnResult.ClientID%>").val(record);
    }

    function addHeading() {
        var node = $('#tree2').tree('getSelectedNode');
        var Error = "";
        if (!node)
            Error = 'Please Select Node \n';
        if ($("#<%=txtHeading.ClientID%>").val() == '')
            Error += 'Please Enter Heading Name';
        if (Error != "") {
            alert(Error);
            return false;
        }
        $('#tree2').tree(
            'addNodeAfter',
            {
                label: $("#<%=txtHeading.ClientID%>").val(),
                ItemType: 1, // 1 for Heading
            },
            node
        );
    }

    function AddTempPlugg() {
        var node = $('#tree2').tree('getSelectedNode');
        var Error = "";
        if (!node)
            Error = 'Please Select Node \n';
        if ($("#<%=txtAddPlugg.ClientID%>").val() == '')
            Error += 'Please Enter Plugg ID';
        if (Error != "") {
            alert(Error);
            return false;
        }
        $('#tree2').tree(
           'addNodeAfter',
           {
               ItemID: $("#<%=txtAddPlugg.ClientID%>").val(),
               ItemType: 0, // 0 for plugg              
               label: $("#<%=lblPlugg.ClientID%>").text(),
           },
           node
        );
    }

    function RemoveNode() {
        var node = $('#tree2').tree('getSelectedNode');
        $('#tree2').tree('removeNode', node);
        return false;
    }
</script>