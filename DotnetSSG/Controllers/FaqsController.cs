using System.Collections.Generic;

namespace Generate
{
    class FaqsController
    {
        public static IEnumerable<Faq> Get()
        {
            return new List<Faq>()
            {
                new Faq()
                {
                    Question = "How long have you been coding for",
                    Answer = "4 Years"
                },
                new Faq()
                {
                    Question = "What is your tech stack",
                    Answer = "primarily dotnet"
                }
            };
        }
    }
}