using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using brregApi;
using Customers;

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
int single = 0;
int other = 0;
int smallAs = 0;
int mediumAs = 0;
int largeAs = 0;
using (var reader = new StreamReader(csvPath))
using (var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture) { Delimiter = "," }))
{
    csv.Read();
    csv.ReadHeader();
    while (csv.Read())
    {
        var OrgCode = csv.GetField<string>("OrgCode");
        var Employees = csv.GetField<int>("Employees");
        if (OrgCode != null)
        {
            if(OrgCode == "ENK"){
                single += 1;
            }
            else if (OrgCode == "AS"){
                if (Employees > 10){
                    largeAs += 1;
                }
                else if (Employees >= 5){
                    mediumAs += 1;
                }
                else {
                    smallAs += 1;
                }
            }
            else {
                other += 1;
            }
        }
    }
}
Console.WriteLine("ENK Antall: " + single);
Console.WriteLine("ANDRE Antall: " + other);
Console.WriteLine("AS 0-4 ansatte Antall: " + smallAs);
Console.WriteLine("AS 5-10 ansatte Antall: " + mediumAs);
Console.WriteLine("AS > 10 ansatte Antall: " + largeAs);




