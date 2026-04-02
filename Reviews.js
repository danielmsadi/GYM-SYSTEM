function selectReview(row) {

    let rows = document.querySelectorAll("#reviewTable tbody tr");
    rows.forEach(r => r.classList.remove("selected-row"));

    row.classList.add("selected-row");

    let id = row.getAttribute("data-id");
    let name = row.getAttribute("data-name");
    let comment = row.getAttribute("data-comment");
    let rating = row.getAttribute("data-rating");

    document.getElementById("reviewId").value = id;
    document.getElementById("deleteReviewId").value = id;

    document.getElementById("name").value = name;
    document.getElementById("comment").value = comment;
    document.getElementById("rating").value = rating;
}

function clearForm() {
    document.getElementById("reviewForm").reset();
    document.getElementById("reviewId").value = "";
    document.getElementById("deleteReviewId").value = "";

    let rows = document.querySelectorAll("#reviewTable tbody tr");
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