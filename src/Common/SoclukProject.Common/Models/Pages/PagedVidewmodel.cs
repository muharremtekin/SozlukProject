namespace SoclukProject.Common.Models.Pages;
public class PagedViewmodel<T> where T : class
{
    public IList<T> Results { get; set; }
    public Page PageInfo { get; set; }
    public PagedViewmodel() : this(new List<T>(), new Page())
    {

    }
    public PagedViewmodel(IList<T> results, Page pageInfo)
    {
        Results = results;
        PageInfo = pageInfo;
    }
}

