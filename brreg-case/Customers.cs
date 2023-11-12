namespace Customers;
public class Customer
    {
        public string? organisasjonsnummer { get; set; }
        public string? navn { get; set; }
        public Organisasjonsform? organisasjonsform { get; set; }  
        public Naeringskode1? naeringskode1 { get; set; }
        public int antallAnsatte { get; set; }
        public string? slettedato { get; set; }
        public bool konkurs { get; set; }
    }

    public class Organisasjonsform
    {
        public string? kode { get; set; }
    }

    public class Naeringskode1
    {
        public string? kode { get; set; }
    }