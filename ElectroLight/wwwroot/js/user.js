var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tbldata').DataTable({
        "ajax": { url: '/user/getall' },
        "columns": [
            { data: 'fullName', "width": "20%" },
            { data: 'email', "width": "25%" },
            { data: 'phoneNumber', "width": "15%" },
            { data: 'role', "width": "15%" },
            {
                data: 'id',
                "render": function (data) {
                    return `<div class="text-center">
                    <div class="w-75 btn-group" role="group ">
                     <a href="/user/Update?id=${data}" class="btn btn-success mx-2" p-2> <i class="bi bi-pencil-square"></i> Edit</a>               
                     <a onClick=Delete('/user/delete/${data}') class="btn btn-danger mx-2 p-2"> <i class="bi bi-trash-fill"></i> Delete</a>
                    </div>
                     </div>`
                },
                "width": "25%"
            }
        ]
    });
}

function Delete(url) {
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        icon: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {

        if (result.isConfirmed) {

            $.ajax({
                url: url,
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

                    toastr.error("Something went wrong");

                }
            })
        }
    })
}