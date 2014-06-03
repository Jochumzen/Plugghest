<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CreatePlugg.aspx.cs" Inherits="Christoc.Modules.CreatePlugg3.CreatePlugg" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1
        {
            width: 100%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    &nbsp; &nbsp;<asp:Button ID="btnOk" runat="server" OnClick="btnOk_Click" Text="Ok" />
        <br />
        <table class="auto-style1">
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td><label id="lblTitle" runat="server" ></label> </td>
                <td>
                    <asp:TextBox ID="txtTitle" runat="server" ToolTip="Please enter title"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>
                    <asp:TextBox ID="txtTitle0" runat="server" ToolTip="Please enter title"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>
                    <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True">a</asp:ListItem>
                        <asp:ListItem>b</asp:ListItem>
                        <asp:ListItem>c</asp:ListItem>
                    </asp:RadioButtonList>
                </td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
