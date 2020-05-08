using System.Collections.Generic;

namespace Generate
{
    public class ServicesController
    {
        public static IEnumerable<Service> Get()
        {
            return new List<Service>()
            {
                new Service
                {
                    ServiceName = "WebDev",
                    ServiceDescription = "Developing experience for the Web"
                },
                new Service
                {
                    ServiceName = "SoftwareDev"
                }
            };
        }
    }
}