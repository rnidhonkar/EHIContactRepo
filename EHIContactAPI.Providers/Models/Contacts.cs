namespace EHIContactAPI.Providers.Models
{
    public class Contacts
    {
        public long ContactId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int PhoneNumber { get; set; }
        public CustomerStatus Status { get; set; }
    }
}
