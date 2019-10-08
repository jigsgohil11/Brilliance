
var MainHtml = "";
$("#tabs_from_customfield_FromSelection_CommanForm").sortable({
    group: 'no-drop',
    handle: 'li.highlight',
    onDragStart: function ($item, container, _super) {
        MainHtml = $("#tabs_from_customfield_FromSelection_CommanForm").html();
        if (!container.options.drop)
            $item.clone().insertAfter($item);
        _super($item, container);
    },
    onDrop: function ($item, container, _super) {
        container.el.removeClass("active");
        _super($item, container);

        if ($($item).attr("data-type") == "Section") {
            if ($($item).parent().attr("data-type") == "Section") {
                $("#tabs_from_customfield_FromSelection_CommanForm").html(MainHtml);
                //UIkit.modal.alert("You Can't Add Section In Section...!!!");
            }
            else {
                if ($($item).find("h3.full_width_in_card").length == 0 && $($item).find("ol[data-type='Section']").length == 0) {
                    fnCreateFieldByFieldType("Section", $item);
                }
            }
        }
        if ($($item).attr("data-type") == "Panel") {
            if ($($item).parent().attr("data-type") == "Main") {
                $("#tabs_from_customfield_FromSelection_CommanForm").html(MainHtml);
                //UIkit.modal.alert("You Can't Add Section In Section...!!!");
            }
            else {
                fnCreateFieldByFieldType("Panel", $item);
            }
        }
        else if ($($item).attr("data-type") == "CheckBox") {
            if ($($item).parent().attr("data-type") == "Main") {
                $("#tabs_from_customfield_FromSelection_CommanForm").html(MainHtml);
                //UIkit.modal.alert("Add Field In Section Area, You Can't Add Field Out Side Of Section...!!!")
            }
            else {
                fnCreateFieldByFieldType("CheckBox", $item);
            }
        }
        else if ($($item).attr("data-type") == "RadioButton") {
            if ($($item).parent().attr("data-type") == "Main") {
                $("#tabs_from_customfield_FromSelection_CommanForm").html(MainHtml);
                //UIkit.modal.alert("Add Field In Section Area, You Can't Add Field Out Side Of Section...!!!");
            }
            else {
                fnCreateFieldByFieldType("RadioButton", $item);
            }
        }
        else if ($($item).attr("data-type") == "TextBox") {
            if ($($item).parent().attr("data-type") == "Main") {
                $("#tabs_from_customfield_FromSelection_CommanForm").html(MainHtml);
                //UIkit.modal.alert("Add Field In Section Area, You Can't Add Field Out Side Of Section...!!!");
            }
            else {
                fnCreateFieldByFieldType("TextBox", $item);
            }
        }
        else if ($($item).attr("data-type") == "TextArea") {
            if ($($item).parent().attr("data-type") == "Main") {
                $("#tabs_from_customfield_FromSelection_CommanForm").html(MainHtml);
                //UIkit.modal.alert("Add Field In Section Area, You Can't Add Field Out Side Of Section...!!!");
            }
            else {
                fnCreateFieldByFieldType("TextArea", $item);
            }
        }
        else if ($($item).attr("data-type") == "Selection") {
            if ($($item).parent().attr("data-type") == "Main") {
                $("#tabs_from_customfield_FromSelection_CommanForm").html(MainHtml);
                //UIkit.modal.alert("Add Field In Section Area, You Can't Add Field Out Side Of Section...!!!");
            }
            else {
                fnCreateFieldByFieldType("Selection", $item);
            }
        }
    },
});

function fnCreateFieldByFieldType(Type, obj) {
    if (Type == "Section") {
        $(obj).html("<i title='Remove' class='fa fa-remove material-icons' onclick='fnremovethiselements(this)' ></i><h3 data-id='00000000-0000-0000-0000-000000000000' class='full_width_in_card heading_c section-option margin-0' section-name='Section Name' isOptional='true' isOn='OnPage' section-detail='' section-weightage='0'><label class='dsp-label'>Section Name</label></h3><ol data-type='Section'></ol>").find("h3.full_width_in_card").attr("onclick", "fnSetSectionProperty(this)");
    } else if (Type == "Panel") {
        $(obj).html("<i title='Remove' class='fa fa-remove material-icons' onclick='fnremovethiselements(this)' ></i><label class='dsp-label'>Panel</label><div data-id='00000000-0000-0000-0000-000000000000' class='field-option' tool-tip='' questionweightage='' ltr='Left' isoptional='true' iscommentoptional='true' isheader='false' ismultiselect='false' iscombo='false' isattachment='false' length='0' row='0' option='Label 1~Label 2~Label 3' option-id='00000000-0000-0000-0000-000000000000~00000000-0000-0000-0000-000000000000~00000000-0000-0000-0000-000000000000' option-weightage='0~0~0' dis-lab='Panel' CurrectAnswer=''><label>Label 1</label><br/><label>Label 2</label><br/><label>Label 3</label></div>").attr("onclick", "fnSetFieldProperty(this)");
    } else if (Type == "CheckBox") {
        $(obj).html("<i title='Remove' class='fa fa-remove material-icons' onclick='fnremovethiselements(this)' ></i><label class='dsp-label'>Check Box</label><div data-id='00000000-0000-0000-0000-000000000000' class='field-option'  dis-lab='Check Box' tool-tip='' QuestionWeightage='' ltr='Left' isOptional='true' isCommentOptional='true' isheader='false' isMultiSelect='true' row='0' length='0' iscombo='false' isattachment='false' option='Check Box1~Check Box2~Check Box3' option-id='00000000-0000-0000-0000-000000000000~00000000-0000-0000-0000-000000000000~00000000-0000-0000-0000-000000000000' CurrectAnswer='Check Box1' option-weightage='0~0~0' ><label><i class='fa fa-check-square' ></i> Check Box1</label> <label><i class='fa fa-check-square' ></i> Check Box2</label> <label><i class='fa fa-check-square' ></i> Check Box3</label></div><div class='clearfix'></div>").attr("onclick", "fnSetFieldProperty(this)");
    } else if (Type == "RadioButton") {
        $(obj).html("<i title='Remove' class='fa fa-remove material-icons' onclick='fnremovethiselements(this)' ></i><label class='dsp-label'>Radio Button</label><div data-id='00000000-0000-0000-0000-000000000000' class='field-option'  dis-lab='Radio Button' tool-tip='' QuestionWeightage='' ltr='Left' isOptional='true' isCommentOptional='true' isheader='false' isMultiSelect='false' iscombo='false' isattachment='false' length='0' row='0' option='Radio Button1~Radio Button2~Radio Button3' option-id='00000000-0000-0000-0000-000000000000~00000000-0000-0000-0000-000000000000~00000000-0000-0000-0000-000000000000' CurrectAnswer='Radio Button1' option-weightage='0~0~0' ><label><i class='fa fa-dot-circle-o' ></i> Radio Button1</label> <label><i class='fa fa-dot-circle-o' ></i> Radio Button2</label> <label><i class='fa fa-dot-circle-o' ></i> Radio Button3</label></div><div class='clearfix'></div>").attr("onclick", "fnSetFieldProperty(this)");
    } else if (Type == "TextBox") {
        $(obj).html("<i title='Remove' class='fa fa-remove material-icons' onclick='fnremovethiselements(this)' ></i><label class='dsp-label'>Text Box</label><div data-id='00000000-0000-0000-0000-000000000000' class='field-option input-group' dis-lab='Text Box' tool-tip='' QuestionWeightage='' ltr='Left' isOptional='true' isCommentOptional='true' isheader='false' length='10' isMultiSelect='false' option='' option-id='' iscombo='false' isattachment='false' row='0'><input readonly type='text' class='form-control' /></div><div class='clearfix'></div>").attr("onclick", "fnSetFieldProperty(this)");
    } else if (Type == "TextArea") {
        $(obj).html("<i title='Remove' class='fa fa-remove material-icons' onclick='fnremovethiselements(this)' ></i> <label class='dsp-label'>Text Area</label><div data-id='00000000-0000-0000-0000-000000000000' class='field-option' dis-lab='Text Area' tool-tip='' QuestionWeightage='' ltr='Left' isOptional='true' isCommentOptional='true' isheader='false' length='10' isMultiSelect='false' row='3' iscombo='false' isattachment='false' option='' option-id=''><textarea readonly rows='1' class='form-control'></textarea></div><div class='clearfix'></div>").attr("onclick", "fnSetFieldProperty(this)");
    } else if (Type == "Selection") {
        $(obj).html("<i title='Remove' class='fa fa-remove material-icons' onclick='fnremovethiselements(this)' ></i> <label class='dsp-label'>Select List</label><div data-id='00000000-0000-0000-0000-000000000000' class='field-option input-group btn-group bootstrap-select form-control bs-select'  dis-lab='Select List' tool-tip='' QuestionWeightage='' ltr='Left' isOptional='true' isCommentOptional='true' isheader='false' isMultiSelect='false' length='0' row='0' iscombo='true' option-weightage='0~0~0' isattachment='false' option='Option 1~Option 2~Option 3' option-id='00000000-0000-0000-0000-000000000000~00000000-0000-0000-0000-000000000000~00000000-0000-0000-0000-000000000000' CurrectAnswer='Option 1' ><select class='form-control bs-select' disabled><option value=''>Choose Value...</option></select><div class='clearfix'></div>").attr("onclick", "fnSetFieldProperty(this)");
        $(".bs-select").selectpicker('refresh');
    }
}

$("#olFieldSelectionArea").sortable({
    group: 'no-drop',
    drop: false
});

function fnSetFieldProperty(obj) {
    $("#tabs_from_customfield_FromSelection_CommanForm li").removeClass("active");
    $(obj).addClass("active");

    $("#div-Option-Multiselection, #div-Option-Rows, #div-Option-Length, #div-Option-FieldOption, #div-LabelOption-FieldOption, #IsHeader-div").hide();

    $("#tabs_from_customfield_FieldProperty_form_txtDisplayName").val($(obj).find("div.field-option").attr("dis-lab"));

    if ($(obj).find("div.field-option").attr("isOptional") == "true")
        $("#tabs_from_customfield_FieldProperty_form_chkFieldIsOptional").prop('checked', true);
    else
        $("#tabs_from_customfield_FieldProperty_form_chkFieldIsOptional").prop('checked', false);

    if ($(obj).find("div.field-option").attr("isCommentOptional") == "true")
        $("#tabs_from_customfield_FieldProperty_form_chkCommentIsOptional").prop('checked', true);
    else
        $("#tabs_from_customfield_FieldProperty_form_chkCommentIsOptional").prop('checked', false);

    if ($(obj).find("div.field-option").attr("isheader") == "true")
        $("#tabs_from_customfield_FieldProperty_form_chkIsHeader").prop('checked', true);
    else
        $("#tabs_from_customfield_FieldProperty_form_chkIsHeader").prop('checked', false);

    $("#tabs_from_customfield_FieldProperty_form_txtToolTip").val($(obj).find("div.field-option").attr("tool-tip"));

    $("#tabs_from_customfield_FieldProperty_form_txtQuestionWeightage").val($(obj).find("div.field-option").attr("QuestionWeightage"));

    $('#tabs_from_customfield_FieldProperty_form_ddlLTRTerm').val($(obj).find("div.field-option").attr("ltr"))
    //$('select').select2();

    if ($(obj).attr("data-type") == "CheckBox") {

        var varOption = $(obj).find("div.field-option").attr("option").split("~");
        var varOptionIDs = $(obj).find("div.field-option").attr("option-id").split("~");
        var varOptionWeightage = $(obj).find("div.field-option").attr("option-weightage").split("~");

        $("#tabs_from_customfield_FieldProperty_form_tblOptions").html("");
        for (key in varOption) {
            if (varOption[key].trim() != "") {
                if (varOption[key] == $(obj).find("div.field-option").attr("CurrectAnswer")) {
                    $("#tabs_from_customfield_FieldProperty_form_tblOptions").append("<tr><td><input onkeyup='CreateFieldOptionOnUI();' type='text' class='form-control LabelText' value='" + varOption[key] + "' option-id-val='" + varOptionIDs[key] + "' isselected='true' /></td><td><input name='optionsRadioAns' id='optionsRadioAns_" + key + "' value='" + varOption[key] + "' type='radio' checked='checked' radiobtnval='" + varOption[key] + "' onclick='CurrectAnswer(this);'></td><td><input onkeyup='CreateFieldOptionOnUI();' type='text' class='form-control OptionWeightage' value='" + varOptionWeightage[key] + "' option-weightage='" + varOptionWeightage[key] + "' /></td><td class='uk-text-center'><a href='javascript:;' onclick='RemoveFieldOptionRow(this)'><i class='md-icon material-icons fa fa-minus-circle'></i></a></td></tr>");
                } else {
                    $("#tabs_from_customfield_FieldProperty_form_tblOptions").append("<tr><td><input onkeyup='CreateFieldOptionOnUI();' type='text' class='form-control LabelText' value='" + varOption[key] + "' option-id-val='" + varOptionIDs[key] + "' isselected='false' /></td><td><input name='optionsRadioAns' id='optionsRadioAns_" + key + "' value='" + varOption[key] + "' type='radio' radiobtnval='" + varOption[key] + "' onclick='CurrectAnswer(this);'></td><td><input onkeyup='CreateFieldOptionOnUI();' type='text' class='form-control OptionWeightage' value='" + varOptionWeightage[key] + "' option-weightage='" + varOptionWeightage[key] + "' /></td><td class='uk-text-center'><a href='javascript:;' onclick='RemoveFieldOptionRow(this)'><i class='md-icon material-icons fa fa-minus-circle'></i></a></td></tr>");
                }
            }
        }
        $("#div-Option-FieldOption").show();
    }
    else if ($(obj).attr("data-type") == "Date") { }
    else if ($(obj).attr("data-type") == "Time") { }
    else if ($(obj).attr("data-type") == "File") { }
    else if ($(obj).attr("data-type") == "Image") { }
    else if ($(obj).attr("data-type") == "Password") {

        $("#tabs_from_customfield_FieldProperty_form_Length").val($(obj).find("div.field-option").attr("length"));

        $("#div-Option-Length").show();

    } else if ($(obj).attr("data-type") == "RadioButton") {
        var varOption = $(obj).find("div.field-option").attr("option").split("~");
        var varOptionIDs = $(obj).find("div.field-option").attr("option-id").split("~");
        var varOptionWeightage = $(obj).find("div.field-option").attr("option-weightage").split("~");

        $("#tabs_from_customfield_FieldProperty_form_tblOptions").html("");
        for (key in varOption) {
            if (varOption[key].trim() != "") {
                if (varOption[key] == $(obj).find("div.field-option").attr("CurrectAnswer")) {
                    $("#tabs_from_customfield_FieldProperty_form_tblOptions").append("<tr><td><input onkeyup='CreateFieldOptionOnUI();' type='text' class='form-control LabelText' value='" + varOption[key] + "' option-id-val='" + varOptionIDs[key] + "' isselected='true' /></td><td><input name='optionsRadioAns' id='optionsRadioAns_" + key + "' value='" + varOption[key] + "' type='radio' checked='checked' radiobtnval='" + varOption[key] + "' onclick='CurrectAnswer(this);'></td><td><input onkeyup='CreateFieldOptionOnUI();' type='text' class='form-control OptionWeightage' value='" + varOptionWeightage[key] + "' option-weightage='" + varOptionWeightage[key] + "' /></td><td class='uk-text-center'><a href='javascript:;' onclick='RemoveFieldOptionRow(this)'><i class='md-icon material-icons fa fa-minus-circle'></i></a></td></tr>");
                } else {
                    $("#tabs_from_customfield_FieldProperty_form_tblOptions").append("<tr><td><input onkeyup='CreateFieldOptionOnUI();' type='text' class='form-control LabelText' value='" + varOption[key] + "' option-id-val='" + varOptionIDs[key] + "' isselected='false' /></td><td><input name='optionsRadioAns' id='optionsRadioAns_" + key + "' value='" + varOption[key] + "' type='radio' radiobtnval='" + varOption[key] + "' onclick='CurrectAnswer(this);'></td><td><input onkeyup='CreateFieldOptionOnUI();' type='text' class='form-control OptionWeightage' value='" + varOptionWeightage[key] + "' option-weightage='" + varOptionWeightage[key] + "' /></td><td class='uk-text-center'><a href='javascript:;' onclick='RemoveFieldOptionRow(this)'><i class='md-icon material-icons fa fa-minus-circle'></i></a></td></tr>");
                }
            }
        }
        $("#div-Option-FieldOption").show();
    } else if ($(obj).attr("data-type") == "Panel") {
        var varOption = $(obj).find("div.field-option").attr("option").split("~");
        var varOptionIDs = $(obj).find("div.field-option").attr("option-id").split("~");

        $("#tabs_from_customfield_FieldProperty_form_tblLabelOptions").html("");

        for (key in varOption) {
            if (varOption[key].trim() != "") {
                $("#tabs_from_customfield_FieldProperty_form_tblLabelOptions").append("<tr><td><input onkeyup='CreateFieldOptionOnUI();' type='text' class='form-control LabelText' value='" + varOption[key] + "' option-id-val='" + varOptionIDs[key] + "' /></td><td class='uk-text-center'><a href='javascript:;' onclick='RemoveFieldOptionRow(this)'><i class='md-icon material-icons fa fa-minus-circle'></i></a></td></tr>");
            }
        }

        $("#div-LabelOption-FieldOption").show();
        $("#IsHeader-div").show();
    } else if ($(obj).attr("data-type") == "TextBox") {

        $("#tabs_from_customfield_FieldProperty_form_Length").val($(obj).find("div.field-option").attr("length"));

        $("#div-Option-Length").show();

    } else if ($(obj).attr("data-type") == "TextArea") {

        $("#tabs_from_customfield_FieldProperty_form_txtRows").val($(obj).find("div.field-option").attr("row"));
        $("#tabs_from_customfield_FieldProperty_form_Length").val($(obj).find("div.field-option").attr("length"));

        $("#div-Option-Length, #div-Option-Rows").show();

    } else if ($(obj).attr("data-type") == "Selection") {

        var varOption = $(obj).find("div.field-option").attr("option").split("~");
        var varOptionIDs = $(obj).find("div.field-option").attr("option-id").split("~");
        $("#tabs_from_customfield_FieldProperty_form_tblOptions").html("");
        for (key in varOption) {
            if (varOption[key].trim() != "") {
                if (varOption[key] == $(obj).find("div.field-option").attr("CurrectAnswer")) {
                    $("#tabs_from_customfield_FieldProperty_form_tblOptions").append("<tr><td><input onkeyup='CreateFieldOptionOnUI();' type='text' class='form-control LabelText' value='" + varOption[key] + "' option-id-val='" + varOptionIDs[key] + "' /></td><td><input name='optionsRadioAns' id='optionsRadioAns_" + key + "' value='" + varOption[key] + "' type='radio' checked='checked' radiobtnval='" + varOption[key] + "' onclick='CurrectAnswer(this);'></td><td class='uk-text-center'><a href='javascript:;' onclick='RemoveFieldOptionRow(this)'><i class='md-icon material-icons fa fa-minus-circle'></i></a></td></tr>");
                } else {
                    $("#tabs_from_customfield_FieldProperty_form_tblOptions").append("<tr><td><input onkeyup='CreateFieldOptionOnUI();' type='text' class='form-control LabelText' value='" + varOption[key] + "' option-id-val='" + varOptionIDs[key] + "' /></td><td><input name='optionsRadioAns' id='optionsRadioAns_" + key + "' value='" + varOption[key] + "' type='radio' radiobtnval='" + varOption[key] + "' onclick='CurrectAnswer(this);'></td><td class='uk-text-center'><a href='javascript:;' onclick='RemoveFieldOptionRow(this)'><i class='md-icon material-icons fa fa-minus-circle'></i></a></td></tr>");
                }
            }
        }
        if ($(obj).find("div.field-option").attr("isMultiSelect") == "true")
            $("#tabs_from_customfield_FieldProperty_form_chkFieldIsMultiselection").prop('checked', true);
        else
            $("#tabs_from_customfield_FieldProperty_form_chkFieldIsMultiselection").prop('checked', false);

        $("#div-Option-FieldOption").show();
        $("#div-Option-Multiselection").show();
    }

    $("#tabs_from_customfield_FieldSelectionTabMenu li").removeClass("active");
    $("#tabs_from_customfield_FieldSelectionTabMenu li:nth-child(2) a").trigger("click");

}

function fnSetSectionProperty(obj) {

    $("#tabs_from_customfield_FromSelection_CommanForm li").removeClass("active");
    $(obj).parent("li").addClass("active");

    $("#tabs_from_customfield_SectionProperty_form_txtSectionName").val($(obj).attr("section-name"));
    $("#tabs_from_customfield_SectionProperty_form_txtWeightage").val($(obj).attr("section-weightage"));

    if ($(obj).attr("isOptional") == "true")
        $("#tabs_from_customfield_SectionProperty_form_chkIsOptional").prop("checked", true);
    else
        $("#tabs_from_customfield_SectionProperty_form_chkIsOptional").prop("checked", false);

    $("input[type='radio'][name='tabs_from_customfield_SectionProperty_form_chkIsInPagePopupTab'][value='" + $(obj).attr("isOn") + "']").prop("checked", true);
    $("#tabs_from_customfield_SectionProperty_form_txtSectionDetail").val($(obj).attr("section-detail"));

    $("#tabs_from_customfield_FieldSelectionTabMenu li").removeClass("active");
    $("#tabs_from_customfield_FieldSelectionTabMenu li:nth-child(3) a").trigger("click");
}

function RemoveFieldOptionRow(obj) {
    $(obj).parent("td").parent("tr").remove();
    CreateFieldOptionOnUI();
}

function AddFieldOptionRow() {
    var STR = "";
    if ($("#tabs_from_customfield_FromSelection_CommanForm li[data-type='CheckBox'].active").length > 0) {
        STR = "Check Box";
    }
    else if ($("#tabs_from_customfield_FromSelection_CommanForm li[data-type='RadioButton'].active").length > 0) {
        STR = "Radio Button";
    }
    else if ($("#tabs_from_customfield_FromSelection_CommanForm li[data-type='Selection'].active").length > 0) {
        STR = "Select Option";
    }
    else if ($("#tabs_from_customfield_FromSelection_CommanForm li[data-type='Panel'].active").length > 0) {
        STR = "Label";
    }

    if (STR == "Label") {
        $("#tabs_from_customfield_FieldProperty_form_tblLabelOptions").append("<tr><td><input onkeyup='CreateFieldOptionOnUI();' type='text' class='form-control LabelText' value='" + STR + "' option-id-val='00000000-0000-0000-0000-000000000000' /></td><td class='uk-text-center'><a href='javascript:;' onclick='RemoveFieldOptionRow(this)'><i class='md-icon material-icons fa fa-minus-circle'></i></a></td></tr>");
    } else {
        $("#tabs_from_customfield_FieldProperty_form_tblOptions").append("<tr><td><input onkeyup='CreateFieldOptionOnUI();' type='text' class='form-control LabelText' value='" + STR + "' option-id-val='00000000-0000-0000-0000-000000000000' /></td><td><input name='optionsRadioAns' value='" + STR + "' type='radio' radiobtnval='" + STR + "' onclick='CurrectAnswer(this);'></td><td><input onkeyup='CreateFieldOptionOnUI();' type='text' class='form-control OptionWeightage' value='0' option-weightage='0' /></td><td class='uk-text-center'><a href='javascript:;' onclick='RemoveFieldOptionRow(this)'><i class='fa fa-minus-circle'></i></a></td></tr>");
    }
    CreateFieldOptionOnUI();
}

function CurrectAnswer(obj) {
    if ($("#tabs_from_customfield_FromSelection_CommanForm li[data-type='RadioButton'].active").length > 0) {
        $("#tabs_from_customfield_FromSelection_CommanForm li[data-type='RadioButton'].active div.field-option").attr("CurrectAnswer", "");
        $("#tabs_from_customfield_FromSelection_CommanForm li[data-type='RadioButton'].active div.field-option").attr("CurrectAnswer", $(obj).attr("radiobtnval"));
    }

    if ($("#tabs_from_customfield_FromSelection_CommanForm li[data-type='CheckBox'].active").length > 0) {
        $("#tabs_from_customfield_FromSelection_CommanForm li[data-type='CheckBox'].active div.field-option").attr("CurrectAnswer", "");
        $("#tabs_from_customfield_FromSelection_CommanForm li[data-type='CheckBox'].active div.field-option").attr("CurrectAnswer", $(obj).attr("radiobtnval"));
    }

    if ($("#tabs_from_customfield_FromSelection_CommanForm li[data-type='Selection'].active").length > 0) {
        $("#tabs_from_customfield_FromSelection_CommanForm li[data-type='Selection'].active div.field-option").attr("CurrectAnswer", "");
        $("#tabs_from_customfield_FromSelection_CommanForm li[data-type='Selection'].active div.field-option").attr("CurrectAnswer", $(obj).attr("radiobtnval"));
    }

    $("#tabs_from_customfield_FieldProperty_form_tblOptions input[type='text'].LabelText").each(function () {
        if ($(this).val() == $(obj).attr("radiobtnval")) {
            $(this).attr("isselected", "true");
        } else {
            $(this).attr("isselected", "false");
        }
    });
}

function CreateFieldOptionOnUI() {
    var STR = "";
    var varUI = "";
    var varOption = "";
    var varOptionIDs = "";
    var varOptionWeightage = "";

    if ($("#tabs_from_customfield_FromSelection_CommanForm li[data-type='CheckBox'].active").length > 0) {
        STR = "Check Box";
        $("#tabs_from_customfield_FieldProperty_form_tblOptions input[type='text'].LabelText").each(function () {
            varUI = varUI + " <label><i class='material-icons fa fa-check-square'></i> " + $(this).val() + "</label>";
            varOption = varOption + $(this).val() + "~";
            varOptionIDs = varOptionIDs + $(this).attr("option-id-val") + "~";
            if ($(this).attr("isselected") == "true") {
                $("#tabs_from_customfield_FromSelection_CommanForm li[data-type='CheckBox'].active div.field-option").attr("CurrectAnswer", "");
                $("#tabs_from_customfield_FromSelection_CommanForm li[data-type='CheckBox'].active div.field-option").attr("CurrectAnswer", $(this).val());
            }
            $(this).closest("tr").find("td:eq(1)").find("input[type='radio']").val($(this).val());
            $(this).closest("tr").find("td:eq(1)").find("input[type='radio']").attr("radiobtnval", $(this).val());
        });
        $("#tabs_from_customfield_FieldProperty_form_tblOptions input[type='text'].OptionWeightage").each(function () {
            varOptionWeightage = varOptionWeightage + $(this).val() + "~";
        });
        $("#tabs_from_customfield_FromSelection_CommanForm li[data-type='CheckBox'].active div.field-option").html("");
        $("#tabs_from_customfield_FromSelection_CommanForm li[data-type='CheckBox'].active div.field-option").attr("option", "");
        $("#tabs_from_customfield_FromSelection_CommanForm li[data-type='CheckBox'].active div.field-option").attr("option-id", "");
        $("#tabs_from_customfield_FromSelection_CommanForm li[data-type='CheckBox'].active div.field-option").attr("option-weightage", "");
        $("#tabs_from_customfield_FromSelection_CommanForm li[data-type='CheckBox'].active div.field-option").append(varUI);
        $("#tabs_from_customfield_FromSelection_CommanForm li[data-type='CheckBox'].active div.field-option").attr("option", varOption);
        $("#tabs_from_customfield_FromSelection_CommanForm li[data-type='CheckBox'].active div.field-option").attr("option-id", varOptionIDs);
        $("#tabs_from_customfield_FromSelection_CommanForm li[data-type='CheckBox'].active div.field-option").attr("option-weightage", varOptionWeightage);
    }
    else if ($("#tabs_from_customfield_FromSelection_CommanForm li[data-type='RadioButton'].active").length > 0) {
        STR = "Radio Button";
        $("#tabs_from_customfield_FieldProperty_form_tblOptions input[type='text'].LabelText").each(function () {
            varUI = varUI + " <label><i class='material-icons fa fa-dot-circle-o'></i> " + $(this).val() + "</label>";
            varOption = varOption + $(this).val() + "~";
            varOptionIDs = varOptionIDs + $(this).attr("option-id-val") + "~";
            if ($(this).attr("isselected") == "true") {
                $("#tabs_from_customfield_FromSelection_CommanForm li[data-type='RadioButton'].active div.field-option").attr("CurrectAnswer", "");
                $("#tabs_from_customfield_FromSelection_CommanForm li[data-type='RadioButton'].active div.field-option").attr("CurrectAnswer", $(this).val());
            }
            $(this).closest("tr").find("td:eq(1)").find("input[type='radio']").val($(this).val());
            $(this).closest("tr").find("td:eq(1)").find("input[type='radio']").attr("radiobtnval", $(this).val());
        });
        $("#tabs_from_customfield_FieldProperty_form_tblOptions input[type='text'].OptionWeightage").each(function () {
            varOptionWeightage = varOptionWeightage + $(this).val() + "~";
        });
        $("#tabs_from_customfield_FromSelection_CommanForm li[data-type='RadioButton'].active div.field-option").html("");
        $("#tabs_from_customfield_FromSelection_CommanForm li[data-type='RadioButton'].active div.field-option").attr("option", "");
        $("#tabs_from_customfield_FromSelection_CommanForm li[data-type='RadioButton'].active div.field-option").attr("option-id", "");
        $("#tabs_from_customfield_FromSelection_CommanForm li[data-type='RadioButton'].active div.field-option").attr("option-weightage", "");
        $("#tabs_from_customfield_FromSelection_CommanForm li[data-type='RadioButton'].active div.field-option").append(varUI);
        $("#tabs_from_customfield_FromSelection_CommanForm li[data-type='RadioButton'].active div.field-option").attr("option", varOption);
        $("#tabs_from_customfield_FromSelection_CommanForm li[data-type='RadioButton'].active div.field-option").attr("option-id", varOptionIDs);
        $("#tabs_from_customfield_FromSelection_CommanForm li[data-type='RadioButton'].active div.field-option").attr("option-weightage", varOptionWeightage);
    }
    else if ($("#tabs_from_customfield_FromSelection_CommanForm li[data-type='Panel'].active").length > 0) {
        STR = "Label";
        $("#tabs_from_customfield_FieldProperty_form_tblLabelOptions input[type='text'].LabelText").each(function () {
            varUI = varUI + " <label>" + $(this).val() + "</label><br/>";
            varOption = varOption + $(this).val() + "~";
            varOptionIDs = varOptionIDs + $(this).attr("option-id-val") + "~";
        });

        $("#tabs_from_customfield_FromSelection_CommanForm li[data-type='Panel'].active div.field-option").html("");
        $("#tabs_from_customfield_FromSelection_CommanForm li[data-type='Panel'].active div.field-option").attr("option", "");
        $("#tabs_from_customfield_FromSelection_CommanForm li[data-type='Panel'].active div.field-option").attr("option-id", "");
        $("#tabs_from_customfield_FromSelection_CommanForm li[data-type='Panel'].active div.field-option").append(varUI);
        $("#tabs_from_customfield_FromSelection_CommanForm li[data-type='Panel'].active div.field-option").attr("option", varOption);
        $("#tabs_from_customfield_FromSelection_CommanForm li[data-type='Panel'].active div.field-option").attr("option-id", varOptionIDs);
    }
    else if ($("#tabs_from_customfield_FromSelection_CommanForm li[data-type='Selection'].active").length > 0) {
        STR = "Select Option";
        $("#tabs_from_customfield_FieldProperty_form_tblOptions input[type='text'].LabelText").each(function () {
            varOption = varOption + $(this).val() + "~";
            varOptionIDs = varOptionIDs + $(this).attr("option-id-val") + "~";
            if ($(this).attr("isselected") == "true") {
                $("#tabs_from_customfield_FromSelection_CommanForm li[data-type='Selection'].active div.field-option").attr("CurrectAnswer", "");
                $("#tabs_from_customfield_FromSelection_CommanForm li[data-type='Selection'].active div.field-option").attr("CurrectAnswer", $(this).val());
            }
            $(this).closest("tr").find("td:eq(1)").find("input[type='radio']").val($(this).val());
            $(this).closest("tr").find("td:eq(1)").find("input[type='radio']").attr("radiobtnval", $(this).val());
        });
        $("#tabs_from_customfield_FromSelection_CommanForm li[data-type='Selection'].active div.field-option").attr("option", "");
        $("#tabs_from_customfield_FromSelection_CommanForm li[data-type='Selection'].active div.field-option").attr("option-id", "");
        $("#tabs_from_customfield_FromSelection_CommanForm li[data-type='Selection'].active div.field-option").attr("option", varOption);
        $("#tabs_from_customfield_FromSelection_CommanForm li[data-type='Selection'].active div.field-option").attr("option-id", varOptionIDs);
    }
}

$(document).ready(function () {

    $(".btnPrevious, .btnSubmit").hide();

    $("#m_tabs_5_1").click(function () {
        $("#frmQuestionnaireSetup").valid();
        if ($("#frmQuestionnaireSetup .tab-content .active .field-validation-error").length > 0 || $("#frmQuestionnaireSetup .tab-content .active .field-validation-error").length > 0) { }
        else {
            $("#tab1").fadeIn();
            $("#tab1").addClass("active");
            $("#tab2").removeClass("active");
            $(this).closest("ul").children("li").removeClass("active");
            $(this).closest("li").addClass("active");
            $("#tab2").hide();
            $(".btnPrevious").hide();
            $(".btnSubmit").hide();
            $(".btnNext").show();
            $(".bs-select").selectpicker('refresh');
        }
    });

    $("#m_tabs_5_2").click(function () {
        $("#frmQuestionnaireSetup").valid();
        if ($("#frmQuestionnaireSetup .tab-content .active .field-validation-error").length > 0 || $("#frmQuestionnaireSetup .tab-content .active .field-validation-error").length > 0) { }
        else {
            $("#tab2").fadeIn();
            $("#tab2").addClass("active");
            $("#tab1").removeClass("active");
            $(this).closest("ul").children("li").removeClass("active");
            $(this).closest("li").addClass("active");
            $("#tab1").hide();
            $(".btnPrevious").show();
            $(".btnSubmit").show();
            $(".btnNext").hide();
            $(".bs-select").selectpicker('refresh');
        }
    });
});

$(".btnNext").click(function () {
    $("#frmQuestionnaireSetup").valid();
    if ($("#frmQuestionnaireSetup .tab-content .active .field-validation-error").length > 0 || $("#frmQuestionnaireSetup .tab-content .active .input-validation-error").length > 0) { }
    else {
        var selA = $(".employee").find("li.active").children("a").attr("id");
        if (selA == "m_tabs_5_1") {
            $("#m_tabs_5_2").trigger("click");
            $("#tab2").addClass("active");
            $("#tab1,#tab3").removeClass("active");
            $(".btnPrevious").show();
            $(".btnNext").hide();
            $(".btnSubmit").show();
        }
    }
});

$(".btnPrevious").click(function () {
    $("#frmQuestionnaireSetup").valid();
    if ($("#frmQuestionnaireSetup .tab-content .active .field-validation-error").length > 0 || $("#frmQuestionnaireSetup .tab-content .active .input-validation-error").length > 0) { }
    else {
        var selA = $(".employee").find("li.active").children("a").attr("id");

        if (selA == "m_tabs_5_2") {
            $("#m_tabs_5_1").trigger("click");
            $("#tab1").addClass("active");
            $("#tab2,#tab3").removeClass("active");
            $(".btnPrevious").hide();
            $(".btnNext").show();
            $(".btnSubmit").hide();
        }
    }
});

$("#tabs_from_customfield_FormView li").click(function () {
    $("#tabs_from_customfield_FormView li").removeClass("uk-active");
    $(this).addClass("uk-active");
})

//// Section Option Events Start
$("#tabs_from_customfield_SectionProperty_form_txtSectionName").keyup(function () {
    if ($(this).val().trim() != "") {
        $("#tabs_from_customfield_FromSelection_CommanForm li[data-type='Section'].active h3.section-option").attr("section-name", $(this).val())
        $("#tabs_from_customfield_FromSelection_CommanForm li[data-type='Section'].active h3.section-option label.dsp-label").text($(this).val())
    }
    else {
        $("#tabs_from_customfield_FromSelection_CommanForm li[data-type='Section'].active h3.section-option").attr("section-name", "Section Name")
        $("#tabs_from_customfield_FromSelection_CommanForm li[data-type='Section'].active h3.section-option label.dsp-label").text("Section Name")
    }
});

$("#tabs_from_customfield_SectionProperty_form_chkIsOptional").click(function () {
    if ($(this).is(":checked")) {
        $("#tabs_from_customfield_FromSelection_CommanForm li[data-type='Section'].active h3.section-option").attr("isoptional", "true")
    }
    else {
        $("#tabs_from_customfield_FromSelection_CommanForm li[data-type='Section'].active h3.section-option").attr("isoptional", "false")
    }
})

$("input[type='radio'][name='tabs_from_customfield_SectionProperty_form_chkIsInPagePopupTab']").click(function () {
    $("#tabs_from_customfield_FromSelection_CommanForm li[data-type='Section'].active h3.section-option").attr("ison", $(this).val())
})

$("#tabs_from_customfield_SectionProperty_form_txtSectionDetail").keyup(function () {
    $("#tabs_from_customfield_FromSelection_CommanForm li[data-type='Section'].active h3.section-option").attr("section-detail", $(this).val())
});

$("#tabs_from_customfield_SectionProperty_form_txtWeightage").keyup(function () {
    $("#tabs_from_customfield_FromSelection_CommanForm li[data-type='Section'].active h3.section-option").attr("section-weightage", $(this).val())
});
//// Section Option Events End

//// Section Option Field Start
$("#tabs_from_customfield_FieldProperty_form_txtDisplayName").keyup(function () {
    if ($(this).val().trim() != "") {
        $("#tabs_from_customfield_FromSelection_CommanForm li[onclick='fnSetFieldProperty(this)'].active div.field-option").attr("dis-lab", $(this).val());
        $("#tabs_from_customfield_FromSelection_CommanForm li[onclick='fnSetFieldProperty(this)'].active label.dsp-label").text($(this).val());
    }
    else {
        $("#tabs_from_customfield_FromSelection_CommanForm li[onclick='fnSetFieldProperty(this)'].active div.field-option").attr("dis-lab", "Display Lable");
        $("#tabs_from_customfield_FromSelection_CommanForm li[onclick='fnSetFieldProperty(this)'].active label.dsp-label").text("Display Lable");
    }
});

$("#tabs_from_customfield_FieldProperty_form_ddlLTRTerm").change(function () {
    if ($(this).val().trim() != "") {
        $("#tabs_from_customfield_FromSelection_CommanForm li[onclick='fnSetFieldProperty(this)'].active div.field-option").attr("ltr", $(this).val());
    }
    else {
        $("#tabs_from_customfield_FromSelection_CommanForm li[onclick='fnSetFieldProperty(this)'].active div.field-option").attr("ltr", "Left");
    }
})

$("#tabs_from_customfield_FieldProperty_form_txtToolTip").keyup(function () {
    $("#tabs_from_customfield_FromSelection_CommanForm li[onclick='fnSetFieldProperty(this)'].active div.field-option").attr("tool-tip", $(this).val());
});

$("#tabs_from_customfield_FieldProperty_form_txtQuestionWeightage").keyup(function () {
    $("#tabs_from_customfield_FromSelection_CommanForm li[onclick='fnSetFieldProperty(this)'].active div.field-option").attr("QuestionWeightage", $(this).val());
});

$("#tabs_from_customfield_FieldProperty_form_chkFieldIsMultiselection").click(function () {
    if ($(this).is(":checked")) {
        $("#tabs_from_customfield_FromSelection_CommanForm li[onclick='fnSetFieldProperty(this)'].active div.field-option").attr("ismultiselect", "true")
    }
    else {
        $("#tabs_from_customfield_FromSelection_CommanForm li[onclick='fnSetFieldProperty(this)'].active div.field-option").attr("ismultiselect", "false")
    }
})

$("#tabs_from_customfield_FieldProperty_form_chkFieldIsOptional").click(function () {
    if ($(this).is(":checked")) {
        $("#tabs_from_customfield_FromSelection_CommanForm li[onclick='fnSetFieldProperty(this)'].active div.field-option").attr("isoptional", "true")
    }
    else {
        $("#tabs_from_customfield_FromSelection_CommanForm li[onclick='fnSetFieldProperty(this)'].active div.field-option").attr("isoptional", "false")
    }
})

$("#tabs_from_customfield_FieldProperty_form_chkCommentIsOptional").click(function () {
    if ($(this).is(":checked")) {
        $("#tabs_from_customfield_FromSelection_CommanForm li[onclick='fnSetFieldProperty(this)'].active div.field-option").attr("isCommentOptional", "true")
    }
    else {
        $("#tabs_from_customfield_FromSelection_CommanForm li[onclick='fnSetFieldProperty(this)'].active div.field-option").attr("isCommentOptional", "false")
    }
})

$("#tabs_from_customfield_FieldProperty_form_chkIsHeader").click(function () {
    if ($(this).is(":checked")) {
        $("#tabs_from_customfield_FromSelection_CommanForm li[onclick='fnSetFieldProperty(this)'].active div.field-option").attr("isheader", "true")
    }
    else {
        $("#tabs_from_customfield_FromSelection_CommanForm li[onclick='fnSetFieldProperty(this)'].active div.field-option").attr("isheader", "false")
    }
})

$("#tabs_from_customfield_FieldProperty_form_Length").keyup(function () {
    if ($(this).val().trim() != "" && !isNaN($(this).val().trim())) {
        $("#tabs_from_customfield_FromSelection_CommanForm li[onclick='fnSetFieldProperty(this)'].active div.field-option").attr("length", $(this).val())
    }
    else {
        $("#tabs_from_customfield_FromSelection_CommanForm li[onclick='fnSetFieldProperty(this)'].active div.field-option").attr("length", "")
        $(this).val("");
    }
});

$("#tabs_from_customfield_FieldProperty_form_txtRows").keyup(function () {
    if ($(this).val().trim() != "" && !isNaN($(this).val().trim())) {
        $("#tabs_from_customfield_FromSelection_CommanForm li[onclick='fnSetFieldProperty(this)'].active div.field-option").attr("row", $(this).val())
    }
    else {
        $("#tabs_from_customfield_FromSelection_CommanForm li[onclick='fnSetFieldProperty(this)'].active div.field-option").attr("row", "")
        $(this).val("");
    }
});
//// Section Option Field End

function fnAddFieldAndSectionOnClick(obj) {
    //if ($(obj).attr("data-type") == "Section") {
    //    fnCreateFieldByFieldType($(obj).attr("data-type"), $("#tabs_from_customfield_FromSelection_CommanForm"))
    //}
    //else {
    //    if ($("#tabs_from_customfield_FromSelection_CommanForm li[data-type='Section'].active").length > 0)
    //        fnCreateFieldByFieldType($(obj).attr("data-type"), $("#tabs_from_customfield_FromSelection_CommanForm li[data-type='Section'].active ol[data-type='Section']"))
    //    else
    //        UIkit.modal.alert("Section is not selected...!!!, Select a section where you want to add field");
    //}
}

var SectionDelObj;
function fnremovethiselements(obj) {

    SectionDelObj = obj;
    if ($(obj).parent("li").attr("data-type").trim().toUpperCase() == "SECTION") {
        var SectionID = $(obj).next("h3").attr("data-id");
        if (SectionID.trim() != "" && SectionID.trim() != "00000000-0000-0000-0000-000000000000") {
            $("#SectionConfirmationModal").modal('show');
        } else if (SectionID.trim() == "00000000-0000-0000-0000-000000000000") {
            $(obj).parent("li").remove();
        }
    } else {
        var CustomFieldID = $(obj).next("label").next("div.field-option").attr("data-id");
        if (CustomFieldID.trim() != "" && CustomFieldID.trim() != "00000000-0000-0000-0000-000000000000") {
            $("#QuestionConfirmationModal").modal('show');
        } else if (CustomFieldID.trim() == "00000000-0000-0000-0000-000000000000") {
            $(obj).parent("li").remove();
        }
    }
}

function fnCustomfield_details_CommanFormSubmit() {
    if ($("#tabs_from_customfield_FromSelection_CommanForm li").length > 0) {
        var fncdcfs_Sections = [];
        var fncdcfs_Fields = [];
        var fncdcfs_Fields_Options = [];
        $("#tabs_from_customfield_FromSelection_CommanForm li[data-type='Section']").each(function (i) {
            var isOptional = true;

            if ($(this).find("h3.section-option").attr("isoptional").trim().toUpperCase() == "TRUE") {
                isOptional = true;
            }
            else {
                isOptional = false;
            }
            var $objSec = $(this);


            fncdcfs_Sections.push({
                TemplateID: $("#TemplateID").val(),
                TemplateSectionID: $(this).find("h3.section-option").attr("data-id").trim(),
                SectionName: $(this).find("h3.section-option").attr("section-name").trim(),
                SectionDetail: $(this).find("h3.section-option").attr("section-detail").trim(),
                TotalWeightage: $(this).find("h3.section-option").attr("section-weightage").trim(),
                OrderNo: i + 1,
                SectionNumber: i + 1,
                IsOptional: isOptional,
            });

            $(this).find("li.highlight").each(function (j) {
                isOptional = true;
                var IsCommentRequired = false;
                var IsHeader = false;
                var $objSecQues = $(this);

                if ($(this).find("div.field-option").attr("isoptional").trim().toUpperCase() == "TRUE") {
                    isOptional = true;
                }
                else {
                    isOptional = false;
                }

                if ($(this).find("div.field-option").attr("isCommentOptional").trim().toUpperCase() == "TRUE") {
                    IsCommentRequired = false;
                }
                else {
                    IsCommentRequired = true;
                }

                if ($(this).find("div.field-option").attr("isheader").trim().toUpperCase() == "TRUE") {
                    IsHeader = true;
                }
                else {
                    IsHeader = false;
                }

                fncdcfs_Fields.push({
                    TempSecQuestionID: $(this).find("div.field-option").attr("data-id").trim(),
                    TemplateID: $("#TemplateID").val(),
                    TemplateSectionID: $($objSec).find("h3.section-option").attr("data-id").trim(),
                    QuestionTitle: $(this).find("div.field-option").attr("dis-lab").trim(),
                    OrderNo: j + 1,
                    QuestionWeightage: $(this).find("div.field-option").attr("QuestionWeightage").trim(),
                    QuestionType_Term: $(this).attr("data-type").trim(),
                    IsOptional: isOptional,
                    IsCommentRequired: IsCommentRequired,
                    TextLength: $(this).find("div.field-option").attr("length").trim(),
                    TextAreaRows: $(this).find("div.field-option").attr("row").trim(),
                    SectionOrderNo: i + 1,
                    IsHeader: IsHeader,
                    //IdealValue: $(this).find("div.field-option").attr("option").trim(),  
                });

                if ($(this).find("div.field-option").attr("option").trim() != '' && $(this).find("div.field-option").attr("option-id").trim() != '') {

                    var varOption = $(this).find("div.field-option").attr("option").split("~");
                    var OptionIDs = $(this).find("div.field-option").attr("option-id").split("~");
                    var OptionWeightage = "";

                    if ($(this).find("div.field-option").attr("option-weightage").trim() != '') {
                        OptionWeightage = $(this).find("div.field-option").attr("option-weightage").split("~");
                    }

                    for (key in varOption) {
                        if (varOption[key].trim() != "") {

                            var IsTrueAns = false;
                            if ($($objSecQues).find("div.field-option").attr("CurrectAnswer").trim() != '' && $($objSecQues).find("div.field-option").attr("CurrectAnswer").trim() == varOption[key]) {
                                IsTrueAns = true;
                            }

                            fncdcfs_Fields_Options.push({
                                TempSectionQueOptionID: OptionIDs[key],
                                TempSectionQuestionID: $($objSecQues).find("div.field-option").attr("data-id").trim(),
                                OptionText: varOption[key],
                                OrderNo: parseInt(key) + 1,
                                QuestionType_Term: $($objSecQues).attr("data-type").trim(),
                                TotalWeightage: OptionWeightage[key],
                                IsTrueAns: IsTrueAns,
                                FieldOrderNo: j + 1,
                                SectionOrderNoForOption: i + 1
                            });

                        }
                    }
                }
            });
        });

        if (fncdcfs_Fields.length != 0 && fncdcfs_Sections.length != 0) {

            $("#fncdcfs_Sections").val(JSON.stringify(fncdcfs_Sections));
            $("#fncdcfs_Fields").val(JSON.stringify(fncdcfs_Fields));
            $("#fncdcfs_Fields_Options").val(JSON.stringify(fncdcfs_Fields_Options));

            $("#frmQuestionnaireSetup").submit();
        } else {
            toastr["error"]("Please create custom form", "Questionnaire Setup", { "positionClass": "toast-bottom-right", "progressBar": true });
        }
    } else {
        toastr["error"]("Please create custom form", "Questionnaire Setup", { "positionClass": "toast-bottom-right", "progressBar": true });
    }
}

function SaveQuestionnaireSetupResponse(response) {
    if (response == "_Logon_") {
        window.location.href = "/Login/Login";
    }
    else {
        if (response.IsSuccess) {
            SetMessageForPageLoad(response.Message, "QuestionnaireSetupsaved");
            window.location.href = "/Setup/QuestionnaireSetup/QuestionnaireSetups/";
        } else {
            ShowMessage(response.Message, "error");
        }
    }
}

function DeleteSection() {
    if ($(SectionDelObj).parent("li").attr("data-type").trim().toUpperCase() == "SECTION") {
        var SectionID = $(SectionDelObj).next("h3").attr("data-id");
        if (SectionID.trim() != "" && SectionID.trim() != "00000000-0000-0000-0000-000000000000") {
            ShowLoader();
            $.ajax({
                url: "/Setup/QuestionnaireSetup/DeleteSectionData",
                type: "POST",
                data: { data: SectionID },
                success: function (data) {
                    if (data == "_Logon_") {
                        window.location.href = "/Login/Login";
                    }
                    else {
                        $("#SectionConfirmationModal").modal('hide');
                        if (data != null) {
                            if (data.IsSuccess) {
                                $(SectionDelObj).parent("li").remove();
                            }
                        }
                    }
                    HideLoader();
                }
            });
        }
    }
}

function DeleteSectionQuestion() {
    var CustomFieldID = $(SectionDelObj).next("label").next("div.field-option").attr("data-id");
    if (CustomFieldID.trim() != "" && CustomFieldID.trim() != "00000000-0000-0000-0000-000000000000") {
        ShowLoader();
        $.ajax({
            url: "/Setup/QuestionnaireSetup/DeleteSectionQuestionData",
            type: "POST",
            data: { data: CustomFieldID },
            success: function (data) {
                if (data == "_Logon_") {
                    window.location.href = "/Login/Login";
                }
                else {
                    $("#QuestionConfirmationModal").modal('hide');
                    if (data != null) {
                        if (data.IsSuccess) {
                            $(SectionDelObj).parent("li").remove();
                        }
                    }
                }
                HideLoader();
            }
        });
    }
}