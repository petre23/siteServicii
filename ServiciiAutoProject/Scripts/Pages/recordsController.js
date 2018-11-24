/// <reference path="../jsgrid/jsgrid.js" />
/// <reference path="../jsgrid/jsgrid.min.js" />

var recordsController =
{
    selectedRecord: "",
    getRecords: function () {
        $.ajax({
            type: 'post',
            dataType: 'json',
            url: "/Records/GetRecords",
            success: function (res) {
                recordsController.initGrid(res.records);
            },
            error: function (jqXHR, textStatus, exception, errorThrown) {
                $("#errorDialog").html(JSON.parse(jqXHR.responseText).error);
                $("#errorDialog").dialog("open");
            }
        });
    },
    initGrid: function (records) {
        $("#editButton").prop('disabled', true);
        $("#deleteButton").prop('disabled', true);

        $("#recordsGrid").jsGrid({
            width: "100%",
            height: "80vh",

            inserting: false,
            editing: false,
            sorting: true,
            paging: false,
            rowClick: function (args) {
                $("#recordsGrid tr").removeClass("selected-row");
                $selectedRow = $(args.event.target).closest("tr");
                $selectedRow.addClass("selected-row");

                recordsController.selectedRecord = args.item.Id;

                $("#editButton").prop('disabled', false);
                $("#deleteButton").prop('disabled', false);
            },
            data: records,
            fields: [
                { name: "Id", title: 'Id', type: "text", css: "hide" },
                { name: "CarRegistartionNumber", title: 'Numar Inmatriculare', type: "text", width: 80 },
                { name: "ExpirationDateString", title: 'Data expirare', type: "text", width: 80 },
                { name: "RecordTypeName", title: 'Tip inregistrare', type: "text", width: 80 },
                { name: "VehicleTypeName", title: 'Tip vehicul', type: "text", width: 80 },
                { name: "AdditionalInfo", title: 'Informatii aditionale', type: "text", width: 80 }
            ]
        });
    },
    addNewRecord: function () {
        window.location = "/Records/EditRecord";
    },
    editRecord: function () {
        if (recordsController.selectedRecord) {
            window.location = "/Records/EditRecord?recordId=" + recordsController.selectedRecord;
        }
    },
    deleteRercord: function () {
        if (this.confirmDeleteRecord()) {
            $.ajax({
                type: 'post',
                dataType: 'json',
                url: "/Records/DeleteRecord",
                data: { recordId: editRecordController.selectedRecord },
                success: function (res) {
                    recordsController.getRecords();
                },
                error: function (jqXHR, textStatus, exception, errorThrown) {
                    $("#errorDialog").html(JSON.parse(jqXHR.responseText).error);
                    $("#errorDialog").dialog("open");
                }
            });
        }
    },
    confirmDeleteRecord: function () {
        var txt;
        var r = confirm("Sunteti sigur ca vreti sa stergeti acesta inregistrare?");
        if (r == true) {
            return true;
        } else {
            return false;
        }
    }
};
recordsController.getRecords();