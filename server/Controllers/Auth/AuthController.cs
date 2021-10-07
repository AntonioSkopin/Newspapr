using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using server.Entities;
using server.Helpers;
using server.Models;
using server.Models.Auth;
using server.Services;
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
                return Unauthorized(new { Message = "Please enter valid credentials." });

            // Check if account is activated, if so give the user the token, if not give feedback msg
            return await _authService.CheckIfAccountIsActivated(user.Email) ? 
                            Ok(JWTService.CreateToken(user, _appSettings)) 
                            : 
                            Unauthorized(new { Message = "Please activate your account. Instructions can be found in your mail: " + user.Email });
        }

        [HttpPost]
        public async Task<ActionResult<Object>> Register([FromBody] RegisterModel model)
        {
            // Map model to entity
            var user = _mapper.Map<User>(model);

            // Create user
            var createdUser = await _authService.Register(user, model.Password);

            if (createdUser is ExceptionModel)
            {
                return BadRequest(new { Message = createdUser.ExceptionMessage });
            }
            
            // Send the user a confirmation email
            var mailService = new MailService(_configuration);
            mailService.SendRegisterConfirmationMail(createdUser.Email, createdUser.ActivationPin);
            
            return Ok(new { Message = "Account is created successfully. You can login now." });
        }

        [HttpPost]
        public async Task<ActionResult> ActivateAccount([FromBody] ActivateModel model)
        {
            var user = await _authService.ActivateAccount(model);

            return user != null ? 
                    Ok(new { Message = "Your account is successfully verified. You can login now" }) 
                    :
                    BadRequest(new { Message = "Invalid Pincode entered, please try again." });
        }
    }
}