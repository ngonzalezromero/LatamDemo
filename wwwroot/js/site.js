$(document).ready(function() {

    $('#divOrder').hide();
    $('#divCategory').hide();



    $("#selectDataSource").change(function() {
        $.ajax({
            type: "GET",
            url: 'ProductsList/' + this.value,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function(data) {

                $('#tblList tbody').remove();
                var trHTML = '<tbody>';
                if (data != undefined) {
                    $.each(data, function(i, item) {
                        trHTML += '<tr><td>' + item.Id + '</td><td>' + item.Name + '</td></tr>';
                    });
                }
                trHTML += '</tbody>';
                $('#tblList ').append(trHTML);
            },
            error: function(msg) {
                alert('error');
            }
        });
    });

    $("#selectSourceCategories").change(function() {
        $.ajax({
            type: "GET",
            url: 'CategoriesList/' + this.value,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function(data) {

                $('#cblist').html('');
                var trHTML = '<div class="form-check">';
                if (data != undefined) {
                    $('#divCategory').show();
                    $.each(data, function(i, item) {
                        trHTML += ' <label class="form-check-label"><input class="checkCategories" type="checkbox" value="' + item.Id + '" name="categories" id="' + item.Id + '" class="form-check-input">&nbsp;' + item.Name + '</label><br />';
                    });
                } else {
                    $('#divCategory').hide();
                }
                trHTML += '</div>';
                $('#cblist').append(trHTML);

            },
            error: function(msg) {
                alert('error');
            }
        });

        $.ajax({
            type: "GET",
            url: 'OrdersList/' + this.value,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function(data) {

                $('#selectOrder').html('');
                var trHTML = '';
                if (data != undefined) {
                    $('#divOrder').show();
                    $.each(data, function(i, item) {
                        trHTML += ' <option value="' + item.Id + '">' + item.Name + '</option>';
                    });
                } else {
                    $('#divOrder').hide();
                }
                $('#selectOrder').append(trHTML);

            },
            error: function(msg) {
                alert('error');
            }
        });
    });

    $("body").on("submit", ".frmAdd", function(e) {
        e.preventDefault();

        var dataSource = $("#selectSourceCategories").val();

        if (dataSource == 0) {
            alert("Select Data Source");
            return;
        }

        var productName = $('#txtName').val();
        if (productName.length == 0) {
            alert("Select product name");
            return;
        }

        var tempCategories = new Array();
        $(".checkCategories:checked").each(function() {
            tempCategories.push({ Id: $(this).val(), Name: "" });
        });

        if (tempCategories.length == 0) {
            alert("Select categories");
            return;
        }

        var model = {};
        model.Name = productName;
        model.DataSource = dataSource;
        model.Categories = tempCategories;
        model.Order = { Id: $("#selectOrder").val(), Name: "" };

        $.ajax({
            type: "POST",
            url: "AddProduct",
            data: JSON.stringify(model),
            contentType: 'application/json',
            dataType: "json",
            success: function(result) {
                if (result.response) {
                    alert("Saved");
                } else {
                    alert(result.reason);
                }
            }
        });

    });
});