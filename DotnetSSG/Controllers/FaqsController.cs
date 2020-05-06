using System.Collections.Generic;

namespace Generate
{
    class FaqsController
    {
        public static IEnumerable<Faq> GetFaqs()
        {
            return new List<Faq>()
            {
                new Faq()
                {
                    Question = "HelloWorld",
                    Answer = "Hi There"
                },
                new Faq()
                {
                    Question = "HelloWorld",
                    Answer = "Hi There"
                }
            };
        }
    }
}