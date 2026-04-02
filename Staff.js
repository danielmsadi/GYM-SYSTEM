
function selectStaff(row) {

    let rows = document.querySelectorAll("#staffsTable tbody tr");
    rows.forEach(r => r.classList.remove("selected-row"));

    row.classList.add("selected-row");

    let id = row.getAttribute("data-id");
    let role = row.getAttribute("data-role");
    let work = row.getAttribute("data-work");
    let salary = row.getAttribute("data-salary");
    let user = row.getAttribute("data-user");
    let job = row.getAttribute("data-job");
    let shift = row.getAttribute("data-shift");

    document.getElementById("staffId").value = id;
    document.getElementById("deleteStaffId").value = id;

    document.getElementById("roleId").value = role;
    document.getElementById("workingh").value = work;
    document.getElementById("salaryId").value = salary;
    document.getElementById("userId").value = user;
    document.getElementById("jobtitle").value = job;
    document.getElementById("shift").value = shift;
}


function clearForm() {

    document.getElementById("staffId").value = "";
    document.getElementById("deleteStaffId").value = "";

    document.getElementById("roleId").value = "";
    document.getElementById("workingh").value = "";
    document.getElementById("salaryId").value = "";
    document.getElementById("userId").value = "";
    document.getElementById("jobtitle").value = "";
    document.getElementById("shift").value = "";

    let rows = document.querySelectorAll("#staffsTable tbody tr");
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