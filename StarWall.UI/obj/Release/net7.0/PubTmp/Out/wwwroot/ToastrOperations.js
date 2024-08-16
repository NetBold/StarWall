window.ShowToastr = (type, message) => {
    if (type == 'success') {
        toastr.success(message, 'Operation Completed Successfully');
    }
    if (type == 'error') {
        toastr.error(message, 'Operation Failed');
    }
}