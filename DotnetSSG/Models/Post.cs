namespace Generate
{
    public class Post : BaseModel
    {
        public string Title;
        public string Desc;
        public int Views;

        public override string GetSlug()
        {
            return Title;
        }
    }
}