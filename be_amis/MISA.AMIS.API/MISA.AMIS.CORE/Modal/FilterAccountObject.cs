using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.AMIS.CORE.Modal
{
    public class FilterAccountObject
    {
        public Filter filter { get; set; }
        public string searchText { get; set; }
        public int pageSize { get; set; }
        public int pageIndex { get; set; }

    }

    public class Filter
    {
        public string m_AccountObjectGroupCode { get; set; }
        /// <summary>
        /// Thành phố
        /// </summary>
        /// CreatedBy: NmQuang(14/1/2022)
        public string m_ProvinceOrCity { get; set; }
        /// <summary>
        /// Quận huyện
        /// </summary>
        /// CreatedBy: NmQuang(14/1/2022)
        public string m_District { get; set; }
        /// <summary>
        /// Phường xã
        /// </summary>
        /// CreatedBy: NmQuang(14/1/2022)
        public string m_WardOrCommune { get; set; }

    }
}
