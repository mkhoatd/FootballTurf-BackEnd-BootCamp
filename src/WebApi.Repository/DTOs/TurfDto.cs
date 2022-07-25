namespace WebApi.Repository.DTOs
{
    public class TurfDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string TurfType { get; set; }
        public List<string> ImageLinks { get; set; }
        public int Rating { get; set; }
    }
}
