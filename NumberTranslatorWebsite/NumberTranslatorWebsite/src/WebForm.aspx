<%@ Page Language="C#" AutoEventWireup="true" CodeFile="WebForm.aspx.cs" Inherits="src_WebForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title>WebForm</title>
    <link href="css/bootstrap.min.css" rel="stylesheet" type="text/css"/>

    <script src="js/jquery-3.3.1.min.js"></script>
    <script src="js/tether.min.js"></script>
    <script src="js/bootstrap.min.js"></script>
</head>
    <body>
        <div class="container">
            <div class="row">
                <div class="col-md-12">
                    <div class="jumbotron">
                        <h1><%= Resources.Resources.Traductor %></h1>
                        <p><%= Resources.Resources.Descripcion %></p>
                    </div>
                    <form id="webForm" runat="server">
                        <div class="form-group">
                            <div class="input-group">
                                <div class="input-group-prepend"><span class="input-group-text"><%= Resources.Resources.Introducir_numero %></span></div><asp:TextBox ID="number" runat="server" CssClass="form-control"></asp:TextBox>
                                <div class="input-group-append"><asp:Button ID="convertButton" runat="server" CssClass="btn btn-primary" Text="Traducir!" OnClick="convertButton_Click" /></div>
                            </div>
                        </div>
                    </form>
                    <asp:Panel runat="server" CssClass="container" ID="tabs_panel"></asp:Panel>
                    <div runat="server" class="container" id="tabs"></div>
                </div>
            </div>
        </div>
</body>
</html>
