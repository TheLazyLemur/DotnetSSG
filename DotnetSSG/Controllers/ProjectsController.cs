using System.Collections.Generic;

namespace Generate
{
    public class ProjectsController
    {
        public static IEnumerable<Project> Get()
        {
            return new List<Project>()
            {
                new Project()
                {
                    Title = "The Lost Botanist",
                    Description = "A 5 minutes interactive film created and showcased at Annecy Film Festival"
                },
                new Project()
                {
                    Title = "The Crescendo Of Ecstasy",
                    Description = "Marys art through her eyes."
                }
            };
        }
    }
}