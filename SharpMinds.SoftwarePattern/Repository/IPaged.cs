namespace SharpMinds.SoftwarePattern.Repository;

public interface IPage<T>
{
    public int CurrentPage { get; set; }
    public int NextPage { get; set; }
    public int PageCount { get; set; }
    public int PageSize { get; set; }
    public int Total { get; set; }
    public bool HasMore { get; set; }
    public ICollection<T> Content { get; set; }
}
public class Page<T> : IPage<T>
{
    public int CurrentPage { get; set; }
    public int NextPage { get; set; }
    public int PageCount { get; set; }
    public int PageSize { get; set; }
    public int Total { get; set; }
    public bool HasMore { get; set; }
    public ICollection<T> Content { get; set; }

    public Page()
    {
        CurrentPage = 1;
        NextPage = 2;
        PageCount = 1;
        PageSize = 0;
        Total = 0;
        HasMore = false;
    }
        
    public Page(int currentPage, int nextPage, int pageCount, int pageSize, int total, ICollection<T> content)
    {
        CurrentPage = currentPage;
        NextPage = nextPage;
        PageCount = pageCount;
        PageSize = pageSize;
        Total = total;
        Content = content;
        HasMore = content != null && currentPage * pageSize < pageCount * pageSize;
    }
    

    public static Page<S> CloneEmptyPage<S>(Page<T> page) => new (page.CurrentPage, page.NextPage, page.PageCount, page.PageSize, page.Total, null);
    public static Page<T> EmptyPage() => new(1, 2, 1, 0, 0, new List<T>());
    public static Page<T> SinglePage(ICollection<T> content) => new(1, 2, 1, content.Count, content.Count, content);
}