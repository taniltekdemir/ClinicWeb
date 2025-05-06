namespace Cilink.WebApi.Models
{
    public class ConnectionInformation
    {
        public string? Server { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? DatabaseName { get; set; }
        public string? AppName { get; set; }
        public bool IncludeUserToAppName { get; set; }

        public string? ModuleName { get; set; }

        public string GetConnectionString()
        {
            if (IncludeUserToAppName)
                AppName = $"{AppName}.{ModuleName}";

            string con = $"Data Source={Server};Initial Catalog={DatabaseName};User ID={UserName};Password={Password};Language=Turkish;Application Name={AppName};TrustServerCertificate=true";
            return con;
        }
    }
}
