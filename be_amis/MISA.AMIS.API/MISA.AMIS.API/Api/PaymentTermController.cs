using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.CORE.Entities;
using MISA.AMIS.CORE.Interfaces.Repository;
using MISA.AMIS.CORE.Interfaces.Services;
using MISA.AMIS.CORE.Sevices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.AMIS.API.Api
{
    public class PaymentTermController : AMISControlller<PaymentTerm>
    {
        IPaymentTermService _PaymentTermService;
        public PaymentTermController(IPaymentTermService paymentTermService, IBaseRepository<PaymentTerm> baseRepository) : base(paymentTermService, baseRepository)
        {
                _PaymentTermService = paymentTermService;
        }

    }
}
