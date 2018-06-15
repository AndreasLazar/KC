using System;
using Microsoft.EntityFrameworkCore;

namespace MeineApp.Data
{
    public class KcDbContext : DbContext
    {
        public KcDbContext(DbContextOptions<KcDbContext> options) : base (options) { }

        public DbSet<Speaker> Speakers { get; set; }
        public DbSet<KnowledgeCircle> KnowledgeCircles { get; set; }
    }
}