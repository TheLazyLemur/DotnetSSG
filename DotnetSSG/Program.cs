using System.Collections.Generic;
using System.IO;
using System.Linq;
using RazorEngine;
using RazorEngine.Configuration;
using RazorEngine.Templating;

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
            Build(PostsController.GetPosts().ToList());

            ConfigureRazorEngine();
            Build(FaqsController.GetFaqs().ToList());

            ConfigureRazorEngine();
            Build(ServicesController.GetServices().ToList());
        }

        private static void ConfigureRazorEngine()
        {
            var config = new TemplateServiceConfiguration {Language = Language.CSharp};
            var razorEngineService = RazorEngineService.Create(config);
            Engine.Razor = razorEngineService;
        }

        public static void BuildCollection(string collectionName, string entryName, string htmlFromTemplate)
        {
            var header = File.ReadAllText($"{TemplateDirectory}header.html");
            var footer = File.ReadAllText($"{TemplateDirectory}footer.html");
            var index = File.ReadAllText($"{TemplateDirectory}index.html");

            Directory.CreateDirectory($"{OutputDirectory}{collectionName + "s"}/{entryName}");

            var fs = File.Create($"{OutputDirectory}{collectionName + "s"}/{entryName}/index.html");
            fs.Close();
            File.WriteAllText($"{OutputDirectory}index.html", header + index + footer);
            File.WriteAllText($"{OutputDirectory}{collectionName + "s"}/{entryName}/index.html",
                header + htmlFromTemplate + footer);
        }

        public static void Build<T>(List<T> items)
        {
            var item = items;
            var template = File.ReadAllText($"{TemplateDirectory}/{typeof(T).Name.ToLower()}.html");

            foreach (var i in item)
            {
                var n = i as BaseModel;
                var result = Engine.Razor.RunCompile(template, "templateKey", typeof(T), i);
                if (n != null) BuildCollection(typeof(T).Name, n.GetSlug(), result);
            }
        }
    }
}