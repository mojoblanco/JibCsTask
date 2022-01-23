using Jibble.Core.Helpers;

ConsoleKeyInfo key;
do
{
    AppHelper.DisplayMainMenu();
    

    var selectedInput = Console.ReadLine();
    int.TryParse(selectedInput, out int selectedId);

    var selectedOption = AppHelper.GetSelectedMenuOption(selectedId);

    if (selectedOption == null)
        UIHelpers.DisplayOutput("Invalid option selected", ConsoleColor.Red);
    else
        selectedOption.ActionHandler();


    Console.WriteLine("Press r/R to restart, Any other key to quit ");
    key = Console.ReadKey();
} while (key.Key == ConsoleKey.R);
