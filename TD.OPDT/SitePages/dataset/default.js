var dataSetApi = (function () {
    var apiPrefix = 'opdtapi';
    var controllerName = 'datasets';

    function getById(id) {
        return new Promise(function (resolve, reject) {
            $.ajax({
                type: 'GET',
                url: `/${apiPrefix}/${controllerName}/${id}`,
                success: function (res) { resolve(res.result) },
                error: function (e) { reject(e) }
            })
        });
    }

    function add({ Name, Code, Order, Active, FieldId, OfficeId, Attachments, LinkApi }) {
        return new Promise(function (resolve, reject) {
            $.ajax({
                type: 'POST',
                url: `/${apiPrefix}/${controllerName}`,
                data: JSON.stringify({
                    Name, Code, Order, Active, FieldId, OfficeId, Attachments, LinkApi,
                }),
                contentType: 'application/json',
                success: function (res) { resolve(res.result) },
                error: function (e) { reject(e) },
            });
        });
    }

    function update(id, { Name, Code, Order, Active, FieldId, OfficeId, Attachments, LinkApi }) {
        return new Promise(function (resolve, reject) {
            $.ajax({
                type: 'PUT',
                url: `/${apiPrefix}/${controllerName}/${id}`,
                data: JSON.stringify({
                    Name, Code, Order, Active, FieldId, OfficeId, Attachments, LinkApi,
                }),
                contentType: 'application/json',
                success: function () { resolve() },
                error: function (e) { reject(e) },
            });
        });
    }

    function _delete(id) {
        return new Promise(function (resolve, reject) {
            $.ajax({
                type: 'DELETE',
                url: `/${apiPrefix}/${controllerName}/${id}`,
                contentType: 'application/json',
                success: function () { resolve() },
                error: function (e) { reject(e) },
            });
        });
    }

    return {
        getById,
        add,
        update,
        delete: _delete
    }
})();

var dataSetDataTable = (function () {
    var table

    var columnsDefine = [
        {
            title: 'STT',
            data: null,
            render: function (data, type, full, meta) {
                return meta.row + 1;
            },
            class: 'text-center',
        },
        {
            title: 'Mã',
            data: "Code",
            class: 'text-center',
        },
        {
            title: 'Tên lĩnh vực',
            data: "Name",
            class: 'text-center',
        },
        {
            title: 'Thứ tự',
            data: "Order",
            class: 'text-center',
        },
        {
            title: 'Sử dụng',
            data: null,
            class: 'text-center',
            render: function (data, type, full, meta) {
                return `<label class="m-checkbox m-checkbox--single  m-checkbox--success m-checkbox">
                            <input type="checkbox" name="checkbox" disabled ${data.Active ? "checked" : ""} value="true">
                            <span></span>
                        </label>`;
            },
        },
        {
            orderable: false,
            data: null,
            title: "Thao tác",
            render: function (data, type, item) {
                var re = `<a href="javascript:void(0)" btn-editdata  data-id="${data.Id}"
                            class="m-portlet__nav-link btn m-btn btn-outline-success m-btn--icon m-btn--icon-only m-btn--pill" data-toggle="m-tooltip" title="Sửa thông tin"><i class="la la-edit"></i> </a>`;
                re += `<a href="javascript:void(0)" btn-deletedata data-id="${data.Id}"
                            class="m-portlet__nav-link btn m-btn btn-outline-danger m-btn--icon m-btn--icon-only m-btn--pill" data-toggle="m-tooltip" title="Xóa dữ liệu"><i class="la la-trash"></i> </a>`;
                return re;
            },
            class: 'text-center',
        },
    ]

    function init() {
        table = $('.td-datatable').DataTable({
            ajax: {
                url: '/opdtapi/dataSets',
                dataSrc: function (res) {
                    return res.result;
                },
            },
            serverSide: false,
            bLengthChange: false,
            sDom: 'lrtip',
            columns: columnsDefine,
        })
            .on('click', '[btn-editdata]', function () {
                var id = $(this).attr('data-id');
                update(id);
            })
            .on('click', '[btn-deletedata]', function () {
                var id = $(this).attr('data-id');
                _delete(id);
            });
    }

    function reload() {
        table.ajax.url('/opdtapi/dataSets').load();
    }

    function add() {
        tdcore.modals
            .modal('Thêm tập dữ liệu')
            .content($('#add-template').html())
            .size(600, 400)
            .okCancel()
            .ready(function (mdl) {
                tdcore.forms.WidgetActivator.parse(mdl.panel.content).then(function () {
                    tdcore.forms.Widget.findWidgets(mdl.panel.content, false, tdcore.forms.Form)[0];
                });
            })
            .addCmd('OK', function (mdl) {
                var form = tdcore.forms.Widget.findWidgets(
                    mdl.panel.content,
                    false,
                    tdcore.forms.Form
                )[0];

                return form.tryValidate().then(function () {
                    var val = form.getData();
                    val.Active = val.Active[0] || false;

                    // var uploader = tdcore.forms.getWidget(document.getElementById('uploader'));
                    // // upload file
                    // if (uploader.queuedFiles && uploader.queuedFiles.length) {
                    //     uploader.uploadQueue()
                    //     .then(function(res) {
                    //         // lưu thông tin file
                    //         var fileAttachments = uploader.uploadedFiles.map(function (f) { return { Url: f.url, Name: f.name } });
                    //         val.Attachments = fileAttachments;

                    //         dataSetApi.add(val).then(function (data) {
                    //             toastr.success('Thành công');
                    //             dataSetDataTable.reload();
                    //         });
                    //     });
                    // }

                    dataSetApi.add(val).then(function (data) {
                        toastr.success('Thành công');
                        reload();
                    });
                })
            })
            .closeOnly()
            .show();
    }

    function update(id) {
        tdcore.modals
            .modal('Sửa tập dữ liệu')
            .content($('#edit-template').html())
            .size(600, 400)
            .okCancel()
            .ready(function (mdl) {
                tdcore.forms.WidgetActivator.parse(mdl.panel.content).then(function () {
                    var form = tdcore.forms.Widget.findWidgets(mdl.panel.content, false, tdcore.forms.Form)[0];

                    dataSetApi.getById(id).then(function (data) {
                        form.setData(data);
                    })
                });
            })
            .addCmd('OK', function (mdl) {
                var form = tdcore.forms.Widget.findWidgets(
                    mdl.panel.content,
                    false,
                    tdcore.forms.Form
                )[0];

                return form.tryValidate().then(function () {
                    var val = form.getData();
                    val.Active = val.Active[0] || false
                    dataSetApi.update(id, val).then(function () {
                        toastr.success('Thành công');
                        reload();
                    })
                })
            })
            .closeOnly()
            .show();
    }

    function _delete(id) {
        if (confirm('Bạn có chắc muốn xóa dữ liệu này')) {
            dataSetApi.delete(id).then(function () {
                toastr.success('Thành công');
                reload();
            });
        }
    }

    return {
        init,
        reload,
        add,
        update,
        delete: _delete
    }
})();

$(document).ready(function () {
    dataSetDataTable.init();

    $('[btn-adddata]').click(function () {
        dataSetDataTable.add();
    })
});