using MISA.AMIS.CORE.Entities;
using MISA.AMIS.CORE.Modal;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace MISA.AMIS.CORE.Interfaces.Repository
{
    public interface ICAPaymentReponsitory : IBaseRepository<MasterDetailEntity>
    {   
        /// <summary>
        /// thêm Phiếu chi va chi tiết phiếu chi
        /// </summary>
        /// <param name="entity">thông tin phiếu chi</param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        public int InsertMasterDetail(MasterDetailEntity entity);
        
        /// <summary>
        /// hàm lấy dữ liệu phiếu chi
        /// </summary>
        /// <param name="filter">dữ liệu lọc</param>
        /// <returns>các bản ghi và tông tin số trang và trang</returns>
        public PagingDataCaPayment GetDataCaPayment(FilterCaPayment filter);
        /// <summary>
        /// hàm lấy dữ liệu phiếu chi
        /// </summary>
        /// <param name="filter">dữ liệu lọc</param>
        /// <returns>các bản ghi và tông tin số trang và trang</returns>
        public PagingDataCaPayment GetDataCaPayment2(FilterCaPayment2 filter);
        /// <summary>
        /// Hàm xóa đối tượng
        /// </summary>
        /// <param name="entityId">id của đối tượng</param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// @Author nmquang 19-12-2021
        public int DeleteCaPayment(Guid entityId);

        /// <summary>
        /// Method sử dụng để xóa nhiều bản ghi của bảng Employee
        /// </summary>
        /// <param name="listId">Danh sách id của tài sản</param>
        /// <returns>Trả về số bản ghi ảnh hưởng khi xóa</returns>     
        /// @Author nmquang 19-12-2021
        int DeleteMultipleCaPaymentRecords(string listId);

        /// <summary>
        /// lấy dữ liệu đổ vào file excel
        /// </summary>
        /// <returns>các bản ghi được tìm thấy</returns>
        public List<CaPaymentDto> DataExcel();

        /// <summary>
        /// lấy số phiếu chi mới
        /// </summary>
        /// <returns> số phiếu chi mới</returns>
        public string GetCaPaymentNo();
    }
}
