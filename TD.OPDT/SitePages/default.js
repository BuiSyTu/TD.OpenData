(function (factory) {
    factory(jQuery, tdcore.views, tdcore.lhapis, tdcore.modals, tdcore.forms);
})(function ($, views, dnapis, modals, forms) {
    "use strict";
    //== Class definition
    var officeRemoteAjax = function () {
        //== Private functions
        var loadDatatable = function () {
            views.table('.td-datatable', {
                "serverSide": true,
            })
                .useDataLoader(
                    new views.TDApiDataLoader(
                        new TD.MT.DiemONhiems().items
                    )
                )
                .addCheckColumn()
                .addIndexColumn('STT')
                .addColumn(
                    {
                        title: "Tên điểm ô nhiễm",
                        data: "Title",
                        searchable: false,
                        orderable: false
                    },
                    {
                        title: "Thao tác",
                        render: function (data, type, row, meta) {
                            var source = $("#action-template").html();
                            var template = Handlebars.compile(source);
                            return template(row);
                        },
                        orderable: false,
                        searchable: false
                    })
                .build();
        };

        return {
            // public functions
            init: function () {
                loadDatatable();
                $('.td-datatable').on('click', '[edit]', function () {
                    var id = $(this).attr("data-id");
                    office.AddOrEdit(id);
                }).on('click', '[delete]', function () {
                    var id = $(this).attr("data-id");
                    office.Delete(id);
                });
            },
        };
    }();

    jQuery(document).ready(function () {
        officeRemoteAjax.init();
        $('[add]').click(function () {
            var id = $(this).attr("data-id");
            office.AddOrEdit(id);
        });
        $('[delete]').click(function () {
            var id = $(this).attr("data-id");
            office.Delete(id);
        });
    });

    var office = {};

    office.AddOrEdit = function (id) {
        modals.modal(id ? 'Sửa thông tin ' : 'Thêm mới ')
            .content($('#addnew-template').html())
            .size(600, 250)
            .ready(function (mdl) {
                return forms.WidgetActivator.parse(mdl.panel.content)
                    .then(function () {
                        if (id) {
                            return new TD.MT.DiemONhiems().getSingle(id)
                                .then(function (res) { return res.json().result })
                                .then(function (data) {
                                    var form = forms.Widget.findWidgets(mdl.panel.content, false, forms.Form)[0];

                                    form.setData(data);
                                }).catch(function () {
                                    console.log("Post error: " + error);
                                });
                        }
                    });
            })
            .okCancel()
            .addCmd('OK', function (mdl, prevResult) {
                var form = forms.Widget.findWidgets(mdl.panel.content, false, forms.Form)[0];
                return form.tryValidate()
                    .then(async function () {
                        var val = form.getData();
                        mdl.data = val;
                        if (id) {
                            return new Promise(function (resolve) {

                                new TD.MT.DiemONhiems().update(id, val)
                                    .then(function () {
                                        toastr.success('Sửa thành công!');
                                        var table = $('.td-datatable').DataTable();
                                        table.ajax.reload();
                                        resolve();
                                    })
                                    .catch(function () {
                                        toastr.info('Lưu thất bại!');
                                    });
                            });
                        }
                        else {
                            return new Promise(function (resolve) {
                                new TD.MT.DiemONhiems().add(val)
                                    .then(function () {
                                        toastr.success('Thêm thành công!');
                                        var table = $('.td-datatable').DataTable();
                                        table.ajax.reload();
                                        resolve();
                                    }).catch(function () {
                                        toastr.info('Lưu thất bại!');
                                    });
                            });
                        }
                    })
                    .catch(function () {
                        return false;
                    });

            })
            .show();
    }

    office.Delete = function (id) {
        var officeApi = new TD.MT.DiemONhiems();
        if (id) {
            if (confirm("Bạn thực sự muốn xóa đơn vị này?")) {

                officeApi.delete(id).then(function (data) {
                    if (data.status == 200) {
                        toastr.success("Thực hiện thành công");
                        var table = $('.td-datatable').DataTable();
                        table.ajax.reload();
                    } else {
                        toastr.error("Thực hiện không thành công");
                    }
                });
            }
        } else {
            var table = $('.td-datatable').DataTable();
            var length = table.rows('.selected').data().length;
            var selected = table.rows('.selected').data();
            if (length <= 0) {
                toastr.warning("Bạn chưa chọn đơn vị nào!");
            } else if (length > 0) {
                if (confirm("Bạn thực sự muốn xóa đơn vị này?")) {
                    for (var i = 0; i < length; i++) {
                        officeApi.delete(selected[i].ID).then(function (data) {
                            if (data.status == 200) {
                                toastr.success("Thực hiện thành công");
                                var table = $('.td-datatable').DataTable();
                                table.ajax.reload();
                            } else {
                                toastr.error("Thực hiện không thành công");
                            }
                        });
                    }
                }
            }
        }
    };
});

