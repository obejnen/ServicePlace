$(function () {
    $("#providers").hide();
    $("#order-responses").hide();
    $("#provider-responses").hide();
    $("#orders-button").addClass("w3-black");
});
function openContent(id) {
    $("#orders").hide();
    $("#providers").hide();
    $("#order-responses").hide();
    $("#provider-responses").hide();
    $(".w3-black").addClass("w3-white");
    $(".w3-black").removeClass("w3-black");
    $("#" + id).show();
    $("#" + id + "-button").addClass("w3-black");
};