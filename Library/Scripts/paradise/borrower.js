$(document).ready(function () {
    $("a[href='#borrower-library-card']").on('click', function () {
        showBorrowerProfile();
        showOpenBorrows();
    });

    $("a[href='#admin-manage-borrowers']").on('click', function () {
        $.ajax({
            url: "Borrower/getAllBorrowers",
            type: "GET",
            success: function (data) {
                debugger;
                $("#adminBorrowersTableBody").empty();
                $.each(data, function (idx, obj) {
                    row = "<tr><td>" + obj.id + "</td><td>" + obj.firstName + " " + obj.lastName +
                     "</td><td>" + obj.sex + "</td><td>" + obj.address +
                     "</td><td>" + obj.phone + "</td><td>" + obj.mail +
                     "</td><td>test</td></tr>" + "</td></tr>";
                    $("#adminBorrowersTableBody").append(row);
                });

            }
        });
    });

    $("#showBorrowerHistory").click(function () {
        openBorrowsHistoryModal()
    });

    $("#saveBorrowerChanges").click(function () {
        debugger;
        $.ajax({
            dataType: "json",
            url: "Borrower/updateBorrower",
            data: {
                "id": $("#updateBorrowerId").val(),
                "userId": $("#updateBorrowerUserId").val(),
                "firstName": $("#updateBorrowerFirstName").val(),
                "lastName": $("#updateBorrowerLastName").val(),
                "sex": $('input:radio[name=sexOptionsRadio]:checked').val(),
                "phone": $("#updateBorrowerPhone").val(),
                "address": $("#updateBorrowerAddress").val(),
                "mail": $("#updateBorrowerMail").val()
            },
            success: function (saveMessage) {
                if (!saveMessage) {
                    $("#backToCardLink").click();
                }
                else {
                    alert(saveMessage);
                }
            }
        });
    });
});

function showOpenBorrows() {
    debugger;
        $.ajax({
            dataType: "json",
            url: "Borrow/getCurrentBorrowerOpenBorrows",
            success: function (borrowers) {
                if (borrowers.length > 0) {
                    $("#userOpenBorrowsTablaNone").hide();
                    $("#userOpenBorrowsTabla").show();
                    $("#userOpenBorrowsTablaBody").empty();
                    $.each(borrowers, function (idx, obj) {
                        row = "<tr><td>" + obj.bookTitle + "</td><td>" + obj.bookAuthor +
                         "</td><td>" + obj.borrowDate +
                         "</td><td>" + "<a onclick='returnBook(" + obj.borrowSeqNumber + ")'>Return Book</a>" + "</td></tr>";
                        $("#userOpenBorrowsTablaBody").append(row);
                    });
                } else {
                    $("#userOpenBorrowsTablaNone").show();
                    $("#userOpenBorrowsTabla").hide();
                }
            }
        });
};

function openBorrowsHistoryModal() {
    debugger;
    $.ajax({
        dataType: "json",
        url: "Borrow/getCurrentBorrowerHistoryBorrows",
        success: function (borrowers) {
            debugger;
            $("#userBorrowHistoryTableBody").empty();
            $.each(borrowers, function (idx, obj) {
                row = "<tr><td>" + obj.bookTitle + "</td><td>" + obj.bookAuthor +
                     "</td><td>" + obj.borrowDate +
                     "</td><td>" + "<a onclick='openDetailsModal(" + obj.bookId + ")'>See Book Detils</a>" + "</td></tr>";
                $("#userBorrowHistoryTableBody").append(row);
            });


            $('#userBorrowHistory').modal('show');

        }
    });
};
function returnBook(borrowSeq) {
    $.ajax({
        dataType: "json",
        data: { "borrowSeq": borrowSeq },
        url: "Borrow/returnBorrow",
        success: function (message) {
            if (!message) {
                showOpenBorrows();
            }
            else {
                alert(message);
            }
        }
    });
};

    function showBorrowerProfile() {
        $.ajax({
            dataType: "json",
            url: "Borrower/getCurrentBorrower",
            success: function (borrower) {
                $("#cardHeader").text(borrower.firstName + "'s Library Card");
                $("#borrowerFirstName").text(borrower.firstName);
                $("#borrowerLastName").text(borrower.lastName);
                $("#borrowerSex").text(borrower.sex);
                $("#borrowerPhone").text(borrower.phone);
                $("#borrowerAddress").text(borrower.address);
                $("#borrowerMail").text(borrower.mail);

                $("#updateBorrowerId").val(borrower.id).prop('disabled', true);
                $("#updateBorrowerUserId").val(borrower.userId);
                $("#updateBorrowerFirstName").val(borrower.firstName);
                $("#updateBorrowerLastName").val(borrower.lastName);
                $("input:radio[name=sexOptionsRadio][value=" + borrower.sex + "]").prop('checked', true);
                $("#updateBorrowerPhone").val(borrower.phone);
                $("#updateBorrowerAddress").val(borrower.address);
                $("#updateBorrowerMail").val(borrower.mail);
            }
        });
    };