using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.CORE.Entities;
using MISA.AMIS.CORE.Interfaces.Repository;
using MISA.AMIS.CORE.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.AMIS.API.Api
{
    public class AccountObjectGroupController : AMISControlller<AccountObjectGroup>
    {
        IAccountObjectGroupService _AccountObjectGroupService;
        public AccountObjectGroupController(IAccountObjectGroupService accountObjectGroupService, IBaseRepository<AccountObjectGroup> baseRepository) : base(accountObjectGroupService, baseRepository)
        {
            _AccountObjectGroupService = accountObjectGroupService;
        }

    }
}
