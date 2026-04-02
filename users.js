

function selectUser(row) {

    
    let rows = document.querySelectorAll("#userTable tbody tr");
    rows.forEach(r => r.classList.remove("selected-row"));

    
    row.classList.add("selected-row");

    
    let id = row.getAttribute("data-id");
    let name = row.getAttribute("data-name");
    let email = row.getAttribute("data-email");
    let gender = row.getAttribute("data-gender");
    let phone = row.getAttribute("data-phone");
    let date = row.getAttribute("data-date");
    let password = row.getAttribute("data-password");

    
    document.getElementById("userId").value = id;
    document.getElementById("deleteUserId").value = id;

    
    document.getElementById("name").value = name;
    document.getElementById("email").value = email;
    document.getElementById("gender").value = gender;
    document.getElementById("phone").value = phone;

    
    if (date && date.includes("T")) {
        document.getElementById("date").value = date.substring(0, 16);
    } else {
        document.getElementById("date").value = date;
    }

    document.getElementById("password").value = password;
}


function clearForm() {
    document.getElementById("userForm").reset();
    document.getElementById("userId").value = "";
    document.getElementById("deleteUserId").value = "";

    let rows = document.querySelectorAll("#userTable tbody tr");
    rows.forEach(r => r.classList.remove("selected-row"));
}


function exitDashboard() {
    if (confirm("Exit dashboard and return to main page?")) {
        window.location.href = "/Main";
    }
}


function logout() {
    if (confirm("Are you sure you want to logout?")) {
        window.location.href = "/Index";
    }
}