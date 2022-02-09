using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.AMIS.CORE.Entities
{
    public class MasterDetailEntity : BaseEntity
    {
        public CaPayment CaPayment { get; set; }
        public List<CaPaymentDetail> CaPaymentDetailList { get; set; }
    }

    //public class MasterDetailEntity : BaseEntity
    //{
    //    public MasterEntity MasterEntity { get; set; }
    //    public DetailEntity DetailEntity { get; set; }
    //}

    //public class MasterEntity
    //{
    //    //public JObject Entity { get; set; }

    //    public object Entity { get; set; }
    //    public string Type { get; set; }
    //}

    //public class DetailEntity
    //{
    //    //public JArray Entity { get; set; }
    //    public List<object> Entity { get; set; }
    //    public string Type { get; set; }
    //}
}
