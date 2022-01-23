using Jibble.Core.Dto;
using Jibble.Core.Services;
using RestSharp;
using System.Text;

namespace Jibble.Core.Helpers
{
    public class AppHelper
    {
        private static RestClient _client = new RestClient("http://services.odata.org/TripPinRESTierService");

        public static List<SearchMenuOption> FieldSearchOptions = new List<SearchMenuOption>
        {
            new SearchMenuOption(1, "FirstName"),
            new SearchMenuOption(2, "LastName"),
            new SearchMenuOption(3, "UserName"),
        };

        private static List<MenuOption> _mainMenuOptions = new List<MenuOption>
        {
            new MenuOption(1, "List People", () => FetchPeople()),
            new MenuOption(2, "Search People", () => SearchPeople()),
            new MenuOption(3, "Show Person Details", () => GetPerson()),
            new MenuOption(4, "Edit Person", () => UpdatePerson()),
        };

        public static void DisplayMainMenu()
        {
            Console.Clear();
            Console.WriteLine("Hello!, What would you like to do?");

            foreach (var option in _mainMenuOptions)
            {
                Console.WriteLine($"{option.Id} - {option.Description}");
            }

            Console.Write("Pick a number to perform an action: ");
        }

        public static MenuOption GetSelectedMenuOption(int id)
        {
            return _mainMenuOptions.FirstOrDefault(option => option.Id == id);
        }

        public static void FetchPeople()
        {
            Console.WriteLine("Fetching data. Please wait...");
            var data = DataService.FetchPeople(_client);

            if (data == null)
            {
                UIHelpers.DisplayOutput("Error while fetching people", ConsoleColor.Red);
                return;
            }

            var output = new StringBuilder();
            foreach (var item in data.People)
            {
                output.Append(String.Format("FirstName: {0} | LastName: {1} | Username: {2} {3}", item.FirstName, item.LastName, item.UserName, Environment.NewLine));
            }

            UIHelpers.DisplayOutput(output.ToString());
        }

        public static void SearchPeople()
        {
            DisplayFieldSearchMenu();
            var selectedInput = UIHelpers.ReadInput("Select the field ou want to search by");
            int.TryParse(selectedInput, out int selectedId);

            var option = FieldSearchOptions.FirstOrDefault(x => x.Id == selectedId);

            if (option == null)
            {
                UIHelpers.DisplayOutput("Invalid option selected", ConsoleColor.Red);
                return;
            }

            var keyword = UIHelpers.ReadInput("Enter the search keyword");

            Console.WriteLine(string.Format("Searching for: {0}. Please wait...", keyword));

            var data = DataService.SearchPeople(_client, option.Field, keyword);

            if (data == null)
            {
                UIHelpers.DisplayOutput("Error sarching for people", ConsoleColor.Red);
                return;
            }

            if (!data.People.Any())
            {
                UIHelpers.DisplayOutput("No results found", ConsoleColor.Yellow);
                return;
            }

            var output = new StringBuilder(string.Format("{0} results found {1}", data.People.Count(), Environment.NewLine));
            foreach (var item in data.People)
            {
                output.Append(String.Format("FirstName: {0} | LastName: {1} | Username: {2} {3}", item.FirstName, item.LastName, item.UserName, Environment.NewLine));
            }

            UIHelpers.DisplayOutput(output.ToString());
        }

        public static void GetPerson()
        {
            Console.Clear();
            var username = UIHelpers.ReadInput("Enter the username of the person you want to view");

            Console.WriteLine(string.Format("Getting details for: {0}. Please wait...", username));

            var data = DataService.GetPerson(_client, username);

            if (data == null)
            {
                UIHelpers.DisplayOutput("Error while getting person details", ConsoleColor.Red);
                return;
            }

            var output = String.Format("FirstName: {0} | LastName: {1} | Username: {2} {3}", data.FirstName, data.LastName, data.UserName, Environment.NewLine);
            UIHelpers.DisplayOutput(output);
        }

        public static void UpdatePerson()
        {
            Console.Clear();
            Console.WriteLine("Enter the following details to update a person info: ");

            var username = UIHelpers.ReadInput("Username");
            var firstName = UIHelpers.ReadInput("FirstName");
            var lastName = UIHelpers.ReadInput("LastName");
            var payload = new UpdatePersonDto(firstName, lastName);

            Console.WriteLine(string.Format("Updating details for: {0}. Please wait...", username));

            var data = DataService.UpdatePerson(_client, username, payload);

            if (data == null)
            {
                UIHelpers.DisplayOutput("Error while updating person details", ConsoleColor.Red);
                return;
            }


            var output = String.Format("Data updated for: {0} successfully}", username);
            UIHelpers.DisplayOutput(output);
        }

        private static void DisplayFieldSearchMenu()
        {
            Console.Clear();
            foreach (var option in FieldSearchOptions)
            {
                Console.WriteLine($"{option.Id} - {option.Field}");
            }
        }
    }
}
