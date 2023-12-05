using Insurance.API.Models;

namespace Insurance.API.Services
{
    public interface IInsuranceService
    {
        Task<InsuranceItem> GetByIdAndCustomerNumber(Guid guidId, string customerNumber);
        Task<InsuranceItem> Add(InsuranceItem item);        
        Task<InsuranceItem> Update(InsuranceItem item);
        void Delete(Guid guidId, string customerNumber);
    }
}
