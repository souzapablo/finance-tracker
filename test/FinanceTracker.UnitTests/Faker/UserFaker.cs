using Bogus;
using FinanceTracker.Api.Features.Users;

namespace FinanceTracker.UnitTests.Faker;
internal class UserFaker
{
    public static User Generate() =>
    new Faker<User>("pt_BR")
        .CustomInstantiator(f => new User(
            f.Person.UserName,
            f.Person.Email,
            f.Person.FirstName,
            f.Person.LastName
        ))
        .Generate();
}
