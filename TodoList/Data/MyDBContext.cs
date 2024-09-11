using Microsoft.EntityFrameworkCore;

namespace TodoList.Data
{
    public class MyDBContext : DbContext
    {
        public MyDBContext(DbContextOptions options) : base(options) { }

        #region Dbset
        public DbSet<Job> Jobs { get; set; }
        #endregion
    }
}
