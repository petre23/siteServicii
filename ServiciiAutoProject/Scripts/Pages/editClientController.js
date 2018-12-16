var editClientController = {
    clientId: null,
    saveClient: function (client) {
        $.ajax({
            type: 'post',
            dataType: 'json',
            url: "/Client/SaveClient",
            data: client,
            success: function (res) {

            },
            error: function (jqXHR, textStatus, exception, errorThrown) {
                $("#errorDialog").html(JSON.parse(jqXHR.responseText).error);
                setTimeout(function () { $("#errorDialog").dialog("open"); }, 1000);
            }
        });
    },
    getClient: function () {
        $.ajax({
            type: 'post',
            dataType: 'json',
            url: "/Client/GetClientById",
            data: {
                clientId: this.getParameterByName("clientId", new URL(window.location.href))
            },
            success: function (res) {
                editClientController.clientId = res.client.Id;
                var client = res.client;
                editClientController.setClientDetails(client);
                editClientController.initControls(client);
            },
            error: function (jqXHR, textStatus, exception, errorThrown) {
                $("#errorDialog").html(JSON.parse(jqXHR.responseText).error);
                $("#errorDialog").dialog("open");
            }
        });
    },
    setClientDetails: function (client) {
        $("#name").val(client.Name);
        $("#email").val(client.Email);
        $("#phoneNumber").val(client.PhoneNumber);
    },
    getParameterByName: function (name, url) {
        if (!url) url = window.location.href;
        name = name.replace(/[\[\]]/g, "\\$&");
        var regex = new RegExp("[?&]" + name + "(=([^&#]*)|&|#|$)"),
            results = regex.exec(url);
        if (!results) return null;
        if (!results[2]) return '';
        return decodeURIComponent(results[2].replace(/\+/g, " "));
    },
    setupNewClientInfo: function () {
        this.client = null;
        $("#name").val("");
        $("#email").val("");
        $("#phoneNumber").val("");
    },
    initClient: function () {
        var url = new URL(window.location.href);
        var clientId = editClientController.getParameterByName("clientId", new URL(window.location.href));
        if (clientId) {
            editClientController.getClient();
        } else {
            editClientController.setupNewClientInfo();
            setTimeout(function () { editClientController.initControls(); }, 0);
        }
    },
    initControls: function (client) {
        $('#saveClient').on('submit', function (e) {
            e.preventDefault();
        });
    },
    cancelEdit: function () {
        if (this.confirmCancel()) {
            window.location = "/Client/Index";
        }
    },
    validateAndSaveClient: function () {
        var clientInfo = {
            Id: this.clientId,
            ClientName: $("#name").val(),
            Email: $("#email").val(),
            PhoneNumber: $("#phoneNumber").val()
        };
        this.saveClient(clientInfo);
    },
    confirmCancel: function () {
        var txt;
        var r = confirm(" Sunteti sigur ca vreti sa anulati aceasta operatie? \r\n Toate modificarile for fi pierdute!");
        if (r == true) {
            return true;
        } else {
            return false;
        }
    },
};
editClientController.initClient();