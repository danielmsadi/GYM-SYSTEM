function selectPM(row) {

    let rows = document.querySelectorAll("#pmTable tbody tr");
    rows.forEach(r => r.classList.remove("selected-row"));

    row.classList.add("selected-row");

    let id = row.getAttribute("data-id");
    let type = row.getAttribute("data-type");

    document.getElementById("pmId").value = id;
    document.getElementById("deletePmId").value = id;

    document.getElementById("type").value = type;
}

function clearForm() {
    document.getElementById("pmForm").reset();
    document.getElementById("pmId").value = "";
    document.getElementById("deletePmId").value = "";

    let rows = document.querySelectorAll("#pmTable tbody tr");
    rows.forEach(r => r.classList.remove("selected-row"));
}

function exitDashboard() {
    if (confirm("Exit and return to main page?")) {
        window.location.href = "/Main";
    }
}

function logout() {
    if (confirm("Are you sure you want to logout?")) {
        window.location.href = "/Index";
    }
}