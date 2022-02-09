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
    public class CaPaymentController : AMISControlller<MasterDetailEntity>
    {
        ICAPaymentService _CAPaymentService;
       
        public CaPaymentController(ICAPaymentService CAPaymentService, IBaseRepository<MasterDetailEntity> baseRepository) : base(CAPaymentService, baseRepository)
        {
            _CAPaymentService = CAPaymentService;
        }
        /// <summary>
        /// thêm phiếu chi và chi tiết phiếu chi
        /// </summary>
        /// <param name="entity">thông tin phiếu chi được thêm</param>
        /// <returns>trả về số bản ghi được tác động</returns>
        [HttpPost("insertMasterDetail")]
        public IActionResult Insertmasterdetail(MasterDetailEntity entity)
        {
            try
            {
                var res = _CAPaymentService.InsertMasterDetail(entity);
                if (res.MisaCode == MISA.AMIS.CORE.Entity.MISAEnum.Success)
                {
                    return StatusCode(201, res.Data);
                }
                else
                {
                    return BadRequest(res);
                }
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }

        /// <summary>
        /// Hàm lấy data và dữ liệu pagging
        /// </summary>
        /// <param name="filter"> thông tin tìm kiếm</param>
        /// <returns>Các bản ghi được tìm thấy và thông tin số bản ghi/trang và số trang</returns>
        [HttpPost("filterPage")]
        public IActionResult GetDataCaPayment(FilterCaPayment filter)
        {
            try
            {
                var entities = _CAPaymentService.GetDataCaPayment(filter);
                return Ok(entities);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }

        }

        /// <summary>
        /// Phương thức Delete, Xóa dữ liệu của đối tượng trên cơ sở dữ liệu
        /// </summary>
        /// <param name="entityId">id của đối tượng cần xóa</param>
        /// <returns>Số bản ghi bị ảnh hưởng. Nếu lỗi trả về thông tin lỗi</returns>
        /// @Author nmquang 19-12-2021
        [HttpDelete("MaserDetail/{entityId}")]
        public IActionResult DeleteCaPayment(Guid entityId)
        {
            try
            {
                {
                    var res = _CAPaymentService.DeleteCaPayment(entityId);
                    return Ok(res);
                }
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }


        }





        /// <summary>
        /// Phương thức Delete. Xóa nhiều nhân viên
        /// Truyền qua body, phải truyền vào kiểu đối tượng, ko được phép truyền kiểu dữ liệu nguyên thủy
        /// </summary>
        /// <param name="listId">danh sách id nhân viên cần xóa</param>
        /// <returns>trả về 0 nếu xóa thành công</returns>
        /// @Author nmquang 19-12-2021
        [HttpDelete("MaserDetail/DeleteMulti/{listId}")]
        public IActionResult DeleteMultipleCaPaymentRecord(string listId)
        {
            //nếu xóa tất cả listId = list id = 0000-0000-0000-000-0000
            try
            {
                var rowAffect = _CAPaymentService.DeleteMultipleCaPaymentRecords(listId);
                return Ok(rowAffect);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }

        }
        /// <summary>
        /// hàm lấy số phiếu chi
        /// </summary>
        /// <returns> số phiếu chi</returns>
        [HttpGet("NewNo")]
        public IActionResult GetNewEntityCode()
        {
            try
            {
                var employeeCode = _CAPaymentService.GetCaPaymentNo();
                return Ok(employeeCode);
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
            var result = _CAPaymentService.DataExcel();
            // đường dẫn đến file gốc
            var filePath = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\MISA.AMIS.CORE\ExcelTemplate\Thu_chi_tien_mat.xlsx"));
            FileInfo existingFile = new FileInfo(filePath);
            // bản quyền
            ExcelPackage.LicenseContext = LicenseContext.Commercial;
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (ExcelPackage package = new ExcelPackage(existingFile))
            {

                ExcelWorksheet sheet = package.Workbook.Worksheets[0];
                // đổ dữ liệu vào sheet
                sheet.Cells[2, 1].Value = DateTime.Now.ToString("dd-MM-yyyy");
                int rowId = 4;
                int stt = 1;
                foreach (var row in result)
                {
                    sheet.Cells[rowId, 1].AutoFitColumns(10, 10);
                    for (int i = 1; i <= 8; i++)
                    {
                        // Thêm border cho cột
                        sheet.Cells[rowId, i].Style.Border.Top.Style = ExcelBorderStyle.Thin;
                        sheet.Cells[rowId, i].Style.Border.Left.Style = ExcelBorderStyle.Thin;
                        sheet.Cells[rowId, i].Style.Border.Right.Style = ExcelBorderStyle.Thin;
                        sheet.Cells[rowId, i].Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                        // Thêm width vs height cho cột
                        sheet.Cells[rowId, i + 1].AutoFitColumns(20, 20);
                        // ghép border các cột
                        sheet.Cells[rowId, i + 1].Merge = true;
                    }
                    sheet.Cells[rowId, 1].Value = stt;
                    sheet.Cells[rowId, 2].Value = row.PostedDate;
                    sheet.Cells[rowId, 3].Value = row.CaPaymentDate;
                    sheet.Cells[rowId, 4].Value = row.CaPaymentNo;
                    sheet.Cells[rowId, 5].Value = row.Resion;
                    sheet.Cells[rowId, 6].Value = row.TotalAmount;
                    sheet.Cells[rowId, 7].Value = row.AccountObjectName;
                    sheet.Cells[rowId, 8].Value = row.AccountObjectCode;
                    stt++;
                    rowId++;
                }
                stream = new MemoryStream(package.GetAsByteArray());
            }
            stream.Position = 0;
            var fileName = $"DanhSachPhieuChi_{DateTime.Now.ToString("dd-MM-yyyy")}.xlsx";
            return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",  // trả về file xlsx
                fileName);
        }

    }
}
