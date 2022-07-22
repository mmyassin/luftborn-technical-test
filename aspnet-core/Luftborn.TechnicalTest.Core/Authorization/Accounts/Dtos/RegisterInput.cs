using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Luftborn.TechnicalTest.Authorization.Users;

namespace Luftborn.TechnicalTest.Authorization.Accounts.Dtos
{
    public class RegisterInput
    {
        [Required]
        [StringLength(User.MaxNameLength)]
        public string Name { get; set; }

        [Required]
        [StringLength(User.MaxSurnameLength)]
        public string Surname { get; set; }

        [Required]
        [StringLength(User.MaxUserNameLength)]
        public string UserName { get; set; }

        [Required]
        [EmailAddress]
        [StringLength(User.MaxEmailAddressLength)]
        public string EmailAddress { get; set; }

        [Required]
        [StringLength(User.MaxPlainPasswordLength)]
        public string Password { get; set; }
    }
}
