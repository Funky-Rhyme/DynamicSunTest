using DynamicSunTest.Utils;
using Microsoft.AspNetCore.Mvc;

namespace DynamicSunTest.Controllers
{
    public class FileController : Controller
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ReportsContext _dbContext;

        public FileController(IWebHostEnvironment webHostEnvironment, ReportsContext dbContext)
        {
            _webHostEnvironment = webHostEnvironment;
            _dbContext = dbContext;
        }

        [HttpPost]
        public async Task<IActionResult> Upload(List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);

            if (size != 0)
            {
                string uploadFolder = Path.Combine(_webHostEnvironment.WebRootPath, "Uploads");

                if (!Directory.Exists(uploadFolder))
                {
                    Directory.CreateDirectory(uploadFolder);
                }
                
                var processor = new ReportsProcessing(_dbContext);

                foreach (var file in files)
                {
                    var fileName = Path.GetTempFileName();
                    fileName = Path.GetFileNameWithoutExtension(fileName) + ".xlsx";

                    var filePath = Path.Combine(uploadFolder, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    var provider = new ExcelDataProvider();
                    var reports = provider.GetExcelData(filePath);

                    processor.AddToDb(reports);

                    foreach (var tempFile in Directory.EnumerateFiles(uploadFolder)) 
                    {
                        System.IO.File.Delete(tempFile);
                    }
                }


                ViewBag.Message = "Файлы успешно загружены";

                return View("Views/Home/ArchivesLoad.cshtml");
            }
            else
            {
                ViewBag.Error = true;
                ViewBag.Message = "Ошбика загрузки файлов";

                return View("Views/Home/ArchivesLoad.cshtml");
            }
        }
    }
}
