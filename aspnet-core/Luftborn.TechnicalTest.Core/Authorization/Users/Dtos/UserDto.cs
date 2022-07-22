namespace Luftborn.TechnicalTest.Authorization.Users.Dtos
{
    public class UserDto
    {
        public virtual string UserName { get; set; }
        public virtual string EmailAddress { get; set; }
        public virtual string PhoneNumber { get; set; }
        public virtual string Name { get; set; }
        public virtual string Surname { get; set; }
        public virtual string Password { get; set; }
    }
}
