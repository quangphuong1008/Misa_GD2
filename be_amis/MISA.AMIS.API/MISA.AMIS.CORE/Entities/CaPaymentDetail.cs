using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.AMIS.CORE.Entities
{
    public class CaPaymentDetail:BaseEntity
    {
        /// <summary>
        /// Khóa chính chi tiết phiếu thu
        /// </summary>
     
        public Guid CaPaymentDetailId { get; set; }
        /// <summary>
        /// Khóa chính chi tiết phiếu thu
        /// </summary>
   
        public Guid CaPaymentId { get; set; }
        /// <summary>
        /// Diễn giải
        /// </summary>
    
        public string Decription { get; set; }
        /// <summary>
        /// id tài khoản nợ
        /// </summary>
   
        public string DebitAccountId { get; set; }
        /// <summary>
        /// id tài khoản có
        /// </summary>
 
        public string CreditAccountId { get; set; }
        /// <summary>
        /// Số tiền
        /// </summary>

        public decimal Amount { get; set; }
        /// <summary>
        /// Id Nhà cung cấp trả tiền
        /// </summary>
        public Guid? AccountObjectId { get; set; }

        public string AccountObjectCode { get; set; }
        /// <summary>
        /// Tên nhà cung cấp
        /// </summary>
        public string AccountObjectName { get; set; }
        /// <summary>
        /// tài khoản NCC trả tiền
        /// </summary>
        public string BankAcount { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>

        public string CreatedBy { get; set; }
        /// <summary>
        /// Ngày tạo
        /// </summary>
   
        public DateTime? CreatedDate { get; set; }
        /// <summary>
        /// Người sửa
        /// </summary>
        
        public string ModifiedBy { get; set; }
        /// <summary>
        /// Ngày sửa
        /// </summary>
        
        public DateTime ModifiedDate { get; set; }
    }
}
