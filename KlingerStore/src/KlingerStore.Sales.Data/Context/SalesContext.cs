using KlingerStore.Core.Domain.Communication.Mediatr;
using KlingerStore.Core.Domain.Data.Interfaces;
using KlingerStore.Core.Domain.Message;
using KlingerStore.Sales.Domain.Class;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace KlingerStore.Sales.Data.Context
{
    public class SalesContext : DbContext, IUnitOfWork
    {
        private readonly IMediatrHandler _mediatrHandler;
        public SalesContext(DbContextOptions<SalesContext> options, IMediatrHandler mediatrHandler) : base(options)
        {
            _mediatrHandler = mediatrHandler;
        }

        public DbSet<Order> Order { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }
        public DbSet<Voucher> Voucher { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SalesContext).Assembly);

            foreach (var property in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetProperties().Where(p => p.ClrType == typeof(string))))
                property.SetColumnType("varchar(255)");

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
                relationship.DeleteBehavior = DeleteBehavior.ClientSetNull;

            modelBuilder.Ignore<Event>();

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(SalesContext).Assembly);

            modelBuilder.HasSequence<int>("MinhaSequencia").StartsAt(1000).IncrementsBy(1);
            base.OnModelCreating(modelBuilder);
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

            var success = await base.SaveChangesAsync() > 0;
            if(success) await _mediatrHandler.SendEvent(this);
            
            return success;
        }
    }
}
