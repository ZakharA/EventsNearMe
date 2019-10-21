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

namespace EventsNearMe.Controllers
{
    public class InnovationController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        // GET: Innovation
        public ActionResult SendBookingConfirmation(int bookingID)
        {
            Booking booking = db.Bookings.Include(b => b.Event.Location).FirstOrDefault(b => b.BookingID == bookingID);
            GeneratePDF(booking);
            return View(booking.Event);
        }

        private void GeneratePDF(Booking booking)
        {
            var Renderer = new IronPdf.HtmlToPdf();
            Renderer.PrintOptions.CssMediaType = PdfPrintOptions.PdfCssMediaType.Print;
            Renderer.PrintOptions.RenderDelay = 10000;
            string bookingHtml = RenderRazorViewToString("~/Views/Innovation/SendBookingConfirmation.cshtml", booking.Event);
            var PDF = Renderer.RenderHtmlAsPdf(bookingHtml);
            var pdfName = booking.BookingID.ToString() + booking.Event.EventID.ToString() + booking.Event.Name + ".pdf";
            var OutputPath = "~/Content/PDF/"+ pdfName;
            PDF.SaveAs(OutputPath);
            //Execute(Server.MapPath(OutputPath), pdfName, booking).Wait();

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
    }
}