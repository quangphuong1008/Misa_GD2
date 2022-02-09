using MISA.AMIS.CORE.Entities;
using MISA.AMIS.CORE.Interfaces.Repository;
using MISA.AMIS.CORE.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.AMIS.CORE.Sevices
{
    public class PaymentTermService : BaseService<PaymentTerm>, IPaymentTermService
    {
        IPaymentTermRepository _paymentTermRepository;
        public PaymentTermService(IPaymentTermRepository paymentTermRepository) : base(paymentTermRepository)
        {
            _paymentTermRepository = paymentTermRepository;
        }

    }
    
}
