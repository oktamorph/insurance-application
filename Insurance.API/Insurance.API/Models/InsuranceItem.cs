namespace Insurance.API.Models
{
    public class InsuranceItem
    {
        public int Id { get; set; }
        public Guid InsuranceGuid { get; set; }
        public required string CustomerNumber { get; set; } //Personnnummer
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
    }
}
