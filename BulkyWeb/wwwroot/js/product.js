﻿var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": {
            url: '/admin/product/getall'
        },
        "columns": [
            { data: 'title', "width": "25%" },
            { data: 'isbn', "width": "15%" },
            { data: 'listPrice', "width": "10%" },
            { data: 'author', "width": "15%" },
            { data: 'category.name', "width": "10%" },
            {
                data: 'id', "render": function (data) {
                    return `<div class="w-75 btn-group" role="group">
                                <a href="/admin/product/upsert?id=${data}" class="btn btn-primary mx-2"><i class ="bi bi-pencil-square"></i>Edit</a>
                                <a onClick=Delete('/admin/product/delete/${data}') class="btn btn-danger mx-2"><i class ="bi bi-trash-fill"></i>Delte</a>
                            </div>`
            }, "width": "25%" }
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

function Delete(url) {
    Swal.fire({
        title: "Are you sure?",
        text: "You won't be able to revert this!",
        icon: "warning",
        showCancelButton: true,
        confirmButtonColor: "#3085d6",
        cancelButtonColor: "#d33",
        confirmButtonText: "Yes, delete it!"
    }).then((result) => {
        if (result.isConfirmed) {
            $.ajax({
                url: url,
                type: 'DELETE',
                success: function (data) {
                    dataTable.ajax.reload();
                    toastr.success(data.message);
                }
            })
        }
    });
}
