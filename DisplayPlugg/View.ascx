<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="View.ascx.cs" Inherits="Plugghest.Modules.DisplayPlugg.View"    %>
<%@ Register TagPrefix="dnn" TagName="TextEditor" Src="~/controls/TextEditor.ascx" %>


<script>
    function getRichtext() {
        $('#'+'<%=hdnrichtext.ClientID%>').val($('#editor').html());
    }
</script>

<script src="Script/js/jquery-1.10.2.js"></script>
	<script src="Script/js/jquery-ui-1.10.4.custom.js"></script>

<%--      <link href="/DesktopModules/CreatePlugg2/Script/external/prettify.css" rel="stylesheet" />
    <link href="http://netdna.bootstrapcdn.com/twitter-bootstrap/2.3.1/css/bootstrap-combined.no-icons.min.css" rel="stylesheet">
    <link href="http://netdna.bootstrapcdn.com/twitter-bootstrap/2.3.1/css/bootstrap-responsive.min.css" rel="stylesheet">
		<link href="http://netdna.bootstrapcdn.com/font-awesome/3.0.2/css/font-awesome.css" rel="stylesheet">
   
          <script src="/DesktopModules/CreatePlugg2/Script/external/jquery.hotkeys.js"></script>
    <script src="http://netdna.bootstrapcdn.com/twitter-bootstrap/2.3.1/js/bootstrap.min.js"></script>
		<link href="/DesktopModules/CreatePlugg2/Script/index.css" rel="stylesheet" />
     <script src="/DesktopModules/CreatePlugg2/Script/bootstrap-wysiwyg.js"></script>--%>

     <script src="Script/js/jquery-1.10.2.js"></script>
	<script src="Script/js/jquery-ui-1.10.4.custom.js"></script>
    <link href="http://dnndev.me/Script/external/prettify.css" rel="stylesheet" />
    <link href="http://netdna.bootstrapcdn.com/twitter-bootstrap/2.3.1/css/bootstrap-combined.no-icons.min.css" rel="stylesheet">
    <link href="http://netdna.bootstrapcdn.com/twitter-bootstrap/2.3.1/css/bootstrap-responsive.min.css" rel="stylesheet">
    <link href="http://netdna.bootstrapcdn.com/font-awesome/3.0.2/css/font-awesome.css" rel="stylesheet">

    <script src="http://dnndev.me/Script/external/jquery.hotkeys.js"></script>
    <script src="http://netdna.bootstrapcdn.com/twitter-bootstrap/2.3.1/js/bootstrap.min.js"></script>
    <link href="http://dnndev.me/Script/index.css" rel="stylesheet" />
    <script src="http://dnndev.me/Script/bootstrap-wysiwyg.js"></script>








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
      








<script>
    $(document).ready(function(){
        //$('.btncs').click(function () {
        //    alert("abc");
        //    //$('#element_to_pop_up').bPopup({
        //    //    closeClass: 'b-close'
        //    //});
        //});
        $('.btncs').live('click', function () {
            var clickedID = this.id;
            var string = "#" + clickedID;           
            var id = $($(string).prev()).attr("id");        
            var newid = $("#" + id).val();

            $('#<%=hdn.ClientID%>').val(newid);
          
           
            //hdn
        });

        function SomeMethod(id)
        {
            alert(id);
        }
    });
</script>
<%--    <script type="text/javascript">
             function disable()
             {
                 alert("calling");
                 //document.getElementById("mySelect1").disabled=true;
             }
    $(document).ready(function () {

        function AddPlug_Click() {
            alert("calling");
        }
    });
    </script>--%>
<style>
    .btneditplug
    {
        float:right;
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
        width:100%;
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


    /*.element_to_pop_up, #popup2, .bMulti {
        left: 433px;
position: absolute;
top: 100px;
z-index: 9999;
opacity: 1;
display: block;
background-color: #fff;
border-radius: 10px 10px 10px 10px;
box-shadow: 0 0 25px 5px #999;
color: #111;
display: none;
min-width: 450px;
padding: 25px;

}

    .button.b-close, .button.bClose {

border-radius: 7px 7px 7px 7px;
box-shadow: none;
font: bold 131% sans-serif;
padding: 0 6px 2px;
position: absolute;
right: -7px;
top: -7px;
cursor:pointer;
}*/
</style>


            <!-- Element to pop up -->

<%-- <div id="element_to_pop_up" class="element_to_pop_up">
                <span class="button b-close"><span>X</span></span>
                Content of popup</div>


<div id="DivRRtxt" class="element_to_pop_up">
                <span class="button b-close"><span>X</span></span>


                Rich Rich Text</div>
<div id="DivRtxt" class="element_to_pop_up">
                <span class="button b-close"><span>X</span></span>

                Rich text</div>
<div id="Divlbl" class="element_to_pop_up">
                <span class="button b-close"><span>X</span></span>
               Label</div>
<div id="DivLtex" class="element_to_pop_up">
                <span class="button b-close"><span>X</span></span>
               Latex</div>

<div id="DivUtube" class="element_to_pop_up">
                <span class="button b-close"><span>X</span></span>
                You tube</div>--%>



<div >
  
   <asp:Button CssClass="cls" ID="btnlocal" Text="View this Plugg in the language it was created " runat="server" OnClick="btnlocal_Click"  />
  <asp:Button CssClass="cls" ID="btncanceltrans" Text="Cancel translation" runat="server" OnClick="btncanceltrans_Click"  />
    
 <asp:Button CssClass="btneditplug" ID="btnSaveSubjects" resourcekey="btnSaveSubjects" Text="Edit Plug" runat="server" OnClick="btnExitEditMode_Click"  />
    <asp:Button CssClass="btneditplug" ID="btncanceledit"  resourcekey="btncanceledit" Text="Cancel Plug" runat="server" OnClick="btncanceledit_Click"  />
        <asp:Button CssClass="btneditplug" ID="btntransplug" meta:resourcekey="btntransplug" Text="Help us with the translation of this Plugg" runat="server" OnClick="btntransplug_Click"  />
   
    </div>
<br />
<asp:Panel runat="server" ID="pnlRRT">
<dnn:TextEditor runat="server" id ="richrichtext"></dnn:TextEditor>
     <asp:Button ID="Button3" runat="server" Text="Save" OnClick="Button3_Click" /><asp:Button ID="Button4" runat="server" Text="Cancel" OnClick="Button4_Click" />
    </asp:Panel>
<asp:Panel ID="richtextbox" runat="server" >
<div  class='container'>
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

                    <div id='editor' >
                        Go ahead&hellip;
                    </div>
                    <br />

                </div>



            </div>
     <asp:Button ID="Button5" OnClientClick="getRichtext()" runat="server" Text="Save" OnClick="Button5_Click" /><asp:Button ID="Button6" runat="server" Text="Cancel" OnClick="Button6_Click" />
    </asp:Panel>
<%--<label  id="lbllabel" runat="server" resourcekey="lblExample" ></label>--%>
<asp:Panel runat="server" ID="pnllabel">
<asp:TextBox runat="server" ID="txtlabel" ></asp:TextBox>
    <asp:Button ID="Save" runat="server" Text="Save" OnClick="Save_Click" /><asp:Button ID="Cancel" runat="server" Text="Cancel" OnClick="Cancel_Click" />
    <asp:HiddenField ID="hdnlabel" runat="server" />
    </asp:Panel>


<asp:Panel runat="server" ID="pnlletex">
</asp:Panel>
            
                  <%--   <asp:Button ID="btnEditPlugg" runat="server" Text="Edit Plugg"  Visible="False" OnClick="btnEditPlugg_Click" />
    <asp:Button ID="btnExitEditMode" runat="server" Text="Exit Edit Mode"  Visible="False" OnClick="btnExitEditMode_Click" />--%>
                   <asp:HiddenField ID="hdn" Value="aa" runat="server" />
            

<table class="auto-style1" >
    <tr>
        <td><div id="divTitle" runat="server" class="dispalyplug"></div></td>
        <td><div id="divTransaltor" runat="server" class="dispalyplug"></div></td>

      
    </tr>
</table>

                   
  <asp:HiddenField ID="hdnrichtext" runat="server" />

   
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

