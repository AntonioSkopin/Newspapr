using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using server.Entities;
using server.Models.Auth;

namespace server.Services.Auth
{
    public interface IAuthService
    {
        Task<User> GetUser(Guid user_gd);
        Task<User> Authenticate(AuthenticateModel model);
        Task Register(User user, string password);
        Task ActivateAccount(string pincode);
        Task<bool> CheckIfAccountIsActivated(Guid gd);
    }
    public class AuthService : SqlService, IAuthService
    {

        private readonly IConfiguration _configuration;

        public AuthService(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

        public async Task ActivateAccount(string pincode)
        {
            var getUserQuery =
            @"
                SELECT * FROM Users
                WHERE ActivationPin = @_pin
            ";

            // Store result of query
            var user = await GetQuery<User>(getUserQuery, new
            {
                _pin = pincode
            });

            // Validation
            if (user == null)
                throw new ArgumentException("No user with provided PIN found.");

            // Set user profile to active
            var activateProfileQuery =
            @"
                UPDATE Users
                SET IsActivated = 1,
                    ActivationPIN = NULL
                WHERE Gd = @_gd
            ";

            await PutQuery(activateProfileQuery, new
            {
                _gd = user.Gd
            });
        }

        public async Task<User> Authenticate(AuthenticateModel model)
        {
            // Check if user provided all data
            if (string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password))
                throw new ArgumentException("Please enter both your email and password!");

            var getUserQuery =
            @"
                SELECT * FROM Users
                Where Email = @_email
            ";

            // Execute query and store found user
            var user = await GetQuery<User>(getUserQuery, new
            {
                _email = model.Email
            });

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

        public async Task Register(User user, string password)
        {
            // Validation
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Please enter a password.");

            var getUserQuery =
            @"
                SELECT * FROM Users
                WHERE Email = @_email
            ";

            // Execute query and store results
            var foundUser = await GetQuery<User>(getUserQuery, new
            {
                _email = user.Email
            });

            // Check if email is already taken
            if (foundUser != null)
                throw new Exception("Email is already taken.");

            // Get the password hash
            byte[] passwordHash, passwordSalt;
            PasswordService.CreatePasswordHash(password, out passwordHash, out passwordSalt);

            var insertUserQuery =
            @"
                INSERT INTO Users
                VALUES(NEWID(), @_fullname, @_email, @_hash, @_salt, @_isActivated, @_pin)
            ";

            // Execute query and store new user
            var createdUser = await PostQuery<User>(insertUserQuery, new
            {
                _fullname = user.Fullname,
                _email = user.Email,
                _hash = passwordHash,
                _salt = passwordSalt,
                _isActivated = false,
                _pin = GeneratePin()
            });
        }

        public async Task<bool> CheckIfAccountIsActivated(Guid gd)
        {
            // Search for user
            var getUserQuery =
            @"
                SELECT * FROM Users
                WHERE Gd = @_gd
            ";

            var user = await GetQuery<User>(getUserQuery, new
            {
                _gd = gd
            });

            // Validation
            if (user == null)
                throw new ArgumentException("No user found with provided ID");

            if (!user.IsActivated)
                return false;

            return true;
        }

        // Generates a 4 digit PIN code
        private static string GeneratePin()
        {
            Random random = new Random();
            return random.Next(0, 9999).ToString("D4");
        }
    }
}