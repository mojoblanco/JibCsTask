namespace Jibble.Core.Helpers
{
    public class UIHelpers
    {
        public static string ReadInput(string label)
        {
            Console.Write(String.Format("{0}: ", label));
            var input = Console.ReadLine();

            return input;
        }

        public static void DisplayOutput(string output, ConsoleColor color = ConsoleColor.Green)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(output);
            Console.ResetColor();
        }
    }
}
