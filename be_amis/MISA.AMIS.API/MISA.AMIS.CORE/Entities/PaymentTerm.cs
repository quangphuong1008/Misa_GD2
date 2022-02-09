using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.AMIS.CORE.Entities
{
    public class PaymentTerm : BaseEntity
    {
        public Guid PaymentTermId { get; set; }
        public string PaymentTermCode { get; set; }
        public string PaymentTermName { get; set; }

    }
}
