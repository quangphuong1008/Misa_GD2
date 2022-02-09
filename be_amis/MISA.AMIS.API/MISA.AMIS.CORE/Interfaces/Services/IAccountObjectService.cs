using MISA.AMIS.CORE.Entities;
using MISA.AMIS.CORE.Modal;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.AMIS.CORE.Interfaces.Services
{
    public interface IAccountObjectService:IBaseService<AccountObject>
    {

        /// <summary>
        /// lấy dữ liệu đổ vào file excel
        /// </summary>
        /// <returns>dữ liệu nhân viên dạng IEnumerable(duyệt các phần tử) </returns>
        /// @Author nmquang 19-12-2021

        public IEnumerable<AccountObject> DataExcel();


        /// <summary>
        /// Lọc dữ liệu kết hợp giữa search , filter, paging. 
        /// </summary>
        /// <param name="searchText">Ký tự muốn tìm kiếm</param>
        /// <param name="pageSize">Số lượng record trên 1 trang</param>
        /// <param name="m_PageIndex">Trang hiện tại</param>
        /// <returns>Danh sách nhân viên được tìm thấy và thông tin trạng thái request</returns>
        /// @Author nmquang 19-12-2021

        public PagingDataAccountObject GetAccountObjectPaging(string searchText, int pageSize, int m_PageIndex);
        public PagingDataAccountObject GetAccountObjectPaging2(FilterAccountObject filter);
    }


}
