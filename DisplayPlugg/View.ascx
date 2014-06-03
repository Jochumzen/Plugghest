<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="View.ascx.cs" Inherits="Plugghest.Modules.DisplayPlugg.View" %>
<%@ Register TagPrefix="dnn" TagName="TextEditor" Src="~/controls/TextEditor.ascx" %>


<%----------------------------------------tree -start-----------------------------------------%>

 <link href="/DesktopModules/DisplayPlugg/Script/js/jqtree.css" rel="stylesheet" />
    <script src="/DesktopModules/DisplayPlugg/Script/js/tree.jquery.js"></script>
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
      function getRichtext() {
          $('#' + '<%=hdnrichtext.ClientID%>').val($('#editor').html());
      }
    </script>   

<%----------------------------------------tree -end-----------------------------------------%>

<script src="/DesktopModules/DisplayPlugg/Script/js/jquery-ui-1.10.4.custom.js"></script>
<link href="/DesktopModules/DisplayPlugg/Script/external/prettify.css" rel="stylesheet" />
<link href="http://netdna.bootstrapcdn.com/twitter-bootstrap/2.3.1/css/bootstrap-combined.no-icons.min.css" rel="stylesheet">
<link href="http://netdna.bootstrapcdn.com/twitter-bootstrap/2.3.1/css/bootstrap-responsive.min.css" rel="stylesheet">
<link href="http://netdna.bootstrapcdn.com/font-awesome/3.0.2/css/font-awesome.css" rel="stylesheet">

<script src="/DesktopModules/DisplayPlugg/Script/external/jquery.hotkeys.js"></script>
<script src="http://netdna.bootstrapcdn.com/twitter-bootstrap/2.3.1/js/bootstrap.min.js"></script>
<link href="/DesktopModules/DisplayPlugg/Script/index.css" rel="stylesheet" />
<script src="/DesktopModules/DisplayPlugg/Script/bootstrap-wysiwyg.js"></script>

<script>
    $(function () {
        function initToolbarBootstrapBindings() {
            var fonts = ['Serif', 'Sans', 'Arial', 'Arial Black', 'Courier',
                  'Courier New', 'Comic Sans MS', 'Helvetica', 'Impact', 'Lucida Grande', 'Lucida Sans', 'Tahoma', 'Times',
                  'Times New Roman', 'Verdana'],
                  fontTarget = $('[title=Font]').siblings('.dropdown-menu');
            $.each(fonts, function (idx, fontName) {
                fontTarget.append($('<li><a data-edit="fontName ' + fontName + '" style="font-family:\'' + fontName + '\'">' + fontName + '</a></li>'));
            });
            $('a[title]').tooltip({ container: 'body' });
            $('.dropdown-menu input').click(function () { return false; })
                .change(function () { $(this).parent('.dropdown-menu').siblings('.dropdown-toggle').dropdown('toggle'); })
            .keydown('esc', function () { this.value = ''; $(this).change(); });

            $('[data-role=magic-overlay]').each(function () {
                var overlay = $(this), target = $(overlay.data('target'));
                overlay.css('opacity', 0).css('position', 'absolute').offset(target.offset()).width(target.outerWidth()).height(target.outerHeight());
            });
            if ("onwebkitspeechchange" in document.createElement("input")) {
                var editorOffset = $('#editor').offset();
                $('#voiceBtn').css('position', 'absolute').offset({ top: editorOffset.top, left: editorOffset.left + $('#editor').innerWidth() - 35 });
            } else {
                $('#voiceBtn').hide();
            }
        };
        function showErrorAlert(reason, detail) {
            var msg = '';
            if (reason === 'unsupported-file-type') { msg = "Unsupported format " + detail; }
            else {
                console.log("error uploading file", reason, detail);
            }
            $('<div class="alert"> <button type="button" class="close" data-dismiss="alert">&times;</button>' +
             '<strong>File upload error</strong> ' + msg + ' </div>').prependTo('#alerts');
        };
        initToolbarBootstrapBindings();
        $('#editor').wysiwyg({ fileUploadError: showErrorAlert });
        window.prettyPrint && prettyPrint();
    });
</script>


<script>
    (function (i, s, o, g, r, a, m) {
        i['GoogleAnalyticsObject'] = r; i[r] = i[r] || function () {
            (i[r].q = i[r].q || []).push(arguments)
        }, i[r].l = 1 * new Date(); a = s.createElement(o),
        m = s.getElementsByTagName(o)[0]; a.async = 1; a.src = g; m.parentNode.insertBefore(a, m)
    })(window, document, 'script', '//www.google-analytics.com/analytics.js', 'ga');
    ga('create', 'UA-37452180-6', 'github.io');
    ga('send', 'pageview');
</script>
<script>(function (d, s, id) {
    var js, fjs = d.getElementsByTagName(s)[0];
    if (d.getElementById(id)) return;
    js = d.createElement(s); js.id = id;
    js.src = "http://connect.facebook.net/en_GB/all.js#xfbml=1";
    fjs.parentNode.insertBefore(js, fjs);
}(document, 'script', 'facebook-jssdk'));
</script>

<script>!function (d, s, id) { var js, fjs = d.getElementsByTagName(s)[0]; if (!d.getElementById(id)) { js = d.createElement(s); js.id = id; js.src = "http://platform.twitter.com/widgets.js"; fjs.parentNode.insertBefore(js, fjs); } }(document, "script", "twitter-wjs");</script>




 <script type="text/javascript">
     function getYt() {         
       
         var a = $("#title").text();
         $("#" + '<%=yttitle.ClientID%>').val(a);
         $("#" + '<%=ytduration.ClientID%>').val($("#duration").text());
         $("#" + '<%=ytYouTubeCode.ClientID%>').val($("#YouTubeCode").text());
         $("#" + '<%=ytAuthor.ClientID%>').val($("#Author").text());
         $("#" + '<%=ytYouTubeCreatedOn.ClientID%>').val( $("#YouTubeCreatedOn").text());
         $("#" + '<%=ytYouTubeComment.ClientID%>').val($("#YouTubeComment").text());
     }

     function SelSub() {      
         if ($("#<%=hdnNodeSubjectId.ClientID%>").val() != "") {                    
           
         }
         else {
             alert("Please select any one subject");
             return false;
         }      
      }

    </script>

<%-- you tube --%>


<style>
    .small_fount
    {
        font-size:12px;
    }
    body > #feedHeaderContainer
    {
        display: none;
    }
    .btneditplug
    {
        float: right;
    }

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


<!-- Element to pop up -->




<asp:HiddenField ID="hdnTreeData" runat="server" Value="" />
<asp:HiddenField ID="hdnNodeSubjectId" runat="server" />
<asp:HiddenField ID="hdnDelbtnId" runat="server" />
   
<div>

    <asp:Button CssClass="cls small_fount" ID="btnlocal" Text="View this Plugg in the language it was created " runat="server" OnClick="btnlocal_Click" />
    <asp:Button CssClass="cls" ID="btncanceltrans" Text="Cancel translation" runat="server" OnClick="btncanceltrans_Click" Visible="False" />

    <asp:Button CssClass="btneditplug" ID="btnEditPlug" resourcekey="btnSaveSubjects" Text="Edit Plug" runat="server" OnClick="btnEditPlugg_Click" />
    <asp:Button CssClass="btneditplug" ID="btncanceledit" resourcekey="btncanceledit" Text="Cancel Plug" runat="server" OnClick="btncanceledit_Click" Visible="False" />
    <asp:Button CssClass="btneditplug small_fount" ID="btntransplug" meta:resourcekey="btntransplug" Text="Help us with the translation of this Plugg" runat="server" OnClick="btntransplug_Click" />

</div>

<asp:Label ID="lblnoCom" runat="server" Visible="false"></asp:Label>
<asp:Panel runat="server" ID="pnlRRT" Visible="False">
    <dnn:texteditor runat="server" id="richrichtext"></dnn:texteditor>
    <asp:Button ID="btnSaveRRt" runat="server" Text="Save" OnClick="btnSaveRRt_Click" /><asp:Button ID="btnCanRRt" runat="server" Text="Cancel" OnClick="Cancel_Click" />
</asp:Panel>
<asp:Panel ID="richtextbox" runat="server" Visible="False">
    <div class='container'>
        <div class='hero-unit'>

            <div id='alerts'></div>
            <div class='btn-toolbar' data-role='editor-toolbar' data-target='#editor'>


                <div class='btn-group'>
                    <a class='btn' data-edit='bold' title='Bold (Ctrl/Cmd+B)'><i class='icon-bold'></i></a>
                    <a class='btn' data-edit='italic' title='Italic (Ctrl/Cmd+I)'><i class='icon-italic'></i></a>

                </div>
                <div class='btn-group'>
                    <a class='btn' data-edit='insertunorderedlist' title='Bullet list'><i class='icon-list-ul'></i></a>
                    <a class='btn' data-edit='insertorderedlist' title='Number list'><i class='icon-list-ol'></i></a>

                </div>

                <div class='btn-group'>
                    <a class='btn dropdown-toggle' data-toggle='dropdown' title='Hyperlink'><i class='icon-link'></i></a>
                    <div class='dropdown-menu input-append'>
                        <input class='span2' placeholder='URL' type='text' data-edit='createLink' />
                        <button class='btn' type='button'>Add</button>
                    </div>
                    <a class='btn' data-edit='unlink' title='Remove Hyperlink'><i class='icon-cut'></i></a>

                </div>


                <div class='btn-group'>
                    <a class='btn' data-edit='undo' title='Undo (Ctrl/Cmd+Z)'><i class='icon-undo'></i></a>
                    <a class='btn' data-edit='redo' title='Redo (Ctrl/Cmd+Y)'><i class='icon-repeat'></i></a>
                </div>

            </div>

            <div id='editor'>
                Go ahead&hellip;
            </div>
            <br />

        </div>



    </div>
    <asp:Button ID="btnSaveRt" OnClientClick="getRichtext()" runat="server" Text="Save" OnClick="btnSaveRt_Click" /><asp:Button ID="btnCanRt" runat="server" Text="Cancel" OnClick="Cancel_Click" Height="27px" />
</asp:Panel>
<%--<label  id="lbllabel" runat="server" resourcekey="lblExample" ></label>--%>
<asp:Panel runat="server" ID="pnllabel" Visible="False">
    <asp:TextBox runat="server" ID="txtlabel"></asp:TextBox>
    <asp:Button ID="btnLabelSave" runat="server" Text="Save" OnClick="btnLabelSave_Click" /><asp:Button ID="Cancel" runat="server" Text="Cancel" OnClick="Cancel_Click" />
    <asp:HiddenField ID="hdnlabel" runat="server" />
</asp:Panel>
<asp:Panel runat="server" ID="pnlYoutube" Visible="false">
    <asp:TextBox ID="txtYouTube" runat="server" />

    <input type="button" id="btnGetYoutubeVideo" value="Get Video" onclick="CheckURL(this);" />
    <h2   class="title"></h2>

    <h3   class="duration"> </h3>
    <h6  class="YouTubeCode"></h6>
    <h7  class="Author"></h7>
    <h7  class="YouTubeCreatedOn"></h7>
    <h7   class="YouTubeComment"></h7>

    <script type="text/javascript">
        function CheckURL(Control) {

            var code = "";
            var url = $($(Control).parent()).find('input[type=text]').val();
            if (url.length == 11) {
                code = url;
            }
            else if (url.indexOf("www.youtube.com") > -1) {

                code = url.substr(url.length - 11, 11);
            }
            else {
                alert("Invalid URL");
            }
            if ($(Control).parent().find("iframe").length > 0) {
                $(Control).parent().find("iframe").remove();
            }
            $($($(Control).parent()).find("input[type=button]")).after("  <iframe style='display: block' width='420' height='345'src='http://www.youtube.com/embed/" + code + "'></iframe>");

            //$.ajax({
            //    url: "http://gdata.youtube.com/feeds/api/videos/" + code + "?v=2&alt=json",
            //    dataType: "jsonp",
            //    success: function (data) {
            //        alert(data);
            //    }
            //});
            $.getJSON('http://gdata.youtube.com/feeds/api/videos/' + code + '?v=2&alt=jsonc', function (data, status, xhr) {

                $($($(Control).parent()).find(".title")).html("title :<span id='title'>" + data.data.title + "</span><br />");
                $($($(Control).parent()).find(".duration")).html("duration :<span id='duration'>" + data.data.duration + "</span> Seconds<br />");
                $($($(Control).parent()).find(".YouTubeCode")).html("YouTube Code :<span id='YouTubeCode'>" + data.data.id + "</span><br />");
                $($($(Control).parent()).find(".Author")).html("Author :<span id='Author'>" + data.data.uploader + "</span><br />");
                $($($(Control).parent()).find(".YouTubeCreatedOn")).html("YouTube CreatedOn :<span id='YouTubeCreatedOn'>" + data.data.uploaded + "</span><br />");
                $($($(Control).parent()).find(".YouTubeComment")).html("YouTube Author Comment :<span id='YouTubeComment'>" + data.data.description + "</span><br />");
                // data contains the JSON-Object below
            });
         

        };
    </script>

    <asp:Button ID="btnYtSave" runat="server" Text="Save" OnClientClick="getYt()" OnClick="btnYtSave_Click" /><asp:Button ID="btnYtCaNCEL" runat="server" Text="Cancel" OnClick="Cancel_Click" />
</asp:Panel>



<asp:Panel runat="server" ID="pnlletex">
</asp:Panel>
 <div class="tree">
                <div id="tree2"></div>
            </div>
<asp:Panel runat="server" ID="pnlTree">
      <asp:Button ID="btnSelSub" OnClientClick="SelSub()" runat="server" Text="Save" OnClick="btnSelSub_Click"  /><asp:Button ID="btnTreecancel" runat="server" Text="Cancel" OnClick="Cancel_Click" />
</asp:Panel>
<%--   <asp:Button ID="btnEditPlugg" runat="server" Text="Edit Plugg"  Visible="False" OnClick="btnEditPlugg_Click" />
    <asp:Button ID="btnExitEditMode" runat="server" Text="Exit Edit Mode"  Visible="False" OnClick="btnExitEditMode_Click" />--%>


<asp:HiddenField ID="hdnDDLtxt" runat="server" />

<asp:Panel ID="pnlPluggCom" runat="server">
<table class="auto-style1">
    <tr>
        <td>
            <div runat="server" ID="divTree">
                <asp:Label ID="lbltree" runat="server"></asp:Label>              
</div>          
            <div id="divTitle" runat="server" class="dispalyplug"></div>
        </td>
        <td>
            <div id="divTransaltor" runat="server" class="dispalyplug"></div>
        </td>


    </tr>
</table>
</asp:Panel>

<asp:HiddenField ID="hdnrichtext" runat="server" />
   <asp:HiddenField ID="yttitle" runat="server" />
                  <asp:HiddenField ID="ytduration" runat="server" />
                        <asp:HiddenField ID="ytYouTubeCode" runat="server" />
                              <asp:HiddenField ID="ytAuthor" runat="server" />
                                    <asp:HiddenField ID="ytYouTubeCreatedOn" runat="server" />
                                          <asp:HiddenField ID="ytYouTubeComment" runat="server" />



