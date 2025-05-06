using Cilink.WebApi.EndPoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder => builder
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials()
        .SetIsOriginAllowed((host) => true));
});




var app = builder.Build();

app.UseDefaultFiles();
app.UseCors("CorsPolicy");
app.UseStaticFiles();

app.UseHttpsRedirection();

app.MapDataEndPoints();
app.MapAuthEndPoints();
app.MapHelpEndPoints();

app.Run();

