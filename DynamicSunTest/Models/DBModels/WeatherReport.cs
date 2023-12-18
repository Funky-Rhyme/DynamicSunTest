namespace DynamicSunTest.Models.DBModels
{
    public class WeatherReport
    {
        public uint ReportId { get; set; }
        public DateOnly Date { get; set; }
        public TimeOnly Time { get; set; }
        public float Temperature { get; set; }
        public int Humidity { get; set; }
        public float DewPoint { get; set; }
        public short AtmoPressure { get; set; }
        public string WindDirection { get; set; }
        public short WindSpeed { get; set; }
        public byte Cloudiness { get; set; }

        /// <summary>
        /// Нижняя граница облачности
        /// </summary>
        public ushort CloudinessLimit { get; set; }
        /// <summary>
        /// Горизонтальная видимость
        /// </summary>
        public ushort Visibility { get; set; }
        /// <summary>
        /// Погодные явления
        /// </summary>
        public string WeatherPhenomena { get; set; }
    }
}
