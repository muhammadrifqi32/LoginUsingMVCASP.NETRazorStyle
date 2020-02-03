var table = null;

$(document).ready(function () {
    debugger;
    table = $('#myTable').DataTable({
        "ajax": {
            url: "/Roles/List",
            type: "GET",
            dataType: "json"
        },
        "columnDefs":
            [{
                "targets": [1],
                "orderable": false
            }],
        "columns": [
            { "data": "Name" },
            {
                "render": function (data, type, row) {
                    return '<button class="btn btn-warning " data-placement="left" data-toggle="tooltip" data-animation="false" title="Edit" onclick="return GetById(' + row.Id + ')"> <i class="mdi mdi-pencil"></i></button >' + '&nbsp;' +
                        '<button class="btn btn-danger" data-placement="right" data-toggle="tooltip" data-animation="false" title="Delete" onclick="return Delete(' + row.Id + ')"> <i class="mdi mdi-eraser"></i></button >'
                }
            }]
    });
});


$(document).ready(function () {
    $('[data-toggle="tooltip"]').tooltip();
});


function ClearScreen() {
    $('#Id').val('');
    $('#name').val('');
    $('#Update').hide();
    $('#Save').show();
}

//function loadRole() {
//    //debugger;
//    $.ajax({
//        url: "/Roles/List",
//        type: "GET",
//        contentType: "application/json;charset=utf-8",
//        dataType: "json",
//        async: false,
//        success: function (result) {
//            //debugger;
//            var html = '';
//            $.each(result, function (key, Role) {
//                //debugger;
//                html += '<tr>';
//                html += '<td>' + Role.Name + '</td>';
//                html += '<td><a href="#" class="fa fa-pencil" data-toggle="tooltip" title="Edit" id="Update" onclick="return GetbyId(' + Role.id + ')"></a> |';
//                html += ' <a href="#" class="fa fa-trash" data-toggle="tooltip" title="Delete" id="Delete" onclick="return Delete(' + Role.id + ')" ></button ></td > ';
//                html += '</tr>';
//            });
//            $('.rolebody').html(html);
//        },
//        error: function (errormessage) {
//            alert(errormessage.responseText);
//        }
//    });
//}

function Save() {
    //debugger;
    if ($('#Name').val() == 0) {
        Swal.fire({
            position: 'center',
            type: 'error',
            title: 'Please Full Fill The Role Name',
            showConfirmButton: false,
            timer: 1500
        });
    } else {
        var Role = new Object();
        Role.Name = $('#Name').val();
        $.ajax({
            type: 'POST',
            url: '/Roles/InsertOrUpdate/',
            data: Role
        }).then((result) => {
            debugger;
            if (result > 0) {
                Swal.fire({
                    position: 'center',
                    type: 'success',
                    title: 'Role Added Successfully'
                });
                table.ajax.reload();
            } else {
                Swal.fire('Error', 'Failed to Delete', 'error');
                ClearScreen();
            }
        })
    }
}
function GetById(id) {
    debugger;
    $.ajax({
        url: "/Roles/GetbyId/",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        data: { id: id },
        success: function (result) {
            debugger;
            $('#Id').val(result.id);
            $('#Name').val(result.Name);
            $('#myModal').modal('show');
            $('#Update').show();
            $('#Save').hide();
        },
        error: function (errormessage) {
            alert(errormessage.responseText);
        }
    });
}
function Update() {
    //debugger;
    if ($('#Name').val() == 0) {
        Swal.fire({
            position: 'center',
            type: 'error',
            title: 'Please Full Fill The Role Name',
            showConfirmButton: false,
            timer: 1500
        });
    } else {
        var Role = new Object();
        Role.id = $('#Id').val();
        Role.Name = $('#Name').val();
        $.ajax({
            type: "POST",
            url: '/Roles/InsertOrUpdate/',
            data: Role
        }).then((result) => {
            debugger;
            if (result > 0) {
                Swal.fire({
                    position: 'center',
                    type: 'success',
                    title: 'Role Updated Successfully'
                });
                table.ajax.reload();
            } else {
                Swal.fire('Error', 'Failed to Delete', 'error');
                ClearScreen();
            }
        })
    }
}
function Delete(id) {
    //debugger;
    Swal.fire({
        title: 'Are you sure?',
        text: "You won't be able to revert this!",
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Yes, delete it!'
    }).then((result) => {
        if (result.value) {
            //debugger;
            $.ajax({
                url: "/Roles/DeleteRole/",
                data: { id: id }
            }).then((result) => {
                debugger;
                if (result > 0) {
                    Swal.fire({
                        position: 'center',
                        type: 'success',
                        title: 'Delete Successfully'
                    });
                    table.ajax.reload();
                } else {
                    Swal.fire('Error', 'Failed to Delete', 'error');
                    ClearScreen();
                }
            })
        };
    });
}