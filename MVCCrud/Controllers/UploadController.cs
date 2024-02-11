using Microsoft.AspNetCore.Mvc;

namespace MVCCrud.Controllers
{
    public class UploadController : Controller
    {
        private readonly IWebHostEnvironment _hostingEnvironment;

        public UploadController(IWebHostEnvironment hostingEnv)
        {
            _hostingEnvironment = hostingEnv;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Upload(IFormFile formFile)
        {
            string webrootPath  = _hostingEnvironment.WebRootPath;
            var fileName = Path.GetFileName(formFile.FileName);
            var finalFilePath = Path.Combine(webrootPath, "images", fileName);
            using (var stream = System.IO.File.Create(finalFilePath))
            {
                await formFile.CopyToAsync(stream);
            }

            return RedirectToAction("Index");
        }
    }
}
