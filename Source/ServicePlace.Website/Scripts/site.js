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
}

function hideForm() {
    $("#open-form-button").show();
    $("#hide-form-button").hide();
    $("#response-div").hide();
}

function addToOrderResponse(data) {
    hideForm();
    $("#response-form")[0].reset();
    $("#order-responses").append(data);
}

function addToProviderResponse(data) {
    hideForm();
    $("#response-form")[0].reset();
    $("#provider-responses").append(data);
}

function removeFromProviderResponses(id) {
    $("#provider-response-" + id).remove();
}

function removeFromOrderResponses(id) {
    $("#order-response-" + id).remove();
}

function closeNotification(id) {
    $("#" + id).hide();
}

function setLang(lang) {
    $("#lang-field").val(lang);
    $("#lang-form").submit();
}