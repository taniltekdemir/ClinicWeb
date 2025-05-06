namespace Cilink.WebApi.Models
{
    public class GenericRequest
    {
        public Dictionary<string, object>? Parameters { get; set; }
        public string? UserAgent { get; set; }
        public string? Token { get; set; }
        public string? Language { get; set; }
    }
}
