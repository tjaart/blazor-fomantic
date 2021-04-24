function showSimpleToast(message) {
    $('body')
        .toast({
            message: message
        });
}
function showToast(toastObj) {
    $('body')
        .toast(toastObj);
}

function checkBox() {
    $(`.ui.checkbox`)
        .checkbox()
    ;
}