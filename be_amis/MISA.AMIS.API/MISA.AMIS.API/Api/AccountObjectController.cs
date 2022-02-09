using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.CORE.Entities;
using MISA.AMIS.CORE.Interfaces.Repository;
using MISA.AMIS.CORE.Interfaces.Services;
using MISA.AMIS.CORE.Modal;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MISA.AMIS.API.Api
{
    public class AccountObjectController: AMISControlller<AccountObject>
    {
        IAccountObjectService _accountObjectService;
        //IDepartmentRepository departmentRepository;
        public AccountObjectController(IAccountObjectService accountObjectService, IBaseRepository<AccountObject> baseRepository) : base(accountObjectService, baseRepository)
        {
            _accountObjectService = accountObjectService;
        }
        /// <summary>
        /// Hàm lấy data và dữ liệu pagging
        /// </summary>
        /// <param name="filter"> thông tin tìm kiếm</param>
        /// <returns>Các bản ghi được tìm thấy và thông tin số bản ghi/trang và số trang</returns>
        [HttpPost("Filterr")]
        public object GetAccountObjectPaging2(FilterAccountObject filter)
        {
            try
            {
                return _accountObjectService.GetAccountObjectPaging2(filter);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }

        }

        /// <summary>
        /// Phương thức Get. Lấy dữ liệu export ra file excel
        /// </summary>
        /// <returns>fiel excel</returns>
        ///  @Author nmquang 19-12-2021
        [HttpGet("ExportExcel")]
        public IActionResult Export()
        {
            //Tạo đối tượng memoryStream

            var stream = new MemoryStream();
            var result = _accountObjectService.DataExcel();
            // đường dẫn đến file gốc
            var filePath = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\MISA.AMIS.CORE\ExcelTemplate\Danh_sach_nha_cung_cap.xlsx"));
            FileInfo existingFile = new FileInfo(filePath);
            // bản quyền
            ExcelPackage.LicenseContext = LicenseContext.Commercial;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage package = new ExcelPackage(existingFile))
            {
                // sheet 1
                ExcelWorksheet sheet = package.Workbook.Worksheets[0];
                // đổ dữ liệu vào sheet
                sheet.Cells[2, 1].Value = DateTime.Now.ToString("dd-MM-yyyy");
                int rowId = 4;
                int stt = 1;
                foreach (var row in result)
                {
                    sheet.Cells[rowId, 1].AutoFitColumns(10, 10);
                    for (int i = 1; i <= 7; i++)
                    {
                        // Thêm border cho cột
                        sheet.Cells[rowId, i].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        sheet.Cells[rowId, i].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        sheet.Cells[rowId, i].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        sheet.Cells[rowId, i].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        // Thêm width vs height cho cột
                        sheet.Cells[rowId, i + 1].AutoFitColumns(20, 20);
                        sheet.Cells[rowId, i + 1].Merge = true;
                    }
                    sheet.Cells[rowId, 1].Value = stt;
                    sheet.Cells[rowId, 2].Value = row.AccountObjectCode;
                    sheet.Cells[rowId, 3].Value = row.AccountObjectName;
                    sheet.Cells[rowId, 4].Value = row.Address;
                    sheet.Cells[rowId, 5].Value = row.AccountObjectGroupListId;
                    sheet.Cells[rowId, 6].Value = row.TaxCode;
                    sheet.Cells[rowId, 7].Value = row.Phone;
                    stt++;
                    rowId++;
                }
                stream = new MemoryStream(package.GetAsByteArray());
            }
            stream.Position = 0;
            var fileName = $"DanhSachNhaCungCap_{DateTime.Now.ToString("dd-MM-yyyy")}.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                fileName);
        }
    }
}
