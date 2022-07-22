using Luftborn.TechnicalTest.Authentication.JwtBearer;
using Luftborn.TechnicalTest.Authorization;
using Luftborn.TechnicalTest.Authorization.Accounts.Dtos;
using Luftborn.TechnicalTest.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Luftborn.TechnicalTest.Host.Controllers
{
    [Route("api/[controller]/[action]")]
    public class AccountController : TechnicalTestControllerBase
    {
        private readonly AuthManager _authManager;

        public AccountController(AuthManager authManager)
        {
            _authManager = authManager;
        }

        [HttpPost]
        public async Task<RegisterOutput> Register([FromBody]RegisterInput input)
        {
            var result = await _authManager.Register(input);
            return result;
        }
    }
}
