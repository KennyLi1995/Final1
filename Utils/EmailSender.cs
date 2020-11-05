using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;

namespace FinalAssignment2.Utils
{
    public class EmailSender
    {
        // Please use your API KEY here.
        private const String API_KEY = "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";

        public void Send(List<EmailAddress> emailAddresses, String subject, String contents, HttpPostedFileBase postedFileBase)
        {
            var client = new SendGridClient(API_KEY);
            var from = new EmailAddress("kennyyyds@gmail.com", "Super English Training Campus");
            var tos = emailAddresses;
            var plainTextContent = contents;
            var htmlContent = "<p>" + contents + "</p >";
            var msg = MailHelper.CreateSingleEmailToMultipleRecipients(from, tos, subject, plainTextContent, htmlContent);
        
            if (postedFileBase != null && postedFileBase.ContentLength > 0)
            {
                string theFileName = Path.GetFileName(postedFileBase.FileName);
                byte[] fileBytes = new byte[postedFileBase.ContentLength];
                using (BinaryReader theReader = new BinaryReader(postedFileBase.InputStream))
                {
                    fileBytes = theReader.ReadBytes(postedFileBase.ContentLength);
                }
                string dataAsString = Convert.ToBase64String(fileBytes);
                msg.AddAttachment(theFileName, dataAsString);
            }

            var response = client.SendEmailAsync(msg);
        }

    }
}