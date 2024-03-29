﻿using Microsoft.AspNetCore.Hosting;
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
        IMailService _mailService;
        public HomeController(ILogger<HomeController> logger, IHostingEnvironment env, IMailSender mailSender, IMailService mailService)
        {
            _logger = logger;
            _env = env;
            _mailSender = mailSender;
            _mailService = mailService;
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
            MailContent mailContent = new MailContent();
            mailContent.clientName = model.name;
            //mailContent.clientEmail = model.clientEmail;
            //mailContent.clientPassword = model.clientPassword;
            mailContent.email = model.email;
            //mailContent.ccemail = model.ccemail;
            //model.bccemail = model.bccemail;
            mailContent.subject = model.subject;
            mailContent.message = model.message;
            try
            {
                var data = await _mailService.SendMail(mailContent);
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

                string filePath = "~/documents/MyResume.pdf";

                Response.Headers.Add("Content-Disposition", "inline; filename=resume_osibemekun_temitoyosi.pdf");

                return File(filePath, "application/pdf");


                ////Name the File
                //string fileName = "osibemekun_temitoyosi_resume.pdf";

                ////Build the File Path.
                //var webRoot = _env.WebRootPath;

                //string path = System.IO.Path.Combine(webRoot, @"documents\osibemekun_temitoyosi_resume.pdf");

                ////Read the File data into Byte Array.
                //byte[] bytes = System.IO.File.ReadAllBytes(path);

                ////Send the File to Download.
                //return File(bytes, "application/octet-stream", fileName);
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

                string filePath = "~/documents/Resume.docx";

                Response.Headers.Add("Content-Disposition", "inline; filename=osibemekun_temitoyosi_resume.docx");

                return File(filePath, "application/docx");


                ////Name the File
                //string fileName = "osibemekun_temitoyosi_resume.docx";

                ////Build the File Path.
                //var webRoot = _env.WebRootPath;
                
                //string path = System.IO.Path.Combine(webRoot, @"documents\osibemekun_temitoyosi_resume.docx");

                ////Read the File data into Byte Array.
                //byte[] bytes = System.IO.File.ReadAllBytes(path);

                ////Send the File to Download.
                //return File(bytes, "application/octet-stream", fileName);
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
