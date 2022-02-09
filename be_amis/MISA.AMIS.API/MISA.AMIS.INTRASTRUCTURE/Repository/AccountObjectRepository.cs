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
    public class AccountObjectRepository: BaseRepository<AccountObject>, IAccountObjectRepository
    {
        public AccountObjectRepository(IConfiguration configuration) : base(configuration)
        {

        }
        /// <summary>
        /// Hàm thêm mới đối tượng
        /// </summary>
        /// <param name="param">Thông tin đối tượng</param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// @Author nmquang 19-12-2021
        public override int Insert(AccountObject param)
        {

            string[] strs = { "EntityState", "AccountObjectBankAccountJson", "AccountObjectShippingAddressJson" };

            // Khai báo chuỗi SQL động:
            var sqlColumDynamic = "";
            var sqlParamDynamic = "";

            var dynamicParams = new DynamicParameters();

            // Lấy ra các properties của đối tượng:
            var props = param.GetType().GetProperties();
            // Duyệt từng property:
            foreach (var prop in props)
            {
                if (!strs.Contains(prop.Name)) { 
               // if (prop.Name != "EntityState" || prop.Name !="AccountObjectBankAccountJson") { 

                    // Lấy tên của property:
                    var propName = prop.Name;
                    // Lấy ra giá trị của property:
                    var propValue = prop.Name == "AccountObjectBankAccount" || prop.Name == "AccountObjectShippingAddress" ? JsonConvert.SerializeObject(prop.GetValue(param)) : prop.GetValue(param);

                    // Lấy ra kiểu dữ liệu của property:
                    var propType = prop.PropertyType;

                    if (propName == $"{_tableName}Id" && propType == typeof(Guid))
                    {
                        propValue = Guid.NewGuid();
                    }

                    // Bổ sung vào chuỗi Column động:
                    sqlColumDynamic += $"{propName},";
                    sqlParamDynamic += $"@{propName},";
                    dynamicParams.Add($"@{propName}", propValue);
                }

            };

            // Cắt phần tử dấu phẩy cuối cùng trong chuối:
            sqlColumDynamic = sqlColumDynamic.Substring(0, sqlColumDynamic.Length - 1);
            sqlParamDynamic = sqlParamDynamic.Substring(0, sqlParamDynamic.Length - 1);
            var className = param.GetType().Name;
            var sqlDynamic = $"INSERT INTO {className}({sqlColumDynamic}) VALUES({sqlParamDynamic})";

            var res = _sqlConnection.Execute(sqlDynamic, param: dynamicParams, commandType: CommandType.Text);
            return res;
        }
        /// <summary>
        /// Hàm cập nhật thông tin của đối tượng
        /// </summary>
        /// <param name="param">thông tin sau khi thay đổi của đối tương</param>
        /// <param name="AccountObjectId">Id của đối tượng</param>
        /// <returns>Số bản ghi bị ảnh hưởng</returns>
        /// @Author nmquang 19-12-2021
        public override int Update(AccountObject param, Guid AccountObjectId)
        {
            var _tableName = "AccountObject";
            string[] strs = { "EntityState", "AccountObjectBankAccountJson", "AccountObjectShippingAddressJson" };
            // Khai báo chuỗi SQL động:
            var sqlValue = "";
            var dynamicParams = new DynamicParameters();
            // Lấy ra các properties của đối tượng:
            var props = param.GetType().GetProperties();
            // Duyệt từng property:
            foreach (var prop in props)
            {
                if (!strs.Contains(prop.Name))
                {
                    // Lấy tên của property:
                    var propName = prop.Name;
                    // Lấy ra giá trị của property:
                    var propValue = prop.Name == "AccountObjectBankAccount" || prop.Name == "AccountObjectShippingAddress" ? JsonConvert.SerializeObject(prop.GetValue(param)) : prop.GetValue(param);

                    // Lấy ra kiểu dữ liệu của property:
                    var propType = prop.PropertyType;

                    if ((propName == $"{_tableName}Id") && propType == typeof(Guid))
                    {
                        continue;
                    }

                    sqlValue += $"{propName} = @{propName},";
                    dynamicParams.Add($"@{propName}", propValue);
                }
            };
            // bỏ dấu phẩy cuối cùng
            sqlValue = sqlValue.Substring(0, sqlValue.Length - 1);
            var className = param.GetType().Name;

            var sqlDynamic = $"UPDATE {className} SET {sqlValue} WHERE {_tableName}Id = @{_tableName}Id";
            dynamicParams.Add($"@{_tableName}Id", AccountObjectId);
            var res = _sqlConnection.Execute(sqlDynamic, param: dynamicParams);
            return res;
        }
        /// <summary>
        /// hàm lấy tất cả bản ghi của đối tượng
        /// </summary>
        /// <returns>Tất cả bản ghi</returns>
        /// @Author nmquang 19-12-2021
        public override List<AccountObject> GetAll()
        {
            var tableName = "AccountObject";
            var columns = "AccountObjectId,TaxCode,AccountObjectCode,AccountObjectName,Phone,Website,AccountObjectGroupListId,Address,EmployeeId,Prefix,EinvoiceContactName,EinvoiceContactMobile,EinvoiceContactEmail,LegalRepresentative,PaymentTermId,MaximizeDebtAmount,DueTime,AccountObjectBankAccount AS AccountObjectBankAccountJson ,AccountObjectShippingAddress AS AccountObjectShippingAddressJson,Country,District,WardOrCommune,ProvinceOrCity,Description,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,PayAccount";
            var employees = _sqlConnection.Query<AccountObject>($"SELECT {columns} FROM {tableName} ORDER BY {tableName}Code DESC");
            return employees.ToList();
        }
        /// <summary>
        /// Hàm lấy Đối tượng qua ID
        /// </summary>
        /// <param name="entityId">ID đối tượng</param>
        /// <returns>Đối tượng có ID tương ứng</returns>
        /// @Author nmquang 19-12-2021
        public override AccountObject GetById(Guid entityId)
        {
            var tableName = "AccountObject";
            var columns = "AccountObjectId,TaxCode,AccountObjectCode,AccountObjectName,Phone,Website,AccountObjectGroupListId,Address,EmployeeId,Prefix,EinvoiceContactName,EinvoiceContactMobile,EinvoiceContactEmail,LegalRepresentative,PaymentTermId,MaximizeDebtAmount,DueTime,AccountObjectBankAccount AS AccountObjectBankAccountJson ,AccountObjectShippingAddress AS AccountObjectShippingAddressJson,Country,District,WardOrCommune,ProvinceOrCity,Description,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,PayAccount";
           
            var sqlCommand = $"SELECT {columns} FROM {tableName} WHERE {tableName}Id = @{tableName}Id";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add($"@{tableName}Id", entityId);
            // QueryFirstOrDefault - Lấy bản ghi đầu tiền từ câu lệnh truy vấn, nếu không có dữ liệu thì trả về null:
            var employee = _sqlConnection.QueryFirstOrDefault<AccountObject>(sqlCommand, param: parameters);
            return employee;
        }
        /// <summary>
        /// Hàm lấy mã nhân viên mới
        /// </summary>
        /// <returns>mã nhân viên mới </returns>
        /// @Author nmquang 19-12-2021
        public override string GetEntityCode()
        {
            var accountObjectCode = _sqlConnection.Query<string>($"SELECT CONCAT('NCC', CONVERT(MAX(CONVERT(SUBSTRING(ao.AccountObjectCode,4,LENGTH(ao.AccountObjectCode)), int))+1, CHAR)) FROM AccountObject ao;");
            return accountObjectCode.ToList()[0];
        }
        /// <summary>
        /// tìm kiếm nhân viên theo mã code
        /// </summary>
        /// <param name="EntityCode"></param>
        /// <returns>Trả về bản ghi đầu tiên được tìm thấy </returns>
        /// @Author nmquang 19-12-2021
        public override AccountObject GetByEntityCode(string AccountObjectCode)
        {

            var tableName = "AccountObject";
            var columns = "AccountObjectId,TaxCode,AccountObjectCode,AccountObjectName,Phone,Website,AccountObjectGroupListId,Address,EmployeeId,Prefix,EinvoiceContactName,EinvoiceContactMobile,EinvoiceContactEmail,LegalRepresentative,PaymentTermId,MaximizeDebtAmount,DueTime,AccountObjectBankAccount AS AccountObjectBankAccountJson ,AccountObjectShippingAddress AS AccountObjectShippingAddressJson,Country,District,WardOrCommune,ProvinceOrCity,Description,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,PayAccount";

            var sqlCommand = $"SELECT {columns} FROM {tableName} WHERE {tableName}Code = @{tableName}Code";
            DynamicParameters parameters = new DynamicParameters();
            parameters.Add($"@{tableName}Code", AccountObjectCode);
            // QueryFirstOrDefault - Lấy bản ghi đầu tiền từ câu lệnh truy vấn, nếu không có dữ liệu thì trả về null:
            var employee = _sqlConnection.QueryFirstOrDefault<AccountObject>(sqlCommand, param: parameters);
            return employee;
        }

        /// <summary>
        /// Lọc dữ liệu kết hợp giữa search , filter, paging. 
        /// </summary>
        /// <param name="searchText">Ký tự muốn tìm kiếm</param>
        /// <param name="pageSize">Số lượng record trên 1 trang</param>
        /// <param name="m_PageIndex">Trang hiện tại</param>
        /// <returns>Danh sách bản ghi được tìm thấy</returns>
        /// @Author nmquang 19-12-2021
        public PagingDataAccountObject GetAccountObjectPaging(string searchText, int pageSize, int pageIndex)
        {
            //Tính lại pageIndex
            pageIndex = (pageIndex - 1) * pageSize;

            //Build tham số đầu vào cho store
            var input = searchText != null ? searchText : string.Empty;
            DynamicParameters parameters = new DynamicParameters();
            var totalRecord = 0;
            var totalPage = 0;

            parameters.Add("@m_AccountObjectName", input);
            parameters.Add("@m_AccountObjectCode", input);
            parameters.Add("@m_PageSize", pageSize);
            parameters.Add("@m_PageIndex", pageIndex);
            parameters.Add("@m_TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@m_TotalPage", dbType: DbType.Int32, direction: ParameterDirection.Output);
            var employees = _sqlConnection.Query<AccountObjectDto>("Proc_GetAccountObjectPaging", param: parameters, commandType: CommandType.StoredProcedure).ToList();
            totalRecord = parameters.Get<int>("@m_TotalRecord");
            totalPage = parameters.Get<int>("@m_TotalPage");


            return new PagingDataAccountObject
            {
                Data = employees,
                TotalRecord = totalRecord,
                TotalPage = totalPage,
            };
        }

        /// <summary>
        /// hàm lấy dữ liệu 
        /// </summary>
        /// <param name="filter">thông tin lọc</param>
        /// <returns>danh sách bản ghi được tìm thấy</returns>
        public PagingDataAccountObject GetAccountObjectPaging2(FilterAccountObject filter)
        {
            //Tính lại pageIndex
            filter.pageIndex = (filter.pageIndex - 1) * filter.pageSize;
            //Build tham số đầu vào cho store
            var input = filter.searchText != null ? filter.searchText : string.Empty;
            DynamicParameters parameters = MappingType(filter.filter);
            var totalRecord = 0;
            var totalPage = 0;
            parameters.Add("@m_TextSearch", input);   
            parameters.Add("@m_PageSize", filter.pageSize);
            parameters.Add("@m_PageIndex", filter.pageIndex);
            parameters.Add("@m_TotalRecord", dbType: DbType.Int32, direction: ParameterDirection.Output);
            parameters.Add("@m_TotalPage", dbType: DbType.Int32, direction: ParameterDirection.Output);
            var datas = _sqlConnection.Query<AccountObjectDto>("Proc_GetAccountObjectPaging2", param: parameters, commandType: CommandType.StoredProcedure).ToList();
            totalRecord = parameters.Get<int>("@m_TotalRecord");
            totalPage = parameters.Get<int>("@m_TotalPage");

            return new PagingDataAccountObject
            {
                Data = datas,
                TotalRecord = totalRecord,
                TotalPage = totalPage,
            };
        }

        /// <summary>
        /// Chuyển đổi enity về 1 mảng đối tượng @...
        /// </summary>
        /// <param name="entity">đối tượng</param>
        /// <returns>Trả về 1 DynamicParameters</returns>
        
    }
}
