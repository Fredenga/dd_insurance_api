using InsuranceAPI.Auth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InsuranceAPI.Config
{
    public class EmailVerificationTokenConfig : IEntityTypeConfiguration<EmailVerificationToken>
    {
        public void Configure(EntityTypeBuilder<EmailVerificationToken> builder)
        {
            builder.HasKey(e => e.ID);
            builder.HasOne(e => e.Admin).WithMany().HasForeignKey(e => e.userID);

        }
    }
}
