using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KlingerStore.Payment.Data.Mappings
{
    public class PaymentMapping : IEntityTypeConfiguration<Domain.Class.Payment>
    {
        public void Configure(EntityTypeBuilder<Domain.Class.Payment> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.NameCart)
                .IsRequired()
                .HasColumnType("varchar(250)");

            builder.Property(c => c.NumberCart)
                .IsRequired()
                .HasColumnType("varchar(16)");

            builder.Property(c => c.ExpiracaoCart)
                .IsRequired()
                .HasColumnType("varchar(10)");

            builder.Property(c => c.CvvCart)
                .IsRequired()
                .HasColumnType("varchar(4)");

            builder.HasOne(c => c.Transaction)
                .WithOne(c => c.Payment);

            builder.ToTable("TB_Payment");
        }
    }
}
