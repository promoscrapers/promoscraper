<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MyProductPage.aspx.cs" Inherits="MyProductPage" %>

<!DOCTYPE html>



<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
<script type="text/javascript">
    var pageIndex = 1;
    var pageCount;
    $(window).scroll(function () {
        if ($(window).scrollTop() == $(document).height() - $(window).height()) {
            GetRecords();
        }
    });
    function GetRecords() {
        pageIndex++;
        if (pageIndex == 2 || pageIndex <= pageCount) {
            //$("#loader").show();
            $.ajax({
                type: "POST",
                url: "MyProductPage.aspx/GetProducts",
                data: '{pageIndex: ' + pageIndex + '}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: OnSuccess,
                failure: function (response) {
                    alert(response.d);
                },
                error: function (response) {
                    alert(response.d);
                }
            });
        }
    }
    function OnSuccess(response) {
        var xmlDoc = $.parseXML(response.d);
        var xml = $(xmlDoc);
       
        pageCount = parseInt(xml.find("PageCount").eq(0).find("PageCount").text());

        var products = xml.find("Table");
        products.each(function () {
           
            var iDiv = document.createElement('div');
            iDiv.innerHTML = "<div style=\"height:200px;width:200px;border-color:black\"><img src=\"" + products.find("ImageURL").text() + "\" alt=\"No Image Available\" style=\"display: inline;\"></div>";
            document.getElementsByTagName('body')[0].appendChild(iDiv);
        });
    }
</script>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div >
    
    </div>
    </form>
</body>
</html>
