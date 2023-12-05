using Insurance.API.Models;

namespace Storage.API.Models
{
    public class StorageItem
    {
        public Guid Id { get; set; }
        public required InsuranceItem InsuranceItem { get; set; }
    }
}
