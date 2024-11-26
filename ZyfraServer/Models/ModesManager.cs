using Microsoft.EntityFrameworkCore;


namespace ZyfraServer.Models
{
    public partial class ModelsManager : DbContext
    {
        protected readonly IConfiguration Configuration;
        public ModelsManager(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
        }
        public virtual DbSet<ZyfraData> ZyfraData { get; set; }

        protected override void OnModelCreating(ModelBuilder
        modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ZyfraData>(entity =>
            {
                entity.Property(e => e.Value).IsRequired();
            });
        }
    }
}
