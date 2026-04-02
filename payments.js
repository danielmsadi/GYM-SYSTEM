function selectPayment(row) {

    let rows = document.querySelectorAll("#paymentsTable tbody tr");
    rows.forEach(r => r.classList.remove("selected-row"));

    row.classList.add("selected-row");

    let id = row.getAttribute("data-id");
    let method = row.getAttribute("data-method");
    let type = row.getAttribute("data-type");
    let tax = row.getAttribute("data-tax");
    let amount = row.getAttribute("data-amount");
    let currency = row.getAttribute("data-currency");

    document.getElementById("paymentId").value = id;
    document.getElementById("deletePaymentId").value = id;

    document.getElementById("methodId").value = method;
    document.getElementById("type").value = type;
    document.getElementById("tax").value = tax;
    document.getElementById("amount").value = amount;
    document.getElementById("currencyId").value = currency;
}

function clearForm() {
    document.getElementById("paymentForm").reset();
    document.getElementById("paymentId").value = "";
    document.getElementById("deletePaymentId").value = "";

    let rows = document.querySelectorAll("#paymentsTable tbody tr");
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