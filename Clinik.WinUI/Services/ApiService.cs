using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Clinik.WinUI.Services
{
    class ApiService
    {

        
        public static dynamic DoRequest(string Method,string Api, object payload)
        {
            try
            {
                string ApiURL = Properties.Settings.Default.ApiURL;
                string url = $"https://{ApiURL}/api/{Api}";

                // Serialize payload to JSON
                string jsonPayload = JsonConvert.SerializeObject(payload);

                // Create the request
                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = Method;
                httpRequest.ContentType = "application/json";
                httpRequest.Accept = "application/json";

                // Add the payload to the request
                using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
                {
                    streamWriter.Write(jsonPayload);
                    streamWriter.Flush();
                    streamWriter.Close();
                }

                // Get the response
                using (var httpResponse = (HttpWebResponse)httpRequest.GetResponse())
                {
                    using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        string result = streamReader.ReadToEnd();
                        return JsonConvert.DeserializeObject<dynamic>(result); ; // Return the response body
                    }
                }
            }
            catch (WebException ex)
            {
                // Handle web exception
                if (ex.Response != null)
                {
                    using (var errorResponse = (HttpWebResponse)ex.Response)
                    using (var reader = new StreamReader(errorResponse.GetResponseStream()))
                    {
                        string errorBody = reader.ReadToEnd();
                        Console.WriteLine($"Error Response: {errorBody}");
                        throw new ApplicationException($"HTTP Error: {errorResponse.StatusCode}, Body: {errorBody}", ex);
                    }
                }
                else
                {
                    throw new ApplicationException("An error occurred while making the request.", ex);
                }
            }
            catch (Exception ex)
            {
                // Handle generic exceptions
                Console.WriteLine($"Exception: {ex.Message}");
                throw;
            }


        }


        public static DataTable JsonArrayToDataTable(string jsonArray)
        {
            // Parse JSON array
            JArray array = JArray.Parse(jsonArray);

            // Create DataTable
            DataTable dataTable = new DataTable();

            if (array.First != null)
            {
                foreach (JProperty property in array.First?.Children<JProperty>())
                {
                    if (property.Value.Type != JTokenType.Array)
                    {
                        dataTable.Columns.Add(property.Name, typeof(string));
                    }
                }

                // Add rows to DataTable
                foreach (JObject obj in array)
                {
                    DataRow row = dataTable.NewRow();
                    foreach (JProperty property in obj.Properties())
                    {
                        if (property.Value.Type != JTokenType.Array)
                        {
                            if (property.Value.Type == JTokenType.Date)
                                row[property.Name] = ((DateTime)property.Value).ToString("dd.MM.yyyy HH:mm");
                            else
                                row[property.Name] = property.Value;
                        }
                    }
                    dataTable.Rows.Add(row);
                }
            }

            return dataTable;
        }
    }
}
