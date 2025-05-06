using System.Net.Mime;

namespace Cilink.WebApi.EndPoints
{
    public static class Help
    {
        public static void MapHelpEndPoints(this WebApplication app)
        {
            app.MapGet("/Help/{AppCode}", (HttpResponse response) =>
            {
                string _html = @$"<!doctype html>
                <html>
                    <head><title>miniHTML</title></head>
                    <body>
                        <h1>Hello World</h1>
                        <p>The time on the server is {DateTime.Now:O}</p>
                    </body>
                </html>";


                response.ContentType = MediaTypeNames.Text.Html;
                response.ContentLength = System.Text.Encoding.UTF8.GetByteCount(_html);
                return response.WriteAsync(_html);
            });

        }
    }
}
