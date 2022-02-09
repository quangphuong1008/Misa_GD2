using Microsoft.Extensions.Configuration;
using MISA.AMIS.CORE.Entities;
using MISA.AMIS.CORE.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.AMIS.INTRASTRUCTURE.Repository
{
    public class PaymentTermRepository : BaseRepository<PaymentTerm>, IPaymentTermRepository
    {
        public PaymentTermRepository(IConfiguration configuration) : base(configuration)
        {

        }
    }
}
