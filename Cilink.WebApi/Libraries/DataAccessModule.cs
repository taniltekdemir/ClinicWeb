using Cilink.WebApi.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Text;

namespace Cilink.WebApi.Libraries
{
    public static class DataAccessModule
    {
        public static DataTable GetTable(string SpName, GenericRequest request, ConnectionInformation ci)
        {
            DataTable result = new();
            using (SqlConnection con = new(ci.GetConnectionString()))
            {
                SqlCommand cmd = con.CreateCommand();
                SetInitialComments(
                    con,
                    UserToken: string.Format("{0}", request.Token),
                    Language: string.Format("{0}", request.Language));
                try
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = SpName;
                    //cmd.CommandTimeout = 999999999;
                    if (request.Parameters != null)
                    {
                        foreach (KeyValuePair<string, object> parameter in request.Parameters)
                        {
                            if (parameter.Value != null)
                                cmd.Parameters.AddWithValue(parameter.Key.ToString(), parameter.Value.ToString());
                            else
                                cmd.Parameters.AddWithValue(parameter.Key.ToString(), DBNull.Value);
                        }
                    }
                    new SqlDataAdapter(cmd).Fill(result);

                }
                catch (Exception exp)
                {
                    // LOGGING 
                    throw;
                }
                finally
                {
                    if (con != null)
                    {
                        if (con.State == ConnectionState.Open)
                            con.Close();
                    }
                }
            }
            return result;
        }
        public static object Login(AuthRequest request, ConnectionInformation ci, string ip)
        {
            //object result = null;
            DataTable tableResult = new();

            using (SqlConnection con = new(ci.GetConnectionString()))
            {

                SqlCommand cmd = con.CreateCommand();
                try
                {

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.CommandText = "core.Login";
                    cmd.Parameters.AddWithValue("LoginName", request.UID);
                    cmd.Parameters.AddWithValue("Password", request.PASS);
                    cmd.Parameters.AddWithValue("IP", ip);


                    new SqlDataAdapter(cmd).Fill(tableResult);
                    //result = tableResult;

                }
                catch (SqlException exp)
                {

                    tableResult.Columns.Add("Error", typeof(string));
                    tableResult.Rows.Add(new object[] { exp.Message });
                    //result = tableResult;
                    //throw exp;
                }
                catch (Exception exp)
                {
                    tableResult.Columns.Add("Error", typeof(string));
                    tableResult.Rows.Add(new object[] { exp.Message });
                    //result = tableResult;
                    //throw exp;
                }
                finally
                {
                    if (con != null)
                    {
                        if (con.State == ConnectionState.Open)
                            con.Close();
                    }
                }
            }

            return tableResult;
        }
        public static object Logout(string Token, ConnectionInformation ci)
        {
            //object result = null;@
            DataTable tableResult = new();

            using (SqlConnection con = new(ci.GetConnectionString()))
            {

                SqlCommand cmd = con.CreateCommand();
                try
                {

                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.CommandText = "core.Logout";
                    cmd.Parameters.AddWithValue("Token", Token.ToString());

                    new SqlDataAdapter(cmd).Fill(tableResult);
                    //result = tableResult;

                }
                catch (SqlException exp)
                {
                    tableResult.Columns.Add("Error", typeof(string));
                    tableResult.Rows.Add(new object[] { exp.Message });
                    //result = tableResult;
                    //throw exp;
                }
                catch (Exception exp)
                {
                    tableResult.Columns.Add("Error", typeof(string));
                    tableResult.Rows.Add(new object[] { exp.Message });
                    //result = tableResult;
                    //throw exp;
                }
                finally
                {
                    if (con != null)
                    {
                        if (con.State == ConnectionState.Open)
                            con.Close();
                    }
                }
            }

            return tableResult;
        }
        public static object TokenCheck(string Token, ConnectionInformation ci)
        {
            //object result = null;
            DataTable tableResult = new();

            using (SqlConnection con = new(ci.GetConnectionString()))
            {

                SqlCommand cmd = con.CreateCommand();
                try
                {

                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "core.TokenCheck";
                    cmd.Parameters.AddWithValue("Token", Token.ToString());

                    new SqlDataAdapter(cmd).Fill(tableResult);
                    //result = tableResult;

                }
                catch (SqlException exp)
                {
                    tableResult.Columns.Add("Error", typeof(string));
                    tableResult.Rows.Add(new object[] { exp.Message });
                    //result = tableResult;
                    //throw exp;
                }
                catch (Exception exp)
                {
                    tableResult.Columns.Add("Error", typeof(string));
                    tableResult.Rows.Add(new object[] { exp.Message });
                    //result = tableResult;
                    //throw exp;
                }
                finally
                {
                    if (con != null)
                    {
                        if (con.State == ConnectionState.Open)
                            con.Close();
                    }
                }
            }

            return tableResult;
        }
        static void SetInitialComments(SqlConnection con, string UserToken, string Language)
        {
            StringBuilder sb = new();
            sb.AppendLine("set nocount on");
            sb.AppendLine("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");
            sb.AppendLine("SET dateformat ymd");

            if (UserToken != string.Empty)
                sb.AppendLine($"EXECUTE core.Sys_setUserToken '{UserToken}'");

            if (Language != string.Empty)
                sb.AppendLine($"EXECUTE core.Sys_setLanguage '{Language}'");

            SqlCommand command = con.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = sb.ToString();
            if (con.State != ConnectionState.Open)
                con.Open();

            command.ExecuteNonQuery();
        }
    }
}
