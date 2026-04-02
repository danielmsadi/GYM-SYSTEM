

function selectStore(row) {

    let rows = document.querySelectorAll("#storesTable tbody tr");
    rows.forEach(r => r.classList.remove("selected-row"));

    row.classList.add("selected-row");

    let id = row.getAttribute("data-id");
    let name = row.getAttribute("data-name");
    let club = row.getAttribute("data-club");

    document.getElementById("storeId").value = id;
    document.getElementById("deleteStoreId").value = id;

    document.getElementById("name").value = name;
    document.getElementById("clubId").value = club;
}


function clearForm() {
    document.getElementById("storeId").value = "";
    document.getElementById("deleteStoreId").value = "";
    document.getElementById("name").value = "";
    document.getElementById("clubId").value = "";

    let rows = document.querySelectorAll("#storesTable tbody tr");
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

