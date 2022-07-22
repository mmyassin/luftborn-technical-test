using System;

namespace Luftborn.TechnicalTest.Models.TokenAuth
{
    public class AuthenticateResultModel
    {
        public string AccessToken { get; set; }

        public int ExpireInSeconds { get; set; }

        public Guid UserId { get; set; }
    }
}
