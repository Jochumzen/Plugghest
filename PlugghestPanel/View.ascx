<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="View.ascx.cs" Inherits="Plugghest.Modules.PlugghestPanel.View" %>

Delete Course:&nbsp;&nbsp;
<asp:TextBox ID="tbDeleteCourseID" runat="server" Width="56px"></asp:TextBox>
&nbsp;
<asp:Button ID="btnDeleteCourse" runat="server" OnClick="btnDeleteCourse_Click" Text="Delete" />
<br />
<br />
Delete Tab (55 or 23,24 or 25-30):&nbsp;
<asp:TextBox ID="tbDeleteTabID" runat="server" Width="72px"></asp:TextBox>
&nbsp;&nbsp;&nbsp;
<asp:Button ID="btnDeleteTab" runat="server" Text="Delete" OnClick="btnDeleteTab_Click" />
<br />
<br />
<asp:Button ID="btnDeleteCourses" runat="server" Text="Delete all courses" />


