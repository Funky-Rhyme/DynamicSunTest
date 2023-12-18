using DynamicSunTest.Models.DBModels;
using DynamicSunTest.Models.DTO;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Numerics;
namespace DynamicSunTest.Utils
{
    public class ReportsProcessing
    {
        static bool isDbRebuild = false;

        private readonly ReportsContext _dbContext;

        public ReportsProcessing(ReportsContext dbContext)
        {
            _dbContext = dbContext;
        }

        private List<WeatherReport> MapToDbEntities(List<WeatherDTO> dtos)
        {
            var result = new List<WeatherReport>();

            foreach (var dto in dtos)
            {
                result.Add(new WeatherReport
                {
                    Date = DateOnly.Parse(dto.Date),
                    Time = TimeOnly.Parse(dto.Time),
                    Temperature = ParseStrToNum<float>(dto.Temperature),
                    Humidity = ParseStrToNum<int>(dto.Humidity),
                    DewPoint = ParseStrToNum<float>(dto.DewPoint),
                    AtmoPressure = ParseStrToNum<short>(dto.AtmoPressure),
                    WindDirection = dto.WindDirection,
                    WindSpeed = ParseStrToNum<short>(dto.WindSpeed),
                    Cloudiness = ParseStrToNum<byte>(dto.Cloudiness),
                    CloudinessLimit = ParseStrToNum<ushort>(dto.CloudinessLimit),
                    Visibility = ParseStrToNum<ushort>(dto.Visibility),
                    WeatherPhenomena = dto.WeatherPhenomena,
                });
            }

            return result;
        }

        public async Task<List<WeatherDTO>> GetDBEntitiesToDTO()
        {
            var result = new List<WeatherDTO>();
            var dbEntities = await _dbContext.WeatherReports
                .ToListAsync();
            foreach (var dbEntity in dbEntities)
            {
                result.Add(new WeatherDTO()
                {
                    Date = dbEntity.Date.ToString(),
                    Time = dbEntity.Time.ToString(),
                    Temperature = dbEntity.Temperature.ToString(),
                    Humidity = dbEntity.Humidity.ToString(),
                    DewPoint = dbEntity.DewPoint.ToString(),
                    AtmoPressure = dbEntity.AtmoPressure.ToString(),
                    WindDirection = dbEntity.WindDirection.ToString(),
                    WindSpeed = dbEntity.WindSpeed.ToString(),
                    Cloudiness = dbEntity.Cloudiness.ToString(),
                    CloudinessLimit = dbEntity.CloudinessLimit.ToString(),
                    Visibility = dbEntity.Visibility.ToString(),
                    WeatherPhenomena = dbEntity.WeatherPhenomena.ToString(),
                });
            }

            return result;
        }
        private static T ParseStrToNum<T>(string number) where T : INumber<T>
        {
            T result = T.Zero;

            bool success = T.TryParse(number, NumberStyles.Any |
                          NumberStyles.AllowDecimalPoint |
                          NumberStyles.Float, CultureInfo.CurrentCulture, out result);

            return result;
        }


        public async void AddToDb(List<WeatherDTO> reportsDTO)
        {
            var reports = MapToDbEntities(reportsDTO);

#if DEBUG
            Console.WriteLine($"Число считанных строк: {reportsDTO.Count}");
            if (isDbRebuild == false)
            {
                _dbContext.Database.EnsureDeleted();
                _dbContext.Database.EnsureCreated();

                isDbRebuild = true;
            }
#endif

            foreach (var report in reports)
            {
                await _dbContext.AddAsync(report);
            }

            _dbContext.SaveChanges();

        }
    }
}
