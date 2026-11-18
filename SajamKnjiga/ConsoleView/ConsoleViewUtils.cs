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
    }
}