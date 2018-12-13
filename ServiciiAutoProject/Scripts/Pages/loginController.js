var loginController =
    {
        login: function () {
            $.ajax({
                type: 'post',
                dataType: 'json',
                data: {
                    Username: $("#email").val(),
                    Password: $("#password").val()
                },
                url: "/Login/Login",
                success: function (res) {
                    if (res.userName && res.userName != '') {
                        console.log(res.userName);
                        window.location = '/Records/Index';
                    } else {
                        alert('Email sau parola invalide');
                    }
                },
                error: function (jqXHR, textStatus, exception, errorThrown) {
                    $("#errorDialog").html(JSON.parse(jqXHR.responseText).error);
                    $("#errorDialog").dialog("open");
                }
            });
        },
        validateLogin: function ()
        {
            if (loginForm.checkValidity())
            {
                this.login();
            }
        },
        logout: function () {
            $.ajax({
                type: 'post',
                dataType: 'json',
                url: "/Login/Logout",
                success: function (res) {
                   
                }
            });   
        }
    };