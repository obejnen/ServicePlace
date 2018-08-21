$(function () {
    $("#response-form").hide();
    $("#hide-form-button").hide();
});
function openForm() {
    $("#hide-form-button").show();
    $("#open-form-button").hide();
    $("#response-form").show();
    $("#response-form").addClass("w3-animate-top");
}

function hideForm() {
    $("#open-form-button").show();
    $("#hide-form-button").hide();
    $("#response-form").hide();
    $("#response-form").removeClass("w3-animate-top");
}