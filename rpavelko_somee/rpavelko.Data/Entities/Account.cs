using System;

namespace rpavelko.Data.Entities
{
    public class Account
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public Guid? ConfirmationCode { get; set; }
        public string PwdHash { get; set; }
        public string PwdSalt { get; set; }
    }
}
