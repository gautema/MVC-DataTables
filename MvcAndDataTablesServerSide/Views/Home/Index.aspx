<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Home Page
</asp:Content>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    <script src="../../Content/js/jquery.js" type="text/javascript"></script>
    <script src="../../Content/js/jquery.dataTables.min.js" type="text/javascript"></script>
    <script type="text/javascript">
       $(document).ready(function() {
       $('#persons').dataTable({
               "bProcessing": true,
               "bServerSide": true,
               "sAjaxSource": "/Home/GetPersons",
               "aoColumns": [
                { "sTitle": "Name" },
                { "sTitle": "Number" },
                { "sTitle": "Date"}]
                               
           });
       });

    </script>
     <style type="text/css" title="currentStyle"> 
            @import "/content/css/demo_page.css";
            @import "/content/css/demo_table.css";
    </style> 
    <h2>Persons</h2>
    <table id="persons" class="display">
    </table>

</asp:Content>
