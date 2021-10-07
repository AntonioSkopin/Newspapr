using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using server.Entities;
using server.Models;
using server.Models.Auth;
using server.Services;

namespace server.Services.Auth
{
    public interface IAuthService
    {
        Task<User> GetUser(Guid user_gd);
        Task<User> Authenticate(AuthenticateModel model);
        Task<dynamic> Register(User user, string password);
        Task<User> ActivateAccount(ActivateModel model);
        Task<bool> CheckIfAccountIsActivated(string email);
    }
    public class AuthService : SqlService, IAuthService
    {

        private readonly IConfiguration _configuration;

        public AuthService(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

        public async Task<User> ActivateAccount(ActivateModel model)
        {
            var getUserQuery =
            @"
                SELECT * FROM Users
                WHERE ActivationPin = @_pin
            ";

            // Store result of query
            var user = await GetQuery<User>(getUserQuery, new
            {
                _pin = model.Pincode
            });

            // Validation
            if (user == null)
                return null;

            // Set user profile to active
            var activateProfileQuery =
            @"
                UPDATE Users
                SET IsActivated = 1,
                    ActivationPIN = NULL
                WHERE Gd = @_gd
            ";

            await PutQuery<User>(activateProfileQuery, new
            {
                _gd = user.Gd
            });

            return user;
        }

        public async Task<User> Authenticate(AuthenticateModel model)
        {
            UserService userService = new UserService(_configuration);
            var user = await userService.GetUser(model.Email);

            // Check if user exists
            if (user == null)
                return null;

            // Check if password is correct
            if (!PasswordService.VerifyPasswordHash(model.Password, user.PasswordHash, user.PasswordSalt))
                return null;

            // Authentication successful
            return user;
        }

        public async Task<User> GetUser(Guid user_gd)
        {
            var getUserQuery =
            @"
                SELECT * FROM Users
                WHERE Gd = @_gd
            ";

            return await GetQuery<User>(getUserQuery, new
            {
                _gd = user_gd
            });
        }

        public async Task<dynamic> Register(User user, string password)
        {
            UserService userService = new UserService(_configuration);

            // Check if user Email is already taken
            var foundUser = await userService.GetUser(user.Email);
            var isEmailTaken = CheckIfEmailIsTaken(foundUser);
            if (isEmailTaken != null)
                return isEmailTaken;

            // Create user & return created user
            await userService.InsertUser(user, password);
            var createdUser = await userService.GetUser(user.Email);
            return createdUser;
        }

        public async Task<bool> CheckIfAccountIsActivated(string email)
        {
            UserService userService = new UserService(_configuration);
            var user = await userService.GetUser(email);

            // Validation
            if (user == null)
                throw new ArgumentException("No user found with provided ID");

            if (!user.IsActivated)
                return false;

            return true;
        }

        private static ExceptionModel CheckIfEmailIsTaken(User user)
        {
            // Check if email is already taken
            if (user != null)
            {
                ExceptionModel exception = new ExceptionModel();
                exception.ExceptionType =  "Validation error";
                exception.ExceptionMessage = "Email adress is already taken.";
                return exception;
            }
            return null;
        }
    }
}