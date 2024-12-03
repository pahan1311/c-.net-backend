using backend.Data;
using backend.DTOs.User;
using backend.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace backend.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly ApplicationDBContext dbContext;

        public UserController(ApplicationDBContext dbContext)
        {
            this.dbContext = dbContext;
        }

        

        [HttpGet("all")]
        public IActionResult GetAllUser()
        {
            var allUser = dbContext.Users.ToList();
            return Ok(allUser);
        }

        [HttpPost("create")]
        public IActionResult CreateUser(CreateUserDTO dto){
            var user = new User(){
                //Name = dto.Name,
                Email = dto.Email,
                ConfirmPassword = dto.ConfirmPassword,
                Username = dto.Username,
                Password = dto.Password,
                
                

            };

             dbContext.Users.Add(user);
             dbContext.SaveChanges();

             return Ok(user);
        }

          [HttpPost("login")]
       public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
{
    var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Email == loginDTO.Email);

    if (user == null || user.Password != loginDTO.Password) // Basic comparison
    {
        return Unauthorized(new { message = "Invalid credentials" });
    }

    return Ok(new
    {
        message = "Login successful",
        user = new
        {
            id = user.Id,
            email = user.Email,
            username = user.Username
        }


    });
}
     [HttpPost("signUp")]
        public async Task<IActionResult> Register([FromBody] RegisterDTO registerDTO)
        {
             Console.WriteLine("SignUpfunction");
            Console.WriteLine(registerDTO) ;
            // Check if email already exists
            var existingUser = await dbContext.Users.FirstOrDefaultAsync(u => u.Email == registerDTO.Email);
            if (existingUser != null)
            {
                return Conflict(new { message = "Email is already in use" });
            }

            // Check if passwords match
            if (registerDTO.Password != registerDTO.ConfirmPassword)
            {
                return BadRequest(new { message = "Passwords do not match" });
            }

            // Create new user entity
            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = registerDTO.Username,
                Email = registerDTO.Email,
                Password = registerDTO.Password ,
                ConfirmPassword = registerDTO.ConfirmPassword,
                
                
            };

            // Add user to the database
            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync();

            return Ok(new { message = "Registration successful" });
        }
    }
}
    

