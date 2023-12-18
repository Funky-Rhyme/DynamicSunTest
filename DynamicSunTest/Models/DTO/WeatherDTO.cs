namespace DynamicSunTest.Models.DTO
{
    public class WeatherDTO
    {
        private int _month;
        private int _year;

        public int Month
        {
            get => _month;
            set
            {
                _month = DateOnly.Parse(Date).Month;
            }
        }

        public int Year
        {
            get => _year;
            set
            {
                _year = DateOnly.Parse(Date).Year;
            }
        }


        public string Date { get; set; }
        public string Time { get; set; }
        public string Temperature { get; set; }
        public string Humidity { get; set; }
        public string DewPoint {  get; set; }
        public string AtmoPressure { get; set; }
        public string WindDirection { get; set; }
        public string WindSpeed { get; set; }
        public string Cloudiness {  get; set; }

        /// <summary>
        /// Нижняя граница облачности
        /// </summary>
        public string CloudinessLimit { get; set; }
        /// <summary>
        /// Горизонтальная видимость
        /// </summary>
        public string Visibility { get; set; } 
        /// <summary>
        /// Погодные явления
        /// </summary>
        public string WeatherPhenomena { get; set; }

    }
}
