using Cilink.WebApi.Libraries;
using Cilink.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Cilink.WebApi.EndPoints
{
    public static class DataEndPoints
    {
        public static void MapDataEndPoints(this WebApplication app)
        {
            app.MapPost("/Api/Data/{Command}", (string Command, [FromBody] Dictionary<string, object>? Parameters, HttpResponse response, HttpContext context) =>
            {
                object resp = new();
                string token = context.Request.Headers["Token"];
                string userAgent = context.Request.Headers["User-Agent"];
                string languaege = context.Request.Headers["Accept-Language"];

                if (!Guid.TryParse(token, out Guid Token))
                {
                    resp = new ErrorMessage(string.Format("Invalid token format - {0}", token));
                    response.StatusCode = 401;
                }
                else
                {
                    var con = GetConnection(app, userAgent);

                    try
                    {
                        Command = "post." + Command;
                        DataTable dt = DataAccessModule.GetTable(Command, new GenericRequest() { Token = token, Parameters = Parameters, Language = languaege, UserAgent = userAgent }, con);
                        response.StatusCode = 200;
                        if (Command.EndsWith("single"))
                            resp = Helper.SingleRowTable(dt);
                        else
                            resp = Helper.MultiRowTable(dt);

                    }
                    catch (SqlException exp)
                    {
                        HandleSqlException(ref response, ref resp, exp);
                    }
                    catch (Exception exp)
                    {
                        // TODO: LOGGING REQUIRED
                        response.StatusCode = 500;
                        new ErrorMessage("Server Error");
                    }
                }
                return resp;
            });

            app.MapGet("/Api/Data/{Command}", (string Command, [FromBody] Dictionary<string, object>? Parameters, HttpResponse response, HttpContext context) =>
            {
                object resp = new();
                string token = context.Request.Headers["Token"];
                string userAgent = context.Request.Headers["User-Agent"];
                string languaege = context.Request.Headers["Accept-Language"];

                if (!Guid.TryParse(token, out Guid Token))
                {
                    resp = new ErrorMessage(string.Format("Invalid token format - {0}", token));
                    response.StatusCode = 401;
                }
                else
                {
                    var con = GetConnection(app, userAgent);

                    try
                    {
                        Command = "get." + Command;
                        DataTable dt = DataAccessModule.GetTable(Command, new GenericRequest() { Token = token, Parameters = Parameters, Language = languaege, UserAgent = userAgent }, con);
                        response.StatusCode = 200;
                        if (Command.EndsWith("single"))
                            resp = Helper.SingleRowTable(dt);
                        else
                            resp = Helper.MultiRowTable(dt);

                    }
                    catch (SqlException exp)
                    {
                        HandleSqlException(ref response, ref resp, exp);
                    }
                    catch (Exception exp)
                    {
                        // TODO: LOGGING REQUIRED
                        response.StatusCode = 500;
                        new ErrorMessage("Server Error");
                    }
                }
                return resp;
            });

        }

        private static void HandleSqlException(ref HttpResponse response, ref object resp, SqlException exp)
        {
            if (exp.Message.StartsWith("User") && (exp.Message.Contains("not found") || exp.Message.Contains("not valid") || exp.Message.Contains("not active")))
            {
                response.StatusCode = 401;
                resp = new ErrorMessage(exp.Message);
            }
            else
                response.StatusCode = 500;




            if (exp.Number == 50000)
            {
                response.StatusCode = 409;
                resp = new ErrorMessage(exp.Message);
            }
            else
            {
                response.StatusCode = 500;
                resp = new ErrorMessage("Server Error");
            }
        }

        public static ConnectionInformation GetConnection(WebApplication app, string userAgent)
        {
            return new ConnectionInformation()
            {
                Server = app.Configuration[$"AppCodes:P:Server"],
                UserName = app.Configuration[$"AppCodes:P:UserName"],
                Password = app.Configuration[$"AppCodes:P:Password"],
                DatabaseName = app.Configuration[$"AppCodes:P:DatabaseName"],
                AppName = app.Configuration[$"AppCodes:P:AppName"],
                IncludeUserToAppName = Convert.ToBoolean(app.Configuration[$"AppCodes:P:IncludeUserToAppName"]),
                ModuleName = userAgent
            };
        }
    }

}
