using desafio_api_cadastro.Dto;
using desafio_api_cadastro.Models;
using desafio_api_cadastro.Repository;
using desafio_api_cadastro.Services;
using desafio_api_cadastro.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace desafio_api_cadastro.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class LoginController : ControllerBase
    {
        private readonly ILogger<LoginController> _logger;
        private readonly IuserRepository _iuserRepository;

        public LoginController(ILogger<LoginController> logger, IuserRepository iuserRepository)
        {
            _logger = logger;
            _iuserRepository = iuserRepository;
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult doLogin([FromBody] LoginRequestDto loginRequest)
        {

            try
            {
                if(!String.IsNullOrEmpty(loginRequest.password) && !String.IsNullOrEmpty(loginRequest.email) &&
                    !String.IsNullOrWhiteSpace(loginRequest.password) && !String.IsNullOrWhiteSpace(loginRequest.email))
                {
                    User user = _iuserRepository.GetUsuarioPorLoginSenha(loginRequest.email.ToLower(), MD5Utils.GenerateHashMD5(loginRequest.password));

                    if(user != null)
                    {
                        return Ok(new loginReturn()
                        {
                            email = user.email,
                            name = user.name,
                            token = TokenServices.CreateToken(user)
                        });
                    }
                    else
                    {
                        return BadRequest(new ErrorResponseDto()
                        {
                            Description = "email or password is not valid",
                            Status = StatusCodes.Status400BadRequest
                        });
                    }    
                }
                else
                {
                    return BadRequest(new ErrorResponseDto()
                    {
                        Description = "user did not fill in the fields correctly",
                        Status = StatusCodes.Status400BadRequest
                    });
                }
            }
            catch(Exception e)
            {
                _logger.LogError("Authentication failed:" + e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponseDto()
                {
                    Description = "Authentication error occurred",
                    Status = StatusCodes.Status500InternalServerError
                });
            }
        }

    }
}
