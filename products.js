function selectProduct(row) {

    let rows = document.querySelectorAll("#productsTable tbody tr");
    rows.forEach(r => r.classList.remove("selected-row"));

    row.classList.add("selected-row");

    let id = row.getAttribute("data-id");
    let name = row.getAttribute("data-name");
    let description = row.getAttribute("data-description");
    let price = row.getAttribute("data-price");
    let currency = row.getAttribute("data-currency");

    document.getElementById("productId").value = id;
    document.getElementById("deleteProductId").value = id;

    document.getElementById("name").value = name;
    document.getElementById("description").value = description;
    document.getElementById("price").value = price;
    document.getElementById("currencyId").value = currency;
}

function clearForm() {
    document.getElementById("productForm").reset();
    document.getElementById("productId").value = "";
    document.getElementById("deleteProductId").value = "";

    let rows = document.querySelectorAll("#productsTable tbody tr");
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