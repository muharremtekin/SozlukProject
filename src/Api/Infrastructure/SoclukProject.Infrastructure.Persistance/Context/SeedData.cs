using Bogus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SoclukProject.Api.Domain.Models;
using SoclukProject.Common.Infrastructure;

namespace SoclukProject.Infrastructure.Persistance.Context;

public class SeedData
{
    private static List<User> GetUsers()
    {
        var res = new Faker<User>("tr")
                .RuleFor(u => u.Id, u => Guid.NewGuid())
                .RuleFor(u => u.CreateDate, u => u.Date.Between(DateTime.UtcNow.AddDays(-50), DateTime.UtcNow))
                .RuleFor(u => u.FirstName, u => u.Person.FirstName)
                .RuleFor(u => u.LastName, u => u.Person.LastName)
                .RuleFor(u => u.EmailAddress, u => u.Internet.Email())
                .RuleFor(u => u.UserName, u => u.Internet.UserName())
                .RuleFor(u => u.Password, u => PasswordEncryptor.Encrypt(u.Internet.Password()))
                .RuleFor(u => u.EmailConfirmed, u => u.PickRandom(true, false))
            .Generate(500);

        return res;
    }

    public async Task SeedAsync(IConfiguration configuration)
    {
        var dbContextBuilder = new DbContextOptionsBuilder();
        dbContextBuilder.UseNpgsql("Server=localhost;Database=SozlukProject;Port=5432;User ID=postgres;Password=mysecretpassword");

        var context = new SozlukContext(dbContextBuilder.Options);

        var users = GetUsers();
        var userIds = users.Select(u => u.Id);

        await context.Users.AddRangeAsync(users);

        var entryGuids = Enumerable.Range(0, 150).Select(e => Guid.NewGuid()).ToList();
        int counter = 0;
        var entries = new Faker<Entry>("tr")
                .RuleFor(e => e.Id, e => entryGuids[counter++])
                .RuleFor(e => e.CreateDate, e => e.Date.Between(DateTime.UtcNow.AddDays(-50), DateTime.UtcNow))
                .RuleFor(e => e.Subject, e => e.Lorem.Sentence(5, 5))
                .RuleFor(e => e.Content, e => e.Lorem.Paragraph(2))
                .RuleFor(e => e.CreatedById, e => e.PickRandom(userIds))
            .Generate(150);

        await context.Entries.AddRangeAsync(entries);

        var comments = new Faker<EntryComment>("tr")
            .RuleFor(e => e.Id, e => Guid.NewGuid())
            .RuleFor(e => e.CreateDate, e => e.Date.Between(DateTime.UtcNow.AddDays(-50), DateTime.UtcNow))
            .RuleFor(e => e.Content, e => e.Lorem.Paragraph(2))
            .RuleFor(e => e.CreatedById, e => e.PickRandom(userIds))
            .RuleFor(e => e.EntryId, e => e.PickRandom(entryGuids))
            .Generate(1000);

        await context.EntryComments.AddRangeAsync(comments);

        await context.SaveChangesAsync();

    }
}

