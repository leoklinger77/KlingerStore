using KlingerStore.Payment.Domain.Class;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KlingerStore.Payment.Data.Mappings
{
    public class TransactionMapping : IEntityTypeConfiguration<Domain.Class.Transaction>
    {        

        public void Configure(EntityTypeBuilder<Transaction> builder)
        {
            builder.HasKey(c => c.OrderId);

            builder.ToTable("TB_Transaction");
        }
    }
}
