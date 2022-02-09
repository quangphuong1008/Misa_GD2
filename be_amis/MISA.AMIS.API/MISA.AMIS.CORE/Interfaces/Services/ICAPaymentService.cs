using MISA.AMIS.CORE.Entities;
using MISA.AMIS.CORE.Modal;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.AMIS.CORE.Interfaces.Services
{
    public interface ICAPaymentService: IBaseService<MasterDetailEntity>
    {
        /// <summary>
        /// Hàm thêm đối tươnjg
        /// </summary>
        /// <param name="entity">thông tin đối tượng</param>
        /// <returns>trả về số bản ghi bị ảnh hưởng</returns>
        public ServiceResult InsertMasterDetail(MasterDetailEntity entity);
        /// <summary>
        /// hàm lấy dữ liệu kết hợp paging tìm kiếm
        /// </summary>
        /// <param name="filter">thông tin tìm kiếm</param>
        /// <returns>các bản ghi tương ứng</returns>
        public PagingDataCaPayment GetDataCaPayment(FilterCaPayment filter);

        /// <summary>
        /// hàm lấy dữ liệu kết hợp paging tìm kiếm
        /// </summary>
        /// <param name="filter">thông tin tìm kiếm</param>
        /// <returns>các bản ghi tương ứng</returns>
        public PagingDataCaPayment GetDataCaPayment2(FilterCaPayment2 filter);
        /// <summary>
        /// xóa đối tượng
        /// </summary>
        /// <param name="entityId">id của đối tượng</param>
        /// <returns>trả về số bản ghi bị ảnh hưởng</returns>
        /// @Author nmquang 19-12-2021
        public ServiceResult DeleteCaPayment(Guid entityId);


        /// <summary>
        /// Method sử dụng để xóa nhiều bản ghi của bảng 
        /// </summary>
        /// <param name="listId">Danh sách id của tài sản</param>
        /// <returns>Trả về số bản ghi ảnh hưởng khi xóa và thông tin trạng thái request</returns>
        /// @Author nmquang 19-12-2021
        public ServiceResult DeleteMultipleCaPaymentRecords(string listId);

        /// <summary>
        /// lấy dữ liệu đổ vào file excel
        /// </summary>
        /// <returns>dữ liệu nhân viên dạng IEnumerable(duyệt các phần tử) </returns>
        /// @Author nmquang 19-12-2021

        public IEnumerable<CaPaymentDto> DataExcel();

        /// <summary>
        /// lấy số phiếu chi mới
        /// </summary>
        /// <returns>Số phiếu chi mới</returns>
        public ServiceResult GetCaPaymentNo();
    }
   
}
