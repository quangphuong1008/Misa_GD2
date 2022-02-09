/**
 * 
 */
const Resource = {
    VN: {
        
        DeleteCaPaymentConfirm: function (input) {
            return `Bạn có thực sự muốn xóa phiếu chi <${input}> không?`;
        },
        DeleteAllConfirm: function (input) {
            return `Bạn có thực sự muốn xóa <${input}> nhân viên không?`;
        },
        GetCaPaymentNoConfirm: function (input) {
            return `Số chứng từ <${input}> đã tồn tại. Nếu không tìm thấy số chứng từ <${input}>. Bạn có muốn chương trình tự động tăng số chứng từ không?`;
        },
        ToastMessage: {
            RefreshSucess: "Làm mới dữ liệu thành công !",
            DeleteSuccess: "Xóa thành công !",
            Error: "Có lỗi trong quá trình xử lý",
            AddSuccess: "Thêm mới thành công !",
            UpdateSuccess: "Sửa dữ liệu thành công !"
        },

        Warning:{
            caPaymentNoIsEmpty: "Số phiếu chi không được để trống",
            DebitAccountIsEmpty: "Tài khoản nợ không được để trống",
            CaPaymentDateError: "Ngày hạch toán phải lớn hơn ngày phiếu chi",

        },
        Error:{
            ListDeleteNull:"Danh sách để xóa trống"
        },
        Question:{
            DeleteAll: "Bạn có thật sự muốn xóa các nhân viên đã chọn không?",
            CloseForm:"Dữ liệu đã bị thay đổi. Bạn có muốn cất không?"
        }
    },
    EN: {
        Gender: {
            Male: "Male",
            Female: "Female",
            Other: "Other"
        }
    },
    AMIS_SERVICE_URL: "http://localhost:51831/api/v1",
    FormMode: {
        Add: 0,
        Edit: 1,
        Show: 2,
        Clone: 3
    }
}

export default Resource;