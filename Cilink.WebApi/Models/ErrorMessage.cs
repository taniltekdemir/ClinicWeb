namespace Cilink.WebApi.Models
{
    public class ErrorMessage
    {
        public ErrorMessage(object message) { this.Message = message; }
        public object Message { get; set; }
    }
}
