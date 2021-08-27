document.querySelector('#inputemail').addEventListener('blur', validateEmail);

document.querySelector('#inputpassword').addEventListener('blur', validatePassword);

document.querySelector('#inputname').addEventListener('blur', validateUsername);

document.querySelector('#inputaccount').addEventListener('blur', validateaccount);


const reSpaces = /^\S+$/;


function validateaccount() {
    const password = document.querySelector('#inputaccount');
    const re = /^\w+$/;
    if (re.test(password.value) && reSpaces.test(password.value)) {
        password.classList.remove('is-invalid');
        password.classList.add('is-valid');

        return true;
    } else {
        password.classList.add('is-invalid');
        password.classList.remove('is-valid');

        return false;
    }
}






function validateUsername(e) {
    const username = document.querySelector('#inputname');
    if (reSpaces.test(username.value)) {
        username.classList.remove('is-invalid');
        username.classList.add('is-valid');
        return true;
    } else {
        username.classList.remove('is-valid');

        username.classList.add('is-invalid');
        return false;
    }
}

function validateEmail(e) {
    const email = document.querySelector('#inputemail');
    const re = /^([a-zA-Z0-9_\-?\.?]){3,}@([a-zA-Z]){3,}\.([a-zA-Z]){2,5}$/;

    if (reSpaces.test(email.value) && re.test(email.value)) {
        email.classList.remove('is-invalid');
        email.classList.add('is-valid');

        return true;
    } else {
        email.classList.add('is-invalid');
        email.classList.remove('is-valid');

        return false;
    }
}

function validatePassword() {
    const password = document.querySelector('#inputpassword');
    const re = /^\w+$/;
    if (re.test(password.value) && reSpaces.test(password.value)) {
        password.classList.remove('is-invalid');
        password.classList.add('is-valid');

        return true;
    } else {
        password.classList.add('is-invalid');
        password.classList.remove('is-valid');

        return false;
    }
}










//(function () {
//    const forms = document.querySelectorAll('.needs-validation');

//    for (let form of forms) {
//        form.addEventListener(
//            'submit',
//            function (event) {
//                if (
//                    !form.checkValidity() ||
//                    !validateEmail() ||
//                    !validateUsername() ||
//                    !validatePassword()
//                ) {
//                    event.preventDefault();
//                    event.stopPropagation();
//                } else {
//                    form.classList.add('was-validated');
//                }
//            },
//            false
//        );
//    }
//})();