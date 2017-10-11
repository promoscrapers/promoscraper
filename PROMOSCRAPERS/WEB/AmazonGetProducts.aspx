<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AmazonGetProducts.aspx.cs" Inherits="AmazonGetProducts" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="btnGetProducts" runat="server" Text="GEt Products" OnClick="btnGetProducts_Click" />
    </div>
        <div>
            <asp:Button ID="btngetCategories" runat="server" Text="Get Categories" OnClick="btngetCategories_Click" />
        </div>
      
    </form>
</body>
</html>
