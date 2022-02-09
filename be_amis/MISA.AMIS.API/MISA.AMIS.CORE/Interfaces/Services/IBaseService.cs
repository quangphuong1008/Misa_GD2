using MISA.AMIS.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.CORE.Interfaces.Services
{
    public interface IBaseService<TEntity>
    {
        /// <summary>
        /// hàm lấy toàn bộ bản ghi của đối tượng
        /// </summary>
        /// <returns>trả về toàn bộ bản ghi của đối tượng</returns>
        /// @Author nmquang 19-12-2021
        public ServiceResult GetAll();
        /// <summary>
        /// lấy bản ghi của đối tượng theo ID
        /// </summary>
        /// <param name="entityId">ID đối tượng</param>
        /// <returns>trả về  đối tượng, nếu không thấy thì trả về null</returns>
        /// @Author nmquang 19-12-2021
        public ServiceResult GetById(Guid entityId);
        /// <summary>
        /// lấy bản ghi của đối tượng theo ID
        /// </summary>
        /// <param name="entityId">ID đối tượng</param>
        /// <returns>trả về  đối tượng, nếu không thấy thì trả về null</returns>
        /// @Author nmquang 19-12-2021
        public ServiceResult GetMasterDetailById(Guid entityId);

        /// <summary>
        /// Hàm lấy mã nhân viên mới
        /// </summary>
        /// <returns>mã nhân viên mới và thông tin trạng thái request</returns>
        /// @Author nmquang 19-12-2021
        public ServiceResult GetMasterDetailByCode(string entityCode);

        /// <summary>
        /// Hàm lấy mã nhân viên mới
        /// </summary>
        /// <returns>mã nhân viên mới và thông tin trạng thái request</returns>
        /// @Author nmquang 19-12-2021
        public ServiceResult GetEntityCode();


        /// <summary>
        /// tìm kiếm nhân viên theo mã code
        /// </summary>
        /// <param name="EmployeeCode"></param>
        /// <returns>Trả về bản ghi đầu tiên được tìm thấy và thông tin trạng thái request</returns>
        /// @Author nmquang 19-12-2021
        public ServiceResult GetByEntityCode(string EntityCode);

        /// <summary>
        /// thêm mới 1 đối tượng
        /// </summary>
        /// <param name="entity">các thuộc tính của đối tượng </param>
        /// <returns>trả về số bản ghi bị ảnh hưởng</returns>
        /// @Author nmquang 19-12-2021
        /// 


        public ServiceResult Insert(TEntity entity);
        /// <summary>
        /// sửa đối tượng
        /// </summary>
        /// <param name="entity">các thuộc tính của đối tượng</param>
        /// <param name="entityId">id của đối tượng</param>
        /// <returns>số bản ghi bị ảnh hưởng</returns>
        /// @Author nmquang 19-12-2021
        public ServiceResult Update(TEntity entity, Guid entityId);
        /// <summary>
        /// xóa đối tượng
        /// </summary>
        /// <param name="entityId">id của đối tượng</param>
        /// <returns>trả về số bản ghi bị ảnh hưởng</returns>
        /// @Author nmquang 19-12-2021
        public ServiceResult Delete(Guid entityId);
 

        /// <summary>
        /// Method sử dụng để xóa nhiều bản ghi của bảng 
        /// </summary>
        /// <param name="listId">Danh sách id của tài sản</param>
        /// <returns>Trả về số bản ghi ảnh hưởng khi xóa và thông tin trạng thái request</returns>
        /// @Author nmquang 19-12-2021
        public ServiceResult DeleteMultipleRecords(string listId);
    }
}
