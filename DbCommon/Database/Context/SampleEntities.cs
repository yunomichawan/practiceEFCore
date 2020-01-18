using DbCommon.Database.Models;
using Microsoft.EntityFrameworkCore;

namespace DbCommon.Database.Context
{
    public class SampleEntities : DbContext
    {
        /// <summary>
        /// エリアマスタ
        /// </summary>
        public DbSet<MArea> Areas { get; set; }

        /// <summary>
        /// 店舗マスタ
        /// </summary>
        public DbSet<MShop> Shops { get; set; }

        /// <summary>
        /// 売上高(日別)
        /// </summary>
        public DbSet<TDailySales> DailySales { get; set; }

        /// <summary>
        /// 売上高（月別）
        /// </summary>
        public DbSet<TMonthlySales> MonthlySales { get; set; }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SampleEntities()
        {
            
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseMySQL(Properties.Resources.AwsConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // エリアと店舗情報で1:nの関係を作成
            modelBuilder.Entity<MShop>()
                .HasOne(s => s.Area)
                .WithMany(a => a.Shops);
            // HasForeignKeyを使用して任意で外部キーを指定することも可能。
            //.HasForeignKey(s => s.AreaId);

            // 店舗情報と売上高（日別）で1:nの関係を作成
            modelBuilder.Entity<TDailySales>()
                .HasOne(d => d.Shop)
                .WithMany(s => s.SalesDailies);

            // 店舗情報と売上高（月別）で1:nの関係を作成
            modelBuilder.Entity<TMonthlySales>()
                .HasOne(m => m.Shop)
                .WithMany(s => s.SalesMonthlies);
        }
    }
}
