
$(document).ready(function() {
    $("a[href='#all-books']").on('click', function () {
        showAllBooks();
    });
});

function openDetailsModal(id) {
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
        }});

    $('#detailsModal').modal('show');


    //$('#detailsModal').on('shown.bs.modal', function () {
    //    book = getDetails(id);
    //    debugger;
    //    $('#detailsBookTitle').text(book.title);
    //    $('#detailsBookSeries').text('(' + book.series + ((book.number) ? ' #' + book.number : "") + ')');
    //    $('#detailsBookAuthor').text('by ' + book.author);
    //    $('#detailsCopies').text(book.copies + ' copies available')
    //    $('#detailsBookSummery').text(book.summery);
    //    $('#detailsBookPublishData').text('published at ' + book.publicationYear + ' by ' + book.publisher + ', ' + book.language);
    //    $('#detailsBookCategory').text(book.category);

    //    $('#deleteBook').click(function () {

    //    });
    //});
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
                        "targets": [5],
                        "mData":"id",
                        "mRender": function (data, type, full) {
                            debugger;
                            return "<a onclick='openDetailsModal("+full[3]+")')>See Details</a>";
                        }
                    }]
                });
            }
            oTable.fnClearTable();

            var booksJson = [];

            $.each(data, function (idx, obj) {
                booksJson.push([obj.title, obj.author, obj.series, obj.number, obj.publicationYear]);
            });

            oTable.fnAddData(booksJson);
        }
    });
}


function getDetails(id) {
    var book;
    $.ajax({
        dataType: "json",
        url: "/Books/getDetails",
        data: { "id": id },
        async: false
    })
        .success(function (data) {
            book = data;
        });
    debugger;
    return book;
};
