using AutoMapper;
using Ignite.MedicationRequest.API.Mappings;

namespace Ignite.MedicationRequest.API.Tests
{
    public class MappingProfileTests
    {
        [Fact]
        public void MappingConfiguration_ShouldBeCorrect()
        {
            var mappingConfiguration = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));
            mappingConfiguration.AssertConfigurationIsValid();
        }
    }
}
