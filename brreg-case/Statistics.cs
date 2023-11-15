namespace Statistics{

    public class Statistic {
        private static int single, other, smallAS, mediumAS, largeAS, total = 0;

        public static void UpdateStatistics(string orgCode, int employees)
        {
            total += 1;
            if(orgCode == "ENK"){
                single += 1;
            }
            else if (orgCode == "AS"){
                CheckEmployees(employees);
            }
            else {
                other += 1;
            }
        }

        private static void CheckEmployees(int employees)
        {
            if (employees > 10){
                    largeAS += 1;
                }
                else if (employees >= 5){
                    mediumAS += 1;
                }
                else {
                    smallAS += 1;
                }
        }

        public static void PrintStatistics()
        {
            Console.WriteLine("ENK - antall: " + single + " prosent: " + (float)single/total*100 + "%");
            Console.WriteLine("ANDRE - antall: " + other + " prosent: " + (float)other/total*100 + "%");
            Console.WriteLine("AS 0-4 ansatte - antall: " + smallAS + " prosent: " + (float)smallAS/total*100 + "%");
            Console.WriteLine("AS 5-10 ansatte - antall: " + mediumAS + " prosent: " + (float)mediumAS/total*100 + "%");
            Console.WriteLine("AS > 10 ansatte - antall: " + largeAS + " prosent: " + (float)largeAS/total*100 + "%");
        }

    }
}