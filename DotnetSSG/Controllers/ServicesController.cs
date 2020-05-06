using System.Collections.Generic;

namespace Generate
{
    public class ServicesController
    {
        public static IEnumerable<Service> GetServices()
        {
            return new List<Service>()
            {
                new Service
                {
                    ServiceName = "WebDev"
                },
                new Service
                {
                    ServiceName = "SoftwareDev"
                }
            };
        }
    }
}