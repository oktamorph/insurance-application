using Storage.API.Models;

namespace Storage.API.Services
{
    public interface IStorageService
    {
        Task<IEnumerable<StorageItem>> GetStorageItems();
        Task<StorageItem> GetById(Guid id);
        Task<StorageItem> Add(StorageItem item);
        Task<StorageItem> Update(StorageItem item);
        void Delete(Guid id);
    }
}
