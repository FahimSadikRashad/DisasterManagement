using Microsoft.AspNetCore.Mvc;
using DisasterManagement.Data;
using DisasterManagement.Dtos;
using DisasterManagement.Models;
using System.Linq;

[ApiController]
[Route("api/[controller]")]
public class CrisisController : ControllerBase
{
    private readonly DisasterContext _context;

    public CrisisController(DisasterContext context)
    {
        _context = context;
    }

    // Get all crises (Response: 200 OK)
    [HttpGet]
    public ActionResult<IEnumerable<CrisisDto>> GetCrises()
    {
        var crises = _context.Crises
            .Select(c => new CrisisDto 
            {
                Id = c.Id,
                Title = c.Title,
                Description = c.Description,
                Location = c.Location,
                Severity = c.Severity,
                IsApproved = c.IsApproved
            }).ToList();

        return Ok(crises);
    }

    // Get a single crisis by id (Response: 200 OK or 404 Not Found)
    [HttpGet("{id}")]
    public ActionResult<CrisisDto> GetCrisis(int id)
    {
        var crisis = _context.Crises.FirstOrDefault(c => c.Id == id);
        
        if (crisis == null)
            return NotFound("Crisis not found");

        var crisisDto = new CrisisDto
        {
            Id = crisis.Id,
            Title = crisis.Title,
            Description = crisis.Description,
            Location = crisis.Location,
            Severity = crisis.Severity,
            IsApproved = crisis.IsApproved
        };

        return Ok(crisisDto);
    }

    // Create a new crisis (Response: 201 Created or 400 Bad Request)
    [HttpPost]
    public ActionResult<CrisisDto> AddCrisis(CrisisCreateDto crisisCreateDto)
    {
        var crisis = new Crisis
        {
            Title = crisisCreateDto.Title,
            Description = crisisCreateDto.Description,
            Location = crisisCreateDto.Location,
            Severity = crisisCreateDto.Severity,
            IsApproved = false // Admin will approve later
        };

        _context.Crises.Add(crisis);
        _context.SaveChanges();

        var crisisDto = new CrisisDto
        {
            Id = crisis.Id,
            Title = crisis.Title,
            Description = crisis.Description,
            Location = crisis.Location,
            Severity = crisis.Severity,
            IsApproved = crisis.IsApproved
        };

        return CreatedAtAction(nameof(GetCrisis), new { id = crisis.Id }, crisisDto);
    }

    // Update a crisis (Response: 204 No Content or 404 Not Found)
    [HttpPut("{id}")]
    public IActionResult UpdateCrisis(int id, CrisisCreateDto crisisUpdateDto)
    {
        var crisis = _context.Crises.FirstOrDefault(c => c.Id == id);

        if (crisis == null)
            return NotFound("Crisis not found");

        crisis.Title = crisisUpdateDto.Title;
        crisis.Description = crisisUpdateDto.Description;
        crisis.Location = crisisUpdateDto.Location;
        crisis.Severity = crisisUpdateDto.Severity;

        _context.SaveChanges();

        return NoContent();
    }

    // Delete a crisis (Response: 204 No Content or 404 Not Found)
    [HttpDelete("{id}")]
    public IActionResult DeleteCrisis(int id)
    {
        var crisis = _context.Crises.FirstOrDefault(c => c.Id == id);

        if (crisis == null)
            return NotFound("Crisis not found");

        _context.Crises.Remove(crisis);
        _context.SaveChanges();

        return NoContent();
    }
}
