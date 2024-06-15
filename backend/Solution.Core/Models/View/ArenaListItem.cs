namespace Solution.Core.Models.View;

public class ArenaListItem
{
    public int Id { get; set; }

    public string Name { get; set; }

    public int NumberOfHeroes { get; set; }

    public string Winner { get; set; }

    public List<string> Logs { get; set; }
}
