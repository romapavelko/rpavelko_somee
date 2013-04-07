using System;
using System.Linq;
using rpavelko.Data.Core;
using rpavelko.Data.Entities;
using rpavelko.Data.Repositories.Interfaces;
using rpavelko.Data.Utils;

namespace rpavelko.Data.Repositories
{
    public class AccountRepository : RepositoryBase<Account>, IAccountRepository
    {
        public AccountRepository(IContextFactory factory)
            :base(factory)
        {
        }

        public bool ValidateUser(string email, string password)
        {
            var account = Context.Accounts.FirstOrDefault(a => a.Email == email);
            if (account == null)
            {
                return false;
            }
            var salt = account.PwdSalt;
            var hash1 = account.PwdHash;
            var hash2 = Security.HashPassword(password, salt);
            return hash1 == hash2;
        }

        public bool UnconfirmedAccountCheck(string email)
        {
            return Context.Accounts.SingleOrDefault(g => g.Email.Equals(email, StringComparison.CurrentCultureIgnoreCase) && g.ConfirmationCode != null) != null;
        }

        public Account GetAccountByEmail(string email)
        {
            return Context.Accounts.SingleOrDefault(g => g.Email.Equals(email, StringComparison.CurrentCultureIgnoreCase) && g.ConfirmationCode == null);
        }

        public Account GetAccountByEmailAndIgnoreConfirm(string email)
        {
            return Context.Accounts.SingleOrDefault(g => g.Email.Equals(email, StringComparison.CurrentCultureIgnoreCase));
        }
    }
}
