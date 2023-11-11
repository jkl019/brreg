
class Program
{
    static void Main()
    {
        string filePath = Path.Combine(@"..\..\..\files\po-kunder.csv");

        using (var reader = new StreamReader(filePath))
        {
            while (!reader.EndOfStream)
            {
                var line = reader.ReadLine();
                var values = line.Split(';');

                foreach (var value in values)
                {
                    Console.Write(value + " ");
                }

                Console.WriteLine();
            }
        }
    }
}
