function selectTS(row) {

    let rows = document.querySelectorAll("#tsTable tbody tr");
    rows.forEach(r => r.classList.remove("selected-row"));
    row.classList.add("selected-row");

    document.getElementById("tsId").value = row.dataset.id;
    document.getElementById("deleteTsId").value = row.dataset.id;

    document.getElementById("sportId").value = row.dataset.sport;
    document.getElementById("trainerId").value = row.dataset.trainer;
    document.getElementById("name").value = row.dataset.name;
}

function clearForm() {
    document.getElementById("tsForm").reset();
    document.getElementById("tsId").value = "";
    document.getElementById("deleteTsId").value = "";

    let rows = document.querySelectorAll("#tsTable tbody tr");
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