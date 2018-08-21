$(function () {
    $("#orders").hide();
    $("#providers").hide();
    $("#order-responses").hide();
    $("#provider-responses").hide();
    $("#order-categories").hide();
    $("#provider-categories").hide();
});
function openContent(id) {
    $("#users").hide();
    $("#orders").hide();
    $("#providers").hide();
    $("#order-responses").hide();
    $("#provider-responses").hide();
    $("#order-categories").hide();
    $("#provider-categories").hide();
    $("#" + id).show();
};