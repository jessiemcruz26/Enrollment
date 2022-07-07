$(function () {
    $("#datepicker").datepicker();

    var ids = [];
    $(document).on('click', '.btn_Delete', function () {
        $(this).parents('tr').remove();
        var _assoc = $(this).parents('tr').find('.assoc').val();
        ids.push(_assoc);
        $('#removeClassIds').val(ids);
    });

    $("#BtnSearchStudent").click(function () {
        if ($('#txtStudentNumber').val().trim() != "") {
            $("#StudentSearchForm").submit();
        }
    });

    $("#BtnSave").click(function () {
        if (IsFormValid()) {
            $("#StudentForm").submit();
        }
    });

    function IsFormValid() {
        $('#Error').empty();

        if ($('#StudentNumber').val().trim() == "") {
            $('#StudentNumber').addClass('errorClass');
            $('#Error').append("Error: Student Number should have a value <br />");
        }
        else {
            $('#StudentNumber').removeClass('errorClass');
        }

        if ($('#FirstName').val().trim() == "") {
            $('#FirstName').addClass('errorClass');
            $('#Error').append("Error: First name is required <br /> ");
        }
        else {
            $('#FirstName').removeClass('errorClass');
        }

        if ($('#LastName').val().trim() == "") {
            $('#LastName').addClass('errorClass');
            $('#Error').append("Error: Last name is required <br />");
        }
        else {
            $('#LastName').removeClass('errorClass');
        }

        if ($('#Error').text() != "")
            return false;

        return true;
    }
});
