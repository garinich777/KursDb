using KursDb.Model.Tables;
using System.Data.Entity;

namespace KursDb.Model
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(string nameOrConnectionString = "DbConnectionString") : base(nameOrConnectionString) 
        { }

        public DbSet<Sole> Soles { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Insole> Insole { get; set; }
        public DbSet<Pattern> Patterns { get; set; }
        public DbSet<Fitting> Fittings { get; set; }
        public DbSet<UpBillet> UpBillets { get; set; }
        public DbSet<ShoeTree> ShoeTrees { get; set; }
        public DbSet<ShoeModel> ShoeModels { get; set; }
        public DbSet<DownBillet> DownBillets { get; set; }
        public DbSet<ModelInOrder> ModelsInOrder { get; set; }
        public DbSet<FittingInModel> FittingsInModel { get; set; }
    }
}
