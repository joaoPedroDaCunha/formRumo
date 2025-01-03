using Microsoft.EntityFrameworkCore;
using Rumo.Models;

namespace Rumo.Data;

public class Context(DbContextOptions<Context> options) : DbContext(options)
{
    public required DbSet<Vehicle> Vehicles {get;set;}
    public required DbSet<Aet> Aets {get;set;}
    public required DbSet<Verser> Versers {get;set;}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder.UseNpgsql("Host=localhost:5432;Database=novorumo;Username=postgres;Password=102030"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Vehicle>().HasIndex(p => p.Renavam).IsUnique(true);

        modelBuilder.Entity<Vehicle>().HasIndex(p => p.Chassis).IsUnique(true);

        modelBuilder.Entity<Aet>().HasOne(a => a.Vehicle).WithMany(u => u.Aets).HasForeignKey(a => a.VehicleId);

        modelBuilder.Entity<Aet>().HasKey(a => a.id);

        modelBuilder.Entity<Verser>().HasOne(a => a.Vehicle).WithMany(u => u.Versers).HasForeignKey(a => a.VehicleId);

        modelBuilder.Entity<Verser>().HasOne(a => a.Aet).WithMany(a => a.Versers).HasForeignKey(a => a.AetId);

        modelBuilder.Entity<Verser>().HasKey(a => a.id);

        base.OnModelCreating(modelBuilder);
    }
}