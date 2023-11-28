using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarRentalManagement.Server.Data;
using CarRentalManagement.Shared.Domain;
using CarRentalManagement.Server.IRepository;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CarRentalManagement.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        //private readonly ApplicationDbContext _context;
        private readonly IUnitOfWork _unitOfWork;

        //public VehiclesController(ApplicationDbContext context)
        public VehiclesController(IUnitOfWork unitOfWork)

        {
            //_context = context;
            _unitOfWork = unitOfWork;
        }

        // GET: api/Vehicles
        [HttpGet]
        //public async Task<ActionResult<IEnumerable<Vehicles>>> GetVehicles()
        public async Task<IActionResult> GetVehicles()
        {
            var Vehicles = await _unitOfWork.Vehicles.GetAll();
            return Ok(Vehicles);
            //return await _context.Vehicles.ToListAsync();
        }
        // GET: api/Vehicles/5
        [HttpGet("{id}")]
        //public async Task<ActionResult<Vehicles>> GetVehicles(int id)
        public async Task<IActionResult> GetVehicles(int id)
        {
            var Vehicles = await _unitOfWork.Vehicles.Get(q => q.Id == id);

            if (Vehicles == null)
            {
                return NotFound();
            }

            return Ok(Vehicles);
        }

        // PUT: api/Vehicles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVehicles(int id, Vehicle Vehicles)

        {
            if (id != Vehicles.Id)
            {
                return BadRequest();
            }

            //_context.Entry(Vehicles).State = EntityState.Modified;
            _unitOfWork.Vehicles.Update(Vehicles);

            try
            {
                await _unitOfWork.Save(HttpContext);
            }
            catch (DbUpdateConcurrencyException)
            {
                //if (!VehiclesExists(id))
                if (!await VehiclesExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Vehicles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Vehicle>> PostVehicles(Vehicle Vehicles)
        {
            await _unitOfWork.Vehicles.Insert(Vehicles);
            await _unitOfWork.Save(HttpContext);
            return CreatedAtAction("Get Vehicles", new { id = Vehicles.Id }, Vehicles);
        }

        // DELETE: api/Vehicles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicles(int id)
        {

            var Vehicles = await _unitOfWork.Vehicles.Get(q => q.Id == id);
            if (Vehicles == null)
            {
                return NotFound();
            }

            // _context.Vehicles.Remove(Vehicles);
            // await _context.SaveChangesAsync();
            await _unitOfWork.Vehicles.Delete(id);
            await _unitOfWork.Save(HttpContext);

            return NoContent();
        }

        private async Task<bool> VehiclesExists(int id)
        {
            //return (_context.Vehicles?.Any(e => e.Id == id)).GetValueOrDefault();
            var Vehicles = await _unitOfWork.Vehicles.Get(q => q.Id == id);
            return Vehicles != null;
        }
    }
}
