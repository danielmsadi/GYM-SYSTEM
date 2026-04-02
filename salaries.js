function selectSalary(row) {

    let rows = document.querySelectorAll("#salariesTable tbody tr");
    rows.forEach(r => r.classList.remove("selected-row"));

    row.classList.add("selected-row");

    let id = row.getAttribute("data-id");
    let staff = row.getAttribute("data-staff");
    let amount = row.getAttribute("data-amount");
    let currency = row.getAttribute("data-currency");
    let date = row.getAttribute("data-date");

    document.getElementById("salaryId").value = id;
    document.getElementById("deleteSalaryId").value = id;

    document.getElementById("staffId").value = staff;
    document.getElementById("amount").value = amount;
    document.getElementById("currencyId").value = currency;

    if (date.includes("T"))
        document.getElementById("date").value = date.split("T")[0];
    else
        document.getElementById("date").value = date;
}

function clearForm() {
    document.getElementById("salaryForm").reset();
    document.getElementById("salaryId").value = "";
    document.getElementById("deleteSalaryId").value = "";

    let rows = document.querySelectorAll("#salariesTable tbody tr");
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