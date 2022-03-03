using RazorEngine;
using RazorEngine.Templating;
using System;
using System.IO;
using System.Reflection;

namespace Application.Services.PdfGenerator
{
    public abstract class HtmlBuilder : Templates
    {
        public bool Success { get; private set; }
        protected string FolderPath { get; set; }
        public string Html { get; set; }

        public string BuildHtml(object model)
        {
            var buildDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            buildDir = buildDir + "/Services/PdfGenerator/" + FolderPath + "/Index.cshtml";

            Guid key = Guid.NewGuid();

            try
            {
                var template = File.ReadAllText(buildDir);

                Html = Engine.Razor.RunCompile(template, key.ToString(), model.GetType(), model);
            }
            catch (Exception ex)
            {
                Html = null;
                Console.WriteLine(ex);
                Success = false;

                return Html;
            }

            Success = true;

            return Html;
        }
    }
}