namespace SoclukProject.Common.Models;

public enum VoteType
{
    None = -1,
    UpVote = 1,
    DownVote = 0
}
public static class VoteExtensions
{
    public static string GetVoteType(this VoteType type)
    {
        return type.ToString();
    }
}