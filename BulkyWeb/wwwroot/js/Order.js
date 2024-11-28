var dataTable;

$(document).ready(function () {
    var url = window.location.search;
    if (url.includes("inprocess")) {
        loadDataTable("inprocess");
    } else if (url.includes("completed")) {
        loadDataTable("completed");
    } else if (url.includes("pending")) {
        loadDataTable("pending");
    } else if (url.includes("approved")) {
        loadDataTable("approved");
    } else {
        loadDataTable("all");
    }
});


function loadDataTable(status) {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            url: '/admin/order/getall?status='+status
        },
        "columns": [
            { data: 'id', "width": "5%" },
            { data: 'name', "width": "25%" },
            { data: 'phoneNumber', "width": "20%" },
            { data: 'applicationUser.email', "width": "20%" },
            { data: 'orderStatus', "width": "10%" },
            { data: 'orderTotal', "width": "10%" },
            {
                data: 'id', "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                                <a href="/admin/order/details?orderId=${data}" class="btn btn-primary mx-2"><i class ="bi bi-pencil-square"></i></a>
                            </div>`
            }, "width": "10%" }
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