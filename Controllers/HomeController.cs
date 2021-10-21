using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using YosioluwaOsibemekun.Email;
using YosioluwaOsibemekun.Formatting;
using YosioluwaOsibemekun.Models;

namespace YosioluwaOsibemekun.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IHostingEnvironment _env;
        IMailSender _mailSender;
        public HomeController(ILogger<HomeController> logger, IHostingEnvironment env, IMailSender mailSender)
        {
            _logger = logger;
            _env = env;
            _mailSender = mailSender;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> SendEmail(SendEmailModel model)
        {
            var responses = ApiResponse.GetApiResponseMessages();

            try
            {
                var data = await _mailSender.SendEmail(model.Name, model.Message, model.Subject, model.Email);
                return Json(new DataResult { StatusCode = responses[ApiResponse.ApiResponseStatus.Successful], Message = ApiResponse.ApiResponseStatus.Successful.ToString(), Data = data });

            }
            catch (Exception ex)
            {
                    return Json(new DataResult { StatusCode = responses[ApiResponse.ApiResponseStatus.Failed], Message = ex.Message, Data = string.Empty });

            }

        }

        [HttpGet]
        public FileResult DownloadResumePdf()
        {
            try
            {
                //Name the File
                string fileName = "osibemekun_temitoyosi_resume.pdf";

                //Build the File Path.
                var webRoot = _env.WebRootPath;

                string path = System.IO.Path.Combine(webRoot, @"documents\osibemekun_temitoyosi_resume.pdf");

                //Read the File data into Byte Array.
                byte[] bytes = System.IO.File.ReadAllBytes(path);

                //Send the File to Download.
                return File(bytes, "application/octet-stream", fileName);
            }
            catch (Exception)
            {

                throw;
            }
        

        }

        [HttpGet]
        public FileResult DownloadResumeDocx()
        {
            try
            {
                //Name the File
                string fileName = "osibemekun_temitoyosi_resume.docx";

                //Build the File Path.
                var webRoot = _env.WebRootPath;
                
                string path = System.IO.Path.Combine(webRoot, @"documents\osibemekun_temitoyosi_resume.docx");

                //Read the File data into Byte Array.
                byte[] bytes = System.IO.File.ReadAllBytes(path);

                //Send the File to Download.
                return File(bytes, "application/octet-stream", fileName);
            }
            catch (Exception)
            {

                throw;
            }


        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
