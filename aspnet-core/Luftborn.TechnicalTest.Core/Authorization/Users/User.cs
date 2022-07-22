using Luftborn.TechnicalTest.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Luftborn.TechnicalTest.Authorization.Users
{
    public class User : Entity<Guid>
    {

        public const int MaxUserNameLength = 256;
        public const int MaxEmailAddressLength = 256;
        public const int MaxNameLength = 64;
        public const int MaxSurnameLength = 64;
        public const int MaxAuthenticationSourceLength = 64;
        public const string AdminUserName = "admin";
        public const int MaxPasswordLength = 128;
        public const int MaxPlainPasswordLength = 32;
        public const int MaxEmailConfirmationCodeLength = 328;
        public const int MaxPasswordResetCodeLength = 328;
        public const int MaxPhoneNumberLength = 32;
        public const int MaxSecurityStampLength = 128;

        [Required]
        [StringLength(MaxUserNameLength)]
        public virtual string UserName { get; set; }
        [Required]
        [StringLength(MaxEmailAddressLength)]
        public virtual string EmailAddress { get; set; }
        [Required]
        [StringLength(MaxNameLength)]
        public virtual string Name { get; set; }
        [Required]
        [StringLength(MaxSurnameLength)]
        public virtual string Surname { get; set; }
        [NotMapped]
        public virtual string FullName { get { return this.Name + " " + this.Surname; } }
        [Required]
        [StringLength(MaxPasswordLength)]
        public virtual byte[] PasswordHash { get; set; }
        public virtual byte[] PasswordSalt { get; set; }
        [StringLength(MaxPasswordResetCodeLength)]
        public virtual string PasswordResetCode { get; set; }

        [StringLength(MaxPhoneNumberLength)]
        public virtual string PhoneNumber { get; set; }
        public virtual bool IsActive { get; set; }

        public User()
        {
            Id = Guid.NewGuid();
        }
        public override string ToString()
        {
            return $"[User {Id}] {UserName}";
        }
    }
}
