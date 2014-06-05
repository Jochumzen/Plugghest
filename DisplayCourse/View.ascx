<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="View.ascx.cs" Inherits="Plugghest.Modules.DisplayCourse.View" %>
<%@ Register TagPrefix="dnn" TagName="TextEditor" Src="~/controls/TextEditor.ascx" %>

<link href="module.css" rel="stylesheet" />

 <link href="/DesktopModules/DisplayCourse/Script/js/jqtree.css" rel="stylesheet" />
    <script src="/DesktopModules/DisplayCourse/Script/js/tree.jquery.js"></script>
  <script type="text/javascript">

      $(document).ready(function () {
          $("#" + '<%=pnlTree.ClientID%>').hide();


          $('.btncs').bind('click', function () {
              var clickedID = this.id;
              var string = "#" + clickedID;
              var id = $($(string).prev()).attr("id");
              var newid = $("#" + id).val();
              $('#<%=hdnDDLtxt.ClientID%>').val(newid);
          });

          $(".btnTreeEdit").click(function () {
              $('#' + '<%=pnlCourseCom.ClientID%>').hide();
              $("#" + '<%=pnlTree.ClientID%>').show();
              var $tree = $('#tree2');
              $('#tree2').tree({
                  data: eval($("#" + '<%=hdnTreeData.ClientID%>').attr('value')),
                  selectable: true,
                  autoEscape: false,
                  autoOpen: true,
              });

              $tree.bind(
          'tree.select',
          function (event) {
              if (event.node) {
                  var node = event.node;
                  // alert(node.Mother.getjson)               
                  $("#<%=hdnNodeSubjectId.ClientID%>").val(node.SubjectId);
              }
          }
          );

          });

      });    
    </script> 
<style>
    .small_fount
    {
        font-size:12px;
    }
    body > #feedHeaderContainer
    {
        display: none;
    }
    .btneditCourse
    {
        float: right;
    }

    .dispalyCourse
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

    .auto-style1
    {
        width: 100%;
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

<asp:HiddenField ID="hdnTreeData" runat="server" Value="" />
<asp:HiddenField ID="hdnNodeSubjectId" runat="server" />
<asp:HiddenField ID="hdnDelbtnId" runat="server" />
<asp:HiddenField ID="hdnDDLtxt" runat="server" />
<asp:HiddenField ID="hdnrichtext" runat="server" />
<div>

    <asp:Button CssClass="cls small_fount" ID="btnlocal" Text="View this Course in the language it was created " runat="server" OnClick="btnlocal_Click" />
    <asp:Button CssClass="cls" ID="btncanceltrans" Text="Cancel translation" runat="server" OnClick="btncanceltrans_Click" Visible="False" />

    <asp:Button CssClass="btneditCourse" ID="btnEditCourse"  resourcekey="btnSaveSubjects" Text="Edit Course" runat="server" OnClick="btnEditcourse_Click" />
    <asp:Button CssClass="btneditCourse" ID="btncanceledit" resourcekey="btncanceledit" Text="Cancel Course" runat="server" OnClick="btncanceledit_Click" Visible="False" />
    <asp:Button CssClass="btneditCourse small_fount" ID="btntransCourse" meta:resourcekey="btntransCourse" Text="Help us with the translation of this Course" runat="server" OnClick="btntransCourse_Click" />

</div>
<asp:Label ID="lblnoCom" runat="server" Visible="false"></asp:Label>

<asp:Panel runat="server" ID="pnlRRT" Visible="False">
    <dnn:texteditor runat="server" id="richrichtext"></dnn:texteditor>
    <asp:Button ID="btnSaveRRt" runat="server" Text="Save" OnClick="btnSaveRRt_Click" /><asp:Button ID="btnCanRRt" runat="server" Text="Cancel" OnClick="Cancel_Click" />
</asp:Panel>
<asp:Panel runat="server" ID="pnlTitle" Visible="False">
    <asp:TextBox runat="server" ID="txttitle"></asp:TextBox>
    <asp:Button ID="btnTitleSave" runat="server" Text="Save" OnClick="btnTitleSave_Click" /><asp:Button ID="Cancel" runat="server" Text="Cancel" OnClick="Cancel_Click" />
    <asp:HiddenField ID="hdntitle" runat="server" />
</asp:Panel>
<asp:Panel runat="server" ID="pnlDescription" Visible="False">
    <asp:TextBox runat="server" ID="txtDescription" TextMode="MultiLine"></asp:TextBox>
    <asp:Button ID="btnDescriptionSave" runat="server" Text="Save" OnClick="btnDescriptionSave_Click" /><asp:Button ID="btnDescCancel" runat="server" Text="Cancel" OnClick="Cancel_Click" />
    <asp:HiddenField ID="hdndescription" runat="server" />
</asp:Panel>

<div class="tree">
                <div id="tree2"></div>
            </div>
<asp:Panel runat="server" ID="pnlTree">
      <asp:Button ID="btnSelSub" OnClientClick="SelSub()" runat="server" Text="Save" OnClick="btnSelSub_Click"  /><asp:Button ID="btnTreecancel" runat="server" Text="Cancel" OnClick="Cancel_Click" />
</asp:Panel>

<asp:Panel ID="pnlCourseCom" runat="server">

         <div runat="server" ID="divTree">
                <asp:Label ID="lbltree" runat="server"></asp:Label>     
              
              
</div>    
       <asp:Label runat="server" ID="lblTitle" ></asp:Label>
 
    <asp:Button ID="btneditT" runat="server" Visible="false" OnClick="btneditT_Click" />    <asp:Button ID="btnGoogleTransTxtOkT" runat="server" Visible="false" OnClick="btneditT_Click" />   <asp:Button ID="btnImpGTransT" runat="server" Visible="false" OnClick="btnImpGTransT_Click"  />   <asp:Button ID="btnImpHumTnsTxtT" runat="server" Visible="false" OnClick="btneditT_Click" />
   
       <br /> <asp:Label runat="server" ID="lblDescription" ></asp:Label> 
   
    <asp:Button ID="btneditD" runat="server" Visible="false" OnClick="btneditD_Click" />    <asp:Button ID="btnGoogleTransTxtOkD" runat="server" Visible="false" OnClick="btneditD_Click" />   <asp:Button ID="btnImpGTransD" runat="server" Visible="false" OnClick="btnImpGTransD_Click"  />   <asp:Button ID="btnImpHumTnsTxtD" runat="server"  Visible="false" OnClick="btneditD_Click" />
    <br />
      <asp:Label runat="server" ID="lblRichRichTxt" ></asp:Label>
   
    <asp:Button ID="btneditR" runat="server" Visible="false" OnClick="btneditR_Click" />   <asp:Button ID="btnGoogleTransTxtOkR" runat="server" Visible="false" OnClick="btneditR_Click" />   <asp:Button ID="btnImpGTransR" runat="server" Visible="false" OnClick="btnImpGTransR_Click"  />   <asp:Button ID="btnImpHumTnsTxtR" runat="server" Visible="false" OnClick="btneditR_Click" />
   

</asp:Panel>

