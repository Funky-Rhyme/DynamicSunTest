using DynamicSunTest.Models.DTO;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Data;

namespace DynamicSunTest.Utils
{
    public class ExcelDataProvider
    {
        public List<WeatherDTO> GetExcelData(string filePath)
        {
            List<WeatherDTO> weatherList = new List<WeatherDTO>();
            ISheet sheet;

            using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                stream.Position = 0;

                XSSFWorkbook workBook = new XSSFWorkbook(stream);
                var sheetsNum = workBook.NumberOfSheets;

                for (int i = 0; i < sheetsNum; i++)
                {
                    sheet = workBook.GetSheetAt(i);

                    for (int j = 4; j <= sheet.LastRowNum; j++)
                    {
                        var row = sheet.GetRow(j);
                        weatherList.Add(MapWeatherDTO(row));
                    }
                }
            }

            return weatherList;
        }

        private WeatherDTO MapWeatherDTO(IRow row)
        {
            string GetString(int a)
            {
                return row.GetCell(a) is null ? string.Empty
                 : Convert.ToString(row.GetCell(a));
            }

            return new WeatherDTO
            {
                Date = GetString(0),
                Time = GetString(1),
                Temperature = GetString(2),
                Humidity = GetString(3),
                DewPoint = GetString(4),
                AtmoPressure = GetString(5),
                WindDirection = GetString(6),
                WindSpeed = GetString(7),
                Cloudiness = GetString(8),
                CloudinessLimit = GetString(9),
                Visibility = GetString(10),
                WeatherPhenomena = GetString(11),
            };
        }

    }
}
