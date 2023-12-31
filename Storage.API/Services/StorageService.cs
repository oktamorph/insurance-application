﻿using Microsoft.EntityFrameworkCore;
using Storage.API.DBContext;
using Storage.API.Models;

namespace Storage.API.Services
{
    public class StorageService : IStorageService
    {
        #region Variables
        private readonly StorageContext _storageContext;
        #endregion
        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="storageContext">StorageContext instance.</param>
        public StorageService(StorageContext storageContext)
        {
            this._storageContext = storageContext;
        }
        /// <summary>
        /// The method that returns the storage item by guidId from the database.
        /// </summary>
        /// <param name="guidId">GuidID parameter.</param>
        /// <param name="customerNumber">Customer number parameter.</param>
        /// <returns></returns>
        public async Task<StorageItem> GetByIdAndCustomerNumber(Guid guidId, string customerNumber)
        {
            return await _storageContext.StorageItems.FirstOrDefaultAsync(x => x.InsuranceGuid == guidId && x.CustomerNumber == customerNumber);
        }
        /// <summary>
        /// The method that adds new storage item in the database.
        /// </summary>
        /// <param name="item">StorageItem instance.</param>
        /// <returns></returns>
        public async Task<StorageItem> Add(StorageItem item)
        {
            var result = await _storageContext.StorageItems.AddAsync(item);
            await _storageContext.SaveChangesAsync();

            return result.Entity;
        }
        /// <summary>
        /// The method that updates the storage item in the database.
        /// </summary>
        /// <param name="item">StorageItem instance.</param>
        /// <returns></returns>
        public async Task<StorageItem> Update(StorageItem item)
        {
            var result = await _storageContext.StorageItems.FirstOrDefaultAsync(x => x.InsuranceGuid == item.InsuranceGuid && x.CustomerNumber == item.CustomerNumber);

            if (result != null)
            {
                result.FirstName = item.FirstName;
                result.LastName = item.LastName;

                await _storageContext.SaveChangesAsync();                
            }

            return result;
        }
        /// <summary>
        /// The method that deletes the storage item from the database.
        /// </summary>
        /// <param name="guidId">GuidID parameter.</param>
        /// <param name="customerNumber">Customer number parameter.</param>
        public async void Delete(Guid guidId, string customerNumber)
        {
            _storageContext.StorageItems.Where(x => x.InsuranceGuid == guidId && x.CustomerNumber == customerNumber).ExecuteDelete();
            await _storageContext.SaveChangesAsync();
        }
    }
}
