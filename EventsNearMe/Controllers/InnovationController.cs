using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using EventsNearMe.Models;
using System.IO;
using System.Threading.Tasks;

using IronPdf;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Web.Hosting;
using Newtonsoft.Json;

namespace EventsNearMe.Controllers
{
    public class InnovationController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        // GET: Innovation
        public ActionResult SendBookingConfirmation(int bookingID)
        {
            Booking booking = db.Bookings.Include(b => b.Event.Location).FirstOrDefault(b => b.BookingID == bookingID);
            string bookingHtml = RenderRazorViewToString("~/Views/Innovation/SendBookingConfirmation.cshtml", booking.Event);
            Task.Run(async () => await sendComfirmation(booking, bookingHtml));
            return View(booking.Event);
        }

        public void sendUpdateOnEvent(int eventId)
        {
            Event UpdatedEvent = db.Events.Where(e => e.EventID == eventId).FirstOrDefault();
            List<string> EventBookingEmail = db.Bookings.Include(b => b.User).Select(b => b.User.Email).ToList();
            Task.Run(async () => await sendBulkEmail(EventBookingEmail, UpdatedEvent));
        }

        static async Task Execute(String OutputPath, string pdfName, Booking booking)
        {
            var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("noreply@eventsnearme.com", "Booking confirmation");
            var subject = "Successful booking " + booking.Event.Name;
            var to = new EmailAddress(booking.User.Email, "Example User");
            var plainTextContent = "You have succesfully booked " + booking.Event.Name;
            var htmlContent = "Please see attached PDF file";
            var attachment = new Attachment();
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
            var bytes = System.IO.File.ReadAllBytes(OutputPath);
            var file = Convert.ToBase64String(bytes);
            msg.AddAttachment(pdfName, file);
            var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
        }



        private string RenderRazorViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View,
                ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }

        private static async Task sendComfirmation(Booking booking, string html)
        {
            var PDF = await RenderPdfAsync(html);
            var pdfName = booking.BookingID.ToString() + booking.Event.EventID.ToString() + booking.Event.Name + ".pdf";
            var OutputPath = "~/Content/PDF/" + pdfName;
            PDF.SaveAs(HostingEnvironment.MapPath(OutputPath));
            Execute(HostingEnvironment.MapPath(OutputPath), pdfName, booking).Wait();
        }

        private static async Task sendBulkEmail(List<string> emails, Event UpdatedEvent)
        {
            var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
            var client = new SendGridClient(apiKey);
            var from = new EmailAddress("noreply@eventsnearme.com", "EventsNearMe");
            var tos = emails.Select(e => new EmailAddress(e, e)).ToList();
            var subject = "Change to the upcomming event :" + UpdatedEvent.Name;
            var plainTextContent = "There's been some changes to " + UpdatedEvent.Name;
            var htmlContent = "Please visit our website find out what has change.";
            var showAllRecipients = false;
            var msg = MailHelper.CreateSingleEmailToMultipleRecipients(from, tos, subject, plainTextContent, htmlContent, showAllRecipients);
            var response = await client.SendEmailAsync(msg).ConfigureAwait(false);
        }

        private static async Task<IronPdf.PdfDocument> RenderPdfAsync(string Html, IronPdf.PdfPrintOptions PrintOptions = null)
        {
            return await Task.Run(() => RenderPdf(Html, PrintOptions));
        }

        private static IronPdf.PdfDocument RenderPdf(string Html, IronPdf.PdfPrintOptions PrintOptions = null)
        {
            var Renderer = new IronPdf.HtmlToPdf();
            Renderer.PrintOptions.CssMediaType = PdfPrintOptions.PdfCssMediaType.Print;
            if (PrintOptions != null)
            {
                Renderer.PrintOptions = PrintOptions;
            }

            PdfDocument Pdf = Renderer.RenderHtmlAsPdf(Html);
            return Pdf;
        }


        private class UpdateTemplate
        {
            [JsonProperty("name")]
            public string name { get; set; }

            [JsonProperty("event")]
            public string eventTitle { get; set; }

        }
    }

}