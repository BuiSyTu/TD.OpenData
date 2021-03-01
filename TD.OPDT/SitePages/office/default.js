!(function (factory) {
    factory(jQuery, tdcore.modals, tdcore.forms);
})(function ($, modals, forms) {
    var table;
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
                            <input type="checkbox" name="checkbox" disabled ${!data.IsHidden ? "checked" : ""} value="true">
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

    var FieldDataTable = {};

    FieldDataTable.Init = function () {
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
                FieldDataTable.Update(id);
            })
            .on('click', '[btn-deletedata]', function () {
                var id = $(this).attr('data-id');
                FieldDataTable.Delete(id);
            });
    }

    FieldDataTable.Reload = function () {
        table.ajax.url('/opdtapi/fields').load();
    }

    FieldDataTable.Add = function () {
        modals
            .modal('Thêm lĩnh vực')
            .content($('#add-template').html())
            .size(500, 350)
            .okCancel()
            .ready(function (mdl) {
                forms.WidgetActivator.parse(mdl.panel.content).then(function () {
                    forms.Widget.findWidgets(mdl.panel.content, false, forms.Form)[0];
                });
            })
            .addCmd('OK', function (mdl) {
                var form = forms.Widget.findWidgets(
                    mdl.panel.content,
                    false,
                    forms.Form
                )[0];

                return form.tryValidate().then(function () {
                    var val = form.getData();
                    console.log(val);
                    FieldApi.Add(val).then(function (data) {
                        FieldDataTable.Reload();
                    })
                })
            })
            .closeOnly()
            .show();
    }

    FieldDataTable.Update = function (id) {
        modals
            .modal('Sửa lĩnh vực')
            .content($('#edit-template').html())
            .size(500, 350)
            .okCancel()
            .ready(function (mdl) {
                forms.WidgetActivator.parse(mdl.panel.content).then(function () {
                    var form = forms.Widget.findWidgets(mdl.panel.content, false, forms.Form)[0];

                    FieldApi.GetById(id).then(function (data) {
                        form.setData(data);
                    })
                });
            })
            .addCmd('OK', function (mdl) {
                var form = forms.Widget.findWidgets(
                    mdl.panel.content,
                    false,
                    forms.Form
                )[0];


                return form.tryValidate().then(function () {
                    var val = form.getData();
                    console.log(val);
                    FieldApi.Add(val).then(function (data) {
                        FieldDataTable.Reload();
                    })
                })
            })
            .closeOnly()
            .show();
    }

    FieldDataTable.Delete = function (id) {
        if (confirm("Bạn có chắc muốn xóa dữ liệu này")) {
            FieldApi.Delete(id).then(function (data) {
                FieldDataTable.Reload();
            });
        }
    }

    var FieldApi = {};

    FieldApi.GetById = function (id) {
        return new Promise(function (resolve, reject) {
            $.ajax({
                type: 'GET',
                url: `/opdtapi/fields/${id}`,
                success: function (res) { resolve(res.result) },
                error: function (e) { reject(e) }
            })
        })
    }

    FieldApi.Add = function (val) {
        return new Promise(function (resolve, reject) {
            $.ajax({
                type: 'POST',
                url: '/opdtapi/fields',
                data: JSON.stringify({
                    Name: val.Name,
                    Code: val.Code,
                    Order: val.Order,
                    IsHidden: val.IsHidden === '1'
                }),
                contentType: 'application/json',
                success: function (res) { resolve(res.result) },
                error: function (e) { reject(e) },
            });
        });
    }

    FieldApi.Delete = function (id) {
        return new Promise(function (resolve, reject) {
            $.ajax({
                type: 'DELETE',
                url: `/opdtapi/fields/${id}`,
                contentType: 'application/json',
                success: function (res) { resolve() },
                error: function (e) { reject(e) },
            });
        });
    }

    $(document).ready(function () {
        FieldDataTable.Init();

        $('[btn-adddata]').click(function () {
            FieldDataTable.Add();
        })
    })
});