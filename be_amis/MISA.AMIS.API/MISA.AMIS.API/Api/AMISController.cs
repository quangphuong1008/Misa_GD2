
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.CORE.Entities;
using MISA.AMIS.CORE.Entity;
using MISA.AMIS.CORE.Interfaces.Repository;
using MISA.AMIS.CORE.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace MISA.AMIS.API.Api
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AMISControlller<TEntity> : ControllerBase
    {

        IBaseService<TEntity> _baseService;
        public AMISControlller(IBaseService<TEntity> baseService, IBaseRepository<TEntity> baseRepository)
        {
            _baseService = baseService;

        }
        /// <summary>
        /// Phương thức Get, lấy tất cả dữ liệu của đối tượng
        /// </summary>
        /// <returns>Dữ liệu của đối tượng. Nếu lỗi trả về thông tin lỗi</returns>
        /// @Author nmquang 19-12-2021
        [HttpGet]
        public virtual IActionResult Get()
        {
            try
            {
                var entities = _baseService.GetAll();
                return Ok(entities);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }

        }

        /// <summary>
        /// Phương thức Get, lấy dữ liệu của đối tượng được tìm kiếm quan id
        /// </summary>
        /// <param name="entityId">id của đối tượng</param>
        /// <returns>Dữ liệu của đối tượng được tìm kiếm. Nếu lỗi trả về thông tin lỗi</returns>
        /// @Author nmquang 19-12-2021


        /// <summary>
        /// Phương thức Get. Lấy thông tin của đối tượng trong cơ sở dữ liệu
        /// </summary>
        /// <returns>Thông tin của đối tượng. Nếu gặp lỗi trả về thông tin lỗi</returns>
        /// @Author nmquang 19-12-2021

        [HttpGet("{entityId}")]
        public IActionResult Get(Guid entityId)
        {
            try
            {
                var entity = _baseService.GetById(entityId);
                return Ok(entity);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }
        }


        [HttpGet("GetMasterDetailById")]
        public IActionResult GetMasterDetailById(Guid id)
        {
            try
            {
                var entity = _baseService.GetMasterDetailById(id);
                return Ok(entity);
            }
            catch (Exception ex)
            {

                return HandleException(ex);
            }
        }

        [HttpGet("GetMasterDetailByCode")]
        public IActionResult GetMasterDetailByCode(string Code)
        {
            try
            {
                var entity = _baseService.GetMasterDetailByCode(Code);
                return Ok(entity);
            }
            catch (Exception ex)
            {

                return HandleException(ex);
            }
        }

        /// <summary>
        /// Phương thức Get. Lấy Mã mới của đối tượng không chứa trong cơ sở dữ liệu
        /// </summary>
        /// <returns>Mã code mới. Nếu gặp lỗi trả về thông tin lỗi</returns>
        [HttpGet("NewCode")]
        public IActionResult GetNewEntityCode()
        {
            try
            {
                var employeeCode = _baseService.GetEntityCode();
                return Ok(employeeCode);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }

        }

        [HttpGet("GetByCode")]
        public IActionResult GetByEntityCode(string EntityCode)
        {
            try
            {
                //nếu null hoặc '' thì ko xử lí gì cả , trả về luon
                if (String.IsNullOrEmpty(EntityCode))
                {
                    return BadRequest();
                }
                var entity = _baseService.GetByEntityCode(EntityCode);
                return Ok(entity);
            }
            catch (Exception ex)
            {

                return HandleException(ex);
            }

        }
        /// <summary>
        /// Phương thức Post, Thêm thông tin của đối tượng vào Cơ sở dữ liệu
        /// </summary>
        /// <param name="entity">Thông tin của đối tượng cần thêm</param>
        /// <returns>Số bản ghi bị ảnh hưởng. Nếu lỗi trả về thông tin lỗi</returns>
        /// @Author nmquang 19-12-2021
        [HttpPost]
        public IActionResult Post(TEntity entity)
        {
            try
            {
                var res = _baseService.Insert(entity);
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

        // [HttpPost("insertMasterDetail")]
        //public IActionResult InsertMasterDetail(MasterDetailEntity entity)
        //{
        //    try
        //    {
        //        var res = _baseService.SaveMasterDetail(entity);
        //        if (res.MisaCode == MISA.AMIS.CORE.Entity.MISAEnum.Success)
        //        {
        //            return StatusCode(201, res.Data);
        //        }
        //        else
        //        {
        //            return BadRequest(res);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return HandleException(ex);
        //    }
        //    //   return 0;
        //}

        /// <summary>
        /// Phương thức Delete, Xóa dữ liệu của đối tượng trên cơ sở dữ liệu
        /// </summary>
        /// <param name="entityId">id của đối tượng cần xóa</param>
        /// <returns>Số bản ghi bị ảnh hưởng. Nếu lỗi trả về thông tin lỗi</returns>
        /// @Author nmquang 19-12-2021
        [HttpDelete("{entityId}")]
        public IActionResult Delete(Guid entityId)
        {
            try
            {
                {
                    var res = _baseService.Delete(entityId);
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
        [HttpDelete("DeleteMulti/{listId}")]
        public IActionResult DeleteMultipleRecord(string listId)
        {
            //nếu xóa tất cả listId = list id = 0000-0000-0000-000-0000
            try
            {
                var rowAffect = _baseService.DeleteMultipleRecords(listId);
                return Ok(rowAffect);
            }
            catch (Exception ex)
            {
                return HandleException(ex);
            }

        }


        /// <summary>
        /// Phương thức Put, Cập nhật dữ liệu của đối tượng trên cơ sở dữ liệu
        /// </summary>
        /// <param name="entityId">id của đối tượng cần sửa</param>
        /// <returns>Số bản ghi bị ảnh hưởng. Nếu lỗi trả về thông tin lỗi</returns>
        /// @Author nmquang 19-12-2021
        /// 

        [HttpPut("{entityId}")]
        public IActionResult Put(Guid entityId, [FromBody] TEntity entity)
        {
            try
            {
                var res = _baseService.Update(entity, entityId);
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
        /// Hàm trả về thông tin khi gặp Exception
        /// </summary>
        /// <param name="ex">Exception</param>
        /// <returns>Các thông tin, và cách giải quyết cho Người dùng và Dev</returns>
        /// @Author nmquang 19-12-2021
        /// 




        protected ObjectResult HandleException(Exception ex)
        {
            var serviceResult = new ServiceResult
            {
                Data = new
                {
                    devMsg = ex.Message,
                    cusMsg = "Có lỗi xảy ra vui lòng liên hệ Misa"
                },
                Messeger = "Có lỗi xảy ra vui lòng liên hệ Misa",
                MisaCode = MISAEnum.Exception
            };

            return StatusCode(500, serviceResult);
            }
        }
    }
