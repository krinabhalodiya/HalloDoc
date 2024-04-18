// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

const phoneInputField = document.querySelector("#phone");
const phoneInput = window.intlTelInput(phoneInputField, {
    separateDialCode: true,
    preferredCountries: ["in"],
    hiddenInput: "full",
    utilsScript: "https://cdnjs.cloudflare.com/ajax/libs/intl-tel-input/17.0.8/js/utils.js",
});

const input = document.querySelector("#phone");
const button = document.querySelector("#submit");
const errorMsg = document.querySelector("#error-msg");
const errorMap = ["Invalid number", "Invalid country code", "Too short", "Too long", "Invalid number"];
const reset = () => {
    input.classList.remove("error");
    errorMsg.innerHTML = "";
    errorMsg.classList.add("hide");
};
const showError = (msg) => {
    input.classList.add("error");
    errorMsg.innerHTML = msg;
    errorMsg.classList.remove("hide");
};
button.addEventListener('click', () => {
    reset();
    if (!input.value.trim()) {
        showError("Required");
    } else if (phoneInput.isValidNumber()) {
        $("form").submit();
    } else {
        const errorCode = phoneInput.getValidationError();
        const msg = errorMap[errorCode] || "Invalid number";
        showError(msg);
    }
});

// on keyup / change flag: reset
input.addEventListener('change', reset);
input.addEventListener('keyup', reset);

$(document).ready(function () {
    $(".t-tab > .btn > .rounded").click(function () {
        $(".t-tab > .btn > .rounded").removeClass("active");
        $(this).addClass("active");
    });

    $(".t-filter > .btn ").click(function () {
        $(".t-filter> .btn").removeClass("filterborder");
        $(this).addClass("filterborder");
    });

    $(".t-filter > .btn ").click(function () {
        $(".t-filter> .btn").removeClass("filterborder");
        $(this).addClass("filterborder");
    });
});

function DisplayFileName() {
    const fileInput = document.getElementById('files');
    if (fileInput.files.length > 0) {
        document.getElementById('fileName').innerHTML = fileInput.files[0].name;
    }
}

