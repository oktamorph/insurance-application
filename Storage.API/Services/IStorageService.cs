using Storage.API.Models;

namespace Storage.API.Services
{
    public interface IStorageService
    {
        Task<StorageItem> GetByIdAndCustomerNumber(Guid guidId, string customerNumber);
        Task<StorageItem> Add(StorageItem item);
        Task<StorageItem> Update(StorageItem item);
        void Delete(Guid guidId, string customerNumber);
    }
}
