using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading;
using System.IO;
using FinalAssignment2.Models;
using FinalAssignment2.Utils;
using SendGrid;
using SendGrid.Helpers.Mail;


namespace FinalAssignment2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Send_Email()
        {
            return View(new SendEmailViewModel());
        }

        [HttpPost]
        public ActionResult Send_Email(SendEmailViewModel model, HttpPostedFileBase postedFileBase)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    String toEmail = model.ToEmail;
                    String subject = model.Subject;
                    String contents = model.Contents;
                    //HttpPostedFileBase postedFileBase= model.Upload;

                    EmailSender es = new EmailSender();
                    String[] toEmailArray = toEmail.Split(';');
                    List<EmailAddress> emailAddresses = new List<EmailAddress>();
                    foreach (String s in toEmailArray) {
                        emailAddresses.Add(new EmailAddress(s, ""));
                    }
                    es.Send(emailAddresses, subject, contents, postedFileBase);

                    ViewBag.Result = "Email has been send.";

                    ModelState.Clear();

                    return View(new SendEmailViewModel());
                }
                catch
                {
                    return View();
                }
            }

            return View();
        }
    }
}