document.getElementById("Password").addEventListener("keyup", () => {
    document.getElementById("password-message").style.display = "block";
    const password = document.getElementById("Password").value;
    passwordValidator(password)
})

const passwordValidator = (password) => {
    if (password !== null) {

        if (password.length >= 6) {
            document.getElementById("char").classList = "text-success";
            document.getElementById("charIcon").classList = "fa-solid fa-check"
        } else {
            document.getElementById("char").classList = "text-danger";
            document.getElementById("charIcon").classList = "fa-solid fa-x"
        }

        if (/\d/.test(password)) {
            document.getElementById("num").classList = "text-success";
            document.getElementById("numIcon").classList = "fa-solid fa-check"
        } else {
            document.getElementById("num").classList = "text-danger";
            document.getElementById("numIcon").classList = "fa-solid fa-x"
        }

        if (/[!@#$%&*]/.test(password)) {
            document.getElementById("alp").classList = "text-success";
            document.getElementById("alpIcon").classList = "fa-solid fa-check"
        } else {
            document.getElementById("alp").classList = "text-danger";
            document.getElementById("alpIcon").classList = "fa-solid fa-x"
        }
    }
}