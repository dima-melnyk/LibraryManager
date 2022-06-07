console.log("check")

let modalButtonLogin = document.querySelector(".modalButtonLogin");
let modalButtonRegister = document.querySelector(".modalButtonRegister");
let loginWrapper = document.querySelector(".loginWrapper");
let registerWrapper = document.querySelector(".registerWrapper");
let registerContainer = document.querySelector(".registerContainer");
let loginButton = document.querySelector(".loginButton");
let registerButton = document.querySelector(".registerButton");
let container = document.querySelector(".container");
let loginValidations = document.querySelectorAll(".loginValidation");
let registerValidations = document.querySelectorAll(".registerValidation");

let closeButtons = document.querySelectorAll(".closeButton");

for (const loginValidation of loginValidations) {
    if (loginValidation.childNodes.length !== 0) {
        modalButtonRegister.classList.add('active');
        modalButtonLogin.classList.add('active');
        registerWrapper.classList.remove('active');
        loginWrapper.classList.add('active');
        container.classList.add('active');
        registerContainer.classList.add('active');
        break;
    }
}

for (const registerValidation of registerValidations) {
    if (registerValidation.childNodes.length !== 0) {
        modalButtonRegister.classList.add('active');
        modalButtonLogin.classList.add('active');
        registerWrapper.classList.add('active');
        loginWrapper.classList.remove('active');
        container.classList.add('active');
        break;
    }
}

modalButtonLogin.addEventListener("click", function (e) {
    modalButtonRegister.classList.add('active');
    modalButtonLogin.classList.add('active');
    registerWrapper.classList.remove('active');
    loginWrapper.classList.add('active');
    container.classList.add('active');
    registerContainer.classList.add('active');

});

modalButtonRegister.addEventListener("click", function (e) {
    modalButtonRegister.classList.add('active');
    modalButtonLogin.classList.add('active');
    registerWrapper.classList.add('active');
    loginWrapper.classList.remove('active');
    container.classList.add('active');
});


for (const closeButton of closeButtons) {
    closeButton.addEventListener("click", function (e) {
        modalButtonRegister.classList.remove('active');
        modalButtonLogin.classList.remove('active');
        registerWrapper.classList.remove('active');
        registerContainer.classList.remove('active');
        loginWrapper.classList.remove('active');
        container.classList.remove('active');
    });
}

loginButton.addEventListener("click", function (e) {
    e.preventDefault();
});

registerButton.addEventListener("click", function (e) {
    e.preventDefault();
});