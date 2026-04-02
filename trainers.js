function selectTrainer(row) {

    let rows = document.querySelectorAll("#trainersTable tbody tr");
    rows.forEach(r => r.classList.remove("selected-row"));

    row.classList.add("selected-row");

    let id = row.getAttribute("data-id");
    let name = row.getAttribute("data-name");
    let salary = row.getAttribute("data-salary");
    let ts = row.getAttribute("data-ts");

    document.getElementById("trainerId").value = id;
    document.getElementById("deleteTrainerId").value = id;

    document.getElementById("name").value = name;
    document.getElementById("salaryId").value = salary;
    document.getElementById("trainersportsId").value = ts;
}

function clearForm() {

    document.getElementById("trainerForm").reset();
    document.getElementById("trainerId").value = "";
    document.getElementById("deleteTrainerId").value = "";

    let rows = document.querySelectorAll("#trainersTable tbody tr");
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
