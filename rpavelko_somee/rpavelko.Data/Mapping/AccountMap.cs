using System.Data.Entity.ModelConfiguration;
using rpavelko.Data.Entities;

namespace rpavelko.Data.Mapping
{
    public class AccountMap : EntityTypeConfiguration<Account>
    {
        public AccountMap()
        {
            // Primary Key
            HasKey(a => a.Id);

            //Property(a => a.Id).HasColumnName("accID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None); //--------------- For SQL-Migration Script

            // Table & Column Mappings
            ToTable("tblAccounts");
            Property(a => a.Id).HasColumnName("accID");
            Property(a => a.FirstName).HasColumnName("accFirstName");
            Property(a => a.LastName).HasColumnName("accLastName");
            Property(a => a.Email).HasColumnName("accEmail");
            Property(a => a.Phone).HasColumnName("accPhone");
            Property(a => a.ConfirmationCode).HasColumnName("accConfirmationCode");
            Property(a => a.PwdHash).HasColumnName("accPwdHash");
            Property(a => a.PwdSalt).HasColumnName("accPwdSalt");
        }
    }
}
