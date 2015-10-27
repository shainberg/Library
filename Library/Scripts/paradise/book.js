
$(document).ready(function () {
    $("a[href='#all-books']").on('click', function () {
        showAllBooks();
    });
    $("a[href='#admin-manage-books']").on('click', function () {
        loadAllBooks();
    });
    $("a[href='#best-books']").on('click', function () {
        debugger;
        loadBestBooks();
    });
});

function createNewBook() {
    $.ajax({
        dataType: "json",
        data: createBookData(),
        url: "Books/createBook",
        success: function (message) {
            if (!message) {
                loadAllBooks();
                $('#bookModal').modal('hide');
            }
            else {
                alert(message);
            }
        }
    });
}

function createBookData(id) {
    json = {
        "title": $('#bookModalBookTitle').val(),
        "author": $('#bookModalAuthor').val(),
        "publisher": $('#bookModalPublisher').val(),
        "publicationYear": $('#bookModalPublicationYear').val(),
        "language": $('#bookModalLanguage').val(),
        "series": $('#bookModalSeries').val(),
        "number": $('#bookModalNumber').val(),
        "category": $('#bookModalCategory').val(),
        "copies": $('#bookModalCopies').val(),
        "summery": $('#bookModalSummery').val()
    };

    if (id) {
        json.id = id
    }

    return json;
}

function updateBook(id) {
    $.ajax({
        dataType: "json",
        data: createBookData(id),
        url: "Books/EditBook",
        success: function (message) {
            if (!message) {
                loadAllBooks();
                $('#bookModal').modal('hide');           
            }
            else {
                alert(message);
            }
        }
    });
}


function openBookModal(id) {
    if (id) {
        $.ajax({
            dataType: "json",
            url: "Books/getBookById",
            data: { "id": id },
            success: function (book) {
                debugger;
                $('#bookModalTitle').text("Edit");

                $('#bookModalBookTitle').val(book.title);
                $('#bookModalAuthor').val(book.author);
                $('#bookModalPublisher').val(book.publisher);
                $('#bookModalPublicationYear').val(book.publicationYear);
                $('#bookModalLanguage').val(book.language);
                $('#bookModalSeries').val(book.series);
                $('#bookModalNumber').val(book.number);
                $('#bookModalCategory').val(book.category);
                $('#bookModalCopies').val(book.copies);
                $('#bookModalSummery').val(book.summery);

                $('#createBook').hide();
                $('#saveBookChanges').click(function () {
                    updateBook(id)
                });
                $('#saveBookChanges').show();
            }
        });
    }
    else {

        $('#bookModalTitle').text("Create");
        $(".bookInput").val("");
        $('#createBook').show();
        $('#saveBookChanges').hide();
    }
    $('#bookModal').modal('show');
}

function loadBestBooks() {
    $.ajax({
        url: "Books/getBestBooksJson",
        type: "GET",
        success: function (data) {
            debugger;
            $("#bestBooksTableBody").empty();
            $.each(data, function (idx, obj) {
                row = "<tr><td>" + obj.bookTitle + "</td><td>" + obj.bookAuthor +
                 "</td><td>" + obj.bookSeries + "</td><td>" + obj.bookNumber +
                 "</td><td>" + obj.bookPublicationYear + "</td><td>" + obj.bookAvailableCopies +
                 "</td><td><a onclick='openBookDetailsModal(" + obj.bookId + ")')>See Details</a>"
                + "</td></tr>";
                $("#bestBooksTableBody").append(row);
            });
        }
    });
}


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
                        "onclick='openBookModal(" + obj.id + ")'" +
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
                debugger;
                $('#detailsBookSeries').text('(' + book.series + ((book.number) ? ' #' + book.number : "") + ')');
                $('#detailsBookAuthor').text('by ' + book.author);
                $('#detailsCopies').text(book.copies + ' copies available')
                $('#detailsBookSummery').text((book.summery == null) ? "" : book.summery);
                $('#detailsBookPublishData').text('published at ' + book.publicationYear + ' by ' + book.publisher + ', ' + book.language);
                $('#detailsBookCategory').text(book.category);
                $('#borrowBook').click(function () {
                    borrowBook(book.id);
                });
            }});

        $('#bookDetailsModal').modal('show');
};


function borrowBook(bookId) {
    $.ajax({
        dataType: "json",
        data: { "bookId": bookId },
        url: "Borrow/createBorrow",
        success: function (message) {
            if (!message) {
                $('#bookDetailsModal').modal('hide');
                loadBestBooks();
                loadAllBooks();
                showAllBooks();
            }
            else {
                alert(message);
            }
        }
    });
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