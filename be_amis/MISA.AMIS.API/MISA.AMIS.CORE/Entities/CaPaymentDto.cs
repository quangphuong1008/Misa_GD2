using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.AMIS.CORE.Entities
{
    public class CaPaymentDto
    {
        /// <summary>
        /// Khóa chính phiếu chi
        /// </summary>

        public Guid CaPaymentId { get; set; }
        /// <summary>
        /// Số phiếu chi
        /// </summary>

        public string CaPaymentNo { get; set; }
        /// <summary>
        /// Mã nhà cung cấp
        /// </summary>

        public Guid? AccountObjectId { get; set; }
        /// <summary>
        /// Người nhận
        /// </summary>
        /// 

        public string AccountObjectCode { get; set; }

        public string AccountObjectName { get; set; }

        public string Receiver { get; set; }
        /// <summary>
        /// Địa chỉ
        /// </summary>

        public string Address { get; set; }
        /// <summary>
        /// Lý do chi tiền
        /// </summary>

        public string Resion { get; set; }
        /// <summary>
        /// Mã nhân viên
        /// </summary>

        public Guid? EmployeeId { get; set; }
        /// <summary>
        /// Ngày chi phiếu
        /// </summary>

        public DateTime? CaPaymentDate { get; set; }
        /// <summary>
        /// Ngày hạch toán
        /// </summary>

        public DateTime? PostedDate { get; set; }
        /// <summary>
        /// Tổng tiền
        /// </summary>

        public float TotalAmount { get; set; }
        /// <summary>
        /// File ảnh
        /// </summary>

        public string File { get; set; }
        /// <summary>
        /// Người tạo
        /// </summary>

        public string CreatedBy { get; set; }
        /// <summary>
        /// Ngày tạo
        /// </summary>

        public DateTime? CreatedDate { get; set; }
        /// <summary>
        /// Ngày sửa
        /// </summary>

        public DateTime? ModifiedDate { get; set; }
        /// <summary>
        /// Người sửa
        /// </summary>

        public string ModifiedBy { get; set; }
    }
}
