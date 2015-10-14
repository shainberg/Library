$(document).ready(function () {
    $("a[href='#admin-manage-borrows']").on('click', function () {
        $.ajax({
            url: "Borrow/getAllBorrows",
            type: "GET",
            success: function (data) {
                debugger;
                $("#adminBorrowsTableBody").empty();
                $.each(data, function (idx, obj) {
                    debugger;
                    row = "<tr><td>" + obj.borrowerFirstName + " " + obj.borrowerLastName +
                    "</td><td>" + obj.bookTitle + "</td><td>" + obj.bookAuthor +
                     "</td><td>" + obj.borrowDate + "</td><td>" + obj.returnDate +
                     "</td><td>" + "</td></tr>";
                    $("#adminBorrowsTableBody").append(row);
                });

            }
        });
    });
});