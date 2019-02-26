/// <reference path="../jsgrid/jsgrid.js" />
/// <reference path="../jsgrid/jsgrid.min.js" />

var clientsController =
{
    selectedClient: "",
    getClients: function () {
        $.ajax({
            type: 'post',
            dataType: 'json',
            url: "/Client/GetClients",
            success: function (res) {
                clientsController.initGrid(res.clients);
            },
            error: function (jqXHR, textStatus, exception, errorThrown) {
                $("#errorDialog").html(JSON.parse(jqXHR.responseText).error);
                $("#errorDialog").dialog("open");
            }
        });
    },
    initGrid: function (clients) {
        $("#editButton").prop('disabled', true);
        $("#deleteButton").prop('disabled', true);

        $("#clientsGrid").jsGrid({
            width: "100%",
            height: "80vh",

            inserting: false,
            editing: false,
            sorting: true,
            paging: false,
            rowClick: function (args) {
                $("#clientsGrid tr").removeClass("selected-row");
                $selectedRow = $(args.event.target).closest("tr");
                $selectedRow.addClass("selected-row");

                clientsController.selectedClient = args.item.Id;

                $("#editButton").prop('disabled', false);
                $("#deleteButton").prop('disabled', false);
            },
            data: clients,
            fields: [
                { name: "Id", title: 'Id', type: "text", css: "hide" },
                { name: "Name", title: 'Nume client', type: "text", width: 80 },
                { name: "PhoneNumber", title: 'Numar telefon', type: "text", width: 80 },
                { name: "Email", title: 'Email', type: "text", width: 80 }
            ]
        });
    },
    addNewClient: function () {
        window.location = "/Client/EditClient";
    },
    editClient: function () {
        if (clientsController.selectedClient) {
            window.location = "/Client/EditClient?clientId=" + clientsController.selectedClient;
        }
    },
    deleteClient: function () {
        if (this.confirmDeleteClient()) {
            $.ajax({
                type: 'post',
                dataType: 'json',
                url: "/Client/DeleteClient",
                data: { clientId: clientsController.selectedClient },
                success: function (res) {
                    clientsController.getClients();
                },
                error: function (jqXHR, textStatus, exception, errorThrown) {
                    $("#errorDialog").html(JSON.parse(jqXHR.responseText).error);
                    $("#errorDialog").dialog("open");
                }
            });
        }
    },
    confirmDeleteClient: function () {
        var txt;
        var r = confirm("Sunteti sigur ca vreti sa stergeti acesta client?\nToate inregistrarile care apartin acestui client vor fi de asemenea sterse!");
        if (r == true) {
            return true;
        } else {
            return false;
        }
    }
};
clientsController.getClients();