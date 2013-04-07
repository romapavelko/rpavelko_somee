using rpavelko.Data.Entities;

namespace rpavelko.Data.Repositories.Interfaces
{
    public interface IAccountRepository : IRepository<Account>
    {
        Account GetAccountByEmail(string email);
        Account GetAccountByEmailAndIgnoreConfirm(string email);
        bool UnconfirmedAccountCheck(string email);
        bool ValidateUser(string email, string password);
    }
}
