using System.Text.Json.Serialization;
namespace Customers;
public record class Customer(
    [property: JsonPropertyName("organisasjonsnummer")] string OrgNumber,
    [property: JsonPropertyName("navn")] string Name,
    [property: JsonPropertyName("organisasjonsform")] OrgForm OrgCode,
    [property: JsonPropertyName("naeringskode1")] Business BusCode,
    [property: JsonPropertyName("antallAnsatte")] int Employees,
    [property: JsonPropertyName("slettedato")] string Deleted,
    [property: JsonPropertyName("konkurs")] bool Bankrupt
);


public record class OrgForm(
    [property: JsonPropertyName("kode")] string OrgCode
);

public record class Business(
    [property: JsonPropertyName("kode")] string BusCode
);
    