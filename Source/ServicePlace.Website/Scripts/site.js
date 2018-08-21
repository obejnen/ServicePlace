function toggleSidebar(selector) {
    $("#" + selector).toggle();
}

$(function () {
    $("#response-div").hide();
    $("#hide-form-button").hide();
});
function openForm() {
    $("#hide-form-button").show();
    $("#open-form-button").hide();
    $("#response-div").show();
    $("#response-div").addClass("w3-animate-top");
}

function hideForm() {
    $("#open-form-button").show();
    $("#hide-form-button").hide();
    $("#response-div").hide();
    $("#response-div").removeClass("w3-animate-top");
}

function addToOrderResponse(data) {
    $("#response-div").hide();
    $("#response-form")[0].reset();
    $("#order-responses").append(data);
}