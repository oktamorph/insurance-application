using Microsoft.EntityFrameworkCore;
using Storage.API.DBContext;
using Storage.API.Models;
using System.Diagnostics;

namespace Storage.API.Services
{
    public class StorageService : IStorageService
    {
        private readonly StorageContext _storageContext;
        public StorageService(StorageContext storageContext)
        {
            this._storageContext = storageContext;
        }
        public async Task<IEnumerable<StorageItem>> GetStorageItems()
        {
            return await _storageContext.Storages.ToListAsync();
        }
        public async Task<StorageItem> GetById(Guid id)
        {
            return await _storageContext.Storages.FirstOrDefaultAsync(x => x.InsuranceGuid == id);
        }
        public async Task<StorageItem> Add(StorageItem item)
        {
            var result = await _storageContext.Storages.AddAsync(item);
            await _storageContext.SaveChangesAsync();

            return result.Entity;
        }
        public async Task<StorageItem> Update(StorageItem item)
        {
            var result = await _storageContext.Storages.FirstOrDefaultAsync(x => x.InsuranceGuid == item.InsuranceGuid);

            if (result != null)
            {
                result.FirstName = item.FirstName;
                result.LastName = item.LastName;

                await _storageContext.SaveChangesAsync();
                return result;
            }

            return null;
        }
        public async void Delete(Guid id)
        {
            var result = await _storageContext.Storages.FirstOrDefaultAsync(x => x.InsuranceGuid == id);
            if (result != null)
            {
                _storageContext.Storages.Remove(result);
                await _storageContext.SaveChangesAsync();
            }
        }
    }
}
