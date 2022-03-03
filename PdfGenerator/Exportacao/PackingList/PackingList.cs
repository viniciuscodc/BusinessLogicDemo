using Application.Services.PdfGenerator;
using System;

namespace Application.PdfGenerator.Exportacao.PackingList
{
    public class PackingList : HtmlBuilder
    {
        public PackingList(object model)
        {
            this.FolderPath = "Exportacao/PackingList";
            BuildHtml(model);
            AddStaticFiles();
        }

        public void AddStaticFiles()
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            bool isDevelopment = environment == "Development";
            string url = isDevelopment ? "https://localhost:5001/" : "http://localhost:5000/";

            var cssName = "invoiceStyle.css";
            var imgName = "logo.jpg";
            string imgPath = url + "pdfGenerator/logo.jpg";
            string cssPath = url + "pdfGenerator/invoiceStyle.css";

            //Insert img and css from own server
            try
            {
                this.Html = this.Html.Replace(imgName, imgPath);
                this.Html = this.Html.Replace(cssName, cssPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}