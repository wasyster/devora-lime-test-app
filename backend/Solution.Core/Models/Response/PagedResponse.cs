namespace Solution.Core.Models.Response;

public class PagedResponse<T>
{
    public int Count { get; set; }

    public ICollection<T> Items { get; set; }
}
