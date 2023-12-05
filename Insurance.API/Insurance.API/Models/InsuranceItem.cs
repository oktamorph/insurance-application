namespace Insurance.API.Models
{
    public class InsuranceItem
    {
        public Guid Id { get; set; }
        public required string CustomerNumber { get; set; } //Personnnummer
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
    }
}
