<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="YouTube.aspx.cs" Inherits="Christoc.Modules.CreatePlugg2.YouTube" %>

    <%@ Register TagPrefix="dnn" TagName="label" Src="~/controls/LabelControl.ascx" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">

    <script src="/DesktopModules/CreatePlugg2/Script/js/jquery-1.10.2.js"></script>
    <script src="/DesktopModules/CreatePlugg2/Script/js/jquery-ui-1.10.4.custom.js"></script>
    <style>
        body > #feedHeaderContainer
        {
            display: none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">



          <dnn:label HelpKey="help" helptext="Enter the you tube URL Example: http://www.youtube.com/watch?v=HQODPOTikic" text="Youtube" id="lblYouTube" runat="server" />
        <asp:TextBox ID="txtYouTube" runat="server" />

        <input type="button" id="btnGetYoutubeVideo" value="Get Video" onclick="CheckURL(this);" />
        <h2 class="title"></h2>
        <h3 class="duration"></h3>
        <h6 class="YouTubeCode"></h6>
        <h7 class="Author"></h7>
        <h7 class="YouTubeCreatedOn"></h7>
        <h7 class="YouTubeComment"></h7>
        

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

                    $($($(Control).parent()).find(".title")).html("title :<span>" + data.data.title+"</span><br />");
                    $($($(Control).parent()).find(".duration")).html("duration :<span>" + data.data.duration + "</span> Seconds<br />");
                    $($($(Control).parent()).find(".YouTubeCode")).html("YouTube Code :<span>" + data.data.id + "</span><br />");
                    $($($(Control).parent()).find(".Author")).html("Author :<span>" + data.data.uploader + "</span><br />");
                    $($($(Control).parent()).find(".YouTubeCreatedOn")).html("YouTube CreatedOn :<span>" + data.data.uploaded + "</span><br />");
                    $($($(Control).parent()).find(".YouTubeComment")).html("YouTube Author Comment :<span>" + data.data.description + "</span><br />");
                    // data contains the JSON-Object below
                });



            };
        </script>
    </form>
</body>
</html>
