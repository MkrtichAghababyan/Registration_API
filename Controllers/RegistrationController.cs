using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Registration.Models;
using Registration.Services;
using Serilog;

namespace Registration.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegistrationController : ControllerBase
    {
        private readonly IRegisterService _registerservice;
        private readonly ILogger<RegisterService> _logger;
        public RegistrationController(IRegisterService registerservice, ILogger<RegisterService> logger)
        {
            _registerservice = registerservice;
            _logger = logger;
        }
        [HttpPost("Post")]
        public async Task<ActionResult<User>> Register([FromBody]User user)
        {
            //role:null urish dzev chi ashxatum
            _logger.LogInformation("|Log ||Testing");
            var result = _registerservice.Register(user);
            if(result == null)
            {
                return NotFound("Password Missmatch");
            }
            return Ok(result);
        }
        [HttpGet("Signin{email},{password}")]
        public async Task<ActionResult<IEnumerable<User>>> Singin(string email, string password, string? promocode)
        {
            _logger.LogInformation("Logs For Signin");
            var result = _registerservice.Singin(email, password, promocode);
            if (result == null)
            {
                return NotFound("You Are Not Registered Please Register");
            }
            return Ok(result);
        }
        [HttpPut("Update{email},{password}")]
        public async Task<ActionResult<IEnumerable<User>>> UpdateUser(string email, string password,User user)
        {
            _logger.LogInformation("Logs For UpdateUser");
            var result  = _registerservice.UpdateUser(email, password, user);
            if(result == null)
            {
                return NotFound("You Have To Be Registered For Updating Your Acount");
            }
            return Ok(result);
        }
        [HttpDelete("Delete{email},{password}")]
        public async Task<ActionResult<string>> DeleteUser(string email,string password)
        {
            _logger.LogInformation("Logs For DeleteUser");
            var result = _registerservice.DeleteUser(email, password);
            if (result == null)
            {
                return NotFound("You Have To Be Registered For Updating Your Acount");
            }
            return Ok(result);
        }
        
    }
}
