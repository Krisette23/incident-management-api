using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using incidentmanagement.Models;
using incidentmanagement.Database;
using incidentmanagement.DTOs;

namespace incidentmanagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncidentController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public IncidentController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _context.Incidents.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var incident = await _context.Incidents.FindAsync(id);
            if (incident == null)
            {
                return NotFound();
            }
            return Ok(incident);
        }

        [HttpPut("{id}")]  
        public async Task<IActionResult> Update(int id, Incident incident)
        {
            if (id != incident.Id)
            {
                return BadRequest();
            }
            _context.Entry(incident).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IncidentExists(id))
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

        private bool IncidentExists(int id)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateIncidentDTO dTO)
        {

            var incident = new Incident
            {
                Title = dTO.Title,
                Description = dTO.Description,
                Priority = dTO.Priority
            };

            _context.Incidents.Add(incident);
            await _context.SaveChangesAsync();
            //return CreatedAtAction(nameof(GetById), new { id = incident.Id }, incident);

            var incidentResponse = new
            {
                incident.Id,
                incident.Title,
                incident.Description,
                incident.Priority,
                incident.Status,
                incident.CreatedAt,
                incident.CreatedByUserId,
                incident.AssignedToUserId
            };

            return CreatedAtAction(nameof(GetById), new { id = incident.Id }, incidentResponse);
        }


        [HttpDelete("{id}")]   
        public async Task<IActionResult> Delete(int id)
        {
            var incident = await _context.Incidents.FindAsync(id);
            if (incident == null)
            {
                return NotFound();
            }
            _context.Incidents.Remove(incident);
            await _context.SaveChangesAsync();
            return NoContent();
        }

    }
}
