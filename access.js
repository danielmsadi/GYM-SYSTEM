function selectAccess(row) {

    let rows = document.querySelectorAll("#accessTable tbody tr");
    rows.forEach(r => r.classList.remove("selected-row"));
    row.classList.add("selected-row");

    document.getElementById("accessId").value = row.dataset.id;
    document.getElementById("deleteAccessId").value = row.dataset.id;

    document.getElementById("type").value = row.dataset.type;
    document.getElementById("name").value = row.dataset.name;
    document.getElementById("userId").value = row.dataset.user;
}

function clearForm() {
    document.getElementById("accessForm").reset();
    document.getElementById("accessId").value = "";
    document.getElementById("deleteAccessId").value = "";

    let rows = document.querySelectorAll("#accessTable tbody tr");
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