﻿// For Add Add Functionality
function InitNoteCloning() {

    try {

        $('.main-multi-field-wrapper').each(function () {
            //$('.select2').each(function(){$(this).select2("destroy")});

            var $wrapper = $('.sub-multi-fields', this);
            var $this = $(this);

            $(".add-note", $this).click(function (e) {


                debugger;
                var $len = $('.multi-field', $wrapper).length;
                var $clone = $('.multi-field:first-child', $wrapper);

                if ($len == 20) {
                    alert('Maximum Limit Reached please remove item from list...')
                    return false;
                }
                $clone.clone().appendTo($wrapper).find('input,textarea,file').each(function () {
                    /// $len = $len + 1;
                    debugger;
                    if ($(this).hasClass("clr")) {

                        $(this).attr('id', $(this).attr('id'), $len);
                        var tt = $(this).attr('id', $(this).attr('id'));
                        var ctrl = tt[0].id;
                        var newctrlid = ctrl + '_' + $len;
                        var ctrl = tt.attr("id", newctrlid);
                        debugger;
                        $(this).attr('name', $(this).attr('name').replace(/\d+/g, $len));
                        if ($(this).hasClass("txtnote")) {
                            debugger;
                            $(ctrl).val('Note ID:0' + $len + 1);
                        }


                    }
                    else {
                        $(this).attr('id', $(this).attr('id'), $len);
                        var tt = $(this).attr('id', $(this).attr('id'));
                        var ctrl = tt[0].id;
                        var newctrlid = ctrl + '_' + $len;
                        var ctrl = tt.attr("id", newctrlid);
                        $(this).attr('name', $(this).attr('name').replace(/\d+/g, $len));
                        if ($(this).hasClass("txtnote")) {
                            debugger;
                            var sr = parseInt($len) + 1
                            $(ctrl).val('Note ID:0' + sr);
                        }


                    }

                    //






                });
                $('.multi-field', $wrapper).each(function (idx) {


                    var drp = $(this).closest('td').find('select').attr("id", idx + 1)
                    var lnk = $(this).closest('td').find('a').attr("id", idx + 1)
                    var $row = $(this).closest("tr:first-child").attr("id", idx + 1);

                    $(this).find('td:first-child').html(idx + 1);
                });
                ///// select2 = $('.select2').select2();
            });
            $($wrapper).on("click", ".multi-field .remove-note", function (e) {

                var dltid = $(this).attr('data-dlt-id');
                var $cthis = $(this);
                debugger;

                if (dltid) {
                    var cnfirm = confirm("Deleted data cant't be Recoverd");
                    if (cnfirm) {
                        $.post('/PartyMaster/deletePartyMasterAttachment', { id: dltid }, function (d) {
                            if (d == "Success") {
                                RemoveColumn($wrapper, $this, $cthis)

                            }
                        });
                    }
                }
                else {
                    RemoveColumn($wrapper, $this, $cthis);

                }
                $('.qty').trigger("keyup");
            });
        });
        function RemoveColumn($wrapper, $this, $cthis) {
            if ($('.multi-field', $wrapper).length > 1) {
                ////select2.select2('destroy');

                $cthis.closest('tr').remove();
                $('.multi-field', $wrapper).each(function (indx) {
                    $(this).find('input,select').each(function () {
                        var $ths = $(this);

                        $ths.attr('name', $ths.attr('name').replace(/\d+/g, indx));
                    });
                });
                //// select2 = $('.select2').select2();
            }
            else if ($('.multi-field', $wrapper).length==0) {
                $("#divadd").hide();
                $(".btnaddnote").show();
            }
            //TotlaAmt($wrapper, $this);
            $('.multi-field', $wrapper).each(function (idx) {
                $(this).find('td:first-child').html(idx + 1);
            });
        }
    } catch (e) {

    }

};