namespace Ignite.MedicationRequest.API.Models
{
    public class Clinician
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Guid RegistrationId { get; set; }
    }
}
