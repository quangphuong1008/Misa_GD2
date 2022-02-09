using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.AMIS.CORE.Modal
{
    public class FilterCaPayment2
    {
        public DateTime startTime { get; set; }
        public DateTime endTime { get; set; }
        public string searchText { get; set; }
        public int pageSize { get; set; }
        public int pageIndex { get; set; }
        public List<FilterColum> listFilter { get; set; }
    }
    public class FilterColum
    {
        public string table_name { get; set; }
        public string type { get; set; }
        public string value  { get; set; }

    }
}
