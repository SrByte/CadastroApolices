

using Microsoft.EntityFrameworkCore;
using MongoDB.Bson.Serialization;
using MongoDB.EntityFrameworkCore.Extensions;
using WebMongoAPI.Models;

namespace WebMongoAPI;

public class MongoContext : DbContext
{
    public MongoContext(DbContextOptions options)
        : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Seguro> Seguros { get; set; }
    //public DbSet<Beneficiario> Beneficiarios { get; set; }
    //public DbSet<Cobertura> Coberturas { get; set; }



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<User>().ToCollection("User");
        modelBuilder.Entity<Seguro>().ToCollection("Seguro");

    }
}