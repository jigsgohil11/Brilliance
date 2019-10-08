var table = $('#dt_task').DataTable({});
var table = $('#dt_Discussion').DataTable({});
var table = $('#dt_timesheet').DataTable({});
var table = $('#dt_Area').DataTable({});
var table = $('#dt_subtask').DataTable({});
var table = $('#dt_areatask').DataTable({});
var table = $('#dt_timesheetForEditTask').DataTable({});
var table = $('#tblassignmentfileDocument').DataTable({});
//var table = $('#tblEmpDocument').DataTable({});

$(document).ready(function () {
    $('[data-toggle="tooltip"]').tooltip();
    LoadChart();
    //$(".form_datetime").datetimepicker({
    //    autoclose: true,
    //    isRTL: App.isRTL(),
    //    format: "dd MM yyyy - hh:ii",
    //    fontAwesome: true,
    //    pickerPosition: (App.isRTL() ? "bottom-right" : "bottom-left")
    //});

    $('#Tasks_Comment').summernote({
        height: 100,                 // set editor height

        minHeight: null,             // set minimum height of editor
        maxHeight: null,             // set maximum height of editor

        focus: true                 // set focus to editable area after initializing summernote
    });

    $("#tab_15_1").click(function () {
        var TabType = "Overview";
        GetDataListByTabClick(TabType);
        $("#tab1").fadeIn();
        $("#tab1").addClass("active");
        $("#tab2,#tab3,#tab4,#tab5,#tab6,#tab7").removeClass("active");
        $(this).closest("ul").children("li").removeClass("active");
        $(this).closest("li").addClass("active");
        $("#tab2,#tab3,#tab4,#tab5,#tab6,#tab7").hide();
    });

    $("#tab_15_2").click(function () {
        var TabType = "Milestone";
        GetDataListByTabClick(TabType);
        $("#tab2").fadeIn();
        $("#tab2").addClass("active");
        $("#tab1,#tab3,#tab4,#tab5,#tab6,#tab7").removeClass("active");
        $(this).closest("ul").children("li").removeClass("active");
        $(this).closest("li").addClass("active");
        $("#tab1,#tab3,#tab4,#tab5,#tab6,#tab7").hide();
    });

    $("#tab_15_3").click(function () {
        var TabType = "Tasks";
        GetDataListForTaskFilter(TabType);
        $("#tab3").fadeIn();
        $("#tab3").addClass("active");
        $("#tab1,#tab2,#tab4,#tab5,#tab6,#tab7").removeClass("active");
        $(this).closest("ul").children("li").removeClass("active");
        $(this).closest("li").addClass("active");
        $("#tab1,#tab2,#tab4,#tab5,#tab6,#tab7").hide();
    });

    $("#tab_15_4").click(function () {
        var TabType = "TimeSheet";
        GetDataListByTabClick(TabType);
        $("#tab4").fadeIn();
        $("#tab4").addClass("active");
        $("#tab1,#tab2,#tab3,#tab5,#tab6,#tab7").removeClass("active");
        $(this).closest("ul").children("li").removeClass("active");
        $(this).closest("li").addClass("active");
        $("#tab1,#tab2,#tab3,#tab5,#tab6,#tab7").hide();
    });

    $("#tab_15_5").click(function () {
        var TabType = "File";
        GetDataListByTabClick(TabType);
        $("#tab5").fadeIn();
        $("#tab5").addClass("active");
        $("#tab1,#tab2,#tab3,#tab4,#tab6,#tab7").removeClass("active");
        $(this).closest("ul").children("li").removeClass("active");
        $(this).closest("li").addClass("active");
        $("#tab1,#tab2,#tab3,#tab4,#tab6,#tab7").hide();
    });

    $("#tab_15_6").click(function () {
        var TabType = "Discussion";
        GetDataListByTabClick(TabType);
        $("#tab6").fadeIn();
        $("#tab6").addClass("active");
        $("#tab1,#tab2,#tab3,#tab4,#tab5,#tab7").removeClass("active");
        $(this).closest("ul").children("li").removeClass("active");
        $(this).closest("li").addClass("active");
        $("#tab1,#tab2,#tab3,#tab4,#tab5,#tab7").hide();
    });

    $("#tab_15_7").click(function () {
        var TabType = "Area";
        GetDataListByTabClick(TabType);
        $("#tab7").fadeIn();
        $("#tab7").addClass("active");
        $("#tab1,#tab2,#tab3,#tab4,#tab5,#tab6").removeClass("active");
        $(this).closest("ul").children("li").removeClass("active");
        $(this).closest("li").addClass("active");
        $("#tab1,#tab2,#tab3,#tab4,#tab5,#tab6").hide();
    });

    if (NotificationType.trim() != '') {
        if (NotificationType.toString().toUpperCase() == "TASK" || NotificationType.toString().toUpperCase() == "SUBTASK") {
            $("#tab_15_3").click();
            //$("#ProjectViewDataUL li.Overview").removeClass("active");
            //$("#ProjectViewDataUL li.Tasks").addClass("active");
            //$("#ProjectTabData .tab-pane.Overview").removeClass("active");
            //$("#ProjectTabData .tab-pane.Tasks").addClass("active");
            //var AssignmentID = $("#hiddenAssignmentID").val();
            //var form = "form_LoadTaskData";
            //LoadTaskData(NotificationAssociationID, AssignmentID, form);
        }
    } else {
        if (DefaultAccessTab.trim() != '') {
            if (DefaultAccessTab.toString().toUpperCase() == "OVERVIEW") {
                //$("#tab_15_1").click();
            }
            else if (DefaultAccessTab.toString().toUpperCase() == "AREA") {
                $("#tab_15_7").click();
            }
            else if (DefaultAccessTab.toString().toUpperCase() == "MILESTONE") {
                $("#tab_15_2").click();
            }
            else if (DefaultAccessTab.toString().toUpperCase() == "TASKS") {
                $("#tab_15_3").click();
            }
            else if (DefaultAccessTab.toString().toUpperCase() == "TIMESHEET") {
                $("#tab_15_4").click();
            }
            else if (DefaultAccessTab.toString().toUpperCase() == "FILE") {
                $("#tab_15_5").click();
            }
            else if (DefaultAccessTab.toString().toUpperCase() == "DISCUSSION") {
                $("#tab_15_6").click();
            }
        }
    }

});

function GetDataListByTabClick(TabType) {
    var hiddenAssignmentID = $("#hiddenAssignmentID").val();
    ShowLoader();
    $.ajax({
        url: "/Pms/ProjectView/GetDataListByTabClick",
        type: "POST",
        data: { TabTypeStr: TabType, AssignmentIDStr: hiddenAssignmentID, AreaIDStr: AreaFilterNameForMilestoneList },
        success: function (response) {
            HideLoader();
            if (response == "_Logon_") {
                window.location.href = "/Login/Login";
            }
            else {
                if (TabType == "Overview") {
                    $("#OverviewByProjectID").html(response);
                    LoadChart();
                    $("#range_2").ionRangeSlider({
                        min: 100,
                        max: 1000,
                        from: 550
                    });
                    $('.bs-select').selectpicker('refresh');
                    var widgets = widgets = DonutWidget.draw();
                    $('[data-toggle="tooltip"]').tooltip();
                }
                if (TabType == "Milestone") {
                    $("#MileastoneListByProjectID").html(response);
                    $('[data-toggle="tooltip"]').tooltip();
                    $("#MilestoneList_Filter_AreaID option[value='" + AreaFilterNameForMilestoneList + "']").attr('selected', 'selected');
                    $('.bs-select').selectpicker('refresh');
                    $('.scroller').slimScroll({
                        allowPageScroll: true, // allow page scroll when the element scroll is ended
                        size: '7px',
                        color: ($(this).attr("data-handle-color") ? $(this).attr("data-handle-color") : '#bbb'),
                        wrapperClass: ($(this).attr("data-wrapper-class") ? $(this).attr("data-wrapper-class") : 'slimScrollDiv'),
                        railColor: ($(this).attr("data-rail-color") ? $(this).attr("data-rail-color") : '#eaeaea'),
                        position: 'right',
                        height: '345px',
                        alwaysVisible: ($(this).attr("data-always-visible") == "1" ? true : false),
                        railVisible: ($(this).attr("data-rail-visible") == "1" ? true : false),
                        disableFadeOut: true
                    });
                }
                if (TabType == "Area") {
                    $("#AreaListByProjectID").html(response);
                    var table = $('#dt_Area').DataTable({});
                    $('[data-toggle="tooltip"]').tooltip();
                }
                if (TabType == "Tasks") {
                    $("#TaskListByProjectID").html(response);
                    var table = $('#dt_task').DataTable({});
                    $('[data-toggle="tooltip"]').tooltip();
                    if (NotificationType.trim() != '') {
                        if (NotificationType.toString().toUpperCase() == "TASK") {
                            //$("#tab_15_3").click();
                            //$("#ProjectViewDataUL li.Overview").removeClass("active");
                            //$("#ProjectViewDataUL li.Tasks").addClass("active");
                            //$("#ProjectTabData .tab-pane.Overview").removeClass("active");
                            //$("#ProjectTabData .tab-pane.Tasks").addClass("active");
                            var AssignmentID = $("#hiddenAssignmentID").val();
                            var form = "form_TaskDetails";
                            EditTaskData(NotificationAssociationID, AssignmentID, form);
                        }
                        if (NotificationType.toString().toUpperCase() == "SUBTASK") {
                            var AssignmentID = $("#hiddenAssignmentID").val();
                            var form = "form_SubTaskDetails";
                            EditSubTaskData(NotificationAssociationID, AssignmentID, form, RefNotificationID)
                        }
                    }
                }
                if (TabType == "TimeSheet") {
                    $("#TimesheetListByProjectID").html(response);
                    var table = $('#dt_timesheet').DataTable({});
                    $('[data-toggle="tooltip"]').tooltip();
                }
                if (TabType == "Discussion") {
                    $("#DiscussionByProjectID").html(response);
                    var table = $('#dt_Discussion').DataTable({});
                    $('[data-toggle="tooltip"]').tooltip();
                }
                if (TabType == "File") {
                    $("#FileListByProjectID").html(response);
                    var table = $('#tblassignmentfileDocument').DataTable({});
                    $('.bs-select').selectpicker('refresh');
                    $('[data-toggle="tooltip"]').tooltip();
                }
            }
        }
    })
}

function GetDataListForTaskFilter(TabType) {
    var hiddenAssignmentID = $("#hiddenAssignmentID").val();
    ShowLoader();
    $.ajax({
        url: "/Pms/ProjectView/GetDataListForTaskFilter",
        type: "POST",
        data: { AssignmentIDStr: hiddenAssignmentID, AreaIDStr: AreaFilterNameForMilestoneList },
        success: function (response) {
            HideLoader();
            if (response == "_Logon_") {
                window.location.href = "/Login/Login";
            }
            else {
                if (TabType == "Tasks") {
                    $("#TaskFilterDIV").html(response);
                    $('.bs-select').selectpicker('refresh');
                    $('.date-picker123').datepicker({
                        format: 'dd/mm/yyyy',
                        autoclose: true,
                        orientation: "bottom",
                        todayHighlight: true
                    });

                    $(function () {
                        var startDate = new Date('01/01/2018');
                        var FromEndDate = new Date();
                        var ToEndDate = new Date();

                        $('.date-picker-start').datepicker({
                            autoclose: true,
                            format: 'dd/mm/yyyy',
                            orientation: "bottom",
                            todayHighlight: true
                        })
                            .on('changeDate', function (selected) {
                                $('.date-picker-end').val("");
                                startDate = new Date(selected.date.valueOf());
                                startDate.setDate(startDate.getDate(new Date(selected.date.valueOf())));
                                $('.date-picker-end').datepicker('setStartDate', startDate);
                            });
                        $('.date-picker-end').datepicker({
                            format: 'dd/mm/yyyy',
                            autoclose: true,
                            orientation: "bottom",
                            todayHighlight: true
                        })
                    });

                    $("#TaskList_Filter_AreaID option[value='" + AreaFilterNameForMilestoneList + "']").attr('selected', 'selected');
                    $('.bs-select').selectpicker('refresh');

                    GetDataListByTabClick(TabType);
                }
            }
        }
    })
}

function showChartTooltip(x, y, xValue, yValue) {
    $('<div id="tooltip" class="chart-tooltip">' + yValue + '<\/div>').css({
        position: 'absolute',
        display: 'none',
        top: y - 40,
        left: x - 40,
        border: '0px solid #ccc',
        padding: '2px 6px',
        'background-color': '#fff'
    }).appendTo("body").fadeIn(200);
}

var visitors = [
        ['02/2013', 10],
        ['03/2013', 9],
        ['04/2013', 5],
        ['05/2013', 8],
        ['06/2013', 9],
        ['07/2013', 10],
        ['08/2013', 02],
        ['09/2013', 04],
        ['10/2013', 7]
];

function LoadChart() {

    if ($('#site_statistics').size() != 0) {

        $('#site_statistics_loading').hide();
        $('#site_statistics_content').show();

        var plot_statistics = $.plot($("#site_statistics"), [{
            data: visitors,
            lines: {
                fill: 0.6,
                lineWidth: 0
            },
            color: ['#f89f9f']
        }, {
            data: visitors,
            points: {
                show: true,
                fill: true,
                radius: 5,
                fillColor: "#f89f9f",
                lineWidth: 3
            },
            color: '#fff',
            shadowSize: 0
        }],

            {
                xaxis: {
                    tickLength: 0,
                    tickDecimals: 0,
                    mode: "categories",
                    min: 0,
                    font: {
                        lineHeight: 14,
                        style: "normal",
                        variant: "small-caps",
                        color: "#6F7B8A"
                    }
                },
                yaxis: {
                    ticks: 5,
                    tickDecimals: 0,
                    tickColor: "#eee",
                    font: {
                        lineHeight: 14,
                        style: "normal",
                        variant: "small-caps",
                        color: "#6F7B8A"
                    }
                },
                grid: {
                    hoverable: true,
                    clickable: true,
                    tickColor: "#eee",
                    borderColor: "#eee",
                    borderWidth: 1
                }
            });

        var previousPoint = null;
        $("#site_statistics").bind("plothover", function (event, pos, item) {
            $("#x").text(pos.x.toFixed(2));
            $("#y").text(pos.y.toFixed(2));
            if (item) {
                if (previousPoint != item.dataIndex) {
                    previousPoint = item.dataIndex;

                    $("#tooltip").remove();
                    var x = item.datapoint[0].toFixed(2),
                        y = item.datapoint[1].toFixed(2);

                    showChartTooltip(item.pageX, item.pageY, item.datapoint[0], item.datapoint[1] + ' hours');
                }
            } else {
                $("#tooltip").remove();
                previousPoint = null;
            }
        });
    }

}

function OpenNewTaskModel(form) {
    GetNewDataForTaskPopup(form);
    //ClearPopup(form);
    //var hiddenAssignmentID = $("#hiddenAssignmentID").val();
    //$("#AssignmentIDForTask").val(hiddenAssignmentID);
    //$("#NewTaskForm").modal('show');
}

function OpenSubTaskListModel(TaskID, AssignmentID, TaskTitle) {
    ShowLoader();
    $.ajax({
        url: "/Pms/ProjectView/GetSubTaskListByTaskID",
        type: "POST",
        data: { TaskIDStr: TaskID, AssignmentIDStr: AssignmentID },
        success: function (response) {
            HideLoader();
            if (response == "_Logon_") {
                window.location.href = "/Login/Login";
            }
            else {
                $("#SubtaskListPopup").html(response);
                $("#SubTaskListForm").modal('show');
                var title = "Sub Tasks ( " + TaskTitle + " )";
                $("#modal_title").text(title);
                $("#hiddentitle").val(TaskTitle);
                var table = $('#dt_subtask').DataTable({});
                $('[data-toggle="tooltip"]').tooltip();
            }
        }
    })
}

function OpenAreaTaskListModel(AssignmentAreaID, AssignmentID, AreaTitle) {
    ShowLoader();
    $.ajax({
        url: "/Pms/ProjectView/GetAreaTaskListByAssignmentAreaID",
        type: "POST",
        data: { AssignmentAreaIDStr: AssignmentAreaID, AssignmentIDStr: AssignmentID },
        success: function (response) {
            HideLoader();
            if (response == "_Logon_") {
                window.location.href = "/Login/Login";
            }
            else {
                $("#AreataskListPopup").html(response);
                $("#AreaTaskListForm").modal('show');
                var title = "Tasks ( " + AreaTitle + " )";
                $("#modal_title").text(title);
                $("#hiddenAreatitle").val(AreaTitle);
                var table = $('#dt_areatask').DataTable({});
                $('[data-toggle="tooltip"]').tooltip();
            }
        }
    })
}

var dropzone;
var IsAllFileUploaded = false;

function OpenNewAreaModel(form) {
    ClearPopup(form);
    ShowLoader();
    $.ajax({
        url: "/Pms/ProjectView/ClearAttachments",
        type: "POST",
        data: {},
        success: function (response) {
            HideLoader();
            if (response == "_Logon_") {
                window.location.href = "/Login/Login";
            }
            else {

            }
        }
    });
    var hiddenAssignmentID = $("#hiddenAssignmentID").val();
    ShowLoader();
    $.ajax({
        url: "/Pms/ProjectView/GetNewDataForArea",
        type: "POST",
        data: { AssignmentIDStr: hiddenAssignmentID },
        success: function (response) {
            HideLoader();
            if (response == "_Logon_") {
                window.location.href = "/Login/Login";
            }
            else {
                $("#LoadAreaData").html(response);
                $("#AddNewAreaForm").modal('show');
                $("#" + form).removeData('validator');
                $("#" + form).removeData('unobtrusiveValidation');
                $.validator.unobtrusive.parse("#" + form);
                $("#AssignmentIDForArea").val(hiddenAssignmentID);
                $('.bs-select').selectpicker('refresh');
                $('.date-picker').datepicker({
                    format: 'dd/mm/yyyy',
                    autoclose: true,
                    orientation: "bottom",
                    todayHighlight: true
                });


                $(".date-picker").on("change", function (evt, params) {
                    $(evt.target).valid();
                });
                $(function () {

                    $.validator.methods.date = function (value, element) {
                        return this.optional(element) || moment(value, "DD/MM/YYYY", true).isValid();
                    }
                });
                $("#Areatab1").click(function () {

                    $("#area_tab1").fadeIn();
                    $("#area_tab1").addClass("active");
                    $("#area_tab2").removeClass("active");
                    $(this).closest("ul").children("li").removeClass("active");
                    $(this).closest("li").addClass("active");
                    $("#area_tab2").hide();
                });

                $("#Areatab2").click(function () {
                    $("#area_tab2").fadeIn();
                    $("#area_tab2").addClass("active");
                    $("#area_tab1").removeClass("active");
                    $(this).closest("ul").children("li").removeClass("active");
                    $(this).closest("li").addClass("active");
                    $("#area_tab1").hide();
                });

                $('#form_area_document_dropzone').dropzone({
                    url: "#",
                    acceptedFiles: ".jpeg,.jpg,.png,.gif,.pdf,.txt,.mp3,.mp4,.doc,.xls,.xlsx,.docx",
                    maxFilesize: 6,
                    paramName: "file",
                    maxThumbnailFilesize: 50,
                    url: "/Pms/ProjectView/DropzoneFileuploadFromArea",
                    addRemoveLinks: true,
                    init: function () {
                        var thisDropzone = this;
                        dropzone = this;
                        $.getJSON("/Pms/ProjectView/GetAttachmentsForAreaFiles/").done(function (data) {

                            if (data.Data != '') {
                                $.each(data.Data, function (index, item) {
                                    //// Create the mock file:
                                    var mockFile = {
                                        name: item.FileName,
                                        size: 2234566
                                    };
                                    // Call the default addedfile event handler
                                    thisDropzone.emit("addedfile", mockFile);
                                    $(mockFile.previewElement).attr("temp-data", item.AttachementID);
                                    var a = document.createElement('a');
                                    a.setAttribute('href', "/Areas/Pms/Documents/" + item.FilePath);
                                    a.setAttribute('target', '_blank');
                                    a.setAttribute('Class', 'DownloadLink');
                                    a.innerHTML = "<i class=\"fa fa-download\"></i>";
                                    mockFile.previewTemplate.appendChild(a);



                                    //var divele = document.createElement("div");


                                    //var textbx = document.createElement("input");
                                    //textbx.setAttribute("type", "text");
                                    //textbx.setAttribute("value", "");
                                    //textbx.setAttribute("id", "FileUploadId");
                                    //textbx.setAttribute("style", "width:99px;margin-top:5px;");
                                    //textbx.setAttribute("placeholder", "File Name");
                                    //divele.appendChild(textbx);
                                    //mockFile.previewTemplate.appendChild(divele);

                                });
                            }
                        });
                        this.on('sending', function (file, xhr, formData) {
                            formData.append('AssignAreaID', $("#hiddenAssignmentAreaID").val());
                            formData.append('ClientID', '');

                        });
                        this.on('success', function (file, json) {
                            $(file.previewElement).attr("temp-data", json.DI);

                            var a = document.createElement('a');
                            a.setAttribute('href', "/Areas/Pms/Documents" + "/" + json.path);
                            a.setAttribute('target', '_blank');
                            a.setAttribute('Class', 'DownloadLink');
                            a.innerHTML = "<i class=\"fa fa-download\"></i>";
                            file.previewTemplate.appendChild(a);

                            //var divele = document.createElement("div");


                            //var textbx = document.createElement("input");
                            //textbx.setAttribute("type", "text");
                            //textbx.setAttribute("id", "FileUploadId");
                            //textbx.setAttribute("value", "");
                            //textbx.setAttribute("style", "width:99px;margin-top:5px;");
                            //textbx.setAttribute("placeholder", "File Name");
                            //divele.appendChild(textbx);
                            //file.previewTemplate.appendChild(divele);
                        });
                        this.on('error', function (file, json) {
                            if (file.size > 6291456) {
                                thisDropzone.removeFile(file);
                                ShowMessage("File is too big.Max filesize:6MB.", "error");
                            }
                        });
                        this.on('removedfile', function (file) {

                            var id = $(file.previewElement).attr("temp-data");
                            ShowLoader();
                            $.ajax({
                                url: "/Pms/Projectview/RemoveAreasFiles?data=" + id,
                                type: "POST",
                                data: {},
                                processData: false,
                                contentType: false,
                                success: function (response) {
                                    HideLoader();
                                    if (response == "_Logon_") {
                                        window.location.href = "/Login/Login";
                                    }
                                    else {
                                        //RemoveProcessBar();
                                    }
                                }
                            });
                        });
                        this.on("complete", function (file) {
                            if (this.getUploadingFiles().length === 0 && this.getQueuedFiles().length === 0) {
                                IsAllFileUploaded = true;
                            }
                        });
                    }
                });
            }
        }
    })
}

function CalculateTotalManDays() {
    $("#AssignmentAreas_TotalMenDays").val('');
    if ($.trim($("#AssignmentAreas_MenDaysPerReview").val()) != "" && $.trim($("#AssignmentAreas_Frequency_Term").val()) != "" && $.trim($("#AssignmentAreas_Qty").val()) != "") {
        var ManDaysPerReview = parseInt($("#AssignmentAreas_MenDaysPerReview").val());
        var FrequencyTerm = $("#AssignmentAreas_Frequency_Term").val();
        var Qty = parseInt($("#AssignmentAreas_Qty").val());

        if (FrequencyTerm.toUpperCase() == "ANNUAL") {
            $("#AssignmentAreas_TotalMenDays").val(ManDaysPerReview * 12 * Qty);
        }
        if (FrequencyTerm.toUpperCase() == "HALF YEARLY") {
            $("#AssignmentAreas_TotalMenDays").val(ManDaysPerReview * 6 * Qty);
        }
        if (FrequencyTerm.toUpperCase() == "QUARTERLY") {
            $("#AssignmentAreas_TotalMenDays").val(ManDaysPerReview * 4 * Qty);
        }
        if (FrequencyTerm.toUpperCase() == "MONTHLY") {
            $("#AssignmentAreas_TotalMenDays").val(ManDaysPerReview * 1 * Qty);
        }
    }
}

function OpenNewTaskFromMilestoneModel(form, MilestoneID) {
    GetNewDataForMilestoneTaskPopup(form, MilestoneID);
    //ClearPopup(form);
    //var hiddenAssignmentID = $("#hiddenAssignmentID").val();
    //$("#AssignmentIDForTaskMilestone").val(hiddenAssignmentID);
    //$("#MilestoneNewTaskForm").modal('show');
}

function GetNewDataForMilestoneTaskPopup(form, MilestoneID) {
    ClearPopup(form);
    var hiddenAssignmentID = $("#hiddenAssignmentID").val();
    ShowLoader();
    $.ajax({
        url: "/Pms/ProjectView/GetNewDataForMilestoneTaskPopup",
        type: "POST",
        data: { AssignmentIDStr: hiddenAssignmentID, MilestoneIDStr: MilestoneID },
        success: function (response) {
            HideLoader();
            if (response == "_Logon_") {
                window.location.href = "/Login/Login";
            }
            else {
                $("#MilestoneNewTaskForm").modal('show');
                $("#MiletaskData").html(response);
                $("#IsAllowToCompleteTask").val("false");
                $("#" + form).removeData('validator');
                $("#" + form).removeData('unobtrusiveValidation');
                $.validator.unobtrusive.parse("#" + form);
                $("#AssignmentIDForTaskMilestone").val(hiddenAssignmentID);
                $('.bs-select').selectpicker('refresh');
                $(function () {
                    var startDate = new Date('01/01/2018');
                    var FromEndDate = new Date();
                    var ToEndDate = new Date();

                    $('.start_date_frm_mil').datepicker({
                        autoclose: true,
                        format: 'dd/mm/yyyy',
                        orientation: "bottom",
                        todayHighlight: true
                    })
                        .on('changeDate', function (selected) {
                            $('.end_date_frm_mil').val("");
                            startDate = new Date(selected.date.valueOf());
                            startDate.setDate(startDate.getDate(new Date(selected.date.valueOf())));
                            $('.end_date_frm_mil').datepicker('setStartDate', startDate);
                        });
                    $('.end_date_frm_mil').datepicker({
                        format: 'dd/mm/yyyy',
                        autoclose: true,
                        orientation: "bottom",
                        todayHighlight: true
                    })
                });
                $(function () {
                    var startDate = new Date('01/01/2018');
                    var FromEndDate = new Date();
                    var ToEndDate = new Date();

                    $('.actual_start_date_frm_mil').datepicker({
                        autoclose: true,
                        format: 'dd/mm/yyyy',
                        orientation: "bottom",
                        todayHighlight: true,
                        endDate: "0d"
                    })
                        .on('changeDate', function (selected) {
                            $('.actual_end_date_frm_mil').val("");
                            startDate = new Date(selected.date.valueOf());
                            startDate.setDate(startDate.getDate(new Date(selected.date.valueOf())));
                            $('.actual_end_date_frm_mil').datepicker('setStartDate', startDate);
                        });
                    $('.actual_end_date_frm_mil').datepicker({
                        format: 'dd/mm/yyyy',
                        autoclose: true,
                        orientation: "bottom",
                        todayHighlight: true,
                        endDate: "0d"
                    })
                });
                $(".start_date_frm_mil").on("change", function (evt, params) {
                    $(evt.target).valid();
                });
                $(".end_date_frm_mil").on("change", function (evt, params) {
                    $(evt.target).valid();
                });
                $(".actual_start_date_frm_mil").on("change", function (evt, params) {
                    $(evt.target).valid();
                });
                $(".actual_end_date_frm_mil").on("change", function (evt, params) {
                    $(evt.target).valid();
                });
                $(function () {

                    $.validator.methods.date = function (value, element) {
                        return this.optional(element) || moment(value, "DD/MM/YYYY", true).isValid();
                    }
                });
            }
        }
    })
}

function GetNewDataForTaskPopup(form) {
    ClearPopup(form);
    var hiddenAssignmentID = $("#hiddenAssignmentID").val();
    ShowLoader();
    $.ajax({
        url: "/Pms/ProjectView/GetNewDataForTaskPopup",
        type: "POST",
        data: { AssignmentIDStr: hiddenAssignmentID, AreaIDStr: AreaFilterNameForMilestoneList },
        success: function (response) {
            //$("#" + form).each(function () { $.data($(this)[0], 'validator', false); });
            //$.validator.unobtrusive.parse("#" + form);
            HideLoader();
            if (response == "_Logon_") {
                window.location.href = "/Login/Login";
            }
            else {
                $("#taskPopup").html(response);
                $("#NewTaskForm").modal('show');
                $("#IsAllowToCompleteTask").val("false");
                $("#" + form).removeData('validator');
                $("#" + form).removeData('unobtrusiveValidation');
                $.validator.unobtrusive.parse("#" + form);
                $("#AssignmentIDForTask").val(hiddenAssignmentID);
                $('.bs-select').selectpicker('refresh');
                //$('.date-picker').datepicker({
                //    format: 'dd/mm/yyyy',
                //    autoclose: true,
                //    orientation: "bottom",
                //    todayHighlight: true
                //})
                $(function () {
                    var startDate = new Date('01/01/2018');
                    var FromEndDate = new Date();
                    var ToEndDate = new Date();

                    $('.start_date_tp').datepicker({
                        autoclose: true,
                        format: 'dd/mm/yyyy',
                        orientation: "bottom",
                        todayHighlight: true
                    })
                        .on('changeDate', function (selected) {
                            $('.end_date_tp').val("");
                            startDate = new Date(selected.date.valueOf());
                            startDate.setDate(startDate.getDate(new Date(selected.date.valueOf())));
                            $('.end_date_tp').datepicker('setStartDate', startDate);
                        });
                    $('.end_date_tp').datepicker({
                        format: 'dd/mm/yyyy',
                        autoclose: true,
                        orientation: "bottom",
                        todayHighlight: true
                    })
                });
                $(function () {
                    var startDate = new Date('01/01/2018');
                    var FromEndDate = new Date();
                    var ToEndDate = new Date();

                    $('.actual_start_date_tp').datepicker({
                        autoclose: true,
                        format: 'dd/mm/yyyy',
                        orientation: "bottom",
                        todayHighlight: true,
                        endDate: "0d"
                    })
                        .on('changeDate', function (selected) {
                            $('.actual_end_date_tp').val("");
                            startDate = new Date(selected.date.valueOf());
                            startDate.setDate(startDate.getDate(new Date(selected.date.valueOf())));
                            $('.actual_end_date_tp').datepicker('setStartDate', startDate);
                        });
                    $('.actual_end_date_tp').datepicker({
                        format: 'dd/mm/yyyy',
                        autoclose: true,
                        orientation: "bottom",
                        todayHighlight: true,
                        endDate: "0d"
                    })
                });

                $(".start_date_tp").on("change", function (evt, params) {
                    $(evt.target).valid();
                });
                $(".end_date_tp").on("change", function (evt, params) {
                    $(evt.target).valid();
                });
                $(".actual_start_date_tp").on("change", function (evt, params) {
                    $(evt.target).valid();
                });
                $(".actual_end_date_tp").on("change", function (evt, params) {
                    $(evt.target).valid();
                });
                $(function () {

                    $.validator.methods.date = function (value, element) {
                        return this.optional(element) || moment(value, "DD/MM/YYYY", true).isValid();
                    }
                });
            }
        }
    })
}

function OpenNewMilestoneModel(form) {

    var AreaID = AreaFilterNameForMilestoneList;
    ClearPopup(form);

    var hiddenAssignmentID = $("#hiddenAssignmentID").val();
    ShowLoader();
    $.ajax({
        url: "/Pms/ProjectView/GetNewDataForMilestonePopup",
        type: "POST",
        data: { AssignmentIDStr: hiddenAssignmentID, AreaIDStr: AreaID },
        success: function (response) {
            HideLoader();
            if (response == "_Logon_") {
                window.location.href = "/Login/Login";
            }
            else {
                $("#milestonepopup").html(response);
                $("#NewMilestoneForm").modal('show');
                AreaFilterNameForMilestoneList = AreaID.toString().toUpperCase();
                $("#Milestones_AreaID option[value='" + AreaFilterNameForMilestoneList + "']").attr('selected', 'selected');
                $('.bs-select').selectpicker('refresh');
                $('#DVRecuringterm').hide();
                $("#" + form).removeData('validator');
                $("#" + form).removeData('unobtrusiveValidation');
                $.validator.unobtrusive.parse("#" + form);
                $("#AssignmentIDForMilestone").val(hiddenAssignmentID);
                $('.bs-select').selectpicker('refresh');
                //$('.date-picker').datepicker({
                //    format: 'dd/mm/yyyy',
                //    autoclose: true,
                //    orientation: "bottom",
                //    todayHighlight: true
                //})
                $(function () {
                    var startDate = new Date('01/01/2018');
                    var FromEndDate = new Date();
                    var ToEndDate = new Date();

                    $('.start_date_mil').datepicker({
                        autoclose: true,
                        format: 'dd/mm/yyyy',
                        orientation: "bottom",
                        todayHighlight: true
                    })
                        .on('changeDate', function (selected) {
                            $('.end_date_mil').val("");
                            startDate = new Date(selected.date.valueOf());
                            startDate.setDate(startDate.getDate(new Date(selected.date.valueOf())));
                            $('.end_date_mil').datepicker('setStartDate', startDate);
                        });
                    $('.end_date_mil').datepicker({
                        format: 'dd/mm/yyyy',
                        autoclose: true,
                        orientation: "bottom",
                        todayHighlight: true
                    })
                });
                //alert(AssignmentStartDate + ":::::::::::" + AssignmentEndDate);
                //$('.start_date').datepicker('setStartDate', AssignmentStartDate);
                //$('.start_date').datepicker('setEndDate', AssignmentEndDate);
                $(".start_date_mil").on("change", function (evt, params) {
                    $(evt.target).valid();
                });
                $(".end_date_mil").on("change", function (evt, params) {
                    $(evt.target).valid();
                });
                $(function () {

                    $.validator.methods.date = function (value, element) {
                        return this.optional(element) || moment(value, "DD/MM/YYYY", true).isValid();
                    }
                });
            }
        }
    })
}

function GetAreaWiseMemberListForMilestone(obj) {
    $("#Milestones_AssignTo").empty();
    //$("#Milestones_AssignTo").append('<option value="">--Select Member--</option>');
    $("#Milestones_AssignTo").val("");
    $("#Milestones_AssignTo").selectpicker('refresh');
    $("#Milestones_MilestoneManagerID").empty();
    $("#Milestones_MilestoneManagerID").val("");
    $("#Milestones_MilestoneManagerID").selectpicker('refresh');
    var AreaID = $(obj).find("option:selected").val().toLowerCase();
    var hiddenAssignmentID = $("#hiddenAssignmentID").val();
    var postData = {
        varAreaID: AreaID,
        varAssignmentID: hiddenAssignmentID
    };
    ShowLoader();
    AjaxCall("/Pms/ProjectView/GetAreaWiseMemberListForMilestone", JSON.stringify(postData), "post", "json", "application/json", false).done(function (data) {
        HideLoader();
        if (data == "_Logon_") {
            window.location.href = "/Login/Login";
        }
        else {
            if (data == 'error') {
                toastr["error"]("Oops Something Wentwrong !!!", "Milestone", { "positionClass": "toast-bottom-right", "progressBar": true });
            }
            else {
                $("#Milestones_AssignTo").html('');
                $("#Milestones_AssignTo").val('');
                //$("#Milestones_AssignTo").append('<option value="">--Select Member--</option>');
                for (var i = 0; i < data.length; i++) {
                    $("#Milestones_AssignTo").append('<option value="' + data[i].EmployeeID + '">' + data[i].DisplayName + '</option>');
                }
                $("#Milestones_AssignTo").selectpicker('refresh');

                $("#Milestones_MilestoneManagerID").html('');
                $("#Milestones_MilestoneManagerID").val('');
                $("#Milestones_MilestoneManagerID").append('<option value="">--Select Team Lead--</option>');
                for (var i = 0; i < data.length; i++) {
                    $("#Milestones_MilestoneManagerID").append('<option value="' + data[i].EmployeeID + '">' + data[i].DisplayName + '</option>');
                }
                $("#Milestones_MilestoneManagerID").selectpicker('refresh');
            }
        }
    });
}

function GetAreaWiseMilestoneListForAreaTask(obj) {
    $("#AreaTask_MilestoneID").empty();
    $("#AreaTask_MilestoneID").val("");
    $("#AreaTask_MilestoneID").selectpicker('refresh');
    $("#AreaTask_AssignTo").empty();
    $("#AreaTask_AssignTo").val("");
    $("#AreaTask_AssignTo").selectpicker('refresh');
    var AssignmentAreaID = $(obj).find("option:selected").val().toLowerCase();
    var hiddenAssignmentID = $("#hiddenAssignmentID").val();
    var postData = {
        varAssignmentAreaID: AssignmentAreaID,
        varAssignmentID: hiddenAssignmentID
    };
    ShowLoader();
    AjaxCall("/Pms/ProjectView/GetAreaWiseMilestoneListForAreaTask", JSON.stringify(postData), "post", "json", "application/json", false).done(function (data) {
        HideLoader();
        if (data == "_Logon_") {
            window.location.href = "/Login/Login";
        }
        else {
            if (data == 'error') {
                toastr["error"]("Oops Something Wentwrong !!!", "Milestone", { "positionClass": "toast-bottom-right", "progressBar": true });
            }
            else {
                $("#AreaTask_MilestoneID").html('');
                $("#AreaTask_MilestoneID").val('');
                $("#AreaTask_MilestoneID").append('<option value="">--Select Milestone--</option>');
                for (var i = 0; i < data.length; i++) {
                    $("#AreaTask_MilestoneID").append('<option value="' + data[i].MilestoneID + '">' + data[i].MilestoneTitle + '</option>');
                }
                $("#AreaTask_MilestoneID").selectpicker('refresh');
            }
        }
    });
}

function GetMilestoneWiseMemberListForTask(obj) {
    $("#Tasks_AssignTo").empty();
    //$("#Milestones_AssignTo").append('<option value="">--Select Member--</option>');
    $("#Tasks_AssignTo").val("");
    $("#Tasks_AssignTo").selectpicker('refresh');
    var MilestoneID = $(obj).find("option:selected").val().toLowerCase();
    var hiddenAssignmentID = $("#hiddenAssignmentID").val();
    var postData = {
        varMilestoneID: MilestoneID,
        varAssignmentID: hiddenAssignmentID
    };
    ShowLoader();
    AjaxCall("/Pms/ProjectView/GetMilestoneWiseMemberListForTask", JSON.stringify(postData), "post", "json", "application/json", false).done(function (data) {
        HideLoader();
        if (data == "_Logon_") {
            window.location.href = "/Login/Login";
        }
        else {
            if (data == 'error') {
                toastr["error"]("Oops Something Wentwrong !!!", "Milestone", { "positionClass": "toast-bottom-right", "progressBar": true });
            }
            else {
                $("#Tasks_AssignTo").html('');
                $("#Tasks_AssignTo").val('');
                //$("#Tasks_AssignTo").append('<option value="">--Select Member--</option>');
                for (var i = 0; i < data.length; i++) {
                    $("#Tasks_AssignTo").append('<option value="' + data[i].EmployeeID + '">' + data[i].DisplayName + '</option>');
                }
                $("#Tasks_AssignTo").selectpicker('refresh');
            }
        }
    });
}

function GetMilestoneWiseTaskListForTimesheet(obj) {
    $("#Timesheet_TaskID").empty();
    $("#Timesheet_TaskID").append('<option value="">--Select Task--</option>');
    $("#Timesheet_TaskID").val("");
    $("#Timesheet_TaskID").selectpicker('refresh');
    var MilestoneID = $(obj).find("option:selected").val().toLowerCase();
    var AreaID = $("#Timesheet_AreaID").find("option:selected").val().toLowerCase();
    var hiddenAssignmentID = $("#hiddenAssignmentID").val();
    var postData = {
        varMilestoneID: MilestoneID,
        varAreaID: AreaID,
        varAssignmentID: hiddenAssignmentID
    };
    ShowLoader();
    AjaxCall("/Pms/ProjectView/GetMilestoneWiseTaskListForTimesheet", JSON.stringify(postData), "post", "json", "application/json", false).done(function (data) {
        HideLoader();
        if (data == "_Logon_") {
            window.location.href = "/Login/Login";
        }
        else {
            if (data == 'error') {
                toastr["error"]("Oops Something Wentwrong !!!", "Milestone", { "positionClass": "toast-bottom-right", "progressBar": true });
            }
            else {
                $("#Timesheet_TaskID").html('');
                $("#Timesheet_TaskID").val('');
                $("#Timesheet_TaskID").append('<option value="">--Select Task--</option>');
                for (var i = 0; i < data.length; i++) {
                    $("#Timesheet_TaskID").append('<option value="' + data[i].TaskID + '">' + data[i].TaskTitle + '</option>');
                }
                $("#Timesheet_TaskID").selectpicker('refresh');
            }
        }
    });
}

function GetMilestoneWiseTaskListForFile(obj) {
    $("#File_TaskID").empty();
    $("#File_TaskID").append('<option value="">--Select Task--</option>');
    $("#File_TaskID").val("");
    $("#File_TaskID").selectpicker('refresh');
    var MilestoneID = $(obj).find("option:selected").val().toLowerCase();
    var AreaID = $("#File_AreaID").find("option:selected").val().toLowerCase();
    var hiddenAssignmentID = $("#hiddenAssignmentID").val();
    var postData = {
        varMilestoneID: MilestoneID,
        varAreaID: AreaID,
        varAssignmentID: hiddenAssignmentID
    };
    ShowLoader();
    AjaxCall("/Pms/ProjectView/GetMilestoneWiseTaskListForTimesheet", JSON.stringify(postData), "post", "json", "application/json", false).done(function (data) {
        HideLoader();
        if (data == "_Logon_") {
            window.location.href = "/Login/Login";
        }
        else {
            if (data == 'error') {
                toastr["error"]("Oops Something Wentwrong !!!", "Milestone", { "positionClass": "toast-bottom-right", "progressBar": true });
            }
            else {
                $("#File_TaskID").html('');
                $("#File_TaskID").val('');
                $("#File_TaskID").append('<option value="">--Select Task--</option>');
                for (var i = 0; i < data.length; i++) {
                    $("#File_TaskID").append('<option value="' + data[i].TaskID + '">' + data[i].TaskTitle + '</option>');
                }
                $("#File_TaskID").selectpicker('refresh');
            }
        }
    });
}

function GetAreaWiseMilestoneAndTaskListForTimesheet(obj) {
    $("#Timesheet_MilestoneID").empty();
    $("#Timesheet_MilestoneID").append('<option value="">--Select Milestone--</option>');
    $("#Timesheet_MilestoneID").val("");
    $("#Timesheet_MilestoneID").selectpicker('refresh');
    $("#Timesheet_TaskID").empty();
    $("#Timesheet_TaskID").append('<option value="">--Select Task--</option>');
    $("#Timesheet_TaskID").val("");
    $("#Timesheet_TaskID").selectpicker('refresh');
    var AreaID = $(obj).find("option:selected").val().toLowerCase();
    var hiddenAssignmentID = $("#hiddenAssignmentID").val();
    var postData = {
        varAreaID: AreaID,
        varAssignmentID: hiddenAssignmentID
    };
    ShowLoader();
    $.ajax({
        url: "/Pms/ProjectView/GetAreaWiseMilestoneAndTaskListForTimesheet",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(postData),
        success: function (response) {
            HideLoader();
            if (response == "_Logon_") {
                window.location.href = "/Login/Login";
            }
            else {
                var xmlDoc = $.parseXML(response.data);
                var xml = $(xmlDoc);

                var locations = xml.find("Table");
                $("#Timesheet_MilestoneID").html('');
                $("#Timesheet_MilestoneID").val('');
                $("#Timesheet_MilestoneID").append('<option value="">--Select Milestone--</option>');
                $(locations).each(function () {
                    $("#Timesheet_MilestoneID").append('<option value="' + $(this).find("MilestoneID").text() + '">' + $(this).find("MilestoneTitle").text() + '</option>');
                });
                $('#Timesheet_MilestoneID').selectpicker('refresh');

                var level = xml.find("Table1");
                $("#Timesheet_TaskID").html('');
                $("#Timesheet_TaskID").val('');
                $("#Timesheet_TaskID").append('<option value="">--Select Task--</option>');
                $(level).each(function () {
                    $("#Timesheet_TaskID").append('<option value="' + $(this).find("TaskID").text() + '">' + $(this).find("TaskTitle").text() + '</option>');
                });
                $('#Timesheet_TaskID').selectpicker('refresh');
            }
        }
    })
}

function GetAreaWiseMilestoneListForFile(obj) {
    $("#File_MilestoneID").empty();
    $("#File_MilestoneID").append('<option value="">--Select Milestone--</option>');
    $("#File_MilestoneID").val("");
    $("#File_MilestoneID").selectpicker('refresh');
    $("#File_TaskID").empty();
    $("#File_TaskID").append('<option value="">--Select Task--</option>');
    $("#File_TaskID").val("");
    $("#File_TaskID").selectpicker('refresh');
    var AreaID = $(obj).find("option:selected").val().toLowerCase();
    var hiddenAssignmentID = $("#hiddenAssignmentID").val();
    var postData = {
        varAreaID: AreaID,
        varAssignmentID: hiddenAssignmentID
    };
    ShowLoader();
    $.ajax({
        url: "/Pms/ProjectView/GetAreaWiseMilestoneAndTaskListForTimesheet",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(postData),
        success: function (response) {
            HideLoader();
            if (response == "_Logon_") {
                window.location.href = "/Login/Login";
            }
            else {
                var xmlDoc = $.parseXML(response.data);
                var xml = $(xmlDoc);

                var locations = xml.find("Table");
                $("#File_MilestoneID").html('');
                $("#File_MilestoneID").val('');
                $("#File_MilestoneID").append('<option value="">--Select Milestone--</option>');
                $(locations).each(function () {
                    $("#File_MilestoneID").append('<option value="' + $(this).find("MilestoneID").text() + '">' + $(this).find("MilestoneTitle").text() + '</option>');
                });
                $('#File_MilestoneID').selectpicker('refresh');

                var level = xml.find("Table1");
                $("#File_TaskID").html('');
                $("#File_TaskID").val('');
                $("#File_TaskID").append('<option value="">--Select Task--</option>');
                $(level).each(function () {
                    $("#File_TaskID").append('<option value="' + $(this).find("TaskID").text() + '">' + $(this).find("TaskTitle").text() + '</option>');
                });
                $('#File_TaskID').selectpicker('refresh');
            }
        }
    })
}

function GetAreaWiseMilestoneListForTaskListFilter(obj) {
    $("#TaskList_Filter_MilestoneID").empty();
    $("#TaskList_Filter_MilestoneID").append('<option value="">--Select Milestone--</option>');
    $("#TaskList_Filter_MilestoneID").val("");
    $("#TaskList_Filter_MilestoneID").selectpicker('refresh');
    var AreaID = $(obj).find("option:selected").val().toLowerCase();
    var SelectedAreaText = $("#TaskList_Filter_AreaID option:selected").text();

    var hiddenAssignmentID = $("#hiddenAssignmentID").val();
    var postData = {
        varAreaID: AreaID,
        varAssignmentID: hiddenAssignmentID
    };
    ShowLoader();
    $.ajax({
        url: "/Pms/ProjectView/GetAreaWiseMilestoneAndTaskListForTask",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(postData),
        success: function (response) {
            HideLoader();
            if (response == "_Logon_") {
                window.location.href = "/Login/Login";
            }
            else {
                var xmlDoc = $.parseXML(response.data);
                var xml = $(xmlDoc);

                var locations = xml.find("Table");
                $("#TaskList_Filter_MilestoneID").html('');
                $("#TaskList_Filter_MilestoneID").val('');
                $("#TaskList_Filter_MilestoneID").append('<option value="">--Select Milestone--</option>');
                $(locations).each(function () {
                    $("#TaskList_Filter_MilestoneID").append('<option value="' + $(this).find("MilestoneID").text() + '">' + $(this).find("MilestoneTitle").text() + '</option>');
                });
                $('#TaskList_Filter_MilestoneID').selectpicker('refresh');

                if (SelectedAreaText == "All") {
                    AreaFilterNameForMilestoneList = SelectedAreaText;
                } else {
                    AreaFilterNameForMilestoneList = AreaID.toString().toUpperCase();
                }
                SearchTaskListByFilter();
            }
        }
    })
}

function GetMilestoneWiseMemberListAndTaskListForSubTask(obj) {
    $("#SubTasks_RefTaskID").empty();
    //$("#Milestones_AssignTo").append('<option value="">--Select Member--</option>');
    $("#SubTasks_RefTaskID").val("");
    $("#SubTasks_RefTaskID").selectpicker('refresh');
    $("#SubTasks_AssignTo").empty();
    $("#SubTasks_AssignTo").val("");
    $("#SubTasks_AssignTo").selectpicker('refresh');
    var MilestoneID = $(obj).find("option:selected").val().toLowerCase();
    var hiddenAssignmentID = $("#hiddenAssignmentID").val();
    ShowLoader();
    $.ajax({
        url: "/Pms/ProjectView/GetMilestoneWiseMemberListAndTaskListForSubTask",
        type: "POST",
        data: { varMilestoneID: MilestoneID, varAssignmentID: hiddenAssignmentID },
        success: function (response) {
            HideLoader();
            if (response == "_Logon_") {
                window.location.href = "/Login/Login";
            }
            else {
                var xmlDoc = $.parseXML(response.data);
                var xml = $(xmlDoc);

                var locations = xml.find("Table");
                $("#SubTasks_RefTaskID").html('');
                $("#SubTasks_RefTaskID").val('');
                $("#SubTasks_RefTaskID").append('<option value="">--Select Task--</option>');
                $(locations).each(function () {
                    $("#SubTasks_RefTaskID").append('<option value="' + $(this).find("TaskID").text() + '">' + $(this).find("TaskTitle").text() + '</option>');
                });
                $('#SubTasks_RefTaskID').selectpicker('refresh');

                //var level = xml.find("Table1");
                //$("#SubTasks_AssignTo").html('');
                //$("#SubTasks_AssignTo").val('');
                //$("#SubTasks_AssignTo").append('<option value="">--Select Member--</option>');
                //$(level).each(function () {
                //    $("#SubTasks_AssignTo").append('<option value="' + $(this).find("EmployeeID").text() + '">' + $(this).find("DisplayName").text() + '</option>');
                //});
                //$('#SubTasks_AssignTo').selectpicker('refresh');
            }
        }
    })
}

function GetTaskWiseMemberListForSubTask(obj) {
    $("#SubTasks_AssignTo").empty();
    $("#SubTasks_AssignTo").val("");
    $("#SubTasks_AssignTo").selectpicker('refresh');
    var TaskID = $(obj).find("option:selected").val().toLowerCase();
    var hiddenAssignmentID = $("#hiddenAssignmentID").val();
    ShowLoader();
    $.ajax({
        url: "/Pms/ProjectView/GetTaskWiseMemberListForSubTask",
        type: "POST",
        data: { varTaskID: TaskID, varAssignmentID: hiddenAssignmentID },
        success: function (response) {
            HideLoader();
            if (response == "_Logon_") {
                window.location.href = "/Login/Login";
            }
            else {
                var xmlDoc = $.parseXML(response.data);
                var xml = $(xmlDoc);

                var level = xml.find("Table");
                $("#SubTasks_AssignTo").html('');
                $("#SubTasks_AssignTo").val('');
                //$("#SubTasks_AssignTo").append('<option value="">--Select Member--</option>');
                $(level).each(function () {
                    $("#SubTasks_AssignTo").append('<option value="' + $(this).find("EmployeeID").text() + '">' + $(this).find("DisplayName").text() + '</option>');
                });
                $('#SubTasks_AssignTo').selectpicker('refresh');
            }
        }
    })
}

function GetMilestoneWiseMemberListForTaskFromMilestone(obj) {
    $("#MilestoneTask_AssignTo").empty();
    //$("#Milestones_AssignTo").append('<option value="">--Select Member--</option>');
    $("#MilestoneTask_AssignTo").val("");
    $("#MilestoneTask_AssignTo").selectpicker('refresh');
    var MilestoneID = $(obj).find("option:selected").val().toLowerCase();
    var hiddenAssignmentID = $("#hiddenAssignmentID").val();
    var postData = {
        varMilestoneID: MilestoneID,
        varAssignmentID: hiddenAssignmentID
    };
    ShowLoader();
    AjaxCall("/Pms/ProjectView/GetMilestoneWiseMemberListForTask", JSON.stringify(postData), "post", "json", "application/json", false).done(function (data) {
        HideLoader();
        if (data == "_Logon_") {
            window.location.href = "/Login/Login";
        }
        else {
            if (data == 'error') {
                toastr["error"]("Oops Something Wentwrong !!!", "Milestone", { "positionClass": "toast-bottom-right", "progressBar": true });
            }
            else {
                $("#MilestoneTask_AssignTo").html('');
                $("#MilestoneTask_AssignTo").val('');
                //$("#MilestoneTask_AssignTo").append('<option value="">--Select Member--</option>');
                for (var i = 0; i < data.length; i++) {
                    $("#MilestoneTask_AssignTo").append('<option value="' + data[i].EmployeeID + '">' + data[i].DisplayName + '</option>');
                }
                $("#MilestoneTask_AssignTo").selectpicker('refresh');
            }
        }
    });
}

function GetMilestoneWiseMemberListForTaskFromAreaTask(obj) {
    $("#AreaTask_AssignTo").empty();
    //$("#Milestones_AssignTo").append('<option value="">--Select Member--</option>');
    $("#AreaTask_AssignTo").val("");
    $("#AreaTask_AssignTo").selectpicker('refresh');
    var MilestoneID = $(obj).find("option:selected").val().toLowerCase();
    var hiddenAssignmentID = $("#hiddenAssignmentID").val();
    var postData = {
        varMilestoneID: MilestoneID,
        varAssignmentID: hiddenAssignmentID
    };
    ShowLoader();
    AjaxCall("/Pms/ProjectView/GetMilestoneWiseMemberListForTask", JSON.stringify(postData), "post", "json", "application/json", false).done(function (data) {
        HideLoader();
        if (data == "_Logon_") {
            window.location.href = "/Login/Login";
        }
        else {
            if (data == 'error') {
                toastr["error"]("Oops Something Wentwrong !!!", "Milestone", { "positionClass": "toast-bottom-right", "progressBar": true });
            }
            else {
                $("#AreaTask_AssignTo").html('');
                $("#AreaTask_AssignTo").val('');
                //$("#AreaTask_AssignTo").append('<option value="">--Select Member--</option>');
                for (var i = 0; i < data.length; i++) {
                    $("#AreaTask_AssignTo").append('<option value="' + data[i].EmployeeID + '">' + data[i].DisplayName + '</option>');
                }
                $("#AreaTask_AssignTo").selectpicker('refresh');
            }
        }
    });
}

function ClearPopup(form) {
    $("#" + form)[0].reset();
    $(".field-validation-valid").find('span').html('');
    $('#' + form + ' input[type=text]').val("");
    $('#' + form + ' input[type=hidden]').val("");
    $('#' + form + ' .bs-select').val("");
    $('#' + form + ' .bs-select').selectpicker('refresh');
}

function AddNewDiscussion(form) {
    ClearPopup(form);
    var hiddenAssignmentID = $("#hiddenAssignmentID").val();
    $("#AssignmentIDForDiscussion").val(hiddenAssignmentID);
    $("#DiscussionIsEdit").val("false");
    $("#AddNewDiscussionForm").modal('show');
}

function fnEditViewTask() {
    if ($("#form_LoadTaskData").valid()) {
        var AreaID = AreaFilterNameForMilestoneList;
        var TaskDetail = $("#EditTask_TaskDetail").val();
        var Comment = $("#EditTask_Comment").val();
        //var Comment = "";
        //var StartDate = $("#EditTask_StartDate").val();
        //var EndDate = $("#EditTask_EndDate").val();
        //var ActualStartDate = $("#EditTask_ActualStartDate").val();
        //var ActualEndDate = $("#EditTask_ActualEndDate").val();
        var AssignTo = $.trim($("#EditTask_AssignTo").val());
        var StartTime = $.trim($("#EditTask_StartTime").val());
        var EndTime = $.trim($("#EditTask_EndTime").val());
        var AssignmentID = $("#AssignmentIDForTaskEditView").val();
        var IsEdit = $("#IsViewtaskEdit").val();
        var TaskID = $("#hiddenTaskViewID").val();
        var Priority = $("#PrioritySelectedTerm").text();
        var Status = $("#StatusSelectedTerm").text();
        var FromWhichView = $("#IsFromWhichView").val();
        var MainTaskIDForSubTask = $("#MainTaskIDForSubTask").val();
        var MainTaskTitleForSubTask = $("#MainTaskTitleForSubTask").val();

        //data: { TaskDetail: TaskDetail, Comment: JSON.stringify(Comment), AssignTo: AssignTo, StartDate: StartDate, EndDate: EndDate, ActualStartDate: ActualStartDate, ActualEndDate: ActualEndDate, AssignmentIDStr: AssignmentID, IsEditStr: IsEdit, TaskIDStr: TaskID, Status: Status, Priority: Priority, FromWhichView: FromWhichView },

        var postData = {
            TaskDetail: $("#EditTask_TaskDetail").val(),
            Comment: $("#EditTask_Comment").val(),
            //StartDate: $("#EditTask_StartDate").val(),
            //EndDate: $("#EditTask_EndDate").val(),
            //ActualStartDate: $("#EditTask_ActualStartDate").val(),
            //ActualEndDate: $("#EditTask_ActualEndDate").val(),
            AssignTo: $.trim($("#EditTask_AssignTo").val()),
            StartTime: $.trim($("#EditTask_StartTime").val()),
            EndTime: $.trim($("#EditTask_EndTime").val()),
            AssignmentIDStr: $("#AssignmentIDForTaskEditView").val(),
            IsEditStr: $("#IsViewtaskEdit").val(),
            TaskIDStr: $("#hiddenTaskViewID").val(),
            Priority: $("#PrioritySelectedTerm").text(),
            Status: $("#StatusSelectedTerm").text(),
            FromWhichView: $("#IsFromWhichView").val(),
            AreaIDStr: AreaID
        };
        ShowLoader();
        $.ajax({
            url: "/Pms/ProjectView/SaveTaskEditViewDetail",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(postData),
            success: function (response) {
                HideLoader();
                if (response == "_Logon_") {
                    window.location.href = "/Login/Login";
                }
                else {
                    if (FromWhichView == "TaskList") {
                        $("#Edittask").modal('hide');
                        $("#TaskListByProjectID").html(response);
                        var table = $('#dt_task').DataTable({});
                        $('[data-toggle="tooltip"]').tooltip();
                    } else if (FromWhichView == "SubTaskList") {
                        $("#Edittask").modal('hide');
                        $("#TaskListByProjectID").html(response);
                        var table = $('#dt_task').DataTable({});
                        OpenSubTaskListModel(MainTaskIDForSubTask, AssignmentID, MainTaskTitleForSubTask);
                    } else {
                        $("#Edittask").modal('hide');
                        $("#MileastoneListByProjectID").html(response);
                        $('[data-toggle="tooltip"]').tooltip();
                        $('.scroller').slimScroll({
                            allowPageScroll: true, // allow page scroll when the element scroll is ended
                            size: '7px',
                            color: ($(this).attr("data-handle-color") ? $(this).attr("data-handle-color") : '#bbb'),
                            wrapperClass: ($(this).attr("data-wrapper-class") ? $(this).attr("data-wrapper-class") : 'slimScrollDiv'),
                            railColor: ($(this).attr("data-rail-color") ? $(this).attr("data-rail-color") : '#eaeaea'),
                            position: 'right',
                            height: '345px',
                            alwaysVisible: ($(this).attr("data-always-visible") == "1" ? true : false),
                            railVisible: ($(this).attr("data-rail-visible") == "1" ? true : false),
                            disableFadeOut: true
                        });
                        if (AreaID == "All") {
                            AreaFilterNameForMilestoneList = AreaID;
                        } else {
                            AreaFilterNameForMilestoneList = AreaID.toString().toUpperCase();
                        }
                        $("#MilestoneList_Filter_AreaID option[value='" + AreaFilterNameForMilestoneList + "']").attr('selected', 'selected');
                        $('.bs-select').selectpicker('refresh');
                    }
                }
            }
        })
    }
}

function fnEditViewAreaTask() {
    if ($("#form_LoadAreaTaskData").valid()) {
        var TaskDetail = $("#EditAreaTask_TaskDetail").val();
        var Comment = $("#EditAreaTask_Comment").val();
        //var StartDate = $("#EditAreaTask_StartDate").val();
        //var EndDate = $("#EditAreaTask_EndDate").val();
        //var ActualStartDate = $("#EditAreaTask_ActualStartDate").val();
        //var ActualEndDate = $("#EditAreaTask_ActualEndDate").val();
        var AssignTo = $.trim($("#EditAreaTask_AssignTo").val());
        var StartTime = $.trim($("#EditAreaTask_StartTime").val());
        var EndTime = $.trim($("#EditAreaTask_EndTime").val());
        var AssignmentID = $("#AssignmentIDForAreaTaskEditView").val();
        var IsEdit = $("#IsViewAreataskEdit").val();
        var TaskID = $("#hiddenAreaTaskViewID").val();
        var Priority = $("#AreaPrioritySelectedTerm").text();
        var Status = $("#StatusSelectedTermArea").text();
        var FromWhichView = $("#IsFromWhichView").val();
        var AssignmentAreaID = $("#AssignmentAreaIDForAreaTask").val();
        var AreaTitleForTask = $("#AreaTitleForTask").val();

        var postData = {
            TaskDetail: $("#EditAreaTask_TaskDetail").val(),
            Comment: $("#EditAreaTask_Comment").val(),
            //StartDate: $("#EditAreaTask_StartDate").val(),
            //EndDate: $("#EditAreaTask_EndDate").val(),
            //ActualStartDate: $("#EditAreaTask_ActualStartDate").val(),
            //ActualEndDate: $("#EditAreaTask_ActualEndDate").val(),
            AssignTo: $.trim($("#EditAreaTask_AssignTo").val()),
            StartTime: $.trim($("#EditAreaTask_StartTime").val()),
            EndTime: $.trim($("#EditAreaTask_EndTime").val()),
            AssignmentIDStr: $("#AssignmentIDForAreaTaskEditView").val(),
            IsEditStr: $("#IsViewAreataskEdit").val(),
            TaskIDStr: $("#hiddenAreaTaskViewID").val(),
            Priority: $("#AreaPrioritySelectedTerm").text(),
            Status: $("#StatusSelectedTermArea").text(),
            FromWhichView: $("#IsFromWhichView").val(),
            AreaIDStr: ""
        };
        ShowLoader();
        $.ajax({
            url: "/Pms/ProjectView/SaveTaskEditViewDetail",
            type: "POST",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify(postData),
            success: function (response) {
                HideLoader();
                if (response == "_Logon_") {
                    window.location.href = "/Login/Login";
                }
                else {
                    if (FromWhichView == "AreaTaskList") {
                        $("#EditAreaTask").modal('hide');
                        OpenAreaTaskListModel(AssignmentAreaID, AssignmentID, AreaTitleForTask);
                    }
                }
            }
        })
    }
}

function fnAddNewTask() {

    if ($("#form_TaskDetails").valid()) {
        var AreaID = AreaFilterNameForMilestoneList;
        $("#FilteredAreaValForTask").val(AreaID);
        //if ($.trim($("#Tasks_ActualEndDate").val()) != "" && $.trim($("#IsAllowToCompleteTask").val()) != '' && $("#IsAllowToCompleteTask").val().toString().toUpperCase() == "FALSE") {
        //    toastr["error"]("Please fill timesheet for this task.", "Task", { "positionClass": "toast-bottom-right", "progressBar": true });
        //}
        //else {
        if (!($("#VALmassage_0").text() == "File size exceeds 5 MB!" || $("#VALmassage_1").text() == "File size exceeds 5 MB!" || $("#VALmassage_2").text() == "File size exceeds 5 MB!" || $("#VALmassage_3").text() == "File size exceeds 5 MB!" || $("#VALmassage_4").text() == "File size exceeds 5 MB!")) {

            if ($("#form_TaskDetails").valid()) {

                var table = $("#dt_taskFile tbody");

                table.find('tr').each(function (i) {

                    $(this).find("input[type='hidden']:eq(0)").attr("id", "projectdocumentlist_" + i + "__AttachementID");
                    $(this).find("input[type='hidden']:eq(0)").attr("name", "projectdocumentlist[" + i + "].AttachementID");

                    $(this).find("input[type='hidden']:eq(1)").attr("id", "projectdocumentlist_" + i + "__DocumentID");
                    $(this).find("input[type='hidden']:eq(1)").attr("name", "projectdocumentlist[" + i + "].DocumentID");

                    $(this).find("input[type='hidden']:eq(2)").attr("id", "projectdocumentlist_" + i + "__Term");
                    $(this).find("input[type='hidden']:eq(2)").attr("name", "projectdocumentlist[" + i + "].Term");

                    $(this).find("input[type='hidden']:eq(3)").attr("id", "projectdocumentlist_" + i + "__ProjectTermID");
                    $(this).find("input[type='hidden']:eq(3)").attr("name", "projectdocumentlist[" + i + "].ProjectTermID");

                    $(this).find("input[type='hidden']:eq(4)").attr("id", "projectdocumentlist_" + i + "__FilePath");
                    $(this).find("input[type='hidden']:eq(4)").attr("name", "projectdocumentlist[" + i + "].FilePath");

                    $(this).find("input[type='hidden']:eq(5)").attr("id", "projectdocumentlist_" + i + "__FileName");
                    $(this).find("input[type='hidden']:eq(5)").attr("name", "projectdocumentlist[" + i + "].FileName");

                    $(this).find("input[type='hidden']:eq(6)").attr("id", "projectdocumentlist_" + i + "__FileSize");
                    $(this).find("input[type='hidden']:eq(6)").attr("name", "projectdocumentlist[" + i + "].FileSize");

                    $(this).find("input[type='hidden']:eq(7)").attr("id", "projectdocumentlist_" + i + "__ExtensionType_Term");
                    $(this).find("input[type='hidden']:eq(7)").attr("name", "projectdocumentlist[" + i + "].ExtensionType_Term");

                    $(this).find("input[type='hidden']:eq(8)").attr("id", "projectdocumentlist_" + i + "__DisplayName");
                    $(this).find("input[type='hidden']:eq(8)").attr("name", "projectdocumentlist[" + i + "].DisplayName");

                    $(this).find("input[type='hidden']:eq(9)").attr("id", "projectdocumentlist_" + i + "__CustomTerm");
                    $(this).find("input[type='hidden']:eq(9)").attr("name", "projectdocumentlist[" + i + "].CustomTerm");

                    $(this).find('td:eq(0)').find('select').attr("id", "projectdocumentlist_" + i + "__DocType_Term");
                    $(this).find('td:eq(0)').find('select').attr("name", "projectdocumentlist[" + i + "].DocType_Term");

                    $(this).find('td:eq(0)').find('input').attr("id", "projectdocumentlist_" + i + "__OtherDocType");
                    $(this).find('td:eq(0)').find('input').attr("name", "projectdocumentlist[" + i + "].OtherDocType");

                    $(this).find('td:eq(1)').find('select').attr("id", "projectdocumentlist_" + i + "__Status_Term");
                    $(this).find('td:eq(1)').find('select').attr("name", "projectdocumentlist[" + i + "].Status_Term");

                    $(this).find('td:eq(2)').find('input[class="isnew"]').attr("id", "projectdocumentlist_" + i + "__IsNew");
                    $(this).find('td:eq(2)').find('input[class="isnew"]').attr("name", "projectdocumentlist[" + i + "].IsNew");

                    $(this).find('td:eq(3)').find('input[versionno]').attr("id", "projectdocumentlist_" + i + "__VersionNo");
                    $(this).find('td:eq(3)').find('input[versionno]').attr("name", "projectdocumentlist[" + i + "].VersionNo");


                    $(this).find('td:eq(0)').find('select').attr("id", "projectdocumentlist_" + i + "__DocType_Term").removeAttr("disabled");

                    $(this).find('td:eq(3)').find('input[versionno]').attr("id", "projectdocumentlist_" + i + "__VersionNo").removeAttr("disabled");

                });

                $(".bs-select").selectpicker('refresh');

                table.find('tr').each(function (i) {
                    $(this).find('td:eq(0)').find('input[otherdoctype]').attr("id", "projectdocumentlist_" + i + "__OtherDocType").removeAttr("disabled");
                    var rowCount = 1;
                    rowCount = rowCount + i;
                    $("#hiddenRowCount").val(rowCount);
                })

                $("#Tasks_StartDate").removeAttr('disabled');
                $("#Tasks_EndDate").removeAttr('disabled');
                var form = $('#form_TaskDetails')[0];
                var data = new FormData(form);
                ShowLoader();
                $.ajax({
                    type: "POST",
                    enctype: 'multipart/form-data',
                    url: "/Pms/ProjectView/SaveTaskDetail",
                    data: data,
                    processData: false,
                    contentType: false,
                    cache: false,
                    timeout: 600000,
                    success: function (response) {
                        HideLoader();
                        if (response == "_Logon_") {
                            window.location.href = "/Login/Login";
                        } else if (response.IsSuccess && response.Data == "NotAllowToComplete") {
                            if (response.Message == "Please fill timesheet for this task.") {
                                toastr["error"]("Please fill timesheet for this task.", "Task", { "positionClass": "toast-bottom-right", "progressBar": true });
                            } else {
                                toastr["error"]("You can not complete this task as subtask are open.", "Task", { "positionClass": "toast-bottom-right", "progressBar": true });
                            }
                        }
                        else {
                            $("#NewTaskForm").modal('hide');
                            $("#TaskListByProjectID").html(response);
                            var table = $('#dt_task').DataTable({});
                            $('[data-toggle="tooltip"]').tooltip();
                        }
                    },
                    error: function (e) {
                        $("#result").text(e.responseText);
                        console.log("ERROR : ", e);
                        $("#btnSubmit").prop("disabled", false);

                    }
                });
            }
        }
        //}
    }
}

function fnAddNewSubTask() {
    if ($("#form_SubTaskDetails").valid()) {
        $("#SubTasks_StartDate").removeAttr('disabled');
        $("#SubTasks_EndDate").removeAttr('disabled');
        var TaskTitle = $("#SubTasks_TaskTitle").val();
        var MilestoneID = $("#SubTasks_MilestoneID").val();
        var RefTaskID = $.trim($("#SubTasks_RefTaskID").val());
        var TotalMenDays = $("#SubTasks_TotalMenDays").val();
        var AssignTo = $.trim($("#SubTasks_AssignTo").val());
        var StartDate = $("#SubTasks_StartDate").val();
        var EndDate = $("#SubTasks_EndDate").val();
        var ActualStartDate = $("#SubTasks_ActualStartDate").val();
        var ActualEndDate = $("#SubTasks_ActualEndDate").val();
        var Status = $("#SubTasks_Status_Term").val();
        var Priority = $("#SubTasks_Priority_Term").val();
        var TaskDetail = $("#SubTasks_TaskDetail").val();
        var AssignmentID = $("#AssignmentIDForSubTask").val();
        var IsEdit = $("#IsEditForSubTask").val();
        var SubTaskID = $("#hiddenSubTaskID").val();
        var hiddenRefTaskID = $("#hiddenRefTaskID").val();
        var hiddentitle = $("#hiddentitleinedit").val();

        ShowLoader();
        $.ajax({
            url: "/Pms/ProjectView/SaveSubTaskDetail",
            type: "POST",
            data: { TaskTitle: TaskTitle, MilestoneID: MilestoneID, RefTaskID: RefTaskID, AssignTo: AssignTo, TotalMenDays: TotalMenDays, StartDate: StartDate, EndDate: EndDate, ActualStartDate: ActualStartDate, ActualEndDate: ActualEndDate, Status: Status, Priority: Priority, TaskDetail: TaskDetail, AssignmentID: AssignmentID, IsEditStr: IsEdit, SubTaskIDStr: SubTaskID, AreaIDStr: AreaFilterNameForMilestoneList },
            success: function (response) {
                HideLoader();
                if (response == "_Logon_") {
                    window.location.href = "/Login/Login";
                }
                else {
                    if (IsEdit == "true") {
                        $("#NewSubTaskForm").modal('hide');
                        OpenSubTaskListModel(hiddenRefTaskID, AssignmentID, hiddentitle);
                    } else {
                        $("#NewSubTaskForm").modal('hide');
                        $("#TaskListByProjectID").html(response);
                        var table = $('#dt_task').DataTable({});
                        $('[data-toggle="tooltip"]').tooltip();
                    }
                }
            }
        })
    }
}

function fnAddNewAreaTask() {
    if ($("#form_AreaTaskDetails").valid()) {
        $("#AreaTask_StartDate").removeAttr('disabled');
        $("#AreaTask_EndDate").removeAttr('disabled');
        var TaskTitle = $("#AreaTask_TaskTitle").val();
        var AssignmentAreaID = $("#AreaTask_AreaID").val();
        var MilestoneID = $("#AreaTask_MilestoneID").val();
        var AssignTo = $.trim($("#AreaTask_AssignTo").val());
        var TotalMenDays = $("#AreaTask_TotalMenDays").val();
        var StartDate = $("#AreaTask_StartDate").val();
        var EndDate = $("#AreaTask_EndDate").val();
        var ActualStartDate = $("#AreaTask_ActualStartDate").val();
        var ActualEndDate = $("#AreaTask_ActualEndDate").val();
        var Status = $("#AreaTask_Status_Term").val();
        var Priority = $("#AreaTask_Priority_Term").val();
        var TaskDetail = $("#AreaTask_TaskDetail").val();
        var AssignmentID = $("#AssignmentIDForAreaTask").val();
        var IsEdit = $("#IsEditForAreaTask").val();
        var AreaTaskID = $("#hiddenAreaTaskID").val();
        var hiddenAssignmentAreaID = $("#hiddenAssignmentAreaID").val();
        var hiddenAreatitle = $("#hiddenAreatitleinedit").val();

        ShowLoader();
        $.ajax({
            url: "/Pms/ProjectView/SaveAreaTaskDetail",
            type: "POST",
            data: { TaskTitle: TaskTitle, AssignmentAreaID: AssignmentAreaID, MilestoneIDStr: MilestoneID, AssignTo: AssignTo, TotalMenDays: TotalMenDays, StartDate: StartDate, EndDate: EndDate, ActualStartDate: ActualStartDate, ActualEndDate: ActualEndDate, Status: Status, Priority: Priority, TaskDetail: TaskDetail, AssignmentID: AssignmentID, IsEditStr: IsEdit, AreaTaskIDStr: AreaTaskID },
            success: function (response) {
                HideLoader();
                if (response == "_Logon_") {
                    window.location.href = "/Login/Login";
                }
                else if (response.IsSuccess && response.Data == "NotAllowToComplete") {
                    if (response.Message == "Please fill timesheet for this task.") {
                        toastr["error"]("Please fill timesheet for this task.", "Area", { "positionClass": "toast-bottom-right", "progressBar": true });
                    } else {
                        toastr["error"]("You can not complete this task as subtask are open.", "Area", { "positionClass": "toast-bottom-right", "progressBar": true });
                    }
                }
                else {
                    if (IsEdit == "true") {
                        $("#NewAreaTaskForm").modal('hide');
                        OpenAreaTaskListModel(hiddenAssignmentAreaID, AssignmentID, hiddenAreatitle);
                    } else {
                        $("#NewAreaTaskForm").modal('hide');
                        $("#AreaListByProjectID").html(response);
                        var table = $('#dt_Area').DataTable({});
                        $('[data-toggle="tooltip"]').tooltip();
                    }
                }
            }
        })
    }
}

function CloseSubTaskEditPopUp() {
    var IsEdit = $("#IsEditForSubTask").val();
    var hiddenRefTaskID = $("#hiddenRefTaskID").val();
    var hiddentitle = $("#hiddentitleinedit").val();
    var AssignmentID = $("#AssignmentIDForSubTask").val();
    if (IsEdit == "true") {
        $("#NewSubTaskForm").modal('hide');
        OpenSubTaskListModel(hiddenRefTaskID, AssignmentID, hiddentitle);
    } else {
        $("#NewSubTaskForm").modal('hide');
    }
}

function CloseEditViewTaskPopUp() {
    var IsFromWhichView = $("#IsFromWhichView").val();
    var MainTaskIDForSubTask = $("#MainTaskIDForSubTask").val();
    var MainTaskTitleForSubTask = $("#MainTaskTitleForSubTask").val();
    var AssignmentID = $("#AssignmentIDForTaskEditView").val();

    if (IsFromWhichView == "SubTaskList") {
        $("#Edittask").modal('hide');
        OpenSubTaskListModel(MainTaskIDForSubTask, AssignmentID, MainTaskTitleForSubTask);
    } else {
        $("#Edittask").modal('hide');
    }
}

function CloseEditViewAreaTaskPopUp() {
    var IsFromWhichView = $("#IsFromWhichView").val();
    var AssignmentAreaIDForAreaTask = $("#AssignmentAreaIDForAreaTask").val();
    var AreaTitleForTask = $("#AreaTitleForTask").val();
    var AssignmentID = $("#AssignmentIDForAreaTaskEditView").val();

    if (IsFromWhichView == "AreaTaskList") {
        $("#EditAreaTask").modal('hide');
        OpenAreaTaskListModel(AssignmentAreaIDForAreaTask, AssignmentID, AreaTitleForTask);
    }
}

function CloseAreaTaskEditPopUp() {
    var IsEdit = $("#IsEditForAreaTask").val();
    var hiddenAssignmentAreaID = $("#hiddenAssignmentAreaID").val();
    var hiddentitle = $("#hiddenAreatitleinedit").val();
    var AssignmentID = $("#AssignmentIDForAreaTask").val();
    if (IsEdit == "true") {
        $("#NewAreaTaskForm").modal('hide');
        OpenAreaTaskListModel(hiddenAssignmentAreaID, AssignmentID, hiddentitle);
    } else {
        $("#NewAreaTaskForm").modal('hide');
    }
}

function OpenAreaTaskListFromDel(obj) {
    $("#DeleteAreaTaskModal").modal('hide');
    OpenAreaTaskListModel($(obj).attr("AssignmentAreaID"), $(obj).attr("AssignmentID"), $(obj).attr("hiddenAreatitle"))
}

function OpenSubTaskListFromDel(obj) {
    $("#DeleteSubTaskModal").modal('hide');
    OpenSubTaskListModel($(obj).attr("RefTaskID"), $(obj).attr("AssignmentID"), $(obj).attr("hiddentitle"))
}

function fnAddNewArea() {

    if ($("#form_AreaDetails").valid()) {
        if (dropzone.getAcceptedFiles().length > 0) {
            if (IsAllFileUploaded == true) {
                var AreaID = $("#AssignmentAreas_AreaID").val();
                var MenDaysPerReview = $("#AssignmentAreas_MenDaysPerReview").val();
                var Frequency_Term = $.trim($("#AssignmentAreas_Frequency_Term").val());
                var Qty = $("#AssignmentAreas_Qty").val();
                var TotalMenDays = $("#AssignmentAreas_TotalMenDays").val();
                var NumberOfIterations = $("#AssignmentAreas_NumberOfIterations").val();
                var AssignTo = $.trim($("#AssignmentAreas_AssignTo").val());
                var AssignmentID = $("#AssignmentIDForArea").val();
                var IsEdit = $("#IsAreaEdit").val();
                var AssignmentAreaID = $("#hiddenAssignmentAreaID").val();
                var IsInEstimation = $("#AssignmentAreas_IsInEstimation").val();
                var NotAvailabe_In_Estimation_Remarks = $("#AssignmentAreas_NotAvailabe_In_Estimation_Remarks").val();

                ShowLoader();
                $.ajax({
                    url: "/Pms/ProjectView/SaveAreaDetail",
                    type: "POST",
                    data: { AreaID: AreaID, MenDaysPerReview: MenDaysPerReview, Frequency_Term: Frequency_Term, Qty: Qty, TotalMenDays: TotalMenDays, NumberOfIterations: NumberOfIterations, AssignmentIDStr: AssignmentID, IsEditStr: IsEdit, AssignmentAreaIDStr: AssignmentAreaID, AssignTo: AssignTo, NotAvailabe_In_Estimation_Remarks_Str: NotAvailabe_In_Estimation_Remarks, IsInEstimationStr: IsInEstimation },
                    success: function (response) {
                        HideLoader();
                        if (response == "_Logon_") {
                            window.location.href = "/Login/Login";
                        }
                        else {
                            $("#AddNewAreaForm").modal('hide');
                            $("#AreaListByProjectID").html(response);
                            var table = $('#dt_Area').DataTable({});
                            $('[data-toggle="tooltip"]').tooltip();
                        }
                    }
                })
            }
            else {
                return false;
            }
        }
        else {
            var AreaID = $("#AssignmentAreas_AreaID").val();
            var MenDaysPerReview = $("#AssignmentAreas_MenDaysPerReview").val();
            var Frequency_Term = $.trim($("#AssignmentAreas_Frequency_Term").val());
            var Qty = $("#AssignmentAreas_Qty").val();
            var TotalMenDays = $("#AssignmentAreas_TotalMenDays").val();
            var NumberOfIterations = $("#AssignmentAreas_NumberOfIterations").val();
            var AssignTo = $.trim($("#AssignmentAreas_AssignTo").val());
            var AssignmentID = $("#AssignmentIDForArea").val();
            var IsEdit = $("#IsAreaEdit").val();
            var AssignmentAreaID = $("#hiddenAssignmentAreaID").val();
            var IsInEstimation = $("#AssignmentAreas_IsInEstimation").val();
            var NotAvailabe_In_Estimation_Remarks = $("#AssignmentAreas_NotAvailabe_In_Estimation_Remarks").val();

            ShowLoader();
            $.ajax({
                url: "/Pms/ProjectView/SaveAreaDetail",
                type: "POST",
                data: { AreaID: AreaID, MenDaysPerReview: MenDaysPerReview, Frequency_Term: Frequency_Term, Qty: Qty, TotalMenDays: TotalMenDays, NumberOfIterations: NumberOfIterations, AssignmentIDStr: AssignmentID, IsEditStr: IsEdit, AssignmentAreaIDStr: AssignmentAreaID, AssignTo: AssignTo, NotAvailabe_In_Estimation_Remarks_Str: NotAvailabe_In_Estimation_Remarks, IsInEstimationStr: IsInEstimation },
                success: function (response) {
                    HideLoader();
                    if (response == "_Logon_") {
                        window.location.href = "/Login/Login";
                    }
                    else {
                        $("#AddNewAreaForm").modal('hide');
                        $("#AreaListByProjectID").html(response);
                        var table = $('#dt_Area').DataTable({});
                        $('[data-toggle="tooltip"]').tooltip();
                    }
                }
            })
        }

    }
}

function fnAddNewTimesheet() {
    if ($("#form_TimeSheet").valid()) {

        $("#Timesheet_EndTime").removeAttr("disabled");

        var StartTime = $("#Timesheet_StartTime").val();
        var EndTime = $("#Timesheet_EndTime").val();
        var TaskID = $.trim($("#Timesheet_TaskID").val());
        var MemberID = $("#Timesheet_MemberID").val();
        var Note = $("#Timesheet_Note").val();
        var AssignmentID = $("#AssignmentIDForTimeSheet").val();
        var IsEdit = $("#IsTimesheetEdit").val();
        var TimeSheetID = $("#hiddenTimeSheetID").val();

        var postData = {
            StartTime: $("#Timesheet_StartTime").val(),
            EndTime: $("#Timesheet_EndTime").val(),
            TaskIDStr: $.trim($("#Timesheet_TaskID").val()),
            MemberIDStr: $("#Timesheet_MemberID").val(),
            Note: $("#Timesheet_Note").val(),
            AssignmentIDStr: $("#AssignmentIDForTimeSheet").val(),
            IsEditStr: $("#IsTimesheetEdit").val(),
            TimeSheetIDStr: $("#hiddenTimeSheetID").val(),
        };

        if ($.trim(StartTime) == $.trim(EndTime)) {
            ShowMessage("StartTime and EndTime should not be same.", "error");
        } else if ($.trim(StartTime) < $.trim(EndTime)) {
            ShowLoader();
            $.ajax({
                url: "/Pms/ProjectView/SaveTimesheetDetail",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify(postData),
                success: function (response) {
                    HideLoader();
                    if (response == "_Logon_") {
                        window.location.href = "/Login/Login";
                    }
                    else {
                        if (response.IsSuccess == false) {
                            ShowMessage(response.Message, "error");
                        } else {
                            $("#AddnewTimesheetForm").modal('hide');
                            $("#TimesheetListByProjectID").html(response);
                            var table = $('#dt_timesheet').DataTable({});
                        }
                    }
                }
            })
        }
        else {
            ShowMessage("StartTime should be less than EndTime.", "error");
        }
    }
}

function fnAddNewDiscussion() {
    if ($("#form_NewDiscussion").valid()) {
        var DiscussionTitle = $("#DiscussionTitle").val();
        var DiscussionDetail = $("#DiscussionDetail").val();
        var AssignmentID = $("#AssignmentIDForDiscussion").val();
        var IsEdit = $("#DiscussionIsEdit").val();
        var DiscussionID = $("#hiddenDiscussionID").val();

        ShowLoader();
        $.ajax({
            url: "/Pms/ProjectView/SaveDiscussionBoard",
            type: "POST",
            data: { DiscussionTitle: DiscussionTitle, DiscussionDetail: DiscussionDetail, AssignmentID: AssignmentID, IsEditStr: IsEdit, DiscussionIDStr: DiscussionID },
            success: function (response) {
                HideLoader();
                if (response == "_Logon_") {
                    window.location.href = "/Login/Login";
                }
                else {
                    $("#AddNewDiscussionForm").modal('hide');
                    $("#DiscussionByProjectID").html(response);
                    var table = $('#dt_Discussion').DataTable({});
                }
            }
        })
    }
}

function fnAddNewTaskFromMilestone() {

    if ($("#form_TaskFromMilestoneDetails").valid()) {
        var AreaID = AreaFilterNameForMilestoneList;
        $("#FilteredAreaVal").val(AreaID);
        //if ($.trim($("#MilestoneTask_ActualEndDate").val()) != "" && $.trim($("#IsAllowToCompleteTask").val()) != '' && $("#IsAllowToCompleteTask").val().toString().toUpperCase() == "FALSE") {
        //    toastr["error"]("Please fill timesheet for this task.", "Task", { "positionClass": "toast-bottom-right", "progressBar": true });
        //}
        //else {
        if (!($("#VALmassage_0").text() == "File size exceeds 5 MB!" || $("#VALmassage_1").text() == "File size exceeds 5 MB!" || $("#VALmassage_2").text() == "File size exceeds 5 MB!" || $("#VALmassage_3").text() == "File size exceeds 5 MB!" || $("#VALmassage_4").text() == "File size exceeds 5 MB!")) {
            if ($("#form_TaskFromMilestoneDetails").valid()) {

                var table = $("#dt_MilestoneTaskFile tbody");

                table.find('tr').each(function (i) {

                    $(this).find("input[type='hidden']:eq(0)").attr("id", "projectdocumentlist_" + i + "__AttachementID");
                    $(this).find("input[type='hidden']:eq(0)").attr("name", "projectdocumentlist[" + i + "].AttachementID");

                    $(this).find("input[type='hidden']:eq(1)").attr("id", "projectdocumentlist_" + i + "__DocumentID");
                    $(this).find("input[type='hidden']:eq(1)").attr("name", "projectdocumentlist[" + i + "].DocumentID");

                    $(this).find("input[type='hidden']:eq(2)").attr("id", "projectdocumentlist_" + i + "__Term");
                    $(this).find("input[type='hidden']:eq(2)").attr("name", "projectdocumentlist[" + i + "].Term");

                    $(this).find("input[type='hidden']:eq(3)").attr("id", "projectdocumentlist_" + i + "__ProjectTermID");
                    $(this).find("input[type='hidden']:eq(3)").attr("name", "projectdocumentlist[" + i + "].ProjectTermID");

                    $(this).find("input[type='hidden']:eq(4)").attr("id", "projectdocumentlist_" + i + "__FilePath");
                    $(this).find("input[type='hidden']:eq(4)").attr("name", "projectdocumentlist[" + i + "].FilePath");

                    $(this).find("input[type='hidden']:eq(5)").attr("id", "projectdocumentlist_" + i + "__FileName");
                    $(this).find("input[type='hidden']:eq(5)").attr("name", "projectdocumentlist[" + i + "].FileName");

                    $(this).find("input[type='hidden']:eq(6)").attr("id", "projectdocumentlist_" + i + "__FileSize");
                    $(this).find("input[type='hidden']:eq(6)").attr("name", "projectdocumentlist[" + i + "].FileSize");

                    $(this).find("input[type='hidden']:eq(7)").attr("id", "projectdocumentlist_" + i + "__ExtensionType_Term");
                    $(this).find("input[type='hidden']:eq(7)").attr("name", "projectdocumentlist[" + i + "].ExtensionType_Term");

                    $(this).find("input[type='hidden']:eq(8)").attr("id", "projectdocumentlist_" + i + "__DisplayName");
                    $(this).find("input[type='hidden']:eq(8)").attr("name", "projectdocumentlist[" + i + "].DisplayName");

                    $(this).find("input[type='hidden']:eq(9)").attr("id", "projectdocumentlist_" + i + "__CustomTerm");
                    $(this).find("input[type='hidden']:eq(9)").attr("name", "projectdocumentlist[" + i + "].CustomTerm");

                    $(this).find('td:eq(0)').find('select').attr("id", "projectdocumentlist_" + i + "__DocType_Term");
                    $(this).find('td:eq(0)').find('select').attr("name", "projectdocumentlist[" + i + "].DocType_Term");

                    $(this).find('td:eq(0)').find('input').attr("id", "projectdocumentlist_" + i + "__OtherDocType");
                    $(this).find('td:eq(0)').find('input').attr("name", "projectdocumentlist[" + i + "].OtherDocType");

                    $(this).find('td:eq(1)').find('select').attr("id", "projectdocumentlist_" + i + "__Status_Term");
                    $(this).find('td:eq(1)').find('select').attr("name", "projectdocumentlist[" + i + "].Status_Term");

                    $(this).find('td:eq(2)').find('input[class="isnew"]').attr("id", "projectdocumentlist_" + i + "__IsNew");
                    $(this).find('td:eq(2)').find('input[class="isnew"]').attr("name", "projectdocumentlist[" + i + "].IsNew");

                    $(this).find('td:eq(3)').find('input[versionno]').attr("id", "projectdocumentlist_" + i + "__VersionNo");
                    $(this).find('td:eq(3)').find('input[versionno]').attr("name", "projectdocumentlist[" + i + "].VersionNo");

                    $(this).find('td:eq(0)').find('select').attr("id", "projectdocumentlist_" + i + "__DocType_Term").removeAttr("disabled");

                    $(this).find('td:eq(3)').find('input[versionno]').attr("id", "projectdocumentlist_" + i + "__VersionNo").removeAttr("disabled");

                });

                $(".bs-select").selectpicker('refresh');

                table.find('tr').each(function (i) {
                    $(this).find('td:eq(0)').find('input[otherdoctype]').attr("id", "projectdocumentlist_" + i + "__OtherDocType").removeAttr("disabled");
                    var rowCount = 1;
                    rowCount = rowCount + i;
                    $("#hiddenMilestoneRowCount").val(rowCount);
                })

                $("#MilestoneTask_StartDate").removeAttr('disabled');
                $("#MilestoneTask_EndDate").removeAttr('disabled');

                var form = $('#form_TaskFromMilestoneDetails')[0];
                var data = new FormData(form);

                ShowLoader();

                $.ajax({
                    type: "POST",
                    enctype: 'multipart/form-data',
                    url: "/Pms/ProjectView/SaveTaskDetailFromMilestone",
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
                        else if (response.IsSuccess && response.Data == "NotAllowToComplete") {
                            if (response.Message == "Please fill timesheet for this task.") {
                                toastr["error"]("Please fill timesheet for this task.", "Milestone", { "positionClass": "toast-bottom-right", "progressBar": true });
                            } else {
                                toastr["error"]("You can not complete this task as subtask are open.", "Milestone", { "positionClass": "toast-bottom-right", "progressBar": true });
                            }
                        }
                        else {
                            $("#MilestoneNewTaskForm").modal('hide');
                            $("#MileastoneListByProjectID").html(response);
                            $('[data-toggle="tooltip"]').tooltip();
                            $('.scroller').slimScroll({
                                allowPageScroll: true, // allow page scroll when the element scroll is ended
                                size: '7px',
                                color: ($(this).attr("data-handle-color") ? $(this).attr("data-handle-color") : '#bbb'),
                                wrapperClass: ($(this).attr("data-wrapper-class") ? $(this).attr("data-wrapper-class") : 'slimScrollDiv'),
                                railColor: ($(this).attr("data-rail-color") ? $(this).attr("data-rail-color") : '#eaeaea'),
                                position: 'right',
                                height: '345px',
                                alwaysVisible: ($(this).attr("data-always-visible") == "1" ? true : false),
                                railVisible: ($(this).attr("data-rail-visible") == "1" ? true : false),
                                disableFadeOut: true
                            });
                            if (AreaID == "All") {
                                AreaFilterNameForMilestoneList = AreaID;
                            } else {
                                AreaFilterNameForMilestoneList = AreaID.toString().toUpperCase();
                            }
                            $("#MilestoneList_Filter_AreaID option[value='" + AreaFilterNameForMilestoneList + "']").attr('selected', 'selected');
                            $('.bs-select').selectpicker('refresh');
                        }
                    },
                    error: function (e) {
                        $("#result").text(e.responseText);
                        console.log("ERROR : ", e);
                        $("#btnSubmit").prop("disabled", false);

                    }
                });
            }
        }
        //}
    }
}

//function fnAddNewTaskFromMilestone() {

//    if ($("#form_TaskFromMilestoneDetails").valid()) {
//        $(".date-picker").removeAttr('disabled');
//        var TaskTitle = $("#MilestoneTask_TaskTitle").val();
//        var MilestoneID = $("#MilestoneTask_MilestoneID").val();
//        var AssignTo = $.trim($("#MilestoneTask_AssignTo").val());
//        var TotalMenDays = $("#MilestoneTask_TotalMenDays").val();
//        var StartDate = $("#MilestoneTask_StartDate").val();
//        var EndDate = $("#MilestoneTask_EndDate").val();
//        var ActualStartDate = $("#MilestoneTask_ActualStartDate").val();
//        var ActualEndDate = $("#MilestoneTask_ActualEndDate").val();
//        var Status = $("#MilestoneTask_Status_Term").val();
//        var Priority = $("#MilestoneTask_Priority_Term").val();
//        var TaskDetail = $("#MilestoneTask_TaskDetail").val();
//        var AssignmentID = $("#AssignmentIDForTaskMilestone").val();

//        ShowLoader();
//        $.ajax({
//            url: "/Pms/ProjectView/SaveTaskDetailFromMilestone",
//            type: "POST",
//            data: { TaskTitle: TaskTitle, MilestoneID: MilestoneID, AssignTo: AssignTo, TotalMenDays: TotalMenDays, StartDate: StartDate, EndDate: EndDate, ActualStartDate: ActualStartDate, ActualEndDate: ActualEndDate, Status: Status, Priority: Priority, TaskDetail: TaskDetail, AssignmentID: AssignmentID },
//            success: function (response) {
//            HideLoader();
//            if (response == "_Logon_") {
//                window.location.href = "/Login/Login";
//            }
//            else {
//                $("#MilestoneNewTaskForm").modal('hide');
//                $("#MileastoneListByProjectID").html(response);
//            }

//          }
//        })
//    }
//}

function fnOpenTaskDeleteModel(DeleteButtonID, DeleteModalID, DeleteIDToset) {
    $("#" + DeleteButtonID).attr("DelID", DeleteIDToset);
    var hiddenAssignmentID = $("#hiddenAssignmentID").val();
    $("#" + DeleteButtonID).attr("AssignmentID", hiddenAssignmentID);
    $("#" + DeleteModalID).modal('show');
}

function fnOpenAssignmetnResourceDeleteModel(DeleteButtonID, DeleteModalID, DeleteIDToset) {
    $("#" + DeleteButtonID).attr("DelID", DeleteIDToset);
    var hiddenAssignmentID = $("#hiddenAssignmentID").val();
    $("#" + DeleteButtonID).attr("AssignmentID", hiddenAssignmentID);
    $("#" + DeleteModalID).modal('show');
}

function fnOpenSubTaskDeleteModel(DeleteButtonID, DeleteModalID, DeleteIDToset, RefTaskID) {
    $("#SubTaskListForm").modal('hide');
    $("#" + DeleteButtonID).attr("DelID", DeleteIDToset);
    $("#CloseSubTaskListDeleteButton").attr("RefTaskID", RefTaskID);
    $("#" + DeleteButtonID).attr("hiddentitle", $("#hiddentitle").val());
    $("#" + DeleteButtonID).attr("RefTaskID", RefTaskID);
    var hiddenAssignmentID = $("#hiddenAssignmentID").val();
    $("#" + DeleteButtonID).attr("AssignmentID", hiddenAssignmentID);
    $("#CloseSubTaskListDeleteButton").attr("AssignmentID", hiddenAssignmentID);
    $("#CloseSubTaskListDeleteButton").attr("hiddentitle", $("#hiddentitle").val());
    $("#" + DeleteModalID).modal('show');
}

function fnOpenAreaTaskDeleteModel(DeleteButtonID, DeleteModalID, DeleteIDToset, RefAssignmentAreaID) {
    $("#AreaTaskListForm").modal('hide');
    $("#" + DeleteButtonID).attr("DelID", DeleteIDToset);
    $("#CloseAreaTaskListDeleteButton").attr("AssignmentAreaID", RefAssignmentAreaID);
    $("#" + DeleteButtonID).attr("hiddenAreatitle", $("#hiddenAreatitle").val());
    $("#" + DeleteButtonID).attr("RefAssignmentAreaID", RefAssignmentAreaID);
    var hiddenAssignmentID = $("#hiddenAssignmentID").val();
    $("#" + DeleteButtonID).attr("AssignmentID", hiddenAssignmentID);
    $("#CloseAreaTaskListDeleteButton").attr("AssignmentID", hiddenAssignmentID);
    $("#CloseAreaTaskListDeleteButton").attr("hiddenAreatitle", $("#hiddenAreatitle").val());
    $("#" + DeleteModalID).modal('show');
}

function fnOpenAreaDeleteModel(DeleteButtonID, DeleteModalID, DeleteIDToset) {
    $("#" + DeleteButtonID).attr("DelID", DeleteIDToset);
    var hiddenAssignmentID = $("#hiddenAssignmentID").val();
    $("#" + DeleteButtonID).attr("AssignmentID", hiddenAssignmentID);
    $("#" + DeleteModalID).modal('show');
}

function fnOpenTimesheetDeleteModel(DeleteButtonID, DeleteModalID, DeleteIDToset) {
    $("#" + DeleteButtonID).attr("DelID", DeleteIDToset);
    var hiddenAssignmentID = $("#hiddenAssignmentID").val();
    $("#" + DeleteButtonID).attr("AssignmentID", hiddenAssignmentID);
    $("#" + DeleteModalID).modal('show');
}

function fnOpenMilestoneDeleteModel(DeleteMilestoneButton, DeleteMilestoneModal, DeleteIDToset) {
    $("#" + DeleteMilestoneButton).attr("DelID", DeleteIDToset);
    var hiddenAssignmentID = $("#hiddenAssignmentID").val();
    $("#" + DeleteMilestoneButton).attr("AssignmentID", hiddenAssignmentID);
    $("#" + DeleteMilestoneModal).modal('show');
}

function DeleteTaskEntity(urlTopass, DeleteButtonID, DeleteModalID, EntityNameToDelete, tableidTopass, Redirectionurl) {
    var trId = $("#" + DeleteButtonID).attr("DelID");
    var postData = {
        DeleteID: $("#" + DeleteButtonID).attr("DelID"),
        AssignmentIDStr: $("#" + DeleteButtonID).attr("AssignmentID"),
        AreaIDStr: AreaFilterNameForMilestoneList
    };
    ShowLoader();
    AjaxCall(urlTopass, JSON.stringify(postData), "post", "json", "application/json", true).done(function (data) {
        HideLoader();
        if (data == "_Logon_") {
            window.location.href = "/Login/Login";
        }
        else {
            $("#" + DeleteModalID).modal('hide');
            if (data == 'error') {
                toastr["error"]("Oops Something Wentwrong !!!", EntityNameToDelete, { "positionClass": "toast-bottom-right", "progressBar": true });
            }
            else {
                if (data.length != 0) {
                    $("#TaskListByProjectID").html(data);
                    var table = $('#dt_task').DataTable({});
                    $('[data-toggle="tooltip"]').tooltip();
                    toastr["success"](EntityNameToDelete + " deleted successfully.", EntityNameToDelete, { "positionClass": "toast-bottom-right", "progressBar": true });
                }
            }
        }
    });

}

function DeleteAssignmentResourceEntity(urlTopass, DeleteButtonID, DeleteModalID, EntityNameToDelete, tableidTopass, Redirectionurl) {
    var trId = $("#" + DeleteButtonID).attr("DelID");
    var postData = {
        DeleteID: $("#" + DeleteButtonID).attr("DelID"),
        AssignmentIDStr: $("#" + DeleteButtonID).attr("AssignmentID")
    };
    ShowLoader();
    AjaxCall(urlTopass, JSON.stringify(postData), "post", "json", "application/json", true).done(function (data) {
        HideLoader();
        if (data == "_Logon_") {
            window.location.href = "/Login/Login";
        }
        else {
            $("#" + DeleteModalID).modal('hide');
            if (data == 'error') {
                toastr["error"]("Oops Something Wentwrong !!!", EntityNameToDelete, { "positionClass": "toast-bottom-right", "progressBar": true });
            }
            else {
                if (data.length != 0) {
                    $("#OverviewByProjectID").html(data);
                    LoadChart();
                    $("#range_2").ionRangeSlider({
                        min: 100,
                        max: 1000,
                        from: 550
                    });
                    $('.bs-select').selectpicker('refresh');
                    var widgets = widgets = DonutWidget.draw();
                    toastr["success"](EntityNameToDelete + " deleted successfully.", EntityNameToDelete, { "positionClass": "toast-bottom-right", "progressBar": true });
                    //window.location.href = Redirectionurl;
                }
            }
        }
    });

}

function DeleteSubTaskEntity(urlTopass, DeleteButtonID, DeleteModalID, EntityNameToDelete, tableidTopass, Redirectionurl) {
    var trId = $("#" + DeleteButtonID).attr("DelID");
    var postData = {
        DeleteID: $("#" + DeleteButtonID).attr("DelID"),
        AssignmentIDStr: $("#" + DeleteButtonID).attr("AssignmentID"),
        AreaIDStr: AreaFilterNameForMilestoneList
    };
    ShowLoader();
    AjaxCall(urlTopass, JSON.stringify(postData), "post", "json", "application/json", true).done(function (data) {
        HideLoader();
        if (data == "_Logon_") {
            window.location.href = "/Login/Login";
        }
        else {
            $("#" + DeleteModalID).modal('hide');
            if (data == 'error') {
                toastr["error"]("Oops Something Wentwrong !!!", EntityNameToDelete, { "positionClass": "toast-bottom-right", "progressBar": true });
            }
            else {
                if (data.length != 0) {
                    $("#SubTaskListForm").modal('hide');
                    OpenSubTaskListModel($("#" + DeleteButtonID).attr("RefTaskID"), $("#" + DeleteButtonID).attr("AssignmentID"), $("#" + DeleteButtonID).attr("hiddentitle"));
                    $("#TaskListByProjectID").html(data);
                    var table = $('#dt_task').DataTable({});
                    $('[data-toggle="tooltip"]').tooltip();
                    toastr["success"](EntityNameToDelete + " deleted successfully.", EntityNameToDelete, { "positionClass": "toast-bottom-right", "progressBar": true });
                    //window.location.href = Redirectionurl;
                }
            }
        }
    });

}

function DeleteAreaTaskEntity(urlTopass, DeleteButtonID, DeleteModalID, EntityNameToDelete, tableidTopass, Redirectionurl) {
    var trId = $("#" + DeleteButtonID).attr("DelID");
    var postData = {
        DeleteID: $("#" + DeleteButtonID).attr("DelID"),
        AssignmentIDStr: $("#" + DeleteButtonID).attr("AssignmentID")
    };
    ShowLoader();
    AjaxCall(urlTopass, JSON.stringify(postData), "post", "json", "application/json", true).done(function (data) {
        HideLoader();
        if (data == "_Logon_") {
            window.location.href = "/Login/Login";
        }
        else {
            $("#" + DeleteModalID).modal('hide');
            if (data == 'error') {
                toastr["error"]("Oops Something Wentwrong !!!", EntityNameToDelete, { "positionClass": "toast-bottom-right", "progressBar": true });
            }
            else {
                if (data.length != 0) {
                    $("#AreaTaskListForm").modal('hide');
                    OpenAreaTaskListModel($("#" + DeleteButtonID).attr("RefAssignmentAreaID"), $("#" + DeleteButtonID).attr("AssignmentID"), $("#" + DeleteButtonID).attr("hiddenAreatitle"));
                    $("#AreaListByProjectID").html(data);
                    var table = $('#dt_Area').DataTable({});
                    $('[data-toggle="tooltip"]').tooltip();
                    toastr["success"](EntityNameToDelete + " deleted successfully.", EntityNameToDelete, { "positionClass": "toast-bottom-right", "progressBar": true });
                    //window.location.href = Redirectionurl;
                }
            }
        }
    });

}

function DeleteAreaEntity(urlTopass, DeleteButtonID, DeleteModalID, EntityNameToDelete, tableidTopass, Redirectionurl) {
    var trId = $("#" + DeleteButtonID).attr("DelID");
    var postData = {
        DeleteID: $("#" + DeleteButtonID).attr("DelID"),
        AssignmentIDStr: $("#" + DeleteButtonID).attr("AssignmentID")
    };
    ShowLoader();
    AjaxCall(urlTopass, JSON.stringify(postData), "post", "json", "application/json", true).done(function (data) {
        HideLoader();
        if (data == "_Logon_") {
            window.location.href = "/Login/Login";
        }
        else {
            $("#" + DeleteModalID).modal('hide');
            if (data == 'error') {
                toastr["error"]("Oops Something Wentwrong !!!", EntityNameToDelete, { "positionClass": "toast-bottom-right", "progressBar": true });
            }
            else {
                if (data.length != 0) {
                    $("#AreaListByProjectID").html(data);
                    var table = $('#dt_Area').DataTable({});
                    $('[data-toggle="tooltip"]').tooltip();
                    toastr["success"](EntityNameToDelete + " deleted successfully.", EntityNameToDelete, { "positionClass": "toast-bottom-right", "progressBar": true });
                    //window.location.href = Redirectionurl;
                }
            }
        }
    });

}

function DeleteTimesheetEntity(urlTopass, DeleteButtonID, DeleteModalID, EntityNameToDelete, tableidTopass, Redirectionurl) {
    var trId = $("#" + DeleteButtonID).attr("DelID");
    var postData = {
        DeleteID: $("#" + DeleteButtonID).attr("DelID"),
        AssignmentIDStr: $("#" + DeleteButtonID).attr("AssignmentID")
    };
    ShowLoader();
    AjaxCall(urlTopass, JSON.stringify(postData), "post", "json", "application/json", true).done(function (data) {
        HideLoader();
        if (data == "_Logon_") {
            window.location.href = "/Login/Login";
        }
        else {
            $("#" + DeleteModalID).modal('hide');
            if (data == 'error') {
                toastr["error"]("Oops Something Wentwrong !!!", EntityNameToDelete, { "positionClass": "toast-bottom-right", "progressBar": true });
            }
            else {
                if (data.length != 0) {
                    $("#TimesheetListByProjectID").html(data);
                    var table = $('#dt_timesheet').DataTable({});
                    toastr["success"](EntityNameToDelete + " deleted successfully.", EntityNameToDelete, { "positionClass": "toast-bottom-right", "progressBar": true });
                    //window.location.href = Redirectionurl;
                }
            }
        }
    });

}

function DeleteMilestoneEntity(urlTopass, DeleteMilestoneButton, DeleteMilestoneModal, EntityNameToDelete, tableidTopass, Redirectionurl) {
    var trId = $("#" + DeleteMilestoneButton).attr("DelID");
    var AreaID = AreaFilterNameForMilestoneList;

    var postData = {
        DeleteID: $("#" + DeleteMilestoneButton).attr("DelID"),
        AssignmentIDStr: $("#" + DeleteMilestoneButton).attr("AssignmentID"),
        AreaIDStr: AreaID
    };
    ShowLoader();
    AjaxCall(urlTopass, JSON.stringify(postData), "post", "json", "application/json", true).done(function (data) {
        HideLoader();
        if (data == "_Logon_") {
            window.location.href = "/Login/Login";
        }
        else {
            $("#" + DeleteMilestoneModal).modal('hide');
            if (data == 'error') {
                toastr["error"]("Oops Something Wentwrong !!!", EntityNameToDelete, { "positionClass": "toast-bottom-right", "progressBar": true });
            }
            else {
                if (data.length != 0) {
                    $("#MileastoneListByProjectID").html(data);
                    $('[data-toggle="tooltip"]').tooltip();
                    $('.scroller').slimScroll({
                        allowPageScroll: true, // allow page scroll when the element scroll is ended
                        size: '7px',
                        color: ($(this).attr("data-handle-color") ? $(this).attr("data-handle-color") : '#bbb'),
                        wrapperClass: ($(this).attr("data-wrapper-class") ? $(this).attr("data-wrapper-class") : 'slimScrollDiv'),
                        railColor: ($(this).attr("data-rail-color") ? $(this).attr("data-rail-color") : '#eaeaea'),
                        position: 'right',
                        height: '345px',
                        alwaysVisible: ($(this).attr("data-always-visible") == "1" ? true : false),
                        railVisible: ($(this).attr("data-rail-visible") == "1" ? true : false),
                        disableFadeOut: true
                    });
                    if (AreaID == "All") {
                        AreaFilterNameForMilestoneList = AreaID;
                    } else {
                        AreaFilterNameForMilestoneList = AreaID.toString().toUpperCase();
                    }
                    $("#MilestoneList_Filter_AreaID option[value='" + AreaFilterNameForMilestoneList + "']").attr('selected', 'selected');
                    $('.bs-select').selectpicker('refresh');
                    toastr["success"](EntityNameToDelete + " deleted successfully.", EntityNameToDelete, { "positionClass": "toast-bottom-right", "progressBar": true });
                    //window.location.href = Redirectionurl;
                }
            }
        }
    });

}

function fnAddNewMilestone() {
    if ($("#form_Milestone").valid()) {

        var AreaID = AreaFilterNameForMilestoneList;

        //var MilestoneCode = $("#Milestones_MilestoneCode").val();
        var MilestoneCode = "";
        var MilestoneTitle = $("#Milestones_MilestoneTitle").val();
        var MilestoneManagerID = $.trim($("#Milestones_MilestoneManagerID").val());
        //var TotalResources = $("#Milestones_TotalResources").val();
        var TotalResources = "";
        var StartDate = $("#Milestones_StartDate").val();
        var EndDate = $("#Milestones_EndDate").val();
        var TotalMenDays = $("#Milestones_TotalMenDays").val();
        var MilestoneDetail = $("#Milestones_MilestoneDetail").val();
        var MilestonesAreaID = $("#Milestones_AreaID").val();
        var MilestonesAssignTo = $.trim($("#Milestones_AssignTo").val());
        var IsBillable = $("#Milestones_IsBillable").prop('checked');
        var AssignmentID = $("#AssignmentIDForMilestone").val();
        var IsEdit = $("#MilestoneIsEdit").val();
        var MilestoneID = $("#hiddenMilestoneID").val();

        ShowLoader();
        $.ajax({
            url: "/Pms/ProjectView/SaveMilestoneDetail",
            type: "POST",
            data: { MilestoneCode: MilestoneCode, MilestoneTitle: MilestoneTitle, MilestoneManagerID: MilestoneManagerID, TotalResources: TotalResources, StartDate: StartDate, EndDate: EndDate, TotalMenDays: TotalMenDays, MilestoneDetail: MilestoneDetail, MilestonesAreaID: MilestonesAreaID, MilestonesAssignTo: MilestonesAssignTo, IsBillable: IsBillable, AssignmentID: AssignmentID, IsEditStr: IsEdit, MilestoneIDStr: MilestoneID, AreaIDStr: AreaID },
            success: function (response) {
                HideLoader();
                if (response == "_Logon_") {
                    window.location.href = "/Login/Login";
                }
                else {
                    $("#NewMilestoneForm").modal('hide');
                    $("#MileastoneListByProjectID").html(response);
                    $('[data-toggle="tooltip"]').tooltip();
                    $('.scroller').slimScroll({
                        allowPageScroll: true, // allow page scroll when the element scroll is ended
                        size: '7px',
                        color: ($(this).attr("data-handle-color") ? $(this).attr("data-handle-color") : '#bbb'),
                        wrapperClass: ($(this).attr("data-wrapper-class") ? $(this).attr("data-wrapper-class") : 'slimScrollDiv'),
                        railColor: ($(this).attr("data-rail-color") ? $(this).attr("data-rail-color") : '#eaeaea'),
                        position: 'right',
                        height: '345px',
                        alwaysVisible: ($(this).attr("data-always-visible") == "1" ? true : false),
                        railVisible: ($(this).attr("data-rail-visible") == "1" ? true : false),
                        disableFadeOut: true
                    });
                    if (AreaID == "All") {
                        AreaFilterNameForMilestoneList = AreaID;
                    } else {
                        AreaFilterNameForMilestoneList = AreaID.toString().toUpperCase();
                    }
                    $("#MilestoneList_Filter_AreaID option[value='" + AreaFilterNameForMilestoneList + "']").attr('selected', 'selected');
                    $('.bs-select').selectpicker('refresh');
                }
            }
        })
    }
}

function AddnewTimesheet(form) {
    ClearPopup(form);
    var hiddenAssignmentID = $("#hiddenAssignmentID").val();
    ShowLoader();
    $.ajax({
        url: "/Pms/ProjectView/GetNewDataForTimeSheetPopup",
        type: "POST",
        data: { AssignmentIDStr: hiddenAssignmentID },
        success: function (response) {
            HideLoader();
            if (response == "_Logon_") {
                window.location.href = "/Login/Login";
            }
            else {
                $("#TimeSheetPopup").html(response);
                $("#AddnewTimesheetForm").modal('show');
                $("#" + form).removeData('validator');
                $("#" + form).removeData('unobtrusiveValidation');
                $.validator.unobtrusive.parse("#" + form);
                $("#AssignmentIDForTimeSheet").val(hiddenAssignmentID);
                $('.bs-select').selectpicker('refresh');
                var TodayEndDate = moment(new Date()).format("DD/MM/YYYY 23:59");

                $(function () {
                    var startDate = new Date('01/01/1900');
                    var FromEndDate = new Date();
                    var ToEndDate = new Date();

                    $('.start_date_tm').datetimepicker({
                        autoclose: true,
                        format: "dd/mm/yyyy hh:ii",
                        fontAwesome: true,
                        showMeridian: true,
                        pickerPosition: "bottom-right",
                        endDate: TodayEndDate
                    })
                        .on('changeDate', function (selected) {
                            $('.end_date_tm').val("");
                            $('.end_date_tm').removeAttr("disabled");
                            startDate = new Date(selected.date.valueOf());
                            startDate.setDate(startDate.getDate(new Date(selected.date.valueOf())));
                            $('.end_date').datetimepicker('setStartDate', startDate);
                        });
                    $('.end_date_tm').datetimepicker({
                        autoclose: true,
                        format: "dd/mm/yyyy hh:ii",
                        fontAwesome: true,
                        showMeridian: true,
                        pickerPosition: "bottom-right",
                        endDate: TodayEndDate
                    })
                });

                $(".start_date_tm").on("change", function (evt, params) {
                    $(evt.target).valid();
                });

                $(".end_date_tm").on("change", function (evt, params) {
                    $(evt.target).valid();
                });

                $(function () {
                    $.validator.methods.date = function (value, element) {
                        return this.optional(element) || moment(value, "DD/MM/YYYY HH:mm", true).isValid();
                    }
                });

                $('#Timesheet_Note').summernote({
                    height: 100,                 // set editor height

                    minHeight: null,             // set minimum height of editor
                    maxHeight: null,             // set maximum height of editor

                    focus: true                 // set focus to editable area after initializing summernote
                });
            }
        }
    })
}

//function LoadTaskData() {
//    $("#Edittask").modal('show');
//}

function LoadTaskData(TaskID, AssignmentID, form) {
    var hiddenAssignmentID = $("#hiddenAssignmentID").val();
    ClearPopup(form);
    ShowLoader();
    $.ajax({
        url: "/Pms/ProjectView/GetTaskDataForEditAndView",
        type: "POST",
        data: { TaskIDStr: TaskID, AssignmentIDStr: AssignmentID },
        success: function (response) {
            HideLoader();
            if (response == "_Logon_") {
                window.location.href = "/Login/Login";
            }
            else {
                $("#LoadTaskData").html(response);
                $("#Edittask").modal('show');
                $("#" + form).removeData('validator');
                $("#" + form).removeData('unobtrusiveValidation');
                $.validator.unobtrusive.parse("#" + form);
                $("#AssignmentIDForTaskEditView").val(hiddenAssignmentID);
                $("#hiddenTaskViewID").val(TaskID);
                $("#MainTaskIDForSubTask").val("");
                $("#MainTaskTitleForSubTask").val("");
                $("#IsFromWhichView").val("TaskList");
                $('.bs-select').selectpicker('refresh');
                $('.date-picker').datepicker({
                    format: 'dd/mm/yyyy',
                    autoclose: true,
                    orientation: "bottom",
                    todayHighlight: true
                })
                $(function () {
                    var startDate = new Date('01/01/1900');
                    var FromEndDate = new Date();
                    var ToEndDate = new Date();

                    $('.start_date_etm').datetimepicker({
                        autoclose: true,
                        format: "dd/mm/yyyy hh:ii",
                        fontAwesome: true,
                        showMeridian: true,
                        pickerPosition: "bottom-right"
                    })
                        .on('changeDate', function (selected) {
                            $('.end_date_etm').val("");
                            startDate = new Date(selected.date.valueOf());
                            startDate.setDate(startDate.getDate(new Date(selected.date.valueOf())));
                            $('.end_date_etm').datetimepicker('setStartDate', startDate);
                        });
                    $('.end_date_etm').datetimepicker({
                        autoclose: true,
                        format: "dd/mm/yyyy hh:ii",
                        fontAwesome: true,
                        showMeridian: true,
                        pickerPosition: "bottom-right"
                    })
                });

                $(".start_date_etm").on("change", function (evt, params) {
                    $(evt.target).valid();
                });

                $(".end_date_etm").on("change", function (evt, params) {
                    $(evt.target).valid();
                });

                $(function () {
                    $.validator.methods.date = function (value, element) {
                        return this.optional(element) || moment(value, "DD/MM/YYYY HH:mm", true).isValid();
                    }
                });
                $('#EditTask_Comment').summernote({
                    height: 100,                 // set editor height

                    minHeight: null,             // set minimum height of editor
                    maxHeight: null,             // set maximum height of editor

                    focus: true                 // set focus to editable area after initializing summernote
                });
                $('[data-toggle="tooltip"]').tooltip();
                var table = $('#dt_timesheetForEditTask').DataTable({});
            }
        }
    })
    //$("#Edittask").modal('show');
}

function LoadSubTaskData(TaskID, AssignmentID, EncryptedRefTaskID, form) {
    var hiddenAssignmentID = $("#hiddenAssignmentID").val();
    ClearPopup(form);
    ShowLoader();
    $.ajax({
        url: "/Pms/ProjectView/GetSubTaskDataForEditView",
        type: "POST",
        data: { TaskIDStr: TaskID, AssignmentIDStr: AssignmentID },
        success: function (response) {
            HideLoader();
            if (response == "_Logon_") {
                window.location.href = "/Login/Login";
            }
            else {
                $("#SubTaskListForm").modal('hide');
                $("#LoadTaskData").html(response);
                $("#Edittask").modal('show');
                $("#" + form).removeData('validator');
                $("#" + form).removeData('unobtrusiveValidation');
                $.validator.unobtrusive.parse("#" + form);
                $("#AssignmentIDForTaskEditView").val(hiddenAssignmentID);
                $("#hiddenTaskViewID").val(TaskID);
                $("#MainTaskIDForSubTask").val(EncryptedRefTaskID);
                $("#MainTaskTitleForSubTask").val($("#hiddentitle").val());
                $("#IsFromWhichView").val("SubTaskList");
                $('.bs-select').selectpicker('refresh');
                $('.date-picker').datepicker({
                    format: 'dd/mm/yyyy',
                    autoclose: true,
                    orientation: "bottom",
                    todayHighlight: true
                })
                $(function () {
                    var startDate = new Date('01/01/1900');
                    var FromEndDate = new Date();
                    var ToEndDate = new Date();

                    $('.start_date_etm').datetimepicker({
                        autoclose: true,
                        format: "dd/mm/yyyy hh:ii",
                        fontAwesome: true,
                        showMeridian: true,
                        pickerPosition: "bottom-right"
                    })
                        .on('changeDate', function (selected) {
                            $('.end_date_etm').val("");
                            startDate = new Date(selected.date.valueOf());
                            startDate.setDate(startDate.getDate(new Date(selected.date.valueOf())));
                            $('.end_date_etm').datetimepicker('setStartDate', startDate);
                        });
                    $('.end_date_etm').datetimepicker({
                        autoclose: true,
                        format: "dd/mm/yyyy hh:ii",
                        fontAwesome: true,
                        showMeridian: true,
                        pickerPosition: "bottom-right"
                    })
                });

                $(".start_date_etm").on("change", function (evt, params) {
                    $(evt.target).valid();
                });

                $(".end_date_etm").on("change", function (evt, params) {
                    $(evt.target).valid();
                });

                $(function () {
                    $.validator.methods.date = function (value, element) {
                        return this.optional(element) || moment(value, "DD/MM/YYYY HH:mm", true).isValid();
                    }
                });
                $('#EditTask_Comment').summernote({
                    height: 100,
                    minHeight: null,
                    maxHeight: null,
                    focus: true
                });
                $('[data-toggle="tooltip"]').tooltip();
                var table = $('#dt_timesheetForEditTask').DataTable({});
            }
        }
    })
}

function LoadAreaTaskData(TaskID, AssignmentID, EncryptedAssignmentAreaID, form) {
    var hiddenAssignmentID = $("#hiddenAssignmentID").val();
    ClearPopup(form);
    ShowLoader();
    $.ajax({
        url: "/Pms/ProjectView/GetAreaTaskDataForEditAndView",
        type: "POST",
        data: { TaskIDStr: TaskID, AssignmentIDStr: AssignmentID },
        success: function (response) {
            HideLoader();
            if (response == "_Logon_") {
                window.location.href = "/Login/Login";
            }
            else {
                $("#AreaTaskListForm").modal('hide');
                $("#LoadAreaTaskData").html(response);
                $("#EditAreaTask").modal('show');
                $("#" + form).removeData('validator');
                $("#" + form).removeData('unobtrusiveValidation');
                $.validator.unobtrusive.parse("#" + form);
                $("#AssignmentIDForAreaTaskEditView").val(hiddenAssignmentID);
                $("#hiddenAreaTaskViewID").val(TaskID);
                $("#AssignmentAreaIDForAreaTask").val(EncryptedAssignmentAreaID);
                $("#AreaTitleForTask").val($("#hiddenAreatitle").val());
                $("#IsFromWhichView").val("AreaTaskList");
                $('.bs-select').selectpicker('refresh');
                //$('.date-picker').datepicker({
                //    format: 'dd/mm/yyyy',
                //    autoclose: true,
                //    orientation: "bottom",
                //    todayHighlight: true
                //})
                $(function () {
                    var startDate = new Date('01/01/1900');
                    var FromEndDate = new Date();
                    var ToEndDate = new Date();

                    $('.start_date_aev').datetimepicker({
                        autoclose: true,
                        format: "dd/mm/yyyy hh:ii",
                        fontAwesome: true,
                        showMeridian: true,
                        pickerPosition: "bottom-right"
                    })
                        .on('changeDate', function (selected) {
                            $('.end_date_aev').val("");
                            startDate = new Date(selected.date.valueOf());
                            startDate.setDate(startDate.getDate(new Date(selected.date.valueOf())));
                            $('.end_date_aev').datetimepicker('setStartDate', startDate);
                        });
                    $('.end_date_aev').datetimepicker({
                        autoclose: true,
                        format: "dd/mm/yyyy hh:ii",
                        fontAwesome: true,
                        showMeridian: true,
                        pickerPosition: "bottom-right"
                    })
                });

                $(".start_date_aev").on("change", function (evt, params) {
                    $(evt.target).valid();
                });

                $(".end_date_aev").on("change", function (evt, params) {
                    $(evt.target).valid();
                });

                $(function () {
                    $.validator.methods.date = function (value, element) {
                        return this.optional(element) || moment(value, "DD/MM/YYYY HH:mm", true).isValid();
                    }
                });
                $('#EditAreaTask_Comment').summernote({
                    height: 100,
                    minHeight: null,
                    maxHeight: null,
                    focus: true
                });
                $('[data-toggle="tooltip"]').tooltip();
                var table = $('#dt_timesheetForEditAreaTask').DataTable({});
            }
        }
    })
}

function LoadTaskDataFromMileStone(TaskID, AssignmentID, form) {
    var hiddenAssignmentID = $("#hiddenAssignmentID").val();
    ClearPopup(form);
    ShowLoader();
    $.ajax({
        url: "/Pms/ProjectView/GetTaskDataForEditAndView",
        type: "POST",
        data: { TaskIDStr: TaskID, AssignmentIDStr: AssignmentID },
        success: function (response) {
            HideLoader();
            if (response == "_Logon_") {
                window.location.href = "/Login/Login";
            }
            else {
                $("#LoadTaskData").html(response);
                $("#Edittask").modal('show');
                $("#" + form).removeData('validator');
                $("#" + form).removeData('unobtrusiveValidation');
                $.validator.unobtrusive.parse("#" + form);
                $("#AssignmentIDForTaskEditView").val(hiddenAssignmentID);
                $("#hiddenTaskViewID").val(TaskID);
                $("#IsFromWhichView").val("MilestoneList");
                $("#MainTaskIDForSubTask").val("");
                $("#MainTaskTitleForSubTask").val("");
                $('.bs-select').selectpicker('refresh');
                $('.date-picker').datepicker({
                    format: 'dd/mm/yyyy',
                    autoclose: true,
                    orientation: "bottom",
                    todayHighlight: true
                })
                $(function () {
                    var startDate = new Date('01/01/1900');
                    var FromEndDate = new Date();
                    var ToEndDate = new Date();

                    $('.start_date_etm').datetimepicker({
                        autoclose: true,
                        format: "dd/mm/yyyy hh:ii",
                        fontAwesome: true,
                        showMeridian: true,
                        pickerPosition: "bottom-right"
                    })
                        .on('changeDate', function (selected) {
                            $('.end_date_etm').val("");
                            startDate = new Date(selected.date.valueOf());
                            startDate.setDate(startDate.getDate(new Date(selected.date.valueOf())));
                            $('.end_date_etm').datetimepicker('setStartDate', startDate);
                        });
                    $('.end_date_etm').datetimepicker({
                        autoclose: true,
                        format: "dd/mm/yyyy hh:ii",
                        fontAwesome: true,
                        showMeridian: true,
                        pickerPosition: "bottom-right"
                    })
                });

                $(".start_date_etm").on("change", function (evt, params) {
                    $(evt.target).valid();
                });

                $(".end_date_etm").on("change", function (evt, params) {
                    $(evt.target).valid();
                });

                $(function () {
                    $.validator.methods.date = function (value, element) {
                        return this.optional(element) || moment(value, "DD/MM/YYYY HH:mm", true).isValid();
                    }
                });
                $('#EditTask_Comment').summernote({
                    height: 100,                 // set editor height

                    minHeight: null,             // set minimum height of editor
                    maxHeight: null,             // set maximum height of editor

                    focus: true                 // set focus to editable area after initializing summernote
                });
                var table = $('#dt_timesheetForEditTask').DataTable({});
                $('[data-toggle="tooltip"]').tooltip();
            }
        }
    })
    //$("#Edittask").modal('show');
}

//function LoadDiscussionData() {
//    LoadDiscussionDetailData();
//    $("#DiscussionList").hide();
//    $("#DiscussionData").show();
//}

function GotoDiscussionList() {
    $("#DiscussionData").hide();
    $("#DiscussionList").show();
}

function OpenCommentBox(obj) {
    $("#" + obj).show();
    $("#textarea_" + obj).val('');
}

function CloseCommentBox(obj) {
    $("#textarea_" + obj).val('');
    $("#" + obj).hide();
}

function LoadDiscussionDetailData(DiscussionBoardID) {
    var postData = {
        AssignmentIDStr: $("#hiddenAssignmentID").val(),
        DiscussionBoardIDStr: DiscussionBoardID
    };
    ShowLoader();
    $.ajax({
        url: "/Pms/ProjectView/GetDiscussionDetailData",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(postData),
        success: function (response) {
            HideLoader();
            if (response == "_Logon_") {
                window.location.href = "/Login/Login";
            }
            else {
                $("#DiscussionList").hide();
                $("#DiscussionData").show();
                $("#DiscussionData").html(response);
            }
        }
    })
}

function CreateNewReferencePost(PostID, DiscussionBoardID) {
    var postData = {
        AssignmentIDStr: $("#hiddenAssignmentID").val(),
        PostIDStr: PostID,
        DiscussionBoardIDStr: DiscussionBoardID,
        Comment: $("#textarea_" + PostID).val()
    }
    ShowLoader();
    $.ajax({
        url: "/Pms/ProjectView/CreateReferencePosts",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(postData),
        success: function (response) {
            HideLoader();
            if (response == "_Logon_") {
                window.location.href = "/Login/Login";
            }
            else {
                $("#textarea_" + PostID).val('');
                $("#DiscussionList").hide();
                $("#DiscussionData").show();
                $("#DiscussionData").html(response);
            }
        }
    })
}

//function FrmSub(DiscussionBoardID) {
//    alert("hello");
//    var form = $('#data')[0];
//    var data = new FormData(form);
//    ShowLoader();
//    $.ajax({
//        type: "POST",
//        enctype: 'multipart/form-data',
//        url: "/Pms/ProjectView/AddFile",
//        data: data,
//        processData: false,
//        contentType: false,
//        cache: false,
//        timeout: 600000,
//        success: function (data) {
//        HideLoader();
//       if (data == "_Logon_") {
//            window.location.href = "/Login/Login";
//        }
//        else {
//            $("#result").text(data);
//            console.log("SUCCESS : ", data);
//            $("#btnSubmit").prop("disabled", false);
//        }


//        },
//        error: function (e) {

//            $("#result").text(e.responseText);
//            console.log("ERROR : ", e);
//            $("#btnSubmit").prop("disabled", false);

//        }
//    });
//}

function CreateNewPost(DiscussionBoardID) {
    var postData = {
        AssignmentIDStr: $("#hiddenAssignmentID").val(),
        DiscussionBoardIDStr: DiscussionBoardID,
        Comment: $("#Posts_Comment").val()
    }
    ShowLoader();
    $.ajax({
        url: "/Pms/ProjectView/CreateNewPost",
        type: "POST",
        contentType: "application/json; charset=utf-8",
        data: JSON.stringify(postData),
        success: function (response) {
            HideLoader();
            if (response == "_Logon_") {
                window.location.href = "/Login/Login";
            }
            else {
                $("#Posts_Comment").val('');
                $("#DiscussionList").hide();
                $("#DiscussionData").show();
                $("#DiscussionData").html(response);
            }
        }
    })
}

function EditTaskData(TaskID, AssignmentID, form) {
    var hiddenAssignmentID = $("#hiddenAssignmentID").val();
    ClearPopup(form);
    ShowLoader();
    $.ajax({
        url: "/Pms/ProjectView/GetTaskDataForEdit",
        type: "POST",
        data: { TaskIDStr: TaskID, AssignmentIDStr: AssignmentID },
        success: function (response) {
            HideLoader();
            if (response == "_Logon_") {
                window.location.href = "/Login/Login";
            }
            else {
                $("#taskPopup").html(response);
                $("#NewTaskForm").modal('show');
                $("#" + form).removeData('validator');
                $("#" + form).removeData('unobtrusiveValidation');
                $.validator.unobtrusive.parse("#" + form);
                $("#AssignmentIDForTask").val(hiddenAssignmentID);
                $("#hiddenTaskID").val(TaskID);
                $("#IsEdit").val("true");
                $('.bs-select').selectpicker('refresh');
                //$('.date-picker').datepicker({
                //    format: 'dd/mm/yyyy',
                //    autoclose: true,
                //    orientation: "bottom",
                //    todayHighlight: true
                //})
                $(function () {
                    var startDate = new Date('01/01/2018');
                    var FromEndDate = new Date();
                    var ToEndDate = new Date();

                    $('.start_date_tp').datepicker({
                        autoclose: true,
                        format: 'dd/mm/yyyy',
                        orientation: "bottom",
                        todayHighlight: true
                    })
                        .on('changeDate', function (selected) {
                            $('.end_date_tp').val("");
                            startDate = new Date(selected.date.valueOf());
                            startDate.setDate(startDate.getDate(new Date(selected.date.valueOf())));
                            $('.end_date_tp').datepicker('setStartDate', startDate);
                        });
                    $('.end_date_tp').datepicker({
                        format: 'dd/mm/yyyy',
                        autoclose: true,
                        orientation: "bottom",
                        todayHighlight: true
                    })
                });
                $(function () {
                    var startDate = new Date('01/01/2018');
                    var FromEndDate = new Date();
                    var ToEndDate = new Date();

                    $('.actual_start_date_tp').datepicker({
                        autoclose: true,
                        format: 'dd/mm/yyyy',
                        orientation: "bottom",
                        todayHighlight: true,
                        endDate: "0d"
                    })
                        .on('changeDate', function (selected) {
                            $('.actual_end_date_tp').val("");
                            startDate = new Date(selected.date.valueOf());
                            startDate.setDate(startDate.getDate(new Date(selected.date.valueOf())));
                            $('.actual_end_date_tp').datepicker('setStartDate', startDate);
                        });
                    $('.actual_end_date_tp').datepicker({
                        format: 'dd/mm/yyyy',
                        autoclose: true,
                        orientation: "bottom",
                        todayHighlight: true,
                        endDate: "0d"
                    })
                });

                $(".start_date_tp").on("change", function (evt, params) {
                    $(evt.target).valid();
                });
                $(".end_date_tp").on("change", function (evt, params) {
                    $(evt.target).valid();
                });
                $(".actual_start_date_tp").on("change", function (evt, params) {
                    $(evt.target).valid();
                });
                $(".actual_end_date_tp").on("change", function (evt, params) {
                    $(evt.target).valid();
                });
                $(function () {

                    $.validator.methods.date = function (value, element) {
                        return this.optional(element) || moment(value, "DD/MM/YYYY", true).isValid();
                    }
                });
                NotificationType = "";
                NotificationAssociationID = "";
            }
        }
    })
}

function EditTaskDataFromMilestoneList(TaskID, AssignmentID, form) {
    var hiddenAssignmentID = $("#hiddenAssignmentID").val();
    ClearPopup(form);
    ShowLoader();
    $.ajax({
        url: "/Pms/ProjectView/GetTaskDataForEditFromMilestoneList",
        type: "POST",
        data: { TaskIDStr: TaskID, AssignmentIDStr: AssignmentID },
        success: function (response) {
            HideLoader();
            if (response == "_Logon_") {
                window.location.href = "/Login/Login";
            }
            else {
                $("#MilestoneNewTaskForm").modal('show');
                $("#MiletaskData").html(response);
                $("#" + form).removeData('validator');
                $("#" + form).removeData('unobtrusiveValidation');
                $.validator.unobtrusive.parse("#" + form);
                $("#AssignmentIDForTaskMilestone").val(hiddenAssignmentID);
                $("#hiddenTaskIDFromMilestone").val(TaskID);
                $("#IsEditFormMilestone").val("true");
                $('.bs-select').selectpicker('refresh');
                $(function () {
                    var startDate = new Date('01/01/2018');
                    var FromEndDate = new Date();
                    var ToEndDate = new Date();

                    $('.start_date_frm_mil').datepicker({
                        autoclose: true,
                        format: 'dd/mm/yyyy',
                        orientation: "bottom",
                        todayHighlight: true
                    })
                        .on('changeDate', function (selected) {
                            $('.end_date_frm_mil').val("");
                            startDate = new Date(selected.date.valueOf());
                            startDate.setDate(startDate.getDate(new Date(selected.date.valueOf())));
                            $('.end_date_frm_mil').datepicker('setStartDate', startDate);
                        });
                    $('.end_date_frm_mil').datepicker({
                        format: 'dd/mm/yyyy',
                        autoclose: true,
                        orientation: "bottom",
                        todayHighlight: true
                    })
                });
                $(function () {
                    var startDate = new Date('01/01/2018');
                    var FromEndDate = new Date();
                    var ToEndDate = new Date();

                    $('.actual_start_date_frm_mil').datepicker({
                        autoclose: true,
                        format: 'dd/mm/yyyy',
                        orientation: "bottom",
                        todayHighlight: true,
                        endDate: "0d"
                    })
                        .on('changeDate', function (selected) {
                            $('.actual_end_date_frm_mil').val("");
                            startDate = new Date(selected.date.valueOf());
                            startDate.setDate(startDate.getDate(new Date(selected.date.valueOf())));
                            $('.actual_end_date_frm_mil').datepicker('setStartDate', startDate);
                        });
                    $('.actual_end_date_frm_mil').datepicker({
                        format: 'dd/mm/yyyy',
                        autoclose: true,
                        orientation: "bottom",
                        todayHighlight: true,
                        endDate: "0d"
                    })
                });

                $(".start_date_frm_mil").on("change", function (evt, params) {
                    $(evt.target).valid();
                });
                $(".end_date_frm_mil").on("change", function (evt, params) {
                    $(evt.target).valid();
                });
                $(".actual_start_date_frm_mil").on("change", function (evt, params) {
                    $(evt.target).valid();
                });
                $(".actual_end_date_frm_mil").on("change", function (evt, params) {
                    $(evt.target).valid();
                });
                $(function () {

                    $.validator.methods.date = function (value, element) {
                        return this.optional(element) || moment(value, "DD/MM/YYYY", true).isValid();
                    }
                });
                NotificationType = "";
                NotificationAssociationID = "";
            }
        }
    })
}

function EditSubTaskData(TaskID, AssignmentID, form, RefTaskID) {
    var hiddenAssignmentID = $("#hiddenAssignmentID").val();
    ClearPopup(form);
    ShowLoader();
    $.ajax({
        url: "/Pms/ProjectView/GetEditSubTaskData",
        type: "POST",
        data: { TaskIDStr: TaskID, AssignmentIDStr: AssignmentID },
        success: function (response) {
            HideLoader();
            if (response == "_Logon_") {
                window.location.href = "/Login/Login";
            }
            else {
                $("#SubTaskListForm").modal('hide');
                $("#SubtaskPopup").html(response);
                $("#NewSubTaskForm").modal('show');
                $("#" + form).removeData('validator');
                $("#" + form).removeData('unobtrusiveValidation');
                $.validator.unobtrusive.parse("#" + form);
                $("#AssignmentIDForSubTask").val(hiddenAssignmentID);
                $("#hiddenSubTaskID").val(TaskID);
                $("#hiddenRefTaskID").val(RefTaskID);
                if (MainTaskTitleOfSubTask != null && MainTaskTitleOfSubTask != '' && MainTaskTitleOfSubTask != undefined) {
                    $("#hiddentitleinedit").val(MainTaskTitleOfSubTask);
                } else {
                    $("#hiddentitleinedit").val($("#hiddentitle").val());
                }
                $("#IsEditForSubTask").val("true");
                $('.bs-select').selectpicker('refresh');
                //$('.date-picker').datepicker({
                //    format: 'dd/mm/yyyy',
                //    autoclose: true,
                //    orientation: "bottom",
                //    todayHighlight: true
                //})
                $(function () {
                    var startDate = new Date('01/01/2018');
                    var FromEndDate = new Date();
                    var ToEndDate = new Date();

                    $('.start_date_sub').datepicker({
                        autoclose: true,
                        format: 'dd/mm/yyyy',
                        orientation: "bottom",
                        todayHighlight: true
                    })
                        .on('changeDate', function (selected) {
                            $('.end_date_sub').val("");
                            startDate = new Date(selected.date.valueOf());
                            startDate.setDate(startDate.getDate(new Date(selected.date.valueOf())));
                            $('.end_date_sub').datepicker('setStartDate', startDate);
                        });
                    $('.end_date_sub').datepicker({
                        format: 'dd/mm/yyyy',
                        autoclose: true,
                        orientation: "bottom",
                        todayHighlight: true
                    })
                });
                $(function () {
                    var startDate = new Date('01/01/2018');
                    var FromEndDate = new Date();
                    var ToEndDate = new Date();

                    $('.actual_start_date_sub').datepicker({
                        autoclose: true,
                        format: 'dd/mm/yyyy',
                        orientation: "bottom",
                        todayHighlight: true,
                        endDate: "0d"
                    })
                        .on('changeDate', function (selected) {
                            $('.actual_end_date_sub').val("");
                            startDate = new Date(selected.date.valueOf());
                            startDate.setDate(startDate.getDate(new Date(selected.date.valueOf())));
                            $('.actual_end_date_sub').datepicker('setStartDate', startDate);
                        });
                    $('.actual_end_date_sub').datepicker({
                        format: 'dd/mm/yyyy',
                        autoclose: true,
                        orientation: "bottom",
                        todayHighlight: true,
                        endDate: "0d"
                    })
                });

                $(".start_date_sub").on("change", function (evt, params) {
                    $(evt.target).valid();
                });
                $(".end_date_sub").on("change", function (evt, params) {
                    $(evt.target).valid();
                });
                $(".actual_start_date_sub").on("change", function (evt, params) {
                    $(evt.target).valid();
                });
                $(".actual_end_date_sub").on("change", function (evt, params) {
                    $(evt.target).valid();
                });
                $(function () {

                    $.validator.methods.date = function (value, element) {
                        return this.optional(element) || moment(value, "DD/MM/YYYY", true).isValid();
                    }
                });
            }
        }
    })
}

function EditAreaTaskData(TaskID, AssignmentID, form, RefAssignmentAreaID) {
    var hiddenAssignmentID = $("#hiddenAssignmentID").val();
    ClearPopup(form);
    ShowLoader();
    $.ajax({
        url: "/Pms/ProjectView/GetEditAreaTaskData",
        type: "POST",
        data: { TaskIDStr: TaskID, AssignmentIDStr: AssignmentID },
        success: function (response) {
            HideLoader();
            if (response == "_Logon_") {
                window.location.href = "/Login/Login";
            }
            else {
                $("#AreaTaskListForm").modal('hide');
                $("#AreataskPopup").html(response);
                $("#NewAreaTaskForm").modal('show');
                $("#" + form).removeData('validator');
                $("#" + form).removeData('unobtrusiveValidation');
                $.validator.unobtrusive.parse("#" + form);
                $("#AssignmentIDForAreaTask").val(hiddenAssignmentID);
                $("#hiddenAreaTaskID").val(TaskID);
                $("#hiddenAssignmentAreaID").val(RefAssignmentAreaID);
                $("#hiddenAreatitleinedit").val($("#hiddenAreatitle").val());
                $("#IsEditForAreaTask").val("true");
                $('.bs-select').selectpicker('refresh');
                //$('.date-picker').datepicker({
                //    format: 'dd/mm/yyyy',
                //    autoclose: true,
                //    orientation: "bottom",
                //    todayHighlight: true
                //})
                $(function () {
                    var startDate = new Date('01/01/2018');
                    var FromEndDate = new Date();
                    var ToEndDate = new Date();

                    $('.start_date_Atp').datepicker({
                        autoclose: true,
                        format: 'dd/mm/yyyy',
                        orientation: "bottom",
                        todayHighlight: true
                    })
                        .on('changeDate', function (selected) {
                            $('.end_date_Atp').val("");
                            startDate = new Date(selected.date.valueOf());
                            startDate.setDate(startDate.getDate(new Date(selected.date.valueOf())));
                            $('.end_date_Atp').datepicker('setStartDate', startDate);
                        });
                    $('.end_date_Atp').datepicker({
                        format: 'dd/mm/yyyy',
                        autoclose: true,
                        orientation: "bottom",
                        todayHighlight: true
                    })
                });
                $(function () {
                    var startDate = new Date('01/01/2018');
                    var FromEndDate = new Date();
                    var ToEndDate = new Date();

                    $('.actual_start_date_Atp').datepicker({
                        autoclose: true,
                        format: 'dd/mm/yyyy',
                        orientation: "bottom",
                        todayHighlight: true,
                        endDate: "0d"
                    })
                        .on('changeDate', function (selected) {
                            $('.actual_end_date_Atp').val("");
                            startDate = new Date(selected.date.valueOf());
                            startDate.setDate(startDate.getDate(new Date(selected.date.valueOf())));
                            $('.actual_end_date_Atp').datepicker('setStartDate', startDate);
                        });
                    $('.actual_end_date_Atp').datepicker({
                        format: 'dd/mm/yyyy',
                        autoclose: true,
                        orientation: "bottom",
                        todayHighlight: true,
                        endDate: "0d"
                    })
                });

                $(".start_date_Atp").on("change", function (evt, params) {
                    $(evt.target).valid();
                });
                $(".end_date_Atp").on("change", function (evt, params) {
                    $(evt.target).valid();
                });
                $(".actual_start_date_Atp").on("change", function (evt, params) {
                    $(evt.target).valid();
                });
                $(".actual_end_date_Atp").on("change", function (evt, params) {
                    $(evt.target).valid();
                });
                $(function () {

                    $.validator.methods.date = function (value, element) {
                        return this.optional(element) || moment(value, "DD/MM/YYYY", true).isValid();
                    }
                });
            }
        }
    })
}

function AddSubTaskData(EncrytedTaskID, AssignmentID, TaskID, form) {
    var hiddenAssignmentID = $("#hiddenAssignmentID").val();
    $("#IsEditForSubTask").val("false");
    ClearPopup(form);
    ShowLoader();
    $.ajax({
        url: "/Pms/ProjectView/GetAddSubTaskData",
        type: "POST",
        data: { TaskIDStr: EncrytedTaskID, AssignmentIDStr: AssignmentID },
        success: function (response) {
            HideLoader();
            if (response == "_Logon_") {
                window.location.href = "/Login/Login";
            }
            else {
                $("#SubtaskPopup").html(response);
                $("#NewSubTaskForm").modal('show');
                $("#" + form).removeData('validator');
                $("#" + form).removeData('unobtrusiveValidation');
                $.validator.unobtrusive.parse("#" + form);
                $("#AssignmentIDForSubTask").val(hiddenAssignmentID);
                $('.bs-select').selectpicker('refresh');
                //$('.date-picker').datepicker({
                //    format: 'dd/mm/yyyy',
                //    autoclose: true,
                //    orientation: "bottom",
                //    todayHighlight: true
                //})
                $(function () {
                    var startDate = new Date('01/01/2018');
                    var FromEndDate = new Date();
                    var ToEndDate = new Date();

                    $('.start_date_sub').datepicker({
                        autoclose: true,
                        format: 'dd/mm/yyyy',
                        orientation: "bottom",
                        todayHighlight: true
                    })
                        .on('changeDate', function (selected) {
                            $('.end_date_sub').val("");
                            startDate = new Date(selected.date.valueOf());
                            startDate.setDate(startDate.getDate(new Date(selected.date.valueOf())));
                            $('.end_date_sub').datepicker('setStartDate', startDate);
                        });
                    $('.end_date_sub').datepicker({
                        format: 'dd/mm/yyyy',
                        autoclose: true,
                        orientation: "bottom",
                        todayHighlight: true
                    })
                });
                $(function () {
                    var startDate = new Date('01/01/2018');
                    var FromEndDate = new Date();
                    var ToEndDate = new Date();

                    $('.actual_start_date_sub').datepicker({
                        autoclose: true,
                        format: 'dd/mm/yyyy',
                        orientation: "bottom",
                        todayHighlight: true,
                        endDate: "0d"
                    })
                        .on('changeDate', function (selected) {
                            $('.actual_end_date_sub').val("");
                            startDate = new Date(selected.date.valueOf());
                            startDate.setDate(startDate.getDate(new Date(selected.date.valueOf())));
                            $('.actual_end_date_sub').datepicker('setStartDate', startDate);
                        });
                    $('.actual_end_date_sub').datepicker({
                        format: 'dd/mm/yyyy',
                        autoclose: true,
                        orientation: "bottom",
                        todayHighlight: true,
                        endDate: "0d"
                    })
                });

                $(".start_date_sub").on("change", function (evt, params) {
                    $(evt.target).valid();
                });
                $(".end_date_sub").on("change", function (evt, params) {
                    $(evt.target).valid();
                });
                $(".actual_start_date_sub").on("change", function (evt, params) {
                    $(evt.target).valid();
                });
                $(".actual_end_date_sub").on("change", function (evt, params) {
                    $(evt.target).valid();
                });
                $(function () {

                    $.validator.methods.date = function (value, element) {
                        return this.optional(element) || moment(value, "DD/MM/YYYY", true).isValid();
                    }
                });
            }
        }
    })
}

//function AddAreaTaskData(EncryptedAssignmentAreaID, AssignmentID, AssignmentAreaID, form) {
//    var hiddenAssignmentID = $("#hiddenAssignmentID").val();
//    $("#IsEditForAreaTask").val("false");
//    ClearPopup(form);
//    ShowLoader();
//    $.ajax({
//        url: "/Pms/ProjectView/GetAddAreaTaskData",
//        type: "POST",
//        data: { AssignmentAreaIDStr: EncryptedAssignmentAreaID, AssignmentIDStr: AssignmentID },
//        success: function (response) {
//            HideLoader();
//            if (response == "_Logon_") {
//                window.location.href = "/Login/Login";
//            }
//            else {
//                $("#AreataskPopup").html(response);
//                $("#NewAreaTaskForm").modal('show');
//                $("#" + form).removeData('validator');
//                $("#" + form).removeData('unobtrusiveValidation');
//                $.validator.unobtrusive.parse("#" + form);
//                $("#AssignmentIDForAreaTask").val(hiddenAssignmentID);
//                $('.bs-select').selectpicker('refresh');
//                $('.date-picker').datepicker({
//                    format: 'dd/mm/yyyy',
//                    autoclose: true,
//                    orientation: "bottom",
//                    todayHighlight: true
//                })
//                $(".date-picker").on("change", function (evt, params) {
//                    $(evt.target).valid();
//                });
//            }
//        }
//    })
//}

function EditAreaData(AssignmentAreaID, AssignmentID, form) {
    var hiddenAssignmentID = $("#hiddenAssignmentID").val();
    ClearPopup(form);
    ShowLoader();
    $.ajax({
        url: "/Pms/ProjectView/GetAreaDataForEdit",
        type: "POST",
        data: { AssignmentAreaIDStr: AssignmentAreaID, AssignmentIDStr: AssignmentID },
        success: function (response) {
            HideLoader();
            if (response == "_Logon_") {
                window.location.href = "/Login/Login";
            }
            else {
                $("#LoadAreaData").html(response);
                $("#AddNewAreaForm").modal('show');
                $("#" + form).removeData('validator');
                $("#" + form).removeData('unobtrusiveValidation');
                $.validator.unobtrusive.parse("#" + form);
                $("#AssignmentIDForArea").val(hiddenAssignmentID);
                $("#hiddenAssignmentAreaID").val(AssignmentAreaID);
                $("#IsAreaEdit").val("true");
                $('.bs-select').selectpicker('refresh');
                $('.date-picker').datepicker({
                    format: 'dd/mm/yyyy',
                    autoclose: true,
                    orientation: "bottom",
                    todayHighlight: true
                })
                $(".date-picker").on("change", function (evt, params) {
                    $(evt.target).valid();
                });
                $(function () {

                    $.validator.methods.date = function (value, element) {
                        return this.optional(element) || moment(value, "DD/MM/YYYY", true).isValid();
                    }
                });
                $("#Areatab1").click(function () {

                    $("#area_tab1").fadeIn();
                    $("#area_tab1").addClass("active");
                    $("#area_tab2").removeClass("active");
                    $(this).closest("ul").children("li").removeClass("active");
                    $(this).closest("li").addClass("active");
                    $("#area_tab2").hide();
                });

                $("#Areatab2").click(function () {
                    $("#area_tab2").fadeIn();
                    $("#area_tab2").addClass("active");
                    $("#area_tab1").removeClass("active");
                    $(this).closest("ul").children("li").removeClass("active");
                    $(this).closest("li").addClass("active");
                    $("#area_tab1").hide();
                });

                $('#form_area_document_dropzone').dropzone({
                    url: "#",
                    acceptedFiles: ".jpeg,.jpg,.png,.gif,.pdf,.txt,.mp3,.mp4,.doc,.xls,.xlsx,.docx",
                    maxFilesize: 6,
                    paramName: "file",
                    maxThumbnailFilesize: 50,
                    url: "/Pms/ProjectView/DropzoneFileuploadFromArea",
                    addRemoveLinks: true,
                    init: function () {
                        var thisDropzone = this;
                        dropzone = this;
                        $.getJSON("/Pms/ProjectView/GetAttachmentsForAreaFiles/").done(function (data) {

                            if (data.Data != '') {
                                $.each(data.Data, function (index, item) {
                                    //// Create the mock file:
                                    var mockFile = {
                                        name: item.FileName,
                                        size: 2234566
                                    };
                                    // Call the default addedfile event handler
                                    thisDropzone.emit("addedfile", mockFile);
                                    $(mockFile.previewElement).attr("temp-data", item.AttachementID);

                                    var a = document.createElement('a');
                                    a.setAttribute('href', "/Areas/Pms/Documents/" + item.FilePath);
                                    a.setAttribute('target', '_blank');
                                    a.setAttribute('Class', 'DownloadLink');
                                    a.innerHTML = "<i class=\"fa fa-download\"></i>";
                                    mockFile.previewTemplate.appendChild(a);



                                    //var divele = document.createElement("div");


                                    //var textbx = document.createElement("input");
                                    //textbx.setAttribute("type", "text");
                                    //textbx.setAttribute("value", "");
                                    //textbx.setAttribute("id", "FileUploadId");
                                    //textbx.setAttribute("style", "width:99px;margin-top:5px;");
                                    //textbx.setAttribute("placeholder", "File Name");
                                    //divele.appendChild(textbx);
                                    //mockFile.previewTemplate.appendChild(divele);

                                });
                            }
                        });
                        this.on('sending', function (file, xhr, formData) {
                            formData.append('AssignAreaID', $("#hiddenAssignmentAreaID").val());
                            formData.append('ClientID', '');

                        });
                        this.on('success', function (file, json) {
                            $(file.previewElement).attr("temp-data", json.DI);

                            var a = document.createElement('a');
                            a.setAttribute('href', "/Areas/Pms/Documents" + "/" + json.path);
                            a.setAttribute('target', '_blank');
                            a.setAttribute('Class', 'DownloadLink');
                            a.innerHTML = "<i class=\"fa fa-download\"></i>";
                            file.previewTemplate.appendChild(a);

                            //var divele = document.createElement("div");


                            //var textbx = document.createElement("input");
                            //textbx.setAttribute("type", "text");
                            //textbx.setAttribute("id", "FileUploadId");
                            //textbx.setAttribute("value", "");
                            //textbx.setAttribute("style", "width:99px;margin-top:5px;");
                            //textbx.setAttribute("placeholder", "File Name");
                            //divele.appendChild(textbx);
                            //file.previewTemplate.appendChild(divele);
                        });
                        this.on('error', function (file, json) {
                            if (file.size > 6291456) {
                                thisDropzone.removeFile(file);
                                ShowMessage("File is too big.Max filesize:6MB.", "error");
                            }
                        });
                        this.on('removedfile', function (file) {
                            var id = $(file.previewElement).attr("temp-data");
                            ShowLoader();
                            $.ajax({
                                url: "/Pms/Projectview/RemoveAreasFiles?data=" + id,
                                type: "POST",
                                processData: false,
                                contentType: false,
                                success: function (response) {
                                    HideLoader();
                                    if (response == "_Logon_") {
                                        window.location.href = "/Login/Login";
                                    }
                                    else {
                                        //RemoveProcessBar();
                                    }
                                }
                            });
                        });
                        this.on("complete", function (file) {
                            if (this.getUploadingFiles().length === 0 && this.getQueuedFiles().length === 0) {
                                IsAllFileUploaded = true;
                            }
                        });
                    }
                });
            }
        }
    })
}

function EditTimesheetData(TimesheetID, AssignmentID, form) {

    var hiddenAssignmentID = $("#hiddenAssignmentID").val();
    ClearPopup(form);
    ShowLoader();
    $.ajax({
        url: "/Pms/ProjectView/GetTimesheetDataForEdit",
        type: "POST",
        data: { TimesheetIDStr: TimesheetID, AssignmentIDStr: AssignmentID },
        success: function (response) {
            HideLoader();
            if (response == "_Logon_") {
                window.location.href = "/Login/Login";
            }
            else {
                $("#TimeSheetPopup").html(response);
                $("#AddnewTimesheetForm").modal('show');
                $("#" + form).removeData('validator');
                $("#" + form).removeData('unobtrusiveValidation');
                $.validator.unobtrusive.parse("#" + form);
                $("#AssignmentIDForTimeSheet").val(hiddenAssignmentID);
                $("#hiddenTimeSheetID").val(TimesheetID);
                $("#IsTimesheetEdit").val("true");
                $('.bs-select').selectpicker('refresh');
                var TodayEndDate = moment(new Date()).format("DD/MM/YYYY 23:59");
                $('.start_date_tm').datetimepicker({
                    autoclose: true,
                    format: "dd/mm/yyyy hh:ii",
                    fontAwesome: true,
                    showMeridian: true,
                    pickerPosition: "bottom-right",
                    endDate: TodayEndDate
                })
                        .on('changeDate', function (selected) {
                            $('.end_date_tm').val("");
                            $('.end_date_tm').removeAttr("disabled");
                            startDate = new Date(selected.date.valueOf());
                            startDate.setDate(startDate.getDate(new Date(selected.date.valueOf())));
                            $('.end_date_tm').datetimepicker('setStartDate', startDate);
                        });
                $('.end_date_tm').datetimepicker({
                    autoclose: true,
                    format: "dd/mm/yyyy hh:ii",
                    fontAwesome: true,
                    showMeridian: true,
                    pickerPosition: "bottom-right",
                    endDate: TodayEndDate
                })


                $(".start_date_tm").on("change", function (evt, params) {
                    $(evt.target).valid();
                });

                $(".end_date_tm").on("change", function (evt, params) {
                    $(evt.target).valid();
                });

                $(function () {
                    $.validator.methods.date = function (value, element) {
                        return this.optional(element) || moment(value, "DD/MM/YYYY HH:mm", true).isValid();
                    }
                });

                //$(".form_datetime").datetimepicker({
                //    autoclose: true,
                //    format: "dd/mm/yyyy hh:ii",
                //    fontAwesome: true,
                //    pickerPosition: "bottom-right"
                //});

                //$(".form_datetime").on("change", function (evt, params) {
                //    $(evt.target).valid();
                //});



                //$(function () {
                //    $.validator.methods.date = function (value, element) {
                //        return this.optional(element) || moment(value, "DD/MM/YYYY HH:mm", true).isValid();
                //    }
                //});

                $('#Timesheet_Note').summernote({
                    height: 100,                 // set editor height

                    minHeight: null,             // set minimum height of editor
                    maxHeight: null,             // set maximum height of editor

                    focus: true                 // set focus to editable area after initializing summernote
                });
            }
        }
    })
}

function EditDiscussionData(DiscussionBoardID, AssignmentID, form) {
    var hiddenAssignmentID = $("#hiddenAssignmentID").val();
    ClearPopup(form);
    ShowLoader();
    $.ajax({
        url: "/Pms/ProjectView/GetDiscussionDataForEdit",
        type: "POST",
        data: { DiscussionBoardIDStr: DiscussionBoardID, AssignmentIDStr: AssignmentID },
        success: function (response) {
            HideLoader();
            if (response == "_Logon_") {
                window.location.href = "/Login/Login";
            }
            else {
                $("#DiscussionPopup").html(response);
                $("#AddNewDiscussionForm").modal('show');
                $("#" + form).removeData('validator');
                $("#" + form).removeData('unobtrusiveValidation');
                $.validator.unobtrusive.parse("#" + form);
                $("#AssignmentIDForDiscussion").val(hiddenAssignmentID);
                $("#hiddenDiscussionID").val(DiscussionBoardID);
                $("#DiscussionIsEdit").val("true");
                $('.bs-select').selectpicker('refresh');
                $('.date-picker').datepicker({
                    format: 'dd/mm/yyyy',
                    autoclose: true,
                    orientation: "bottom",
                    todayHighlight: true
                })
                $(".date-picker").on("change", function (evt, params) {
                    $(evt.target).valid();
                });
                $(function () {

                    $.validator.methods.date = function (value, element) {
                        return this.optional(element) || moment(value, "DD/MM/YYYY", true).isValid();
                    }
                });
            }
        }
    })
}

function EditMilestoneData(MilestoneID, AssignmentID, form) {
    var hiddenAssignmentID = $("#hiddenAssignmentID").val();
    ClearPopup(form);

    ShowLoader();
    $.ajax({
        url: "/Pms/ProjectView/GetMilestoneDataForEdit",
        type: "POST",
        data: { MilestoneIDStr: MilestoneID, AssignmentIDStr: AssignmentID },
        success: function (response) {
            HideLoader();
            if (response == "_Logon_") {
                window.location.href = "/Login/Login";
            }
            else {
                $("#milestonepopup").html(response);
                $("#NewMilestoneForm").modal('show');
                $("#" + form).removeData('validator');
                $("#" + form).removeData('unobtrusiveValidation');
                $.validator.unobtrusive.parse("#" + form);
                $("#AssignmentIDForMilestone").val(hiddenAssignmentID);
                $("#hiddenMilestoneID").val(MilestoneID);
                $("#MilestoneIsEdit").val("true");
                $('.bs-select').selectpicker('refresh');
                //$('.date-picker').datepicker({
                //    format: 'dd/mm/yyyy',
                //    autoclose: true,
                //    orientation: "bottom",
                //    todayHighlight: true
                //})
                $(function () {
                    var startDate = new Date('01/01/2018');
                    var FromEndDate = new Date();
                    var ToEndDate = new Date();

                    $('.start_date_mil').datepicker({
                        autoclose: true,
                        format: 'dd/mm/yyyy',
                        orientation: "bottom",
                        todayHighlight: true
                    })
                        .on('changeDate', function (selected) {
                            $('.end_date_mil').val("");
                            startDate = new Date(selected.date.valueOf());
                            startDate.setDate(startDate.getDate(new Date(selected.date.valueOf())));
                            $('.end_date_mil').datepicker('setStartDate', startDate);
                        });
                    $('.end_date_mil').datepicker({
                        format: 'dd/mm/yyyy',
                        autoclose: true,
                        orientation: "bottom",
                        todayHighlight: true
                    })
                });
                $(".start_date_mil").on("change", function (evt, params) {
                    $(evt.target).valid();
                });
                $(".end_date_mil").on("change", function (evt, params) {
                    $(evt.target).valid();
                });
                $(function () {

                    $.validator.methods.date = function (value, element) {
                        return this.optional(element) || moment(value, "DD/MM/YYYY", true).isValid();
                    }
                });
            }
        }
    })
}

function SelectPriorityVal(obj) {
    var SelectedVal = $(obj).text();
    $("#PrioritySelectedTerm").text(SelectedVal);
}

function SelectAreaPriorityVal(obj) {
    var SelectedVal = $(obj).text();
    $("#AreaPrioritySelectedTerm").text(SelectedVal);
}

function SelectStatusVal(obj) {
    var SelectedVal = $(obj).text();
    $("#StatusSelectedTerm").text(SelectedVal);
}

function SelectAreaStatusVal(obj) {
    var SelectedVal = $(obj).text();
    $("#StatusSelectedTermArea").text(SelectedVal);
}

function fnOpenDiscussionDeleteModel(DeleteButtonID, DeleteModalID, DeleteIDToset) {
    $("#" + DeleteButtonID).attr("DelID", DeleteIDToset);
    var hiddenAssignmentID = $("#hiddenAssignmentID").val();
    $("#" + DeleteButtonID).attr("AssignmentID", hiddenAssignmentID);
    $("#" + DeleteModalID).modal('show');
}

function DeleteDiscussionEntity(urlTopass, DeleteButtonID, DeleteModalID, EntityNameToDelete, tableidTopass, Redirectionurl) {
    var trId = $("#" + DeleteButtonID).attr("DelID");
    var postData = {
        DeleteID: $("#" + DeleteButtonID).attr("DelID"),
        AssignmentIDStr: $("#" + DeleteButtonID).attr("AssignmentID")
    };
    ShowLoader();
    AjaxCall(urlTopass, JSON.stringify(postData), "post", "json", "application/json", true).done(function (data) {
        HideLoader();
        if (data == "_Logon_") {
            window.location.href = "/Login/Login";
        }
        else {
            $("#" + DeleteModalID).modal('hide');
            if (data == 'error') {
                toastr["error"]("Oops Something Wentwrong !!!", EntityNameToDelete, { "positionClass": "toast-bottom-right", "progressBar": true });
            }
            else {
                if (data.length != 0) {
                    $("#DiscussionByProjectID").html(data);
                    var table = $('#dt_Discussion').DataTable({});
                    toastr["success"](EntityNameToDelete + " deleted successfully.", EntityNameToDelete, { "positionClass": "toast-bottom-right", "progressBar": true });
                    //window.location.href = Redirectionurl;
                }
            }
        }
    });

}

function AddFileTaskData(EncrytedTaskID, AssignmentID, TaskID, form) {
    var hiddenAssignmentID = $("#hiddenAssignmentID").val();
    $("#IsEditForSubTask").val("false");
    ClearPopup(form);
    ShowLoader();
    $.ajax({
        url: "/Pms/ProjectView/AddTaskFiles",
        type: "POST",
        data: { TaskIDStr: EncrytedTaskID, AssignmentIDStr: AssignmentID },
        success: function (response) {
            HideLoader();
            if (response == "_Logon_") {
                window.location.href = "/Login/Login";
            }
            else {
                $("#LoadFileData").html(response);
                $("#AddNewFileForm").modal('show');
            }
        }
    })
}

function ProjectTableIndexOrdering() {
    var table = $("#dt_taskFile tbody");
    table.find('tr').each(function (i) {

        $(this).find("input[type='hidden']:eq(0)").attr("id", "projectdocumentlist_" + i + "__AttachementID");
        $(this).find("input[type='hidden']:eq(0)").attr("name", "projectdocumentlist[" + i + "].AttachementID");

        $(this).find("input[type='hidden']:eq(1)").attr("id", "projectdocumentlist_" + i + "__DocumentID");
        $(this).find("input[type='hidden']:eq(1)").attr("name", "projectdocumentlist[" + i + "].DocumentID");

        $(this).find("input[type='hidden']:eq(2)").attr("id", "projectdocumentlist_" + i + "__Term");
        $(this).find("input[type='hidden']:eq(2)").attr("name", "projectdocumentlist[" + i + "].Term");

        $(this).find("input[type='hidden']:eq(3)").attr("id", "projectdocumentlist_" + i + "__ProjectTermID");
        $(this).find("input[type='hidden']:eq(3)").attr("name", "projectdocumentlist[" + i + "].ProjectTermID");

        $(this).find("input[type='hidden']:eq(4)").attr("id", "projectdocumentlist_" + i + "__FilePath");
        $(this).find("input[type='hidden']:eq(4)").attr("name", "projectdocumentlist[" + i + "].FilePath");

        $(this).find("input[type='hidden']:eq(5)").attr("id", "projectdocumentlist_" + i + "__FileName");
        $(this).find("input[type='hidden']:eq(5)").attr("name", "projectdocumentlist[" + i + "].FileName");

        $(this).find("input[type='hidden']:eq(6)").attr("id", "projectdocumentlist_" + i + "__FileSize");
        $(this).find("input[type='hidden']:eq(6)").attr("name", "projectdocumentlist[" + i + "].FileSize");

        $(this).find("input[type='hidden']:eq(7)").attr("id", "projectdocumentlist_" + i + "__ExtensionType_Term");
        $(this).find("input[type='hidden']:eq(7)").attr("name", "projectdocumentlist[" + i + "].ExtensionType_Term");

        $(this).find("input[type='hidden']:eq(8)").attr("id", "projectdocumentlist_" + i + "__DisplayName");
        $(this).find("input[type='hidden']:eq(8)").attr("name", "projectdocumentlist[" + i + "].DisplayName");

        $(this).find("input[type='hidden']:eq(9)").attr("id", "projectdocumentlist_" + i + "__CustomTerm");
        $(this).find("input[type='hidden']:eq(9)").attr("name", "activitydocumentlist[" + i + "].CustomTerm");

        $(this).find('td:eq(0)').find('select').attr("id", "projectdocumentlist_" + i + "__DocType_Term");
        $(this).find('td:eq(0)').find('select').attr("name", "projectdocumentlist[" + i + "].DocType_Term");

        $(this).find('td:eq(1)').find('input[class="isnew"]').attr("id", "projectdocumentlist_" + i + "__IsNew");
        $(this).find('td:eq(1)').find('input[class="isnew"]').attr("name", "projectdocumentlist[" + i + "].IsNew");

        $(this).find('td:eq(2)').find('input[versionno]').attr("id", "projectdocumentlist_" + i + "__VersionNo");
        $(this).find('td:eq(2)').find('input[versionno]').attr("name", "projectdocumentlist[" + i + "].VersionNo");


        $(this).find('td:eq(0)').find('select').attr("id", "projectdocumentlist_" + i + "__DocType_Term").removeAttr("disabled");

        $(this).find('td:eq(2)').find('input[versionno]').attr("id", "projectdocumentlist_" + i + "__VersionNo").removeAttr("disabled");

    });
}

function SearchFileListByFilter() {
    var AreaID = $("#File_AreaID").val();
    var MilestoneID = $("#File_MilestoneID").val();
    var TaskID = $("#File_TaskID").val();
    var hiddenAssignmentID = $("#hiddenAssignmentID").val();

    ShowLoader();
    $.ajax({
        url: "/Pms/ProjectView/GetFileListByFilter",
        type: "POST",
        data: { AreaIDStr: AreaID, MilestoneIDStr: MilestoneID, TaskIDStr: TaskID, AssignmentIDStr: hiddenAssignmentID },
        success: function (response) {
            HideLoader();
            if (response == "_Logon_") {
                window.location.href = "/Login/Login";
            }
            else {
                $("#FileDocumentList").html('');
                $("#FileDocumentList").html(response);
                var table = $('#tblassignmentfileDocument').DataTable({});
            }
        }
    })
}

function SearchTaskListByFilter() {
    var AreaID = AreaFilterNameForMilestoneList;
    var MilestoneID = $("#TaskList_Filter_MilestoneID").val();
    var hiddenAssignmentID = $("#hiddenAssignmentID").val();
    var StartDate = $("#TaskList_StartDate").val();
    var EndDate = $("#TaskList_EndDate").val();

    ShowLoader();
    $.ajax({
        url: "/Pms/ProjectView/GetTaskListByFilter",
        type: "POST",
        data: { AreaIDStr: AreaID, MilestoneIDStr: MilestoneID, AssignmentIDStr: hiddenAssignmentID, StartDate: StartDate, EndDate: EndDate },
        success: function (response) {
            HideLoader();
            if (response == "_Logon_") {
                window.location.href = "/Login/Login";
            }
            else {
                $("#TaskListByProjectID").html('');
                $("#TaskListByProjectID").html(response);
                var table = $('#dt_task').DataTable({});
                $('[data-toggle="tooltip"]').tooltip();
            }
        }
    })
}

function GetAreaWiseMilestoneListForMilsetoneTab(obj) {

    var AreaID = $("#MilestoneList_Filter_AreaID").val();
    var hiddenAssignmentID = $("#hiddenAssignmentID").val();
    var SelectedAreaText = $("#MilestoneList_Filter_AreaID option:selected").text();

    ShowLoader();
    $.ajax({
        url: "/Pms/ProjectView/GetAreaWiseMilestoneListForMilsetoneTab",
        type: "POST",
        data: { AreaIDStr: AreaID, AssignmentIDStr: hiddenAssignmentID },
        success: function (response) {
            HideLoader();
            if (response == "_Logon_") {
                window.location.href = "/Login/Login";
            }
            else {
                $("#MileastoneListByProjectID").html('');
                $("#MileastoneListByProjectID").html(response);
                $('[data-toggle="tooltip"]').tooltip();
                $('.scroller').slimScroll({
                    allowPageScroll: true, // allow page scroll when the element scroll is ended
                    size: '7px',
                    color: ($(this).attr("data-handle-color") ? $(this).attr("data-handle-color") : '#bbb'),
                    wrapperClass: ($(this).attr("data-wrapper-class") ? $(this).attr("data-wrapper-class") : 'slimScrollDiv'),
                    railColor: ($(this).attr("data-rail-color") ? $(this).attr("data-rail-color") : '#eaeaea'),
                    position: 'right',
                    height: '345px',
                    alwaysVisible: ($(this).attr("data-always-visible") == "1" ? true : false),
                    railVisible: ($(this).attr("data-rail-visible") == "1" ? true : false),
                    disableFadeOut: true
                });
                if (SelectedAreaText == "All") {
                    AreaFilterNameForMilestoneList = SelectedAreaText;
                } else {
                    AreaFilterNameForMilestoneList = AreaID.toString().toUpperCase();
                }
                $("#MilestoneList_Filter_AreaID option[value='" + AreaFilterNameForMilestoneList + "']").attr('selected', 'selected');
                $('.bs-select').selectpicker('refresh');
            }
        }
    })
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

function fnAddNewAreaClosePopup() {
    ShowLoader();
    $.ajax({
        url: "/Pms/ProjectView/ClearAttachments",
        type: "POST",
        data: {},
        success: function (response) {
            HideLoader();
            if (response == "_Logon_") {
                window.location.href = "/Login/Login";
            }
            else {
                $('#AddNewAreaForm').modal('hide');
            }
        }
    });
}

function CheckAreaIsOutOfProposal(obj) {
    var AreaID = $(obj).val();
    var HiddenAssignmentID = $("#AssignmentIDForArea").val();

    if (AreaID != '' && HiddenAssignmentID != '') {
        ShowLoader();
        $.ajax({
            url: "/Pms/ProjectView/CheckAreaIsOutOfProposal",
            type: "POST",
            data: { AreaIDStr: AreaID, HiddenAssignmentIDStr: HiddenAssignmentID },
            success: function (response) {
                HideLoader();
                if (response == "_Logon_") {
                    window.location.href = "/Login/Login";
                }
                else {
                    if (response) {
                        $("#AssignmentAreas_IsInEstimation").val('true');
                        $("#RemarksRow").addClass('hide');
                        $("#AssignmentAreas_NotAvailabe_In_Estimation_Remarks").removeAttr('required');
                    } else {
                        $("#AssignmentAreas_IsInEstimation").val('false');
                        $("#RemarksRow").removeClass('hide');
                        $("#AssignmentAreas_NotAvailabe_In_Estimation_Remarks").attr('required', 'required');
                    }
                }
            }
        })
    } else {
        $("#AssignmentAreas_IsInEstimation").val('false');
        $("#RemarksRow").addClass('hide');
        $("#AssignmentAreas_NotAvailabe_In_Estimation_Remarks").removeAttr('required');
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

function SetMaxVersionToNew(DocTypeName, DoDecrement) {
    debugger;
    for (var i = 0; i < CALBYMEVersion.length; i++) {
        if (CALBYMEVersion[i].DocType === DocTypeName) {
            if (DoDecrement) {
                CALBYMEVersion[i].MaxVersion = parseInt(CALBYMEVersion[i].MaxVersion) - 1;
            }
            else {
                CALBYMEVersion[i].MaxVersion = parseInt(CALBYMEVersion[i].MaxVersion) + 1;
            }
            return;
        }
    }
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

function TaskFileFormSubmit() {
    ActivityTableIndexOrdering();
    ShowLoader();
    $("#form_TaskFile").submit();
}

function ProjectFileFormSubmit() {
    ActivityTableIndexOrdering();
    if (!($("#VALmassage_0").text() == "File size exceeds 5 MB!" || $("#VALmassage_1").text() == "File size exceeds 5 MB!" || $("#VALmassage_2").text() == "File size exceeds 5 MB!" || $("#VALmassage_3").text() == "File size exceeds 5 MB!" || $("#VALmassage_4").text() == "File size exceeds 5 MB!")) {
        if ($("#form_ProjectFile").valid()) {
            ShowLoader();
            $("#form_ProjectFile").submit();
        }
    }
}

function DeleteActivityRowData(obj) {
    var version = $(obj).closest('tr').find('td:eq(3)').find('input[versionno]').val();
    var docTypeoption = $(obj).closest('tr').find('td:eq(0)').find("select").val();
    var table = $("#dt_taskFile tbody");
    var regex = /Hot/;
    $('#dt_taskFile').find('tbody').find('select option[value="' + docTypeoption + '"]:selected').each(function () {
        var CurrentTdValue = parseInt($(this).closest('tr').find('td:eq(3)').find('input[versionno]').val());
        if (CurrentTdValue > version) {
            $(this).closest('tr').find('td:eq(3)').find('input[versionno]').val(parseInt(CurrentTdValue) - 1);
        }
    });
    $(obj).closest('tr').remove();
    if (docTypeoption.toString().trim() != undefined && docTypeoption.toString().trim() != '') {
        SetMaxVersionToNew(docTypeoption, true);
    }
}

function DeleteActivityRowDataFromMilestoneTask(obj) {
    var version = $(obj).closest('tr').find('td:eq(3)').find('input[versionno]').val();
    var docTypeoption = $(obj).closest('tr').find('td:eq(0)').find("select").val();
    var table = $("#dt_MilestoneTaskFile tbody");
    var regex = /Hot/;
    $('#dt_MilestoneTaskFile').find('tbody').find('select option[value="' + docTypeoption + '"]:selected').each(function () {
        var CurrentTdValue = parseInt($(this).closest('tr').find('td:eq(3)').find('input[versionno]').val());
        if (CurrentTdValue > version) {
            $(this).closest('tr').find('td:eq(3)').find('input[versionno]').val(parseInt(CurrentTdValue) - 1);
        }
    });
    $(obj).closest('tr').remove();
    if (docTypeoption.toString().trim() != undefined && docTypeoption.toString().trim() != '') {
        SetMaxVersionToNew(docTypeoption, true);
    }
}

function fngetVersionValue(obj, IsActive) {
    debugger;
    var DocTypeToCheck = $(obj).find("option:selected").val();
    var TaskID = $("#Tasks_TaskID").val();
    var DocResultarray = $.grep(CALBYMEVersion, function (n, i) {
        return n.DocType === DocTypeToCheck;
    });

    if (DocResultarray != null && DocResultarray != 'undefined' && DocResultarray.length > 0) {
        var MaxversionNoToSet = DocResultarray[0].MaxVersion;
        if (TaskID != null && TaskID != "00000000-0000-0000-0000-000000000000") {
            MaxversionNoToSet = parseInt(MaxversionNoToSet) + 1;
            $(obj).closest('tr').find('.versionVal').val(MaxversionNoToSet);
            SetMaxVersionToNew(DocTypeToCheck, false);
        }
        else {
            $(obj).closest('tr').find('.versionVal').val(MaxversionNoToSet);
            SetMaxVersionToNew(DocTypeToCheck, false);
        }
    }

    if (DocTypeToCheck.toString().trim() != '' && DocTypeToCheck.toString().trim().toUpperCase() == "OTHER") {
        $(obj).closest("td").find("input.OtherType").removeClass("hide input-sm input-small");
    } else {
        $(obj).closest("td").find("input.OtherType").addClass("hide input-sm input-small");
    }

    for (var i = 0; i < CALBYMEVersion.length; i++) {
        var CurrentDocType = CALBYMEVersion[i].DocType;
        var j = 0;
        $('#dt_taskFile').find('tbody').find('select option[value="' + CurrentDocType + '"]:selected').each(function () {
            $(this).closest('tr').find('td:eq(3)').find('input[versionno]').val(j + 1);
            j = j + 1;
        });
        CALBYMEVersion[i].MaxVersion = parseInt(j);
    }
}

function fngetVersionValueForMilestoneTask(obj, IsActive) {
    debugger;
    var DocTypeToCheck = $(obj).find("option:selected").val();
    var TaskID = $("#MilestoneTask_TaskID").val();
    var DocResultarray = $.grep(CALBYMEVersion, function (n, i) {
        return n.DocType === DocTypeToCheck;
    });

    if (DocResultarray != null && DocResultarray != 'undefined' && DocResultarray.length > 0) {
        var MaxversionNoToSet = DocResultarray[0].MaxVersion;
        if (TaskID != null && TaskID != "00000000-0000-0000-0000-000000000000") {
            MaxversionNoToSet = parseInt(MaxversionNoToSet) + 1;
            $(obj).closest('tr').find('.versionVal').val(MaxversionNoToSet);
            SetMaxVersionToNew(DocTypeToCheck, false);
        }
        else {
            $(obj).closest('tr').find('.versionVal').val(MaxversionNoToSet);
            SetMaxVersionToNew(DocTypeToCheck, false);
        }
    }

    if (DocTypeToCheck.toString().trim() != '' && DocTypeToCheck.toString().trim().toUpperCase() == "OTHER") {
        $(obj).closest("td").find("input.OtherType").removeClass("hide input-sm input-small");
    } else {
        $(obj).closest("td").find("input.OtherType").addClass("hide input-sm input-small");
    }

    for (var i = 0; i < CALBYMEVersion.length; i++) {
        var CurrentDocType = CALBYMEVersion[i].DocType;
        var j = 0;
        $('#dt_MilestoneTaskFile').find('tbody').find('select option[value="' + CurrentDocType + '"]:selected').each(function () {
            $(this).closest('tr').find('td:eq(3)').find('input[versionno]').val(j + 1);
            j = j + 1;
        });
        CALBYMEVersion[i].MaxVersion = parseInt(j);
    }
}

function AddNewRowForFile(obj, isEnabled) {
    var TaskIndex = $("#TaskIndex").val();
    TaskIndex = parseInt(TaskIndex);
    var html = $("#mycustomtrTaskFileDiv tr").html();
    var docTypeoption = $(obj).closest('tr').find('td:eq(0)').find("select").val();
    var docTypeoptionOthertext = $(obj).closest('tr').find('td:eq(0) .OtherType').val();

    if (isEnabled) {
        var ID = GenerateNewGUID();
        var trhtml = $("<tr id=" + ID + ">" + html + "</tr>");
        $("#TaskFilesDataTbl_Body").append(trhtml);

        if (isEnabled) {
            $("#" + ID).find('td:eq(0)').find('select').removeAttr('Disabled');
            //$("#" + ID).find('td:eq(3)').find('input[versionno]').removeAttr('Disabled');
        }

        $("#TaskIndex").val(TaskIndex);
        $(".bs-select").selectpicker('refresh');
        $("#" + ID + " td:eq(0) > div:first").removeAttr("class");
        $("#" + ID + " td:eq(0) > div:first .btn.dropdown-toggle.bs-placeholder.btn-default:first").remove();
        $("#" + ID + " td:eq(0) > div:first .dropdown-menu.open:first").remove();
        $("#" + ID + " td:eq(1) > div:first").removeAttr("class");
        $("#" + ID + " td:eq(1) > div:first .btn.dropdown-toggle.bs-placeholder.btn-default:first").remove();
        $("#" + ID + " td:eq(1) > div:first .dropdown-menu.open:first").remove();
        var DocTypeID = "projectdocumentlist_" + TaskIndex + "__DocType_Term";
        var DocTypeName = "projectdocumentlist[" + TaskIndex + "].DocType_Term";
        var StatusID = "projectdocumentlist_" + TaskIndex + "__Status_Term";
        var StatusName = "projectdocumentlist[" + TaskIndex + "].Status_Term";
        var VersionNoID = "projectdocumentlist_" + TaskIndex + "__VersionNo";
        var VersionNoName = "projectdocumentlist[" + TaskIndex + "].VersionNo";
        var attachid = "projectdocumentlist_" + TaskIndex + "__AttachementID";
        var attachmentname = "projectdocumentlist[" + TaskIndex + "].AttachementID";
        var DocumentID = "projectdocumentlist_" + TaskIndex + "__DocumentID";
        var Docname = "projectdocumentlist[" + TaskIndex + "].DocumentID";
        var FileID = "projectdocumentlist_" + TaskIndex + "__Term";
        var FileName = "projectdocumentlist[" + TaskIndex + "].Term";
        var ProjectTermID = "projectdocumentlist_" + TaskIndex + "__ProjectTermID";
        var ProjectTermName = "projectdocumentlist[" + TaskIndex + "].ProjectTermID";
        var filepath = "projectdocumentlist_" + TaskIndex + "__FilePath";
        var filepathname = "projectdocumentlist[" + TaskIndex + "].FilePath";
        var attachfile = "projectdocumentlist_" + TaskIndex + "__FileName";
        var attachfilename = "projectdocumentlist[" + TaskIndex + "].FileName";
        var FileSize = "projectdocumentlist_" + TaskIndex + "__FileSize";
        var FileSizeName = "projectdocumentlist[" + TaskIndex + "].FileSize";
        var ExtentionTypeTerm = "projectdocumentlist_" + TaskIndex + "__ExtensionType_Term";
        var ExtentionTypeTermName = "projectdocumentlist[" + TaskIndex + "].ExtensionType_Term";
        var DisplayNameId = "projectdocumentlist_" + TaskIndex + "__DisplayName";
        var DisplayName = "projectdocumentlist[" + TaskIndex + "].DisplayName";
        var IsNewId = "projectdocumentlist_" + TaskIndex + "__IsNew";
        var IsNewName = "projectdocumentlist[" + TaskIndex + "].IsNew";
        var OtherDocTypeName = "projectdocumentlist[" + TaskIndex + "].OtherDocType";
        var OtherDocTypeID = "projectdocumentlist_" + TaskIndex + "__OtherDocType";
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
        $("#" + ID + " .isnew").attr("id", IsNewId);
        $("#" + ID + " .isnew").attr("name", IsNewName);

        var CustomID = "projectdocumentlist_" + TaskIndex + "__CustomTerm";
        var CustommName = "projectdocumentlist[" + TaskIndex + "].CustomTerm";

        $("#" + ID + " .CustomField").attr("id", CustomID);
        $("#" + ID + " .CustomField").attr("name", CustommName);
        $("#" + ID + " .CustomField").val("Term_" + TaskIndex + "");

        $("#" + ID + " td:eq(0) div.btn-group.bootstrap-select.form-control.bs-select .btn.dropdown-toggle.bs-placeholder.btn-default").attr("data-id", DocTypeID);
        $("#" + ID + " td:eq(0) input.OtherType").attr("id", OtherDocTypeID);
        $("#" + ID + " td:eq(0) input.OtherType").attr("name", OtherDocTypeName);
        $("#" + ID + " td:eq(0) div.btn-group.bootstrap-select.form-control.bs-select select.form-control.bs-select").attr("id", DocTypeID);
        $("#" + ID + " td:eq(0) div.btn-group.bootstrap-select.form-control.bs-select select.form-control.bs-select").attr("name", DocTypeName);
        $("#" + ID + " td:eq(1) div.btn-group.bootstrap-select.form-control.bs-select .btn.dropdown-toggle.bs-placeholder.btn-default").attr("data-id", StatusID);
        $("#" + ID + " td:eq(1) div.btn-group.bootstrap-select.form-control.bs-select select.form-control.bs-select").attr("id", StatusID);
        $("#" + ID + " td:eq(1) div.btn-group.bootstrap-select.form-control.bs-select select.form-control.bs-select").attr("name", StatusName);
        $("#" + ID + " td:eq(2) input[type='file']").attr("id", "Term_" + TaskIndex + "");
        $("#" + ID + " td:eq(2) input[type='file']").attr("name", "Term_" + TaskIndex + "");
        $("#" + ID + " td:eq(3) .form-control").attr("id", VersionNoID);
        $("#" + ID + " td:eq(3) .form-control").attr("name", VersionNoName);
        if ($(obj).attr("addAttr")) {
            $('#' + DocTypeID + ' option[value="' + docTypeoption + '"]').prop("selected", true);
            $('#' + DocTypeID).selectpicker('refresh');

            if (docTypeoption.toString().trim() != '' && docTypeoption.toString().trim().toUpperCase() == "OTHER") {
                $('#' + DocTypeID + ' option[value="' + docTypeoption + '"]').closest("td").find("input.OtherType").removeClass("hide input-sm input-small");
                $('#' + DocTypeID + ' option[value="' + docTypeoption + '"]').closest('tr').find('td:eq(0) .OtherType').val(docTypeoptionOthertext);
                $('#' + DocTypeID + ' option[value="' + docTypeoption + '"]').closest('tr').find('td:eq(0) .OtherType').attr('disabled', 'disabled');
            }
        }
        var TaskID = $("#Tasks_TaskID").val();
        debugger;
        if (TaskID != null && TaskID != "00000000-0000-0000-0000-000000000000") {
            var DocResultarray = $.grep(CALBYMEVersion, function (n, i) {
                return n.DocType === docTypeoption;
            });
            if (DocResultarray != null && DocResultarray != 'undefined' && DocResultarray.length > 0) {
                var MaxversionNoToSet = DocResultarray[0].MaxVersion;
                MaxversionNoToSet = parseInt(MaxversionNoToSet) + 1;
                $('#' + VersionNoID).val(MaxversionNoToSet);
            }

            SetMaxVersionToNew(docTypeoption, false);
        }
        else {
            var DocResultarray = $.grep(CALBYMEVersion, function (n, i) {
                return n.DocType === docTypeoption;
            });
            if (DocResultarray != null && DocResultarray != 'undefined' && DocResultarray.length > 0) {
                var MaxversionNoToSet = DocResultarray[0].MaxVersion;
                MaxversionNoToSet = parseInt(MaxversionNoToSet);
                $('#' + VersionNoID).val(MaxversionNoToSet);
            }

            SetMaxVersionToNew(docTypeoption, false);
        }
        TaskIndex = parseInt(TaskIndex) + 1;
        $("#TaskIndex").val(TaskIndex);
        $("#VersionIndex").val(VersionIndex);
        $(".OtherType").removeClass("input-inline input-sm input-small");
        $(".versionno").removeClass("input-inline input-sm input-small");
    } else {
        if (docTypeoption.toString().trim() != undefined && docTypeoption.toString().trim() != null && docTypeoption.toString().trim() != '') {
            var ID = GenerateNewGUID();
            var trhtml = $("<tr id=" + ID + ">" + html + "</tr>");
            $("#TaskFilesDataTbl_Body").append(trhtml);

            if (isEnabled) {
                $("#" + ID).find('td:eq(0)').find('select').removeAttr('Disabled');
                //$("#" + ID).find('td:eq(3)').find('input[versionno]').removeAttr('Disabled');
            }

            $("#TaskIndex").val(TaskIndex);
            $(".bs-select").selectpicker('refresh');
            $("#" + ID + " td:eq(0) > div:first").removeAttr("class");
            $("#" + ID + " td:eq(0) > div:first .btn.dropdown-toggle.bs-placeholder.btn-default:first").remove();
            $("#" + ID + " td:eq(0) > div:first .dropdown-menu.open:first").remove();
            $("#" + ID + " td:eq(1) > div:first").removeAttr("class");
            $("#" + ID + " td:eq(1) > div:first .btn.dropdown-toggle.bs-placeholder.btn-default:first").remove();
            $("#" + ID + " td:eq(1) > div:first .dropdown-menu.open:first").remove();
            var DocTypeID = "projectdocumentlist_" + TaskIndex + "__DocType_Term";
            var DocTypeName = "projectdocumentlist[" + TaskIndex + "].DocType_Term";
            var StatusID = "projectdocumentlist_" + TaskIndex + "__Status_Term";
            var StatusName = "projectdocumentlist[" + TaskIndex + "].Status_Term";
            var VersionNoID = "projectdocumentlist_" + TaskIndex + "__VersionNo";
            var VersionNoName = "projectdocumentlist[" + TaskIndex + "].VersionNo";
            var attachid = "projectdocumentlist_" + TaskIndex + "__AttachementID";
            var attachmentname = "projectdocumentlist[" + TaskIndex + "].AttachementID";
            var DocumentID = "projectdocumentlist_" + TaskIndex + "__DocumentID";
            var Docname = "projectdocumentlist[" + TaskIndex + "].DocumentID";
            var FileID = "projectdocumentlist_" + TaskIndex + "__Term";
            var FileName = "projectdocumentlist[" + TaskIndex + "].Term";
            var ProjectTermID = "projectdocumentlist_" + TaskIndex + "__ProjectTermID";
            var ProjectTermName = "projectdocumentlist[" + TaskIndex + "].ProjectTermID";
            var filepath = "projectdocumentlist_" + TaskIndex + "__FilePath";
            var filepathname = "projectdocumentlist[" + TaskIndex + "].FilePath";
            var attachfile = "projectdocumentlist_" + TaskIndex + "__FileName";
            var attachfilename = "projectdocumentlist[" + TaskIndex + "].FileName";
            var FileSize = "projectdocumentlist_" + TaskIndex + "__FileSize";
            var FileSizeName = "projectdocumentlist[" + TaskIndex + "].FileSize";
            var ExtentionTypeTerm = "projectdocumentlist_" + TaskIndex + "__ExtensionType_Term";
            var ExtentionTypeTermName = "projectdocumentlist[" + TaskIndex + "].ExtensionType_Term";
            var DisplayNameId = "projectdocumentlist_" + TaskIndex + "__DisplayName";
            var DisplayName = "projectdocumentlist[" + TaskIndex + "].DisplayName";
            var IsNewId = "projectdocumentlist_" + TaskIndex + "__IsNew";
            var IsNewName = "projectdocumentlist[" + TaskIndex + "].IsNew";
            var OtherDocTypeName = "projectdocumentlist[" + TaskIndex + "].OtherDocType";
            var OtherDocTypeID = "projectdocumentlist_" + TaskIndex + "__OtherDocType";
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
            $("#" + ID + " .isnew").attr("id", IsNewId);
            $("#" + ID + " .isnew").attr("name", IsNewName);

            var CustomID = "projectdocumentlist_" + TaskIndex + "__CustomTerm";
            var CustommName = "projectdocumentlist[" + TaskIndex + "].CustomTerm";

            $("#" + ID + " .CustomField").attr("id", CustomID);
            $("#" + ID + " .CustomField").attr("name", CustommName);
            $("#" + ID + " .CustomField").val("Term_" + TaskIndex + "");

            $("#" + ID + " td:eq(0) div.btn-group.bootstrap-select.form-control.bs-select .btn.dropdown-toggle.bs-placeholder.btn-default").attr("data-id", DocTypeID);
            $("#" + ID + " td:eq(0) input.OtherType").attr("id", OtherDocTypeID);
            $("#" + ID + " td:eq(0) input.OtherType").attr("name", OtherDocTypeName);
            $("#" + ID + " td:eq(0) div.btn-group.bootstrap-select.form-control.bs-select select.form-control.bs-select").attr("id", DocTypeID);
            $("#" + ID + " td:eq(0) div.btn-group.bootstrap-select.form-control.bs-select select.form-control.bs-select").attr("name", DocTypeName);
            $("#" + ID + " td:eq(1) div.btn-group.bootstrap-select.form-control.bs-select .btn.dropdown-toggle.bs-placeholder.btn-default").attr("data-id", StatusID);
            $("#" + ID + " td:eq(1) div.btn-group.bootstrap-select.form-control.bs-select select.form-control.bs-select").attr("id", StatusID);
            $("#" + ID + " td:eq(1) div.btn-group.bootstrap-select.form-control.bs-select select.form-control.bs-select").attr("name", StatusName);
            $("#" + ID + " td:eq(2) input[type='file']").attr("id", "Term_" + TaskIndex + "");
            $("#" + ID + " td:eq(2) input[type='file']").attr("name", "Term_" + TaskIndex + "");
            $("#" + ID + " td:eq(3) .form-control").attr("id", VersionNoID);
            $("#" + ID + " td:eq(3) .form-control").attr("name", VersionNoName);
            if ($(obj).attr("addAttr")) {
                $('#' + DocTypeID + ' option[value="' + docTypeoption + '"]').prop("selected", true);
                $('#' + DocTypeID).selectpicker('refresh');

                if (docTypeoption.toString().trim() != '' && docTypeoption.toString().trim().toUpperCase() == "OTHER") {
                    $('#' + DocTypeID + ' option[value="' + docTypeoption + '"]').closest("td").find("input.OtherType").removeClass("hide input-sm input-small");
                    $('#' + DocTypeID + ' option[value="' + docTypeoption + '"]').closest('tr').find('td:eq(0) .OtherType').val(docTypeoptionOthertext);
                    $('#' + DocTypeID + ' option[value="' + docTypeoption + '"]').closest('tr').find('td:eq(0) .OtherType').attr('disabled', 'disabled');
                }
            }
            var TaskID = $("#Tasks_TaskID").val();
            debugger;
            if (TaskID != null && TaskID != "00000000-0000-0000-0000-000000000000") {
                var DocResultarray = $.grep(CALBYMEVersion, function (n, i) {
                    return n.DocType === docTypeoption;
                });
                if (DocResultarray != null && DocResultarray != 'undefined' && DocResultarray.length > 0) {
                    var MaxversionNoToSet = DocResultarray[0].MaxVersion;
                    MaxversionNoToSet = parseInt(MaxversionNoToSet) + 1;
                    $('#' + VersionNoID).val(MaxversionNoToSet);
                }

                SetMaxVersionToNew(docTypeoption, false);
            }
            else {
                var DocResultarray = $.grep(CALBYMEVersion, function (n, i) {
                    return n.DocType === docTypeoption;
                });
                if (DocResultarray != null && DocResultarray != 'undefined' && DocResultarray.length > 0) {
                    var MaxversionNoToSet = DocResultarray[0].MaxVersion;
                    MaxversionNoToSet = parseInt(MaxversionNoToSet) + 1;
                    $('#' + VersionNoID).val(MaxversionNoToSet);
                }

                SetMaxVersionToNew(docTypeoption, false);
            }
            TaskIndex = parseInt(TaskIndex) + 1;
            $("#TaskIndex").val(TaskIndex);
            $("#VersionIndex").val(VersionIndex);
            $(".OtherType").removeClass("input-inline input-sm input-small");
            $(".versionno").removeClass("input-inline input-sm input-small");
        } else {
            toastr["error"]("Please Select Document Type", "Task", { "positionClass": "toast-bottom-right", "progressBar": true });
        }
    }
}

function AddNewRowForFileInMilestoneTask(obj, isEnabled) {
    debugger;
    var TaskIndex = $("#MilestoneTaskIndex").val();
    TaskIndex = parseInt(TaskIndex);
    var html = $("#mycustomtrMilestoneTaskFileDiv tr").html();
    var docTypeoption = $(obj).closest('tr').find('td:eq(0)').find("select").val();
    var docTypeoptionOthertext = $(obj).closest('tr').find('td:eq(0) .OtherType').val();

    if (isEnabled) {
        var ID = GenerateNewGUID();
        var trhtml = $("<tr id=" + ID + ">" + html + "</tr>");
        $("#MilestoneTaskFilesDataTbl_Body").append(trhtml);


        if (isEnabled) {
            $("#" + ID).find('td:eq(0)').find('select').removeAttr('Disabled');
            $("#" + ID).find('td:eq(3)').find('input[versionno]').removeAttr('Disabled');
        }

        $("#MilestoneTaskIndex").val(TaskIndex);
        $(".bs-select").selectpicker('refresh');
        $("#" + ID + " td:eq(0) > div:first").removeAttr("class");
        $("#" + ID + " td:eq(0) > div:first .btn.dropdown-toggle.bs-placeholder.btn-default:first").remove();
        $("#" + ID + " td:eq(0) > div:first .dropdown-menu.open:first").remove();
        $("#" + ID + " td:eq(1) > div:first").removeAttr("class");
        $("#" + ID + " td:eq(1) > div:first .btn.dropdown-toggle.bs-placeholder.btn-default:first").remove();
        $("#" + ID + " td:eq(1) > div:first .dropdown-menu.open:first").remove();
        var DocTypeID = "projectdocumentlist_" + TaskIndex + "__DocType_Term";
        var DocTypeName = "projectdocumentlist[" + TaskIndex + "].DocType_Term";
        var StatusID = "projectdocumentlist_" + TaskIndex + "__Status_Term";
        var StatusName = "projectdocumentlist[" + TaskIndex + "].Status_Term";
        var VersionNoID = "projectdocumentlist_" + TaskIndex + "__VersionNo";
        var VersionNoName = "projectdocumentlist[" + TaskIndex + "].VersionNo";
        var attachid = "projectdocumentlist_" + TaskIndex + "__AttachementID";
        var attachmentname = "projectdocumentlist[" + TaskIndex + "].AttachementID";
        var DocumentID = "projectdocumentlist_" + TaskIndex + "__DocumentID";
        var Docname = "projectdocumentlist[" + TaskIndex + "].DocumentID";
        var FileID = "projectdocumentlist_" + TaskIndex + "__Term";
        var FileName = "projectdocumentlist[" + TaskIndex + "].Term";
        var ProjectTermID = "projectdocumentlist_" + TaskIndex + "__ProjectTermID";
        var ProjectTermName = "projectdocumentlist[" + TaskIndex + "].ProjectTermID";
        var filepath = "projectdocumentlist_" + TaskIndex + "__FilePath";
        var filepathname = "projectdocumentlist[" + TaskIndex + "].FilePath";
        var attachfile = "projectdocumentlist_" + TaskIndex + "__FileName";
        var attachfilename = "projectdocumentlist[" + TaskIndex + "].FileName";
        var FileSize = "projectdocumentlist_" + TaskIndex + "__FileSize";
        var FileSizeName = "projectdocumentlist[" + TaskIndex + "].FileSize";
        var ExtentionTypeTerm = "projectdocumentlist_" + TaskIndex + "__ExtensionType_Term";
        var ExtentionTypeTermName = "projectdocumentlist[" + TaskIndex + "].ExtensionType_Term";
        var DisplayNameId = "projectdocumentlist_" + TaskIndex + "__DisplayName";
        var DisplayName = "projectdocumentlist[" + TaskIndex + "].DisplayName";
        var IsNewId = "projectdocumentlist_" + TaskIndex + "__IsNew";
        var IsNewName = "projectdocumentlist[" + TaskIndex + "].IsNew";
        var OtherDocTypeName = "projectdocumentlist[" + TaskIndex + "].OtherDocType";
        var OtherDocTypeID = "projectdocumentlist_" + TaskIndex + "__OtherDocType";
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
        $("#" + ID + " .isnew").attr("id", IsNewId);
        $("#" + ID + " .isnew").attr("name", IsNewName);

        var CustomID = "projectdocumentlist_" + TaskIndex + "__CustomTerm";
        var CustommName = "projectdocumentlist[" + TaskIndex + "].CustomTerm";

        $("#" + ID + " .CustomField").attr("id", CustomID);
        $("#" + ID + " .CustomField").attr("name", CustommName);
        $("#" + ID + " .CustomField").val("Term_" + TaskIndex + "");

        $("#" + ID + " td:eq(0) div.btn-group.bootstrap-select.form-control.bs-select .btn.dropdown-toggle.bs-placeholder.btn-default").attr("data-id", DocTypeID);
        $("#" + ID + " td:eq(0) input.OtherType").attr("id", OtherDocTypeID);
        $("#" + ID + " td:eq(0) input.OtherType").attr("name", OtherDocTypeName);
        $("#" + ID + " td:eq(0) div.btn-group.bootstrap-select.form-control.bs-select select.form-control.bs-select").attr("id", DocTypeID);
        $("#" + ID + " td:eq(0) div.btn-group.bootstrap-select.form-control.bs-select select.form-control.bs-select").attr("name", DocTypeName);
        $("#" + ID + " td:eq(1) div.btn-group.bootstrap-select.form-control.bs-select .btn.dropdown-toggle.bs-placeholder.btn-default").attr("data-id", StatusID);
        $("#" + ID + " td:eq(1) div.btn-group.bootstrap-select.form-control.bs-select select.form-control.bs-select").attr("id", StatusID);
        $("#" + ID + " td:eq(1) div.btn-group.bootstrap-select.form-control.bs-select select.form-control.bs-select").attr("name", StatusName);
        $("#" + ID + " td:eq(2) input[type='file']").attr("id", "Term_" + TaskIndex + "");
        $("#" + ID + " td:eq(2) input[type='file']").attr("name", "Term_" + TaskIndex + "");
        $("#" + ID + " td:eq(3) .form-control").attr("id", VersionNoID);
        $("#" + ID + " td:eq(3) .form-control").attr("name", VersionNoName);
        if ($(obj).attr("addAttr")) {
            $('#' + DocTypeID + ' option[value="' + docTypeoption + '"]').prop("selected", true);
            $('#' + DocTypeID).selectpicker('refresh');

            if (docTypeoption.toString().trim() != '' && docTypeoption.toString().trim().toUpperCase() == "OTHER") {
                $('#' + DocTypeID + ' option[value="' + docTypeoption + '"]').closest("td").find("input.OtherType").removeClass("hide input-sm input-small");
                $('#' + DocTypeID + ' option[value="' + docTypeoption + '"]').closest('tr').find('td:eq(0) .OtherType').val(docTypeoptionOthertext);
                $('#' + DocTypeID + ' option[value="' + docTypeoption + '"]').closest('tr').find('td:eq(0) .OtherType').attr('disabled', 'disabled');
            }
        }
        var TaskID = $("#Tasks_TaskID").val();

        if (isEnabled) {
            if (TaskID != null && TaskID != "00000000-0000-0000-0000-000000000000") {
                var DocResultarray = $.grep(CALBYMEVersion, function (n, i) {
                    return n.DocType === docTypeoption;
                });
                if (DocResultarray != null && DocResultarray != 'undefined' && DocResultarray.length > 0) {
                    var MaxversionNoToSet = DocResultarray[0].MaxVersion;
                    MaxversionNoToSet = parseInt(MaxversionNoToSet) + 1;
                    $('#' + VersionNoID).val(MaxversionNoToSet);
                }
                SetMaxVersionToNew(docTypeoption, false);
            }
            else {
                var DocResultarray = $.grep(CALBYMEVersion, function (n, i) {
                    return n.DocType === docTypeoption;
                });
                if (DocResultarray != null && DocResultarray != 'undefined' && DocResultarray.length > 0) {
                    var MaxversionNoToSet = DocResultarray[0].MaxVersion;
                    MaxversionNoToSet = parseInt(MaxversionNoToSet);
                    $('#' + VersionNoID).val(MaxversionNoToSet);
                }
                SetMaxVersionToNew(docTypeoption, false);
            }
        } else {
            var DocResultarray = $.grep(CALBYMEVersion, function (n, i) {
                return n.DocType === docTypeoption;
            });
            if (DocResultarray != null && DocResultarray != 'undefined' && DocResultarray.length > 0) {
                var MaxversionNoToSet = DocResultarray[0].MaxVersion;
                MaxversionNoToSet = parseInt(MaxversionNoToSet) + 1;
                $('#' + VersionNoID).val(MaxversionNoToSet);
            }
            SetMaxVersionToNew(docTypeoption, false);
        }

        TaskIndex = parseInt(TaskIndex) + 1;
        $("#MilestoneTaskIndex").val(TaskIndex);
        $("#MilestoneVersionIndex").val(VersionIndex);
        $(".OtherType").removeClass("input-inline input-sm input-small");
        $(".versionno").removeClass("input-inline input-sm input-small");
    } else {
        if (docTypeoption.toString().trim() != undefined && docTypeoption.toString().trim() != null && docTypeoption.toString().trim() != '') {
            var ID = GenerateNewGUID();
            var trhtml = $("<tr id=" + ID + ">" + html + "</tr>");
            $("#MilestoneTaskFilesDataTbl_Body").append(trhtml);


            if (isEnabled) {
                $("#" + ID).find('td:eq(0)').find('select').removeAttr('Disabled');
                $("#" + ID).find('td:eq(3)').find('input[versionno]').removeAttr('Disabled');
            }

            $("#MilestoneTaskIndex").val(TaskIndex);
            $(".bs-select").selectpicker('refresh');
            $("#" + ID + " td:eq(0) > div:first").removeAttr("class");
            $("#" + ID + " td:eq(0) > div:first .btn.dropdown-toggle.bs-placeholder.btn-default:first").remove();
            $("#" + ID + " td:eq(0) > div:first .dropdown-menu.open:first").remove();
            $("#" + ID + " td:eq(1) > div:first").removeAttr("class");
            $("#" + ID + " td:eq(1) > div:first .btn.dropdown-toggle.bs-placeholder.btn-default:first").remove();
            $("#" + ID + " td:eq(1) > div:first .dropdown-menu.open:first").remove();
            var DocTypeID = "projectdocumentlist_" + TaskIndex + "__DocType_Term";
            var DocTypeName = "projectdocumentlist[" + TaskIndex + "].DocType_Term";
            var StatusID = "projectdocumentlist_" + TaskIndex + "__Status_Term";
            var StatusName = "projectdocumentlist[" + TaskIndex + "].Status_Term";
            var VersionNoID = "projectdocumentlist_" + TaskIndex + "__VersionNo";
            var VersionNoName = "projectdocumentlist[" + TaskIndex + "].VersionNo";
            var attachid = "projectdocumentlist_" + TaskIndex + "__AttachementID";
            var attachmentname = "projectdocumentlist[" + TaskIndex + "].AttachementID";
            var DocumentID = "projectdocumentlist_" + TaskIndex + "__DocumentID";
            var Docname = "projectdocumentlist[" + TaskIndex + "].DocumentID";
            var FileID = "projectdocumentlist_" + TaskIndex + "__Term";
            var FileName = "projectdocumentlist[" + TaskIndex + "].Term";
            var ProjectTermID = "projectdocumentlist_" + TaskIndex + "__ProjectTermID";
            var ProjectTermName = "projectdocumentlist[" + TaskIndex + "].ProjectTermID";
            var filepath = "projectdocumentlist_" + TaskIndex + "__FilePath";
            var filepathname = "projectdocumentlist[" + TaskIndex + "].FilePath";
            var attachfile = "projectdocumentlist_" + TaskIndex + "__FileName";
            var attachfilename = "projectdocumentlist[" + TaskIndex + "].FileName";
            var FileSize = "projectdocumentlist_" + TaskIndex + "__FileSize";
            var FileSizeName = "projectdocumentlist[" + TaskIndex + "].FileSize";
            var ExtentionTypeTerm = "projectdocumentlist_" + TaskIndex + "__ExtensionType_Term";
            var ExtentionTypeTermName = "projectdocumentlist[" + TaskIndex + "].ExtensionType_Term";
            var DisplayNameId = "projectdocumentlist_" + TaskIndex + "__DisplayName";
            var DisplayName = "projectdocumentlist[" + TaskIndex + "].DisplayName";
            var IsNewId = "projectdocumentlist_" + TaskIndex + "__IsNew";
            var IsNewName = "projectdocumentlist[" + TaskIndex + "].IsNew";
            var OtherDocTypeName = "projectdocumentlist[" + TaskIndex + "].OtherDocType";
            var OtherDocTypeID = "projectdocumentlist_" + TaskIndex + "__OtherDocType";
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
            $("#" + ID + " .isnew").attr("id", IsNewId);
            $("#" + ID + " .isnew").attr("name", IsNewName);

            var CustomID = "projectdocumentlist_" + TaskIndex + "__CustomTerm";
            var CustommName = "projectdocumentlist[" + TaskIndex + "].CustomTerm";

            $("#" + ID + " .CustomField").attr("id", CustomID);
            $("#" + ID + " .CustomField").attr("name", CustommName);
            $("#" + ID + " .CustomField").val("Term_" + TaskIndex + "");

            $("#" + ID + " td:eq(0) div.btn-group.bootstrap-select.form-control.bs-select .btn.dropdown-toggle.bs-placeholder.btn-default").attr("data-id", DocTypeID);
            $("#" + ID + " td:eq(0) input.OtherType").attr("id", OtherDocTypeID);
            $("#" + ID + " td:eq(0) input.OtherType").attr("name", OtherDocTypeName);
            $("#" + ID + " td:eq(0) div.btn-group.bootstrap-select.form-control.bs-select select.form-control.bs-select").attr("id", DocTypeID);
            $("#" + ID + " td:eq(0) div.btn-group.bootstrap-select.form-control.bs-select select.form-control.bs-select").attr("name", DocTypeName);
            $("#" + ID + " td:eq(1) div.btn-group.bootstrap-select.form-control.bs-select .btn.dropdown-toggle.bs-placeholder.btn-default").attr("data-id", StatusID);
            $("#" + ID + " td:eq(1) div.btn-group.bootstrap-select.form-control.bs-select select.form-control.bs-select").attr("id", StatusID);
            $("#" + ID + " td:eq(1) div.btn-group.bootstrap-select.form-control.bs-select select.form-control.bs-select").attr("name", StatusName);
            $("#" + ID + " td:eq(2) input[type='file']").attr("id", "Term_" + TaskIndex + "");
            $("#" + ID + " td:eq(2) input[type='file']").attr("name", "Term_" + TaskIndex + "");
            $("#" + ID + " td:eq(3) .form-control").attr("id", VersionNoID);
            $("#" + ID + " td:eq(3) .form-control").attr("name", VersionNoName);
            if ($(obj).attr("addAttr")) {
                $('#' + DocTypeID + ' option[value="' + docTypeoption + '"]').prop("selected", true);
                $('#' + DocTypeID).selectpicker('refresh');

                if (docTypeoption.toString().trim() != '' && docTypeoption.toString().trim().toUpperCase() == "OTHER") {
                    $('#' + DocTypeID + ' option[value="' + docTypeoption + '"]').closest("td").find("input.OtherType").removeClass("hide input-sm input-small");
                    $('#' + DocTypeID + ' option[value="' + docTypeoption + '"]').closest('tr').find('td:eq(0) .OtherType').val(docTypeoptionOthertext);
                    $('#' + DocTypeID + ' option[value="' + docTypeoption + '"]').closest('tr').find('td:eq(0) .OtherType').attr('disabled', 'disabled');
                }
            }
            var TaskID = $("#Tasks_TaskID").val();

            if (isEnabled) {
                if (TaskID != null && TaskID != "00000000-0000-0000-0000-000000000000") {
                    var DocResultarray = $.grep(CALBYMEVersion, function (n, i) {
                        return n.DocType === docTypeoption;
                    });
                    if (DocResultarray != null && DocResultarray != 'undefined' && DocResultarray.length > 0) {
                        var MaxversionNoToSet = DocResultarray[0].MaxVersion;
                        MaxversionNoToSet = parseInt(MaxversionNoToSet) + 1;
                        $('#' + VersionNoID).val(MaxversionNoToSet);
                    }
                    SetMaxVersionToNew(docTypeoption, false);
                }
                else {
                    var DocResultarray = $.grep(CALBYMEVersion, function (n, i) {
                        return n.DocType === docTypeoption;
                    });
                    if (DocResultarray != null && DocResultarray != 'undefined' && DocResultarray.length > 0) {
                        var MaxversionNoToSet = DocResultarray[0].MaxVersion;
                        MaxversionNoToSet = parseInt(MaxversionNoToSet);
                        $('#' + VersionNoID).val(MaxversionNoToSet);
                    }
                    SetMaxVersionToNew(docTypeoption, false);
                }
            } else {
                var DocResultarray = $.grep(CALBYMEVersion, function (n, i) {
                    return n.DocType === docTypeoption;
                });
                if (DocResultarray != null && DocResultarray != 'undefined' && DocResultarray.length > 0) {
                    var MaxversionNoToSet = DocResultarray[0].MaxVersion;
                    MaxversionNoToSet = parseInt(MaxversionNoToSet) + 1;
                    $('#' + VersionNoID).val(MaxversionNoToSet);
                }
                SetMaxVersionToNew(docTypeoption, false);
            }

            TaskIndex = parseInt(TaskIndex) + 1;
            $("#MilestoneTaskIndex").val(TaskIndex);
            $("#MilestoneVersionIndex").val(VersionIndex);
            $(".OtherType").removeClass("input-inline input-sm input-small");
            $(".versionno").removeClass("input-inline input-sm input-small");
        } else {
            toastr["error"]("Please Select Document Type", "Task", { "positionClass": "toast-bottom-right", "progressBar": true });
        }
    }
}

function GotoMilestoneList(obj) {
    var AreaID = $(obj).attr("areaid");
    AreaFilterNameForMilestoneList = AreaID.toString().toUpperCase();
    $("#tab_15_2").click();
}