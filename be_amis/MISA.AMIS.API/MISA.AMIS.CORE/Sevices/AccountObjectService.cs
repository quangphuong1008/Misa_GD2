using MISA.AMIS.CORE.Entities;
using MISA.AMIS.CORE.Interfaces.Repository;
using MISA.AMIS.CORE.Interfaces.Services;
using MISA.AMIS.CORE.Modal;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.AMIS.CORE.Sevices
{
    public class AccountObjectService: BaseService<AccountObject>, IAccountObjectService
    {
        IAccountObjectRepository _accountObjectRepository;
        public AccountObjectService(IAccountObjectRepository accountObjectRepository) : base(accountObjectRepository)
        {
            _accountObjectRepository = accountObjectRepository;
        }

        public IEnumerable<AccountObject> DataExcel()
        {
            var data = _accountObjectRepository.GetAll();
            return data;
        }

        public PagingDataAccountObject GetAccountObjectPaging(string searchText, int pageSize, int m_PageIndex)
        {
            return _accountObjectRepository.GetAccountObjectPaging(searchText, pageSize, m_PageIndex);
        }

        public PagingDataAccountObject GetAccountObjectPaging2(FilterAccountObject filter)
        {
            return _accountObjectRepository.GetAccountObjectPaging2(filter);
        }

 
    }
}
