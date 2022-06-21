

namespace ShoppingApp.IdentityContext
{
    public class IdentityDataContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public IdentityDataContext(DbContextOptions options) : base(options)
        {
        }
    }
}
