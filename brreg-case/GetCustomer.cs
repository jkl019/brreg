
using System.Text.Json;
using Customers;

namespace brregApi {
    public static class ApiHelper
    {
        public static readonly HttpClient client = new HttpClient();

        public static async Task<Customer?> GetCustomerFromApi(string apiUrl)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode();
                
                string responseBody = await response.Content.ReadAsStringAsync();
                Customer? customer = JsonSerializer.Deserialize<Customer>(responseBody);

                if (customer?.slettedato != null)
                {
                    return null;
                }

                return customer;
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }
    }
    
}