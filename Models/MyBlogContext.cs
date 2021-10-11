using Microsoft.EntityFrameworkCore;

namespace entity_fr.models{
    public class MyBlogContext:DbContext
    {
        public MyBlogContext( DbContextOptions<MyBlogContext> options) : base(options)
        {
            //
        }
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            base.OnConfiguring(builder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        // Tao bảng có dữ liệu của class Article
        public DbSet<Article> articles { get; set; }

    }
}