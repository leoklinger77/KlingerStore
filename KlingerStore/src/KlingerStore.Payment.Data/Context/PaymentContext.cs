using KlingerStore.Core.Domain.Communication.Mediatr;
using KlingerStore.Core.Domain.Data.Interfaces;
using KlingerStore.Core.Domain.Message;
using KlingerStore.Payment.Domain.Class;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace KlingerStore.Payment.Data.Context
{
    public class PaymentContext : DbContext, IUnitOfWork
    {
        private readonly IMediatrHandler _mediatrHandler;
        public PaymentContext(DbContextOptions<PaymentContext> options, IMediatrHandler mediatrHandler) : base(options)
        {
            _mediatrHandler = mediatrHandler;
        }

        public DbSet<Domain.Class.Payment> Payment { get; set; }
        public DbSet<Transaction> Transaction { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PaymentContext).Assembly);

            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(255)");

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
                relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            modelBuilder.Ignore<Event>();

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PaymentContext).Assembly);
        }

        public async Task<bool> Commit()
        {
            foreach (var entry in ChangeTracker.Entries().Where(entry => entry.Entity.GetType().GetProperty("InsertDate") != null))
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Property("InsertDate").CurrentValue = DateTime.Now;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Property("InsertDate").IsModified = false;
                }
            }            

            var success =  await base.SaveChangesAsync() > 0;
            if (success)
            {
                await _mediatrHandler.SendEvent(this);
            }
            return success;
        }       
    }
}
