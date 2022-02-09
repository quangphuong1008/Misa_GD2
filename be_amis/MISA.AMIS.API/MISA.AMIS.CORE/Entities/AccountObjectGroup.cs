using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.AMIS.CORE.Entities
{
    public class AccountObjectGroup:BaseEntity
    {
        public Guid AccountObjectGroupId { get; set; }
        public string AccountObjectGroupCode { get; set; }
        public string AccountObjectGroupName { get; set; }
    }
}
