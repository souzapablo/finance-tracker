using FinanceTracker.Api.Features.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinanceTracker.Api.Infra.Data.Mappings;

public class UserMapping : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.Id);

        builder.HasQueryFilter(u => !u.IsDeleted);

        builder.HasIndex(u => u.Email)
            .IsUnique(); 
        
        builder.HasIndex(u => u.Username)
            .IsUnique();
    }
}
