
using System.Text.Json;
using Customers;

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
                        return null;
                    }
                    return customer;
                }
                return null;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }
    }
    
}