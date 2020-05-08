using System.Collections.Generic;

namespace Generate
{
    class PostsController
    {
        public static IEnumerable<Post> Get()
        {
            return new List<Post>()
            {
                new Post
                {
                    Title = "FirstPost",
                    Desc =
                        "This is a post to prototype SSG with dotnet and eventually deploying to netlify, the goal is to create a small blog in the fashion of an SSG.",
                    Views = 6
                },
                new Post
                {
                    Title = "SecondPost",
                    Desc =
                        "The process of building this website builder",
                    Views = 6
                },
                new Post
                {
                    Title = "Third",
                    Desc =
                        "The process of building this website builder",
                    Views = 6
                }
            };
        }
    }
}