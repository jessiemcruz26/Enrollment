$(function () {
    $("#datepicker").datepicker();

    $("#BtnSearch").click(function () {
        if ($('#txtInstructorNumber').val().trim() != "") {
            $("#InstructorSearchForm").submit();
        }
    });

    $("#BtnSaveInstructor").click(function () {
        if (IsFormValid()) {
            $("#InstructorForm").submit();
        }
    });

    function IsFormValid() {
        $('#Error').empty();

        if ($('#InstructorNumber').val().trim() == "") {
            $('#InstructorNumber').addClass('errorClass');
            $('#Error').append("Error: Instructor Number should have a value <br/>");
        }
        else {
            $('#InstructorNumber').removeClass('errorClass');
        }

        if ($('#FirstName').val().trim() == "") {
            $('#FirstName').addClass('errorClass');
            $('#Error').append("Error: First name is required <br/> ");
        }
        else {
            $('#FirstName').removeClass('errorClass');
        }

        if ($('#LastName').val().trim() == "") {
            $('#LastName').addClass('errorClass');
            $('#Error').append("Error: Last name is required <br/>");
        }
        else {
            $('#LastName').removeClass('errorClass');
        }

        if ($('#Error').text() != "")
            return false;

        return true;
    }
});