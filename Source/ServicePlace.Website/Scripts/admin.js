$(function () {
    $("#orders").hide();
    $("#providers").hide();
    $("#order-responses").hide();
    $("#provider-responses").hide();
    $("#order-categories").hide();
    $("#provider-categories").hide();
    $("#users-button").addClass("w3-black");
});
function openContent(id) {
    $("#users").hide();
    $(".w3-black").addClass("w3-white");
    $(".w3-black").removeClass("w3-black");
    $("#orders").hide();
    $("#providers").hide();
    $("#order-responses").hide();
    $("#provider-responses").hide();
    $("#order-categories").hide();
    $("#provider-categories").hide();
    $("#" + id).show();
    $("#" + id + "-button").addClass("w3-black");
};