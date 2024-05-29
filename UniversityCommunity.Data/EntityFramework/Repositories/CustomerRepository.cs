﻿using UniversityCommunity.Data.EntityFramework.Base;
using UniversityCommunity.Data.EntityFramework.Entities;
using UniversityCommunity.Data.EntityFramework.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace UniversityCommunity.Data.EntityFramework.Repositories
{
    public class CustomerRepository : EfCoreRepositoryBase<Customer>, ICustomerRepository, IScopedRepository
    {
        private readonly UniversityCommunityContext _context;

        public CustomerRepository(UniversityCommunityContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Cities>> GetCityDataAsync()
        {
            return await _context.Cities
             .OrderBy(p => p.CityID)
            .ToListAsync();
        }

        //public async Task AddCustomerTransaction(CustomerSaveTransactionDto customerSaveTransactionDto)
        //{
        //    _context.CustomerTransactions.Add(new CustomerTransaction
        //    {
        //        CustomerID = customerSaveTransactionDto.CustomerID,
        //        GameTransactionID = customerSaveTransactionDto.GameTransactionID,
        //        GameID = customerSaveTransactionDto.GameID,
        //        TransactionType = 1,
        //        TransactionDetail = customerSaveTransactionDto.TransactionDetail,
        //        Createdate = DateTime.Now
        //    });

        //    await _context.SaveChangesAsync();
        //}       

        //public async Task<List<CustomerTransaction>> GetGameHistory(int customerID)
        //{
        //    var customerTransactions = await _context.CustomerTransactions
        //        .Where(p => p.CustomerID == customerID)
        //        .Select(p => new CustomerTransaction
        //        {
        //            Createdate = p.Createdate,
        //            TransactionDetail = p.TransactionDetail
        //        })
        //        .ToListAsync();            

        //    return customerTransactions;
        //}

    }
}
