function selectClub(row) {

    let rows = document.querySelectorAll("#clubsTable tbody tr");
    rows.forEach(r => r.classList.remove("selected-row"));

    row.classList.add("selected-row");

    document.getElementById("clubId").value = row.dataset.id;
    document.getElementById("deleteClubId").value = row.dataset.id;

    document.getElementById("name").value = row.dataset.name;
    document.getElementById("description").value = row.dataset.description;
    document.getElementById("locationId").value = row.dataset.location;
    document.getElementById("sportId").value = row.dataset.sport;
    document.getElementById("price").value = row.dataset.price;
}

function clearForm() {
    document.getElementById("clubForm").reset();
    document.getElementById("clubId").value = "";
    document.getElementById("deleteClubId").value = "";

    let rows = document.querySelectorAll("#clubsTable tbody tr");
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