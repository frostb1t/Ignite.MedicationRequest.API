using Ignite.MedicationRequest.API.Models.Enums;

namespace Ignite.MedicationRequest.API.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Sex Sex { get; set; }
    }
}
