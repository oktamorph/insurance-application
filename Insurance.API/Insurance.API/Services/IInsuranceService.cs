using Insurance.API.Models;

namespace Insurance.API.Services
{
    public interface IInsuranceService
    {
        IEnumerable<InsuranceItem> GetInsuranceItems();
        InsuranceItem GetById(Guid id);
        InsuranceItem Add(InsuranceItem item);
        
        InsuranceItem Update(InsuranceItem item);
        InsuranceItem Delete(Guid id);
    }
}
