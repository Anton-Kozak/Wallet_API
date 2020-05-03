using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Wallets_API.DBClasses;
using Wallets_API.Models;

namespace Wallets_API.Repository
{
    public interface IWalletRepository
    {
        Task<bool> CreateWallet(Wallet wallet);
    }
}
