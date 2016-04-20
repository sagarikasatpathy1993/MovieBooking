$(document).ready(function () {
    var emailExp = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;
    
   
    $('#btnlogin').click(function ()
    {
        var email = $('#mail').val();
        var password = $('#password').val();
        if (email == "" || !email.match(emailExp))
        {
            $('#msg').text("Enter  emailId");
            return false;
        }
        else if(password=="")
          {
            $('#pass').text("Enter Password");
            return false;
        }
        else if (password.length < 5)
        {
            $('#pass').text("Password should be more than 5 Characters");
            return false;
        }
        return true;
    });


   
});
