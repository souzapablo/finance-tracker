using Bogus;
using FinanceTracker.Api.Features.Accounts;

namespace FinanceTracker.UnitTests.Faker;
internal class AccountFaker
{
    public static Account Generate(Guid? id) =>
    new Faker<Account>("pt_BR")
        .CustomInstantiator(f => new Account(
            f.Company.CompanyName(),
            id ?? Guid.NewGuid()
        ))
        .Generate();
}
