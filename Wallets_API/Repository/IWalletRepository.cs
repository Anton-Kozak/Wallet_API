using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wallets_API.DBClasses;
using Wallets_API.DTO;
using Wallets_API.Models;
using Wallets_API.Models.CustomModels;

namespace Wallets_API.Repository
{
    public interface IWalletRepository
    {
        Task<bool> CreateWallet(Wallet wallet, ApplicationUser user);
        Task<Wallet> GetCurrentWallet(int walletId);
        Task<ResponseData> EditWallet(int walletId, WalletToReturnDTO walletToEdit);       
    }
}
