var editRecordController = {
    recordId: null,
    clientId: null,
    reuseRecord: false,
    saveRecord: function (record) {
        $.ajax({
            type: 'post',
            dataType: 'json',
            url: "/Records/SaveRecord",
            data: record,
            success: function (res) {
                window.location = "/Records/Index";
            },
            error: function (jqXHR, textStatus, exception, errorThrown) {
                $("#errorDialog").html(JSON.parse(jqXHR.responseText).error);
                setTimeout(function () { $("#errorDialog").dialog("open"); }, 1000);
            }
        });
    },
    getRecord: function () {
        $.ajax({
            type: 'post',
            dataType: 'json',
            url: "/Records/GetRecordsById",
            data: {
                recordId: this.getParameterByName("recordId", new URL(window.location.href))
            },
            success: function (res) {
                editRecordController.recordId = res.record.Id;
                var record = res.record;
                editRecordController.setRecordDetails(record);
                editRecordController.initControls(record);
            },
            error: function (jqXHR, textStatus, exception, errorThrown) {
                $("#errorDialog").html(JSON.parse(jqXHR.responseText).error);
                $("#errorDialog").dialog("open");
            }
        });
    },
    setRecordDetails: function (record) {
        $("#nrImatriculare").val(record.CarRegistartionNumber);
        $("#clientName").val(record.ClientName);
        $("#email").val(record.Email);
        $("#phoneNumber").val(record.PhoneNumber);
        $("#vehicleType").val(record.VehicleTypeId);
        $("#recordType").val(record.RecordType);
        $("#expirationDate").val(record.ExpirationDateString);
        $("#additionalInfo").val(record.AdditionalInfo);
        $("#clientInformedStatus").val(record.ClientInformedStatusId);
        editRecordController.clientId = record.ClientId;
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
    setupNewDriveInfo: function () {
        this.driveId = null;

        $("#nrImatriculare").val("");
        $("#clientName").val("");
        $("#email").val("");
        $("#phoneNumber").val("");
        $("#vehicleType").val("");
        $("#recordType").val("");
        $("#expirationDate").val("");
        $("#additionalInfo").val("");
        $("#clientInformedStatus").val("");
    },
    initRecord: function () {
        var url = new URL(window.location.href);
        var recordId = editRecordController.getParameterByName("recordId", new URL(window.location.href));
        if (recordId) {
            editRecordController.getRecord();
        } else {
            editRecordController.setupNewRecordInfo();
            setTimeout(function () { editRecordController.initControls(); }, 0);
        }
    },
    setupNewRecordInfo: function () {
        this.recordId = null;
        $("#nrImatriculare").val("");
        $("#clientName").val("");
        $("#email").val("");
        $("#phoneNumber").val("");
        $("#vehicleType").val("");
        $("#recordType").val("");
        $("#expirationDate").val("");
        $("#additionalInfo").val("");
        $("#clientInformedStatus").val("");
    },
    initControls: function (record) {
        $("#expirationDate").datepicker({ dateFormat: 'dd/mm/yy', changeMonth: true, changeYear: true });

        $('#saveRecord').on('submit', function (e) {
            e.preventDefault();
        });
    },
    cancelEdit: function () {
        if (this.confirmCancel()) {
            window.location = "/Records/Index";
        }
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
    validateAndSaveRecord: function () {
        var recordInfo = {
            Id: this.recordId,
            CarRegistartionNumber: $("#nrImatriculare").val(),
            ClientId: editRecordController.clientId,
            ClientName: $("#clientName").val(),
            Email: $("#email").val(),
            PhoneNumber: $("#phoneNumber").val(),
            VehicleTypeId: $("#vehicleType").val(),
            RecordType: $("#recordType").val(),
            ExpirationDate: this.getCorrectDateFormat($("#expirationDate").val()),
            AdditionalInfo: $("#additionalInfo").val(),
            ClientInformedStatusId: $("#clientInformedStatus").val()
        };
        this.saveRecord(recordInfo);
    },
    setTruckForWorker: function () {
        var truckId = $("#" + $("#driver").val()).attr("truck-id");
        $("#truck").val(truckId);
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
    setRecordReuse: function (value) {
        this.reuseDrive = value;
    },
    sendSMSMessageToCustomer: function () {
        var recordInfo = {
            Id: this.recordId,
            CarRegistartionNumber: $("#nrImatriculare").val(),
            ClientId: editRecordController.clientId,
            ClientName: $("#clientName").val(),
            Email: $("#email").val(),
            PhoneNumber: $("#phoneNumber").val(),
            VehicleTypeId: $("#vehicleType").val(),
            RecordType: $("#recordType").val(),
            ExpirationDate: this.getCorrectDateFormat($("#expirationDate").val()),
            AdditionalInfo: $("#additionalInfo").val(),
            ClientInformedStatusId: $("#clientInformedStatus").val()
        };
        editRecordController.sendSMSToCustomer(recordInfo);
    },
    sendSMSToCustomer: function (recordInfo) {
        $.ajax({
            type: 'post',
            dataType: 'json',
            url: "/Records/SendMessageToClient",
            data: {
                record: recordInfo
            },
            success: function (res) {
                //sms send message
            },
            error: function (jqXHR, textStatus, exception, errorThrown) {
                $("#errorDialog").html(JSON.parse(jqXHR.responseText).error);
                $("#errorDialog").dialog("open");
            }
        });
    }
};
editRecordController.initRecord();