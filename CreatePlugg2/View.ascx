<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="View.ascx.cs" Inherits="Christoc.Modules.CreatePlugg2.View" Strict="false" WarningLevel="0"%>
<%@ Register TagPrefix="dnn" TagName="TextEditor" Src="~/controls/TextEditor.ascx" %>
<%@ Register TagPrefix="dnn" TagName="label" Src="~/controls/LabelControl.ascx" %>
<asp:HiddenField ID="hdStaticListHTML" runat="server" />
<asp:HiddenField ID="hdcmpData" runat="server" />


<html lang="us">
<head>


    <link href="/DesktopModules/CreatePlugg2/Script/css/ui-lightness/jquery-ui-1.10.4.custom.css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="/DesktopModules/CreatePlugg2/Script/css/ui-lightness/style.css" media="screen" />
	<script src="/DesktopModules/CreatePlugg2/Script/js/jquery-1.10.2.js"></script>
	<script src="/DesktopModules/CreatePlugg2/Script/js/jquery-ui-1.10.4.custom.js"></script>

    <%-- ><link href="http://dnndev.me/Script/css/ui-lightness/jquery-ui-1.10.4.custom.css" rel="stylesheet">
    <link rel="stylesheet" type="text/css" href="http://dnndev.me/Script/css/ui-lightness/style.css" media="screen" />

    <script src="http://dnndev.me/Script/js/jquery-1.10.2.js"></script>
    <script src="http://dnndev.me/Script/js/jquery-ui-1.10.4.custom.js"></script> --%>


    <link href="/DesktopModules/CreatePlugg2/Script/js/jqtree.css" rel="stylesheet" />
    <script src="/DesktopModules/CreatePlugg2/Script/js/tree.jquery.js"></script>
    <link href="http://dnndev.me/Script/js/jqtree.css" rel="stylesheet" />
    <script src="http://dnndev.me/Script/js/tree.jquery.js"></script>



    <script>
        var liToRemove = "";
        var divToRemove = "";
        $(function () {
            $("#tabs").tabs().addClass("ui-tabs-vertical ui-helper-clearfix");
            $("#tabs li").removeClass("ui-corner-top").addClass("ui-corner-left");
        });

        function btnStepOneFunc() {
            var tabs = $("#tabs").tabs();
            var ul = tabs.find("ul")[0];
            var i = 1;
            $("#divDynamicList").find('li').each(function () {
                var IId = $(this).attr('id');

                var TabPosition = $("#tabs").find(".ui-tabs-nav li:eq("+i+")");
                TabPosition.after("<li id = 'li" + IId + i + "' ><a href='#" + IId + i + "'>" + IId + "</a></li>");
                //$("<li id = 'li"+IId + i+"' ><a href='#" + IId + i + "'>" + IId  + "</a></li>").appendTo(ul);
                $("<div class='toAdd' id='" + IId + i + "'>" + " <iframe width='100%' height='100%'src='" + IId + ".aspx'></iframe>" + "</div>").appendTo(tabs);

                liToRemove += "li" + IId + i + "#$%";
                divToRemove += IId + i+"#$%";


                i += 1;
            });
            tabs.tabs("refresh");
            tabs.addClass("ui-tabs-vertical ui-helper-clearfix");
            $("#tabs li").removeClass("ui-corner-top").addClass("ui-corner-left");
            $('#divStep2').show();
            $('#divList').hide();
        };


        function PreviousButtonFunc() {

            $.each(liToRemove.split("#$%"), function (index, value) {
                $("#"+value).remove();
            });
            $.each(divToRemove.split("#$%"), function (index, value) {
                $("#"+value).remove();
            });
            //tabs.tabs("refresh");
            //tabs.addClass("ui-tabs-vertical ui-helper-clearfix");
            //$("#tabs li").removeClass("ui-corner-top").addClass("ui-corner-left");
            liToRemove = "";
            divToRemove = "";

            $('#divStep2').hide();
            $('#divList').show();
        };

        function btnSaveFunc() {
  var finalStr = "";
            $('.toAdd').each(function () {

                var id = $(this).attr("id").slice(0,-1);

                var body = $(this).find('iframe').contents().find('body');
                var value = "";
              
                switch (id) {
                    case ("RichRichText"):
                        value = $(body).find('iframe').contents().find('body').html();
                        finalStr += "RichRichText" + "$$$&$$$" + value;
                        break;
                    case ("RichText"):
                        value = $($(body).find('#editor')).html();
                        finalStr += "RichText" + "$$$&$$$" + value;
                        break;
                    case ("Label"):
                        value = $(body).find('input[type=text]').val();
                        finalStr += "Label" + "$$$&$$$" + value;
                        break;
                    case ("Latex"):
                        value = $(body).find('textarea').val();
                        finalStr += "Latex" + "$$$&$$$" + value;
                        break;
                    case ("YouTube"):
                        value += $($(body).find('.title')).find('span').html()+ "&&&$$&&&";
                        value += $($(body).find('.duration')).find('span').html()+ "&&&$$&&&";
                        value += $($(body).find('.YouTubeCode')).find('span').html()+ "&&&$$&&&";
                        value += $($(body).find('.Author')).find('span').html() + "&&&$$&&&";
                        value += $($(body).find('.YouTubeCreatedOn')).find('span').html() + "&&&$$&&&";
                        value += $($(body).find('.YouTubeComment')).find('span').html();
             
                        finalStr += "YouTube" + "$$$&$$$" + value;
                        break;
                }
                finalStr += "$#%#$%";
                $("#" + '<%=hdcmpData.ClientID%>').val(finalStr);
  

            });

        };
    </script>
    <script type="text/javascript">

        $(document).ready(function () {
            $("#divStaticList").html($("#" + '<%=hdStaticListHTML.ClientID%>').val());


            $('#divStep2').hide();
        
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
        <div style="clear:both">
            <br />
        </div>
        <input style="float:right" type="button" onclick="btnStepOneFunc();" value="Continue" />

    </div>
    <div id="divStep2">
    <div id="tabs">
        <ul>
            <li><a href="#Title">Title</a></li>
            <li><a id="AddHeader" href="#Description">Description</a></li>

            <li><a href="#Subject">Subject</a></li>
            <li><a href="#Advanced">Advanced</a></li>
        </ul>

        <div id="Title">
            <dnn:label HelpKey="Title" helptext="Title" text="Title" id="labTitle" runat="server" />
            <asp:TextBox ID="txtTitle" runat="server" />
        </div>
        <div id="Description">
            <dnn:label HelpKey="Description" helptext="Description" text="Description" id="labDescription" runat="server" />
            <asp:TextBox ID="txtDescription" runat="server" />
        </div>



   
  

        <div id="Subject">
            <div class="tree">
                <div id="tree2"></div>
            </div>
            <asp:HiddenField ID="hdnTreeData" runat="server" Value="" />
        </div>
        <div id="Advanced">
            <dnn:label id="lblEditPlug" runat="server" controlname="rdEditPlug" HelpKey="helpEditPlug" helptext="Select one option ." text="Who can edit plugg?" />
            <asp:RadioButtonList ID="rdEditPlug" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                <asp:ListItem resourcekey="Asynchronous" Value="Anyone" Text="Anyone" Selected="True" />
                <asp:ListItem resourcekey="Synchronous" Value="OnlyMe" Text="Only Me" />
            </asp:RadioButtonList>
        </div>
    </div>
         <input type="button" onclick="PreviousButtonFunc();" value="Go to Step 1" />
        <asp:Button ID="btnSave" OnClick="btnSaveTitle_Click"  OnClientClick="btnSaveFunc();" runat="server" Text="Save"  />
        </div>
</body>
</html>



