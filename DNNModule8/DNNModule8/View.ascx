<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="View.ascx.cs" Inherits="Plugghest.Modules.DNNModule8.View" %>


Display Information
<asp:Label ID="txttitle" runat="server"></asp:Label>




<asp:Repeater ID="rptItemList" runat="server" OnItemDataBound="rptItemListOnItemDataBound" OnItemCommand="rptItemListOnItemCommand">
    <HeaderTemplate>
        <ul class="tm_tl">
    </HeaderTemplate>

    <ItemTemplate>
        <li class="tm_t">
            <h3>
                <asp:Label ID="lblitemName" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ItemName").ToString() %>' />
            </h3>
            <asp:Label ID="lblItemDescription" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ItemDescription").ToString() %>' CssClass="tm_td" />

            <asp:Panel ID="pnlAdmin" runat="server" Visible="false">
                <asp:HyperLink ID="lnkEdit" runat="server" ResourceKey="EditItem.Text" Visible="false" Enabled="false" />
                <asp:LinkButton ID="lnkDelete" runat="server" ResourceKey="DeleteItem.Text" Visible="false" Enabled="false" CommandName="Delete" />
            </asp:Panel>
        </li>
    </ItemTemplate>
    <FooterTemplate>
        </ul>
    </FooterTemplate>
</asp:Repeater>


First Number <asp:TextBox ID="txtfirstnumber" runat="server"></asp:TextBox><br />
First Number <asp:TextBox ID="txtsecondnumber" runat="server"></asp:TextBox><br />
Sum <asp:TextBox ID="txtresult" runat="server"></asp:TextBox><br />
<asp:Button ID="btntest" runat="server" Text="Get Sum" OnClick="btntest_Click" />


<div>
    <table>
        <tr>
            <td>
                <asp:Label ID="Label1" runat="server" Text="Parent Tab:" />
            </td>
            <td>
                <asp:DropDownList ID="ParentsDropDownList" runat="server" DataTextField="TabName" DataValueField="TabID" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label2" runat="server" Text="Tab Name:"/>
            </td>
            <td>
                <asp:TextBox ID="txtTabName" runat="server"/>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label3" runat="server" Text="Tab Title:"/>
            </td>
            <td>
                <asp:TextBox ID="txtTabTitle" runat="server"/>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label4" runat="server" Text="Description:"/>
            </td>
            <td>
                <asp:TextBox ID="txtTabDesc" runat="server"/>
            </td>
        </tr>
        <tr>
            <td>
            <asp:Label ID="Label5" runat="server" Text="Key Words:"/>
            </td>
            <td>
                <asp:TextBox ID="txtTabKeyWords" runat="server"/>
                
            </td>
        </tr>
        <tr>
            <asp:Button runat="server" OnClick="btnAddPage_Click" Text="create" />
        </tr>
    </table>
    <br />

</div>

