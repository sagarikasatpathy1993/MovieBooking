$(document).ready(function () {
    var emailExp = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;

    $('#btnReg').click(function ()
    {
        var email = $('#email').val();
        var password = $('#pwd').val();
        var mobilenumber = $('#mnum').val();
        var firstname = $('#firstname').val();
        if (firstname == "")
        {
            $('#firstnamemsg').text("firstname cannot be blank");
            return false;
        }
        else if (email == "" || !email.match(emailExp))
        {
            $('#emailmsg').text("Enter  emailId");
            return false;
        }
        else if (password == "")
        {
            $('#pmsg').text("Enter password");
            return false;
        }
        else if (password.length < 5)
        {
            $('#pmsg').text("Password must be 5 or more than 5 characters");
            return false;
        }
        
        return true;
    });
});