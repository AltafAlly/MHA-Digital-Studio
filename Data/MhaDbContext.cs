using Microsoft.EntityFrameworkCore;
using MHAWebDesign.Models;

namespace MHAWebDesign.Data;

public class MhaDbContext : DbContext
{
    public MhaDbContext(
        DbContextOptions<MhaDbContext> options)
        : base(options)
    {
    }


    public DbSet<Lead> Leads { get; set; }
        = null!;
}