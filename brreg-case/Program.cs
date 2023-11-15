using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using brregApi;
using Customers;
using Statistics;

string filePath = Path.Combine(@"..\..\..\files\po-kunder.csv");

var orgNoList = new List<string>();

Console.WriteLine("Reading " + filePath);
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
Console.WriteLine("Finished reading csv...");
var customersList = new List<Customer>();

Console.WriteLine("Calling API...");
foreach (var orgNo in orgNoList)
{

    var customer = await ApiHelper.GetCustomerFromApi(orgNo);

    if (customer != null)
    {
        customersList.Add(customer);
    }

}
Console.WriteLine("Finished API Calls...");
Console.WriteLine("Number of successfully calls: " + customersList.Count);


string csvPath = @"po-kunder-ny.csv";
Console.WriteLine("Writing to: " + csvPath);
using (var writer = new StreamWriter(csvPath))
using (var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture))
csvWriter.WriteRecords(customersList);
Console.WriteLine("Finished writing to csv...");


Console.WriteLine("Reading " + csvPath);
using (var reader = new StreamReader(csvPath))
using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture) { Delimiter = "," }))
{
    csv.Read();
    csv.ReadHeader();
    while (csv.Read())
    {
        var orgCode = csv.GetField<string>("OrgCode");
        var employees = csv.GetField<int>("Employees");
        if (orgCode != null){
            Statistic.UpdateStatistics(orgCode, employees);
        }
    }
}

Statistic.PrintStatistics();




