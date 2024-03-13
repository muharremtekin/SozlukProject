namespace SoclukProject.Common.Models.ViewModels;
public class UserDetailViewmodel
{
    public Guid Id { get; set; }
    public DateTime CreateDate { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string UserName { get; set; }
    public string EmailAddress { get; set; }
    public bool EmailConfirmed { get; set; }
}

