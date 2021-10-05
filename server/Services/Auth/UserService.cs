using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using server.Entities;
using server.Services.Auth;

namespace server.Services
{
    public interface IUserService
    {
        Task<User> GetUser(string email);
        Task InsertUser(User user, string password);
        Task UpdateUser(User user);
        Task DeleteUser(Guid user_gd);
    }

    public class UserService : SqlService, IUserService
    {
        private readonly IConfiguration _configuration;

        public UserService(IConfiguration configuration) : base(configuration)
        {
            _configuration = configuration;
        }

        public async Task DeleteUser(Guid user_gd)
        {
            var deleteUserQuery =
            @"
                DELETE FROM Users
                WHERE Gd = @_gd
            ";

            await DeleteQuery(deleteUserQuery, new
            {
                _gd = user_gd
            });
        }

        public async Task<User> GetUser(string email)
        {
            var getUserQuery =
            @"
                SELECT * FROM Users
                WHERE Email = @_email
            ";

            return await GetQuery<User>(getUserQuery, new
            {
                _email = email
            });
        }

        public async Task InsertUser(User user, string password)
        {
            var insertUserQuery =
            @"
                INSERT INTO Users
                VALUES(NEWID(), @_fullname, @_email, @_hash, @_salt, @_isActivated, @_pin)
            ";

            // Get the password hash
            byte[] passwordHash, passwordSalt;
            PasswordService.CreatePasswordHash(password, out passwordHash, out passwordSalt);

            await PostQuery(insertUserQuery, new
            {
                _fullname = user.Fullname,
                _email = user.Email,
                _hash = passwordHash,
                _salt = passwordSalt,
                _isActivated = false,
                _pin = GeneratePin()
            });
        }

        public async Task UpdateUser(User user)
        {
            var updateUserQuery =
            @"
                UPDATE Users
                SET Fullname = @_fullname,
                    Email = @_email,
                    PasswordHash = @_hash,
                    PasswordSalt = @_salt,
                    IsActivated = @_is_activated,
                    ActivationPin = @_pin
                WHERE Gd = @_gd
            ";

            await PutQuery(updateUserQuery, new
            {
                _fullname = user.Fullname,
                _email = user.Email,
                _hash = user.PasswordHash,
                _salt = user.PasswordSalt,
                _is_activated = user.IsActivated,
                _pin = user.ActivationPin,
                _gd = user.Gd
            });
        }

        private static string GeneratePin()
        {
            Random random = new Random();
            return random.Next(0, 9999).ToString("D4");
        }
    }
}