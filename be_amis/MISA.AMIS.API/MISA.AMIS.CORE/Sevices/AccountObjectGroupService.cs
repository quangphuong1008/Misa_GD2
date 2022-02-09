using MISA.AMIS.CORE.Entities;
using MISA.AMIS.CORE.Interfaces.Repository;
using MISA.AMIS.CORE.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.AMIS.CORE.Sevices
{
    public class AccountObjectGroupService : BaseService<AccountObjectGroup>, IAccountObjectGroupService
    {
        IAccountObjectGroupRepository _accountObjectGroupRepository;
        public AccountObjectGroupService(IAccountObjectGroupRepository accountObjectGroupRepository) : base(accountObjectGroupRepository)
        {
            _accountObjectGroupRepository = accountObjectGroupRepository;
        }

    }
}
