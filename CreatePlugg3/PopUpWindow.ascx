<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="PopUpWindow.ascx.cs" Inherits="Christoc.Modules.CreatePlugg3.PopUpWindow" %>





<div>

    &nbsp; &nbsp;<asp:Button ID="btnOk" runat="server"  Text="Ok" />
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