namespace ShortUrl.Backend.Models
{
    public class ShortenData
    {
        public int Id {get;set;}
        public string? Url{get; set;}
        public string? ShortCode{get;set;}
        public DateTime CreatedAt{get;set;}
        public DateTime UpdatedAt{get;set;}
        public int AccessCount{get;set;}
    }
}