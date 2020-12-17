using BugTracker.Api.Model;
using Microsoft.EntityFrameworkCore;

namespace BugTracker.Api
{
    /// <summary>
    /// Контекст для БД багтрекера.
    /// </summary>
    public class BugTrackerDbContext : DbContext
    {

        /// <summary>
        /// Конструктор контекста
        /// </summary>
        /// <param name="options"></param>
        public BugTrackerDbContext(DbContextOptions<BugTrackerDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Заявки
        /// </summary>
        public DbSet<Issue> Issues { get; set; }


        /// <summary>
        /// Вехи
        /// </summary>
        public DbSet<Milestone> Milestones { get; set; }

        /// <summary>
        /// настройка модели для БД
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Issue>().HasKey(x => x.Id);
            modelBuilder.Entity<Issue>().Property(x => x.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Milestone>().HasKey(x => x.Id);
            modelBuilder.Entity<Milestone>().Property(x => x.Id).ValueGeneratedOnAdd();
        }
    }
}