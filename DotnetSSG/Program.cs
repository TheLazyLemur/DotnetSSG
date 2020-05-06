using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using RazorEngine;
using RazorEngine.Configuration;
using RazorEngine.Templating;
using RazorEngine.Text;

namespace Generate
{
    class Program
    {
        private const string SiteName = "MyCustomSSG";
        private const string TemplateDirectory = "./Templates/";
        private static readonly string OutputDirectory = $"/var/www/html/{SiteName}/";

        static void Main(string[] args)
        {
            ConfigureRazorEngine();

            var header = Engine.Razor.RunCompile(File.ReadAllText($"{TemplateDirectory}header.html"), "header");
            var footer = Engine.Razor.RunCompile(File.ReadAllText($"{TemplateDirectory}footer.html"), "footer");
            BuildIndexPage(header, footer);

            Build(PostsController.GetPosts().ToList(), header, footer);

            Build(FaqsController.GetFaqs().ToList(), header, footer);

            Build(ServicesController.GetServices().ToList(), header, footer);
        }

        private static void ConfigureRazorEngine()
        {
            var config = new TemplateServiceConfiguration
            {
                Language = Language.CSharp,
                EncodedStringFactory = new HtmlEncodedStringFactory(),
            };
            var razorEngineService = RazorEngineService.Create(config);
            Engine.Razor = razorEngineService;
        }

        public static void BuildCollection(string collectionName, string entryName, string htmlFromTemplate,
            string header, string footer)
        {
            Directory.CreateDirectory($"{OutputDirectory}{collectionName + "s"}/{entryName}");

            var fs = File.Create($"{OutputDirectory}{collectionName + "s"}/{entryName}/index.html");
            fs.Close();

            File.WriteAllText($"{OutputDirectory}{collectionName + "s"}/{entryName}/index.html", htmlFromTemplate);
        }

        private static void BuildIndexPage(string header, string footer)
        {
            var index = Engine.Razor.RunCompile(File.ReadAllText($"{TemplateDirectory}index.html"), "index"); ;
            File.WriteAllText($"{OutputDirectory}index.html", index);
        }

        public static void Build<T>(List<T> items, string header, string footer)
        {
            var item = items;
            var template = File.ReadAllText($"{TemplateDirectory}/{typeof(T).Name.ToLower()}.html");

            foreach (var i in item)
            {
                var n = i as BaseModel;
                var result = Engine.Razor.RunCompile(template, Guid.NewGuid().ToString(), typeof(T), i);
                if (n != null) BuildCollection(typeof(T).Name, n.GetSlug(), result, header, footer);
            }
        }
    }
}