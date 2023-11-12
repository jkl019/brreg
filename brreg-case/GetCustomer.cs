
using System.Net;
using System.Text.Json;
using Customers;
using Logging;

namespace brregApi {
    public static class ApiHelper
    {
        public static readonly HttpClient client = new HttpClient();
        public static string url = "https://data.brreg.no/enhetsregisteret/api/enheter/";

        public static async Task<Customer?> GetCustomerFromApi(string orgNo)
        {
            string apiUrl = url + orgNo;
            try
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();
                
                string responseBody = await response.Content.ReadAsStringAsync();
                if (responseBody != null)
                {
                    Customer? customer = JsonSerializer.Deserialize<Customer>(responseBody);
                    if (customer?.Deleted != null)
                    {
                        _ = new Error(orgNo, response.StatusCode, "Deleted");
                        return null;
                    }
                    if (customer?.Bankrupt == true)
                    {
                        _ = new Error(orgNo, response.StatusCode, "Bankrupt");
                        return null;
                    }
                    return customer;
                }
                _ = new Error(orgNo, response.StatusCode, "Empty response");
                return null;
            }
            catch (HttpRequestException ex)
            {
                var statusCode = ex.StatusCode;
                if (statusCode != null){
                    _ = new Error(orgNo, (HttpStatusCode)statusCode, "Request failed");
                }
                return null;
            }
        }
    }
    
}