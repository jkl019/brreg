using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using brregApi;

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

foreach (var orgNo in orgNoList)
{

    var customer = await ApiHelper.GetCustomerFromApi(orgNo);

    if (customer != null)
    {
        Console.WriteLine($"OrgNummer: {customer?.OrgNumber}");
        Console.WriteLine($"Name: {customer?.Name}");
        Console.WriteLine($"Antall Ansatte: {customer?.Employees}");
        Console.WriteLine($"Næringskode: {customer?.BusCode}");
        Console.WriteLine($"Organisasjonsform: {customer?.OrgCode}");
    }
    else
    {
        Console.WriteLine("Failed to retrive data.");
    }

}
