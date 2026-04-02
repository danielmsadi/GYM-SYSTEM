function selectMembership(row) {

    let rows = document.querySelectorAll("#membershipTable tbody tr");
    rows.forEach(r => r.classList.remove("selected-row"));

    row.classList.add("selected-row");

    document.getElementById("membershipId").value = row.getAttribute("data-id");
    document.getElementById("deleteMembershipId").value = row.getAttribute("data-id");

    document.getElementById("clientId").value = row.getAttribute("data-client");
    document.getElementById("planId").value = row.getAttribute("data-plan");
    document.getElementById("planname").value = row.getAttribute("data-planname");
    document.getElementById("price").value = row.getAttribute("data-price");
    document.getElementById("currency").value = row.getAttribute("data-currency");
    document.getElementById("discount").value = row.getAttribute("data-discount");

    let start = row.getAttribute("data-start");
    let end = row.getAttribute("data-end");

    if (start) document.getElementById("startDate").value = start.replace(" ", "T").substring(0, 16);
    if (end) document.getElementById("endDate").value = end.replace(" ", "T").substring(0, 16);
}

function clearForm() {
    document.getElementById("membershipForm").reset();
    document.getElementById("membershipId").value = "";
    document.getElementById("deleteMembershipId").value = "";

    let rows = document.querySelectorAll("#membershipTable tbody tr");
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