using System.Globalization;

namespace SajamKnjiga.ConsoleView
{
    // Preuzeto iz CRUDExample (Vezbe 3)
    public static class ConsoleViewUtils
    {
        public static int SafeInputInt()
        {
            int input;

            string rawInput = Console.ReadLine() ?? string.Empty;

            while (!int.TryParse(rawInput, out input))
            {
                Console.WriteLine("Nije validan broj, pokusajte ponovo: ");

                rawInput = Console.ReadLine() ?? string.Empty;
            }

            return input;
        }

        // Po uzoru na SafeInputInt
        public static DateTime SafeInputDate()
        {
            DateTime input;

            string rawInput = Console.ReadLine() ?? string.Empty;

            while (!DateTime.TryParseExact(rawInput, "dd.MM.yyyy.", CultureInfo.InvariantCulture,
                       DateTimeStyles.None, out input))
            {
                Console.WriteLine("Nije validan datum (format dd.MM.yyyy.), pokusajte ponovo: ");

                rawInput = Console.ReadLine() ?? string.Empty;
            }

            return input;
        }
    }
}