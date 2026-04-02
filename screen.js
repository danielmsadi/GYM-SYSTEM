function selectScreen(row) {

    let rows = document.querySelectorAll("#screenTable tbody tr");
    rows.forEach(r => r.classList.remove("selected-row"));

    row.classList.add("selected-row");

    let id = row.getAttribute("data-id");
    let name = row.getAttribute("data-name");
    let access = row.getAttribute("data-access");

    document.getElementById("screenId").value = id;
    document.getElementById("deleteScreenId").value = id;

    document.getElementById("name").value = name;
    document.getElementById("accessId").value = access;
}

function clearForm() {
    document.getElementById("screenForm").reset();
    document.getElementById("screenId").value = "";
    document.getElementById("deleteScreenId").value = "";

    let rows = document.querySelectorAll("#screenTable tbody tr");
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