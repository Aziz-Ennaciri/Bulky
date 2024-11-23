$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    $('#tblData').DataTable({
        "ajax": {
            url: '/admin/product/getall',
            type: 'GET',
            dataSrc: 'data'
        },
        "columns": [
            { data: 'title', width: "25%" },
            { data: 'isbn', width: "15%" },
            { data: 'price', width: "10%" },
            { data: 'author', width: "20%" },
            { data: 'category.name', width: "20%" },
            {
                data: null,
                width: "10%",
                render: function (data, type, row) {
                    return `
                        <a href="/Admin/Product/Upsert/${row.id}" class="btn btn-success btn-sm">
                            <i class="bi bi-pencil-square"></i> Edit
                        </a>
                        <a href="#" class="btn btn-danger btn-sm" onclick="deleteProduct(${row.id})">
                            <i class="bi bi-trash"></i> Delete
                        </a>
                    `;
                }
            }
        ]
    });
}

function deleteProduct(id) {
    if (confirm("Are you sure you want to delete this product?")) {
        $.ajax({
            url: `/admin/product/delete/${id}`,
            type: 'POST',
            success: function (response) {
                toastr.success("Product deleted successfully.");
                $('#tblData').DataTable().ajax.reload();
            },
            error: function (error) {
                toastr.error("Something went wrong while deleting the product.");
            }
        });
    }
}
