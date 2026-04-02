
function selectIncome(row) {

    let rows = document.querySelectorAll("#incomeTable tbody tr");
    rows.forEach(r => r.classList.remove("selected-row"));
    row.classList.add("selected-row");

    document.getElementById("incomeId").value = row.dataset.id;
    document.getElementById("deleteIncomeId").value = row.dataset.id;

    document.getElementById("registrationId").value = row.dataset.registration;
    document.getElementById("saleId").value = row.dataset.sale;
    document.getElementById("source").value = row.dataset.source;
    document.getElementById("membershipId").value = row.dataset.membership;
    document.getElementById("planId").value = row.dataset.plan;
    document.getElementById("notes").value = row.dataset.notes;

    let d = row.dataset.date;
    if (d && d.includes("T")) {
        document.getElementById("date").value = d.substring(0, 16);
    } else {
        document.getElementById("date").value = d;
    }
}

function clearForm() {
    document.getElementById("incomeId").value = "";
    document.getElementById("deleteIncomeId").value = "";
    document.querySelector("form").reset();

    let rows = document.querySelectorAll("#incomeTable tbody tr");
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