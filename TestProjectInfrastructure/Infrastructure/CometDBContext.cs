using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.Extensions.Logging;
using System.Diagnostics.Metrics;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Reflection.Emit;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Reflection.Metadata;
using TestProject.Infrastructure.Models;


namespace TestProject.Infrastructure;
public class CometDBContext : DbContext
{

    public CometDBContext(DbContextOptions options) : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Comet>().HasOne(x => x.Recclass).WithMany().HasForeignKey(x => x.RecclassID);
        base.OnModelCreating(modelBuilder);
    }

    public virtual DbSet<Comet> Comets { get; set; }
    public virtual DbSet<Recclass> Recclasses { get; set; }
}