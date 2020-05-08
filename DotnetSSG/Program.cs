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
            BuildTheWebsite();
        }

        private static void BuildTheWebsite()
        {
            ConfigureRazorEngine();

            Engine.Razor.AddTemplate("layout", File.ReadAllText($"{TemplateDirectory}layout.html"));

            //Build index.html
            BuildIndexPage();
            //Build single pages (about)

            //Build Collection Pages
            BuildCollection(PostsController.Get().ToList());
            BuildCollection(FaqsController.Get().ToList());
            BuildCollection(ServicesController.Get().ToList());
            BuildCollection(ProjectsController.Get().ToList());

            //Build sitemap.xml
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

        private static void BuildIndexPage()
        {
            // Build list page
            var index = Guid.NewGuid().ToString();
            Engine.Razor.AddTemplate(index, $@"@{{Layout = ""layout"";}}{File.ReadAllText($"{TemplateDirectory}index.html")}");
            Engine.Razor.Compile(index);
            var result = Engine.Razor.Run(index);
            File.WriteAllText($"{OutputDirectory}index.html", result);
        }
        
        public static void BuildCollection<T>(List<T> items)
        {
            var template =
                File.ReadAllText($"{TemplateDirectory}/{typeof(T).Name.ToLower()}/{typeof(T).Name.ToLower()}.html");
            var collectionName = typeof(T).Name.ToLower();
            Directory.CreateDirectory($"{OutputDirectory}{typeof(T).Name.ToLower() + "s"}");

            BuildListPageTemplates(items, collectionName);
            BuildSinglePageTemplates(items, template, collectionName);
        }

        private static void BuildListPageTemplates<T>(List<T> items, string collectionName)
        {
            var listTemplate =
                File.ReadAllText(
                    $"{TemplateDirectory}{collectionName.ToLower()}/{collectionName.ToLower()}list.html");

            var listHtml = Engine.Razor.RunCompile(listTemplate, Guid.NewGuid().ToString(), null, new
            {
                items
            });

            // Build list page
            var listGuid = Guid.NewGuid().ToString();
            Engine.Razor.AddTemplate(listGuid, $@"@{{Layout = ""layout"";}}{listHtml}");
            Engine.Razor.Compile(listGuid);
            var result = Engine.Razor.Run(listGuid);
            var fileStream = File.Create($"{OutputDirectory}{collectionName.ToLower() + "s"}/index.html");
            fileStream.Close();
            File.WriteAllText($"{OutputDirectory}{collectionName.ToLower() + "s"}/index.html", result);
        }

        private static void BuildSinglePageTemplates<T>(List<T> allItems, string template, string collectionName)
        {
            // Loop through all items in collection
            foreach (var item in allItems)
            {
                var model = item as BaseModel;
                var singleHtml = Engine.Razor.RunCompile(template, Guid.NewGuid().ToString(), typeof(T), item);
                
                if(model == null) continue;
                var entryName = model.GetSlug().ToLower();

                Directory.CreateDirectory($"{OutputDirectory}{typeof(T).Name.ToLower() + "s"}/{entryName}");
                
                // Build single pages
                var singleGuid = Guid.NewGuid().ToString();
                Engine.Razor.AddTemplate(singleGuid, $@"@{{Layout = ""layout"";}}{singleHtml}");
                Engine.Razor.Compile(singleGuid);
                var resultSingle = Engine.Razor.Run(singleGuid);
                var fileStream = File.Create($"{OutputDirectory}{collectionName.ToLower() + "s"}/{entryName}/index.html");
                fileStream.Close();
                File.WriteAllText($"{OutputDirectory}{collectionName.ToLower() + "s"}/{entryName}/index.html",
                    resultSingle);
            }
        }
    }
}