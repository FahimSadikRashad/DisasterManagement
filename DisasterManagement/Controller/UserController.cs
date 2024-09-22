using Microsoft.AspNetCore.Mvc;
using DisasterManagement.Data;
using DisasterManagement.Dtos;
using DisasterManagement.Models;
using Microsoft.EntityFrameworkCore;


[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly DisasterContext _context;

    public UserController(DisasterContext context)
    {
        _context = context;
    }

    // Get all users (Response: 200 OK)
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
    {
        var users = await _context.Users
            .Select(u => new UserDto
            {
                Id = u.Id,
                Username = u.Username,
                FullName = u.FullName,
                PhoneNumber = u.PhoneNumber,
                Role = u.Role
            }).ToListAsync();

        return Ok(users);
    }

    // [EnableCors]
    // User login (Response: 200 OK or 404 Not Found)
    [HttpPost("login")]
    public async Task<ActionResult<LoginResponseDto>> Login([FromBody] LoginDto loginDto)
    {
        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.PhoneNumber == loginDto.Email && u.Password == loginDto.Password && u.Role == loginDto.Role);

        if (user == null)
            return NotFound("User not found");

        return Ok(new LoginResponseDto { Email = user.Username, Role = user.Role });
    }

    // Get a single user by id (Response: 200 OK or 404 Not Found)
    [HttpGet("{id}")]
    public async Task<ActionResult<UserDto>> GetUser(int id)
    {
        var user = await _context.Users.FindAsync(id);

        if (user == null)
            return NotFound("User not found");

        var userDto = new UserDto
        {
            Id = user.Id,
            Username = user.Username,
            FullName = user.FullName,
            PhoneNumber = user.PhoneNumber,
            Role = user.Role
        };

        return Ok(userDto);
    }

    // Create a new user (Response: 201 Created or 400 Bad Request)
    [HttpPost]
    public async Task<ActionResult<UserDto>> CreateUser(UserCreateDto userCreateDto)
    {
        var user = new User
        {
            Username = userCreateDto.Username,
            Password = userCreateDto.Password, // Hashing should be done in production
            FullName = userCreateDto.FullName,
            PhoneNumber = userCreateDto.PhoneNumber,
            Role = userCreateDto.Role
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        var userDto = new UserDto
        {
            Id = user.Id,
            Username = user.Username,
            FullName = user.FullName,
            PhoneNumber = user.PhoneNumber,
            Role = user.Role
        };

        return CreatedAtAction(nameof(GetUser), new { id = user.Id }, userDto);
    }

    // Update a user (Response: 204 No Content or 404 Not Found)
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, UserCreateDto userUpdateDto)
    {
        var user = await _context.Users.FindAsync(id);

        if (user == null)
            return NotFound("User not found");

        user.Username = userUpdateDto.Username;
        user.Password = userUpdateDto.Password; // Hashing should be done in production
        user.FullName = userUpdateDto.FullName;
        user.PhoneNumber = userUpdateDto.PhoneNumber;
        user.Role = userUpdateDto.Role;

        await _context.SaveChangesAsync();

        return NoContent();
    }

    // Delete a user (Response: 204 No Content or 404 Not Found)
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var user = await _context.Users.FindAsync(id);

        if (user == null)
            return NotFound("User not found");

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
