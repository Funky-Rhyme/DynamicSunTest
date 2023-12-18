using DynamicSunTest.Models.DBModels;
using Microsoft.EntityFrameworkCore;

namespace DynamicSunTest.Utils
{
    public class ReportsContext : DbContext
    {
        public DbSet<WeatherReport> WeatherReports { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=localhost;Database=Reports;" +
                    "PersistSecurityInfo=True;User Id=sa;Password=superSTRONGpass(!);" +
                    "Encrypt=False;TrustServerCertificate=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WeatherReport>(entity =>
            {
                entity.HasKey(e => e.ReportId);
                entity.Property(e => e.ReportId).ValueGeneratedOnAdd().UseIdentityColumn().HasColumnName("ReportId");        
                entity.Property(e => e.Date).HasColumnName("Date");
                entity.Property(e => e.Time).HasColumnName("Time");
                entity.Property(e => e.Temperature).HasColumnName("Temperature");
                entity.Property(e => e.Humidity).HasColumnName("Humidity");
                entity.Property(e => e.DewPoint).HasColumnName("Dew Point");
                entity.Property(e => e.AtmoPressure).HasColumnName("Atmospheric Pressure");
                entity.Property(e => e.WindDirection).HasColumnName("Wind Direction");
                entity.Property(e => e.WindSpeed).HasColumnName("Wind Speed");
                entity.Property(e => e.Cloudiness).HasColumnName("Cloudiness");
                entity.Property(e => e.CloudinessLimit).HasColumnName("Cloudiness lower boundary");
                entity.Property(e => e.Visibility).HasColumnName("Horizontal Visibility");
                entity.Property(e => e.WeatherPhenomena).HasColumnName("Weateher Phenomena");
            });
        }
    }
}
