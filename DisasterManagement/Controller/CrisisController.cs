using Microsoft.AspNetCore.Mvc;
using DisasterManagement.Data;
using DisasterManagement.Dtos;
using DisasterManagement.Models;
using Microsoft.EntityFrameworkCore;

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
    public async Task<ActionResult<IEnumerable<CrisisDto>>> GetCrises()
    {
        var crises = await _context.Crises
            .Select(c => new CrisisDto 
            {
                Id = c.Id,
                Title = c.Title,
                Description = c.Description,
                Location = c.Location,
                Severity = c.Severity,
                IsApproved = c.IsApproved,
                Date = c.Date
            }).ToListAsync();

        return Ok(crises);
    }

    // Get a single crisis by id (Response: 200 OK or 404 Not Found)
    [HttpGet("{id}")]
    public async Task<ActionResult<CrisisDto>> GetCrisis(int id)
    {
        var crisis = await _context.Crises.FindAsync(id);

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
    public async Task<ActionResult<CrisisDto>> AddCrisis([FromBody] CrisisCreateDto crisisCreateDto)
    {
        var crisis = new Crisis
        {
            Title = crisisCreateDto.Title,
            Description = crisisCreateDto.Description,
            Location = crisisCreateDto.Location,
            Severity = crisisCreateDto.Severity,
            Help = crisisCreateDto.Help,
            IsApproved = false // Set as false by default, awaiting admin approval
        };

        _context.Crises.Add(crisis);
        await _context.SaveChangesAsync();

        var crisisDto = new CrisisDto
        {
            Id = crisis.Id,
            Title = crisis.Title,
            Description = crisis.Description,
            Location = crisis.Location,
            Severity = crisis.Severity,
            Help = crisis.Help,
            IsApproved = crisis.IsApproved
        };

        return CreatedAtAction(nameof(GetCrisis), new { id = crisis.Id }, crisisDto);
    }

    // Update a crisis (Response: 204 No Content or 404 Not Found)
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCrisis(int id, CrisisPutDto crisisUpdateDto)
    {
        var crisis = await _context.Crises.FindAsync(id);

        if (crisis == null)
            return NotFound("Crisis not found");

        crisis.Severity= crisisUpdateDto.Severity;
        crisis.IsApproved = crisisUpdateDto.IsApproved;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // Delete a crisis (Response: 204 No Content or 404 Not Found)
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCrisis(int id)
    {
        var crisis = await _context.Crises.FindAsync(id);

        if (crisis == null)
            return NotFound("Crisis not found");

        _context.Crises.Remove(crisis);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    // New endpoint to get all volunteers assigned to a crisis
    [HttpGet("{id}/volunteers")]
    public async Task<ActionResult<IEnumerable<VolunteerDto>>> GetVolunteersInCrisis(int id)
    {
        var volunteersInCrisis = await _context.VolunteerCrises
            .Where(vc => vc.CrisisId == id)
            .Select(vc => new VolunteerDto
            {
                Id = vc.Volunteer.Id,
                FullName = vc.Volunteer.FullName,
                Age = vc.Volunteer.Age,
                PhoneNumber = vc.Volunteer.PhoneNumber,
                AssignedTask = vc.Volunteer.AssignedTask
            }).ToListAsync();

        return Ok(volunteersInCrisis);
    }
}
