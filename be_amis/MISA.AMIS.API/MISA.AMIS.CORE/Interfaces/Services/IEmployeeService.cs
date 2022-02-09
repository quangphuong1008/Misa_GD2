using MISA.AMIS.CORE.Entities;
using MISA.AMIS.CORE.Modal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.CORE.Interfaces.Services
{
    public interface IEmployeeService:IBaseService<Employee>
    {
        /// <summary>
        /// hàm lấy tất cả bản ghi của Employee bao gồm cả thông tin tên vị trí
        /// </summary>
        /// <returns>Tất cả bản ghi của nhân viên và thông tin trạng thái request</returns>
        /// @Author nmquang 19-12-2021
        public ServiceResult GetAllField();

        /// <summary>
        /// lấy dữ liệu đổ vào file excel
        /// </summary>
        /// <returns>dữ liệu nhân viên dạng IEnumerable(duyệt các phần tử) </returns>
        /// @Author nmquang 19-12-2021
        public IEnumerable<EmployeeDto> DataExcel(string textSearch);




        /// <summary>
        /// Lọc dữ liệu kết hợp giữa search , filter, paging. 
        /// </summary>
        /// <param name="searchText">Ký tự muốn tìm kiếm</param>
        /// <param name="pageSize">Số lượng record trên 1 trang</param>
        /// <param name="m_PageIndex">Trang hiện tại</param>
        /// <returns>Danh sách nhân viên được tìm thấy và thông tin trạng thái request</returns>
        /// @Author nmquang 19-12-2021

        public PagingData GetEmployeePaging(string searchText, int pageSize, int m_PageIndex);
        /// <summary>
        /// Hàm chuyển đổi tên giới tính phục vụ cho export Excel
        /// </summary>
        /// <param name="gender">id giới tính</param>
        /// <returns>name giới tính</returns>
        /// @Author nmquang 19-12-2021
        public String ChangeGender(string gender);
    }

    
}
