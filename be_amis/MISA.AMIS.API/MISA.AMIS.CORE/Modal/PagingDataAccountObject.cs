using MISA.AMIS.CORE.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.AMIS.CORE.Modal
{
    public class PagingDataAccountObject
    {
        public List<AccountObjectDto> Data { get; set; }
        public int? TotalRecord { get; set; }
        public int? TotalPage { get; set; }
    }
}
