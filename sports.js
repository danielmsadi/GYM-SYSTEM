

function selectSport(row) {

    let rows = document.querySelectorAll("#sportsTable tbody tr");
    rows.forEach(r => r.classList.remove("selected-row"));

    row.classList.add("selected-row");

    let id = row.getAttribute("data-id");
    let name = row.getAttribute("data-name");
    let io = row.getAttribute("data-io");
    let trainer = row.getAttribute("data-trainer");
    let capacity = row.getAttribute("data-capacity");
    let duration = row.getAttribute("data-duration");

    document.getElementById("sportId").value = id;
    document.getElementById("deleteSportId").value = id;

    document.getElementById("name").value = name;
    document.getElementById("inOutdoor").value = io;
    document.getElementById("trainerId").value = trainer;
    document.getElementById("capacity").value = capacity;
    document.getElementById("duration").value = duration;
}


function clearForm() {
    document.getElementById("sportsForm").reset();
    document.getElementById("sportId").value = "";
    document.getElementById("deleteSportId").value = "";

    let rows = document.querySelectorAll("#sportsTable tbody tr");
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