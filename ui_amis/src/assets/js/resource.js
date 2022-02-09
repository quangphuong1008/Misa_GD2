/**
 * 
 */
const Resource = {
    VN: {
        titleEmployee:{
            employeeCode : "Mã nhân viên",
            fullName: "Tên nhân viên",
            gender: "Giới tính",
            dateOfBirth: "Ngày sinh",
            identityNumber: "Số CMND",
            positionName: "Chức danh",
            departmentName: "Tên đơn vị",
            bankAccount: "Số tài khoản",
            bankName: "Tên ngân hàng",
            bankBranch: "Chi nhánh TK ngân hàng",
        },
        Gender: {
            Male: "Nam",
            Female: "Nữ",
            Other: "Khác"
        },
        DeleteConfirm: function (input) {
            return `Bạn có thực sự muốn xóa nhân viên <${input}> không?`;
        },
        DeleteAccountObjecConfirm: function (input) {
            return `Bạn có thực sự muốn xóa nhà cung cấp <${input}> không?`;
        },
        DeleteCaPaymentConfirm: function (input) {
            return `Bạn có thực sự muốn xóa phiếu chi <${input}> không?`;
        },
        DeleteAllConfirm: function (input) {
            return `Bạn có thực sự muốn xóa <${input}> nhân viên không?`;
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
            EmployeeCodeIsEmpty: "Mã nhân viên không được để trống",
            FullNameIsEmpty: "Tên không được để trống",
            DepartmentIsEmpty: "Đơn vị không được để trống hoặc Đơn vị không tồn tại",
            EmployeeCodeIsAvailable: "Mã nhân viên này đã tồn tại trong hệ thống, vui lòng kiểm tra lại !",
            DateOfBirthError: "Ngày sinh không được vượt quá ngày hiện tại",
            EmailError: "Email không đúng định dạng",
            IndentityDate: "Ngày cấp không được vượt quá ngày hiện tại",
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