function selectClient(row) {

    let rows = document.querySelectorAll("#clientsTable tbody tr");
    rows.forEach(r => r.classList.remove("selected-row"));

    row.classList.add("selected-row");

    document.getElementById("clientId").value = row.dataset.id;
    document.getElementById("deleteClientId").value = row.dataset.id;

    document.getElementById("userId").value = row.dataset.user;
    document.getElementById("name").value = row.dataset.name;
    document.getElementById("email").value = row.dataset.email;
    document.getElementById("gender").value = row.dataset.gender;
    document.getElementById("phone").value = row.dataset.phone;

    let date = row.dataset.date;
    if (date && date.includes("T")) {
        document.getElementById("date").value = date.substring(0, 10);
    } else {
        document.getElementById("date").value = date;
    }
}

function clearForm() {
    document.getElementById("clientForm").reset();
    document.getElementById("clientId").value = "";
    document.getElementById("deleteClientId").value = "";

    let rows = document.querySelectorAll("#clientsTable tbody tr");
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