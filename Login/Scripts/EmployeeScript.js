var table = null;

$(document).ready(function () {
    debugger;
    table = $('#myTable').DataTable({
        "ajax": {
            url: "/Employees/List",
            type: "GET",
            dataType: "json"
        },
        "columnDefs":
            [{
                "targets": [3],
                "orderable": false
            }],
        "columns": [
            { "data": "Email" },
            { "data": "Username" },
            { "data": "RoleName" },
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
    $('#Email').val('');
    $('#Username').val('');
    $('#Role_id').val('');
    $('#Update').hide();
    $('#Save').show();
}

//function loadEmployees() {
//    //debugger;
//    $.ajax({
//        url: "/Employees/List",
//        type: "GET",
//        contentType: "application/json;charset=utf-8",
//        dataType: "json",
//        async: false,
//        success: function (result) {
//            //debugger;
//            var html = '';
//            $.each(result, function (key, Employee) {
//                //debugger;
//                html += '<tr>';
//                html += '<td>' + Employee.Email + '</td>';
//                html += '<td>' + Employee.Username + '</td>';
//                html += '<td>' + Employee.Role.Name + '</td>';
//                html += '<td><a href="#" class="fa fa-pencil" data-toggle="tooltip" title="Edit" id="Update" onclick="return GetbyId(' + Employee.id + ')"></a> |';
//                html += ' <a href="#" class="fa fa-trash" data-toggle="tooltip" title="Delete" id="Delete" onclick="return Delete(' + Employee.id + ')" ></button ></td > ';
//                html += '</tr>';
//                html += '</tr>';
//                html += '</tr>';
//            });
//            $('.employeesbody').html(html);
//        },
//        error: function (errormessage) {
//            alert(errormessage.responseText);
//        }
//    });
//}

var Roles = []
function LoadRole(element) {
    //debugger;
    if (Roles.length == 0) {
        $.ajax({
            type: "Get",
            url: "/Roles/LoadRole",
            success: function (data) {
                Roles = data;
                renderRole(element);
            }
        })
    }
    else {
        renderRole(element);
    }
}

function renderRole(element) {
    //debugger;
    var $ele = $(element);
    $ele.empty();
    $.each(Roles, function (i, val) {
        $ele.append($('<option/>').val(val.id).text(val.Name));
    })
}
LoadRole($('#Role_id'));

function Save() {
    //debugger;
    if ($('#Email').val() == 0) {
        Swal.fire({
            position: 'center',
            type: 'error',
            title: 'Please Full Fill The Email',
            showConfirmButton: false,
            timer: 1500
        });
    } else if ($('#Username').val() == 0) {
        Swal.fire({
            position: 'center',
            type: 'error',
            title: 'Please Full Fill The Role Name',
            showConfirmButton: false,
            timer: 1500
        });
    }
    //else if ($('#Role').val() == 0) {
    //    Swal.fire({
    //        position: 'center',
    //        type: 'error',
    //        title: 'Please Select The Role',
    //        showConfirmButton: false,
    //        timer: 1500
    //    });
    //}
    else {
        debugger;
        var User = new Object();
        User.Email = $('#Email').val();
        User.Username = $('#Username').val();
        User.Role_id = $('#Role_id').val();
        $.ajax({
            type: 'POST',
            url: '/Employees/InsertOrUpdate/',
            data: User
        }).then((result) => {
            debugger;
            if (result > 0) {
                Swal.fire({
                    position: 'center',
                    type: 'success',
                    title: 'User Added Successfully'
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
    //debugger;
    $.ajax({
        url: "/Employees/GetbyId/",
        type: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        data: { id: id },
        success: function (result) {
            //debugger;
            $('#Id').val(result.id);
            $('#Email').val(result.Email);
            $('#Username').val(result.Username);
            $('#Role_id').val(result.Role_id);
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
    if ($('#Email').val() == 0) {
        Swal.fire({
            position: 'center',
            type: 'error',
            title: 'Please Full Fill The Email',
            showConfirmButton: false,
            timer: 1500
        });
    } else if ($('#Username').val() == 0) {
        Swal.fire({
            position: 'center',
            type: 'error',
            title: 'Please Full Fill The Role Name',
            showConfirmButton: false,
            timer: 1500
        });
    }
    //else if ($('#Role').val() == 0) {
    //    Swal.fire({
    //        position: 'center',
    //        type: 'error',
    //        title: 'Please Select The Role',
    //        showConfirmButton: false,
    //        timer: 1500
    //    });
    //}
    else {
        debugger;
        var User = new Object();
        User.id = $('#Id').val();
        User.Email = $('#Email').val();
        User.Username = $('#Username').val();
        User.Role_id = $('#Role_id').val();
        $.ajax({
            type: "POST",
            url: '/Employees/InsertOrUpdate/',
            data: User
        }).then((result) => {
            //debugger;
            if (result > 0) {
                Swal.fire({
                    position: 'center',
                    type: 'success',
                    title: 'User Updated Successfully'
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
                url: "/Employees/Delete/",
                data: { id: id }
            }).then((result) => {
                //debugger;
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