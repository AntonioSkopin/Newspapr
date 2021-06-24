using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using server.Entities;
using server.Helpers;
using server.Models.Auth;
using server.Services.Auth;

namespace server.Controllers.Auth
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        public IAuthService _authService;
        private IMapper _mapper;
        private readonly AppSettings _appSettings;
        private IConfiguration _configuration;

        public AuthController(IAuthService authService, IMapper mapper, IOptions<AppSettings> appSettings, IConfiguration configuration)
        {
            _authService = authService;
            _mapper = mapper;
            _appSettings = appSettings.Value;
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Authenticate([FromBody] AuthenticateModel model)
        {
            var user = await _authService.Authenticate(model);

            if (user == null)
                return Unauthorized("Please enter valid credentials.");

            // Check if account is activated, if so give the user the token, if not give feedback msg
            return await _authService.CheckIfAccountIsActivated(user.Gd) ? Ok(JWTService.CreateToken(user, _appSettings)) : Unauthorized("Please activate your account.");
        }

        [HttpPost]
        public async Task<ActionResult> Register([FromBody] RegisterModel model)
        {
            // Map model to entity
            var user = _mapper.Map<User>(model);

            // Create user
            await _authService.Register(user, model.Password);

            // Send the user a confirmation email

            return Ok("Account is created successfully.");
        }

        [HttpPost]
        public async Task ActivateAccount([FromBody] string pincode)
        {
            await _authService.ActivateAccount(pincode);
        }
    }
}