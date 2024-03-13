namespace SoclukProject.Common.Models.ViewModels;
public class BaseFootherRateViewmodel
{
    public VoteType VoteType { get; set; }
}

public class BaseFootherFavoritedViewmodel
{
    public bool IsFavorited { get; set; }
    public int FavoritedCount { get; set; }
}

public class BaseFootherRateFavoritedViewmodel : BaseFootherFavoritedViewmodel
{
    public VoteType VoteType { get; set; }
}