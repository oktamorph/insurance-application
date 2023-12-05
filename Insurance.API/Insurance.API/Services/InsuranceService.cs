using Insurance.API.Models;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Insurance.API.Services
{
    public class InsuranceService : IInsuranceService
    {
        public async Task<InsuranceItem> GetByIdAndCustomerNumber(Guid guidId, string customerNumber)
        {
            throw new NotImplementedException();
        }
        public async Task<InsuranceItem> Add(InsuranceItem item)
        {
            throw new NotImplementedException();
        }
        public async Task<InsuranceItem> Update(InsuranceItem item)
        {
            throw new NotImplementedException();
        }
        public async void Delete(Guid guidId, string customerNumber)
        {
            throw new NotImplementedException();
        }
    }
}
