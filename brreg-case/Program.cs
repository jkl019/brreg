using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using brregApi;
using Customers;

string filePath = Path.Combine(@"..\..\..\files\po-kunder.csv");

var orgNoList = new List<string>();

using (var reader = new StreamReader(filePath))
using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture) { Delimiter = ";" }))
{
    csv.Read();
    csv.ReadHeader();
    while (csv.Read())
    {
        var orgNumber = csv.GetField<string>("OrgNo");
        if (orgNumber != null)
        {
            orgNoList.Add(orgNumber);
        }
    }
}
var customersList = new List<Customer>();
foreach (var orgNo in orgNoList)
{

    var customer = await ApiHelper.GetCustomerFromApi(orgNo);

    if (customer != null)
    {
        customersList.Add(customer);
    }
    else
    {
        Console.WriteLine("Failed to retrive data.");
    }

}
Console.WriteLine(customersList.Count);
