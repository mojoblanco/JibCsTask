namespace Jibble.Core
{
    public class MenuOption
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public Action ActionHandler { get; }

        public MenuOption(int id, string description, Action handler)
        {
            Id = id;
            Description = description;
            ActionHandler = handler;
        }
    }

    public record SearchMenuOption(int Id, string Field);
}
