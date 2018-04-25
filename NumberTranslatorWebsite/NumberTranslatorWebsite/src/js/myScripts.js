$(document).ready(function () {
    $(".hideOnStart").hide();
    $("#purpleContainerIcon").on("click", function () {
        $("#purpleContainer").toggle(250);
    });
    $("#blueContainerIcon").on("click", function () {
        $("#blueContainer").toggle(250);
    });
});
