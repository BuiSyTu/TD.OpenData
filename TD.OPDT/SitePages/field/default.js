var fieldApi = (function () {
    var apiPrefix = 'opdtapi';
    var controllerName = 'fields';

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

    function add(val) {
        return new Promise(function (resolve, reject) {
            $.ajax({
                type: 'POST',
                url: `/${apiPrefix}/${controllerName}`,
                data: JSON.stringify({
                    Name: val.Name,
                    Code: val.Code,
                    Order: val.Order,
                    Active: val.Active,
                }),
                contentType: 'application/json',
                success: function (res) { resolve(res.result) },
                error: function (e) { reject(e) },
            });
        });
    }

    function update(id, val) {
        return new Promise(function (resolve, reject) {
            $.ajax({
                type: 'PUT',
                url: `/${apiPrefix}/${controllerName}/${id}`,
                data: JSON.stringify({
                    Id: id,
                    Name: val.Name,
                    Code: val.Code,
                    Order: val.Order,
                    Active: val.Active,
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

var fieldDataTable = (function () {
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
                url: '/opdtapi/fields',
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
                fieldDataTable.update(id);
            })
            .on('click', '[btn-deletedata]', function () {
                var id = $(this).attr('data-id');
                fieldDataTable.delete(id);
            });
    }

    function reload() {
        table.ajax.url('/opdtapi/fields').load();
    }

    function add() {
        tdcore.modals
            .modal('Thêm lĩnh vực')
            .content($('#add-template').html())
            .size(500, 350)
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
                    fieldApi.add(data).then(function (data) {
                        toastr.success('Thành công');
                        fieldDataTable.reload();
                    })
                })
            })
            .closeOnly()
            .show();
    }

    function update(id) {
        tdcore.modals
            .modal('Sửa lĩnh vực')
            .content($('#edit-template').html())
            .size(500, 350)
            .okCancel()
            .ready(function (mdl) {
                tdcore.forms.WidgetActivator.parse(mdl.panel.content).then(function () {
                    var form = tdcore.forms.Widget.findWidgets(mdl.panel.content, false, tdcore.forms.Form)[0];

                    fieldApi.getById(id).then(function (data) {
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
                    fieldApi.update(id, val).then(function () {
                        toastr.success('Thành công');
                        fieldDataTable.reload();
                    })
                })
            })
            .closeOnly()
            .show();
    }

    function _delete(id) {
        if (confirm('Bạn có chắc muốn xóa dữ liệu này')) {
            fieldApi.delete(id).then(function () {
                toastr.success('Thành công');
                fieldDataTable.reload();
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
    fieldDataTable.init();

    $('[btn-adddata]').click(function () {
        fieldDataTable.add();
    })
});