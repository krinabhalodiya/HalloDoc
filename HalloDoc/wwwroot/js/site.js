// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function phone1() {
    const phoneInputField = document.querySelector("#phone");
    const phoneInput = window.intlTelInput(phoneInputField, {
        initialCountry: "auto",
        geoIpLookup: callback => {
            fetch("https://ipapi.co/json")
                .then(res => res.json())
                .then(data => callback(data.country_code))
                .catch(() => callback("us"));
        },
        separateDialCode: true,
        hiddenInput: "full",
        utilsScript: "https://cdnjs.cloudflare.com/ajax/libs/intl-tel-input/17.0.8/js/utils.js",
    });
    number_validation1(phoneInput);
}
function phone2() {
    const phoneInputField1 = document.querySelector("#phone1");
    const phoneInput1 = window.intlTelInput(phoneInputField, {
        initialCountry: "auto",
        geoIpLookup: callback => {
            fetch("https://ipapi.co/json")
                .then(res => res.json())
                .then(data => callback(data.country_code))
                .catch(() => callback("us"));
        },
        separateDialCode: true,
        hiddenInput: "full",
        utilsScript: "https://cdnjs.cloudflare.com/ajax/libs/intl-tel-input/17.0.8/js/utils.js",
    });
    number_validation2();
}

function number_validation1(e) {
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
        } else if (e.isValidNumber()) {
            $("form").submit();
        } else {
            const errorCode = e.getValidationError();
            const msg = errorMap[errorCode] || "Invalid number";
            showError(msg);
        }
    });
    // on keyup / change flag: reset
    input.addEventListener('change', reset);
    input.addEventListener('keyup', reset);

}

function number_validation2() {
    const input1 = document.querySelector("#phone1");
    const button1 = document.querySelector("#submit");
    const errorMsg1 = document.querySelector("#error-msg");
    const errorMap1 = ["Invalid number", "Invalid country code", "Too short", "Too long", "Invalid number"];
    const reset = () => {
        input1.classList.remove("error");
        errorMsg1.innerHTML = "";
        errorMsg1.classList.add("hide");
    };
    const showError = (msg) => {
        input1.classList.add("error");
        errorMsg1.innerHTML = msg;
        errorMsg1.classList.remove("hide");
    };
    button1.addEventListener('click', () => {
        reset();
        if (!input1.value.trim()) {
            showError("Required");
        } else if (phoneInput1.isValidNumber()) {
            $("form").submit();
        } else {
            const errorCode1 = phoneInput1.getValidationError();
            const msg = errorMap1[errorCode] || "Invalid number";
            showError(msg);
        }
    });
    // on keyup / change flag: reset
    input1.addEventListener('change', reset);
    input1.addEventListener('keyup', reset);

}



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

