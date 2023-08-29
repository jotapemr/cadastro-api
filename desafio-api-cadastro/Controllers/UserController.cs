using desafio_api_cadastro.Dto;
using desafio_api_cadastro.Models;
using desafio_api_cadastro.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace desafio_api_cadastro.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : BaseController
    {
        public readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger, IuserRepository iuserRepository) : base(iuserRepository)
        {
            _logger = logger;
        }

        [HttpGet]
        [Authorize]
        public IActionResult getUser()
        {
            try
            {
                User user = ReadToken();
                return Ok(new UserResponseDTO{
                    Name = user.name
                });
            }
            catch(Exception e)
            {
                _logger.LogError(" An error ocurred while trying to get user");
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDto()
                {
                    Description = "This error as ocurred: " + e.Message,
                    Status = StatusCodes.Status500InternalServerError
                });
            } 
        }

        [HttpPost]
        [AllowAnonymous]          
        public IActionResult saveUser([FromBody] User user)
        {
            try
            {

                if (user != null)
                {
                    var errors = new List<string>();

                    if (string.IsNullOrEmpty(user.name) || string.IsNullOrWhiteSpace(user.name))
                    {
                        errors.Add("Name invalid");
                    }
                    if (string.IsNullOrEmpty(user.email) || string.IsNullOrWhiteSpace(user.email) || !user.email.Contains("@"))
                    {
                        errors.Add("Email invalid");
                    }
                    if (string.IsNullOrEmpty(user.password) || string.IsNullOrWhiteSpace(user.password))
                    {
                        errors.Add("Password invalid");
                    }

                    if (errors.Count > 0)
                    {
                        return BadRequest(new ErrorResponseDto()
                        {
                            Status = StatusCodes.Status400BadRequest,
                            Errors = errors
                        });
                    }

                   
                    user.password = Utils.MD5Utils.GenerateHashMD5(user.password);
                    user.email = user.email.ToLower();

                    if (!_iuserRepository.verificateEmail(user.email))
                    {
                        _iuserRepository.Save(user);
                    }
                    else
                    {

                        return BadRequest(new ErrorResponseDto()
                        {
                            Status = StatusCodes.Status400BadRequest,
                            Description = "This email already exists"
                        });
                    }
                }
                return Ok("User was successfully saved");
                
               
            }
            catch(Exception e)
            {
                _logger.LogError(" An error ocurred while trying to save user");
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDto()
                {
                    Description = "This error as ocurred: " + e.Message,
                    Status = StatusCodes.Status500InternalServerError
                });
            }
        }
        
    }
}
