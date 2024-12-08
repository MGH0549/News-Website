using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace NewsWebsite.Data.Migrations
{
    public class AppUsers : UserManager<IdentityUser>
    {
        public AppUsers(IUserStore<IdentityUser> store, IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<IdentityUser> passwordHasher, IEnumerable<IUserValidator<IdentityUser>> userValidators, IEnumerable<IPasswordValidator<IdentityUser>> passwordValidators, ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services, ILogger<UserManager<IdentityUser>> logger) : base(store, optionsAccessor, passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger)
        {
        }
    }
}
