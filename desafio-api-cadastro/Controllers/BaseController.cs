using desafio_api_cadastro.Models;
using desafio_api_cadastro.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace desafio_api_cadastro.Controllers
{
    [Authorize]
    public class BaseController : ControllerBase
    {
        
        protected readonly IuserRepository _iuserRepository;

        public BaseController(IuserRepository iuserRepository)
        {
            
            _iuserRepository = iuserRepository;
        }

        protected User ReadToken()
        {
            var idUser = User.Claims.Where(c => c.Type == ClaimTypes.Sid).Select(c => c.Value).FirstOrDefault();

            if (string.IsNullOrEmpty(idUser))
            {
                return null;
            }
            else
            {
                return _iuserRepository.GetUserById(int.Parse(idUser));
            }
        }
    }
}
