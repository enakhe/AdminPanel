document.getElementById("Password").addEventListener("keyup", () => {
    document.getElementById("password-message").style.display = "block";
    const password = document.getElementById("Password").value;
    passwordValidator(password);
})

document.getElementById("Username").addEventListener("keyup", async () => {
    const username = document.getElementById("Username").value;
    const response = await fetch(`http://localhost:5071/UserApi/username?username=${username}`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json-patch+json",
        },
    });
    const isValid = await response.json();
    if (isValid.includes("Error")) {
        document.getElementById("usernameOutput").classList = "text-danger";
        document.getElementById("usernameIcon").classList = "fa-solid fa-circle-xmark text-danger mr-1"
    } else {
        document.getElementById("usernameOutput").classList = "text-success";
        document.getElementById("usernameIcon").classList = "fa-solid fa-circle-check text-success mr-1"

    }
    document.getElementById("usernameOutput").innerText = isValid;
    
})

const passwordValidator = (password) => {
    if (password !== null) {

        if (password.length >= 6) {
            document.getElementById("char").classList = "text-success";
            document.getElementById("charIcon").classList = "fa-solid fa-circle-check"
        } else {
            document.getElementById("char").classList = "text-danger";
            document.getElementById("charIcon").classList = "fa-solid fa-circle-xmark"
        }

        if (/\d/.test(password)) {
            document.getElementById("num").classList = "text-success";
            document.getElementById("numIcon").classList = "fa-solid fa-circle-check"
        } else {
            document.getElementById("num").classList = "text-danger";
            document.getElementById("numIcon").classList = "fa-solid fa-circle-xmark"
        }

        if (/[!@#$%&*]/.test(password)) {
            document.getElementById("alp").classList = "text-success";
            document.getElementById("alpIcon").classList = "fa-solid fa-circle-check"
        } else {
            document.getElementById("alp").classList = "text-danger";
            document.getElementById("alpIcon").classList = "fa-solid fa-circle-xmark"
        }
    }
}