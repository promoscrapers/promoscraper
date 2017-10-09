<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="InsertProduct.aspx.cs" Inherits="InsertProduct" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h2>Insert Product</h2>
    <table>
        <tr>
            <td>&nbsp;</td>
             <td><asp:Label ID="lblConfirm" runat="server" Text="OK" Visible="false" Font-Bold="true" ForeColor="Red" /></td>
        </tr>
        <tr>
            <td>Amazon ASIN:</td>
             <td><asp:TextBox ID="txtAmazonASIN" runat="server" MaxLength="500" Width="369px" /></td>
        </tr>
        <tr> 
            <td>Product Name:</td>
             <td><asp:TextBox ID="txtProductName" runat="server" MaxLength="500" Width="371px" /></td>
        </tr>
         <tr>
            <td>Image URL:</td>
             <td><asp:TextBox ID="txtImageURL" runat="server" MaxLength="500" Width="369px" /></td>
        </tr>
         <tr>
            <td>Price:</td>
             <td><asp:TextBox ID="txtPrice" runat="server" MaxLength="500" Width="100px" /></td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td><asp:Button ID="btnSubmit" runat="server" Text="SUBMIT" Height="36px" OnClick="btnSubmit_Click" Width="221px" /></td>
        </tr>

    </table>
</asp:Content>

