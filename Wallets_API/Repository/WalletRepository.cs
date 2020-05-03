using System.Threading.Tasks;
using Wallets_API.Data;
using Wallets_API.DBClasses;

namespace Wallets_API.Repository
{
    public class WalletRepository : IWalletRepository
    {
        private readonly ApplicationDbContext _context;

        public WalletRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<bool> CreateWallet(Wallet wallet)
        {
            _context.Wallets.Add(wallet);

            if (await _context.SaveChangesAsync() > 0)
                return true;
            return false;
        }
    }
}
