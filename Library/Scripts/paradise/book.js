
$(document).ready(function () {
    $("a[href='#all-books']").on('click', function () {
        showAllBooks();
    });
    $("a[href='#admin-manage-books']").on('click', function () {
        loadAllBooks();
    });
});


function loadAllBooks() {
    $.ajax({
        url: "Books/getAllBooks",
        type: "GET",
        success: function (data) {
            $("#adminBooksTableBody").empty();
            $.each(data, function (idx, obj) {
                row = "<tr><td>" + obj.title + "</td><td>" + obj.author +
                 "</td><td>" + obj.series + "</td><td>" + obj.number +
                 "</td><td>" + obj.publicationYear + "</td><td>" + obj.copies +
                 "</td><td><span title='Details' " +
                        "onclick='openBookDetailsModal(" + obj.id + ")'" +
                         "class='glyphicon glyphicon-list-alt'></span>" +
                 "| <span title='Edit' " +
                        "onclick='openEditBookModal(" + obj.id + ")'" +
                         "class='glyphicon glyphicon-edit'></span>" +
                 "| <span title='Delete' " +
                        "onclick='adminDeleteBook(" + obj.id + ")'" +
                         "class='glyphicon glyphicon-trash'></span>"
                + "</td></tr>";
                $("#adminBooksTableBody").append(row);
            });

        }
    });
}

function adminDeleteBook(bookId) {
    bootbox.confirm("Are you sure you want to delete this book?", function (result) {
        if (result == true) {
            debugger;
            $.ajax({
                dataType: "json",
                data: { "bookId": bookId },
                url: "Books/deleteBook",
                success: function (message) {
                    if (!message) {
                        loadAllBooks();
                    }
                    else {
                        alert(message);
                    }
                }
            });
        }
    });
};

function openBookDetailsModal(id) {
        $.ajax({
            dataType: "json",
            url: "Books/getBookById",
            data: { "id": id },
            success: function (book) {
                debugger;
                $('#detailsBookTitle').text(book.title);
                $('#detailsBookSeries').text('(' + book.series + ((book.number) ? ' #' + book.number : "") + ')');
                $('#detailsBookAuthor').text('by ' + book.author);
                $('#detailsCopies').text(book.copies + ' copies available')
                $('#detailsBookSummery').text(book.summery);
                $('#detailsBookPublishData').text('published at ' + book.publicationYear + ' by ' + book.publisher + ', ' + book.language);
                $('#detailsBookCategory').text(book.category);
                $('#borrowBook').click(function () {
                    borrowBook(book.id);
                });
            }});

        $('#bookDetailsModal').modal('show');
};


function showAllBooks() {
    $.ajax({
        url: "Books/getAllBooks",
        type: "GET",
        success: function (data) {
            $("#userBooksTableBody").empty();
            $.each(data, function (idx, obj) {
                row = "<tr><td>" + obj.title + "</td><td>" + obj.author +
                 "</td><td>" + obj.series + "</td><td>" + obj.number +
                 "</td><td>" + obj.publicationYear + "</td><td>" + obj.copies +
                 "</td><td> <a onclick='openBookDetailsModal(" + obj.id + ")')>See Details</a>" 
                + "</td></tr>";
                $("#userBooksTableBody").append(row);
            });

        }
    });
}