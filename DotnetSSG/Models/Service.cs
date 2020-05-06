namespace Generate
{
    public class Service : BaseModel

    {
        public string ServiceName;
        public string ServiceDescription;

        public override string GetSlug()
        {
            return ServiceName;
        }
    }
}