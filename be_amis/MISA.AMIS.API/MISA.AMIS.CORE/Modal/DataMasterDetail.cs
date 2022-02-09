using System;
using System.Collections.Generic;
using System.Text;
using Dapper;

namespace MISA.AMIS.CORE.Modal
{
    public class DataMasterDetail
    {
        public Guid? masterID { get; set; }
        public string sqlDynamic { get; set; }
        public DynamicParameters dynamicParams { get; set; }
    }
}
