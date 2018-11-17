var editRecordController = {
    recordId: null,
    reuseRecord: false,
    saveRecord: function (record) {
        $.ajax({
            type: 'post',
            dataType: 'json',
            url: "/Records/SaveRecord",
            data: record,
            success: function (res) {
                debugger;
            },
            error: function (jqXHR, textStatus, exception, errorThrown) {
                $("#errorDialog").html(JSON.parse(jqXHR.responseText).error);
                setTimeout(function () { $("#errorDialog").dialog("open"); }, 1000);
            }
        });
    },
    getDrive: function () {
        $.ajax({
            type: 'post',
            dataType: 'json',
            url: "/Drive/GetDrive",
            data: {
                idDrive: this.getParameterByName("driveId", new URL(window.location.href))
            },
            success: function (res) {
                editDriveController.driveId = res.drive.Id;
                var drive = res.drive;
                editDriveController.setDriveDetails(drive);
                editDriveController.initControls(drive);
            },
            error: function (jqXHR, textStatus, exception, errorThrown) {
                $("#errorDialog").html(JSON.parse(jqXHR.responseText).error);
                $("#errorDialog").dialog("open");
            }
        });
    },
    setDriveDetails: function (drive) {

        $("#estimatedConsumption").val(drive.EstimatedConsumption);
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

        $("#estimatedConsumption").val("");
    },
    initDrive: function () {
        var url = new URL(window.location.href);
        var driveId = editDriveController.getParameterByName("driveId", new URL(window.location.href));
        if (driveId) {
            editDriveController.getDrive();
        } else {
            editDriveController.setupNewDriveInfo();
            setTimeout(function () { editDriveController.initControls(); }, 0);
        }
    },
    initControls: function (record) {
        //$("#expirationData").datepicker({ dateFormat: 'dd/mm/yy', changeMonth: true, changeYear: true });

        //$('#saveRecord').on('submit', function (e) {
        //    e.preventDefault();
        //});
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
    validateAndSaveDrive: function () {
        var recordInfo = {
            Id: this.recordId,
            CarRegistartionNumber: $("#nrImatriculare").val(),
            ClientName: $("#clientName").val(),
            Email: $("#email").val(),
            PhoneNumber: $("#phoneNumber").val(),
            VehicleTypeId: $("#vehicleType").val(),
            RecordType: $("#registrationType").val(),
            ExpirationDate: $("#expirationDate").val()
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
    }
};
editRecordController.initControls();