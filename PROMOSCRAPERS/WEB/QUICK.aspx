<%@ Page Language="C#" AutoEventWireup="true" CodeFile="QUICK.aspx.cs" Inherits="QUICK" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table>
        <tr><td><asp:TextBox ID="txtbb" TextMode="MultiLine" runat="server" Height="278px" Width="556px"></asp:TextBox></td></tr>
        <tr><td><asp:Button ID="btnbb" runat="server" Text="submit" Height="91px" OnClick="btnbb_Click" Width="395px" /></td></tr>
    </table>

        <asp:Table ID="tbloutput" runat="server" />
    </div>
    </form>
</body>
</html>
