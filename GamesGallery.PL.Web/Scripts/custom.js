/// <reference path="jquery-3.6.0.js" />
/// <reference path="bootstrap.js" />
/// <reference path="jquery.validate.js" />
/// <reference path="jquery.unobtrusive-ajax.js" />
/// <reference path="jquery.validate.unobtrusive.js" />

//const { isEmptyObject } = require("jquery");
//const { search } = require("modernizr");


//Add New Category from Add Game View
function AddCategoryFun() {
    var category = $('#AddCategory').val();
    if (category == "") {
        alert("Please provide a title for category!");
    }
    else if (category.length < 3 || category.length > 50) {
        alert("Please provide a valid title for category!");
    }
    else {
        $.ajax({
            url: "/Admin/Games/AddCategory?category=" + category,
            method: "POST",
            success: function (data) {
                data = JSON.parse(data);
                if (data.Id != 0)
                    $('#CategoriesId').append(`<option value="${data.Id}">${data.Title} </option>`);
                alert(data.Message);
            },
            error: function (err) {
                alert("Something went wrong :(");
            }
        })
    }
    $("#AddCategory").val("");
}

//Image Validation
function ImageValidation(elementID) {
    var image = document.getElementById(elementID);
    if (image == null)
        console.log("Element ID not found : " + elementID);
    else {
        var imagePath = image.value;
        var allowedExtensions = /(\.jpg|\.jpeg|\.png|\.gif)$/i;
        if (!allowedExtensions.exec(imagePath)) {
            alert("Invalid image format, supported formats are : .jpg / .jpeg / .png / .gif");
            image.value = "";
            return false;
        }
        else {
            var imageFileLength = image.files.length;
            if (imageFileLength > 0) {
                for (var i = 0; i <= imageFileLength - 1; i++) {
                    var imageSize = image.files.item(i).size;
                    var file = Math.round((imageSize / 1024));
                    if (file >= 4096 || file < 10) {
                        alert("Choose an image between 10KB to 4MB of file size!");
                        image.value = "";
                    }
                }
            }
        }
    }
}

//Image Validation Multiple
function ImageValidationMultiple(elementID, limit) {
    var image = document.getElementById(elementID);
    if (image == null)
        console.log("Element ID not found : " + elementID);
    else if (image.files.length > limit) {
        image.value = "";
        alert("Choose an maximum of " + limit + " images!");
    }
    else {
        var imagePath = image.value;
        var allowedExtensions = /(\.jpg|\.jpeg|\.png|\.gif)$/i;
        if (!allowedExtensions.exec(imagePath)) {
            alert("Invalid image format, supported formats are : .jpg / .jpeg / .png / .gif");
            image.value = "";
            return false;
        }
        else {
            var imageFileLength = image.files.length;
            if (imageFileLength > 0) {
                for (var i = 0; i <= imageFileLength - 1; i++) {
                    var imageSize = image.files.item(i).size;
                    var file = Math.round((imageSize / 1024));
                    if (file >= 4096 || file < 50) {
                        alert("Choose images between 50KB to 4MB of file size!");
                        image.value = "";
                    }
                }
            }
        }
    }
}

//Index Checkbox
function CheckAllCheckbox(element) {
    if (element.checked == true)
        $("input[name='IdsToDelete']").prop('checked', true);
    else
        $("input[name='IdsToDelete']").prop('checked', false);
}
function CheckUncheckThisCheckbox() {
    if ($("input[name='IdsToDelete']").length == $("input[name='IdsToDelete']:checked").length) {
        $("#checkAll").prop('checked', true);
    }
    else {
        $("#checkAll").prop('checked', false);
    }
}
function validate(form) {
    var selectedRows = $("input[name='IdsToDelete']:checked").length;
    if (selectedRows == 0) {
        alert("Please select at least one row to delete. ");
        return false;
    }
    else {
        return confirm("Are you sure you want to delete " + selectedRows + " row(s)?");
    }
}

//Search Validation for User
function validateSearch(form) {
    var search = $("#SearchString").val().length;

    if (search < 1) {
        return false;
    }
    else {
        return confirm("Are you sure you want to delete " + selectedRows + " row(s)?");
    }
}

//Download Button Clicked
function DownloadGame(id) {
    $.ajax({
        url: "/Games/TotalDownloadsIncrement?id=" + id,
        method: "POST"
    })
}












