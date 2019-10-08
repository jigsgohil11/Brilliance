//$(document).on({
//    "contextmenu": function (e) {
//        e.preventDefault();
//    }
//});

//$(document).keydown(function (event) {
//    if (event.keyCode == 123) { // Prevent F12
//        return false;
//    } else if (event.ctrlKey && event.shiftKey && event.keyCode == 73) { // Prevent Ctrl+Shift+I        
//        return false;
//    } else if (event.ctrlKey && event.shiftKey && event.keyCode == 67) { // Prevent Ctrl+Shift+C
//        return false;
//    } else if (event.ctrlKey && event.shiftKey && event.keyCode == 75) { // Prevent Ctrl+Shift+K
//        return false;
//    } else if (event.ctrlKey && event.shiftKey && event.keyCode == 83) { // Prevent Ctrl+Shift+K
//        return false;
//    } else if (event.ctrlKey && event.shiftKey && event.keyCode == 69) { // Prevent Ctrl+Shift+E
//        return false;
//    } else if (event.ctrlKey && event.shiftKey && event.keyCode == 74) { // Prevent Ctrl+Shift+J
//        return false;
//    } else if (event.ctrlKey && event.shiftKey && event.keyCode == 77) { // Prevent Ctrl+Shift+M
//        return false;
//    } else if (event.shiftKey && event.keyCode == 118) { // Prevent Shift+F7
//        return false;
//    } else if (event.shiftKey && event.keyCode == 116) { // Prevent Shift+F5
//        return false;
//    } else if (event.shiftKey && event.keyCode == 120) { // Prevent Shift+F9
//        return false;
//    } else if (event.shiftKey && event.keyCode == 119) { // Prevent Shift+F8
//        return false;
//    } else if (event.shiftKey && event.keyCode == 115) { // Prevent Shift+F4
//        return false;
//    } else if (event.ctrlKey && event.keyCode == 85) { // Prevent  Ctrl+U
//        return false;
//    }
//});

$(document).ready(function () {

    //if ($(".select2").length > 0) {
    //    $(".select2").select2();
    //}

    //$.sessionTimeout({
    //    title: 'Session Timeout Notification',
    //    message: 'Your session is about to expire.',
    //    redirUrl: '../../../Login/Logout',
    //    logoutUrl: '../../../Login/Logout',
    //    warnAfter: 1080000, //warn after 5 seconds
    //    redirAfter: 1200000, //redirect after 10 secons,
    //    ignoreUserActivity: false,
    //    countdownMessage: 'Redirecting in {timer} seconds.',
    //    countdownBar: true
    //});

    $('#clickmewow').click(function () {
        $('#radio1003').attr('checked', 'checked');
    });


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

        //$(".date-picker").inputmask("d/m/y", {
        //    "placeholder": "dd/mm/yyyy"
        //});

        $(function () {
            $.validator.methods.date = function (value, element) {

                return this.optional(element) || moment(value, "DD/MM/YYYY", true).isValid();
            }
        });
    }

    if ($(".form_datetime").length > 0) {

        $(".form_datetime").datetimepicker({
            autoclose: true,
            format: "dd/mm/yyyy hh:ii",
            fontAwesome: true,
            pickerPosition: "bottom-right"
        });

        $(".form_datetime").on("change", function (evt, params) {
            $(evt.target).valid();
        });


        $(function () {
            $.validator.methods.date = function (value, element) {
                if ($(element).hasClass('date-picker')) {
                    return this.optional(element) || moment(value, "DD/MM/YYYY", true).isValid();
                }
                else if ($(element).hasClass('form_datetime')) {
                    return this.optional(element) || moment(value, "DD/MM/YYYY HH:mm", true).isValid();
                }

            }
        });
    }


    if ($(".only-month-date-picker").length > 0) {
        $('.only-month-date-picker').datepicker({
            format: 'mm/yyyy',
            autoclose: true,
            orientation: "bottom"
        })
    }
    //GetNotifications();
    //window.setInterval(GetNotifications, 600000);

    $("input[data-val-required]").each(function myfunction() {
        if (!$(this).siblings().closest("label").hasClass("mt-checkbox")) {
            $(this).siblings().closest("label").addClass("required1");
        }
    });
});

$(function () {
    $.validator.methods.date = function (value, element) {
        return this.optional(element) || moment(value, "DD/MM/YYYY", true).isValid();
    }
});

var pleaseWaitDiv;
var hideLoading = true;
var IsManualAjaxCall = false;

function ShowLoader() {
    IsManualAjaxCall = true;
    App.blockUI({
        target: 'body',
        animate: true
    });

}

function HideLoader() {
    if (IsManualAjaxCall) {
        IsManualAjaxCall = false;
        App.unblockUI('body');
    }
}

function SetMessageForPageLoad(data, cookieName) {
    Cookies.set(cookieName, data);
}

function ShowPageLoadMessage(cookieName, page) {
    if (Cookies.get(cookieName) != null && Cookies.get(cookieName) != "null") {
        var response = Cookies.get(cookieName);
        toastr.success(response, page, { "positionClass": "toast-bottom-right", "progressBar": true });
        Cookies.set(cookieName, null);
    }
}

function ShowPageLoadErrorMessage(cookieName, page) {
    if (Cookies.get(cookieName) != null && Cookies.get(cookieName) != "null") {
        var response = Cookies.get(cookieName);
        toastr.error(response, page, { "positionClass": "toast-bottom-right", "progressBar": true });
        Cookies.set(cookieName, null);
    }
}

function ShowMessages(data) {

    if (data.IsSuccess != undefined && data.Message != undefined) {
        if (data.IsSuccess && (data.Message != undefined || data.Message != null || data.Message != '')) {
            ShowMessage(data.Message);
        } else {
            //ShowMessage('', 'error', data.Message);
            ShowAlertMessage(data.Message, 'error', Languages.errorMessage);
        }
    }
}

function ShowMessage(message, type) {

    HideLoader();
    toastr.options = {
        "positionClass": "toast-bottom-right",
        "progressBar": true
    }

    if (type != undefined && type == 'error')
        toastr.error(message);
    else if (type != undefined && type == 'warning')
        toastr.warning(message);
    else
        toastr.success(message);
}

function AjaxCall(url, postData, httpmethod, calldatatype, contentType, showLoading, hideLoadingParam, isAsync, isTraditional) {
    if (showLoading)
        IsManualAjaxCall = true;
    if (hideLoadingParam != undefined && !hideLoadingParam)
        hideLoading = hideLoadingParam;
    if (contentType == undefined)
        contentType = "application/x-www-form-urlencoded;charset=UTF-8";

    if (showLoading == undefined)
        showLoading = true;

    if (showLoading == false || showLoading.toString().toLowerCase() == "false")
        showLoading = false;
    else
        showLoading = true;

    if (isAsync == undefined)
        isAsync = true;

    if (isTraditional == undefined)
        isTraditional = true;
    return $.ajax({
        type: httpmethod,
        url: url,
        data: postData,
        global: showLoading,
        //dataType: calldatatype,
        contentType: contentType,
        traditional: isTraditional,
        async: isAsync,
        beforeSend: function () { if (showLoading) ShowLoader(); },
        error: function (err) {
            //console.log(err);
        }
    });
}

function DeleteAnyEntity(urlTopass, DeleteButtonID, DeleteModalID, EntityNameToDelete, tableidTopass, Redirectionurl) {
    var trId = $("#" + DeleteButtonID).attr("DelID");
    var postData = {
        DeleteID: $("#" + DeleteButtonID).attr("DelID")
    };
    ShowLoader();
    AjaxCall(urlTopass, JSON.stringify(postData), "post", "json", "application/json", true).done(function (data) {
        $("#" + DeleteModalID).modal('hide');
        HideLoader();
        if (data == "_Logon_") {
            window.location.href = "/Login/Login";
        }
        else {
            if (data == 'error') {
                toastr["error"]("Oops Something Wentwrong !!!", EntityNameToDelete, { "positionClass": "toast-bottom-right", "progressBar": true });
            }
            else {
                if (data.length != 0) {
                    SetMessageForPageLoad(data.Message, EntityNameToDelete);
                    //toastr["success"](EntityNameToDelete + " deleted successfully.", EntityNameToDelete, { "positionClass": "toast-bottom-right", "progressBar": true });
                    //var $datatable = $("#" + tableidTopass).DataTable();
                    //$datatable.row($("#" + trId)).remove().draw();
                    //$("#" + trId).remove();
                    window.location.href = Redirectionurl;
                }
            }
        }
    });

}

function fnOpenDeleteModel(DeleteButtonID, DeleteModalID, DeleteIDToset) {
    $("#" + DeleteButtonID).attr("DelID", DeleteIDToset);
    $("#" + DeleteModalID).modal('show');
}

function CanAccessEditOrDelete(canCreate, canUpdate, canDelete) {
    if (!CheckBoleanValueFromString(canCreate))
        $(".cannot_access_create").remove();
    if (!CheckBoleanValueFromString(canUpdate))
        $(".cannot_access_update").remove();
    if (!CheckBoleanValueFromString(canDelete))
        $(".cannot_access_remove").remove();

    if (!CheckBoleanValueFromString(canUpdate) && (!CheckBoleanValueFromString(canDelete)))
        $(".edit_delete_permission").hide();
    else
        $(".edit_delete_permission").show();

}

function CheckBoleanValueFromString(strvalue) {
    switch (strvalue.toLowerCase()) {
        case "true":
            return true;
        case "false":
            return false;
        default:
            throw new Error("Cannot convert string to boolean.");
    }
};

$(".bs-select").change(function () {
    if ($.trim($(this).val()) != "") {
        if ($(this).hasClass("input-validation-error")) {
            $(this).removeClass("input-validation-error");
            $(this).addClass("valid");
            $("span[data-valmsg-for='" + $(this).attr("name") + "'").addClass("field-validation-valid").removeClass("field-validation-error").empty();
        }
    }
});

//function GetNotifications() {
//    AjaxCall("/Setup/Dashboard/GetNotifications", JSON.stringify({}), "post", "json", "application/json", false).done(function (data) {
//        if (data == "_Logon_") {
//            window.location.href = "/Login/Login";
//        }
//        else {
//            if (data != null && data != 'undefined' && data.Data != null) {
//                $('#UlNotification').empty();
//                var html = "";
//                $('#NotificationBadge').text(data.Data.length);
//                $('#NotificationCount').text(data.Data.length + " pending");

//                for (var i = 0; i < data.Data.length > 0; i++) {
//                    var href = "";

//                    if (data.Data[i].NotificationType.toUpperCase() == "NEW PROPOSAL" || data.Data[i].NotificationType.toUpperCase() == "APPROVED" || data.Data[i].NotificationType.toUpperCase() == "REJECTED") {

//                        href = "/" + data.Data[i].ModuleName + "/Proposal/Proposal?id=" + data.Data[i].EncryptedAssociationID + "&LeadID=" + data.Data[i].EncryptedLeadId + "&NotificationIDStr=" + data.Data[i].EncryptedNotificationID + "";

//                    } else if (data.Data[i].NotificationType.toUpperCase() == "Proposal Status") {

//                        href = "/" + data.Data[i].ModuleName + "/Proposal/Proposal?id=" + data.Data[i].EncryptedAssociationID + "&LeadID=" + data.Data[i].EncryptedLeadId + "&NotificationIDStr=" + data.Data[i].EncryptedNotificationID + "";

//                    }else if (data.Data[i].NotificationType.toUpperCase() == "NEW ACTIVITY") {

//                        href = "/" + data.Data[i].ModuleName + "/Activity/Activity?id=" + data.Data[i].EncryptedAssociationID + "&LeadID=" + data.Data[i].EncryptedLeadId + "&comingFrom=Activity&NotificationIDStr=" + data.Data[i].EncryptedNotificationID + "";

//                    } else if (data.Data[i].NotificationType.toUpperCase() == "NEW ASSIGNMENT") {

//                        href = "/" + data.Data[i].ModuleName + "/ProjectView/ProjectView?id=" + data.Data[i].EncryptedAssociationID + "&NotificationType=" + data.Data[i].AssociationName + "&NotificationIDStr=" + data.Data[i].EncryptedNotificationID + "";

//                    } else if (data.Data[i].NotificationType.toUpperCase() == "NEW LEAD") {

//                        href = "/" + data.Data[i].ModuleName + "/Leads/Leads?id=" + data.Data[i].EncryptedAssociationID + "&NotificationIDStr=" + data.Data[i].EncryptedNotificationID + "";

//                    } else if (data.Data[i].NotificationType.toUpperCase() == "NEW LEAD ASSIGN") {

//                        href = "/" + data.Data[i].ModuleName + "/Leads/Leads?id=" + data.Data[i].EncryptedAssociationID + "&NotificationIDStr=" + data.Data[i].EncryptedNotificationID + "";

//                    }else if (data.Data[i].NotificationType.toUpperCase() == "LEAVE REQUEST") {

//                        href = "/" + data.Data[i].ModuleName + "/leave/leave?id=" + data.Data[i].EncryptedAssociationID + "&NotificationIDStr=" + data.Data[i].EncryptedNotificationID + "";

//                    } else if (data.Data[i].NotificationType.toUpperCase() == "LEAVE APPROVAL") {

//                        href = "/" + data.Data[i].ModuleName + "/leave/leave?id=" + data.Data[i].EncryptedAssociationID + "&NotificationIDStr=" + data.Data[i].EncryptedNotificationID + "";

//                    } else if (data.Data[i].NotificationType.toUpperCase() == "NEW TRAINING SESSION CREATED" || data.Data[i].NotificationType.toUpperCase() == "TRAINING SESSION FOR DIRECTOR" || data.Data[i].NotificationType.toUpperCase() == "NEW TRAINING SESSION FOR TRAINER") {

//                        href = "/" + data.Data[i].ModuleName + "/TrainingSessionMaster/TrainingSessionMasters?id=" + data.Data[i].EncryptedAssociationID + "&NotificationIDStr=" + data.Data[i].EncryptedNotificationID + "";

//                    } else if (data.Data[i].NotificationType.toUpperCase() == "NEW REQUISITION" || data.Data[i].NotificationType.toUpperCase() == "REQUISITION UPDATE") {

//                        href = "/" + data.Data[i].ModuleName + "/CandidateSearch/RequisitionList?id=" + data.Data[i].EncryptedAssociationID + "&NotificationIDStr=" + data.Data[i].EncryptedNotificationID + "";

//                    } else if (data.Data[i].NotificationType.toUpperCase() == "REQUISITION APPROVAL" || data.Data[i].NotificationType.toUpperCase() == "REQUISITION APPROVAL STATUS") {
//                        href = "/" + data.Data[i].ModuleName + "/CandidateSearch/RequisitionList?id=" + data.Data[i].EncryptedAssociationID + "&NotificationIDStr=" + data.Data[i].EncryptedNotificationID + "";

//                    } else if (data.Data[i].NotificationType.toUpperCase() == "TRAINING FEEDBACK") {

//                        href = "/" + data.Data[i].ModuleName + "/TrainingFeedback/TrainingFeedback?TrainingSessionID=" + data.Data[i].EncryptedAssociationID + "&NotificationIDStr=" + data.Data[i].EncryptedNotificationID + "";

//                    } else if (data.Data[i].NotificationType.toUpperCase() == "CIRCULAR APPROVAL") {

//                        href = "/" + data.Data[i].ModuleName + "/Circular/CircularViewDetails?id=" + data.Data[i].EncryptedAssociationID + "&NotificationIDStr=" + data.Data[i].EncryptedNotificationID + "";

//                    } else if (data.Data[i].NotificationType.toUpperCase() == "TRAINING SESSION") {

//                        href = "/" + data.Data[i].ModuleName + "/TrainingSessionMaster/TrainingSessionMaster?id=" + data.Data[i].EncryptedAssociationID + "&NotificationIDStr=" + data.Data[i].EncryptedNotificationID + "";

//                    } else if (data.Data[i].NotificationType.toUpperCase() == "TRAINING TOPIC PUBLISHED") {

//                        href = "/" + data.Data[i].ModuleName + "/Circular/CircularViewDetails?id=" + data.Data[i].EncryptedAssociationID + "&NotificationIDStr=" + data.Data[i].EncryptedNotificationID + "";

//                    } else if (data.Data[i].NotificationType.toUpperCase() == "NEW TASK") {

//                        href = "/" + data.Data[i].ModuleName + "/ProjectView/ProjectView?id=" + data.Data[i].EncryptedTaskAssignmentID + "&NotificationAssociationIDStr=" + data.Data[i].EncryptedAssociationID + "&NotificationType=" + data.Data[i].AssociationName + "&NotificationIDStr=" + data.Data[i].EncryptedNotificationID + "";

//                    } else if (data.Data[i].NotificationType.toUpperCase() == "NEW SUBTASK") {
//                        href = "/" + data.Data[i].ModuleName + "/ProjectView/ProjectView?id=" + data.Data[i].EncryptedTaskAssignmentID + "&NotificationAssociationIDStr=" + data.Data[i].EncryptedAssociationID + "&NotificationType=" + data.Data[i].AssociationName + "&NotificationIDStr=" + data.Data[i].EncryptedNotificationID + "&RefNotificationIDStr=" + data.Data[i].RefNotificationID + "";

//                    } else if (data.Data[i].NotificationType.toUpperCase() == "NEW ASSET REQUEST" || data.Data[i].NotificationType.toUpperCase() == "ASSET ALLOCATION") {

//                        href = "/" + data.Data[i].ModuleName + "/AssetRequest/AssetRequestList?id=" + data.Data[i].EncryptedAssociationID + "&NotificationIDStr=" + data.Data[i].EncryptedNotificationID + "";

//                    } else if (data.Data[i].NotificationType.toUpperCase() == "NEW REPAIR/LOSS REQUEST") {

//                        href = "/" + data.Data[i].ModuleName + "/RepairLossRequest/RepairLossRequestList?id=" + data.Data[i].EncryptedAssociationID + "&NotificationIDStr=" + data.Data[i].EncryptedNotificationID + "";

//                    } else if (data.Data[i].NotificationType.toUpperCase() == "REPAIR/LOSS REQUEST APPROVAL") {

//                        href = "/" + data.Data[i].ModuleName + "/RepairLossRequest/RepairLossRequestList?id=" + data.Data[i].EncryptedAssociationID + "&NotificationIDStr=" + data.Data[i].EncryptedNotificationID + "";

//                    } else if (data.Data[i].NotificationType.toUpperCase() == "NEW QUOTATION") {

//                        href = "/" + data.Data[i].ModuleName + "/Quotation/Quotations?id=" + data.Data[i].EncryptedAssociationID + "&NotificationIDStr=" + data.Data[i].EncryptedNotificationID + "";

//                    } else if (data.Data[i].NotificationType.toUpperCase() == "QUOTATION APPROVAL") {

//                        href = "/" + data.Data[i].ModuleName + "/Quotation/Quotations?id=" + data.Data[i].EncryptedAssociationID + "&NotificationIDStr=" + data.Data[i].EncryptedNotificationID + "";

//                    }
//                     else if (data.Data[i].NotificationType.toUpperCase() == "ASSET REQUEST APPROVED") {

//                            href = "/" + data.Data[i].ModuleName + "/AssetRequest/AssetRequestList";

//                    }
//                    else if (data.Data[i].NotificationType.toUpperCase() == "RLREQTODIRECTOR" || data.Data[i].NotificationType.toUpperCase() == "RLREQTOITANDSUPERADMIN") {

//                        href = "/" + data.Data[i].ModuleName + "/RepairLossRequest/RepairLossRequestList?id=" + data.Data[i].EncryptedAssociationID + "&NotificationRequestIDStr=" + data.Data[i].EncryptedNotificationID + "";

//                    } else if (data.Data[i].NotificationType.toUpperCase() == "REASSIGNTOEMP") {

//                        href = "/" + data.Data[i].ModuleName + "/RepairLossRequest/UpdateViewReassignNotification?notificationid=" + data.Data[i].EncryptedNotificationID + "";

//                    } else if (data.Data[i].NotificationType.toUpperCase() == "NEW OPE") {

//                        href = "/" + data.Data[i].ModuleName + "/OPERequest/OPERequests?id=" + data.Data[i].EncryptedAssociationID + "&NotificationIDStr=" + data.Data[i].EncryptedNotificationID + "";

//                    } else if (data.Data[i].NotificationType.toUpperCase() == "NEW OPE FOR APPROVAL") {

//                        href = "/" + data.Data[i].ModuleName + "/OPERequest/OPERequests?encryptedid=" + data.Data[i].EncryptedAssociationID + "&EnNotificationIDStr=" + data.Data[i].EncryptedNotificationID + "";

//                    } else if (data.Data[i].NotificationType.toUpperCase() == "OPE APPROVAL STATUS") {

//                        href = "/" + data.Data[i].ModuleName + "/OPERequest/OPERequests?encryptedid=" + data.Data[i].EncryptedAssociationID + "&EnNotificationIDStr=" + data.Data[i].EncryptedNotificationID + "&NotificationType=" + data.Data[i].NotificationType + "";

//                    } else if (data.Data[i].NotificationType.toUpperCase() == "OPE BOOK STATUS") {

//                        href = "/" + data.Data[i].ModuleName + "/OPERequest/OPERequests?encryptedid=" + data.Data[i].EncryptedAssociationID + "&EnNotificationIDStr=" + data.Data[i].EncryptedNotificationID + "&NotiType=" + data.Data[i].NotificationType + "";

//                    } else if (data.Data[i].NotificationType.toUpperCase() == "NEW OPE SETTLEMENT FOR APPROVAL") {

//                        href = "/" + data.Data[i].ModuleName + "/OPERequest/OPERequestSettlement?EnOPEReqID=" + data.Data[i].EncryptedAssociationID + "&NotificationIDStr=" + data.Data[i].EncryptedNotificationID + "";

//                    } else if (data.Data[i].NotificationType.toUpperCase() == "OPE SETTLEMENT APPROVAL STATUS") {

//                        href = "/" + data.Data[i].ModuleName + "/OPERequest/OPERequestSettlement?EnOPEReqID=" + data.Data[i].EncryptedAssociationID + "&NotificationIDStr=" + data.Data[i].EncryptedNotificationID + "";

//                    } else if (data.Data[i].NotificationType.toUpperCase() == "CLOSE OPE") {

//                        href = "/" + data.Data[i].ModuleName + "/OPERequest/OPERequests?id=" + data.Data[i].EncryptedAssociationID + "&NotificationIDStr=" + data.Data[i].EncryptedNotificationID + "";

//                    } else if (data.Data[i].NotificationType.toUpperCase() == "NEW APPRAISAL PROCESS") {

//                        href = "/" + data.Data[i].ModuleName + "/AppraisalHeader/AppraisalViewDetail?id=" + data.Data[i].EncryptedAssociationID + "&NotificationIDStr=" + data.Data[i].EncryptedNotificationID + "";

//                    } else if (data.Data[i].NotificationType.toUpperCase() == "KRA APPRAISAL PROCESS" || data.Data[i].NotificationType.toUpperCase() == "KRA APPRAISAL TL PROCESS") {

//                        href = "/" + data.Data[i].ModuleName + "/AppraisalHeader/Appraisal_KRA?id=" + data.Data[i].EncryptedAssociationID + "&NotificationIDStr=" + data.Data[i].EncryptedNotificationID + "";

//                    } else if (data.Data[i].NotificationType.toUpperCase() == "BVORAL APPRAISAL PROCESS" || data.Data[i].NotificationType.toUpperCase() == "BVoral Appraisal TL Process") {

//                        href = "/" + data.Data[i].ModuleName + "/AppraisalHeader/Appraisal_Bvoral?id=" + data.Data[i].EncryptedAssociationID + "&NotificationIDStr=" + data.Data[i].EncryptedNotificationID + "";

//                    } else if (data.Data[i].NotificationType.toUpperCase() == "SALARY PROCESS") {

//                        href = "/Account/EmployeeInvoice/EmployeeInvoices?NotificationIDStr=" + data.Data[i].EncryptedNotificationID + "";

//                    } else if (data.Data[i].NotificationType.toUpperCase() == "NEW RESIGNATION REQUEST") {

//                        href = "/" + data.Data[i].ModuleName + "/ResignationRequest/ResignationRequestApproval?id=" + data.Data[i].EncryptedAssociationID + "&NotificationIDStr=" + data.Data[i].EncryptedNotificationID + "";

//                    } else if (data.Data[i].NotificationType.toUpperCase() == "RESIGNATION STATUS") {

//                        href = "/" + data.Data[i].ModuleName + "/ResignationRequest/ResignationRequestList?NotificationIDStr=" + data.Data[i].EncryptedNotificationID + "";

//                    } else if (data.Data[i].NotificationType.toUpperCase() == "NEW EXIT CLEARANCE") {

//                        href = "/" + data.Data[i].ModuleName + "/ExitClearance/ExitClearance?id=" + data.Data[i].EncryptedAssociationID + "&NotificationIDStr=" + data.Data[i].EncryptedNotificationID + "";

//                    } else {
//                        href = "javascript:;";
//                    }

//                    //html = html + "<li class='notification'><a href='" + href + "'><span class='time'>" + data.Data[i].NotificationTime + "</span><span class='details'><span class='label label-sm label-icon label-success custom-label-success'><i class='fa fa-bell-o'></i></span>" + data.Data[i].NotificationText + "</span><div class='mt-comment-details'><span class='mt-comment-status mt-comment-status-pending'>From : <span class='label label-sm label-success font11'>" + data.Data[i].UserName + "</span></span></div></a></li>";

//                    html = html + "<li class='notification'><a href='" + href + "'><span class='time'>" + data.Data[i].NotificationTime + "</span><span class='details'>" + data.Data[i].NotificationText + "</span><div class='mt-comment-details'><span class='mt-comment-status mt-comment-status-pending'><b>From : </b><span class='label label-sm label-success font12'>" + data.Data[i].UserName + "</span></span></div></a></li>";
//                }
//                $('#UlNotification').append(html);
//            }
//            else {
//                $('#NotificationBadge').text("0");
//                $('#NotificationCount').text("No pending");
//            }
//        }
//    });
//}

//(function () {
//    function checkTime(i) {
//        return (i < 10) ? "0" + i : i;
//    }

//    function startTime() {
//        var month = new Array();
//        month[0] = "Jan";
//        month[1] = "Feb";
//        month[2] = "Mar";
//        month[3] = "Apr";
//        month[4] = "May";
//        month[5] = "Jun";
//        month[6] = "Jul";
//        month[7] = "Aug";
//        month[8] = "Sept";
//        month[9] = "Oct";
//        month[10] = "Nov";
//        month[11] = "Dec";

//        var today = new Date(),

//            h = checkTime(today.getHours()),
//            m = checkTime(today.getMinutes()),
//            s = checkTime(today.getSeconds());

//        var mn = month[today.getMonth()].toUpperCase();
//        var year = today.getFullYear();
//        var dt = today.getDate();
//        if (dt < 10) {
//            dt = '0' + dt;
//        }
//        var hours = (h + 24 - 2) % 24;
//        var mid = 'am';
//        if (h > 12) {
//            h = h - 12;
//            mid = 'pm';
//        }

//        $('#StartTime').html(h + ":" + m + ":" + s);
//        t = setTimeout(function () {
//            startTime()
//        }, 500);
//        $("#StartDate").text(dt + " " + mn + " " + year + " ");
//        $('#StartIsmornit').text(mid);

//        $('#EndTime').html(h + ":" + m + ":" + s);
//        t = setTimeout(function () {
//            startTime()
//        }, 500);
//        $("#EndDate").text(dt + " " + mn + " " + year + " ");
//        $('#EndIsmornit').text(mid);
//        $('#currentmonth').text(mn).css("text-transform", "uppercase");
//    }
//    startTime();
//})();

//function SaveStartTimeForAttendence(obj) {
//    ShowLoader();
//    $.ajax({
//        url: "/Setup/Dashboard/SaveStartTimeForAttendence",
//        type: "POST",
//        data: "",
//        success: function (response) {
//            HideLoader();
//            if (response == "_Logon_") {
//                window.location.href = "/Login/Login";
//            }
//            else {
//                if (response.IsSuccess) {
//                    $("#StartTimerBtn").addClass("hide");
//                    $("#EndTimerBtn").removeClass("hide");
//                    $("#EndTimerBtn").attr("AttendenceID", response.Data);
//                }
//            }
//        }
//    })
//}

//function SaveEndTimeForAttendence(obj) {
//    var AttendenceID = $(obj).attr("attendenceid");
//    if (AttendenceID.trim() != '') {
//        ShowLoader();
//        $.ajax({
//            url: "/Setup/Dashboard/SaveEndTimeForAttendence",
//            type: "POST",
//            data: { AttendenceIDStr: AttendenceID },
//            success: function (response) {
//                HideLoader();
//                if (response == "_Logon_") {
//                    window.location.href = "/Login/Login";
//                }
//                else {
//                    if (response.IsSuccess) {
//                        $("#EndTimerBtn").removeAttr("AttendenceID");
//                        $("#EndTimerBtn").addClass("hide");
//                        $("#StartTimerBtn").removeClass("hide");
//                    }
//                }
//            }
//        })
//    }
//}
