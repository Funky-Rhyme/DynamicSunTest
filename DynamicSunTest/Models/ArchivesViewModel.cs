using DynamicSunTest.Models.DTO;

namespace DynamicSunTest.Models
{
    public class ArchivesViewModel
    {
        public IEnumerable<WeatherDTO> WeatherDTOs { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
