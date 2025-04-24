using Ignite.MedicationRequest.API.Models.Enums;

namespace Ignite.MedicationRequest.API.Models
{
    public class Medication
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string CodeName { get; set; }
        public string CodeSystem { get; set; }
        public double StrengthValue { get; set; }
        public string StrengthUnit { get; set; }
        public Form Form { get; set; }
    }
}
