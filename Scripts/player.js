

function ValidateRegistrationForm() {

    var flagError = false;

   

    //Role 
    if ($("#role").val() == "" || $("#role").val() == undefined) {

        flagError = true;
        $("#roleError").css("display", "block");
    }
    else {
        var roleVal = $("#role").val();

        if (roleVal == 1 && IsCoachAdded == "true") {
            flagError = true;
            $("#roleError").css("display", "block").html("Coach is already added");
        }
        else {
            $("#roleError").css("display", "none");
        }
    }

    // First Name
    if ($("#firstname").val() == "" || $("#firstname").val() == undefined) {

        flagError = true;
        $("#firstnameError").css("display", "block");
    }
    else {
        $("#firstnameError").css("display", "none");
    }

    // First Name
    if ($("#lastname").val() == "" || $("#lastname").val() == undefined) {

        flagError = true;
        $("#lastnameError").css("display", "block");
    }
    else {
        $("#lastnameError").css("display", "none");
    }

    // contact number
    if ($("#contactnumber").val() == "" || $("#contactnumber").val() == undefined) {

        flagError = true;
        $("#contactnumberError").css("display", "block");
        $("#contactnumberInvalidError").css("display", "none");

    }
    else {
        if (!validateContactNumber($("#contactnumber").val())) {
            flagError = true;
            $("#contactnumberError").css("display", "none");
            $("#contactnumberInvalidError").css("display", "block");
        }
        else {
            $("#contactnumberError").css("display", "none");
            $("#contactnumberInvalidError").css("display", "none");
        }
    }

    // Email
    if ($("#email").val() == "" || $("#email").val() == undefined) {

        flagError = true;
        $("#emailError").css("display", "block");
        $("#emailInvalidError").css("display", "none");

    }
    else {
        if (!ValidateEmail($("#email").val())) {
            flagError = true;
            $("#emailError").css("display", "none");
            $("#emailInvalidError").css("display", "block");
        }
        else {
            $("#emailError").css("display", "none");
            $("#emailInvalidError").css("display", "none");
        }
    }

    if (flagError) {
        return false;
    }
    else {
        return true;
    }

}


function ValidateEmail(email) {
    var regex = /^([a-zA-Z0-9_.+-])+\@(([a-zA-Z0-9-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    return regex.test(email);
}

function validateContactNumber(inputNumber) {
    var regex = /^\d{10}$/;
    return regex.test(inputNumber);
}
