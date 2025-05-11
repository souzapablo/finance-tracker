using FinanceTracker.Api.Entities;
using FinanceTracker.Api.Features.Accounts;
using FinanceTracker.Api.Features.Users;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace FinanceTracker.Api.Infra.Data;

public class FinanceTrackerDbContext(DbContextOptions<FinanceTrackerDbContext> options)
    : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Account> Accounts { get; set; }
    public DbSet<Budget> Budgets { get; set; }
    public DbSet<Card> Cards { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Installment> Installments { get; set; }
    public DbSet<Statement> Statements { get; set; }
    public DbSet<Subscription> Subscriptions { get; set; }
    public DbSet<Transaction> Transactions { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
}
