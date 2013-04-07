using System;
using System.Web.Configuration;
using System.Web.Security;

namespace rpavelko.Data.Utils
{
    public static class Security
    {
        public const int SaltSize = 20;
        private const string SaltChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";

        public static string GenerateSalt()
        {
            var rdm = new Random();
            var buffer = new char[SaltSize];
            for(var i=0; i<SaltSize; i++)
            {
                buffer[i] = SaltChars[rdm.Next(SaltChars.Length)];
            }
            return new string(buffer);
        }

        public static string HashPassword(string password, string salt)
        {
            var pwd = salt + password;
            return FormsAuthentication.HashPasswordForStoringInConfigFile(pwd, FormsAuthPasswordFormat.SHA1.ToString());
        }
    }
}
