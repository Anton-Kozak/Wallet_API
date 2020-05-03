using Microsoft.AspNetCore.Identity;

namespace Wallets_API.Models
{
    public class ApplicationUser: IdentityUser
    {
        public string Address { get; set; }
        public string Age { get; set; }
        public int WalletID { get; set; }
        public bool IsWalletAdmin { get; set; }
    }
}
