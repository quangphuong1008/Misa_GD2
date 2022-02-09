/**
 * 
 */
const Resource = {
    VN: {
        Gender: {
            Male: "Nam",
            Female: "Nữ",
            Other: "Khác"
        },

        DeleteAccountObjecConfirm: function (input) {
            return `Bạn có thực sự muốn xóa nhà cung cấp <${input}> không?`;
        },
        ToastMessage: {
            RefreshSucess: "Làm mới dữ liệu thành công !",
            DeleteSuccess: "Xóa thành công !",
            Error: "Có lỗi trong quá trình xử lý",
            AddSuccess: "Thêm mới thành công !",
            UpdateSuccess: "Sửa dữ liệu thành công !"
        },
        WorkStatus: {
            Working: "Đang làm việc",
            NotWorking: "Đã nghỉ việc",
            Trying: "Đang thử việc"
        },
        Warning:{
            AccountObjectCodeIsEmpty: "Mã nhà cung cấp không được để trống",
            AccountObjectNameIsEmpty: "Tên nhà cung cấp không được phép để trống",
            EmailError: "Email không đúng định dạng",
            IndentityDate: "Ngày cấp không được vượt quá ngày hiện tại",
            AccountObjectCodeIsAvailable: "Mã nhà cung cấp này đã tồn tại trong hệ thống, vui lòng kiểm tra lại !",
        },
        Error:{
            ListDeleteNull:"Danh sách để xóa trống",
            AccountObjectCodeIsUsed: "Mã nhà cung cấp không thể xóa do có phát sinh!"
        },
        Question:{
            DeleteAll: "Bạn có thật sự muốn xóa các nhà cung cấp đã chọn không?",
            CloseForm:"Dữ liệu đã bị thay đổi. Bạn có muốn cất không?"
        },
        Example:{
            Address: "VD: Số 82 Duy Tân, Dịch Vọng Hậu, Cầu Giấy, Hà Nội"
        }
    },
    AMIS_SERVICE_URL: "http://localhost:51831/api/v1",

}

export default Resource;