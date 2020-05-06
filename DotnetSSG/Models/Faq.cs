namespace Generate
{
    public class Faq : BaseModel
    {
        public string Question;
        public string Answer;

        public override string GetSlug()
        {
            return Question;
        }
    }
}