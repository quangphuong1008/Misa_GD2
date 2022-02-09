using MISA.AMIS.CORE.Entities;
using MISA.AMIS.CORE.Modal;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.AMIS.CORE.Interfaces.Repository
{
    public interface IAccountObjectRepository:IBaseRepository<AccountObject>
    {

        /// <summary>
        /// Lọc dữ liệu kết hợp giữa search , filter, paging. 
        /// </summary>
        /// <param name="searchText">Ký tự muốn tìm kiếm</param>
        /// <param name="pageSize">Số lượng record trên 1 trang</param>
        /// <param name="m_PageIndex">Trang hiện tại</param>
        /// <returns>Danh sách bản ghi được tìm thấy</returns>
        /// @Author nmquang 19-12-2021

        PagingDataAccountObject GetAccountObjectPaging(string searchText, int pageSize, int m_PageIndex);
        /// <summary>
        /// hàm lấy dữ liệu 
        /// </summary>
        /// <param name="filter">thông tin lọc</param>
        /// <returns>danh sách bản ghi được tìm thấy</returns>
        PagingDataAccountObject GetAccountObjectPaging2(FilterAccountObject filter);

     

    }
}
