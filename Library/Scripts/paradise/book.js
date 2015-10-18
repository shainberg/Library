
$(document).ready(function () {
    $("a[href='#all-books']").on('click', function () {
        showAllBooks();
    });
    $("a[href='#admin-manage-books']").on('click', function () {
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
                            "onclick='openDeleteBookValidation("+obj.id+")'" +
                             "class='glyphicon glyphicon-trash'></span>"
                    +"</td></tr>";
                    $("#adminBooksTableBody").append(row);
                });

            }
        });
    });
});

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

        $('#detailsModal').modal('show');
};


function showAllBooks() {
    $.ajax({
        url: "Books/getAllBooks",
        type: "GET",
        success: function (data) {
            debugger;
            if ($.fn.DataTable.isDataTable($('#books'))) {
                var oTable = $('#books').dataTable();
            }
            else {
                var oTable = $('#books').dataTable({
                    "autoWidth": false,
                    "ordering": false,
                    "columnDefs": [{
                        "targets": [6],
                        "mData":"id",
                        "mRender": function (data, type, full) {
                            debugger;
                            return "<a onclick='openBookDetailsModal("+full[5]+")')>See Details</a>";
                        }
                    }]
                });
            }
            oTable.fnClearTable();

            var booksJson = [];

            $.each(data, function (idx, obj) {
                booksJson.push([obj.title, obj.author, obj.series, obj.number, obj.publicationYear,obj.copies]);
            });

            oTable.fnAddData(booksJson);
        }
    });
}