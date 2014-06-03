<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="View.ascx.cs" Inherits="Plugghest.Modules.CreatePlugg3.View" %>
<%@ Register TagPrefix="dnn" TagName="label" Src="~/controls/LabelControl.ascx" %>
<%--<a href="javascript:dnnModal.show('<%#DotNetNuke.Common.Globals.NavigateURL(23, "", new string[] { "edit=1" , "language=1"  })%>',false,550,950,true)" class="">Create Plugg</a>--%>
<%--<a href="javascript:dnnModal.show('CreatePlugg.aspx?popUp=true',false,550,950,false)" class="">Create Plugg</a>--%>
<script src="http://dnndev.me/Script/bPopup.js"></script>
<script>
    $(document).ready(function () {
      $('#element_to_pop').hide();
    });
       
  $(function ($) {      
       //  DOM Ready
        $(function () {
            // Binding a click event
            // From jQuery v.1.7.0 use .on() instead of .bind()
            $('#my-button').bind('click', function (e) {              
                e.preventDefault();           
                    $('#element_to_pop').bPopup({
                        appendTo: 'form'
                                           , closeClass: 'ui-dialog-titlebar-close'
                                           , speed: 650
                           , transition: 'slideIn'
                                   , zIndex: 2
                                   , modalClose: true
                    });
            });

        });

    })(jQuery);
</script>
<style>
    .dnnLabel
    {
        width: 128px;
    }
    #element_to_pop, .bMulti {
    background-color: #FFF;
    border-radius: 10px 10px 10px 10px;
    box-shadow: 0 0 25px 5px #999;
    color: #111;
    display: none;
    min-width: 450px;
    min-height: 250px;
    padding: 25px;
}

#element_to_pop .logo {
    color: #2B91AF;
    font: bold 325% 'Petrona',sans;
}

.button.b-close, .button.bClose {
    border-radius: 7px 7px 7px 7px;
    box-shadow: none;
    font: bold 131% sans-serif;
    padding: 0 6px 2px;
    position: absolute;
    right: -7px;
    top: -7px;
}

.button {
    background-color: #2B91AF;
    border-radius: 10px;
    box-shadow: 0 2px 3px rgba(0, 0, 0, 0.3);
    color: #FFF;
    cursor: pointer;
    display: inline-block;
    padding: 10px 20px;
    text-align: center;
    text-decoration: none;
}
</style>
<%-- <button id="my-button"></button>--%>
<a href="#" id="my-button" ><asp:Label ID="lblCreatePlugg" runat="server" ></asp:Label> </a>


            <!-- Element to pop up -->
          

<div id="element_to_pop" class="ui-dialog ui-widget ui-widget-content ui-corner-all ui-front dnnFormPopup ui-draggable ui-resizable"
    style="top: 120px !important">
    <div class="ui-dialog-titlebar ui-widget-header ui-corner-all ui-helper-clearfix">
        <span id="ui-id-1" class="ui-dialog-title"><asp:Label ID="lblCreatePlugg1" runat="server" ></asp:Label></span>
        <div class="dnnModalCtrl">
            <a class="dnnToggleMax" href="#">
                <span>Max</span>

            </a>
            <button class="ui-dialog-titlebar-close"></button>
        </div>

    </div>
    &nbsp; &nbsp;<br />
        <table class="auto-style1">
            <tr>
                <td> <dnn:label ID="lblTitle" runat="server" /></td>
                <td>
                    <asp:TextBox ID="txtTitle" runat="server" ToolTip="Please enter title"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                  </td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td><dnn:label ID="lblDescrip" runat="server" /> </td>
                <td>
                    <asp:TextBox ID="txtDescription" runat="server" ToolTip="Please enter Description" TextMode="MultiLine"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td><dnn:label ID="lblWhoCanEdit" runat="server" /> </td>
                <td>
                    <asp:RadioButtonList ID="rdbtnWhoCanEdit" runat="server" RepeatDirection="Horizontal">
                       
                    </asp:RadioButtonList>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    <asp:Button ID="btnOk" runat="server"  Text="Ok"  OnClick="btnOk_Click" />
                </td>
            </tr>
        </table>
    


</div>


