function selectAttendance(row) {

    let rows = document.querySelectorAll("#attendanceTable tbody tr");
    rows.forEach(r => r.classList.remove("selected-row"));

    row.classList.add("selected-row");

    document.getElementById("attendanceId").value = row.dataset.id;
    document.getElementById("deleteAttendanceId").value = row.dataset.id;

    document.getElementById("staffId").value = row.dataset.staff;
    document.getElementById("checkIn").value = row.dataset.checkin;
    document.getElementById("checkOut").value = row.dataset.checkout;

    let d = row.dataset.date;
    if (d && d.includes("T")) {
        document.getElementById("date").value = d.substring(0, 16);
    } else {
        document.getElementById("date").value = d;
    }
}

function clearForm() {
    document.getElementById("attendanceForm").reset();
    document.getElementById("attendanceId").value = "";
    document.getElementById("deleteAttendanceId").value = "";

    let rows = document.querySelectorAll("#attendanceTable tbody tr");
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