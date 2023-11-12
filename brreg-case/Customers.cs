using System.Text.Json.Serialization;
namespace Customers;
public record class Customer(
    [property: JsonPropertyName("organisasjonsnummer")] string OrgNumber,
    [property: JsonPropertyName("navn")] string Name,
    [property: JsonPropertyName("organisasjonsform.kode")] string OrgCode,
    [property: JsonPropertyName("naeringskode1.kode")] string BusCode,
    [property: JsonPropertyName("antallAnsatte")] int Employees,
    [property: JsonPropertyName("slettedato")] string Deleted,
    [property: JsonPropertyName("konkurs")] bool Bankrupt
);
    