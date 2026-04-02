function selectExpense(row) {

    let rows = document.querySelectorAll("#expensesTable tbody tr");
    rows.forEach(r => r.classList.remove("selected-row"));
    row.classList.add("selected-row");

    document.getElementById("expenseId").value = row.dataset.id;
    document.getElementById("deleteExpenseId").value = row.dataset.id;

    document.getElementById("salaryId").value = row.dataset.salary;
    document.getElementById("type").value = row.dataset.type;
    document.getElementById("paymentId").value = row.dataset.payment;
    document.getElementById("amount").value = row.dataset.amount;
    document.getElementById("currencyId").value = row.dataset.currency;
    document.getElementById("notes").value = row.dataset.notes;

    let d = row.dataset.date;
    if (d && d.includes("T")) {
        document.getElementById("date").value = d.substring(0, 16);
    } else {
        document.getElementById("date").value = d;
    }
}

function clearForm() {
    document.getElementById("expenseForm").reset();
    document.getElementById("expenseId").value = "";
    document.getElementById("deleteExpenseId").value = "";

    let rows = document.querySelectorAll("#expensesTable tbody tr");
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