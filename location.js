
function selectLocation(row) {

    let rows = document.querySelectorAll("#locationTable tbody tr");
    rows.forEach(r => r.classList.remove("selected-row"));

    row.classList.add("selected-row");

    document.getElementById("locationId").value = row.getAttribute("data-id");
    document.getElementById("deleteLocationId").value = row.getAttribute("data-id");

    document.getElementById("area").value = row.getAttribute("data-area");
    document.getElementById("address").value = row.getAttribute("data-address");
    document.getElementById("streetname").value = row.getAttribute("data-street");
}


function clearForm() {
    document.getElementById("locationForm").reset();
    document.getElementById("locationId").value = "";
    document.getElementById("deleteLocationId").value = "";

    let rows = document.querySelectorAll("#locationTable tbody tr");
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