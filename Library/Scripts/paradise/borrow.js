$(document).ready(function () {
    $("a[href='#admin-manage-borrows']").on('click', function () {
        loadAllBorrows();
    });
});

function loadAllBorrows() {
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
                            "| <span title='Delete' " +
                            "onclick='adminDeleteBorrow(" + obj.borrowSeqNumber + ")'" +
                             "class='glyphicon glyphicon-trash'></span>"
                    + "</td></tr>";
                $("#adminBorrowsTableBody").append(row);
            });
            initDataTable();
        }
    });
}




function returnBookAdmin(borrowSeq) {
    $.ajax({
        dataType: "json",
        data: { "borrowSeq": borrowSeq },
        url: "Borrow/returnBorrow",
        success: function (message) {
            if (!message) {
                $('#borrowDetailsModal').modal('hide');
                loadAllBorrows();
            }
            else {
                alert(message);
            }
        }
    });
};


function openBorrowDetailsModal(id) {
    $.ajax({
        dataType: "json",
        url: "Borrow/getBorrow",
        data: { "seqNum": id },
        success: function (borrow) {
            debugger;
            $('#detialsBorrowBorrower').text(borrow.borrowerFirstName + " " + borrow.borrowerLastName);
            $('#detialsBorrowBookTitle').text(borrow.bookTitle);
            $('#detialsBorrowBookAuthor').text(borrow.bookAuthor);
            $('#detialsBorrowBorrowDate').text(borrow.borrowDate)
            $('#detialsBorrowReturnDate').text(borrow.returnDate);
            $('#returnBookButton').click(function () {
                returnBookAdmin(id)
            });
        }
    });

    $('#borrowDetailsModal').modal('show');
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



(function ($) {
    $.fn.dataTableExt.sErrMode = 'throw';
  
    /*
     * Function: fnGetColumnData
     * Purpose:  Return an array of table values from a particular column.
     * Returns:  array string: 1d data array
     * Inputs:   object:oSettings - dataTable settings object. This is always the last argument past to the function
     *           int:iColumn - the id of the column to extract the data from
     *           bool:bUnique - optional - if set to false duplicated values are not filtered out
     *           bool:bFiltered - optional - if set to false all the table data is used (not only the filtered)
     *           bool:bIgnoreEmpty - optional - if set to false empty values are not filtered from the result array
     */
    $.fn.dataTableExt.oApi.fnGetColumnData = function (oSettings, iColumn, bUnique, bFiltered, bIgnoreEmpty) {
        debugger;
        // check that we have a column id
        if (typeof iColumn == "undefined") return new Array();

        // by default we only want unique data
        if (typeof bUnique == "undefined") bUnique = true;

        // by default we do want to only look at filtered data
        if (typeof bFiltered == "undefined") bFiltered = true;

        // by default we do not want to include empty values
        if (typeof bIgnoreEmpty == "undefined") bIgnoreEmpty = true;

        // list of rows which we're going to loop through
        var aiRows;

        // use only filtered rows
        if (bFiltered == true) aiRows = oSettings.aiDisplay;
            // use all rows
        else aiRows = oSettings.aiDisplayMaster; // all row numbers

        // set up data array   
        var asResultData = new Array();

        for (var i = 0, c = aiRows.length; i < c; i++) {
            iRow = aiRows[i];
            var aData = this.fnGetData(iRow);
            var sValue = aData[iColumn];

            // ignore empty values?
            if (bIgnoreEmpty == true && sValue.length == 0) continue;

                // ignore unique values?
            else if (bUnique == true && jQuery.inArray(sValue, asResultData) > -1) continue;

                // else push the value onto the result data array
            else asResultData.push(sValue);
        }

        return asResultData;
    }
}(jQuery));

function fnCreateInput(colIndex) {
    debugger;
    var r = '<input type="text" ';
    
    
  
    r += 'placeholder="Search ' + $('#adminBorrowsTable thead th').eq(colIndex).text() + '"/>';
    
    return r;
}

function initDataTable() {
    debugger;
    /* Initialise the DataTable */
    var oTable = $('#adminBorrowsTable').dataTable({
        "oLanguage": {
            "sSearch": "Search all columns:"
        }
    });

    /* Add a select menu for each TH element in the table footer */
    $("tfoot th").each(function (i) {
        if (i < 3)
        {
            this.innerHTML = fnCreateInput(i);
            $('input[type="text"]', this).on('keyup', function () {
                debugger;
                oTable.fnFilter($(this).val(), i);
            });
        }
        else
        {
            $("tfoot th")[i].innerHTML = "";
        }
    });
}