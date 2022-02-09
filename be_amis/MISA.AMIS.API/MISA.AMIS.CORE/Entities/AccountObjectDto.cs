using MISA.AMIS.CORE.Modal;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.AMIS.CORE.Entities
{
    public class AccountObjectDto : BaseEntity
    {
        /// <summary>
        /// Khóa chính
        /// </summary>
        /// CreatedBy: NmQuang(14/1/2022)
        public Guid AccountObjectId { get; set; }
        /// <summary>
        /// Mã số thuế
        /// </summary>
        /// CreatedBy: NmQuang(14/1/2022)
        public string TaxCode { get; set; }
        /// <summary>
        /// Mã nhà cung cấp
        /// </summary>
        /// CreatedBy: NmQuang(14/1/2022)
        public string AccountObjectCode { get; set; }
        /// <summary>
        /// Tên nhà cung cấp
        /// </summary>
        /// CreatedBy: NmQuang(14/1/2022)
        public string AccountObjectName { get; set; }
        /// <summary>
        /// Số điện thoại
        /// </summary>
        /// CreatedBy: NmQuang(14/1/2022)
        public string Phone { get; set; }
        /// <summary>
        /// Danh sách id nhóm nhà cung cấp
        /// </summary>
        /// CreatedBy: NmQuang(14/1/2022)
        public string AccountObjectGroupListId { get; set; }
        /// <summary>
        /// địa chỉ
        /// </summary>
        /// CreatedBy: NmQuang(14/1/2022)
        public string Address { get; set; }
        /// <summary>
        /// Id của nhân viên
        /// </summary>
        /// CreatedBy: NmQuang(14/1/2022)
        public Guid? EmployeeId { get; set; }

      
        public string AccountObjectBankAccount { get; set; }
        /// <summary>
        /// Danh sách tài khoản ngân hàng gửi từ dtb lên client
        /// </summary>
        /// CreatedBy: NmQuang(14/1/2022)
        public string AccountObjectBankAccountJson { get; set; }
        public string Description { get; set; }
        /// <summary>
        /// Người tạo bản ghi
        /// </summary>
        /// CreatedBy: NmQuang(14/1/2022)
        public string CreatedBy { get; set; }
        /// <summary>
        /// Ngày tạo
        /// </summary>
        /// CreatedBy: NmQuang(14/1/2022)
        public DateTime? CreatedDate { get; set; }
        /// <summary>
        /// Người sửa
        /// </summary>
        /// CreatedBy: NmQuang(14/1/2022)
        public string ModifiedBy { get; set; }
        /// <summary>
        /// Ngày sửa
        /// </summary>
        /// CreatedBy: NmQuang(14/1/2022)
        public DateTime? ModifiedDate { get; set; }
    }
}
