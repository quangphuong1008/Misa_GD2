using MISA.AMIS.CORE.Entities;
using MISA.AMIS.CORE.Entity;
using MISA.AMIS.CORE.Interfaces.Repository;
using MISA.AMIS.CORE.Interfaces.Services;
using MISA.AMIS.CORE.Modal;
using System;
using System.Collections.Generic;
using System.Text;

namespace MISA.AMIS.CORE.Sevices
{
    public class CAPaymentService: BaseService<MasterDetailEntity>, ICAPaymentService
    {
        ICAPaymentReponsitory _CAPaymnetReponsitory;
        public CAPaymentService(ICAPaymentReponsitory cAPaymnetReponsitory) : base(cAPaymnetReponsitory)
        {
            _CAPaymnetReponsitory = cAPaymnetReponsitory;
        }
        /// <summary>
        /// hàm lấy dữ liệu kết hợp paging tìm kiếm
        /// </summary>
        /// <param name="filter">thông tin tìm kiếm</param>
        /// <returns>các bản ghi tương ứng</returns>
        public PagingDataCaPayment GetDataCaPayment(FilterCaPayment filter)
        {
            return _CAPaymnetReponsitory.GetDataCaPayment(filter);
            
        }
        /// <summary>
        /// hàm lấy dữ liệu kết hợp paging tìm kiếm
        /// </summary>
        /// <param name="filter">thông tin tìm kiếm</param>
        /// <returns>các bản ghi tương ứng</returns>
        public PagingDataCaPayment GetDataCaPayment2(FilterCaPayment2 filter)
        {
            return _CAPaymnetReponsitory.GetDataCaPayment2(filter);

        }

        public ServiceResult InsertMasterDetail(MasterDetailEntity entity)
        {
            entity.EntityState = EntityState.AddNew;
            // Xử lý nghiệp vụ:
            var isValid = true;
            var validate  = ValidateObject(entity.CaPayment);
            if (!validate)
            {
                isValid = false;
            }

            foreach (var caPaymentDetal in entity.CaPaymentDetailList)
            {
                validate = ValidateObject(caPaymentDetal);
                if (!validate)
                {
                    isValid = false;
                }
            }
            
            if (isValid == true)
            {
                isValid = ValidateObjectCustom(entity);
            }
            // Thực hiện cập nhật dữ liệu:
            if (isValid == true)
                _serviceResult.Data = _CAPaymnetReponsitory.InsertMasterDetail(entity);
            else
            {
                _serviceResult.MisaCode = MISAEnum.NotValid;

                _serviceResult.Messeger = MyResource.Resource.Msg_IsNotValid;
            }
            return _serviceResult;
        }

        public ServiceResult DeleteCaPayment(Guid entityId)
        {
            _serviceResult.Data = _CAPaymnetReponsitory.DeleteCaPayment(entityId);
            return _serviceResult;
        }


        public ServiceResult DeleteMultipleCaPaymentRecords(string listId)
        {
            _serviceResult.Data = _CAPaymnetReponsitory.DeleteMultipleCaPaymentRecords(listId);
            return _serviceResult;
        }

        private bool ValidateObject(object cAPayment)
        {
            return true;
        }

        public IEnumerable<CaPaymentDto> DataExcel()
        {
            var data = _CAPaymnetReponsitory.DataExcel();
            return data;
        }

        public ServiceResult GetCaPaymentNo()
        {
            _serviceResult.Data = _CAPaymnetReponsitory.GetCaPaymentNo();
            return _serviceResult;
        }

        //private bool ValidateObject(CaPaymentDetail caPaymentDetal)
        //{
        //    return true;
        //}
    }
}
