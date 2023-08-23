﻿$(function () {
    $("#example1").DataTable({
        "responsive": true, "lengthChange": true, "autoWidth": true, "ordering": true, "searching": true,
        "buttons": ["copy", "csv", "excel", "pdf", "print", "colvis"]
    }).buttons().container().appendTo('#example1_wrapper .col-md-6:eq(0)');
    $('#example2').DataTable({
        "paging": true,
        "lengthChange": false,
        "searching": false,
        "ordering": true,
        "info": true,
        "autoWidth": false,
        "responsive": true,
    });
});
$("#Body").summernote();
$("#body").summernote();
$("#Input_Body").summernote();
$("#Input_Description").summernote();