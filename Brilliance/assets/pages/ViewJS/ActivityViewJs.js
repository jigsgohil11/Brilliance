
$(document).ready(function () {
    $('#activity_Detail').summernote({
        height: 200,                 // set editor height

        minHeight: null,             // set minimum height of editor
        maxHeight: null,             // set maximum height of editor

        focus: true                 // set focus to editable area after initializing summernote
    });
    //Set Complete checkbox checked when Activity Assign to login user
    if ($("#activity_Assignee").val() != null) {

        //if ($("#activity_Assignee").val()[0] == (window.UserID).toUpperCase()) {
        //    $("#activity_IsComplete").prop("checked", true);
        //}
        if (window.IsEdit == "False") {

            for (key in $("#activity_Assignee").val()) {
                if ($("#activity_Assignee").val()[key] == (window.UserID).toUpperCase()) {
                    $("#activity_IsComplete").prop("checked", true);
                }
            }
        }

    }
});

$('.timepicker-default').timepicker({
    timeFormat: 'HH:mm:ss',
    autoclose: true,
    showSeconds: true,
    showHour: false,
    minuteStep: 1
});

function fngetDocuments(obj) {
    var StageID = $(obj).find("option:selected").val().toLowerCase();
    var LeadID = $("#activity_LeadID").val()
    var postData = {
        varStageID: StageID,
        varLeadID: LeadID,
    };
    ShowLoader();
    AjaxCall("/Activity/GetDocumentList", JSON.stringify(postData), "post", "json", "application/json", false).done(function (data) {
        HideLoader();
        if (data == "_Logon_") {
            window.location.href = "/Login/Login";
        }
        else {
            if (data == 'error') {
            }
            else {
                if (data.length != 0) {
                    $("#LeadStagesTbl_Body").html(data);
                }
            }
        }
    });
}

function fngetDocumentsList(obj) {
    var StageID = $("#activity_ActivityStage").val();
    var LeadID = $("#activity_LeadID").val()
    var docoptionName = $(obj).closest('tr').find('td:eq(0)').find("select").val();
    var postData = {
        varStageID: StageID,
        varLeadID: LeadID,
        varDocOption: docoptionName
    };
    ShowLoader();
    AjaxCall("/Activity/GetDocumentListCount", JSON.stringify(postData), "post", "json", "application/json", false).done(function (data) {
        HideLoader();
        if (data == "_Logon_") {
            window.location.href = "/Login/Login";
        }
        else {
            if (data == 'error') {
            }
            else {
                if (data.length != 0) {
                    $("#model_form_Activity").modal('hide');
                    $("#DocListData").html(data);
                    $("#NewDocForm").modal('show');
                }
            }
        }
    });
}

function SetMaxVersionToNewActivity(DocTypeName, DoDecrement) {
    var firmversion = "";
    var proposalversion = "";

    if (DocTypeName == "Firm profile") {
        firmversion = $("#FirmProfile").val();
        firmversion = parseInt(firmversion) - 1;
        $("#FirmProfile").val(firmversion);
    }
    else {
        proposalversion = $("#Proposal").val();
        proposalversion = parseInt(proposalversion) - 1;
        $("#Proposal").val(proposalversion);
    }

}

function fngetValue(obj, IsActive) {

    var firmversion = "";
    var proposalversion = "";
    var IsActivityInEditMode = $("#IsActivityInEditMode").val();

    if (IsActivityInEditMode.toUpperCase() == "TRUE") {

        $(obj).closest('tr').find('td:eq(3)').find('input[versionno]').val('');

        var FirmProfileMaxVersionNo = 0;
        var ProposalMaxVersionNo = 0;

        $('#tblEmpDocument').find('tbody').find('select option[value="Firm profile"]:selected').each(function () {
            var CurrentVersionnNo = parseInt($(this).closest('tr').find('td:eq(3)').find('input[versionno]').val());
            if (CurrentVersionnNo >= parseInt(FirmProfileMaxVersionNo)) {
                FirmProfileMaxVersionNo = CurrentVersionnNo;
            }
        });
        $("#FirmProfile").val(FirmProfileMaxVersionNo);

        $('#tblEmpDocument').find('tbody').find('select option[value="Proposal"]:selected').each(function () {
            var CurrentVersionnNo = parseInt($(this).closest('tr').find('td:eq(3)').find('input[versionno]').val());
            if (CurrentVersionnNo >= parseInt(ProposalMaxVersionNo)) {
                ProposalMaxVersionNo = CurrentVersionnNo;
            }
        });
        $("#Proposal").val(ProposalMaxVersionNo);

        var DocType = $(obj).find("option:selected").val();
        if (DocType) {
            if (DocType == "Firm profile") {
                firmversion = $("#FirmProfile").val();
                firmversion = parseInt(firmversion) + 1;
                $("#FirmProfile").val(firmversion);
            }
            else {
                proposalversion = $("#Proposal").val();
                proposalversion = parseInt(proposalversion) + 1;
                $("#Proposal").val(proposalversion);
            }
        }
        if (DocType == "Firm profile") {
            $(obj).closest('tr').find('.versionVal').val(firmversion);
        }
        else {
            $(obj).closest('tr').find('.versionVal').val(proposalversion);
        }

    } else {

        var DocType = $(obj).find("option:selected").val();
        if (DocType) {
            if (DocType == "Firm profile") {
                firmversion = $("#FirmProfile").val();
                firmversion = parseInt(firmversion) + 1;
                $("#FirmProfile").val(firmversion);
            }
            else {
                proposalversion = $("#Proposal").val();
                proposalversion = parseInt(proposalversion) + 1;
                $("#Proposal").val(proposalversion);
            }
        }
        if (DocType == "Firm profile") {
            $(obj).closest('tr').find('.versionVal').val(firmversion);
        }
        else {
            $(obj).closest('tr').find('.versionVal').val(proposalversion);
        }

        var i = 0;
        $('#tblEmpDocument').find('tbody').find('select option[value="Firm profile"]:selected').each(function () {
            $(this).closest('tr').find('td:eq(3)').find('input[versionno]').val(i + 1);
            i = i + 1;
        });
        $("#FirmProfile").val(i);

        var j = 0;
        $('#tblEmpDocument').find('tbody').find('select option[value="Proposal"]:selected').each(function () {
            $(this).closest('tr').find('td:eq(3)').find('input[versionno]').val(j + 1);
            j = j + 1;
        });
        $("#Proposal").val(j);
    }
}

function GenerateNewGUID() {
    function s4() {
        return Math.floor((1 + Math.random()) * 0x10000)
            .toString(16)
            .substring(1);
    }
    return s4() + s4() + '-' + s4() + '-' + s4() + '-' +
        s4() + '-' + s4() + s4() + s4();
}

function Upload(DocID) {
    if (DocID.files.length > 0) {
        if ($(DocID).attr("isnew") != "isnew") {
            var isnewclass = $(DocID).attr("class");
            $('#' + isnewclass).val("true");
        }
        $('#' + DocID.id).parent().next().next().find('#fileid').text('');
        var size = parseFloat(DocID.files[0].size / 1024).toFixed(2);
        if (size > 5120) {
            document.getElementById('VALmassage_' + $(DocID).attr("rpid")).innerHTML = 'File size exceeds 5 MB!';
        }
        else {
            document.getElementById('VALmassage_' + $(DocID).attr("rpid")).innerHTML = '';
        }
    }
    else {
        document.getElementById('VALmassage_' + $(DocID).attr("rpid")).innerHTML = '';
    }
}

function AddNewRow(obj, isEnabled) {
    var StageIndex = $("#StageIndex").val();
    StageIndex = parseInt(StageIndex);
    var FirmProfile = $("#FirmProfile").val();
    FirmProfile = parseInt(FirmProfile) + 1;
    var Proposal = $("#Proposal").val();
    Proposal = parseInt(Proposal) + 1;
    var html = $("#mycustomtr tr").html();
    var docoption = $(obj).closest('tr').find('td:eq(0)').find("select").val();
    //var version = $(obj).closest('tr').find('td:eq(3)').find('input[versionno]').val();
    //version = parseInt(version) + 1;
    var docoptionID = docoption;
    if (isEnabled) {

        var ID = GenerateNewGUID();
        var trhtml = $("<tr id=" + ID + ">" + html + "</tr>");
        $("#LeadStagesTbl_Body").append(trhtml);

        if (docoptionID != "" && docoptionID != undefined) {
            if (docoptionID == "Firm profile") {
                $("#FirmProfile").val(FirmProfile);
            }
            else {
                $("#Proposal").val(Proposal);
            }
        }


        if (isEnabled) {
            $("#" + ID).find('td:eq(0)').find('select').removeAttr('Disabled');
            //$("#" + ID).find('td:eq(3)').find('input[versionno]').removeAttr('Disabled');
        }

        $("#StageIndex").val(StageIndex);
        $(".bs-select").selectpicker('refresh');
        $("#" + ID + " td:eq(0) > div:first").removeAttr("class");
        $("#" + ID + " td:eq(0) > div:first .btn.dropdown-toggle.bs-placeholder.btn-default:first").remove();
        $("#" + ID + " td:eq(0) > div:first .dropdown-menu.open:first").remove();
        $("#" + ID + " td:eq(1) > div:first").removeAttr("class");
        $("#" + ID + " td:eq(1) > div:first .btn.dropdown-toggle.bs-placeholder.btn-default:first").remove();
        $("#" + ID + " td:eq(1) > div:first .dropdown-menu.open:first").remove();
        var DocTypeID = "activitydocumentlist_" + StageIndex + "__DocType_Term";
        var DocTypeName = "activitydocumentlist[" + StageIndex + "].DocType_Term";
        var StatusID = "activitydocumentlist_" + StageIndex + "__Status_Term";
        var StatusName = "activitydocumentlist[" + StageIndex + "].Status_Term";
        var VersionNoID = "activitydocumentlist_" + StageIndex + "__VersionNo";
        var VersionNoName = "activitydocumentlist[" + StageIndex + "].VersionNo";
        var attachid = "activitydocumentlist_" + StageIndex + "__AttachementID";
        var attachmentname = "activitydocumentlist[" + StageIndex + "].AttachementID";
        var DocumentID = "activitydocumentlist_" + StageIndex + "__DocumentID";
        var Docname = "activitydocumentlist[" + StageIndex + "].DocumentID";
        var FileID = "activitydocumentlist_" + StageIndex + "__Term";
        var FileName = "activitydocumentlist[" + StageIndex + "].Term";
        var ProjectTermID = "activitydocumentlist_" + StageIndex + "__ProjectTermID";
        var ProjectTermName = "activitydocumentlist[" + StageIndex + "].ProjectTermID";
        var filepath = "activitydocumentlist_" + StageIndex + "__FilePath";
        var filepathname = "activitydocumentlist[" + StageIndex + "].FilePath";
        var attachfile = "activitydocumentlist_" + StageIndex + "__FileName";
        var attachfilename = "activitydocumentlist[" + StageIndex + "].FileName";
        var FileSize = "activitydocumentlist_" + StageIndex + "__FileSize";
        var FileSizeName = "activitydocumentlist[" + StageIndex + "].FileSize";
        var ExtentionTypeTerm = "activitydocumentlist_" + StageIndex + "__ExtensionType_Term";
        var ExtentionTypeTermName = "activitydocumentlist[" + StageIndex + "].ExtensionType_Term";
        var DisplayNameId = "activitydocumentlist_" + StageIndex + "__DisplayName";
        var DisplayName = "activitydocumentlist[" + StageIndex + "].DisplayName";
        var IsNewId = "activitydocumentlist_" + StageIndex + "__IsNew";
        var IsNewName = "activitydocumentlist[" + StageIndex + "].IsNew";
        $("#" + ID + " .AttachementID").attr("id", attachid);
        $("#" + ID + " .AttachementID").attr("name", attachmentname);
        $("#" + ID + " .DocumentID").attr("id", DocumentID);
        $("#" + ID + " .DocumentID").attr("name", Docname);
        $("#" + ID + " .Term").attr("id", FileID);
        $("#" + ID + " .Term").attr("name", FileName);
        $("#" + ID + " .ProjectTermID").attr("id", ProjectTermID);
        $("#" + ID + " .ProjectTermID").attr("name", ProjectTermName);
        $("#" + ID + " .FilePath").attr("id", filepath);
        $("#" + ID + " .FilePath").attr("name", filepathname);
        $("#" + ID + " .FileName").attr("id", attachfile);
        $("#" + ID + " .FileName").attr("name", attachfilename);
        $("#" + ID + " .FileSize").attr("id", FileSize);
        $("#" + ID + " .FileSize").attr("name", FileSizeName);
        $("#" + ID + " .ExtensionType_Term").attr("id", ExtentionTypeTerm);
        $("#" + ID + " .ExtensionType_Term").attr("name", ExtentionTypeTermName);
        $("#" + ID + " .DisplayName").attr("id", DisplayNameId);
        $("#" + ID + " .DisplayName").attr("name", DisplayName);
        //$("#" + ID + " .VersionNo").attr("id", VersionNoID);
        //$("#" + ID + " .VersionNo").attr("name", VersionNoName);
        $("#" + ID + " .isnew").attr("id", IsNewId);
        $("#" + ID + " .isnew").attr("name", IsNewName);

        var CustomID = "activitydocumentlist_" + StageIndex + "__CustomTerm";
        var CustommName = "activitydocumentlist[" + StageIndex + "].CustomTerm";

        $("#" + ID + " .CustomField").attr("id", CustomID);
        $("#" + ID + " .CustomField").attr("name", CustommName);
        $("#" + ID + " .CustomField").val("Term_" + StageIndex + "");

        $("#" + ID + " td:eq(0) div.btn-group.bootstrap-select.form-control.bs-select .btn.dropdown-toggle.bs-placeholder.btn-default").attr("data-id", DocTypeID);
        $("#" + ID + " td:eq(0) div.btn-group.bootstrap-select.form-control.bs-select select.form-control.bs-select").attr("id", DocTypeID);
        $("#" + ID + " td:eq(0) div.btn-group.bootstrap-select.form-control.bs-select select.form-control.bs-select").attr("name", DocTypeName);
        $("#" + ID + " td:eq(1) div.btn-group.bootstrap-select.form-control.bs-select .btn.dropdown-toggle.bs-placeholder.btn-default").attr("data-id", StatusID);
        $("#" + ID + " td:eq(1) div.btn-group.bootstrap-select.form-control.bs-select select.form-control.bs-select").attr("id", StatusID);
        $("#" + ID + " td:eq(1) div.btn-group.bootstrap-select.form-control.bs-select select.form-control.bs-select").attr("name", StatusName);
        $("#" + ID + " td:eq(2) input[type='file']").attr("id", "Term_" + StageIndex + "");
        $("#" + ID + " td:eq(2) input[type='file']").attr("name", "Term_" + StageIndex + "");
        $("#" + ID + " td:eq(3) .form-control").attr("id", VersionNoID);
        $("#" + ID + " td:eq(3) .form-control").attr("name", VersionNoName);
        if ($(obj).attr("addAttr")) {
            $('#' + DocTypeID + ' option[value="' + docoption + '"]').prop("selected", true);
            $('#' + DocTypeID).selectpicker('refresh');
            //$('#' + DocTypeID).val(docoption).trigger('change');
        }
        if (!isEnabled) {
            if (docoptionID == "Firm profile") {
                $('#' + VersionNoID).val(FirmProfile);
            }
            else {
                $('#' + VersionNoID).val(Proposal);
            }

        }

        StageIndex = parseInt(StageIndex) + 1;
        $("#StageIndex").val(StageIndex);
    }
    else {
        if (docoptionID.toString().trim() != undefined && docoptionID.toString().trim() != null && docoptionID.toString().trim() != '') {
            var ID = GenerateNewGUID();
            var trhtml = $("<tr id=" + ID + ">" + html + "</tr>");
            $("#LeadStagesTbl_Body").append(trhtml);

            if (docoptionID != "" && docoptionID != undefined) {
                if (docoptionID == "Firm profile") {
                    $("#FirmProfile").val(FirmProfile);
                }
                else {
                    $("#Proposal").val(Proposal);
                }
            }


            if (isEnabled) {
                $("#" + ID).find('td:eq(0)').find('select').removeAttr('Disabled');
                //$("#" + ID).find('td:eq(3)').find('input[versionno]').removeAttr('Disabled');
            }

            $("#StageIndex").val(StageIndex);
            $(".bs-select").selectpicker('refresh');
            $("#" + ID + " td:eq(0) > div:first").removeAttr("class");
            $("#" + ID + " td:eq(0) > div:first .btn.dropdown-toggle.bs-placeholder.btn-default:first").remove();
            $("#" + ID + " td:eq(0) > div:first .dropdown-menu.open:first").remove();
            $("#" + ID + " td:eq(1) > div:first").removeAttr("class");
            $("#" + ID + " td:eq(1) > div:first .btn.dropdown-toggle.bs-placeholder.btn-default:first").remove();
            $("#" + ID + " td:eq(1) > div:first .dropdown-menu.open:first").remove();
            var DocTypeID = "activitydocumentlist_" + StageIndex + "__DocType_Term";
            var DocTypeName = "activitydocumentlist[" + StageIndex + "].DocType_Term";
            var StatusID = "activitydocumentlist_" + StageIndex + "__Status_Term";
            var StatusName = "activitydocumentlist[" + StageIndex + "].Status_Term";
            var VersionNoID = "activitydocumentlist_" + StageIndex + "__VersionNo";
            var VersionNoName = "activitydocumentlist[" + StageIndex + "].VersionNo";
            var attachid = "activitydocumentlist_" + StageIndex + "__AttachementID";
            var attachmentname = "activitydocumentlist[" + StageIndex + "].AttachementID";
            var DocumentID = "activitydocumentlist_" + StageIndex + "__DocumentID";
            var Docname = "activitydocumentlist[" + StageIndex + "].DocumentID";
            var FileID = "activitydocumentlist_" + StageIndex + "__Term";
            var FileName = "activitydocumentlist[" + StageIndex + "].Term";
            var ProjectTermID = "activitydocumentlist_" + StageIndex + "__ProjectTermID";
            var ProjectTermName = "activitydocumentlist[" + StageIndex + "].ProjectTermID";
            var filepath = "activitydocumentlist_" + StageIndex + "__FilePath";
            var filepathname = "activitydocumentlist[" + StageIndex + "].FilePath";
            var attachfile = "activitydocumentlist_" + StageIndex + "__FileName";
            var attachfilename = "activitydocumentlist[" + StageIndex + "].FileName";
            var FileSize = "activitydocumentlist_" + StageIndex + "__FileSize";
            var FileSizeName = "activitydocumentlist[" + StageIndex + "].FileSize";
            var ExtentionTypeTerm = "activitydocumentlist_" + StageIndex + "__ExtensionType_Term";
            var ExtentionTypeTermName = "activitydocumentlist[" + StageIndex + "].ExtensionType_Term";
            var DisplayNameId = "activitydocumentlist_" + StageIndex + "__DisplayName";
            var DisplayName = "activitydocumentlist[" + StageIndex + "].DisplayName";
            var IsNewId = "activitydocumentlist_" + StageIndex + "__IsNew";
            var IsNewName = "activitydocumentlist[" + StageIndex + "].IsNew";
            $("#" + ID + " .AttachementID").attr("id", attachid);
            $("#" + ID + " .AttachementID").attr("name", attachmentname);
            $("#" + ID + " .DocumentID").attr("id", DocumentID);
            $("#" + ID + " .DocumentID").attr("name", Docname);
            $("#" + ID + " .Term").attr("id", FileID);
            $("#" + ID + " .Term").attr("name", FileName);
            $("#" + ID + " .ProjectTermID").attr("id", ProjectTermID);
            $("#" + ID + " .ProjectTermID").attr("name", ProjectTermName);
            $("#" + ID + " .FilePath").attr("id", filepath);
            $("#" + ID + " .FilePath").attr("name", filepathname);
            $("#" + ID + " .FileName").attr("id", attachfile);
            $("#" + ID + " .FileName").attr("name", attachfilename);
            $("#" + ID + " .FileSize").attr("id", FileSize);
            $("#" + ID + " .FileSize").attr("name", FileSizeName);
            $("#" + ID + " .ExtensionType_Term").attr("id", ExtentionTypeTerm);
            $("#" + ID + " .ExtensionType_Term").attr("name", ExtentionTypeTermName);
            $("#" + ID + " .DisplayName").attr("id", DisplayNameId);
            $("#" + ID + " .DisplayName").attr("name", DisplayName);
            //$("#" + ID + " .VersionNo").attr("id", VersionNoID);
            //$("#" + ID + " .VersionNo").attr("name", VersionNoName);
            $("#" + ID + " .isnew").attr("id", IsNewId);
            $("#" + ID + " .isnew").attr("name", IsNewName);

            var CustomID = "activitydocumentlist_" + StageIndex + "__CustomTerm";
            var CustommName = "activitydocumentlist[" + StageIndex + "].CustomTerm";

            $("#" + ID + " .CustomField").attr("id", CustomID);
            $("#" + ID + " .CustomField").attr("name", CustommName);
            $("#" + ID + " .CustomField").val("Term_" + StageIndex + "");

            $("#" + ID + " td:eq(0) div.btn-group.bootstrap-select.form-control.bs-select .btn.dropdown-toggle.bs-placeholder.btn-default").attr("data-id", DocTypeID);
            $("#" + ID + " td:eq(0) div.btn-group.bootstrap-select.form-control.bs-select select.form-control.bs-select").attr("id", DocTypeID);
            $("#" + ID + " td:eq(0) div.btn-group.bootstrap-select.form-control.bs-select select.form-control.bs-select").attr("name", DocTypeName);
            $("#" + ID + " td:eq(1) div.btn-group.bootstrap-select.form-control.bs-select .btn.dropdown-toggle.bs-placeholder.btn-default").attr("data-id", StatusID);
            $("#" + ID + " td:eq(1) div.btn-group.bootstrap-select.form-control.bs-select select.form-control.bs-select").attr("id", StatusID);
            $("#" + ID + " td:eq(1) div.btn-group.bootstrap-select.form-control.bs-select select.form-control.bs-select").attr("name", StatusName);
            $("#" + ID + " td:eq(2) input[type='file']").attr("id", "Term_" + StageIndex + "");
            $("#" + ID + " td:eq(2) input[type='file']").attr("name", "Term_" + StageIndex + "");
            $("#" + ID + " td:eq(3) .form-control").attr("id", VersionNoID);
            $("#" + ID + " td:eq(3) .form-control").attr("name", VersionNoName);
            if ($(obj).attr("addAttr")) {
                $('#' + DocTypeID + ' option[value="' + docoption + '"]').prop("selected", true);
                $('#' + DocTypeID).selectpicker('refresh');
                //$('#' + DocTypeID).val(docoption).trigger('change');
            }
            if (!isEnabled) {
                if (docoptionID == "Firm profile") {
                    $('#' + VersionNoID).val(FirmProfile);
                }
                else {
                    $('#' + VersionNoID).val(Proposal);
                }

            }

            StageIndex = parseInt(StageIndex) + 1;
            $("#StageIndex").val(StageIndex);
        } else {
            toastr["error"]("Please Select Document Type", "Activity", { "positionClass": "toast-bottom-right", "progressBar": true });
        }
    }
}

function ActivityTableIndexOrdering() {
    var table = $("#tblEmpDocument tbody");
    table.find('tr').each(function (i) {

        $(this).find("input[type='hidden']:eq(0)").attr("id", "activitydocumentlist_" + i + "__AttachementID");
        $(this).find("input[type='hidden']:eq(0)").attr("name", "activitydocumentlist[" + i + "].AttachementID");

        $(this).find("input[type='hidden']:eq(1)").attr("id", "activitydocumentlist_" + i + "__DocumentID");
        $(this).find("input[type='hidden']:eq(1)").attr("name", "activitydocumentlist[" + i + "].DocumentID");

        $(this).find("input[type='hidden']:eq(2)").attr("id", "activitydocumentlist_" + i + "__Term");
        $(this).find("input[type='hidden']:eq(2)").attr("name", "activitydocumentlist[" + i + "].Term");

        $(this).find("input[type='hidden']:eq(3)").attr("id", "activitydocumentlist_" + i + "__ProjectTermID");
        $(this).find("input[type='hidden']:eq(3)").attr("name", "activitydocumentlist[" + i + "].ProjectTermID");

        $(this).find("input[type='hidden']:eq(4)").attr("id", "activitydocumentlist_" + i + "__FilePath");
        $(this).find("input[type='hidden']:eq(4)").attr("name", "activitydocumentlist[" + i + "].FilePath");

        $(this).find("input[type='hidden']:eq(5)").attr("id", "activitydocumentlist_" + i + "__FileName");
        $(this).find("input[type='hidden']:eq(5)").attr("name", "activitydocumentlist[" + i + "].FileName");

        $(this).find("input[type='hidden']:eq(6)").attr("id", "activitydocumentlist_" + i + "__FileSize");
        $(this).find("input[type='hidden']:eq(6)").attr("name", "activitydocumentlist[" + i + "].FileSize");

        $(this).find("input[type='hidden']:eq(7)").attr("id", "activitydocumentlist_" + i + "__ExtensionType_Term");
        $(this).find("input[type='hidden']:eq(7)").attr("name", "activitydocumentlist[" + i + "].ExtensionType_Term");

        $(this).find("input[type='hidden']:eq(8)").attr("id", "activitydocumentlist_" + i + "__DisplayName");
        $(this).find("input[type='hidden']:eq(8)").attr("name", "activitydocumentlist[" + i + "].DisplayName");

        $(this).find("input[type='hidden']:eq(9)").attr("id", "activitydocumentlist_" + i + "__CustomTerm");
        $(this).find("input[type='hidden']:eq(9)").attr("name", "activitydocumentlist[" + i + "].CustomTerm");

        $(this).find('td:eq(0)').find('select').attr("id", "activitydocumentlist_" + i + "__DocType_Term");
        $(this).find('td:eq(0)').find('select').attr("name", "activitydocumentlist[" + i + "].DocType_Term");

        $(this).find('td:eq(1)').find('select').attr("id", "activitydocumentlist_" + i + "__Status_Term");
        $(this).find('td:eq(1)').find('select').attr("name", "activitydocumentlist[" + i + "].Status_Term");

        $(this).find('td:eq(2)').find('input[class="isnew"]').attr("id", "activitydocumentlist_" + i + "__IsNew");
        $(this).find('td:eq(2)').find('input[class="isnew"]').attr("name", "activitydocumentlist[" + i + "].IsNew");

        $(this).find('td:eq(3)').find('input[versionno]').attr("id", "activitydocumentlist_" + i + "__VersionNo");
        $(this).find('td:eq(3)').find('input[versionno]').attr("name", "activitydocumentlist[" + i + "].VersionNo");


        $(this).find('td:eq(0)').find('select').attr("id", "activitydocumentlist_" + i + "__DocType_Term").removeAttr("disabled");

        $(this).find('td:eq(3)').find('input[versionno]').attr("id", "activitydocumentlist_" + i + "__VersionNo").removeAttr("disabled");

    });
}

function DeleteActivityRowData(obj) {
    var version = $(obj).closest('tr').find('td:eq(3)').find('input[versionno]').val();
    var docTypeoption = $(obj).closest('tr').find('td:eq(0)').find("select").val();
    var table = $("#tblEmpDocument tbody");
    var regex = /Hot/;
    $('#tblEmpDocument').find('tbody').find('select option[value="' + docTypeoption + '"]:selected').each(function () {
        var CurrentTdValue = parseInt($(this).closest('tr').find('td:eq(3)').find('input[versionno]').val());
        if (CurrentTdValue.toString().trim() != undefined && CurrentTdValue.toString().trim() != null && CurrentTdValue.toString().trim() != '' && version.toString().trim() != undefined && version.toString().trim() != null && version.toString().trim() != '') {
            if (CurrentTdValue > version) {
                $(this).closest('tr').find('td:eq(3)').find('input[versionno]').val(parseInt(CurrentTdValue) - 1);
            }
        }
    });
    $(obj).closest('tr').remove();
    //&& version.toString().trim() != undefined && version.toString().trim() != ''
    if (docTypeoption.toString().trim() != undefined && docTypeoption.toString().trim() != '') {
        SetMaxVersionToNewActivity(docTypeoption, true);
    }
}

function ActivityFormSubmit() {
    ActivityTableIndexOrdering();
    var filenameblank = $("#filenameBlank").val();
    if (!($("#VALmassage_0").text() == "File size exceeds 5 MB!" || $("#VALmassage_1").text() == "File size exceeds 5 MB!" || $("#VALmassage_2").text() == "File size exceeds 5 MB!" || $("#VALmassage_3").text() == "File size exceeds 5 MB!" || $("#VALmassage_4").text() == "File size exceeds 5 MB!")) {
        if ($("#frmActivity").valid()) {

            var table = $("#tblEmpDocument tbody");

            table.find('tr').each(function (i) {
                $(this).find('td:eq(0)').find('input[otherdoctype]').attr("id", "activitydocumentlist_" + i + "__OtherDocType").removeAttr("disabled");
                var rowCount = 1;
                rowCount = rowCount + i;
                $("#hiddenAcitivityDocRowCount").val(rowCount);
            })

            var form = $('#frmActivity')[0];
            var data = new FormData(form);

            ShowLoader();

            $.ajax({
                type: "POST",
                enctype: 'multipart/form-data',
                url: "/Crm/Activity/SaveActivity",
                data: data,
                processData: false,
                contentType: false,
                cache: false,
                timeout: 600000,
                success: function (response) {
                    HideLoader();
                    if (response == "_Logon_") {
                        window.location.href = "/Login/Login";
                    }
                    else {
                        if (response.IsSuccess) {
                            if (response.Data == "DASHBOARD") {
                                $("#model_form_Activity").modal('hide');
                                $(".main-dasboard-activity-tab-ul").find("li.active").find("a").click();
                                App.blockUI({
                                    target: '#AllActivity',
                                    boxed: true,
                                    message: 'Processing...'
                                });
                            } else {
                                SetMessageForPageLoad(response.Message, "Activitysaved");
                                window.location.href = "/Crm/LeadInfo/LeadInfo/" + response.Data1 + "";
                            }
                        }
                        else {
                            ShowMessage(response.Message, "error");
                        }
                    }
                }
            });
        }
    }
}

function GetContactList(obj) {
    var LeadID = $(obj).find("option:selected").val().toLowerCase();
    var postData = {
        varLeadID: LeadID
    };
    ShowLoader();
    AjaxCall("/Crm/Activity/GetContactList", JSON.stringify(postData), "post", "json", "application/json", false).done(function (data) {
        HideLoader();
        if (data == "_Logon_") {
            window.location.href = "/Login/Login";
        }
        else {
            if (data == 'error') {
                toastr["error"]("Oops Something Wentwrong !!!", "ClientCountry", { "positionClass": "toast-bottom-right", "progressBar": true });
            }
            else {
                if (data.length != 0) {
                    $("#activity_Contacts").html('');
                    $("#activity_Contacts").append('<option value="">--Select Contact--</option>');
                    for (key in data) {
                        $("#activity_Contacts").append('<option value="' + data[key].Value + '">' + data[key].Text + '</option>');
                    }
                    $('#activity_Contacts').selectpicker('refresh');
                }
                else {
                    $("#activity_Contacts").html('');
                    $("#activity_Contacts").append('<option value="">--Select Contact--</option>');
                    $('#activity_Contacts').selectpicker('refresh');
                }
            }
        }
    });
}

function getSelectedOptions(sel, CurrentUserID) {
    var opts = [],
        opt;
    var len = sel.options.length;
    for (var i = 0; i < len; i++) {
        opt = sel.options[i];

        if (opt.selected) {
            opts.push(opt.value);

        }
    }
    var indxtocheck = opts.indexOf(CurrentUserID.toUpperCase());
    if (indxtocheck >= 0) {
        $("#activity_IsComplete").prop("checked", true);
    }
    else {
        $("#activity_IsComplete").prop("checked", false);
    }
    return opts;
}

function FormInitialization() {
    if ($(".date-picker").length > 0) {
        $('.date-picker').datepicker({
            format: 'dd/mm/yyyy',
            autoclose: true,
            orientation: "bottom",
            todayHighlight: true
        })

        $(".date-picker").on("change", function (evt, params) {
            $(evt.target).valid();
        });

        $('#activity_Detail').summernote({
            height: 200,                 // set editor height

            minHeight: null,             // set minimum height of editor
            maxHeight: null,             // set maximum height of editor

            focus: true                 // set focus to editable area after initializing summernote
        });

        $(function () {
            $.validator.methods.date = function (value, element) {
                return this.optional(element) || moment(value, "DD/MM/YYYY", true).isValid();
            }
        });
    }

    $('.timepicker-no-seconds').timepicker({
        autoclose: true,
        minuteStep: 5
    });

    $('.timepicker-24').timepicker({
        autoclose: true,
        minuteStep: 5,
        showSeconds: false,
        showMeridian: false,
        defaultTime: '00:15'
    });
}

function CloseDocumentPopUp() {
    var ActivityID = $("#activity_EncryptedActivityID").val();
    var LeadID = $("#activity_EncryptedLeadID").val()
    var ComingFrom = "Dashboard"
    $("#NewDocForm").modal('hide');
    fnGetActivityByID(ActivityID, LeadID, ComingFrom);
}

function fnGetActivityByID(EncryptedActivityID, EncryptedLeadID, ComingFrom) {
    var postData = {
        varActivityID: EncryptedActivityID,
        varLeadID: EncryptedLeadID,
        varComingFrom: ComingFrom,
    };
    ShowLoader();
    AjaxCall("/Crm/Activity/GetActivityById", JSON.stringify(postData), "post", "json", "application/json", true).done(function (data) {
        HideLoader();
        if (data == "_Logon_") {
            window.location.href = "/Login/Login";
        }
        else {
            if (data == 'error') {
                //toastr["error"]("Oops Something Wentwrong !!!", "Branch", { "positionClass": "toast-bottom-right", "progressBar": true });
            }
            else {
                if (data.length != 0) {
                    $("#ActivityDataSection").html(data);
                    $("#model_form_Activity").modal('show');
                    $("#frmActivity").removeData('validator');
                    $("#frmActivity").removeData('unobtrusiveValidation');
                    $.validator.unobtrusive.parse("#frmActivity");
                    $(".bs-select").selectpicker('refresh');
                    $("#model_form_Activity").scroll(function () {
                        $('.date-picker').datepicker('place');
                        $('.timepicker').timepicker('place');
                    });
                    FormInitialization();
                }
            }
        }
    });

}
