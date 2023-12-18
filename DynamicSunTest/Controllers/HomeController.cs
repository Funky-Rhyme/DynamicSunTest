using DynamicSunTest.Models;
using DynamicSunTest.Models.DTO;
using DynamicSunTest.Utils;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DynamicSunTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ReportsContext _dbContext;

        public HomeController(ILogger<HomeController> logger, ReportsContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Archives(string sortOrder = "Year", int page = 1)
        {
            int pageSize = 50;

            var processor = new ReportsProcessing(_dbContext);
            IEnumerable<WeatherDTO> result = processor.GetDBEntitiesToDTO().Result;

            switch (sortOrder)
            {
                case "Year":
                    result = result.OrderByDescending(s => s.Year);
                    break;

                case "Month":
                    result = result.OrderByDescending(s => s.Month);
                    break;
            }

            var count = result.Count();
            result = result.Skip((page - 1) * 50).Take(pageSize).ToList();

            var pageViewModel = new PageViewModel(count, page, pageSize);
            var viewModel = new ArchivesViewModel
            {
                PageViewModel = pageViewModel,
                WeatherDTOs = result,
            };

            return View(viewModel);
        }

        public IActionResult ArchivesLoad()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
