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
                data: { id: "id", lockoutEnd:"lockoutEnd" },
                "render": function (data) {
                    var today = new Date().getTime();
                    var lockout = new Date(data.lockoutEnd).getTime();
                    if (lockout > today) {
                        return
                        `<div class="text-center">
                            <a onclick="LockUnlock('${data.id}')" class="btn btn-danger text-white" style="cursor:pointer;width:100px">
                                <i class ="bi bi-lock-fill"></i>Lock
                            </a>
                            <a class="btn btn-success text-white" style="cursor:pointer;width:150px">
                                <i class ="bi bi-unlock-fill"></i>Permission
                            </a>
                        </div>`
                    } else {
                        return
                        `<div class="text-center">
                            <a onclick="LockUnlock('${data.id}')" class="btn btn-success text-white" style="cursor:pointer;width:100px">
                                <i class ="bi bi-unlock-fill"></i>Unlock
                            </a>
                            <a class="btn btn-success text-white" style="cursor:pointer;width:150px">
                                <i class ="bi bi-unlock-fill"></i>Permission
                            </a>
                        </div>`
                    }

                    
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

function LockUnlock(id) {
    $.ajax({
        type: "POST",
        url: '/Admin/User/LockUnlock',
        data: JSON.stringify(id),
        contentType: "application/json",
        success: function (data) {
            if (data.success) {
                toastr.success(data.message);
                dataTable.ajax.reload();
            }
        }
    });
}