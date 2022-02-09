using Microsoft.Extensions.Configuration;
using MISA.AMIS.CORE.Entities;
using MISA.AMIS.CORE.Interfaces.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.AMIS.INTRASTRUCTURE.Repository
{
    public class AccountObjectGroupRepository : BaseRepository<AccountObjectGroup>, IAccountObjectGroupRepository
    {
        public AccountObjectGroupRepository(IConfiguration configuration) : base(configuration)
    {

    }
}
}
