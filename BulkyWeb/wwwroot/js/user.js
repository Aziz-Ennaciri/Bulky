var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            url: '/admin/user/getall'
        },
        "columns": [
            { data: 'name', "width": "25%" },
            { data: 'email', "width": "15%" },
            { data: 'phoneNumber', "width": "10%" },
            { data: 'company.name', "width": "15%" },
            { data: 'role', "width": "10%" },
            {
                data: 'id', "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                                <a href="/admin/company/upsert?id=${data}" class="btn btn-primary mx-2"><i class ="bi bi-pencil-square"></i>Edit</a>
                            </div>`
                }, "width": "25%"
            }
            //{
            //    data: null,
            //    width: "10%",
            //    render: function (data, type, row) {
            //        return `
            //            <a href="/Admin/Product/Upsert/${row.id}" class="btn btn-success btn-sm">
            //                <i class="bi bi-pencil-square"></i> Edit
            //            </a>
            //            <a href="#" class="btn btn-danger btn-sm" onclick="deleteProduct(${row.id})">
            //                <i class="bi bi-trash"></i> Delete
            //            </a>
            //        `;
            //    }
            //}
        ]
    });
}