using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.AMIS.CORE.Modal
{
    public class FilterCaPayment
    {
        //   public Filter filter { get; set; }
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }
        public string searchText { get; set; }
        public int pageSize { get; set; }
        public int pageIndex { get; set; }

    }

    public class FilterCa
    {

    }
}
