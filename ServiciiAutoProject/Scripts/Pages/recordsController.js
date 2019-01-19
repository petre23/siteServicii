/// <reference path="../jsgrid/jsgrid.js" />
/// <reference path="../jsgrid/jsgrid.min.js" />

var recordsController =
{
    selectedRecord: "",
    page: 1,
    pageSize: 100,
    filterSelectedClient: null,
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
    initGrid: function () {
        $("#editButton").prop('disabled', true);
        $("#deleteButton").prop('disabled', true);

        $("#recordsGrid").jsGrid({
            width: "100%",
            height: "68vh",

            inserting: false,
            editing: false,
            sorting: true,
            paging: true,
            autoload: true,
            pageLoading: true,
            pageSize: recordsController.pageSize,
            pageIndex: recordsController.page,
            rowClick: function (args) {
                $("#recordsGrid tr").removeClass("selected-row");
                $selectedRow = $(args.event.target).closest("tr");
                $selectedRow.addClass("selected-row");

                recordsController.selectedRecord = args.item.Id;

                $("#editButton").prop('disabled', false);
                $("#deleteButton").prop('disabled', false);
            },
            controller: {
                loadData: function (filters) {

                    filters.ExpirationDateFrom = recordsController.getCorrectDateFormat($("#expirationDateFrom").val());
                    filters.ExpirationDateUntil = recordsController.getCorrectDateFormat($("#expirationDateUntil").val());
                    filters.ClientId = recordsController.filterSelectedClient;
                    filters.RecordType = $("#recordType").val();
                    filters.PhoneNumber = $("#phoneNumber").val();
                    filters.CarRegistrationNumber = $("#carRegistrationNumber").val();

                    var deferred = $.Deferred();
                    $.ajax({
                        type: "post",
                        url: "/Records/GetRecords",
                        data: filters,
                        dataType: "json",
                        success: function (res) {
                            var dataForRecords = {
                                data: res.records,
                                itemsCount: res.records && res.records.length > 0 ? res.records[0].TotalRows : 0
                            };
                            records = res.records;
                            deferred.resolve(dataForRecords);
                        }
                    });
                    return deferred.promise();
                }
            },
            fields: [
                { name: "Id", title: 'Id', type: "text", css: "hide" },
                { name: "ClientName", title: 'Nume Client', type: "text", width: 80 },
                { name: "PhoneNumber", title: 'Telefon', type: "text", width: 80 },
                { name: "CarRegistartionNumber", title: 'Numar Inmatriculare', type: "text", width: 80 },
                { name: "ExpirationDateString", title: 'Data expirare', type: "text", width: 80 },
                { name: "RecordTypeName", title: 'Tip inregistrare', type: "text", width: 80 },
                { name: "AdditionalInfo", title: 'Informatii aditionale', type: "text", width: 80 }
            ]
        });
    },
    getCorrectDateFormat: function (dateString) {
        if (dateString) {
            var parts = dateString.split("/");
            var date = new Date(parts[2], parts[1] - 1, parts[0]);
            if (date) {
                return date.toISOString();
            }
        }
        return "";
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
                data: { recordId: recordsController.selectedRecord },
                success: function (res) {
                    recordsController.initGrid();
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
    },
    fileSelected: function () {
        var csvFilePath = $("#selectFile").val();
        var extension = csvFilePath.substr((csvFilePath.lastIndexOf('.') + 1));
        if (extension !== "csv") {
            alert("Acest tip de fisier nu este permis. \r\nVa rugam sa alegeti un fisier de tipul .csv!");
            $('#uploadFile').prop('disabled', true);
            return;
        }

        $('#uploadFile').prop('disabled', false);
    },
    showLoadingScreen: function () {
       $("#modalButton").click();
    },
    initPage: function () {
        this.initGrid();

        $("#expirationDateFrom").datepicker({ dateFormat: 'dd/mm/yy', changeMonth: true, changeYear: true });
        $("#expirationDateUntil").datepicker({ dateFormat: 'dd/mm/yy', changeMonth: true, changeYear: true });

        $('input[list]').on('input', function (e) {
            var $input = $(e.target),
                $options = $('#' + $input.attr('list') + ' option'),
                label = $input.val();

            for (var i = 0; i < $options.length; i++) {
                var $option = $options.eq(i);

                if ($.trim($option.text()) === $.trim(label)) {
                    recordsController.filterSelectedClient = $option.attr('data-value');
                    break;
                }
            }
        });
    },
    resetFilters: function () {
        $("#expirationDateFrom").val("");
        $("#expirationDateUntil").val("");
        recordsController.filterSelectedClient = null;
        $("#clientName").val("");
        $("#recordType").val("");
        $("#phoneNumber").val("");
        $("#carRegistrationNumber").val("");
        recordsController.initGrid();
    }
};
recordsController.initPage();