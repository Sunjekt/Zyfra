using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZyfraServer.Models;
using ZyfraServer.Intefaces.Services;
using ZyfraServer.Servieces;

namespace ZyfraServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ZyfraDataController : ControllerBase
    {
        private readonly IZyfraDataService zyfraDataService;

        public ZyfraDataController(IZyfraDataService _zyfraDataService)
        {
            zyfraDataService = _zyfraDataService;
        }

        // GET: api/ZyfraData
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ZyfraData>>> GetZyfraData()
        {
            var zyfraData = zyfraDataService.GetZyfraData();
            return Ok(zyfraData);
        }

        // GET: api/ZyfraData/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ZyfraData>> GetZyfraData(int id)
        {
            var zyfraData = zyfraDataService.GetZyfraDataById(id);
            if (zyfraData == null)
            {
                return NotFound();
            }
            return Ok(zyfraData);
        }

        // PUT: api/ZyfraData/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutZyfraData(int id, ZyfraData zyfraData)
        {
            if (id != zyfraData.Id)
            {
                return BadRequest();
            }
            string errorMessage;

            if (!zyfraDataService.Update(id, zyfraData, out errorMessage))
            {
                return BadRequest(errorMessage);
            }

            return NoContent();
        }

        // POST: api/ZyfraData
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ZyfraData>> PostZyfraData(ZyfraData zyfraData)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var data = zyfraDataService.Add(zyfraData);

            return CreatedAtAction("GetZyfraData", new { id = data.Id }, data);
        }

        // DELETE: api/ZyfraData/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteZyfraData(int id)
        {
            zyfraDataService.Delete(id);
            return NoContent();
        }
    }
}
