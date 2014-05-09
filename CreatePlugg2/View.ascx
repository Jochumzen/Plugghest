<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="View.ascx.cs" Inherits="Christoc.Modules.CreatePlugg2.View" %>
<%@ Register TagPrefix="dnn" TagName="TextEditor" Src="~/controls/TextEditor.ascx" %>
<%@ Register TagPrefix="dnn" TagName="label" Src="~/controls/LabelControl.ascx" %>
<asp:HiddenField ID="hdRichTextHtml" runat="server" />
<asp:HiddenField ID="hdStaticListHTML" runat="server" />


<html lang="us">
<head>


    	<link href="Script/css/ui-lightness/jquery-ui-1.10.4.custom.css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="Script/css/ui-lightness/style.css" media="screen" />
	<script src="Script/js/jquery-1.10.2.js"></script>
	<script src="Script/js/jquery-ui-1.10.4.custom.js"></script>

<%--    <link href="http://dnndev.me/Script/css/ui-lightness/jquery-ui-1.10.4.custom.css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="http://dnndev.me/Script/css/ui-lightness/style.css" media="screen" />

    <script src="http://dnndev.me/Script/js/jquery-1.10.2.js"></script>
    <script src="http://dnndev.me/Script/js/jquery-ui-1.10.4.custom.js"></script>--%>


      <link href="/DesktopModules/CreatePlugg2/Script/js/jqtree.css" rel="stylesheet" />
<script src="/DesktopModules/CreatePlugg2/Script/js/tree.jquery.js"></script>
<%--    <link href="http://dnndev.me/Script/js/jqtree.css" rel="stylesheet" />
    <script src="http://dnndev.me/Script/js/tree.jquery.js"></script>--%>



    <script>
        $(function () {
            $("#tabs").tabs().addClass("ui-tabs-vertical ui-helper-clearfix");
            $("#tabs li").removeClass("ui-corner-top").addClass("ui-corner-left");
        });
    </script>
    <script type="text/javascript">

        $(document).ready(function () {
            $("#divStaticList").html($("#" + '<%=hdStaticListHTML.ClientID%>').val());
            //$("#tabs").hide();
            $('#divList .sortable-list').sortable({
                connectWith: '#divList .sortable-list',
                placeholder: 'my-placeholder',
                update: function () {
                    setFristList();

                }
            });



        });
        function setFristList() {

            $("#divDynamicList .del-spn").show();

            $("#divStaticList").html($("#" + '<%=hdStaticListHTML.ClientID%>').val());

            $('#divList .sortable-list').sortable({
                connectWith: '#divList .sortable-list',
                placeholder: 'my-placeholder',
                update: function () {
                    setFristList();

                }
            });
        };

        function RemoveCon(Control) {

            $($($(Control)).parent()).remove();
        };

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
            alert(code);
            if ($(Control).parent().find("iframe").length > 0) {
                $(Control).parent().find("iframe").remove();
            }
            $($($(Control).parent()).find("h3")).after("  <iframe width='420' height='345'src='http://www.youtube.com/embed/" + code + "'></iframe>");
            //$.ajax({
            //    url: "http://gdata.youtube.com/feeds/api/videos/" + code + "?v=2&alt=json",
            //    dataType: "jsonp",
            //    success: function (data) {
            //        alert(data);
            //    }
            //});
            $.getJSON('http://gdata.youtube.com/feeds/api/videos/' + code + '?v=2&alt=jsonc', function (data, status, xhr) {

                alert(data.data.title);
                alert(data.data.duration);
                $($($(Control).parent()).find("h2")).html("title :" + data.data.title);
                $($($(Control).parent()).find("h3")).html("duration :" + data.data.duration + " Seconds");
                // data contains the JSON-Object below
            });
        };
    </script>
    <script type="text/javascript">

        $(document).ready(function () {
            $('#tree2').tree({
                data: eval($("#" + '<%=hdnTreeData.ClientID%>').attr('value')),
                //data:data,
                //dragAndDrop: true,
                selectable: true,
                autoEscape: false,
                autoOpen: true
            });
        });





    </script>

    <style>
        .ui-tabs-vertical
        {
            width: 55em;
        }

            .ui-tabs-vertical .ui-tabs-nav
            {
                padding: .2em .1em .2em .2em;
                float: left;
                width: 12em;
            }

                .ui-tabs-vertical .ui-tabs-nav li
                {
                    clear: left;
                    width: 100%;
                    border-bottom-width: 1px !important;
                    border-right-width: 0 !important;
                    margin: 0 -1px .2em 0;
                }

                    .ui-tabs-vertical .ui-tabs-nav li a
                    {
                        display: block;
                    }

                    .ui-tabs-vertical .ui-tabs-nav li.ui-tabs-active
                    {
                        padding-bottom: 0;
                        padding-right: .1em;
                        border-right-width: 1px;
                        border-right-width: 1px;
                    }

            .ui-tabs-vertical .ui-tabs-panel
            {
                padding: 1em;
                float: right;
                width: 40em;
            }
    </style>
    <style>
        .del-spn
        {
            background-color: #FF0000;
            border-radius: 36px;
            float: right;
            width: 19px;
            display: none;
            cursor: pointer;
        }

        #divList, #tabs
        {
            clear: both;
        }

        .my-placeholder
        {
            background-color: #BFB;
            border: 1px dashed #666;
            height: 58px;
            margin-bottom: 5px;
        }
    </style>




      <link href="/DesktopModules/CreatePlugg2/Script/external/prettify.css" rel="stylesheet" />
    <link href="http://netdna.bootstrapcdn.com/twitter-bootstrap/2.3.1/css/bootstrap-combined.no-icons.min.css" rel="stylesheet">
    <link href="http://netdna.bootstrapcdn.com/twitter-bootstrap/2.3.1/css/bootstrap-responsive.min.css" rel="stylesheet">
		<link href="http://netdna.bootstrapcdn.com/font-awesome/3.0.2/css/font-awesome.css" rel="stylesheet">
   
          <script src="/DesktopModules/CreatePlugg2/Script/external/jquery.hotkeys.js"></script>
    <script src="http://netdna.bootstrapcdn.com/twitter-bootstrap/2.3.1/js/bootstrap.min.js"></script>
		<link href="/DesktopModules/CreatePlugg2/Script/index.css" rel="stylesheet" />
     <script src="/DesktopModules/CreatePlugg2/Script/bootstrap-wysiwyg.js"></script>

<%--    <link href="http://dnndev.me/Script/external/prettify.css" rel="stylesheet" />
    <link href="http://netdna.bootstrapcdn.com/twitter-bootstrap/2.3.1/css/bootstrap-combined.no-icons.min.css" rel="stylesheet">
    <link href="http://netdna.bootstrapcdn.com/twitter-bootstrap/2.3.1/css/bootstrap-responsive.min.css" rel="stylesheet">
    <link href="http://netdna.bootstrapcdn.com/font-awesome/3.0.2/css/font-awesome.css" rel="stylesheet">

    <script src="http://dnndev.me/Script/external/jquery.hotkeys.js"></script>
    <script src="http://netdna.bootstrapcdn.com/twitter-bootstrap/2.3.1/js/bootstrap.min.js"></script>
    <link href="http://dnndev.me/Script/index.css" rel="stylesheet" />
    <script src="http://dnndev.me/Script/bootstrap-wysiwyg.js"></script>--%>

</head>
<body>
    <div id="divList">

        <div class="column left first" id="divStaticList">

            <ul class="sortable-list">
                <ul class='sortable-list ui-sortable'>

                    <li style='' class='sortable-item' id='Li1'>Sortable item C<span class='del-spn' onclick='RemoveCon(this)'>x</span></li>
                    <li id='B' class='sortable-item' style=''>Sortable item B<span class='del-spn' onclick='RemoveCon(this)'>x</span></li>
                    <li id='E' class='sortable-item' style=''>Sortable item E<span class='del-spn' onclick='RemoveCon(this)'>x</span></li>
                    <li id='k' class='sortable-item' style=''>Sortable item k<span class='del-spn' onclick='RemoveCon(this)'>x</span></li>
                    <li id='Li2' class='sortable-item' style=''>Sortable item D<span class='del-spn' onclick='RemoveCon(this)'>x</span></li>
                    <li class='sortable-item' id='A' style=''>Sortable item A<span class='del-spn' onclick='RemoveCon(this)'>x</span></li>

                </ul>
            </ul>

        </div>

        <div class="column left" id="divDynamicList">

            <ul class="sortable-list">
            </ul>

        </div>



    </div>

    <div id="tabs">
        <ul>
            <li><a href="#Title">Title</a></li>
            <li><a href="#Description">Description</a></li>
            <li><a href="#tabs-1">Rich Rich Text</a></li>
            <li><a href="#tabs-2">Rich Text</a></li>
            <li><a href="#tabs-3">You Tube</a></li>
            <li><a href="#tabs-4">Label</a></li>
            <li><a href="#tabs-5">Latex</a></li>
            <li><a href="#tabs-6">Subject</a></li>
            <li><a href="#tabs-7">Advanced</a></li>
        </ul>

        <div id="Title">
            <dnn:label HelpKey="Title" helptext="Title" text="Title" id="labTitle" runat="server" />
            <asp:TextBox ID="txtTitle" runat="server" />
        </div>
        <div id="Description">
            <dnn:label HelpKey="Description" helptext="Description" text="Description" id="labDescription" runat="server" />
            <asp:TextBox ID="txtDescription" runat="server" />
        </div>



        <div id="tabs-1">
            <dnn:TextEditor ID="txtHtmlText" runat="server" Width="100%" Height="400px" />
        </div>
        <div id="tabs-2">
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
        </div>
        <div id="tabs-3">
            <dnn:label HelpKey="help" helptext="Enter the you tube URL Example: http://www.youtube.com/watch?v=HQODPOTikic" text="Youtube" id="lblYouTube" runat="server" />
            <asp:TextBox ID="txtYouTube" runat="server" />

            <input type="button" id="btnGetYoutubeVideo" value="Get Video" onclick="CheckURL(this);" />
            <h2></h2>
            <h3></h3>

        </div>
        <div id="tabs-4">
            <input type="text" />
        </div>
        <div id="tabs-5">
            <textarea cols="20" rows="2"></textarea>
        </div>
        <div id="tabs-6">
            <div class="tree">
                <div id="tree2"></div>
            </div>
            <asp:HiddenField ID="hdnTreeData" runat="server" Value="" />
        </div>
        <div id="tabs-7">
            <dnn:label id="lblEditPlug" runat="server" controlname="rdEditPlug" HelpKey="helpEditPlug" helptext="Select one option ." text="Who can edit plugg?" />
            <asp:RadioButtonList ID="rdEditPlug" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                <asp:ListItem resourcekey="Asynchronous" Value="Any registered user" Selected="True" />
                <asp:ListItem resourcekey="Synchronous" Value="Only me" />
            </asp:RadioButtonList>
        </div>
    </div>


</body>
</html>




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
