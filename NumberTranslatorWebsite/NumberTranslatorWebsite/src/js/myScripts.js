$(document).ready(function () {
    $(".purpleContainer").on("click", function (event) {
        $(event.target).toggle(250);
    });
    $(".blueContainer").on("click", function (event) {
        $(event.target).toggle(250);
    });
});
