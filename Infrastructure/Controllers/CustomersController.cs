using Domain.Interface.Services;
using Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace Infrastructure.Controllers
{
    [Route("api/[controller]")]
    public class CustomersController : BaseController
    {
        private readonly ICustomersService _service;
        public CustomersController(ICustomersService service)
        {
            _service = service;
        }

        [HttpGet("GetRecords")]
        public async Task<IActionResult> GetRecords(string? keyword, int page, int pageSize)
        {
            try
            {
                var data = await _service.GetRecords(keyword, page, pageSize);
                return await Success(data);
            }
            catch (Exception ex)
            {
                return await Error(ex);
            }
        }

        [HttpGet("GetRecord")]
        public async Task<IActionResult> GetRecord(int id)
        {
            try
            {
                var data = await _service.GetRecord(id);
                return await Success(data);
            }
            catch (Exception ex)
            {
                return await Error(ex) ;
            }
        }
        [HttpPost("Insert")]
        public async Task<IActionResult> Insert([FromBody]Customers entity)
        {
            try
            {
                var data = await _service.Insert(entity);
                return await Success(data);
            }
            catch (Exception ex)
            {
                return await Error(ex);
            }
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody]Customers entity)
        {
            try
            {
                var result = await _service.Update(entity);
                return await Success(result);
            }
            catch (Exception ex)
            {
                return await Error(ex);
            }
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(List<int> entities)
        {
            try
            {
                var result = await _service.Delete(entities);
                return await Success(result);
            }
            catch (Exception ex)
            {
                return await Error(ex);
            }
        }

    }
}
