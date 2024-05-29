using UniversityCommunity.Data.EntityFramework.Base;
using UniversityCommunity.Data.EntityFramework.Entities;

namespace UniversityCommunity.Data.EntityFramework.Repositories.Interfaces
{
    public interface ICustomerRepository : IRepositoryBase<Customer>
    {
        Task<List<Cities>> GetCityDataAsync();
    }
}
