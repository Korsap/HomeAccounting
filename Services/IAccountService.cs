﻿using System.Collections.Generic;
using System.Threading.Tasks;
using DomainModels.Model;

namespace Services
{
    public interface IAccountService
    {
        Task CreateAsync(Account item);
        Task<Account> GetItemAsync(int id);
        Task<IEnumerable<Account>> GetListAsync();
        IEnumerable<Account> GetList();
        Task DeleteAsync(int id);
        Task UpdateAsync(Account item);
        bool HasAnyDependencies(int accountId);
        bool HasEnoughMoney(Account account, decimal summ);
    }
}