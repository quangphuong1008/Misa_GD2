using Dapper;
using Microsoft.Extensions.Configuration;
using MISA.AMIS.CORE.Entities;
using MISA.AMIS.CORE.Interfaces.Repository;
using MISA.AMIS.CORE.Modal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace MISA.AMIS.INTRASTRUCTURE.Repository
{
    public class CAPaymentReponsitory : BaseRepository<MasterDetailEntity>, ICAPaymentReponsitory
    {
        public CAPaymentReponsitory(IConfiguration configuration) : base(configuration)
        {

        }
        /// <summary>
        /// thêm Phiếu chi va chi tiết phiếu chi
        /// </summary>
        /// <param name="param">thông tin phiếu chi</param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        public int InsertMasterDetail(MasterDetailEntity param)
        {
          //  DynamicParameters[] dynamicParams = new DynamicParameters[param.CaPaymentDetailList.Count()];
            var sqlDynamic = "";
            var dynamicParams = new DynamicParameters();
            var dataMaster = new DataMasterDetail();
            var dataDetail = new DataMasterDetail();
            var res = 0;
            if (param.CaPayment != null)
            {
                
                dataMaster = InsertMode(param.CaPayment,0);

                var master = _sqlConnection.Execute(dataMaster.sqlDynamic, param: dataMaster.dynamicParams, commandType: CommandType.Text);
                res += master;
            }
            if(param.CaPaymentDetailList != null)
            {
                var ind = 0;
                foreach (var cAPaymentDetail in param.CaPaymentDetailList)
                {
                    cAPaymentDetail.CaPaymentId = (Guid)dataMaster.masterID;
                    dataDetail = InsertMode(cAPaymentDetail,ind);
                    sqlDynamic += $"{dataDetail.sqlDynamic};";

                   // dynamicParams[ind] = dataDetail.dynamicParams;
                    dynamicParams.AddDynamicParams(dataDetail.dynamicParams);
                    ind++;
                }
                // Cắt phần tử dấu chấm phẩy cuối cùng trong chuối:
                sqlDynamic = sqlDynamic.Substring(0, sqlDynamic.Length - 1);
 
                var detail = _sqlConnection.Execute(sqlDynamic, param: dynamicParams, commandType: CommandType.Text);
                res += detail;
            }
            return res;
        }
        /// <summary>
        /// hàm lấy các paramDynamic phục vụ cho kếts nối databasse
        /// </summary>
        /// <param name="param">objec lấy dữ liệu</param>
        /// <param name="index">index của objec trong mảng</param>
        /// <returns></returns>
        public DataMasterDetail InsertMode(Object param, int index)
        {
            string[] strs = { "EntityState" };

            // Khai báo chuỗi SQL động:
            var sqlColumDynamic = "";
            var sqlParamDynamic = "";
            var dynamicParams = new DynamicParameters();
            var className = param.GetType().Name;
            var Id = new Guid();
            // Lấy ra các properties của đối tượng:
            var props = param.GetType().GetProperties();
            // Duyệt từng property:
            foreach (var prop in props)
            {
                if (!strs.Contains(prop.Name))
                {
                    // if (prop.Name != "EntityState" || prop.Name !="AccountObjectBankAccountJson") { 

                    // Lấy tên của property:
                    var propName = prop.Name;
                    // Lấy ra giá trị của property:
                    var propValue = prop.Name == "AccountObjectBankAccount" || prop.Name == "AccountObjectShippingAddress" ? JsonConvert.SerializeObject(prop.GetValue(param)) : prop.GetValue(param);

                    // Lấy ra kiểu dữ liệu của property:
                    var propType = prop.PropertyType;

                    if (propName == $"{className}Id" && propType == typeof(Guid))
                    {
                        propValue = Guid.NewGuid();
                        Id = (Guid)propValue;
                    }
                    var dynamicParam = $"@{propName}{index}";
                    // Bổ sung vào chuỗi Column động:
                    sqlColumDynamic += $"{propName},";
                    sqlParamDynamic += $"{dynamicParam},";
                    dynamicParams.Add(dynamicParam, propValue);
                }

            };

            // Cắt phần tử dấu phẩy cuối cùng trong chuối:
            sqlColumDynamic = sqlColumDynamic.Substring(0, sqlColumDynamic.Length - 1);
            sqlParamDynamic = sqlParamDynamic.Substring(0, sqlParamDynamic.Length - 1);
          
            var sqlDynamic = $"INSERT INTO {className}({sqlColumDynamic}) VALUES({sqlParamDynamic})";

            return new DataMasterDetail
            {
                masterID = Id,
                sqlDynamic = sqlDynamic,
                dynamicParams = dynamicParams
            };
        }
        /// <summary>
        /// override lại hàm lấy dối tượng qua id
        /// </summary>
        /// <param name="entityId">id đối tượng</param>
        /// <returns>thông tin đối tượng</returns>
        public override object GetMasterDetailById(Guid entityId)
        {
            var _master = "CaPayment";
            var _detail = "CaPaymentDetail";
            var sqlCommand = $"SELECT * FROM {_master} WHERE {_master}Id = @{_master}Id";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add($"@{_master}Id", entityId);
            // QueryFirstOrDefault - Lấy bản ghi đầu tiền từ câu lệnh truy vấn, nếu không có dữ liệu thì trả về null:
            var master = _sqlConnection.QueryFirstOrDefault<CaPayment>(sqlCommand, param: parameters);
   
            var details = _sqlConnection.Query<CaPaymentDetail>($"SELECT * FROM {_detail}  WHERE {_master}Id = @{_master}Id", param: parameters);
            
            return new
            {
                master = master,
                detail = details.ToList()
            };
        }
        /// <summary>
        /// override lại hàm lấy đối tượng qua mã Code
        /// </summary>
        /// <param name="entityCode">mã đối tượng</param>
        /// <returns>thông tin đối tượng</returns>
        public override object GetMasterDetailByCode(string entityCode)
        {
            var _master = "CaPayment";
            var sqlCommand = $"SELECT * FROM {_master} WHERE {_master}No = @{_master}No";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add($"@{_master}No", entityCode);
            // QueryFirstOrDefault - Lấy bản ghi đầu tiền từ câu lệnh truy vấn, nếu không có dữ liệu thì trả về null:
            var master = _sqlConnection.QueryFirstOrDefault<CaPayment>(sqlCommand, param: parameters);


            return new
            {
                master = master
              
            };
        }
        // <summary>
        /// lấy dữ liệu đổ vào file excel
        /// </summary>
        /// <returns>các bản ghi được tìm thấy</returns>
        public List<CaPaymentDto> DataExcel()
        {
            var data = _sqlConnection.Query<CaPaymentDto>($"SELECT ca.*, ao.AccountObjectCode, ao.AccountObjectName FROM CaPayment ca LEFT JOIN AccountObject ao ON ca.AccountObjectId = ao.AccountObjectId ORDER BY ca.CaPaymentNo DESC");
            return data.ToList();
        }
        /// <summary>
        /// hàm lấy dữ liệu phiếu chi
        /// </summary>
        /// <param name="filter">dữ liệu lọc</param>
        /// <returns>các bản ghi và tông tin số trang và trang</returns>
        public PagingDataCaPayment GetDataCaPayment(FilterCaPayment filter)
        {
            {
                //Tính lại pageIndex
                filter.pageIndex = (filter.pageIndex - 1) * filter.pageSize;
                //Build tham số đầu vào cho store
                var input = filter.searchText != null ? filter.searchText : string.Empty;
                //  DynamicParameters parameters =    MappingType(filter.filter);
                DynamicParameters parameters = new DynamicParameters();
                var totalRecord = 0;
                var totalPage = 0;
                parameters.Add("@startTime", filter.startTime);
                parameters.Add("@endTime", filter.endTime);
                parameters.Add("@m_TextSearch", input);
                parameters.Add("@m_PageSize", filter.pageSize);
                parameters.Add("@m_PageIndex", filter.pageIndex);
                parameters.Add("@m_TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("@m_TotalPage", dbType: DbType.Int32, direction: ParameterDirection.Output);
                var datas = _sqlConnection.Query<CaPaymentDto>("Proc_GetCaPaymentPaging", param: parameters, commandType: CommandType.StoredProcedure).ToList();
                totalRecord = parameters.Get<int>("@m_TotalRecord");
                totalPage = parameters.Get<int>("@m_TotalPage");

                return new PagingDataCaPayment
                {
                    Data = datas,
                    TotalRecord = totalRecord,
                    TotalPage = totalPage,
                };
            }
        }
        public PagingDataCaPayment GetDataCaPayment2(FilterCaPayment2 filter)
        {
            {
                //Tính lại pageIndex
                filter.pageIndex = (filter.pageIndex - 1) * filter.pageSize;
                //Build tham số đầu vào cho store
                var input = filter.searchText != null ? filter.searchText : string.Empty;
                DynamicParameters parameters = new DynamicParameters();
                var totalRecord = 0;
                var totalPage = 0;
                parameters.Add("@startTime", filter.startTime);
                parameters.Add("@endTime", filter.endTime);
                parameters.Add("@m_TextSearch", input);
                parameters.Add("@m_PageSize", filter.pageSize);
                parameters.Add("@m_PageIndex", filter.pageIndex);
                parameters.Add("@m_TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);
                parameters.Add("@m_TotalPage", dbType: DbType.Int32, direction: ParameterDirection.Output);
                var datas = _sqlConnection.Query<CaPaymentDto>("Proc_GetCaPaymentPaging2", param: parameters, commandType: CommandType.StoredProcedure).ToList();
                totalRecord = parameters.Get<int>("@m_TotalRecord");
                totalPage = parameters.Get<int>("@m_TotalPage");

                return new PagingDataCaPayment
                {
                    Data = datas,
                    TotalRecord = totalRecord,
                    TotalPage = totalPage,
                };
            }
        }
        /// <summary>
        /// Hàm xóa đối tượng
        /// </summary>
        /// <param name="entityId">id của đối tượng</param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// @Author nmquang 19-12-2021
        public int DeleteCaPayment(Guid entityId)
        {
            var master = "CaPayment";
            var detai = "CaPaymentDetail";
            var sqlCommand = $"DELETE FROM {master} WHERE {master}Id = @{master}Id";
            var sqlCommand1 = $"DELETE FROM {detai} WHERE {master}Id = @{master}Id";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add($"@{master}Id", entityId);
            // QueryFirstOrDefault - Lấy bản ghi đầu tiền từ câu lệnh truy vấn, nếu không có dữ liệu thì trả về null:
            var res = _sqlConnection.Execute(sqlCommand1, param: parameters);
            var res1 = _sqlConnection.Execute(sqlCommand, param: parameters);
            return res + res1;
        }
        /// <summary>
        /// Method sử dụng để xóa nhiều bản ghi của bảng Employee
        /// </summary>
        /// <param name="listId">Danh sách id của tài sản</param>
        /// <returns>Trả về số bản ghi ảnh hưởng khi xóa</returns>     
        /// @Author nmquang 19-12-2021
        public int DeleteMultipleCaPaymentRecords(string listId)
        {
            var master = "CaPayment";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add("listId", listId);

            var rowAffect = 0;
            rowAffect = _sqlConnection.Execute($"Proc_DeleteMultiple{master}", param: parameters, commandType: CommandType.StoredProcedure);
            return rowAffect;
        }

        /// <summary>
        /// lấy số phiếu chi mới
        /// </summary>
        /// <returns> số phiếu chi mới</returns>
        public string GetCaPaymentNo()
        {
            var code = _sqlConnection.Query<string>(" SELECT CONCAT('PC', CONVERT(MAX(CONVERT(SUBSTRING(CaPaymentNo,3,LENGTH(CaPaymentNo)), int))+1, CHAR)) FROM CaPayment;");
            return code.ToList()[0];
        }
    }
}
