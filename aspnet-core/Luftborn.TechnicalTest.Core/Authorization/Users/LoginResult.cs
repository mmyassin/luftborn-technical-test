using System.Security.Claims;


namespace Luftborn.TechnicalTest.Authorization.Users
{
    public class LoginResult
    {
        public LoginResultType Result { get; private set; }
        public User User { get; private set; }

        public ClaimsIdentity Identity { get; private set; }

        public LoginResult(LoginResultType result, User user = null)
        {
            Result = result;
            User = user;
        }

        public LoginResult(User user, ClaimsIdentity identity)
            : this(LoginResultType.Success)
        {
            User = user;
            Identity = identity;
        }
    }
}