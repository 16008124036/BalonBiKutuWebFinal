using Microsoft.EntityFrameworkCore;
using HediyelikEsya.Models;

namespace HediyelikEsya.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Urun> Urunler { get; set; }
    }
}