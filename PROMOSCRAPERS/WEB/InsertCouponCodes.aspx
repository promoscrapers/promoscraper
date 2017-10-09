<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="InsertCouponCodes.aspx.cs" Inherits="InsertCouponCodes" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <h2>Insert Coupon Codes</h2>
    <table>
         <tr>
            <td>&nbsp;</td>
             <td><asp:Label ID="lblConfirm" runat="server" Text="OK" Visible="false" Font-Bold="true" ForeColor="Red" /></td>
        </tr>
        <tr>
            <td>Select Product:</td>
            <td><asp:DropDownList ID="cboProducts" Width="530px" runat="server" Height="16px" /></td>
        </tr>

        <tr>
            <td valign="top">Enter Keywords and %:</td>
            <td>
                <table>
                    <tr><td>
                    <asp:TextBox ID="txtKeyword1" runat="server"  Width="434px" />

                           </td>
                    <td><asp:TextBox ID="txtPercentage1" TextMode="Number" runat="server"  Width="77px" />

                    </td></tr>


                      <tr><td>
                    <asp:TextBox ID="txtKeyword2" runat="server"  Width="434px" />

                           </td>
                    <td><asp:TextBox ID="txtPercentage2" TextMode="Number" runat="server"  Width="77px" />

                    </td></tr>


                      <tr><td>
                    <asp:TextBox ID="txtKeyword3" runat="server" Width="434px" />

                           </td>
                    <td><asp:TextBox ID="txtPercentage3"  TextMode="Number" runat="server"  Width="77px" />

                    </td></tr>


                      <tr><td>
                    <asp:TextBox ID="txtKeyword4" runat="server" Width="434px" />

                           </td>
                    <td><asp:TextBox ID="txtPercentage4"  TextMode="Number" runat="server" Width="77px" />

                    </td></tr>


                      <tr><td>
                    <asp:TextBox ID="txtKeyword5" runat="server"  Width="434px" />

                           </td>
                    <td><asp:TextBox ID="txtPercentage5" TextMode="Number"  runat="server"  Width="77px" />

                    </td></tr>
                </table>


            </td>
        </tr>


        <tr>
            <td valign="top">Enter Coupon Codes:</td>
            <td><asp:TextBox ID="txtCouponCodes" runat="server" TextMode="MultiLine" Height="298px" Width="523px" /></td>
        </tr>

         <tr>
            <td>&nbsp;</td>
              <td><asp:Button ID="btnSubmit" runat="server" Text="SUBMIT" Height="36px" OnClick="btnSubmit_Click" Width="528px" /></td>
        </tr>
    </table>

</asp:Content>

