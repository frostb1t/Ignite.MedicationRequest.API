using AutoMapper;

namespace Ignite.MedicationRequest.API.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // API Requests > Core Requests
            CreateMap<DTOs.Requests.CreateMedicationRequestRequest, Services.Requests.CreateMedicationRequestRequest>()
                .ForMember(
                    dest => dest.PatientId,
                    opt => opt.Ignore()
                );

            // Core Request > Entities
            CreateMap<Services.Requests.CreateMedicationRequestRequest, Models.MedicationRequest>()
                .ForMember(
                    dest => dest.Patient,
                    opt => opt.Ignore()
                ).ForMember(
                    dest => dest.Clinician,
                    opt => opt.Ignore()
                ).ForMember(
                    dest => dest.Medication,
                    opt => opt.Ignore()
                ).ForMember(
                    dest => dest.Id,
                    opt => opt.Ignore()
                );
        }
    }
}
