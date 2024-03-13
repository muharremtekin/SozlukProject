namespace SoclukProject.Common.Models.Pages;
public class Page
{
    public Page() : this(0)
    {
    }
    public Page(int totalRowCount) : this(currentPage: 1, pageSize: 10, totalRowCount: totalRowCount)
    {
    }
    public Page(int pageSize, int totalRowCount) : this(currentPage: 1, pageSize: pageSize, totalRowCount: totalRowCount)
    {
    }

    public Page(int currentPage, int pageSize, int totalRowCount)
    {
        if (currentPage < 1)
            throw new ArgumentException("Invalid page number!");
        if (pageSize < 1)
            throw new ArgumentException("Invalid pageSize number!");

        TotalRowCount = totalRowCount;
        CurrentPage = currentPage;
        PageSize = pageSize;
    }

    public int PageSize { get; set; }
    public int CurrentPage { get; set; }
    public int TotalRowCount { get; set; }
    public int TotalPageCount => (int)Math.Ceiling((double)TotalRowCount / PageSize);
    public int Skip => (CurrentPage - 1) * PageSize;
}

