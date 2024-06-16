var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        ajax: {
            "url": "/admin/Vehicle/getall"
        },
        "columns": [
            { "data": "model.make.name", "width": "15%" },
            { "data": "model.name", "width": "15%" },
            { "data": "year", "width": "15%" },
            { "data": "price", "width": "15%" },
            {
                "data": "id",
                "render": function (data) {
                    return `<a href="/admin/Vehicle/upsert/${data}" class="btn btn-primary">
                                <i class="bi bi-pencil-square"></i> Edit
                            </a>

                            <a onClick=Delete(${data}) class="btn btn-danger">
                                <i class="bi bi-trash"></i> Delete
                             </a>`
                }
            }
        ]
    });
}

function Delete(id) {
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
                url: "/admin/Vehicle/delete/" + id,
                type: 'DELETE',
                success: function (data) {
                    if (data.success) {
                        dataTable.ajax.reload();
                        toastr.success(data.message);
                    } else {
                        toastr.error(data.message);
                    }
                },
                error: function () {
                    toastr.error("Error connecting to endpoint");
                }

            });
        }
    });
}