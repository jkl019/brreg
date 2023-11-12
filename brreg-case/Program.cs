using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using Customers;
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
    string url = "https://data.brreg.no/enhetsregisteret/api/enheter/";
    url = url + orgNo;

    Customer customer = await ApiHelper.GetCustomerFromApi(url);

    if (customer != null)
    {
        Console.WriteLine($"OrgNummer: {customer.organisasjonsnummer}");
        Console.WriteLine($"Name: {customer.navn}");
        Console.WriteLine($"Antall Ansatte: {customer.antallAnsatte}");
        Console.WriteLine($"Næringskode: {customer.naeringskode1.kode}");
        Console.WriteLine($"Organisasjonsform: {customer.organisasjonsform.kode}");
    }
    else
    {
        Console.WriteLine("Failed to retrive data.");
    }

}
