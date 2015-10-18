$(document).ready(function () {
    $("a[href='#admin-manage-borrows']").on('click', function () {
        loadAllBorrows();
    });
});

function loadAllBorrows(){
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
                "</td><td><span title='Details' " +
                            "onclick='openBorrowDetailsModal(" + obj.borrowSeqNumber + ")'" +
                             "class='glyphicon glyphicon-list-alt'></span>" +
                     "| <span title='Edit' " +
                            "onclick='openEditBorrowModal(" + obj.borrowSeqNumber + ")'" +
                             "class='glyphicon glyphicon-edit'></span>" +
                     "| <span title='Delete' " +
                            "onclick='adminDeleteBorrow(" + obj.borrowSeqNumber + ")'" +
                             "class='glyphicon glyphicon-trash'></span>"
                    + "</td></tr>";
                $("#adminBorrowsTableBody").append(row);
            });
        }
    });
}

function borrowBook(bookId) {
    $.ajax({
        dataType: "json",
        data: { "bookId": bookId },
        url: "Borrow/createBorrow",
        success: function (message) {
            if (!message) {
            }
            else {
                alert(message);
            }
        }
    });
};


function adminDeleteBorrow(borrowSeq) {
    bootbox.confirm("Are you sure you want to delete this borrow?", function (result) {
        if (result == true) {
            debugger;   
            $.ajax({
                dataType: "json",
                data: { "borrowSeq": borrowSeq },
                url: "Borrow/deleteBorrow",
                success: function (message) {
                    if (!message) {
                        loadAllBorrows();
                    }
                    else {
                        alert(message);
                    }
                }
            });
        }
    });
};