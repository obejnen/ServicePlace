$(function () {
    $("#providers").hide();
    $("#order-responses").hide();
    $("#provider-responses").hide();
});
function openContent(id) {
    $("#orders").hide();
    $("#providers").hide();
    $("#order-responses").hide();
    $("#provider-responses").hide();
    $("#" + id).show();
};