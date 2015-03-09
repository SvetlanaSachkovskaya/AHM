using System.Data.Entity;
using AHM.Common.DomainModel;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AHM.DataLayer
{
    public class AhmContext : IdentityDbContext<User, Role, int, UserLogin, UserRole, UserClaim>
    {
        static AhmContext()
        {
            Database.SetInitializer<AhmContext>(null);        
        }

        public AhmContext()
            : base("AhmContext")
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }


        public static AhmContext Create()
        {
            return new AhmContext();
        }


        public DbSet<Building> Buildings { get; set; }
        public DbSet<Apartment> Apartments { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<PackageType> PackageTypes { get; set; }
        public DbSet<Occupant> Occupants { get; set; }
        public DbSet<Package> Packages { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<Instruction> Instructions { get; set; }
        public DbSet<UtilitiesClause> UtilitiesClauses { get; set; }
        public DbSet<UtilitiesItem> UtilitiesItems { get; set; }
        public DbSet<Bill> Bills { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UtilitiesItem>()
                .HasRequired(i => i.Bill)
                .WithMany()
                .HasForeignKey(i => i.BillId)
                .WillCascadeOnDelete(false);
            base.OnModelCreating(modelBuilder);
        }
    }
}