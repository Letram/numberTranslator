<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WebForm.aspx.cs" Inherits="src_WebForm" Culture="auto" UICulture="auto"%>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1.0"/>
    <title>WebForm</title>
    <link href="css/bootstrap.min.css" rel="stylesheet" type="text/css"/>
    <link href="//maxcdn.bootstrapcdn.com/font-awesome/4.1.0/css/font-awesome.min.css" rel="stylesheet">
    <link href="css/MyCSS.css" rel="stylesheet" />
    <script src="js/jquery-3.3.1.min.js"></script>
    <script src="js/tether.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
    <script src="js/responsivevoice.js"></script>
</head>
    <body>
        <div class="jumbotron">
            <h1 class="text-center"><%= Resources.Resource.translator %></h1>
            <p class="text-center"><%= Resources.Resource.description %></p>

        </div>
        <div class="container">
            <div class="row">
                <div class="col">
                    <form id="webForm" runat="server">
                        <div class="form-group">
                            <div class="input-group">
                                <div class="input-group-prepend"><span class="input-group-text"><%= Resources.Resource.writeNumber %></span></div><asp:TextBox ID="number" runat="server" CssClass="form-control"></asp:TextBox>
                            </div>
                            <asp:Button ID="convertButton" runat="server" CssClass="btn btn-primary btn-block" Text="Translate!" OnClick="convertButton_Click" />
                        </div>
                    </form>
                    <asp:Panel runat="server" CssClass="container" ID="tabs_panel"></asp:Panel>
                </div>
            </div>
        </div>
        <script src="js/myScripts.js"></script>
</body>
</html>
