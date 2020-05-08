namespace Generate
{
    public class Project : BaseModel
    {
        public string Title;
        public string Description;

        public override string GetSlug()
        {
            return Title;
        }
    }
}