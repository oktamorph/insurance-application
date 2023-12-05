using Microsoft.EntityFrameworkCore;
using Storage.API.Models;

namespace Storage.API.DBContext
{
    public class StorageContext : DbContext
    {
        public StorageContext(DbContextOptions<StorageContext> options) : base(options) { }
        public DbSet<StorageItem> StorageItems { get; set; }
    }
}
