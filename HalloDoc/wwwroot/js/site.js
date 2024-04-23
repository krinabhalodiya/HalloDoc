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
    const input = document.querySelector("#phone");
    const button = document.querySelector("#phone");
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

    button.addEventListener('focusout', (event) => {

        reset();

        if (!input.value.trim()) {
            showError("Required");
            document.getElementById('submit').setAttribute("disabled", "disabled");

        } else {
            // Assume 'e' is a validation object/library with methods isValidNumber() and getValidationError()
            if (phoneInput.isValidNumber(input.value)) {

                const full_number = phoneInput.getNumber(intlTelInputUtils.numberFormat.E164);
                console.log(full_number);
                document.getElementById("phone").value = full_number;
                document.getElementById('submit').removeAttribute("disabled");

            } else {
                const errorCode = phoneInput.getValidationError(input.value);
                const msg = errorMap[errorCode] || "Invalid number";
                showError(msg);
                document.getElementById('submit').setAttribute("disabled", "disabled");
            }
        }
    });

    // Reset error state on input change
    input.addEventListener('change', reset);
    input.addEventListener('keyup', reset);
}
function phone2() {
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

    const phoneInputField1 = document.querySelector("#phone1");
    const phoneInput1 = window.intlTelInput(phoneInputField1, {
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
    const input = document.querySelector("#phone");
    const button = document.querySelector("#phone");
    const errorMsg = document.querySelector("#error-msg");
    const input1 = document.querySelector("#phone1");
    const button1 = document.querySelector("#phone1");
    const errorMsg1 = document.querySelector("#error-msg1");
    const errorMap1 = ["Invalid number", "Invalid country code", "Too short", "Too long", "Invalid number"];
    const reset = () => {
        input.classList.remove("error");
        errorMsg.innerHTML = "";
        errorMsg.classList.add("hide");
    };
    const reset1 = () => {
        input1.classList.remove("error");
        errorMsg1.innerHTML = "";
        errorMsg1.classList.add("hide");
    }
    const showError1 = (msg) => {
        input1.classList.add("error");
        errorMsg1.innerHTML = msg;
        errorMsg1.classList.remove("hide");
    };
    const showError = (msg) => {
        input.classList.add("error");
        errorMsg.innerHTML = msg;
        errorMsg.classList.remove("hide");
    };

    
    button.addEventListener('focusout', (event) => {
        var flag = false;
        var flag1 = false;
        reset();

        if (!input1.value.trim()) {
            showError1("Required");
            document.getElementById('submit').setAttribute("disabled", "disabled");
            flag =  false;
        } else {
            // Assume 'e' is a validation object/library with methods isValidNumber() and getValidationError()
            if (phoneInput1.isValidNumber(input1.value)) {

                const full_number1 = phoneInput1.getNumber(intlTelInputUtils.numberFormat.E164);
                console.log(full_number1);
                document.getElementById("phone1").value = full_number1;
                flag = true;

            } else {
                const errorCode1 = phoneInput1.getValidationError(input1.value);
                const msg1 = errorMap1[errorCode1] || "Invalid number";
                showError1(msg1)
                document.getElementById('submit').setAttribute("disabled", "disabled");
                flag =  false;
            }
        }
        
        if (!input.value.trim()) {
            showError("Required");
            document.getElementById('submit').setAttribute("disabled", "disabled");
            flag = false;
        } else {
            // Assume 'e' is a validation object/library with methods isValidNumber() and getValidationError()
            if (phoneInput.isValidNumber(input.value)) {

                const full_number = phoneInput.getNumber(intlTelInputUtils.numberFormat.E164);
                console.log(full_number);
                document.getElementById("phone").value = full_number;
                flag1 = true;

            } else {
                const errorCode = phoneInput.getValidationError(input.value);
                const msg = errorMap1[errorCode] || "Invalid number";
                showError(msg);
                document.getElementById('submit').setAttribute("disabled", "disabled");
                flag = false;
            }
        }
        console.log(flag);
        console.log(flag1);
        if (flag == true && flag1 == true) {
            document.getElementById('submit').removeAttribute("disabled");
        }
    });
   
    button1.addEventListener('focusout', (event) => {
        var flag = false;
        var flag1 = false;
        reset();

        if (!input1.value.trim()) {
            showError1("Required");
            document.getElementById('submit').setAttribute("disabled", "disabled");
            flag = false;
        } else {
            // Assume 'e' is a validation object/library with methods isValidNumber() and getValidationError()
            if (phoneInput1.isValidNumber(input1.value)) {

                const full_number1 = phoneInput1.getNumber(intlTelInputUtils.numberFormat.E164);
                console.log(full_number1);
                document.getElementById("phone1").value = full_number1;
                flag = true;

            } else {
                const errorCode1 = phoneInput1.getValidationError(input1.value);
                const msg1 = errorMap1[errorCode1] || "Invalid number";
                showError1(msg1)
                document.getElementById('submit').setAttribute("disabled", "disabled");
                flag = false;
            }
        }

        if (!input.value.trim()) {
            showError("Required");
            document.getElementById('submit').setAttribute("disabled", "disabled");
            flag = false;
        } else {
            // Assume 'e' is a validation object/library with methods isValidNumber() and getValidationError()
            if (phoneInput.isValidNumber(input.value)) {

                const full_number = phoneInput.getNumber(intlTelInputUtils.numberFormat.E164);
                console.log(full_number);
                document.getElementById("phone").value = full_number;
                flag1 = true;

            } else {
                const errorCode = phoneInput.getValidationError(input.value);
                const msg = errorMap1[errorCode] || "Invalid number";
                showError(msg);
                document.getElementById('submit').setAttribute("disabled", "disabled");
                flag = false;
            }
        }
        console.log(flag);
        console.log(flag1);
        if (flag == true && flag1 == true) {
            document.getElementById('submit').removeAttribute("disabled");
        }
    });
    
    // on keyup / change flag: reset
    input1.addEventListener('change', reset1);
    input1.addEventListener('keyup', reset1);
    input.addEventListener('change', reset);
    input.addEventListener('keyup', reset);

   
    
}
function phone3() {
    const phoneInputField3 = document.querySelector("#phone3");
    const phoneInput3 = window.intlTelInput(phoneInputField3, {
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
    const input3 = document.querySelector("#phone3");
    const button3 = document.querySelector("#phone3");
    const errorMsg3 = document.querySelector("#error-msg3");
    const errorMap3 = ["Invalid number", "Invalid country code", "Too short", "Too long", "Invalid number"];

    const reset3 = () => {
        input3.classList.remove("error");
        errorMsg3.innerHTML = "";
        errorMsg3.classList.add("hide");
    };

    const showError3 = (msg) => {
        input3.classList.add("error");
        errorMsg3.innerHTML = msg;
        errorMsg3.classList.remove("hide");
    };

    button3.addEventListener('focusout', (event) => {

        reset3();

        if (!input3.value.trim()) {
            showError3("Required");
            document.getElementById('submit3').setAttribute("disabled", "disabled");

        } else {
            // Assume 'e' is a validation object/library with methods isValidNumber() and getValidationError()
            if (phoneInput3.isValidNumber(input3.value)) {

                const full_number3 = phoneInput3.getNumber(intlTelInputUtils.numberFormat.E164);
                console.log(full_number3);
                document.getElementById("phone3").value = full_number3;
                document.getElementById('submit3').removeAttribute("disabled");

            } else {
                const errorCode3 = phoneInput3.getValidationError(input3.value);
                const msg3 = errorMap3[errorCode3] || "Invalid number";
                showError3(msg3);
                document.getElementById('submit3').setAttribute("disabled", "disabled");
            }
        }
    });

    // Reset error state on input change
    input3.addEventListener('change', reset3);
    input3.addEventListener('keyup', reset3);
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

