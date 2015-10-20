/// <reference path="borrower.js" />
$(document).ready(function () {
    $("a[href='#borrower-library-card']").on('click', function () {
        showBorrowerProfile();
        showOpenBorrows();
    });

    $("a[href='#admin-manage-borrowers']").on('click', function () {
        loadAllBorrowers();
    });

    $("#showBorrowerHistory").click(function () {
        openBorrowsHistoryModal();
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

function loadAllBorrowers() {
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
                "</td><td><span title='Details' " +
                        "onclick='openBorrowerDetailsModal(" + obj.id + ")'" +
                         "class='glyphicon glyphicon-list-alt'></span>" +
                 "| <span title='Edit' " +
                        "onclick='openEditBorrowerModal(" + obj.id + ")'" +
                         "class='glyphicon glyphicon-edit'></span>" +
                 "| <span title='Delete' " +
                        "onclick='adminDeleteBorrower(" + obj.id + ")'" +
                         "class='glyphicon glyphicon-trash'></span>"
                + "</td></tr>";
                $("#adminBorrowersTableBody").append(row);
            });
        }
    });
}


function adminDeleteBorrower(borrowerId) {
    bootbox.confirm("Are you sure you want to delete this book?", function (result) {
        if (result == true) {
            debugger;
            $.ajax({
                dataType: "json",
                data: { "borrowerID": borrowerID },
                url: "Borrower/deleteBorrower",
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


function returnBookBorrower(borrowSeq) {
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
                         "</td><td>" + "<a onclick='returnBookBorrower(" + obj.borrowSeqNumber + ")'>Return Book</a>" + "</td></tr>";
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

    function openBorrowerDetailsModal(id) {
        $.ajax({
            dataType: "json",
            url: "Borrower/getBorrower",
            data: { "borrowerId": id },
            success: function (borrower) {
                debugger;
                $('#detialsBorrowerName').text(borrower.firstName + " " + borrower.lastName);
                $('#detialsBorrowerGender').text(borrower.sex);
                $('#detialsBorrowerAddress').text(borrower.address);
                $('#detialsBorrowerPhone').text(borrower.phone);
                $('#detialsBorrowerMail').text(borrower.mail);

                showMap(borrower);
            }
        });

        $('#borrowerDetailsModal').modal('show');
    };

    function showMap(borrower) {
        var geocoder;
        var map;
        
        geocoder = new google.maps.Geocoder();
        var latlng = new google.maps.LatLng(-34.397, 150.644);
        var mapOptions = {
            zoom: 8,
            center: latlng
        }
        map = new google.maps.Map(document.getElementById("map"), mapOptions);

        codeAddress(borrower, geocoder, map);
    }

    function codeAddress(borrower, geocoder, map) {
        geocoder.geocode({ 'address': borrower.address }, function (results, status) {
            if (status == google.maps.GeocoderStatus.OK) {
                var latitude = new google.maps.LatLng(results[0].geometry.location.lat(), results[0].geometry.location.lng());
                /*latitude.lat = results[0].geometry.location.lat();
                latitude.lng = results[0].geometry.location.lng();*/

                map.setCenter(latitude);
                var marker = new google.maps.Marker({
                    map: map,
                    position: latitude,
                    title: borrower.firstName.concat(' ').concat(borrower.lastName)
                });
            } else {
                alert("Geocode was not successful for the following reason: " + status);
            }
        });
        google.maps.event.trigger(map, 'resize');
    }


