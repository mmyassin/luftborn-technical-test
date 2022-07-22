using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Luftborn.TechnicalTest.Authorization.Accounts.Dtos;
using Luftborn.TechnicalTest.Authorization.Users;
using Luftborn.TechnicalTest.Authorization.Users.Dtos;
using Luftborn.TechnicalTest.Domain.Repositories;

namespace Luftborn.TechnicalTest.Authorization
{
    public class AuthManager
    {
        private readonly IRepository<User, Guid> _userRepository;
        public AuthManager(IRepository<User,Guid> userRepository)
        {
            _userRepository = userRepository;
        }

        public virtual async Task<LoginResult> LoginAsync(string userNameOrEmailAddress,string plainPassword)
        {
            var result = await LoginAsyncInternal(userNameOrEmailAddress,plainPassword);

            return result;
        }

        protected virtual async Task<LoginResult> LoginAsyncInternal(string userNameOrEmailAddress, string plainPassword)
        {
            if (string.IsNullOrWhiteSpace(userNameOrEmailAddress))
            {
                throw new ArgumentNullException(nameof(userNameOrEmailAddress));
            }

            if (string.IsNullOrWhiteSpace(plainPassword))
            {
                throw new ArgumentNullException(nameof(plainPassword));
            }

            var user = await _userRepository
                .FirstOrDefaultAsync(u => u.UserName == userNameOrEmailAddress || u.EmailAddress == userNameOrEmailAddress);

            if (user == null)
                return new LoginResult(LoginResultType.InvalidUserNameOrEmailAddress);

            if (!VerifyPasswordHash(plainPassword, user.PasswordHash, user.PasswordSalt))
                return new LoginResult(LoginResultType.InvalidPassword);

            return await CreateLoginResultAsync(user);
        }

        public async Task<RegisterOutput> Register(RegisterInput user)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(user.Password, out passwordHash, out passwordSalt);

            var userOutput = await _userRepository.InsertAsync(new User
            {
                UserName = user.UserName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                EmailAddress = user.EmailAddress,
                Name = user.Name,
                Surname = user.Surname,
                IsActive = true,
            });

            return new RegisterOutput() { CanLogin = true };
        }


        protected virtual async Task<LoginResult> CreateLoginResultAsync(User user)
        {
            if (!user.IsActive)
            {
                return new LoginResult(LoginResultType.UserIsNotActive);
            }

            return new LoginResult(user, new ClaimsIdentity(new List<Claim> 
            {  
                new Claim(ClaimTypes.NameIdentifier, user.Name),
                new Claim(ClaimTypes.Email, user.EmailAddress)
            }));
        }


        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != passwordHash[i]) return false;
                }
                return true;
            }
        }
        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}
