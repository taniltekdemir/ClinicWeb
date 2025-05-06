using Microsoft.AspNetCore.Mvc;
using System.Data;
using Microsoft.Data.SqlClient;
using Cilink.WebApi.Models;
using Cilink.WebApi.Libraries;

namespace Cilink.WebApi.EndPoints
{
    public static class AuthEndPoints
    {
        public static void MapAuthEndPoints(this WebApplication app)
        {

            app.MapPost("/Api/Auth/Login/", (HttpContext context, [FromBody] AuthRequest request, HttpResponse response) =>
            {
                string AppCode = "P";
                string ip = String.Format("{0}", context.Connection.RemoteIpAddress);
                DataTable dt = new();
                Console.WriteLine($"{AppCode} Login");

                try
                {
                    ConnectionInformation con = new()
                    {
                        Server = app.Configuration[$"AppCodes:P:Server"],
                        UserName = app.Configuration[$"AppCodes:P:UserName"],
                        Password = app.Configuration[$"AppCodes:P:Password"],
                        DatabaseName = app.Configuration[$"AppCodes:P:DatabaseName"],
                        AppName = app.Configuration[$"AppCodes:P:AppName"],
                        IncludeUserToAppName = Convert.ToBoolean(app.Configuration[$"AppCodes:P:IncludeUserName"]),
                        ModuleName = "core.Login"
                    };


                    dt = (DataTable)DataAccessModule.Login(request, con, ip);

                    if (dt.Rows[0]["Status"].ToString() == "Success")
                        response.StatusCode = 200;
                    else
                        response.StatusCode = 401;


                }
                catch (SqlException exp)
                {
                    response.StatusCode = 500;
                    dt.Columns.Add("Error", typeof(string));
                    dt.Rows.Add(new object[] { exp.Message });
                }
                catch (Exception exp)
                {
                    response.StatusCode = 500;
                    if (!dt.Columns.Contains("Error"))
                        dt.Columns.Add("Error", typeof(string));
                    dt.Rows.Add(new object[] { exp.Message });
                }

                return Helper.SingleRowTable(dt);
            });

            app.MapPost("/Api/Auth/Logout/", ([FromBody] TokenCheckRequest LocalToken, HttpResponse response, HttpContext context) =>
            {
                string AppCode = "P";
                DataTable dt = new();

                Console.WriteLine($"{AppCode} Logout");

                string Token = "";

                if (!String.IsNullOrEmpty(LocalToken.Token))
                {
                    if (!Guid.TryParse(LocalToken.Token, out Guid TokenCheck))
                    {
                        response.StatusCode = 500;
                        dt.Columns.Add("Message", typeof(string));
                        dt.Rows.Add(new object[] { string.Format("Invalid token format - {0}", LocalToken.Token) });
                    }
                    else
                    {
                        Token = LocalToken.Token;

                        try
                        {
                            ConnectionInformation con = new()
                            {
                                Server = app.Configuration[$"AppCodes:{AppCode}:Server"],
                                UserName = app.Configuration[$"AppCodes:{AppCode}:UserName"],
                                Password = app.Configuration[$"AppCodes:{AppCode}:Password"],
                                DatabaseName = app.Configuration[$"AppCodes:{AppCode}:DatabaseName"],
                                AppName = app.Configuration[$"AppCodes:{AppCode}:AppName"],
                                IncludeUserToAppName = Convert.ToBoolean(app.Configuration[$"AppCodes:{AppCode}:IncludeUserName"]),
                                ModuleName = "core.Logout"
                            };

                            dt = (DataTable)DataAccessModule.Logout(Token, con);

                            if (dt.Rows[0]["Status"].ToString() == "Success")
                            {
                                response.Cookies.Delete("SID");
                                response.StatusCode = 200;
                            }

                        }//Exception handling yapılacak
                        catch (SqlException exp)
                        {
                            response.Cookies.Delete("SID");
                            response.StatusCode = 500;
                            dt.Columns.Add("Message", typeof(string));
                            dt.Rows.Add(new object[] { exp.Message });
                        }
                        catch (Exception exp)
                        {
                            response.Cookies.Delete("SID");
                            response.StatusCode = 500;
                            dt.Columns.Add("Message", typeof(string));
                            dt.Rows.Add(new object[] { exp.Message });
                        }
                    }
                }
                else
                {

                    response.StatusCode = 500;
                    dt.Columns.Add("Message", typeof(string));
                    dt.Rows.Add(new object[] { "Token not found" });
                }
                return Helper.SingleRowTable(dt);
            });

            app.MapPost("/Api/Auth/TokenCheck/", ([FromBody] TokenCheckRequest LocalToken, HttpResponse response, HttpContext context) =>
            {
                string AppCode = "P";
                DataTable dt = new();

                string Token = "";

                if (!String.IsNullOrEmpty(LocalToken.Token))
                {

                    if (!Guid.TryParse(LocalToken.Token, out Guid TokenCheck))
                    {
                        response.StatusCode = 500;
                        dt.Columns.Add("Message", typeof(string));
                        dt.Rows.Add(new object[] { string.Format("Invalid token format - {0}", LocalToken.Token) });
                    }
                    else
                    {
                        Token = LocalToken.Token;
                        try
                        {
                            ConnectionInformation con = new()
                            {
                                Server = app.Configuration[$"AppCodes:{AppCode}:Server"],
                                UserName = app.Configuration[$"AppCodes:{AppCode}:UserName"],
                                Password = app.Configuration[$"AppCodes:{AppCode}:Password"],
                                DatabaseName = app.Configuration[$"AppCodes:{AppCode}:DatabaseName"],
                                AppName = app.Configuration[$"AppCodes:{AppCode}:AppName"],
                                IncludeUserToAppName = Convert.ToBoolean(app.Configuration[$"AppCodes:{AppCode}:IncludeUserName"]),
                                ModuleName = "core.TokenCheck"
                            };

                            dt = (DataTable)DataAccessModule.TokenCheck(Token, con);

                        }//Exception handling yapılacak
                        catch (SqlException exp)
                        {
                            response.StatusCode = 500;
                            dt.Columns.Add("Message", typeof(string));
                            dt.Rows.Add(new object[] { exp.Message });
                        }
                        catch (Exception exp)
                        {
                            response.StatusCode = 500;
                            dt.Columns.Add("Message", typeof(string));
                            dt.Rows.Add(new object[] { exp.Message });
                        }
                    }
                }
                else
                {

                    response.StatusCode = 500;
                    dt.Columns.Add("Message", typeof(string));
                    dt.Rows.Add(new object[] { "Token not found" });
                }
                return Helper.SingleRowTable(dt);
            });

        }
    }
}
