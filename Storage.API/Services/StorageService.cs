using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Storage.API.DBContext;
using Storage.API.Models;
using System.Diagnostics;
using System.Text.Json;

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
            return await _storageContext.StorageItems.ToListAsync();
        }
        public async Task<StorageItem> GetById(Guid id)
        {
            return await _storageContext.StorageItems.FirstOrDefaultAsync(x => x.InsuranceGuid == id);
        }
        public async Task<StorageItem> Add(StorageItem item)
        {
            var result = await _storageContext.StorageItems.AddAsync(item);
            await _storageContext.SaveChangesAsync();

            return result.Entity;
        }
        public async Task<StorageItem> Update(StorageItem item)
        {
            var result = await _storageContext.StorageItems.FirstOrDefaultAsync(x => x.InsuranceGuid == item.InsuranceGuid);

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
            _storageContext.StorageItems.Where(x => x.InsuranceGuid == id).ExecuteDelete();
            await _storageContext.SaveChangesAsync();
        }
    }
}
