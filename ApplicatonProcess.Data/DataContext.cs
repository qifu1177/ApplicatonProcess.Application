using ApplicatonProcess.Data.Models;
using Microsoft.EntityFrameworkCore;


namespace ApplicatonProcess.Data
{
    public class DataContext: DbContext
    {
        public DataContext(DbContextOptions<DataContext> options):base(options)
        { }
        public DbSet<User> Users { get; set; }
        public DbSet<Asset> Assets { get; set; }
        public DbSet<UserAsset> UserAssets { get; set; }
    }
}
